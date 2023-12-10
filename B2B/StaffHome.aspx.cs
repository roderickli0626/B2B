using B2B.Controller;
using B2B.DAO;
using B2B.Model;
using B2B.Util;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class StaffHome : System.Web.UI.Page
    {
        Hashtable HolidayList;

        public OrderController orderController = new OrderController();
        private OrderDAO orderDAO = new OrderDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            if (!loginSystem.IsStaffLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            HolidayList = Getholiday(null, null, "", 0);
            Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
            Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth;
            Calendar1.TitleFormat = TitleFormat.Month;
            Calendar1.ShowGridLines = true;
            Calendar1.DayStyle.Height = new Unit(70);
            Calendar1.DayStyle.Width = new Unit(200);
            Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Center;
            Calendar1.DayStyle.VerticalAlign = VerticalAlign.Middle;
            Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.AliceBlue;

            if (!IsPostBack)
            {
                LoadStatus();
            }
        }
        public void LoadStatus()
        {
            ComboStatus.Items.Clear();
            ComboStatus.Items.Add(new System.Web.UI.WebControls.ListItem("Tutti", "0"));
            ComboStatus.Items.Add(new System.Web.UI.WebControls.ListItem("Confermati", "1"));
            ComboStatus.Items.Add(new System.Web.UI.WebControls.ListItem("Pagati", "2"));
            ComboStatus.Items.Add(new System.Web.UI.WebControls.ListItem("Assegnati", "3"));
            ComboStatus.Items.Add(new System.Web.UI.WebControls.ListItem("Chiusi", "4"));
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StaffOrderEdit.aspx");
        }

        private Hashtable Getholiday(DateTime? startDate, DateTime? endDate, string searchKey, int status)
        {
            Hashtable holiday = new Hashtable();

            List<Order> orders = orderDAO.SearchBy(startDate, endDate, searchKey, status).ToList();

            foreach (Order order in orders)
            {
                string subTitle = "Status: ";

                switch (order.Status)
                {
                    case 1:
                        subTitle += "Confirmed"; break;
                    case 2:
                        subTitle += "Paied"; break;
                    case 3:
                        subTitle += "Assigned"; break;
                    case 4:
                        subTitle += "Closed"; break;
                }

                if (order.EmploymentId != null)
                {
                    List<Employment> employments = new EmployeeController().FindByIDS(order.EmploymentId);
                    if (employments != null)
                    {
                        subTitle += "\nAssgined To: " + string.Join(" ", employments.Select(e => e.Name));
                    }
                }

                holiday[order.StartDate?.ToString("dd/MM/yyyy")] += "<a class='d-block' title='" + subTitle + "' href='AdminOrderEdit.aspx?id=" + order.Id + "'>" + order.Host.Name + " " + order.Id + "<br/>(End:" + order.EndDate?.ToString("dd/MM/yyyy") + ")</a>";
            }

            return holiday;
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //TxtDateFrom.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            UpdateCalendar();
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (HolidayList[e.Day.Date.ToString("dd/MM/yyyy")] != null)
            {
                Literal literal1 = new Literal();
                literal1.Text = "<br/>";
                e.Cell.Controls.Add(literal1);
                Label label1 = new Label();
                label1.Text = (string)HolidayList[e.Day.Date.ToString("dd/MM/yyyy")];
                label1.Font.Size = new FontUnit(FontSize.Small);
                e.Cell.Controls.Add(label1);
            }
        }

        protected void ComboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCalendar();
        }

        protected void TxtDateFrom_TextChanged(object sender, EventArgs e)
        {
            UpdateCalendar();
        }

        protected void TxtDateTo_TextChanged(object sender, EventArgs e)
        {
            UpdateCalendar();
        }

        protected void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateCalendar();
        }

        private void UpdateCalendar()
        {
            int status = ControlUtil.GetSelectedValue(ComboStatus) ?? 0;
            DateTime? from = null;
            DateTime? to = null;

            if (!string.IsNullOrEmpty(TxtDateFrom.Text))
                from = DateTime.ParseExact(TxtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(TxtDateTo.Text))
                to = DateTime.ParseExact(TxtDateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string searchKey = TxtSearch.Text;

            HolidayList = Getholiday(from, to, searchKey, status);
        }

        protected void BtnDownloadPDF_Click(object sender, EventArgs e)
        {
            string search = TxtSearch.Text;
            string dateFrom = TxtDateFrom.Text;
            string dateTo = TxtDateTo.Text;
            int status = ControlUtil.GetSelectedValue(ComboStatus) ?? 0;

            DateTime? from = null;
            DateTime? to = null;
            if (!string.IsNullOrEmpty(dateFrom))
                from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(dateTo))
                to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            List<Order> list = new OrderDAO().SearchBy(from, to, search, status).OrderBy(l => l.Id).ToList();
            List<object> checks = new List<object>();
            foreach (Order fb in list)
            {
                OrderCheck check = new OrderCheck(fb);
                if (fb.EmploymentId != null)
                {
                    List<Employment> employees = new EmployeeController().FindByIDS(fb.EmploymentId);
                    check.EmployeeName = string.Join(",", employees.Select(ee => ee.Name));
                }
                checks.Add(check);
            }

            Document document = new Document();
            document.SetMargins(0, 0, 80, 60);
            // Save the PDF document to a memory stream
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();
            PdfPTable table = new PdfPTable(new float[] { 40f, 40f, 60f, 60f, 30f, 50f, 60f, 50f });
            table.TotalWidth = 370f;
            table.HorizontalAlignment = Element.ALIGN_CENTER;

            table.AddCell("Num.Ordine");
            table.AddCell("Cliente");
            table.AddCell("Data Inizio");
            table.AddCell("Data Fine");
            table.AddCell("Num. Ospiti");
            table.AddCell("Totale");
            table.AddCell("Stato");
            table.AddCell("Assegnato a");

            foreach (OrderCheck check in checks)
            {
                table.AddCell(check.Id.ToString());
                table.AddCell(check.Owner);
                table.AddCell(check.StartDate);
                table.AddCell(check.EndDate);
                table.AddCell(check.NumberOfGuests.ToString());
                table.AddCell(check.TotalAmount.ToString() + " €");

                string status0 = "";
                switch (check.Status)
                {
                    case 0: break;
                    case 1: status0 = "Confermato"; break;
                    case 2: status0 = "Pagato"; break;
                    case 3: status0 = "Assignato"; break;
                    case 4: status0 = "Chiuso"; break;
                }
                table.AddCell(status0);
                table.AddCell(check.EmployeeName == null ? "" : check.EmployeeName);
            }
            document.Add(table);

            // Close the PDF document
            document.Close();
            writer.Close();

            // Set the file name and content type
            string fileName = "Orders.pdf";
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