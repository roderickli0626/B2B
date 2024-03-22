using B2B.Controller;
using B2B.DAO;
using B2B.Model;
using B2B.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class EmployeeViewEdit : System.Web.UI.Page
    {
        private int employeeID;
        private Employment employment;
        private int editItemId;
        private Order editOrder;
        private List<ServiceAllocCheck> serviceAllocList = new List<ServiceAllocCheck>();
        private OrderController orderController = new OrderController();
        private EmployeeController employeeController = new EmployeeController();

        private ServiceDAO serviceDAO = new ServiceDAO();
        private HostDAO hostDAO = new HostDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            employeeID = ParseUtil.TryParseInt(Request.Params["employeeID"]) ?? 0;
            employment = employeeController.FindBy(employeeID);
            if (employment == null)
            {
                Response.Redirect("~/EmployeeLogin.aspx");
                return;
            }

            editItemId = ParseUtil.TryParseInt(Request.Params["id"]) ?? 0;
            if (editItemId != 0)
            {
                editOrder = orderController.FindBy(editItemId);
            }

            if (!IsPostBack)
            {
                if (editItemId != 0)
                {
                    serviceAllocList = orderController.FindServiceByOrder(editItemId);
                    TxtTotalAmount.Text = editOrder.TotalAmount.ToString();
                }
                LoadServiceTable();
                LoadOwners();
                LoadInfo();
                LoadRoom();
                SetVisible();
            }
        }

        private void SetVisible()
        {
            if (editOrder == null)
            {
                pageTitle.InnerText = "ORDINE (nuovo)";
                pageTitle.Attributes.Add("class", "text-dark");
                paymentDiv.Visible = false;
            }
            else if (editOrder.Status == 1)
            {
                pageTitle.InnerText = "Ordine " + editOrder.Id + " (confermato) ";
                pageTitle.Attributes.Add("class", "text-primary");
                paymentDiv.Visible = false;
                ComboOwner.Enabled = false;
                ComboRoom.Enabled = false;
            }
            else if (editOrder.Status == 2)
            {
                pageTitle.InnerText = "ORDINE " + editOrder.Id + " (pagato) ";
                pageTitle.Attributes.Add("class", "text-success");
                ComboOwner.Enabled = false;
                ComboRoom.Enabled = false;
            }
            else if (editOrder.Status == 3)
            {
                pageTitle.InnerText = "ORDINE " + editOrder.Id + " (assegnato) ";
                pageTitle.Attributes.Add("class", "text-warning");
                ComboOwner.Enabled = false;
                ComboRoom.Enabled = false;
            }
            else
            {
                pageTitle.InnerText = "ORDINE " + editOrder.Id + " (chiuso) ";
                pageTitle.Attributes.Add("class", "text-danger");
                ComboOwner.Enabled = false;
                ComboRoom.Enabled = false;
            }

            TxtDateFrom.Enabled = false;
            TxtDateTo.Enabled = false;
            TxtNumberOfGuests.Enabled = false;
            TxtNote.Enabled = false;
        }

        private void LoadInfo()
        {
            if (editOrder == null)
            {
                ControlUtil.SelectValue(ComboOwner, ComboOwner.Items[0].Value);
                return;
            }
            TxtDateFrom.Text = editOrder.StartDate?.ToString("dd/MM/yyyy");
            TxtDateTo.Text = editOrder.EndDate?.ToString("dd/MM/yyyy");
            TxtNote.Text = editOrder.note;
            if (editOrder.Payment != null) TxtPaymentResult.Text = editOrder.Payment.Amount.ToString() + " Pagato, " + editOrder.Payment?.DateOfPay.ToString() ?? "";
            if (editOrder.Voucher != null) TxtVoucher.Text = editOrder.Voucher?.SerialNumberGenerator.ToString() ?? "";
            TxtNumberOfGuests.Text = editOrder.NumberOfGuests.ToString();
            ControlUtil.SelectValue(ComboOwner, editOrder.HostId.ToString());

            username.InnerText = employment.Name;
        }

        private void LoadRoom()
        {
            int ownerId = ControlUtil.GetSelectedValue(ComboOwner) ?? 0;
            List<Room> rooms = new List<Room>();
            RoomDAO roomDao = new RoomDAO();
            rooms = roomDao.FindByOwner(ownerId);
            ControlUtil.DataBind(ComboRoom, rooms, "Id", "Address");
            if (editOrder != null) ControlUtil.SelectValue(ComboRoom, editOrder.RoomId.ToString());
        }

        private void LoadOwners()
        {
            List<Host> hosts = new List<Host>();
            hosts = hostDAO.FindAll().Where(h => h.IsB2BStaff == false).ToList();
            ComboOwner.Items.Clear();
            ControlUtil.DataBind(ComboOwner, hosts, "Id", "Name");
        }

        private void LoadServiceTable()
        {
            ServiceRepeater.DataSource = serviceAllocList;
            ServiceRepeater.DataBind();
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EmployeeView.aspx?id=" + employment.Id);
        }
    }
}