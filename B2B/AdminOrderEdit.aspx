<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.master" AutoEventWireup="true" CodeBehind="AdminOrderEdit.aspx.cs" Inherits="B2B.AdminOrderEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeaderPlaceHolder" runat="server">
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

        .select2-selection.select2-selection--multiple {
            box-shadow: none !important;
            border: 1px solid rgba(255, 255, 255, 0.17);
            background-color: transparent;
            width:100%;
        }
        .select2-container--default.select2-container, .selection {
            width:100% !important;
        }
        .select2-container--default.select2-container--focus .select2-selection--multiple {
            border: 1px solid rgba(255, 255, 255, 0.17);
            width:100%;
        }
        #select2-ComboAssignedTo-container {
            text-align: center
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
        <asp:HiddenField ID="HfAssignedIDs" runat="server" ClientIDMode="Static" />
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">ORDINI</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" />
                    <asp:RequiredFieldValidator ID="ReqValStartDate" runat="server" ErrorMessage="Inserire la data di inizio." ControlToValidate="TxtDateFrom" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValEndDate" runat="server" ErrorMessage="Inserire la data di fine ordine." ControlToValidate="TxtDateTo" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValGuests" runat="server" ErrorMessage="Inserire il numero di Ospiti." ControlToValidate="TxtNumberOfGuests" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator1" runat="server" ErrorMessage="Salvataggio Fallito." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator0" runat="server" ErrorMessage="Selezionare un HOST." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator4" runat="server" ErrorMessage="Selezionare una ROOM." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator2" runat="server" ErrorMessage="Selezionare almeno un SERVIZIO." Display="None"></asp:CustomValidator>
                </div>

                <div class="row">
                    <div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Cliente: </label>
                            <asp:DropDownList ID="ComboOwner" runat="server" CssClass="form-control mr-md" CausesValidation="false"
                                AutoPostBack="true" OnSelectedIndexChanged="ComboOwner_SelectedIndexChanged" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-12">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="input-group align-items-center" style="height: 52px">
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
                            <label for="status">Nr. Ospiti: </label>
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
                <div class="row" runat="server" id="statusDiv" visible="false">
                    <div class="col-lg-4 col-md-4 mx-auto" runat="server">
                        <div class="input-group align-items-center">
                            <label class="col-md-7">Stato Pagamento: </label>
                            <asp:CheckBox runat="server" ID="PaidStatus" CssClass="form-control radio-custom text-primary" Text="Pagato" />
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="paymentDiv" visible="true">
                    <div class="col-lg-6 col-md-6">
                        <div class="input-group align-items-center">
                            <label for="status">Pagamento: </label>
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
                                <label for="status">Categoria Servizio: </label>
                                <asp:DropDownList ID="ComboGrandService" runat="server" CssClass="form-control mr-md" 
                                    ClientIDMode="Static" CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="ComboGrandService_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-12">
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
                        <div class="col-lg-2 col-md-2 mb-2" style="text-align: right">
                            <asp:LinkButton ID="BtnAddService" runat="server" CausesValidation="false" OnClick="BtnAddService_Click" ClientIDMode="Static" CssClass="btn btn-danger btn-lg text-white w-100">
                            <i class="fa fa-plus mr-sm"></i> Agg. Servizio
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="row m-xs">
                                <asp:ValidationSummary ID="ValSummary2" runat="server" ValidationGroup="ValServiceQuantity" CssClass="col-sm-12 asp-validation-message" />
                                <asp:CustomValidator ID="ServerValidator3" runat="server" ErrorMessage="Inserire la quantità." ValidationGroup="ValServiceQuantity" Display="None"></asp:CustomValidator>
                                <asp:CustomValidator ID="ServerValidator5" runat="server" ErrorMessage="Selezionare la stanza." ValidationGroup="ValServiceQuantity" Display="None"></asp:CustomValidator>
                            </div>
                            <asp:HiddenField ID="HfServiceAlloc" runat="server" ClientIDMode="Static" />
                            <asp:Repeater ID="ServiceRepeater" runat="server" OnItemCommand="ServiceRepeater_ItemCommand">
                                <HeaderTemplate>
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped text-center">
                                            <thead>
                                                <tr>
                                                    <th>Foto</th>
                                                    <th>Categoria Servizio</th>
                                                    <th>Servizio</th>
                                                    <th>Descrizione</th>
                                                    <th>Prezzo</th>
                                                    <th>Q.tà</th>
                                                    <th>Totale</th>
                                                    <th>Azioni</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                        <tr>
                                            <td class="text-center">
                                                <asp:ImageButton runat="server" ID="ServiceImage" ImageUrl='<%# (Eval("Image") == "" || Eval("Image") == null) ? "Content/Images/service_default.jpg" : "~/Upload/Service/" + Eval("Image") %>' 
                                                    CssClass="accessoryImage" />
                                                <%--<img src="" runat="server" width="160" height="160"/>--%>
                                                <%--Service <%# Eval("ServiceId")%>--%>
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
                                                    <i class="fa fa-trash" style="font-size:25px"></i>
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
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="row">
                    <div class="col-lg-4 col-md-4 me-auto" runat="server" id="assignDiv">
                        <div class="input-group align-items-center" style="min-height: 52px">
                            <label for="status">Assegnato a: </label>
                            <asp:DropDownList ID="ComboAssignedTo" runat="server" CssClass="form-control mr-md" ClientIDMode="Static" Multiple="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4" runat="server" id="closeDiv">
                        <div class="input-group align-items-center">
                            <label  class="col-md-3">Chiuso: </label>
                            <asp:RadioButton ID="RadioButton1" runat="server" CssClass="form-control radio-custom text-primary" Text="No" GroupName="ClosedOption" Value="1" Checked="true" />
                            <asp:RadioButton ID="RadioButton2" runat="server" CssClass="form-control radio-custom text-success" Text="Sì" GroupName="ClosedOption" Value="2" />
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 ms-auto">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="input-group align-items-center">
                            <ContentTemplate>
                                <label for="product-name">Totale: </label>
                                <asp:TextBox ID="TxtTotalAmount" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                <label class="ms-auto">€</label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnAddService" />
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
                    <asp:Button ID="BtnSave" runat="server" Text="Salva" OnClick="BtnSave_Click" CssClass="btn btn-lg btn-warning col-md-2 col-5 ms-auto" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/AdminHome.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AdminFooterPlaceHolder" runat="server">
    <script src="Scripts/select2.js"></script>
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script src="Scripts/dataTables.responsive.min.js"></script>
    <script>
        var ids = $("#HfAssignedIDs").val();
        if (ids == null || ids == "") $('#ComboAssignedTo').val([]).trigger('change');
        else {
            selectedValues = ids.split(',');
            $('#ComboAssignedTo').val(selectedValues).trigger('change');
        }
    </script>
    <script type="text/javascript">
        function MyFun() {
            $("#ComboRoom").select2({ theme: 'bootstrap' });
            $("#ComboService").select2({ theme: 'bootstrap' });

            $(".table-bordered").dataTable({
                "dom": '<"table-responsive"t>',
                "responsive": true,
                "autoWidth": false,
            });
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

            $("#ComboGrandService").select2({ theme: 'bootstrap' });
            $("#ComboService").select2({ theme: 'bootstrap' });
            $("#ComboOwner").select2({ theme: 'bootstrap' });
            $("#ComboRoom").select2({ theme: 'bootstrap' });
            $("#ComboAssignedTo").select2({ theme: 'bootstrap' });
        })
    </script>
</asp:Content>
