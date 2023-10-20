using B2B.Controller;
using B2B.DAO;
using B2B.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class AdminPaymentEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Payment editPayment;

        private PaymentController paymentController = new PaymentController();

        private OrderDAO orderDAO = new OrderDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            if (!loginSystem.IsAdminLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            editItemId = ParseUtil.TryParseInt(Request.Params["id"]) ?? 0;
            if (editItemId != 0)
            {
                editPayment = paymentController.FindBy(editItemId);
            }

            if (!IsPostBack)
            {
                LoadOrder();
                if (editItemId != 0)
                {
                    LoadInfo();
                }
            }
        }

        private void LoadInfo()
        {
            if (editPayment == null) return;

            pageTitle.InnerText = "PAGAMENTO (modifica)";
            ControlUtil.SelectValue(ComboOrder, editPayment.OrderId.ToString());
            TxtAmount.Text = editPayment.Amount.ToString();
            TxtDateOfPay.Text = editPayment.DateOfPay?.ToString("dd/MM/yyyy");
            TxtPaypalTransitionID.Text = editPayment.PaypalTransitionID.ToString();
            TxtNote.Text = editPayment.Note;
        }

        private void LoadOrder()
        {
            List<Order> orderList = orderDAO.FindAll();
            ComboOrder.Items.Clear();
            foreach (Order order in orderList)
            {
                ComboOrder.Items.Add(new ListItem("Ordine nr. " + order.Id, order.Id.ToString()));
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            bool success = true;

            DateTime dateOfPay = ParseUtil.TryParseDate(TxtDateOfPay.Text) ?? DateTime.Today;
            int orderId = ControlUtil.GetSelectedValue(ComboOrder) ?? 0;
            double Amount = ParseUtil.TryParseDouble(TxtAmount.Text) ?? 0;
            string PaypalTransitionID = TxtPaypalTransitionID.Text;
            string note = TxtNote.Text;
            int id = 0;
            if (editPayment != null) id = editPayment.Id;

            success = paymentController.SavePayment(id, dateOfPay, orderId, Amount, PaypalTransitionID, note);
            if (!success)
            {
                ServerValidator.IsValid = false;
                return;
            }
            Response.Redirect(BtnCancel.PostBackUrl);
        }
    }
}