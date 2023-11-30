<%@ Page Title="" Language="C#" MasterPageFile="~/StaffPage.master" AutoEventWireup="true" CodeBehind="StaffAccommodationEdit.aspx.cs" Inherits="B2B.StaffAccommodationEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StaffContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-8 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">SISTEMAZIONE (nuova)</h2>
            </header>
            <div class="container">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row m-xs">
                    <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" />
                    <asp:RequiredFieldValidator ID="ReqValDescription" runat="server" ErrorMessage="Si prega inserire una descrizione." ControlToValidate="TxtDescription" Display="None" Enabled="True"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Save Failed." Display="None"></asp:CustomValidator>
                </div>

                <div class="row">
                    <div class="col-lg-4 col-md-4">
                        <img src="Content/Images/accommodation_default.jpg" id="serviceImage" runat="server" ClientIDMode="Static" alt="service-image" class="img-thumbnail" style="height:90%; width:100%;"/>
                        <asp:FileUpload runat="server" ID="ImageFile" ClientIDMode="Static" CssClass="hidden-input" />
                    </div>
                    <div class="col-lg-8 col-md-8">
                        <div class="input-group align-items-center">
                            <label for="status">Descrizione: </label>
                            <asp:TextBox ID="TxtDescription" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        </div>
                        <div class="row text-right">
                            <asp:Button ID="BtnSave" runat="server" Text="Salva" OnClick="BtnSave_Click" CssClass="btn btn-lg btn-warning col-md-2 col-5 ms-auto" />
                            <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CausesValidation="False" PostBackUrl="~/StaffAccommodation.aspx" CssClass="btn btn-lg btn-secondary col-md-2 col-5 ms-3 me-3" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="StaffFooterPlaceHolder" runat="server">
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
            }
        })
        
    </script>
</asp:Content>
