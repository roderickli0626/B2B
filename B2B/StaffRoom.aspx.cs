using B2B.Controller;
using B2B.DAO;
using B2B.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class StaffRoom : System.Web.UI.Page
    {
        private AccommodationDAO accommodationDAO = new AccommodationDAO();
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
                LoadStatus();
                //LoadLift();
                LoadType();
            }
        }
        public void LoadStatus()
        {
            ComboStatus.Items.Clear();
            ComboStatus.Items.Add(new ListItem("Tutti", "0"));
            ComboStatus.Items.Add(new ListItem("Inserito", "1"));
            ComboStatus.Items.Add(new ListItem("Verificato", "2"));
            ComboStatus.Items.Add(new ListItem("Rifiutato", "3"));
        }
        //private void LoadLift()
        //{
        //    ComboLift.Items.Clear();
        //    ComboLift.Items.Add(new ListItem("Yes & No", "0"));
        //    ComboLift.Items.Add(new ListItem("Yes", "1"));
        //    ComboLift.Items.Add(new ListItem("No", "2"));
        //}
        private void LoadType()
        {
            List<Accomodation> typeList = new List<Accomodation>();
            typeList = accommodationDAO.FindAll();
            typeList.Add(new Accomodation() { Id = 0, Description = "Tutti" });
            typeList = typeList.OrderBy(x => x.Id).ToList();
            ComboType.Items.Clear();
            ControlUtil.DataBind(ComboType, typeList, "Id", "Description");
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StaffRoomEdit.aspx");
        }
    }
}