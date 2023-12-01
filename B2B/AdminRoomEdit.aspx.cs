using Antlr.Runtime.Tree;
using B2B.Controller;
using B2B.DAO;
using B2B.Model;
using B2B.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class AdminRoomEdit : System.Web.UI.Page
    {
        private int editItemId;
        private Room editRoom;
        private List<AccessoryAllocCheck> accessories = new List<AccessoryAllocCheck>();
        RoomController roomController = new RoomController();
        
        private HostDAO hostDAO = new HostDAO();
        private AccessoryDAO accessoryDAO = new AccessoryDAO();
        private AccommodationDAO accommodationDao = new AccommodationDAO();

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
                LoadOwner();
                LoadType();
                LoadLift();
                LoadAccessory();
                LoadPriceGroup();
                LoadAccessoryTable();
                LoadInfo();
            }
        }
        private void LoadPriceGroup()
        {
            List<PriceListGroup> priceListGroups = new List<PriceListGroup>();
            priceListGroups = new PriceListGroupDAO().FindAll();
            ComboPriceGroup.Items.Clear();
            ControlUtil.DataBind(ComboPriceGroup, priceListGroups, "Id", "DescriptionShort");
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
                pageTitle.InnerText = "ROOM (New) ";
                pageTitle.Attributes.Add("class", "text-dark");
                return;
            }
            ControlUtil.SelectValue(ComboOwner, editRoom.HostId.ToString());
            ControlUtil.SelectValue(ComboType, editRoom.TypeId.ToString());
            ControlUtil.SelectValue(ComboLift, editRoom.Lift == true ? "1" : "2" );
            ControlUtil.SelectValue(ComboPriceGroup, editRoom.PriceListGroupId.ToString());

            switch (editRoom.Status)
            {
                case 1: 
                    { 
                        RadioButton1.Checked = true;
                        pageTitle.InnerText = "ROOM " + editRoom.Id + " (inserita)";
                        pageTitle.Attributes.Add("class", "text-primary");
                    } break;
                case 2:
                    { 
                        RadioButton2.Checked = true;
                        pageTitle.InnerText = "ROOM " + editRoom.Id + " (verificata) ";
                        pageTitle.Attributes.Add("class", "text-success");
                    } break;
                case 3:
                    { 
                        RadioButton3.Checked = true;
                        pageTitle.InnerText = "ROOM " + editRoom.Id + " (rifiutata) ";
                        pageTitle.Attributes.Add("class", "text-danger");
                    } break;
            }
            ComboOwner.Enabled = false;
            TxtAddress.Text = editRoom.Address;
            TxtStairCases.Text = editRoom.StairCases;
            TxtFloor.Text = editRoom.Floor;
            TxtNote.Text = editRoom.Note;
        }

        private void LoadAccessoryTable()
        {
            AccessoryRepeater.DataSource = accessories;
            AccessoryRepeater.DataBind();
        }

        private void LoadOwner()
        {
            List<Host> hosts = new List<Host>();
            hosts = hostDAO.FindAll().Where(h => h.IsB2BStaff == false).ToList();
            ComboOwner.Items.Clear();
            ControlUtil.DataBind(ComboOwner, hosts, "Id", "Name");
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
            ComboLift.Items.Add(new ListItem("Si", "1"));
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
            int? PriceGroupId = ControlUtil.GetSelectedValue(ComboPriceGroup);
            string address = TxtAddress.Text;
            string stairCases = TxtStairCases.Text;
            string floor = TxtFloor.Text;
            string note = TxtNote.Text;
            int status = 1;

            if (RadioButton1.Checked) status = 1;
            else if(RadioButton2.Checked) status = 2;
            else if(RadioButton3.Checked) status = 3;

            accessories = JsonConvert.DeserializeObject<List<AccessoryAllocCheck>>(HfAccessoryAlloc.Value) ?? new List<AccessoryAllocCheck>();
            if (accessories.Count() == 0)
            {
                ServerValidator2.IsValid = false;
                return;
            }

            if (editRoom == null)
            {
                int? hostId = ControlUtil.GetSelectedValue(ComboOwner);

                int? id = roomController.AddRoom(status, typeId, Lift, address, stairCases, floor, hostId, note, PriceGroupId);
                if (id != null) success2 = roomController.AddAccessoryAlloc(id, accessories);
                else success1 = false;
            }
            else
            {
                success1 = roomController.UpdateRoom(editRoom.Id, status, typeId, Lift, address, stairCases, floor,note, PriceGroupId);
                success2 = roomController.AddAccessoryAlloc(editRoom.Id, accessories);
            }

            if (!success1 || !success2)
            {
                ServerValidator1.IsValid = false;
                return;
            }
            Response.Redirect(BtnCancel.PostBackUrl);
        }
    }
}