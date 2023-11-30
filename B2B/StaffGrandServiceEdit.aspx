<%@ Page Title="" Language="C#" MasterPageFile="~/StaffPage.master" AutoEventWireup="true" CodeBehind="StaffGrandServiceEdit.aspx.cs" Inherits="B2B.StaffGrandServiceEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StaffContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">CATEGORIA SERVIZI (nuovo)</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" />
                    <asp:RequiredFieldValidator ID="ReqValDescription" runat="server" ErrorMessage="Si prega inserire la descrizione." ControlToValidate="TxtDescription" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValDescription1" runat="server" ErrorMessage="Si prega inserire la Titolo." ControlToValidate="TxtTitle" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Salvataggio Fallito." Display="None"></asp:CustomValidator>
                </div>

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center">
                            <label for="status">Titolo: </label>
                            <asp:TextBox ID="TxtTitle" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="input-group align-items-center">
                            <label for="status">Descrizione: </label>
                            <asp:TextBox ID="TxtDescription" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="row text-right">
                            <asp:Button ID="BtnSave" runat="server" Text="Salva" OnClick="BtnSave_Click" CssClass="btn btn-lg btn-warning col-md-2 col-5 ms-auto" />
                            <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/StaffGrandService.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="StaffFooterPlaceHolder" runat="server">
</asp:Content>
