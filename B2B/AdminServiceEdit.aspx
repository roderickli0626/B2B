<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.master" AutoEventWireup="true" CodeBehind="AdminServiceEdit.aspx.cs" Inherits="B2B.AdminServiceEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeaderPlaceHolder" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">SERVIZI (nuovo)</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" />
                    <asp:RequiredFieldValidator ID="ReqValTitle" runat="server" ErrorMessage="Inserire un titolo." ControlToValidate="TxtTitle" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValPrice" runat="server" ErrorMessage="Inserire il prezzo." ControlToValidate="TxtPrice" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator0" runat="server" ErrorMessage="Inserire una Categoria Servizio." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Salvataggio Fallito." Display="None"></asp:CustomValidator>
                </div>

                <div class="row">
                    <div class="col-lg-4 col-md-4">
                        <img src="Content/Images/service_default.jpg" id="serviceImage" runat="server" ClientIDMode="Static" alt="service-image" class="img-thumbnail" style="height:93%; width:100%;"/>
                        <asp:FileUpload runat="server" ID="ImageFile" ClientIDMode="Static" CssClass="hidden-input" />
                    </div>
                    <div class="col-lg-8 col-md-8">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-12">
                                <div class="input-group align-items-center" style="height: 52px">
                                    <label for="status">Categoria Servizio: </label>
                                    <asp:DropDownList ID="ComboGrandService" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-12">
                                <div class="input-group align-items-center">
                                    <label for="status">Titolo: </label>
                                    <asp:TextBox ID="TxtTitle" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        
                        <div class="input-group align-items-center">
                            <label for="status">Descrizione: </label>
                            <asp:TextBox ID="TxtDescription" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-12">
                                <div class="input-group align-items-center">
                                    <label for="product-name">Prezzo: </label>
                                    <asp:TextBox ID="TxtPrice" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-12">
                                <div class="input-group align-items-center">
                                    <label  class="col-md-6">Listino Prezzi: </label>
                                    <asp:RadioButton ID="RadioButton1" runat="server" CssClass="form-control radio-custom text-primary" Text=".SI." GroupName="StatusOption" Value="1" Checked="true" />
                                    <asp:RadioButton ID="RadioButton2" runat="server" CssClass="form-control radio-custom text-danger" Text=".NO." GroupName="StatusOption" Value="0" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row text-right">
                    <asp:Button ID="BtnSave" runat="server" Text="Salva" OnClick="BtnSave_Click" CssClass="btn btn-lg btn-warning col-md-2 col-5 ms-auto" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/AdminService.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AdminFooterPlaceHolder" runat="server">
    <script src="Scripts/select2.js"></script>
    <script type="text/javascript">
        $(function () {
            var textarea = $('#TxtDescription');
            var halfHeight = textarea.height() * 0.9;
            // Set the padding-top
            textarea.css('padding-top', halfHeight + 'px');

            $("#ImageFile").change(function () {
                readURL(this);
            });

            $("#serviceImage").click(function () {
                $("#ImageFile").click();
            });

            function readURL(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#serviceImage')
                            .attr('src', e.target.result);
                    };

                    reader.readAsDataURL(input.files[0]);
                }
            };

            $("#ComboGrandService").select2({ theme: 'bootstrap' });
        })
        
    </script>
</asp:Content>
