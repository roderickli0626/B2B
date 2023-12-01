<%@ Page Title="" Language="C#" MasterPageFile="~/StaffPage.master" AutoEventWireup="true" CodeBehind="StaffHome.aspx.cs" Inherits="B2B.StaffHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffHeaderPlaceHolder" runat="server">
    <link rel="stylesheet" href="Content/CSS/datatables.css" />
    <link rel="stylesheet" href="Content/CSS/responsive.dataTables.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StaffContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-10 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4">ORDINI</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row mt-lg-5 mb-2">
                    <div class="col-lg-12 col-md-12 col-12">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" class="overflow-auto">
                            <ContentTemplate>
                                <asp:Calendar ID="Calendar1" runat="server" CssClass="text-center m-auto" BackColor="#FFFFCC" BorderColor="#FFCC66"
                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="12pt"
                                    ForeColor="#663399" ShowGridLines="True" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"
                                    OnVisibleMonthChanged="Calendar1_VisibleMonthChanged">
                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                    <SelectorStyle BackColor="#FFCC66" />
                                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" CssClass="text-center" />
                                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                </asp:Calendar>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ComboStatus" />
                                <asp:AsyncPostBackTrigger ControlID="TxtDateFrom" />
                                <asp:AsyncPostBackTrigger ControlID="TxtDateTo" />
                                <asp:AsyncPostBackTrigger ControlID="TxtSearch" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="row mt-lg-5">
                    <div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Stato: </label>
                            <asp:DropDownList ID="ComboStatus" AutoPostBack="true" OnSelectedIndexChanged="ComboStatus_SelectedIndexChanged" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Dal: </label>
                            <asp:TextBox ID="TxtDateFrom" AutoPostBack="true" OnTextChanged="TxtDateFrom_TextChanged" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"
                            data-plugin-datepicker="" data-plugin-options='{ "format": "dd/mm/yyyy", "todayHighlight": "true" }'></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Al: </label>
                            <asp:TextBox ID="TxtDateTo" AutoPostBack="true" OnTextChanged="TxtDateTo_TextChanged" CssClass="form-control" runat="server" ClientIDMode="Static"
                                data-plugin-datepicker="" data-plugin-options='{ "format": "dd/mm/yyyy", "todayHighlight": "true" }'></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div>
                    <div class="row">
                        <div style="float: left; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12 mb-2">
                            <asp:LinkButton ID="BtnAdd" runat="server" CausesValidation="false" OnClick="BtnAdd_Click" CssClass="btn btn-warning btn-lg text-white w-100">
                            <i class="fa fa-plus mr-sm"></i> Agg.
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-3 col-md-4 col-0"></div>
                        <div class="col-lg-3 col-md-2 col-12 mb-2" style="text-align:right;">
                            <asp:Button runat="server" ID="BtnDownloadPDF" CssClass="btn btn-warning btn-lg text-white w-100" Text="Stampa PDF" OnClick="BtnDownloadPDF_Click" />
                        </div>
                        <div style="float: right; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12">
                            <div class="input-group align-items-center">
                                <label for="product-name">Cerca: </label>
                                <asp:TextBox ID="TxtSearch" AutoPostBack="true" OnTextChanged="TxtSearch_TextChanged" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <table class="table table-bordered table-striped text-center" id="order-table">
                        <thead>
                            <tr>
                                <th>Nr. Ordine</th>
                                <th>Cliente</th>
                                <th>Dal</th>
                                <th>Al</th>
                                <th>Num. di Ospiti</th>
                                <th>Totale</th>
                                <th>Stato</th>
                                <th>Assegnato a</th>
                                <th>Azione</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="StaffFooterPlaceHolder" runat="server">
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script src="Scripts/dataTables.responsive.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var datatable = $('#order-table').dataTable({
                "serverSide": true,
                "ajax": 'DataService.asmx/FindOrders',
                "dom": '<"table-responsive"t>pr',
                "autoWidth": false,
                "pageLength": 20,
                "processing": true,
                "ordering": false,
                "responsive": true,
                "columns": [{
                    "data": "Id",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                }, {
                    "data": "Owner",
                }, {
                    "data": "StartDate",
                    "type": "date"
                }, {
                    "data": "EndDate",
                    "type": "date"
                }, {
                    "data": "NumberOfGuests"
                }, {
                    "data": "TotalAmount",
                    "render": function (data, type, row, meta) {
                        return data + " €";
                    }
                }, {
                    "data": "Status",
                    "render": function (data, type, row, meta) {
                        switch (data) {
                            case 0: return ""; break;
                            case 1: return "<p class='text-primary mb-0'>Confermato</p>"; break;
                            case 2: return "<p class='text-success mb-0'>Pagato</p>"; break;
                            case 3: return "<p class='text-warning mb-0' title='" + row.EmployeeName + "'>Assegnato</p>"; break;
                            case 4: return "<p class='text-danger mb-0'>Chiuso</p>"; break;
                        }
                    }
                    }, {
                        "data": "EmployeeName",
                        "render": function (data, type, row, meta) {
                            return data == null ? "" : data;
                        }
                    }, {
                    "data": null,
                    "render": function (data, type, row, meta) {
                        return '<a href="StaffOrderEdit.aspx?id=' + row.Id + '"><i class="fa fa-edit" style="font-size:25px"></i></a>';
                    }
                }], 

                "fnServerParams": function (aoData) {
                    aoData.dateFrom = $('#TxtDateFrom').val();
                    aoData.dateTo = $('#TxtDateTo').val();
                    aoData.status = $('#ComboStatus').val();
                    aoData.searchVal = $('#TxtSearch').val();
                }
            });

            $('#ComboStatus, #TxtDateFrom, #TxtDateTo').change(function () {
                datatable.fnDraw();
            });

            $('#TxtSearch').on('input', function () {
                datatable.fnDraw();
            });

            var onSuccess = function (data) {
                if (data.success) {

                    datatable.fnDraw();

                } else {
                    alert("Failed!");
                }
            };

            $("#TxtDateFrom").datepicker({
                autoclose: true
            }).on('changeDate', function (e) {
                //on change of date on start datepicker, set end datepicker's date
                $('#TxtDateTo').datepicker('setStartDate', e.date)
                $('#TxtDateTo').val($("#TxtDateFrom").val())
            });

            $("#TxtDateTo").datepicker({
                autoclose: true
            });

            $("#ComboStatus").select2({ theme: 'bootstrap' }); 
        });
    </script>
</asp:Content>
