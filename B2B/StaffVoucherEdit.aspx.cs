using B2B.Controller;
using B2B.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class StaffVoucherEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Voucher editVoucher;

        public VoucherController voucherController = new VoucherController();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            if (!loginSystem.IsStaffLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            editItemId = ParseUtil.TryParseInt(Request.Params["id"]) ?? 0;
            if (editItemId != 0)
            {
                editVoucher = voucherController.FindBy(editItemId);
            }

            if (!IsPostBack)
            {
                LoadInfo();
            }
        }
        private void LoadInfo()
        {
            if (editVoucher == null)
            {
                //Guid id = Guid.NewGuid();
                TxtSerialNumberGenerator.Text = GenerateSerialNumber();
                return;
            }

            pageTitle.InnerText = "VOUCHER (modifica)";
            TxtAmount.Text = editVoucher.Amount.ToString();
            TxtSerialNumberGenerator.Text = editVoucher.SerialNumberGenerator.ToString();
            TxtNote.Text = editVoucher.Note;
        }
        private string GenerateSerialNumber()
        {
            // Create a new StringBuilder
            StringBuilder sb = new StringBuilder();

            // Create a new Random object
            Random random = new Random();

            // Generate the first three random characters
            for (int i = 0; i < 3; i++)
            {
                char c = Convert.ToChar(random.Next(97, 123)); // ASCII codes for a-z
                sb.Append(c);
            }

            // Append the hyphen
            sb.Append("-");

            // Generate the next three random characters
            for (int i = 0; i < 3; i++)
            {
                char c = Convert.ToChar(random.Next(97, 123)); // ASCII codes for a-z
                sb.Append(c);
            }

            // Return the generated random string
            return sb.ToString();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            bool success = true;

            double amount = ParseUtil.TryParseInt(TxtAmount.Text) ?? 0;
            string note = TxtNote.Text;
            string serialNumber = TxtSerialNumberGenerator.Text;
            int id = 0;
            if (editVoucher != null) { id = editVoucher.Id; }

            success = voucherController.SaveVoucher(id, note, serialNumber, amount);
            if (!success)
            {
                ServerValidator.IsValid = false;
                return;
            }
            Response.Redirect(BtnCancel.PostBackUrl);
        }
    }
}