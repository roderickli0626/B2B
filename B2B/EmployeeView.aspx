<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="EmployeeView.aspx.cs" Inherits="B2B.EmployeeView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <link href="Content/CSS/font-awesome.css" rel="stylesheet">
    <link href="Content/CSS/magnific-popup.css" rel="stylesheet">
    <link href="Content/CSS/select2.css" rel="stylesheet">
    <link href="Content/CSS/select2-bootstrap.css" rel="stylesheet">
    <link href="Content/CSS/bootstrap-datepicker3.css" rel="stylesheet">
    <link href="Content/CSS/bootstrap-timepicker.min.css" rel="stylesheet">
    <link href="Content/CSS/pnotify.custom.css?v=1" rel="stylesheet" />

    <link rel="stylesheet" href="Content/CSS/jquery-ui-1.10.4.custom.css" />
    <link rel="stylesheet" href="Content/CSS/bootstrap-multiselect.css" />
    <!-- Theme CSS -->
    <%--<link rel="stylesheet" href="Content/CSS/theme.css" />--%>

    <!-- Skin CSS -->
    <link rel="stylesheet" href="Content/CSS/default.css" />

    <!-- Theme Custom CSS -->
    <link rel="stylesheet" href="Content/CSS/theme-custom.css?v=5" />
    <link rel="stylesheet" href="Content/CSS/admin-custom.css" />
    <link rel="stylesheet" href="Content/CSS/datatables.css" />
    <link rel="stylesheet" href="Content/CSS/responsive.dataTables.min.css" />

    <link rel="stylesheet" href="Content/CSS/select2.css" />
    <link rel="stylesheet" href="Content/CSS/select2-bootstrap.css" />
    <style>
        .select2-selection.select2-selection--single {
            box-shadow: none !important;
            border: none;
        }
        .select2.select2-container.select2-container--bootstrap {
            margin-left: -3px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <nav class="navbar navbar-expand-lg bg-light fixed-top shadow-lg">
        <div class="container">
            <!-- <a class="navbar-brand" href="javascript:;">BnB <span class="tooplate-green">Host</span></a>-->
            <img src="https://gestionale.bnbhosts.it/Content/Images/Logo_BNB_HOSTS.png" width="110" height="50" >


            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>

            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li id="liOut" runat="server" class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle bi-person-circle" href="#" id="navbarLightDropdownMenuLink2" role="button" data-bs-toggle="dropdown" aria-expanded="false"></a>

                        <ul class="dropdown-menu dropdown-menu-light" aria-labelledby="navbarLightDropdownMenuLink1">
                            <li id="liUser" runat="server"><a class="dropdown-item text-bold" href="#" runat="server" id="username">qqq</a></li>
                            <li id="liLogOut" runat="server"><a class="dropdown-item" href="EmployeeLogin.aspx">Esci</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <main style="font-size: 20px; padding-top: 30px" class="bg">
        <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-10 pb-lg-5 bg-black bg-opacity-50">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-white mt-3 mb-4">ORDINI ASSEGNATI</h2>
            </header>
            <div class="container">
                <asp:HiddenField ID="HfEmployeeID" runat="server" ClientIDMode="Static" /> 
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
                        <div class="col-lg-9 col-md-9 col-0"></div>
                        <div style="float: right; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12">
                            <div class="input-group align-items-center">
                                <label for="product-name">Cerca: </label>
                                <asp:TextBox ID="TxtSearch" AutoPostBack="true" OnTextChanged="TxtSearch_TextChanged" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <table class="table table-bordered table-striped text-center table-hover bg-white" id="order-table">
                        <thead>
                            <tr>
                                <th>Nr.</th>
                                <th>Cliente</th>
                                <th>Data Inizio</th>
                                <th>Data Fine</th>
                                <th>Num. Ospiti</th>
                                <th>Totale</th>
                                <th>Stato</th>
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

        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320">
            <path fill="#36363e" fill-opacity="1" d="M0,96L40,117.3C80,139,160,181,240,186.7C320,192,400,160,480,149.3C560,139,640,149,720,176C800,203,880,245,960,250.7C1040,256,1120,224,1200,229.3C1280,235,1360,277,1400,298.7L1440,320L1440,320L1400,320C1360,320,1280,320,1200,320C1120,320,1040,320,960,320C880,320,800,320,720,320C640,320,560,320,480,320C400,320,320,320,240,320C160,320,80,320,40,320L0,320Z"></path>
        </svg>
    </main>

    <footer class="site-footer section-padding">
        <div class="container">
            <div class="row">
                <div class="col-lg-5 col-md-5 col-12 mb-3">
               <!-- <h3><a href="index.html" class="custom-link mb-1">BnB Host</a></h3>-->
                    <a href="login.aspx"> <img src="https://gestionale.bnbhosts.it/Content/Images/Logo_BNB_HOSTS_invertito.png" width="110" height="50" > </a>

                    <p class="text-white">Since 2023, We started services for room rental</p>

                    <p class="text-white"><a href="https://www.softforbet.it" class="text-white" target="_parent">Web Design: SFB</a></p>
                </div>

                <div class="col-lg-3 col-md-3 col-12 ms-lg-auto mb-3">
                    <h3 class="text-white mb-3">Store</h3>

                    <p class="text-white mt-2">
                        <i class="bi-geo-alt"></i>
                        Piazzetta Rosario di Palazzo, 19
                            80132 - Napoli (NA)
                    </p>
                </div>

                <div class="col-lg-4 col-md-4 col-12 mb-3">
                    <h3 class="text-white mb-3">Contact Info</h3>

                    <p class="text-white mb-1">
                        <i class="bi-telephone me-1"></i>

                        <a href="tel: 090-080-0760" class="text-white">(+39)-08155225544112
                        </a>
                        <a href="tel: 090-080-0760" class="text-white">(+39)-335856288774
                        </a>
                    </p>

                    <p class="text-white mb-0">
                        <i class="bi-envelope me-1"></i>

                        <a href="mailto:info@bnbhosts.it" class="text-white">info@bnbhosts.it
                        </a>
                    </p>
                </div>
            </div>
            <div class="row mt-5 pt-lg-5">
                <div class="col-lg-5 col-md-5 col-12 mb-3">
                    <h5 class="text-white mb-3">BBH Management s.r.l.</h5>
                    <p class="text-white">Registered office:</p>
                    <p class="text-white">Piazzetta Rosario di Palazzo, 19 - 80132 Napoli (NA)</p>

                    <p class="text-white">Partita Iva: 10380201219</p>
                </div>

                <div class="col-lg-3 col-md-3 col-12 ms-lg-auto mb-3">
                    <h5 class="text-white mb-3">Informazioni</h5>

                    <p class="text-white"><a href="ContactPage.aspx" class="text-white" target="_blank">Contacts</a></p>
                    <p class="text-white"><a href="Condition.aspx" class="text-white" target="_blank">Services & Conditions</a></p>
                    <p class="text-white"><a href="Privacy.aspx" class="text-white" target="_blank">Privacy Policy</a></p>
                </div>

                <div class="col-lg-4 col-md-4 col-12 mb-3">
                    <h5 class="text-white mb-3">Orario</h5>

                    <p class="text-white">Lunedi - Sabato: 09:00/20:00</p>
                    <p class="text-white"></p>
                    
                </div>

            </div>
        </div>
    </footer>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <script src="Scripts/modernizr.js"></script>
    <%--<script src="Scripts/jquery.min.js"></script>--%>

    <script src="Scripts/jquery.browser.mobile.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/nanoscroller.js"></script>
    <script src="Scripts/bootstrap-datepicker.min.js?v=1.7.0"></script>
    <script src="Scripts/jquery.magnific-popup.js"></script>
    <script src="Scripts/jquery-placeholder.js"></script>
    <script src="Scripts/pnotify.custom.js"></script>



    <!-- Theme Base, Components and Settings -->
    <script src="Scripts/theme.js?v=1"></script>

    <!-- Theme Custom -->
    <script src="Scripts/theme.custom.js"></script>

    <!-- Theme Initialization Files -->
    <script src="Scripts/theme.init.js"></script>

    <script src="Scripts/util.js"></script>


    <script type="text/javascript">
        $(function () {
            window.jQBrowser = getBrowserInfo();

            if (!isBootsrapSupported(window.jQBrowser)) {
                new PNotify({
                    title: 'Browser issue',
                    text: 'This browser agent doesn\'t support full functionality of our service',
                    type: 'error'
                });
            }

            if (IsInternetExplorer()) {
                $(".IE-browser-msg").removeClass("hidden");
                //new PNotify({
                //    title: 'Browser issue',
                //    text: 'Internet Explorer is not supported. We recommend using Google Chrome',
                //    type: 'error'
                //});
            }

            $(window).resize(function () {
                fixDataTableAlignment();
            });

        });
        function fixDataTableAlignment() {
            if ($(".dataTables_scrollBody").length != 0) {
                $(".dataTables_scrollBody").each(function (i, element) {
                    if ($(element).prev().hasClass("dataTables_scrollHead")) {
                        var css = $(element).find("tbody tr:first").css("width");
                        $(element).prev().find("table").css("width", css);

                        var theads = $(element).prev().find("tr:first th");
                        var theadQuickSearch = $(element).prev().find(".quickSearchRow th");

                        //Skip when there are empty rows in table
                        if ($(element).find("tbody tr:first td").length > 1) {
                            $(element).find("tbody tr:first td").each(function (j, td) {
                                var cssWidth = $(td).css("width");
                                if (theads[j])
                                    $(theads[j]).css("width", cssWidth);
                                if (theadQuickSearch[j])
                                    $(theadQuickSearch[j]).css("width", cssWidth);
                            });
                        }

                    }
                })
            }
        }
    </script>

    <script src="Scripts/jquery-ui-1.10.4.custom.js"></script>
    <script src="Scripts/jquery.ui.touch-punch.js"></script>
    <script src="Scripts/jquery.appear.js"></script>
    <script src="Scripts/bootstrap-multiselect.js"></script>
    <script src="Scripts/jquery.autosize.js"></script>

    <script src="Scripts/select2.js"></script>
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script src="Scripts/dataTables.responsive.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var datatable = $('#order-table').dataTable({
                "serverSide": true,
                "ajax": 'DataService.asmx/FindEmployeeOrders',
                "dom": '<"table-responsive"t>pr',
                "autoWidth": false,
                "pageLength": 20,
                "processing": true,
                "ordering": false,
                "responsive": true,
                "columns": [{
                    "data": "Id",
                    "render": function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
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
                            case 1: return "<p class='text-primary mb-0'>Confermati</p>"; break;
                            case 2: return "<p class='text-success mb-0'>Pagati</p>"; break;
                            case 3: return "<p class='text-warning mb-0'>Assegnati</p>"; break;
                            case 4: return "<p class='text-danger mb-0'>Chiusi</p>"; break;
                        }
                    }
                }, {
                    "data": null,
                    "render": function (data, type, row, meta) {
                        return '<a href="EmployeeViewEdit.aspx?id=' + row.Id + '&employeeID=' + $('#HfEmployeeID').val() + '"><i class="fa fa-eye" style="font-size:25px"></i></a>';
                    }
                }], 

                "fnServerParams": function (aoData) {
                    aoData.employeeId = $('#HfEmployeeID').val();
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
