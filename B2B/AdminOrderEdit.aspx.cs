using B2B.Controller;
using B2B.DAO;
using B2B.Model;
using B2B.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class AdminOrderEdit : System.Web.UI.Page
    {
        private int editItemId;
        private bool fromPayment;
        private Order editOrder;
        private List<ServiceAllocCheck> serviceAllocList = new List<ServiceAllocCheck>();
        private OrderController orderController = new OrderController();

        private ServiceDAO serviceDAO = new ServiceDAO();
        private HostDAO hostDAO = new HostDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            if (!loginSystem.IsAdminLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            editItemId = ParseUtil.TryParseInt(Request.Params["id"]) ?? 0;
            fromPayment = ParseUtil.TryParseBool(Request.Params["fromPayment"]) ?? false;
            if (fromPayment) BtnCancel.PostBackUrl = "~/AdminPayment.aspx";

            if (editItemId != 0)
            {
                editOrder = orderController.FindBy(editItemId);
            }

            if (!IsPostBack)
            {
                if (editItemId != 0)
                {
                    HfAssignedIDs.Value = editOrder.EmploymentId;
                    serviceAllocList = orderController.FindServiceByOrder(editItemId);
                    string jsonString = JsonConvert.SerializeObject(serviceAllocList);
                    HfServiceAlloc.Value = jsonString;
                    TxtTotalAmount.Text = editOrder.TotalAmount.ToString();
                }
                LoadGrandService();
                LoadService();
                LoadServiceTable();
                LoadOwners();
                LoadEmployee();
                LoadInfo();
                SetVisible();
            }
        }
        private void LoadGrandService()
        {
            List<GrandService> grandList = new GrandServiceDAO().FindAll();
            ControlUtil.DataBind(ComboGrandService, grandList, "Id", "Title", 0, "");
        }

        private void SetVisible()
        {
            if (editOrder == null)
            {
                pageTitle.InnerText = "ORDINE (New) ";
                pageTitle.Attributes.Add("class", "text-dark");
                paymentDiv.Visible = false;
                assignDiv.Visible = false;
                closeDiv.Visible = false;
            }
            else if (editOrder.Status == 1)
            {
                pageTitle.InnerText = "ORDINE " + editOrder.Id + " (confermato) ";
                pageTitle.Attributes.Add("class", "text-primary");
                paymentDiv.Visible = false;
                assignDiv.Visible = false;
                closeDiv.Visible = false;
                ComboOwner.Enabled = false;
                ComboRoom.Enabled = false;
                if (editOrder.Payment != null)
                {
                    statusDiv.Visible = true;
                }
            }
            else if (editOrder.Status == 2)
            {
                pageTitle.InnerText = "ORDINE " + editOrder.Id + " (pagato) ";
                pageTitle.Attributes.Add("class", "text-success");
                serviceDiv.Visible = false;
                ComboOwner.Enabled = false;
                ComboRoom.Enabled = false;
                closeDiv.Visible = false;
            }
            else if (editOrder.Status == 3)
            {
                pageTitle.InnerText = "ORDINE " + editOrder.Id + " (assegnato) ";
                pageTitle.Attributes.Add("class", "text-warning");
                serviceDiv.Visible = false;
                ComboOwner.Enabled = false;
                ComboRoom.Enabled = false;
            }
            else
            {
                pageTitle.InnerText = "ORDINE " + editOrder.Id + " (chiuso) ";
                pageTitle.Attributes.Add("class", "text-danger");
                serviceDiv.Visible = false;
                ComboOwner.Enabled = false;
                ComboRoom.Enabled = false;
                ComboAssignedTo.Enabled = false;
                RadioButton2.Checked = true;
            }
        }

        private void LoadEmployee()
        {
            List<Employment> employments = new List<Employment>();
            EmploymentDAO empDao = new EmploymentDAO();
            employments = empDao.FindAll();
            ComboAssignedTo.Items.Clear();
            ControlUtil.DataBind(ComboAssignedTo, employments, "Id", "Name");
            ComboAssignedTo.Items.Add(new ListItem("", ""));
        }

        private void LoadInfo()
        {
            if (editOrder == null)
            {
                ControlUtil.SelectValue(ComboOwner, ComboOwner.Items[0].Value);
                LoadRoom();
                return;
            }
            TxtDateFrom.Text = editOrder.StartDate?.ToString("dd/MM/yyyy");
            TxtDateTo.Text = editOrder.EndDate?.ToString("dd/MM/yyyy");
            TxtNote.Text = editOrder.note;
            if (editOrder.Payment != null)
            {
                string payDetails = editOrder.Payment.Note == null ? "(Paypal)" : "(" + editOrder.Payment.Note.ToString() + ")";
                TxtPaymentResult.Text = editOrder.Payment.Amount.ToString() + " €" + payDetails + ", " + editOrder.Payment?.DateOfPay.ToString() ?? "";
            }
            if (editOrder.Voucher != null) TxtVoucher.Text = editOrder.Voucher?.SerialNumberGenerator.ToString() ?? "";
            TxtNumberOfGuests.Text = editOrder.NumberOfGuests.ToString();
            //ControlUtil.SelectValue(ComboAssignedTo, editOrder.EmploymentId);
            ControlUtil.SelectValue(ComboOwner, editOrder.HostId.ToString());
            LoadRoom();
        }

        private void LoadRoom()
        {
            int ownerId = ControlUtil.GetSelectedValue(ComboOwner) ?? 0;
            List<Room> rooms = new List<Room>();
            RoomDAO roomDao = new RoomDAO();
            rooms = roomDao.FindByOwner(ownerId);
            ControlUtil.DataBind(ComboRoom, rooms, "Id", "Address");
            if (editOrder != null) ControlUtil.SelectValue(ComboRoom, editOrder.RoomId.ToString());
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Key", "MyFun()", true);
        }

        private void LoadOwners()
        {
            List<Host> hosts = new List<Host>();
            hosts = hostDAO.FindAll().Where(h => h.IsB2BStaff == false).ToList();
            ComboOwner.Items.Clear();
            ControlUtil.DataBind(ComboOwner, hosts, "Id", "Name");
        }

        private void LoadService()
        {
            int grandServiceID = ControlUtil.GetSelectedValue(ComboGrandService) ?? 0;
            List<Service> services = new List<Service>();
            services = serviceDAO.FindByGrandService(grandServiceID);

            ControlUtil.DataBind(ComboService, services, "Id", "DescriptionShort");
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Key", "MyFun()", true);
        }
        private void LoadServiceTable()
        {
            ServiceRepeater.DataSource = serviceAllocList;
            ServiceRepeater.DataBind();
        }

        protected void BtnAddService_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Key", "MyFun()", true);

            int? addServiceId = ControlUtil.GetSelectedValue(ComboService);
            int? quantity = ParseUtil.TryParseInt(TxtQuantity.Text.Trim());
            int? roomId = ControlUtil.GetSelectedValue(ComboRoom);
            if (addServiceId == null || quantity == null)
            {
                ServerValidator3.IsValid = false;
                return;
            }
            if (roomId == null)
            {
                ServerValidator5.IsValid = false;
                return;
            }
            Room room = new RoomDAO().FindById(roomId ?? 0);
            Service service = serviceDAO.FindById(addServiceId.Value);

            ServiceAllocCheck addService = new ServiceAllocCheck();
            addService.serviceId = addServiceId ?? 0;

            addService.GrandService = service.GrandService.Title;
            addService.Service = service.DescriptionShort;
            addService.Description = service.DescriptionLong;
            addService.Image = service.Image;
            addService.Quantity = quantity ?? 0;
            if (service.HavePriceGroup ?? false)
            {
                double percent = room.PriceListGroup?.Percentuale ?? 0;
                addService.Price = (service.Price ?? 0) * (1 + percent / 100);
                addService.Amount = (quantity ?? 0) * (service.Price ?? 0) * (1 + percent / 100);
            }
            else
            {
                addService.Price = service.Price ?? 0;
                addService.Amount = (quantity ?? 0) * (service.Price ?? 0);
            }
            serviceAllocList = JsonConvert.DeserializeObject<List<ServiceAllocCheck>>(HfServiceAlloc.Value) ?? new List<ServiceAllocCheck>();
            serviceAllocList.Remove(serviceAllocList.Find(s => s.serviceId == addServiceId));
            serviceAllocList.Add(addService);
            string jsonString = JsonConvert.SerializeObject(serviceAllocList);
            HfServiceAlloc.Value = jsonString;
            LoadServiceTable();
            TxtTotalAmount.Text = serviceAllocList.Sum(s => s.Amount).ToString();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            bool success1 = true;
            bool success2 = true;

            DateTime startDate = ParseUtil.TryParseDate(TxtDateFrom.Text) ?? DateTime.Today;
            DateTime endDate = ParseUtil.TryParseDate(TxtDateTo.Text) ?? DateTime.Today.AddDays(30);
            int numberOfGuests = ParseUtil.TryParseInt(TxtNumberOfGuests.Text) ?? 1;
            string note = TxtNote.Text;

            if (editOrder == null)
            {
                int? hostId = ControlUtil.GetSelectedValue(ComboOwner);
                int? roomId = ControlUtil.GetSelectedValue(ComboRoom);
                double totalAmount = ParseUtil.TryParseDouble(TxtTotalAmount.Text) ?? 0;
                DateTime dateCreated = DateTime.Today;
                int status = 1;
                serviceAllocList = JsonConvert.DeserializeObject<List<ServiceAllocCheck>>(HfServiceAlloc.Value) ?? new List<ServiceAllocCheck>();
                if (serviceAllocList.Count() == 0)
                {
                    ServerValidator2.IsValid = false;
                    return;
                }
                if (hostId == null)
                {
                    ServerValidator0.IsValid = false;
                    return;
                }
                if (roomId == null)
                {
                    ServerValidator4.IsValid = false;
                    return;
                }

                int? id = orderController.AddOrder(status, startDate, endDate, numberOfGuests, totalAmount, hostId, roomId, dateCreated, note, null, null);
                if (id != null) success2 = orderController.AddServiceAlloc(id, serviceAllocList);
                else success1 = false;
            }
            else if (editOrder.Status == 1)
            {
                double totalAmount = ParseUtil.TryParseDouble(TxtTotalAmount.Text) ?? 0;
                serviceAllocList = JsonConvert.DeserializeObject<List<ServiceAllocCheck>>(HfServiceAlloc.Value) ?? new List<ServiceAllocCheck>();
                if (serviceAllocList.Count() == 0)
                {
                    ServerValidator2.IsValid = false;
                    return;
                }
                int status = 1;
                if (editOrder.Payment != null)
                {
                    if (PaidStatus.Checked) status = 2;
                }
                success1 = orderController.UpdateOrder(editOrder.Id, status, startDate, endDate, numberOfGuests, totalAmount, null, note, null, null);
                success2 = orderController.AddServiceAlloc(editOrder.Id, serviceAllocList);
            }
            else if (editOrder.Status == 2 || editOrder.Status == 3)
            {
                string[] selectedValues = Request.Form.GetValues(ComboAssignedTo.UniqueID);
                string employeeIds;
                if (selectedValues == null) employeeIds = string.Empty;
                else employeeIds = string.Join(",", selectedValues);

                //int employeeId = ControlUtil.GetSelectedValue(ComboAssignedTo) ?? 0;
                int status = 2;
                //if (employeeId != 0) status = 3;
                if (!string.IsNullOrEmpty(employeeIds)) status = 3;

                if (RadioButton2.Checked) status = 4;

                success1 = orderController.UpdateOrder(editOrder.Id, status, startDate, endDate, numberOfGuests, editOrder.TotalAmount ?? 0, employeeIds, note, null, null);
            }

            if (!success1 || !success2)
            {
                ServerValidator1.IsValid = false;
                return;
            }

            Response.Redirect(BtnCancel.PostBackUrl);

        }

        protected void ComboOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRoom();
        }

        protected void ServiceRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (editOrder != null && (editOrder.Status == 2 || editOrder.Status == 3 || editOrder.Status == 4)) return;
            if (e.CommandName == "Delete")
            {
                int serviceId = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out serviceId))
                {
                    serviceAllocList = JsonConvert.DeserializeObject<List<ServiceAllocCheck>>(HfServiceAlloc.Value);
                    serviceAllocList.Remove(serviceAllocList.Find(s => s.serviceId == serviceId));
                    string jsonString = JsonConvert.SerializeObject(serviceAllocList);
                    HfServiceAlloc.Value = jsonString;
                    LoadServiceTable();
                    TxtTotalAmount.Text = serviceAllocList.Sum(s => s.Amount).ToString();
                }
            }
        }

        protected void ComboGrandService_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadService();
        }
    }
}