<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="EmployeeViewEdit.aspx.cs" Inherits="B2B.EmployeeViewEdit" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <nav class="navbar navbar-expand-lg bg-light fixed-top shadow-lg">
        <div class="container">
          <!--   <a class="navbar-brand" href="javascript:;">BnB <span class="tooplate-green">Host</span></a>-->
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
                            <li id="liLogOut" runat="server"><a class="dropdown-item" href="EmployeeLogin.aspx">Log Out</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <main style="font-size: 20px; padding-top: 30px" class="bg">
        <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
            <section class=" mb-5">
                <header class="text-center">
                    <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">ORDINI</h2>
                </header>
                <div class="container">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-12">
                            <div class="input-group align-items-center">
                                <label for="status">Host: </label>
                                <asp:DropDownList ID="ComboOwner" runat="server" CssClass="form-control mr-md" CausesValidation="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-3 col-12">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="input-group align-items-center">
                                <ContentTemplate>
                                    <label for="status">Room: </label>
                                    <asp:DropDownList ID="ComboRoom" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ComboOwner" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-lg-4 col-md-4 col-12">
                            <div class="input-group align-items-center">
                                <label for="status">Nr. di Ospiti: </label>
                                <asp:TextBox ID="TxtNumberOfGuests" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" TextMode="Number" min="1"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-12">
                            <div class="input-group align-items-center">
                                <label for="product-name">Dal: </label>
                                <asp:TextBox ID="TxtDateFrom" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"
                                    data-plugin-datepicker="" data-plugin-options='{ "format": "dd/mm/yyyy", "todayHighlight": "true" }'></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6 col-12">
                            <div class="input-group align-items-center">
                                <label for="product-name">Al: </label>
                                <asp:TextBox ID="TxtDateTo" CssClass="form-control" runat="server" ClientIDMode="Static"
                                    data-plugin-datepicker="" data-plugin-options='{ "format": "dd/mm/yyyy", "todayHighlight": "true" }'></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="paymentDiv" visible="false">
                        <div class="col-lg-6 col-md-6 col-6">
                            <div class="input-group align-items-center">
                                <label for="status">Pagamento Info: </label>
                                <asp:TextBox ID="TxtPaymentResult" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-6">
                            <div class="input-group align-items-center">
                                <label for="status">Voucher: </label>
                                <asp:TextBox ID="TxtVoucher" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="row m-xs">
                                    <asp:ValidationSummary ID="ValSummary2" runat="server" ValidationGroup="ValServiceQuantity" CssClass="col-sm-12 asp-validation-message" />
                                    <asp:CustomValidator ID="ServerValidator3" runat="server" ErrorMessage="Please select service quantity." ValidationGroup="ValServiceQuantity" Display="None"></asp:CustomValidator>
                                </div>
                                <asp:HiddenField ID="HfServiceAlloc" runat="server" ClientIDMode="Static" />
                                <asp:Repeater ID="ServiceRepeater" runat="server">
                                    <HeaderTemplate>
                                        <div class="table-responsive">
                                            <table class="table table-bordered table-striped text-center">
                                                <thead>
                                                    <tr>
                                                        <th>Servizio</th>
                                                        <th>Descrizione</th>
                                                        <%--<th>Price</th>--%>
                                                        <th>Q.tà</th>
                                                        <%--<th>Amount</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="text-center">
                                                <%--<img src="" runat="server" width="160" height="160"/>--%>
                                                Service <%# Eval("ServiceId")%>
                                            </td>
                                            <td><%# Eval("Description")%></td>
                                            <%--<td><%# Eval("Price")%></td>--%>
                                            <td><%# Eval("Quantity")%></td>
                                            <%--<td><%# Eval("Amount")%></td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                    </table>
                                    </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row hidden-input">
                        <div class="col-lg-6 col-md-6 col-6 ms-auto">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="input-group align-items-center">
                                <ContentTemplate>
                                    <label for="product-name">Totale: </label>
                                    <asp:TextBox ID="TxtTotalAmount" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-12">
                            <div class="input-group align-items-center">
                                <label for="status">Note: </label>
                                <asp:TextBox ID="TxtNote" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row text-right">
                        <asp:Button ID="BtnCancel" runat="server" Text="Chiudi" CausesValidation="False" OnClick="BtnCancel_Click" CssClass="btn btn-lg btn-secondary col-md-2 col-6 ms-auto me-3" />
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

                    <p class="text-white"><a href="https://www.softforbet.com" class="text-white" target="_parent">Web Design: SFB.it</a></p>
                </div>

                <div class="col-lg-3 col-md-3 col-12 ms-lg-auto mb-3">
                    <h3 class="text-white mb-3">Store</h3>

                    <p class="text-white mt-2">
                        <i class="bi-geo-alt"></i>
                        Via Pigna, 82-80129
                            Naples (NA)
                    </p>
                </div>

                <div class="col-lg-4 col-md-4 col-12 mb-3">
                    <h3 class="text-white mb-3">Contact Info</h3>

                    <p class="text-white mb-1">
                        <i class="bi-telephone me-1"></i>

                        <a href="tel: 090-080-0760" class="text-white">(+39)-05555555
                        </a>
                        <a href="tel: 090-080-0760" class="text-white">(+39)-55555555
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
                    <p class="text-white">Reception:</p>
                    

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
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script src="Scripts/dataTables.responsive.min.js"></script>


    <!-- Theme Base, Components and Settings -->
    <script src="Scripts/theme.js?v=1"></script>

    <!-- Theme Custom -->
    <script src="Scripts/theme.custom.js"></script>

    <!-- Theme Initialization Files -->
    <script src="Scripts/theme.init.js"></script>

    <script src="Scripts/util.js"></script>


    <script type="text/javascript">
        $(function () {
            $(".table-bordered").dataTable({
                "dom": '<"table-responsive"t>',
                "responsive": true,
                "autoWidth": false,
            });

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
</asp:Content>
