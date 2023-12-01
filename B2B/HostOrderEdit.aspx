<%@ Page Title="" Language="C#" MasterPageFile="~/HostPage.master" AutoEventWireup="true" CodeBehind="HostOrderEdit.aspx.cs" Inherits="B2B.HostOrderEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HostHeaderPlaceHolder" runat="server">
    <link rel="stylesheet" href="Content/CSS/datatables.css" />
    <link rel="stylesheet" href="Content/CSS/responsive.dataTables.min.css" />
    <link rel="stylesheet" href="Content/CSS/select2.css" />
    <link rel="stylesheet" href="Content/CSS/select2-bootstrap.css" />
    <style>
        .accessoryImage {
            transition: transform .2s; /* Animation */
            width: 100px;
            height: 100px;
        }

        .accessoryImage:hover {
            transform: scale(2.0);
        }

        .select2-selection.select2-selection--single {
            box-shadow: none !important;
            border: none;
        }
        .select2.select2-container.select2-container--bootstrap {
            margin-left: -3px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HostContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5 bg-black bg-opacity-50">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-white mt-3 mb-4" runat="server" id="pageTitle">ORDINI</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" />
                    <asp:RequiredFieldValidator ID="ReqValStartDate" runat="server" ErrorMessage="Inserire la Data Iniziale." ControlToValidate="TxtDateFrom" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValEndDate" runat="server" ErrorMessage="Inserire la Data Finale." ControlToValidate="TxtDateTo" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValGuests" runat="server" ErrorMessage="Inserire il numero di Ospiti." ControlToValidate="TxtNumberOfGuests" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator1" runat="server" ErrorMessage="Salvataggio Fallito." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator0" runat="server" ErrorMessage="Selezionare il Cliente." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator4" runat="server" ErrorMessage="Selezionare la Room." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator2" runat="server" ErrorMessage="Selezionare i Servizi." Display="None"></asp:CustomValidator>
                </div>

                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Room: </label>
                            <asp:DropDownList ID="ComboRoom" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="input-group align-items-center">
                            <label for="status">Ospiti: </label>
                            <asp:TextBox ID="TxtNumberOfGuests" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" TextMode="Number" min="1"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Dal: </label>
                            <asp:TextBox ID="TxtDateFrom" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"
                                data-plugin-datepicker="" data-plugin-options='{ "format": "dd/mm/yyyy", "todayHighlight": "true", "startDate": "0d" }'></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Al: </label>
                            <asp:TextBox ID="TxtDateTo" CssClass="form-control" runat="server" ClientIDMode="Static"
                                data-plugin-datepicker="" data-plugin-options='{ "format": "dd/mm/yyyy", "todayHighlight": "true", "startDate": "1d" }'></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="paymentDiv" visible="true">
                    <div class="col-lg-6 col-md-6">
                        <div class="input-group align-items-center">
                            <label for="status">Pagamento Info: </label>
                            <asp:TextBox ID="TxtPaymentResult" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="input-group align-items-center">
                            <label for="status">Voucher: </label>
                            <asp:TextBox ID="TxtVoucher" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="row" runat="server" id="serviceDiv">
                        <div class="col-lg-4 col-md-4 col-12">
                            <div class="input-group align-items-center" style="height: 52px">
                                <label for="status">Categoria: </label>
                                <asp:DropDownList ID="ComboGrandService" runat="server" CssClass="form-control mr-md" 
                                    ClientIDMode="Static" CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="ComboGrandService_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-12">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="input-group align-items-center" style="height: 52px">
                                <ContentTemplate>
                                    <label for="status">Servizio: </label>
                                    <asp:DropDownList ID="ComboService" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ComboGrandService" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-lg-2 col-md-2">
                            <div class="input-group align-items-center">
                                <label for="product-name">Q.tà: </label>
                                <asp:TextBox ID="TxtQuantity" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" TextMode="Number" min="1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 ms-auto mb-2" >
                            <asp:LinkButton ID="BtnAddService" runat="server" CausesValidation="false" OnClick="BtnAddService_Click" ClientIDMode="Static" CssClass="btn btn-danger btn-lg text-white w-100">
                            <i class="fa fa-plus mr-sm"></i> Aggiungi
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-1 col-md-1 mb-2"  runat="server" id="RecoverDiv" visible="false">
                            <asp:LinkButton ID="BtnRecoverService" runat="server" CausesValidation="false" OnClick="BtnRecoverService_Click" ClientIDMode="Static" CssClass="btn btn-danger btn-lg text-white w-100" ToolTip="Richiama ordine precedente?">
                            <i class="fa fa-refresh mr-sm"></i>Richiama
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="row m-xs">
                                <asp:ValidationSummary ID="ValSummary2" runat="server" ValidationGroup="ValServiceQuantity" CssClass="col-sm-12 asp-validation-message" />
                                <asp:CustomValidator ID="ServerValidator3" runat="server" ErrorMessage="Selezionare Servizio e quantita'." ValidationGroup="ValServiceQuantity" Display="None"></asp:CustomValidator>
                                <asp:CustomValidator ID="ServerValidator5" runat="server" ErrorMessage="Selezionare la stanza." ValidationGroup="ValServiceQuantity" Display="None"></asp:CustomValidator>
                            </div>
                            <asp:HiddenField ID="HfServiceAlloc" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="HfPaymentID" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="HfVoucherID" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="HfVoucherAmount" runat="server" ClientIDMode="Static" />
                            <asp:Repeater ID="ServiceRepeater" runat="server" OnItemCommand="ServiceRepeater_ItemCommand">
                                <HeaderTemplate>
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped text-center bg-white">
                                            <thead>
                                                <tr>
                                                    <th>Foto</th>
                                                    <th>Categoria Servizio</th>
                                                    <th>Servizio</th>
                                                    <th>Descrizione</th>
                                                    <th>Prezzo</th>
                                                    <th>Q.tà</th>
                                                    <th>Totale</th>
                                                    <th>Azione</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                        <tr>
                                            <td class="text-center">
                                                <asp:ImageButton runat="server" ID="ServiceImage" ImageUrl='<%# (Eval("Image") == "" || Eval("Image") == null) ? "Content/Images/service_default.jpg" : "~/Upload/Service/" + Eval("Image") %>' 
                                                    CssClass="accessoryImage" />
                                            </td>
                                            <td><%# Eval("GrandService")%></td>
                                            <td><%# Eval("Service")%></td>
                                            <td><%# Eval("Description")%></td>
                                            <td><%# Eval("Price") + " €"%></td>
                                            <td><%# Eval("Quantity")%></td>
                                            <td><%# Eval("Amount") + " €"%></td>
                                            <td>
                                                <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" OnClientClick="return confirm('Click OK per cancellare.');"
                                                    CommandName="Delete" CommandArgument='<%#Eval("ServiceId") %>'>
                                                    <i class="fa fa-trash style="font-size:25px""></i>
                                                </asp:LinkButton>
                                            </td>
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
                            <asp:AsyncPostBackTrigger ControlID="BtnAddService" />
                            <asp:AsyncPostBackTrigger ControlID="BtnRecoverService" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6" runat="server" id="assignDiv">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Assegnato: </label>
                            <asp:DropDownList ID="ComboAssignedTo" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 ms-auto">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="input-group align-items-center">
                            <ContentTemplate>
                                <label for="product-name">Totale: </label>
                                <asp:TextBox ID="TxtTotalAmount" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                <label class="ms-auto">€</label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnAddService" />
                                <asp:AsyncPostBackTrigger ControlID="BtnRecoverService" />
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
                <div class="row" runat="server" id="payDiv">
                    <div class="col-lg-6 col-md-6">
                        <div class="input-group align-items-center">
                            <label>VoucherID: </label>
                            <asp:TextBox ID="TxtVoucherID" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            <asp:UpdatePanel runat="server" ID="VoucherUpdatepanel">
                                <ContentTemplate>
                                    <p runat="server" id="voucherOK" class="text-bg-light text-success m-2" visible="false"></p>
                                    <p runat="server" id="voucherNo" class="text-bg-light text-danger m-2" visible="false"></p>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="BtnCheckVoucher" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 mb-2">
                        <asp:Button ID="BtnCheckVoucher" runat="server" Text="Voucher Check" CausesValidation="False" Width="100%" CssClass="btn btn-lg btn-primary" OnClick="BtnCheckVoucher_Click" />
                    </div>
                    <div class="col-lg-3 col-md-3 mb-2">
                        <asp:Button ID="Paypal0" runat="server" ClientIDMode="Static" Text="Payment" Width="100%" CssClass="btn btn-lg bg-success text-white" />
                        <asp:Button ID="Paypal" runat="server" ClientIDMode="Static" Text="Payment" Width="100%" CssClass="btn btn-lg bg-success text-white d-none" OnClick="Paypal_ServerClick" OnClientClick="ShowInfo()" />
                        <%--<a href="#" runat="server" id="Paypal" onserverclick="Paypal_ServerClick" class="btn btn-lg bg-success text-white" style="width:100%;">Payment</a>--%>
                    </div>
                </div>
                <div class="row text-right">
                    <asp:Button ID="BtnSave" runat="server" Text="Salva" OnClick="BtnSave_Click" CssClass="btn btn-lg btn-warning col-md-2 col-5 ms-auto" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/HostOrder.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
                </div>
            </div>
        </section>
    <!-- The Modal -->
    <div class="modal" id="myModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header pl-5">
                    <h4 class="modal-title">MODALITA' DI PAGAMENTO</h4>
                </div>

                <!-- Modal body -->
                <div class="modal-body p-lg-2">
                    <asp:UpdatePanel runat="server" ID="UpdatePanelForModal">
                        <ContentTemplate>
                            <asp:HiddenField ID="HfPaymentType" runat="server" ClientIDMode="Static" />
                            <asp:RadioButtonList ID="PaymentType" CssClass="mx-auto" Style="font-size: 22px;" runat="server">
                                <asp:ListItem Text=" Contanti" id="Contanti" clientIDMode="Static" Value="1"></asp:ListItem>
                                <asp:ListItem Text=" Bonifico" id="Bonifico" clientIDMode="static" Value="2"></asp:ListItem>
                                <asp:ListItem Text=" Paypal" Value="3" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnOK" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <!-- Modal footer -->
                <div class="modal-footer p-lg-3 justify-content-around">
                    <asp:Button runat="server" ID="BtnOK" ClientIDMode="Static" CssClass="btn btn-lg btn-success mr-5" Text="OK" OnClick="BtnOK_Click" />
                    <asp:Button runat="server" ID="BtnClose" ClientIDMode="Static" Text="CANCEL" CssClass="btn btn-lg btn-dark" />
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HostFooterPlaceHolder" runat="server">
    <script src="Scripts/select2.js"></script>
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script src="Scripts/dataTables.responsive.min.js"></script>
    <script type="text/javascript">
        $("#BtnClose").click(function () {
            $("#Contanti").removeClass("d-none");
            $("#Bonifico").removeClass("d-none");
            $("#myModal").modal('hide');
            return false;
        });

        $("#Paypal0").click(function () {
            if ($("#HfVoucherID").val() == "") {
                $("#myModal").modal('show');
                return false;
            }
            else {
                var total = $("#TxtTotalAmount").val();
                var voucher = $("#HfVoucherAmount").val();

                if (total == voucher) {
                    return false;
                }
                if (voucher == "0") {
                    $("#myModal").modal('show');
                    return false;
                }
                else {
                    $("#myModal").modal('show');
                    $("#Contanti").addClass("d-none");
                    $("#Bonifico").addClass("d-none");
                    return false;
                }
            }
        });

        function MyFun() {
            $("#ComboService").select2({ theme: 'bootstrap' });

            $(".table-bordered").dataTable({
                "dom": '<"table-responsive"t>',
                "responsive": true,
                "autoWidth": false,
            });
        }

        function ShowInfo() {
            $("#myModal").modal('hide');

            if ($("#HfVoucherID").val() == "") return true;
            else {
                var total = $("#TxtTotalAmount").val();
                var voucher = $("#HfVoucherAmount").val();
                var message = "";
                if (total == voucher) {
                    message = "Totale Ordine €" + total + " dei quali €" + voucher + " detratti dal Voucher prepagato.";
                }
                else {
                    message = "Totale Ordine €" + total + " dei quali €" + voucher + " detratti dal Voucher e altri €" + (total - voucher) + " che pagherai con Paypal. Ora verrai inviato alla pagina di Paypal per rimettere la differenza.";
                }
                alert(message);
            }
        }

        $(function () {
            $(".table-bordered").dataTable({
                "dom": '<"table-responsive"t>',
                "responsive": true,
                "autoWidth": false,
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

            $("#ComboRoom").select2({ theme: 'bootstrap' });
            $("#ComboGrandService").select2({ theme: 'bootstrap' });
            $("#ComboService").select2({ theme: 'bootstrap' }); 
            $("#ComboAssignedTo").select2({ theme: 'bootstrap' }); 
        })
    </script>
</asp:Content>
