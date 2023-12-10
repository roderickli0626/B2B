using B2B.Controller;
using B2B.DAO;
using B2B.Model;
using B2B.PayPalPayment;
using B2B.PayPalPayment;
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
    public partial class HostOrderEdit : System.Web.UI.Page
    {
        private int editItemId;
        private bool isView;
        private Order editOrder;
        private Host host = null;

        private List<ServiceAllocCheck> serviceAllocList = new List<ServiceAllocCheck>();
        private OrderController orderController = new OrderController();

        private ServiceDAO serviceDAO = new ServiceDAO();
        private VoucherDAO voucherDAO = new VoucherDAO();
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
                editOrder = orderController.FindBy(editItemId);
            }

            if (!IsPostBack)
            {
                if (editItemId != 0)
                {
                    serviceAllocList = orderController.FindServiceByOrder(editItemId);
                    string jsonString = JsonConvert.SerializeObject(serviceAllocList);
                    HfServiceAlloc.Value = jsonString;
                    TxtTotalAmount.Text = editOrder.TotalAmount.ToString();
                }
                LoadGrandService();
                LoadService();
                LoadServiceTable();
                LoadEmployee();
                LoadRoom();
                LoadInfo();
                SetVisible();
            }
        }
        private void LoadGrandService()
        {
            List<GrandService> grandList = new GrandServiceDAO().FindAll();
            ControlUtil.DataBind(ComboGrandService, grandList, "Id", "Title", 0, "");
        }
        private void SetVisible()
        {
            if (editOrder == null)
            {
                pageTitle.InnerText = "Ordine (nuovo)";
                paymentDiv.Visible = false;
                assignDiv.Visible = false;
                RecoverDiv.Visible = true;
            }
            else if (editOrder.Status == 1)
            {
                pageTitle.InnerText = "Ordine (confermato)";
                paymentDiv.Visible = false;
                assignDiv.Visible = false;
                ComboRoom.Enabled = false;
            }
            else if (editOrder.Status == 2)
            {
                pageTitle.InnerText = "Ordine (pagato)";
                serviceDiv.Visible = false;
                ComboRoom.Enabled = false;
            }
            else if (editOrder.Status == 3)
            {
                pageTitle.InnerText = "Ordine (assegnato)";
                serviceDiv.Visible = false;
                ComboRoom.Enabled = false;
            }
            else
            {
                pageTitle.InnerText = "Ordine (chiuso)";
                serviceDiv.Visible = false;
                ComboRoom.Enabled = false;
                ComboAssignedTo.Enabled = false;
            }
        }

        private void LoadEmployee()
        {
            ComboAssignedTo.Items.Clear();
            ComboAssignedTo.Items.Add(new ListItem("Yes", "1"));
            ComboAssignedTo.Items.Add(new ListItem("No", "0"));
        }

        private void LoadInfo()
        {
            TxtVoucherID.Text = GetVoucherID();

            if (editOrder == null)
            {
                return;
            }
            TxtDateFrom.Text = editOrder.StartDate?.ToString("dd/MM/yyyy");
            TxtDateTo.Text = editOrder.EndDate?.ToString("dd/MM/yyyy");
            TxtNote.Text = editOrder.note;
            if (editOrder.Payment != null)
            {
                string payDetails = editOrder.Payment.Note == null ? "(Paypal)" : "(" + editOrder.Payment.Note.ToString() + ")";
                TxtPaymentResult.Text = editOrder.Payment.Amount.ToString() + " €" + payDetails + ", " + editOrder.Payment?.DateOfPay.ToString() ?? "";
            }
            if (editOrder.Voucher != null) TxtVoucher.Text = editOrder.Voucher?.SerialNumberGenerator.ToString() ?? "";
            TxtNumberOfGuests.Text = editOrder.NumberOfGuests.ToString();
            ControlUtil.SelectValue(ComboAssignedTo, editOrder.EmploymentId == null ? "0" : "1");
            HfPaymentID.Value = "";
            HfVoucherID.Value = "";

            if (isView)
            {
                TxtDateFrom.Enabled = false;
                TxtDateTo.Enabled = false;
                TxtPaymentResult.Enabled = false;
                TxtNote.Enabled = false;
                ComboAssignedTo.Enabled = false;
                BtnSave.Visible = false;
                BtnCancel.CssClass += " ms-auto btn-dark";
                serviceDiv.Visible = false;
                payDiv.Visible = false;
            }
        }

        private string GetVoucherID()
        {
            LoginController loginSystem = new LoginController();
            Host host = loginSystem.GetCurrentUserAccount();
            if (host == null) return "";
            OrderDAO orderDAO = new OrderDAO();
            List<Order> orderList = orderDAO.FindByHost(host.Id);
            if (orderList.Count == 0) return "";

            string voucherID = "";
            double? voucherAmount = 0;
            foreach (Order order in orderList)
            {
                if (order.Voucher != null)
                {
                    if (voucherAmount < order.Voucher.Amount)
                    {
                        voucherAmount = order.Voucher.Amount;
                        voucherID = order.Voucher.SerialNumberGenerator;
                    }
                        
                }
            }
            return voucherID;
        }

        private void LoadRoom()
        {
            List<Room> rooms = new List<Room>();
            RoomDAO roomDao = new RoomDAO();
            rooms = roomDao.FindByOwner(host.Id);
            ControlUtil.DataBind(ComboRoom, rooms, "Id", "Address", 0, "");
            if (editOrder != null) ControlUtil.SelectValue(ComboRoom, editOrder.RoomId.ToString());
        }

        private void LoadService()
        {
            int grandServiceID = ControlUtil.GetSelectedValue(ComboGrandService) ?? 0;
            List<Service> services = new List<Service>();
            services = serviceDAO.FindByGrandService(grandServiceID);

            ControlUtil.DataBind(ComboService, services, "Id", "DescriptionShort");
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Key", "MyFun()", true);
        }
        private void LoadServiceTable()
        {
            ServiceRepeater.DataSource = serviceAllocList;
            ServiceRepeater.DataBind();
        }

        protected void BtnAddService_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Key", "MyFun()", true);

            int? addServiceId = ControlUtil.GetSelectedValue(ComboService);
            int? quantity = ParseUtil.TryParseInt(TxtQuantity.Text.Trim());
            int? roomId = ControlUtil.GetSelectedValue(ComboRoom);
            if (addServiceId == null || addServiceId == 0 || quantity == null)
            {
                ServerValidator3.IsValid = false;
                return;
            }
            if (roomId == null)
            {
                ServerValidator5.IsValid = false;
                return;
            }
            Room room = new RoomDAO().FindById(roomId ?? 0);
            Service service = serviceDAO.FindById(addServiceId.Value);

            ServiceAllocCheck addService = new ServiceAllocCheck();
            addService.serviceId = addServiceId ?? 0;

            addService.GrandService = service.GrandService.Title;
            addService.Service = service.DescriptionShort;
            addService.Description = service.DescriptionLong;
            addService.Image = service.Image;
            addService.Quantity = quantity ?? 0;
            if (service.HavePriceGroup ?? false)
            {
                double percent = room.PriceListGroup?.Percentuale ?? 0;
                addService.Price = (service.Price ?? 0) * (1 + percent / 100);
                addService.Amount = (quantity ?? 0) * (service.Price ?? 0) * (1 + percent / 100);
            }
            else
            {
                addService.Price = service.Price ?? 0;
                addService.Amount = (quantity ?? 0) * (service.Price ?? 0);
            }
            serviceAllocList = JsonConvert.DeserializeObject<List<ServiceAllocCheck>>(HfServiceAlloc.Value) ?? new List<ServiceAllocCheck>();
            serviceAllocList.Remove(serviceAllocList.Find(s => s.serviceId == addServiceId));
            serviceAllocList.Add(addService);
            string jsonString = JsonConvert.SerializeObject(serviceAllocList);
            HfServiceAlloc.Value = jsonString;
            LoadServiceTable();
            TxtTotalAmount.Text = serviceAllocList.Sum(s => s.Amount).ToString();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            bool isPaid = false;
            if (Session["IsPaid"] != null)
            {
                isPaid = (bool)Session["IsPaid"];
            }

            if (!IsValid) return;
            bool success1 = true;
            bool success2 = true;

            DateTime startDate = ParseUtil.TryParseDate(TxtDateFrom.Text) ?? DateTime.Today;
            DateTime endDate = ParseUtil.TryParseDate(TxtDateTo.Text) ?? DateTime.Today.AddDays(30);
            int numberOfGuests = ParseUtil.TryParseInt(TxtNumberOfGuests.Text) ?? 1;
            string note = TxtNote.Text;

            if (editOrder == null)
            {
                int? hostId = host.Id;
                int? roomId = ControlUtil.GetSelectedValue(ComboRoom);
                double totalAmount = ParseUtil.TryParseDouble(TxtTotalAmount.Text) ?? 0;
                DateTime dateCreated = DateTime.Today;
                int status = 1;
                serviceAllocList = JsonConvert.DeserializeObject<List<ServiceAllocCheck>>(HfServiceAlloc.Value) ?? new List<ServiceAllocCheck>();
                if (serviceAllocList.Count() == 0)
                {
                    ServerValidator2.IsValid = false;
                    return;
                }
                if (hostId == null)
                {
                    ServerValidator0.IsValid = false;
                    return;
                }
                if (roomId == null || roomId == 0)
                {
                    ServerValidator4.IsValid = false;
                    return;
                }
                //Payment
                int? voucherID = null;
                int? paymentID = null;
                if (isPaid)
                {
                    if (HfPaymentType.Value == "3" || HfPaymentType.Value == "")
                    {
                        status = 2;
                        double voucherAmount = 0;
                        if (HfVoucherID.Value != "")
                        {
                            voucherID = ParseUtil.TryParseInt(HfVoucherID.Value) ?? 0;
                            if (voucherID != 0)
                            {
                                Voucher voucher = voucherDAO.FindById(voucherID ?? 0);
                                voucherAmount = voucher.Amount ?? 0;
                                if (voucherAmount >= ParseUtil.TryParseDouble(TxtTotalAmount.Text))
                                {
                                    voucher.Amount = voucherAmount - ParseUtil.TryParseDouble(TxtTotalAmount.Text) ?? 0;
                                    voucherAmount = ParseUtil.TryParseDouble(TxtTotalAmount.Text) ?? 0;
                                }
                                else
                                {
                                    voucher.Amount = 0;
                                }
                                voucherDAO.Update(voucher);
                            }
                        }
                        Payment payment = new Payment();
                        payment.Amount = totalAmount;//TODO Voucher
                        payment.PaypalTransitionID = "Paypal: " + HfPaymentID.Value;
                        payment.DateOfPay = DateTime.Now;
                        payment.Method = 3;
                        payment.Note = "Dal Voucher nr. " + voucherID + ": " + voucherAmount + " €" + ", Paypal: " + (totalAmount - voucherAmount) + " € ";
                        paymentID = new PaymentDAO().Insert2(payment);
                    }
                    else if (HfPaymentType.Value == "2")
                    {
                        status = 1;
                        Payment payment = new Payment();
                        payment.Amount = totalAmount;//TODO Voucher
                        payment.PaypalTransitionID = "Bonifico";
                        payment.DateOfPay = DateTime.Now;
                        payment.Note = "Paied with Bonifico.";
                        payment.Method = 2;
                        paymentID = new PaymentDAO().Insert2(payment);
                    }
                    else
                    {
                        status = 1;
                        Payment payment = new Payment();
                        payment.Amount = totalAmount;//TODO Voucher
                        payment.PaypalTransitionID = "Contanti";
                        payment.DateOfPay = DateTime.Now;
                        payment.Note = "Paied with Contanti.";
                        payment.Method = 1;
                        paymentID = new PaymentDAO().Insert2(payment);
                    }
                }

                int? id = orderController.AddOrder(status, startDate, endDate, numberOfGuests, totalAmount, hostId, roomId, dateCreated, note, paymentID, voucherID);
                if (id != null) success2 = orderController.AddServiceAlloc(id, serviceAllocList);
                else success1 = false;
            }
            else if (editOrder.Status == 1)
            {
                double totalAmount = ParseUtil.TryParseDouble(TxtTotalAmount.Text) ?? 0;
                serviceAllocList = JsonConvert.DeserializeObject<List<ServiceAllocCheck>>(HfServiceAlloc.Value) ?? new List<ServiceAllocCheck>();
                if (serviceAllocList.Count() == 0)
                {
                    ServerValidator2.IsValid = false;
                    return;
                }

                int status = 1;
                //Payment
                int? voucherID = null;
                int? paymentID = null;
                if (isPaid)
                {
                    if (HfPaymentType.Value == "3" || HfPaymentType.Value == "")
                    {
                        status = 2;
                        double voucherAmount = 0;
                        if (HfVoucherID.Value != "")
                        {
                            voucherID = ParseUtil.TryParseInt(HfVoucherID.Value) ?? 0;
                            if (voucherID != 0)
                            {
                                Voucher voucher = voucherDAO.FindById(voucherID ?? 0);
                                voucherAmount = voucher.Amount ?? 0;
                                if (voucherAmount >= ParseUtil.TryParseDouble(TxtTotalAmount.Text))
                                {
                                    voucher.Amount = voucherAmount - ParseUtil.TryParseDouble(TxtTotalAmount.Text) ?? 0;
                                    voucherAmount = ParseUtil.TryParseDouble(TxtTotalAmount.Text) ?? 0;
                                }
                                else
                                {
                                    voucher.Amount = 0;
                                }
                                voucherDAO.Update(voucher);
                            }
                        }
                        Payment payment = new Payment();
                        payment.Amount = totalAmount;//TODO Voucher
                        payment.PaypalTransitionID = "Paypal: " + HfPaymentID.Value;
                        payment.DateOfPay = DateTime.Now;
                        payment.Note = "<B>Dal Voucher nr.<B>: " + voucherID + ": " + voucherAmount + " €" + ", Paypal: " + (totalAmount - voucherAmount) + " € ";
                        payment.OrderId = editOrder.Id;
                        payment.Method = 3;
                        paymentID = new PaymentDAO().Insert2(payment);
                    }
                    else if (HfPaymentType.Value == "2")
                    {
                        status = 1;
                        Payment payment = new Payment();
                        payment.Amount = totalAmount;//TODO Voucher
                        payment.PaypalTransitionID = "Bonifico";
                        payment.DateOfPay = DateTime.Now;
                        payment.Note = "Paied with Bonifico.";
                        payment.OrderId = editOrder.Id;
                        payment.Method = 2;
                        paymentID = new PaymentDAO().Insert2(payment);
                    }
                    else
                    {
                        status = 1;
                        Payment payment = new Payment();
                        payment.Amount = totalAmount;//TODO Voucher
                        payment.PaypalTransitionID = "Contanti";
                        payment.DateOfPay = DateTime.Now;
                        payment.Note = "Paied with Contanti.";
                        payment.OrderId = editOrder.Id;
                        payment.Method = 1;
                        paymentID = new PaymentDAO().Insert2(payment);
                    }
                }

                success1 = orderController.UpdateOrder(editOrder.Id, status, startDate, endDate, numberOfGuests, totalAmount, null, note, paymentID, voucherID);
                success2 = orderController.AddServiceAlloc(editOrder.Id, serviceAllocList);
            }
            else if (editOrder.Status == 2 || editOrder.Status == 3)
            {
                int employeeId = ControlUtil.GetSelectedValue(ComboAssignedTo) ?? 0;
                int status = 2;
                if (employeeId != 0) status = 3;

                //success1 = orderController.UpdateOrder(editOrder.Id, status, startDate, endDate, numberOfGuests, editOrder.TotalAmount ?? 0, employeeId, note, null, null);
                success1 = orderController.UpdateOrder(editOrder.Id, status, startDate, endDate, numberOfGuests, editOrder.TotalAmount ?? 0, null, note, null, null);
            }

            if (!success1 || !success2)
            {
                ServerValidator1.IsValid = false;
                return;
            }

            //Send New Order Add Email to Admin
            if (editOrder == null)
            {
                SendEmail();
            }

            if (isPaid) Session.Remove("IsPaid");
            Response.Redirect(BtnCancel.PostBackUrl);
        }

        private void SendEmail()
        {
            //Send Email
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress("krandall2005@gmail.com", "BnB Host");// Sender details here, replace with valid value
            Msg.Subject = "Aggiunto un nuovo ORDINE"; // subject of email
            Msg.To.Add("Digraziag286@gmail.com"); //Add Email id, to which we will send email
            Msg.Body = host.Name + " ha appena aggiunto un nuovo ORDINE. Consultare il gestionale per i dettagli.";
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

        protected void ServiceRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (editOrder != null && (editOrder.Status == 2 || editOrder.Status == 3 || editOrder.Status == 4)) return;
            if (e.CommandName == "Delete")
            {
                if (isView) return;

                int serviceId = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out serviceId))
                {
                    serviceAllocList = JsonConvert.DeserializeObject<List<ServiceAllocCheck>>(HfServiceAlloc.Value);
                    serviceAllocList.Remove(serviceAllocList.Find(s => s.serviceId == serviceId));
                    string jsonString = JsonConvert.SerializeObject(serviceAllocList);
                    HfServiceAlloc.Value = jsonString;
                    LoadServiceTable();
                    TxtTotalAmount.Text = serviceAllocList.Sum(s => s.Amount).ToString();
                }
            }
        }

        protected void BtnCheckVoucher_Click(object sender, EventArgs e)
        {
            string voucherID = TxtVoucherID.Text;
            if (string.IsNullOrEmpty(voucherID)) { return; }
            Voucher voucher = voucherDAO.FindBySerialNumber(voucherID);
            if (voucher == null)
            {
                voucherNo.Visible = true;
                voucherOK.Visible = false;
                voucherNo.InnerHtml = "  -  Voucher Id NON VALIDO  " + "<i class=\"fa fa-close\"></i>";
            }
            else
            {
                voucherOK.Visible = true;
                voucherNo.Visible = false;
                voucherOK.InnerHtml = "  -  Crediti " + voucher.Amount + "  " + "<i class=\"fa fa-check\"></i>";
                HfVoucherID.Value = voucher.Id.ToString();
                HfVoucherAmount.Value = voucher.Amount.ToString();
                //Voucher
                if (voucher.Amount >= ParseUtil.TryParseDouble(TxtTotalAmount.Text))
                {
                    HfVoucherAmount.Value = TxtTotalAmount.Text.ToString();
                    Session["IsPaid"] = true;
                }
            }
        }

        protected void Paypal_ServerClick(object sender, EventArgs e)
        {
            if (!IsValid) return;

            double voucherAmount = 0;
            if (HfVoucherID.Value != "")
            {
                int voucherID = ParseUtil.TryParseInt(HfVoucherID.Value) ?? 0;
                if (voucherID != 0)
                {
                    Voucher voucher = voucherDAO.FindById(voucherID);
                    voucherAmount = voucher.Amount ?? 0;
                }

                if (voucherAmount >= ParseUtil.TryParseDouble(TxtTotalAmount.Text))
                {
                    Button button = (Button)sender;
                    //button.Text = "Voucher is enough";
                    return;
                }
            }


            PaymentDetails pd = new PaymentDetails();
            pd.Set("ArticleNumber", "Article Number");

            Random zufall = new Random();
            DateTime dt = DateTime.Now;
            string invoiceNumber = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + Convert.ToString(zufall.Next(-100, 100)).PadLeft(2, '0');
            pd.Set("InvoiceNumber", invoiceNumber);
            pd.Set("ItemDescription", "Payment Detail Description");
            pd.Set("ItemName", "Name of Item");
            pd.Set("Quantity", "1");
            //Handle Voucher
            pd.Set("Total", (ParseUtil.TryParseDouble(TxtTotalAmount.Text) - voucherAmount).ToString());
            pd.Set("Execute", "command to execute after payment");

            PaymentPrepare pp = new PaymentPrepare();
            pp.PaymentDetails = pd;
            pp.Description = "Payment description";
            string baseUrl = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf("/"));
            pp.UrlCancel = baseUrl + "/PaymentCancel.aspx";
            pp.UrlReturn = baseUrl + "/PaymentComplete.aspx";

            var payment = pp.CreatePayment();
            string paymentId = payment.id;
            HfPaymentID.Value = paymentId;
            pd.Set(pd.PaymentId, paymentId);
            Session[paymentId] = pd;
            Response.Write("<script type='text/javascript'>");
            Response.Write("window.open('" + payment.GetApprovalUrl() + "','_blank');");
            Response.Write("</script>");
            //Response.Redirect(payment.GetApprovalUrl());
        }

        protected void ComboGrandService_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadService();
        }

        protected void BtnRecoverService_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Key", "MyFun()", true);

            int? roomId = ControlUtil.GetSelectedValue(ComboRoom);
            if (roomId == null)
            {
                ServerValidator5.IsValid = false;
                return;
            }

            List<Order> previousOrders = new OrderDAO().FindByHost(host.Id).Where(o => o.RoomId == roomId).ToList();
            if (previousOrders.Count == 0) return;
            Order previousOrder = previousOrders.Last();

            serviceAllocList = orderController.FindServiceByOrder(previousOrder.Id);
            string jsonString = JsonConvert.SerializeObject(serviceAllocList);
            HfServiceAlloc.Value = jsonString;
            TxtTotalAmount.Text = previousOrder.TotalAmount.ToString();

            LoadServiceTable();
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            string paymentType = PaymentType.SelectedValue;
            HfPaymentType.Value = paymentType;

            if (paymentType == "3")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Payment with Paypal", "document.getElementById('" + Paypal.ClientID + "').click();", true);
            }
            else
            {
                Session["IsPaid"] = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "Hide Modal", "$('#myModal').modal('hide');", true);
            }
        }
    }
}