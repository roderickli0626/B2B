<%@ Page Title="" Language="C#" MasterPageFile="~/HostPage.master" AutoEventWireup="true" CodeBehind="HostRoomEdit.aspx.cs" Inherits="B2B.HostRoomEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HostHeaderPlaceHolder" runat="server">
    <link rel="stylesheet" href="Content/CSS/datatables.css" />
    <link rel="stylesheet" href="Content/CSS/responsive.dataTables.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HostContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5 bg-black bg-opacity-50">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-white mt-3 mb-4" runat="server" id="pageTitle">ROOM</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" />
                    <asp:RequiredFieldValidator ID="ReqValAddress" runat="server" ErrorMessage="Inserire l'indirizzo." ControlToValidate="TxtAddress" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValStairCases" runat="server" ErrorMessage="Inserire la Scala." ControlToValidate="TxtStairCases" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValFloor" runat="server" ErrorMessage="Inserire il Piano." ControlToValidate="TxtFloor" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator1" runat="server" ErrorMessage="Salvataggio Fallito." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator2" runat="server" ErrorMessage="Selezionare gli Accessori." Display="None"></asp:CustomValidator>
                </div>

                <div class="row mb-4">
                    <div class="col-lg-6 col-md-6 mt-auto mb-auto">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Tipo: </label>
                            <asp:DropDownList ID="ComboType" runat="server" ClientIDMode="Static" AutoPostBack="true" CssClass="form-control mr-md" CausesValidation="false" OnSelectedIndexChanged="ComboType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 text-center">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                            <ContentTemplate>
                                <img src="Content/Images/accommodation_default.jpg" runat="server" id="AccommodationImage" class="img-thumbnail" style="height: 200px; width: 250px;" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ComboType" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Ascensore: </label>
                            <asp:DropDownList ID="ComboLift" runat="server" CssClass="form-control mr-md" ClientIDMode="Static" CausesValidation="false">
                            </asp:DropDownList>
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
                        <div class="input-group align-items-center">
                            <label for="status">Note: </label>
                            <asp:TextBox ID="TxtNote" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="row" runat="server" id="accessoryDiv">
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
                                <asp:CustomValidator ID="ServerValidator3" runat="server" ErrorMessage="Inserisci accessorio e quantità." ValidationGroup="ValServiceQuantity" Display="None"></asp:CustomValidator>
                            </div>
                            <asp:HiddenField ID="HfAccessoryAlloc" runat="server" ClientIDMode="Static" />
                            <asp:Repeater ID="AccessoryRepeater" runat="server" OnItemCommand="AccessoryRepeater_ItemCommand">
                                <HeaderTemplate>
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped text-center bg-white">
                                            <thead>
                                                <tr>
                                                    <th>Accessorio</th>
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
                                                    CommandName="Delete" CommandArgument='<%# Eval("AccessoryId") %>'>
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
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/HostHome.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HostFooterPlaceHolder" runat="server">
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

            $("#ComboLift").select2({ theme: 'bootstrap' });
            $("#ComboAccessory").select2({ theme: 'bootstrap' });
            $("#ComboType").select2({ theme: 'bootstrap' }); 
        })
    </script>
</asp:Content>
