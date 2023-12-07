<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="ContactPage.aspx.cs" Inherits="B2B.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <link rel="stylesheet" href="Content/CSS/admin-custom.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <nav class="navbar navbar-expand-lg bg-light fixed-top shadow-lg">
        <div class="container">
            <!-- <a class="navbar-brand" href="javascript:;">BnB <span class="tooplate-green">Host</span></a> -->
            <img src="https://gestionale.bnbhosts.it/Content/Images/Logo_BNB_HOSTS.png" width="110" height="50" >
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
        </div>
    </nav>
    <main style="font-size: 20px; padding-top: 30px" class="bg">
        <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-8 pb-lg-5">
            <section class=" mb-5">
                <header class="text-center">
                    <h2 class="hero-title text-black-50 mt-3 mb-4" runat="server" id="pageTitle">CONTATTACI</h2>
                </header>
                <div class="container">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <div class="row m-xs">
                        <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="col-sm-12 asp-validation-message" ClientIDMode="Static" />
                        <asp:RequiredFieldValidator ID="ReqValName" runat="server" ErrorMessage="Inserire il Nome." ControlToValidate="TxtName" Display="None" Enabled="true"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="ReqValEmail" runat="server" ErrorMessage="Inserire indirizzo Email." ControlToValidate="TxtEmail" Display="None" Enabled="true"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="ReqValMessage" runat="server" ErrorMessage="Inserire il messaggio." ControlToValidate="TxtMessage" Display="None" Enabled="true"></asp:RequiredFieldValidator>
                    </div>

                    <div class="row">
                        <div class="col-lg-8 col-md-8 col-12">
                            <div class="input-group align-items-center">
                                <label>Nome</label>
                                <asp:TextBox ID="TxtName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="input-group align-items-center">
                                <label for="product-name">Email</label>
                                <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            </div>
                            <div class="input-group align-items-center">
                                <label for="product-name">Messaggio</label>
                                <asp:TextBox ID="TxtMessage" runat="server" ClientIDMode="Static" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-12">
                            <h3>Come contattarci</h3>
                            <div class="mb-3">
                                <p class="text-bold mb-0"><i class="bi-telephone me-1 text-warning"></i>Telefono</p>
                                (+39)-081222011
                                (+39)-382665221
                            </div>
                            <div class="mb-3">
                                <p class="text-bold mb-0"><i class="bi-geo-alt me-1 text-warning"></i>Sede Principale</p>
                                Piazzetta Rosario di Palazzo, 19 - 80132 Napoli (NA)
                            </div>
                            <div class="mb-3">
                                <p class="text-bold mb-0"><i class="bi-envelope me-1 text-warning"></i>E-mail</p>
                                info@bnbhosts.it
                            </div>
                        </div>
                        <div class="row text-right">
                            <asp:Button ID="BtnSubmit" runat="server" Text="Invio Messaggio" OnClick="BtnSubmit_Click" CssClass="btn btn-lg btn-warning col-2 ms-auto" />
                        </div>
                    </div>
                </div>
            </section>
        </form>
    </main>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
