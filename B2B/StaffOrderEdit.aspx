﻿<%@ Page Title="" Language="C#" MasterPageFile="~/StaffPage.master" AutoEventWireup="true" CodeBehind="StaffOrderEdit.aspx.cs" Inherits="B2B.StaffOrderEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffHeaderPlaceHolder" runat="server">
    <link rel="stylesheet" href="Content/CSS/datatables.css" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="StaffContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">ORDINI </h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" />
                    <asp:RequiredFieldValidator ID="ReqValStartDate" runat="server" ErrorMessage="Please enter Start Date." ControlToValidate="TxtDateFrom" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValEndDate" runat="server" ErrorMessage="Please enter End Date." ControlToValidate="TxtDateTo" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValGuests" runat="server" ErrorMessage="Please enter Number of Guests." ControlToValidate="TxtNumberOfGuests" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator1" runat="server" ErrorMessage="Save Failed." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator0" runat="server" ErrorMessage="Please Select Host." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator4" runat="server" ErrorMessage="Please Select Room." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator2" runat="server" ErrorMessage="Please Select Services." Display="None"></asp:CustomValidator>
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
                    <div class="col-lg-4 col-md-3 col-12">
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
                                <asp:CustomValidator ID="ServerValidator3" runat="server" ErrorMessage="Please select service quantity." ValidationGroup="ValServiceQuantity" Display="None"></asp:CustomValidator>
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
                    <div class="col-lg-6 col-md-6" runat="server" id="assignDiv">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Assegnato a: </label>
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
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/StaffHome.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="StaffFooterPlaceHolder" runat="server">
    <script src="Scripts/select2.js"></script>
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script type="text/javascript">
        function MyFun() {
            $("#ComboRoom").select2({ theme: 'bootstrap' });
            $("#ComboService").select2({ theme: 'bootstrap' });
        }

        $(function () {
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
