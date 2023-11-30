<%@ Page Title="" Language="C#" MasterPageFile="~/StaffPage.master" AutoEventWireup="true" CodeBehind="StaffHostEdit.aspx.cs" Inherits="B2B.StaffHostEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StaffContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">Cliente (nuovo)</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" ClientIDMode="Static" />
                    <asp:RequiredFieldValidator ID="ReqValEmail" runat="server" ErrorMessage="Inserisci indirizzo Email." ControlToValidate="TxtEmail" Display="None" Enabled="true"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="PasswordValidator1" runat="server" ErrorMessage="Inserisci la password." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="PasswordValidator" runat="server" ErrorMessage="Le password non coincidono." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="EmailValidator" runat="server" ErrorMessage="Email is not correct." Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Email is already registered." Display="None"></asp:CustomValidator>
                </div>

                <div class="row">
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label>Nome</label>
                            <asp:TextBox ID="TxtName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label>Cognome</label>
                            <asp:TextBox ID="TxtSurname" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Email</label>
                            <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Cellulare</label>
                            <asp:TextBox ID="TxtMobile" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Telefono</label>
                            <asp:TextBox ID="TxtPhone" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="PasswordDiv">
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Password</label>
                            <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Ripeti PW</label>
                            <asp:TextBox ID="TxtRepeatPW" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Note</label>
                            <asp:TextBox ID="TxtNote" runat="server" ClientIDMode="Static" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row text-right">
                    <asp:Button ID="BtnSave" runat="server" Text="Salva" OnClick="BtnSave_Click" CssClass="btn btn-lg btn-warning col-md-2 col-5 ms-auto" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/StaffHost.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="StaffFooterPlaceHolder" runat="server">
    <script>
      $(document).ready(function() {
        var textarea = $('#TxtNote');
        var halfHeight = textarea.height() * 0.9;
        // Set the padding-top
        textarea.css('padding-top', halfHeight + 'px');
      });
    </script>
</asp:Content>
