<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="B2B.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <nav class="navbar navbar-expand-lg bg-light fixed-top shadow-lg">
        <div class="container">
          <!--   <a class="navbar-brand" href="javascript:;">BnB <span class="tooplate-green">Host</span></a>-->

            <img src="https://gestionale.bnbhosts.it/Content/Images/Logo_BNB_HOSTS.png" width="110" height="50" >

            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
        </div>
    </nav>

    <main>

        <section class="hero-section hero-slide d-flex justify-content-center align-items-center" id="section_1">
            <div class="container">
                <div class="row">

                    <div class="col-lg-6 col-md-8 text-center mx-auto">
                        <div class="hero-section-text">
                            <small class="section-small-title">Welcome to BnB Host <i class="hero-icon bi-house"></i></small>

                            <h1 class="hero-title text-white mt-2 mb-4">Reset Password</h1>

                            <form class="custom-form hero-form" id="form1" runat="server" autocomplete="off">
                                <div class="row">
                                    <div class="input-group align-items-center">
                                        <label for="product-name">Password</label>
                                        <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control" MaxLength="40" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="input-group align-items-center">
                                        <label for="product-name">Repeat PW</label>
                                        <asp:TextBox ID="TxtRepeatPW" runat="server" TextMode="Password" CssClass="form-control" MaxLength="40" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:Button ID="ResetPW" runat="server" Text="Reset Password" OnClick="ResetPassword_Click" CssClass="btn btn-success btn-lg" />
                                </div>

                                <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="mt-lg-3 mb-lg-2 text-left text-white" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="ReqValPassword" runat="server" ErrorMessage="Please enter Password." CssClass="text-white" ControlToValidate="TxtPassword" Display="None"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Le password non coincidonoed" Display="None"></asp:CustomValidator>
                            </form>
                        </div>
                    </div>

                </div>
            </div>
        </section>

        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320">
            <path fill="#36363e" fill-opacity="1" d="M0,96L40,117.3C80,139,160,181,240,186.7C320,192,400,160,480,149.3C560,139,640,149,720,176C800,203,880,245,960,250.7C1040,256,1120,224,1200,229.3C1280,235,1360,277,1400,298.7L1440,320L1440,320L1400,320C1360,320,1280,320,1200,320C1120,320,1040,320,960,320C880,320,800,320,720,320C640,320,560,320,480,320C400,320,320,320,240,320C160,320,80,320,40,320L0,320Z"></path>
        </svg>
    </main>

    <footer class="site-footer section-padding">
        <div class="container">
            <div class="row">
                <div class="col-lg-5 col-md-5 col-12 mb-3">
                    <!--<h3><a href="index.html" class="custom-link mb-1">BnB Host</a></h3> -->
                    <a href="login.aspx"> <img src="https://gestionale.bnbhosts.it/Content/Images/Logo_BNB_HOSTS_invertito.png" width="110" height="50" > </a>

                    <p class="text-white">Da oggi una nuova realtà è presente a Napoli nei servizi da offrire a BnB e strutture ricettive</p>

                    <p class="text-white"><a href="https://www.softforbet.it" class="text-white" target="_parent">Web Design: SFB</a></p>
                </div>

                <div class="col-lg-3 col-md-3 col-12 ms-lg-auto mb-3">
                    <h3 class="text-white mb-3">Store</h3>

                    <p class="text-white mt-2">
                        <i class="bi-geo-alt"></i>
                        Piazzetta Rosario di Palazzo, 19 - 80132 Napoli
                            
                    </p>
                </div>

                <div class="col-lg-4 col-md-4 col-12 mb-3">
                    <h3 class="text-white mb-3">Contact Info</h3>

                    <p class="text-white mb-1">
                        <i class="bi-telephone me-1"></i>

                        <a href="tel: 090-080-0760" class="text-white">(+39)-08119240053
                        </a>
                        <a href="tel: 090-080-0760" class="text-white">(+39)-3884978026
                        </a>
                    </p>

                    <p class="text-white mb-0">
                        <i class="bi-envelope me-1"></i>

                        <a href="mailto:info@bnbhosts.it" class="text-white">info@bnbhosts.it
                        </a>
                    </p>
                </div>
            </div>
            <div class="row mt-5 pt-lg-5">
                <div class="col-lg-5 col-md-5 col-12 mb-3">
                    <h5 class="text-white mb-3">BBH Management s.r.l.</h5>
                    <p class="text-white">Reception</p>
                    <p class="text-white">Piazzetta Rosario di Palazzo, 19 - 80132 Napoli (NA)</p>
                    <p class="text-white">Partita Iva: 10380201219</p>
                </div>

                <div class="col-lg-3 col-md-3 col-12 ms-lg-auto mb-3">
                    <h5 class="text-white mb-3">Informazioni</h5>

                    <p class="text-white"><a href="ContactPage.aspx" class="text-white" target="_blank">Contacts</a></p>
                    <p class="text-white"><a href="Condition.aspx" class="text-white" target="_blank">Services & Conditions</a></p>
                    <p class="text-white"><a href="Privacy.aspx" class="text-white" target="_blank">Privacy Policy</a></p>
                </div>

                <div class="col-lg-4 col-md-4 col-12 mb-3">
                    <h5 class="text-white mb-3">Orario</h5>

                    <p class="text-white">Lunedi - Sabato: 09:00/20:00</p>
                    <p class="text-white"></p>
                    
                </div>

            </div>
        </div>
    </footer>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
