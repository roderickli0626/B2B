<%@ Page Title="" Language="C#" MasterPageFile="~/StaffPage.master" AutoEventWireup="true" CodeBehind="StaffPaymentEdit.aspx.cs" Inherits="B2B.StaffPaymentEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StaffContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">PAGAMENTO (nuovo)</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" />
                    <asp:RequiredFieldValidator ID="ReqValDateOfPay" runat="server" ErrorMessage="Please enter Date Of Pay." ControlToValidate="TxtDateOfPay" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValAmount" runat="server" ErrorMessage="Please enter Amount." ControlToValidate="TxtAmount" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ReqValPaypalID" runat="server" ErrorMessage="Please enter Paypal Transition ID." ControlToValidate="TxtPaypalTransitionID" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Save Failed." Display="None"></asp:CustomValidator>
                </div>

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Ordine: </label>
                            <asp:DropDownList ID="ComboOrder" runat="server" CssClass="form-control mr-md" CausesValidation="false" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="input-group align-items-center">
                            <label for="product-name">Data di Pag.: </label>
                            <asp:TextBox ID="TxtDateOfPay" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"
                                data-plugin-datepicker="" data-plugin-options='{ "format": "dd/mm/yyyy", "todayHighlight": "true" }'></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="input-group align-items-center">
                            <label for="status">Importo: </label>
                            <asp:TextBox ID="TxtAmount" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-12">
                        <div class="input-group align-items-center">
                            <label for="status">PaypalTransitionID: </label>
                            <asp:TextBox ID="TxtPaypalTransitionID" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
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
                <div class="row text-right">
                    <asp:Button ID="BtnSave" runat="server" Text="Salva" OnClick="BtnSave_Click" CssClass="btn btn-lg btn-warning col-md-2 col-5 ms-auto" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/StaffPayment.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="StaffFooterPlaceHolder" runat="server">
    <script>
        $(function () {
            $("#ComboOrder").select2({ theme: 'bootstrap' });
        })
    </script>
</asp:Content>
