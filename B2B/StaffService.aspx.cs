﻿using B2B.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class StaffService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            if (!loginSystem.IsStaffLoggedIn())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StaffServiceEdit.aspx");
        }
    }
}