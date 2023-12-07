<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="B2B.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <nav class="navbar navbar-expand-lg bg-light fixed-top shadow-lg">
        <div class="container">
           <!-- <a class="navbar-brand" href="javascript:;">BnB <span class="tooplate-green">Host</span></a>-->
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

                    <div class="col-lg-6 col-md-8 text-center mx-auto overflow-auto">
                        <div class="hero-section-text">
                          <!--  <small class="section-small-title">Welcome to BnB Host<i class="hero-icon bi-house"></i></small>-->
                            <img src="https://gestionale.bnbhosts.it/Content/Images/Logo_BNB_HOSTS.png" style="max-width:100%;" height="200" >
                         <h1 class="hero-title text-white mt-2 mb-4">ACCESSO</h1>

                            <form class="custom-form hero-form" id="form1" runat="server" autocomplete="off">
                                <div class="row">
                                    <div class="input-group align-items-center">
                                        <label for="product-name">Email</label>
                                        <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" TextMode="Email" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="input-group align-items-center">
                                        <label for="product-name">Password</label>
                                        <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control" MaxLength="40" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:Button ID="LogInBtn" runat="server" Text="Entra" OnClick="LogInBtn_Click" CssClass="btn btn-warning btn-lg" />
                                </div>
                                <div class="row text-center pt-3">
                                    <a class="col-md-4" href="ForgotPassword.aspx">Password dimenticata ?</a>
                                    <a href="Register.aspx" class="col-md-4">Crea un account</a>
                                    <a class="col-md-4" href="EmployeeLogin.aspx">Accesso privato</a>
                                </div>

                                <asp:ValidationSummary ID="ValSummary" runat="server" CssClass="mt-lg mb-lg text-left bg-warning" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="ReqValEmail" runat="server" ErrorMessage="Inserire l'email." CssClass="text-bg-danger" ControlToValidate="TxtEmail" Display="None"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="ReqValPassword" runat="server" ErrorMessage="Inserire la password." CssClass="text-black" ControlToValidate="TxtPassword" Display="None"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="ServerValidator" runat="server" ErrorMessage="Email o Password errata." Display="None"></asp:CustomValidator>

                            </form>


                      <!--      <div class="hero-btn d-flex justify-content-center align-items-center">
                                <a class="bi-arrow-down hero-btn-link smoothscroll" href="#section_2"></a>
                            </div>    -->
                        </div>
                    </div>

                </div>
            </div>
        </section>

 <!--       <section class="about-section section-padding" id="section_2">
            <div class="container">
                <div class="row align-items-center">

                    <div class="col-lg-5 col-12">
                        <small class="section-small-title">Our Story</small>

                        <h2 class="mt-2 mb-4"><span class="text-muted">Introduzione</span> BnB Host</h2>

                        <h4 class="text-muted mb-3">Da oggi una nuova realtà è presente a Napoli nei servizi da offrire a BnB e strutture ricettive</h4>

                        <p>Offriamo servizi su misura, progettati per adattarsi alle esigenze specifiche del vostro hotel, resort, bed and breakfast o altre strutture ricettive. </p>
                    </div>

                    <div class="col-lg-3 col-md-5 col-5 mx-lg-auto">
                        <img src="Content/Images/childrens-bed-nursery-cot-velvet-childrens-room.jpg" class="about-image about-image-small img-fluid" alt="">
                    </div>

                    <div class="col-lg-4 col-md-7 col-7">
                        <img src="Content/Images/living-room-interior-wall-mockup-warm-tones-with-leather-sofa-which-is-kitchen-3d-rendering.jpg" class="about-image img-fluid" alt="">
                    </div>

                </div>
            </div>
        </section>   -->

 <!--       <section class="shop-section section-padding" id="section_3">
            <div class="container">
                <div class="row">

                    <div class="col-lg-12 col-12">
                        <small class="section-small-title">Our Services</small>

                        <h2 class="mt-2 mb-4"><span class="tooplate-red">Super</span> Services</h2>
                    </div>

                    <div class="col-lg-6 col-12">
                        <div class="shop-thumb">
                            <div class="shop-image-wrap">
                                <a href="shop-detail.html">
                                    <img src="Content/Images/minimal-bathroom-interior-design-with-wooden-furniture.jpg" class="shop-image img-fluid" alt="">
                                </a>

                                <div class="shop-icons-wrap">
                                    <div class="shop-icons d-flex flex-column align-items-center">
                                        <a href="#" class="shop-icon bi-heart"></a>

                                        <a href="#" class="shop-icon bi-bookmark"></a>
                                    </div>

                                    <p class="shop-pricing mb-0 mt-3">
                                        <span class="badge custom-badge">da 12€</span>
                                    </p>
                                </div>

                                <div class="shop-btn-wrap">
                               <a href="#" class="shop-btn custom-btn btn d-flex align-items-center align-items-center">Learn more</a>
                                </div>
                            </div>

                            <div class="shop-body">
                                <h4>Check in / Check out</h4>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 col-12">
                        <div class="shop-thumb">
                            <div class="shop-image-wrap">
                                <a href="shop-detail.html">
                                    <img src="Content/Images/mock-up-poster-modern-dining-room-interior-design-with-white-empty-wall.jpg" class="shop-image img-fluid" alt="">
                                </a>

                                <div class="shop-icons-wrap">
                                    <div class="shop-icons d-flex flex-column align-items-center">
                                        <a href="#" class="shop-icon bi-heart"></a>

                                        <a href="#" class="shop-icon bi-bookmark"></a>
                                    </div>

                                    <p class="shop-pricing mb-0 mt-3">
                                        <span class="badge custom-badge">da 16€</span>
                                    </p>
                                </div>

                                <div class="shop-btn-wrap">
                                    <a href="#" class="shop-btn custom-btn btn d-flex align-items-center align-items-center">Learn more</a>
                                </div>
                            </div>

                            <div class="shop-body">
                                <h4>Riassetto Camera</h4>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-12">
                        <div class="shop-thumb">
                            <div class="shop-image-wrap">
                                <a href="shop-detail.html">
                                    <img src="Content/Images/green-sofa-white-living-room-with-blank-table-mockup.jpg" class="shop-image img-fluid" alt="">
                                </a>

                                <div class="shop-icons-wrap">
                                    <div class="shop-icons d-flex flex-column align-items-center">
                                        <a href="#" class="shop-icon bi-heart"></a>

                                        <a href="#" class="shop-icon bi-bookmark"></a>
                                    </div>

                                    <p class="shop-pricing mb-0 mt-3">
                                        <span class="badge custom-badge">da 13€</span>
                                    </p>
                                </div>

                                <div class="shop-btn-wrap">
                                    <a href="#" class="shop-btn custom-btn btn d-flex align-items-center align-items-center">Learn more</a>
                                </div>
                            </div>

                            <div class="shop-body">
                                <h4>Fornitura Biancheria</h4>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-12">
                        <div class="shop-thumb">
                            <div class="shop-image-wrap">
                                <a href="shop-detail.html">
                                    <img src="Content/Images/concept-home-cooking-with-female-chef.jpg" class="shop-image img-fluid" alt="">
                                </a>

                                <div class="shop-icons-wrap">
                                    <div class="shop-icons d-flex flex-column align-items-center">
                                        <a href="#" class="shop-icon bi-heart"></a>

                                        <a href="#" class="shop-icon bi-bookmark"></a>
                                    </div>

                                    <p class="shop-pricing mb-0 mt-3">
                                        <span class="badge custom-badge">da 8€</span>
                                    </p>
                                </div>

                                <div class="shop-btn-wrap">
                                    <a href="#" class="shop-btn custom-btn btn d-flex align-items-center align-items-center">Learn more</a>
                                </div>
                            </div>

                            <div class="shop-body">
                                <h4>Kit Dog</h4>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-12">
                        <div class="shop-thumb">
                            <div class="shop-image-wrap">
                                <a href="shop-detail.html">
                                    <img src="Content/Images/childrens-bed-nursery-cot-velvet-childrens-room.jpg" class="shop-image img-fluid" alt="">
                                </a>

                                <div class="shop-icons-wrap">
                                    <div class="shop-icons d-flex flex-column align-items-center">
                                        <a href="#" class="shop-icon bi-heart"></a>

                                        <a href="#" class="shop-icon bi-bookmark"></a>
                                    </div>

                                    <p class="shop-pricing mb-0 mt-3">
                                        <span class="badge custom-badge">da 5€</span>
                                    </p>
                                </div>

                                <div class="shop-btn-wrap">
                                    <a href="#" class="shop-btn custom-btn btn d-flex align-items-center align-items-center">Learn more</a>
                                </div>
                            </div>

                            <div class="shop-body">
                                <h4>Kit Cortesia per Uomo</h4>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>  -->

        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320">
            <path fill="#36363e" fill-opacity="1" d="M0,96L40,117.3C80,139,160,181,240,186.7C320,192,400,160,480,149.3C560,139,640,149,720,176C800,203,880,245,960,250.7C1040,256,1120,224,1200,229.3C1280,235,1360,277,1400,298.7L1440,320L1440,320L1400,320C1360,320,1280,320,1200,320C1120,320,1040,320,960,320C880,320,800,320,720,320C640,320,560,320,480,320C400,320,320,320,240,320C160,320,80,320,40,320L0,320Z"></path></svg>
    </main>

    <footer class="site-footer section-padding">
        <div class="container">
            <div class="row">
                <div class="col-lg-5 col-md-5 col-12 mb-3">
                  <!--  <h3><a href="index.html" class="custom-link mb-1">BnB Host</a></h3>-->
                    <a href="login.aspx"> <img src="https://gestionale.bnbhosts.it/Content/Images/Logo_BNB_HOSTS_invertito.png" width="110" height="50" > </a>

                    <p class="text-white">Da oggi una nuova realtà è presente a Napoli</p>

                    <p class="text-white"><a href="https://www.softforbet.it" class="text-white" target="_parent">Web Design: SFB</a></p>
                </div>

                <div class="col-lg-3 col-md-3 col-12 ms-lg-auto mb-3">
                    <h3 class="text-white mb-3">Store</h3>

                    <p class="text-white mt-2">
                        <i class="bi-geo-alt"></i>
                        Piazzetta Rosario di Palazzo, 19
                            80132 - Napoli (NA)
                    </p>
                </div>

                <div class="col-lg-4 col-md-4 col-12 mb-3">
                    <h3 class="text-white mb-3">Contact Info</h3>

                    <p class="text-white mb-1">
                        <i class="bi-telephone me-1"></i>

                        <a href="tel: 090-080-0760" class="text-white">(+39)-0814521905
                        </a>
                        <a href="tel: 090-080-0760" class="text-white">(+39)-3386961805
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
                    <p class="text-white">Reception:</p>
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
                    <h5 class="text-white mb-3">Orari</h5>

                    <p class="text-white">Lunedi - Sabato: 09:00/20:00</p>
                    <p class="text-white"></p>
                    
                </div>

            </div>
        </div>
    </footer>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
