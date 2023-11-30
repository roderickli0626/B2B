<%@ Page Title="" Language="C#" MasterPageFile="~/StaffPage.master" AutoEventWireup="true" CodeBehind="StaffEmployeeEdit.aspx.cs" Inherits="B2B.StaffEmployeeEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StaffContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">COLLABORATORE (nuovo)</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" ClientIDMode="Static" />
                    <asp:RequiredFieldValidator ID="ReqValName" runat="server" ErrorMessage="Please enter name." ControlToValidate="TxtName" Display="None" Enabled="true"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValMobile" runat="server" ErrorMessage="Please enter MobilePhone number." ControlToValidate="TxtMobile" Display="None" Enabled="true"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Save Failed." Display="None"></asp:CustomValidator>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center">
                            <label>Nome</label>
                            <asp:TextBox ID="TxtName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center">
                            <label>Cognome</label>
                            <asp:TextBox ID="TxtSurname" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Cellulare</label>
                            <asp:TextBox ID="TxtMobile" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="PasswordDiv">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Password</label>
                            <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Note</label>
                            <asp:TextBox ID="TxtNote" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row text-right">
                    <asp:Button ID="BtnSave" runat="server" Text="Salva" OnClick="BtnSave_Click" CssClass="btn btn-lg btn-warning col-md-2 col-5 ms-auto" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/StaffEmployee.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
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
