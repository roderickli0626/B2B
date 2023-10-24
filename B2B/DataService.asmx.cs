using B2B.Controller;
using B2B.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace B2B
{
    /// <summary>
    /// Summary description for DataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DataService : System.Web.Services.WebService
    {
        LoginController loginSystem = new LoginController();
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindOrders(int draw, int start, int length,
            string dateFrom, string dateTo, string searchVal, int status)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            DateTime? from = null;
            DateTime? to = null;

            if (!string.IsNullOrEmpty(dateFrom))
                from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(dateTo))
                to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            OrderController orderController = new OrderController();
            SearchResult searchResult = orderController.SearchOrders(from, to, start, length, searchVal, status);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindHostOrders(int draw, int start, int length,
            string dateFrom, string dateTo, string searchVal, int status)
        {
            Host host = loginSystem.GetCurrentUserAccount();
            if (host == null) return;

            DateTime? from = null;
            DateTime? to = null;

            if (!string.IsNullOrEmpty(dateFrom))
                from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(dateTo))
                to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            OrderController orderController = new OrderController();
            SearchResult searchResult = orderController.SearchHostOrders(host.Id, from, to, start, length, searchVal, status);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindEmployeeOrders(int draw, int start, int length,
            string dateFrom, string dateTo, string searchVal, int status, int employeeID)
        {
            Employment employment = new EmployeeController().FindBy(employeeID);
            if (employment == null) return;

            DateTime? from = null;
            DateTime? to = null;

            if (!string.IsNullOrEmpty(dateFrom))
                from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(dateTo))
                to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            OrderController orderController = new OrderController();
            SearchResult searchResult = orderController.SearchEmployeeOrders(employment.Id, from, to, start, length, searchVal, status);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeleteOrder(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            OrderController orderController = new OrderController();
            bool success = orderController.DeleteOrder(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindRooms(int draw, int start, int length,
            int lift, int type, string searchVal, int status)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            RoomController roomController = new RoomController();
            SearchResult searchResult = roomController.SearchRooms(lift, type, start, length, searchVal, status);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeleteRoom(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            RoomController roomController = new RoomController();
            bool success = roomController.DeleteRoom(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindHostRooms(int draw, int start, int length,
            int lift, int type, string searchVal, int status)
        {
            Host host = loginSystem.GetCurrentUserAccount();
            if (host == null) return;

            RoomController roomController = new RoomController();
            SearchResult searchResult = roomController.SearchHostRooms(host.Id, lift, type, start, length, searchVal, status);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindHosts(int draw, int start, int length, int searchKey, string searchVal)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            HostController hostController = new HostController();
            SearchResult searchResult = hostController.SearchHosts(start, length, searchVal, searchKey);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminFindStaffs(int draw, int start, int length, int searchKey, string searchVal)
        {
            if (!loginSystem.IsAdminLoggedIn()) return;

            HostController hostController = new HostController();
            SearchResult searchResult = hostController.SearchStaffs(start, length, searchVal, searchKey);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeleteHost(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            HostController hostController = new HostController();
            bool success = hostController.DeleteHost(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindEmployees(int draw, int start, int length, int searchKey, string searchVal)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            EmployeeController empController = new EmployeeController();
            SearchResult searchResult = empController.SearchBy(start, length, searchVal, searchKey);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeleteEmployee(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            EmployeeController employeeController = new EmployeeController();
            bool success = employeeController.DeleteEmployee(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindPayments(int draw, int start, int length,
            string dateFrom, string dateTo, int method, string searchVal)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            DateTime? from = null;
            DateTime? to = null;

            if (!string.IsNullOrEmpty(dateFrom))
                from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(dateTo))
                to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            PaymentController paymentController = new PaymentController();
            SearchResult searchResult = paymentController.SearchPayments(from, to, start, length, searchVal, method);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeletePayment(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            PaymentController paymentController = new PaymentController();
            bool success = paymentController.DeletePayment(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindVouchers(int draw, int start, int length, string searchVal)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            VoucherController voucherController = new VoucherController();
            SearchResult searchResult = voucherController.SearchBy(start, length, searchVal);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeleteVoucher(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            VoucherController voucherController = new VoucherController();
            bool success = voucherController.DeleteVoucher(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindServices(int draw, int start, int length, string searchVal, int grandServiceID)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            ServiceController serviceController = new ServiceController();
            SearchResult searchResult = serviceController.SearchBy(start, length, searchVal, grandServiceID);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeleteService(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            ServiceController serviceController = new ServiceController();
            bool success = serviceController.DeleteService(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindAccommodations(int draw, int start, int length, string searchVal)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            AccommodationController accommodationController = new AccommodationController();
            SearchResult searchResult = accommodationController.SearchBy(start, length, searchVal);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeleteAccommodation(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            AccommodationController accommodationController = new AccommodationController();
            bool success = accommodationController.DeleteAccommodation(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindAccessories(int draw, int start, int length, string searchVal)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            AccessoryController accessoryController = new AccessoryController();
            SearchResult searchResult = accessoryController.SearchBy(start, length, searchVal);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeleteAccessory(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            AccessoryController accessoryController = new AccessoryController();
            bool success = accessoryController.DeleteAccessory(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindPriceListGroups(int draw, int start, int length, string searchVal)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            PriceListGroupController priceListGroupController = new PriceListGroupController();
            SearchResult searchResult = priceListGroupController.SearchBy(start, length, searchVal);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeletePriceListGroup(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            PriceListGroupController priceListGroupController = new PriceListGroupController();
            bool success = priceListGroupController.DeletePriceListGroup(id);

            ResponseProc(success, "");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FindGrandServices(int draw, int start, int length, string searchVal)
        {
            if (!loginSystem.IsAdminLoggedIn() && !loginSystem.IsStaffLoggedIn()) return;

            GrandServiceController grandServiceController = new GrandServiceController();
            SearchResult searchResult = grandServiceController.SearchBy(start, length, searchVal);

            JSDataTable result = new JSDataTable();
            result.data = (IEnumerable<object>)searchResult.ResultList;
            result.draw = draw;
            result.recordsTotal = searchResult.TotalCount;
            result.recordsFiltered = searchResult.TotalCount;

            ResponseJson(result);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void AdminDeleteGrandService(int id)
        {
            //Is Logged in?
            if (!loginSystem.IsAdminLoggedIn()) return;

            GrandServiceController grandServiceController = new GrandServiceController();
            bool success = grandServiceController.DeleteGrandService(id);

            ResponseProc(success, "");
        }

        protected void ResponseJson(Object result)
        {
            HttpResponse Response = Context.Response;
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(serializer.Serialize(result));
        }
        protected void ResponseJson(Object result, bool success)
        {
            HttpResponse Response = Context.Response;
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(serializer.Serialize(result));
            if (success)
            {
                Response.StatusCode = 200;
            }
            Response.Flush();
        }

        protected void ResponseProc(bool success, object data, string message = "")
        {
            ProcResult result = new ProcResult();
            result.success = success;
            result.data = data;
            result.message = message;
            ResponseJson(result, success);
        }
    }
}
