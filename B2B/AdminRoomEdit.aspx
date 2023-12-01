<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.master" AutoEventWireup="true" CodeBehind="AdminRoomEdit.aspx.cs" Inherits="B2B.AdminRoomEdit" %>
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
            transform: scale(1.5);
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
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">ROOM</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" />
                    <asp:RequiredFieldValidator ID="ReqValAddress" runat="server" ErrorMessage="Inserire l' indirizzo." ControlToValidate="TxtAddress" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValStairCases" runat="server" ErrorMessage="Inserire la scala." ControlToValidate="TxtStairCases" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValFloor" runat="server" ErrorMessage="Inserire il Piano." ControlToValidate="TxtFloor" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator1" runat="server" ErrorMessage="Salvataggio Fallito." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator2" runat="server" ErrorMessage="Selezionare gli accessori." Display="None"></asp:CustomValidator>
                </div>

                <div class="row">
                    <div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Cliente: </label>
                            <asp:DropDownList ID="ComboOwner" runat="server" CssClass="form-control mr-md" CausesValidation="false" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-3 col-12">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Tipo: </label>
                            <asp:DropDownList ID="ComboType" runat="server" CssClass="form-control mr-md" CausesValidation="false" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Ascensore: </label>
                            <asp:DropDownList ID="ComboLift" runat="server" CssClass="form-control mr-md" CausesValidation="false" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label for="status">Indirizzo: </label>
                            <asp:TextBox ID="TxtAddress" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label  class="col-md-3">Stato: </label>
                            <asp:RadioButton ID="RadioButton1" runat="server" CssClass="form-control radio-custom text-primary" Text=". Inserito ." GroupName="StatusOption" Value="1" Checked="true" />
                            <asp:RadioButton ID="RadioButton2" runat="server" CssClass="form-control radio-custom text-success" Text=". Verificato ." GroupName="StatusOption" Value="2" />
                            <asp:RadioButton ID="RadioButton3" runat="server" CssClass="form-control radio-custom text-danger" Text=". Rifiutato ." GroupName="StatusOption" Value="3" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 col-md-4">
                        <div class="input-group align-items-center">
                            <label for="status">Scala: </label>
                            <asp:TextBox ID="TxtStairCases" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="input-group align-items-center">
                            <label for="status">Piano: </label>
                            <asp:TextBox ID="TxtFloor" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Listino: </label>
                            <asp:DropDownList ID="ComboPriceGroup" runat="server" CssClass="form-control mr-md" CausesValidation="false" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
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
                <div>
                    <div class="row" runat="server">
                        <div class="col-lg-6 col-md-6">
                            <div class="input-group align-items-center" style="height: 52px">
                                <label for="status">Accessori: </label>
                                <asp:DropDownList ID="ComboAccessory" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3">
                            <div class="input-group align-items-center">
                                <label for="product-name">Quantità: </label>
                                <asp:TextBox ID="TxtQuantity" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" TextMode="Number" min="1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 mb-2" style="text-align: right">
                            <asp:LinkButton ID="BtnAddAccessory" runat="server" CausesValidation="false" OnClick="BtnAddAccessory_Click" ClientIDMode="Static" CssClass="btn btn-danger btn-lg text-white w-100">
                            <i class="fa fa-plus mr-sm"></i> Agg. Accessori
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="row m-xs">
                                <asp:ValidationSummary ID="ValSummary2" runat="server" ValidationGroup="ValServiceQuantity" CssClass="col-sm-12 asp-validation-message" />
                                <asp:CustomValidator ID="ServerValidator3" runat="server" ErrorMessage="Please select accessory quantity." ValidationGroup="ValServiceQuantity" Display="None"></asp:CustomValidator>
                            </div>
                            <asp:HiddenField ID="HfAccessoryAlloc" runat="server" ClientIDMode="Static" />
                            <asp:Repeater ID="AccessoryRepeater" runat="server" OnItemCommand="AccessoryRepeater_ItemCommand">
                                <HeaderTemplate>
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped text-center">
                                            <thead>
                                                <tr>
                                                    <th>Accessori</th>
                                                    <th>Descrizione</th>
                                                    <th>Quantità</th>
                                                    <th>Azione</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                        <tr>
                                            <td class="text-center">
                                                <asp:ImageButton runat="server" ID="AccessoryImage" ImageUrl='<%# (Eval("Image") == "" || Eval("Image") == null) ? "Content/Images/accessory_default.jpg" : "~/Upload/Accessory/" + Eval("Image") %>' 
                                                    CssClass="accessoryImage" />
                                            </td>
                                            <td><%# Eval("Description")%></td>
                                            <td><%# Eval("Quantity")%></td>
                                            <td>
                                                <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" OnClientClick="return confirm('Click OK per cancellare.');"
                                                    CommandName="Delete" CommandArgument='<%#Eval("AccessoryId") %>'>
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
                            <asp:AsyncPostBackTrigger ControlID="BtnAddAccessory" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="row text-right">
                    <asp:Button ID="BtnSave" runat="server" Text="Salva" OnClick="BtnSave_Click" CssClass="btn btn-lg btn-warning col-md-2 col-5 ms-auto" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/AdminRoom.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
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
        function MyFun() {
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

            $("#ComboOwner").select2({ theme: 'bootstrap' });
            $("#ComboType").select2({ theme: 'bootstrap' }); 
            $("#ComboLift").select2({ theme: 'bootstrap' }); 
            $("#ComboPriceGroup").select2({ theme: 'bootstrap' }); 
            $("#ComboAccessory").select2({ theme: 'bootstrap' }); 
        });
    </script>
</asp:Content>
