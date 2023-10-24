using B2B.Controller;
using B2B.DAO;
using B2B.Model;
using B2B.Util;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class StaffPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            if (!loginSystem.IsStaffLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LoadMethods();
            }
        }
        private void LoadMethods()
        {
            ComboMethod.Items.Clear();
            ComboMethod.Items.Add(new System.Web.UI.WebControls.ListItem("Tutti", "0"));
            ComboMethod.Items.Add(new System.Web.UI.WebControls.ListItem("Contanti", "1"));
            ComboMethod.Items.Add(new System.Web.UI.WebControls.ListItem("Bonifico", "2"));
            ComboMethod.Items.Add(new System.Web.UI.WebControls.ListItem("Paypal", "3"));
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StaffPaymentEdit.aspx");
        }

        protected void BtnDownloadPDF_Click(object sender, EventArgs e)
        {
            string search = TxtSearch.Text;
            string dateFrom = TxtDateFrom.Text;
            string dateTo = TxtDateTo.Text;
            int method = ControlUtil.GetSelectedValue(ComboMethod) ?? 0;

            DateTime? from = null;
            DateTime? to = null;
            if (!string.IsNullOrEmpty(dateFrom))
                from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(dateTo))
                to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            List<Payment> list = new PaymentDAO().SearchBy(from, to, search, method).OrderBy(l => l.Id).ToList();
            List<object> checks = new List<object>();
            foreach (Payment fb in list)
            {
                PaymentCheck check = new PaymentCheck(fb);
                checks.Add(check);
            }

            Document document = new Document();
            document.SetMargins(0, 0, 80, 60);
            // Save the PDF document to a memory stream
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();
            PdfPTable table = new PdfPTable(new float[] { 40f, 80f, 40f, 160f });
            table.TotalWidth = 320f;
            table.HorizontalAlignment = Element.ALIGN_CENTER;

            table.AddCell("Ordine nr.");
            table.AddCell("Data pag.");
            table.AddCell("Importo");
            table.AddCell("Metodo di Pagamento");

            foreach (PaymentCheck check in checks)
            {
                table.AddCell(check.OrderId.ToString());
                table.AddCell(check.DateOfPay);
                table.AddCell(check.Amount.ToString() + " €");
                table.AddCell(check.PaypalTransitionID);
            }
            document.Add(table);

            // Close the PDF document
            document.Close();
            writer.Close();

            // Set the file name and content type
            string fileName = "Payments.pdf";
            string contentType = "application/pdf";

            // Clear the response
            Response.Clear();

            // Set the content type and headers for the response
            Response.ContentType = contentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

            // Write the contents of the memory stream to the response stream
            Response.BinaryWrite(stream.ToArray());

            // Close the memory stream and PDF document
            stream.Close();

            // End the response
            Response.End();
        }
    }
}