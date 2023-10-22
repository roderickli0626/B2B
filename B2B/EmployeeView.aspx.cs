using B2B.Controller;
using B2B.DAO;
using B2B.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class EmployeeView : System.Web.UI.Page
    {
        private int employeeID;
        private Employment employment;
        Hashtable HolidayList;

        private EmployeeController employeeController = new EmployeeController();
        private OrderDAO orderDAO = new OrderDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            employeeID = ParseUtil.TryParseInt(Request.Params["id"]) ?? 0;
            employment = employeeController.FindBy(employeeID);
            if (employment == null)
            {
                Response.Redirect("~/EmployeeLogin.aspx");
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
                username.InnerText = employment.Name;
                HfEmployeeID.Value = employment.Id.ToString();
            }
        }

        public void LoadStatus()
        {
            ComboStatus.Items.Clear();
            ComboStatus.Items.Add(new ListItem("Tutti", "0"));
            ComboStatus.Items.Add(new ListItem("Assegnati", "3"));
            ComboStatus.Items.Add(new ListItem("Chiusi", "4"));
        }


        private Hashtable Getholiday(DateTime? startDate, DateTime? endDate, string searchKey, int status)
        {
            Hashtable holiday = new Hashtable();

            List<Order> orders = orderDAO.SearchByEmployee(employment.Id, startDate, endDate, searchKey, status).ToList();

            foreach (Order order in orders)
            {
                holiday[order.StartDate?.ToString("dd/MM/yyyy")] += "<a class='d-block' " + " href='EmployeeViewEdit.aspx?id=" + order.Id + "&employeeID=" + employeeID + "'>" + "Ordine " + order.Id + "<br/>(End:" + order.EndDate?.ToString("dd/MM/yyyy") + ")</a>";
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
    }
}