using B2B.Controller;
using B2B.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class AdminVoucherEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Voucher editVoucher;

        public VoucherController voucherController = new VoucherController();
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
                Guid id = Guid.NewGuid();
                TxtSerialNumberGenerator.Text = id.ToString();
                return;
            }

            pageTitle.InnerText = "VOUCHER (modifica)";
            TxtAmount.Text = editVoucher.Amount.ToString();
            TxtSerialNumberGenerator.Text = editVoucher.SerialNumberGenerator.ToString();
            TxtNote.Text = editVoucher.Note;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            bool success = true;

            double amount = ParseUtil.TryParseDouble(TxtAmount.Text) ?? 0;
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