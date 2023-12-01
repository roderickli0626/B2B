using B2B.Controller;
using B2B.DAO;
using B2B.Model;
using B2B.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class HostRoomEdit : System.Web.UI.Page
    {
        private int editItemId;
        private bool isView;
        private Room editRoom;
        private Host host = null;
        private List<AccessoryAllocCheck> accessories = new List<AccessoryAllocCheck>();
        RoomController roomController = new RoomController();

        private HostDAO hostDAO = new HostDAO();
        private AccessoryDAO accessoryDAO = new AccessoryDAO();
        private AccommodationDAO accommodationDao = new AccommodationDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginSystem = new LoginController();
            host = loginSystem.GetCurrentUserAccount();
            if (host == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            editItemId = ParseUtil.TryParseInt(Request.Params["id"]) ?? 0;
            isView = ParseUtil.TryParseBool(Request.Params["view"]) ?? false;

            if (editItemId != 0)
            {
                editRoom = roomController.FindBy(editItemId);
            }

            if (!IsPostBack)
            {
                if (editItemId != 0)
                {
                    accessories = roomController.FindAccessoriesByRoom(editItemId);
                    string jsonString = JsonConvert.SerializeObject(accessories);
                    HfAccessoryAlloc.Value = jsonString;
                }
                LoadType();
                LoadLift();
                LoadAccessory();
                LoadAccessoryTable();
                LoadInfo();
            }
        }

        private void LoadAccessory()
        {
            List<Accessory> accs = new List<Accessory>();
            accs = accessoryDAO.FindAll();
            ComboAccessory.Items.Clear();
            ControlUtil.DataBind(ComboAccessory, accs, "Id", "Description");
        }

        private void LoadInfo()
        {
            if (editRoom == null)
            {
                pageTitle.InnerText = "ROOM (nuova)";
                ControlUtil.SelectValue(ComboType, ComboType.Items[0].Value);
                ComboType_SelectedIndexChanged(ComboType, EventArgs.Empty);
                return;
            }
            ControlUtil.SelectValue(ComboType, editRoom.TypeId.ToString());
            ComboType_SelectedIndexChanged(ComboType, EventArgs.Empty);
            ControlUtil.SelectValue(ComboLift, editRoom.Lift == true ? "1" : "2");

            switch (editRoom.Status)
            {
                case 1:
                    {
                        pageTitle.InnerText = "ROOM (inserita)";
                    }
                    break;
                case 2:
                    {
                        pageTitle.InnerText = "ROOM (verificata)";
                    }
                    break;
                case 3:
                    {
                        pageTitle.InnerText = "ROOM (rifiutata)";
                    }
                    break;
            }
            TxtAddress.Text = editRoom.Address;
            TxtStairCases.Text = editRoom.StairCases;
            TxtFloor.Text = editRoom.Floor;
            TxtNote.Text = editRoom.Note;

            if (isView)
            {
                accessoryDiv.Visible = false;
                BtnSave.Visible = false;
                BtnCancel.CssClass += " ms-auto btn-dark";
                TxtAddress.Enabled = false;
                TxtStairCases.Enabled = false;
                TxtNote.Enabled = false;
                TxtFloor.Enabled = false;
                ComboType.Enabled = false;
                ComboLift.Enabled = false;
            }
        }

        private void LoadAccessoryTable()
        {
            AccessoryRepeater.DataSource = accessories;
            AccessoryRepeater.DataBind();
        }

        private void LoadType()
        {
            List<Accomodation> typeList = new List<Accomodation>();
            typeList = accommodationDao.FindAll();
            ComboType.Items.Clear();
            ControlUtil.DataBind(ComboType, typeList, "Id", "Description");
        }
        private void LoadLift()
        {
            ComboLift.Items.Clear();
            ComboLift.Items.Add(new ListItem("Yes", "1"));
            ComboLift.Items.Add(new ListItem("No", "2"));
        }

        protected void BtnAddAccessory_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Key", "MyFun()", true);

            int? addAccessoryId = ControlUtil.GetSelectedValue(ComboAccessory);
            int? quantity = ParseUtil.TryParseInt(TxtQuantity.Text.Trim());
            if (addAccessoryId == null || quantity == null || quantity == 0)
            {
                ServerValidator3.IsValid = false;
                return;
            }

            Accessory accessory = accessoryDAO.FindById(addAccessoryId.Value);

            AccessoryAllocCheck addAlloc = new AccessoryAllocCheck();
            addAlloc.AccessoryId = addAccessoryId ?? 0;
            addAlloc.Description = accessory.Description;
            addAlloc.Image = accessory.Image;
            addAlloc.Quantity = quantity ?? 0;

            accessories = JsonConvert.DeserializeObject<List<AccessoryAllocCheck>>(HfAccessoryAlloc.Value) ?? new List<AccessoryAllocCheck>();
            accessories.Remove(accessories.Find(s => s.AccessoryId == addAccessoryId));
            accessories.Add(addAlloc);
            string jsonString = JsonConvert.SerializeObject(accessories);
            HfAccessoryAlloc.Value = jsonString;
            LoadAccessoryTable();
        }

        protected void AccessoryRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                if (isView) return;

                int accessoryId = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out accessoryId))
                {
                    accessories = JsonConvert.DeserializeObject<List<AccessoryAllocCheck>>(HfAccessoryAlloc.Value);
                    accessories.Remove(accessories.Find(s => s.AccessoryId == accessoryId));
                    string jsonString = JsonConvert.SerializeObject(accessories);
                    HfAccessoryAlloc.Value = jsonString;
                    LoadAccessoryTable();
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid) { return; }
            bool success1 = true;
            bool success2 = true;

            int? typeId = ControlUtil.GetSelectedValue(ComboType);
            int? Lift = ControlUtil.GetSelectedValue(ComboLift);
            string address = TxtAddress.Text;
            string stairCases = TxtStairCases.Text;
            string floor = TxtFloor.Text;
            string note = TxtNote.Text;
            int status = 1;

            accessories = JsonConvert.DeserializeObject<List<AccessoryAllocCheck>>(HfAccessoryAlloc.Value) ?? new List<AccessoryAllocCheck>();
            if (accessories.Count() == 0)
            {
                ServerValidator2.IsValid = false;
                return;
            }

            if (editRoom == null)
            {
                int? hostId = host.Id;

                int? id = roomController.AddRoom(status, typeId, Lift, address, stairCases, floor, hostId, note, null);
                if (id != null) success2 = roomController.AddAccessoryAlloc(id, accessories);
                else success1 = false;
            }
            else
            {
                success1 = roomController.UpdateRoom(editRoom.Id, status, typeId, Lift, address, stairCases, floor, note, null);
                success2 = roomController.AddAccessoryAlloc(editRoom.Id, accessories);
            }

            if (!success1 || !success2)
            {
                ServerValidator1.IsValid = false;
                return;
            }

            //Send Add Room Email to Admin
            if (editRoom == null)
            {
                SendEmail();
            }

            Response.Redirect(BtnCancel.PostBackUrl);
        }

        private void SendEmail()
        {
            //Send Email
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress("Krandall2005@gmail.com", "BnB Host");// Sender details here, replace with valid value
            Msg.Subject = "Nuova Room appena aggiunta!"; // subject of email
            Msg.To.Add("Digraziag286@gmail.com"); //Add Email id, to which we will send email
            Msg.Body = host.Name + " ha appena aggiunto una nuova Room, si prega di consultare il gestionale per i dettagli.";
            Msg.IsBodyHtml = true;
            Msg.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false; // to get rid of error "SMTP server requires a secure connection"
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new System.Net.NetworkCredential("krandall2005@gmail.com", "fyjlmiowttdaovfi");// replace with valid value
            smtp.EnableSsl = true;

            smtp.Send(Msg);
        }

        protected void ComboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int typeId = ControlUtil.GetSelectedValue(ComboType) ?? 0;
            Accomodation  accommodation = accommodationDao.FindById(typeId);
            if (accommodation == null) { return; }
            string imgUrl = string.IsNullOrEmpty(accommodation.Image) ? "Content/Images/accommodation_default.jpg" : "Upload/Accommodation/" + accommodation.Image;
            AccommodationImage.Src = imgUrl;
        }
    }
}