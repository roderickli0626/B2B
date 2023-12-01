<%@ Page Title="" Language="C#" MasterPageFile="~/StaffPage.master" AutoEventWireup="true" CodeBehind="StaffRoom.aspx.cs" Inherits="B2B.StaffRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffHeaderPlaceHolder" runat="server">
    <link rel="stylesheet" href="Content/CSS/datatables.css" />
    <link rel="stylesheet" href="Content/CSS/responsive.dataTables.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StaffContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-10 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4">ROOMS</h2>
            </header>
            <div class="container">
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Stato: </label>
                            <asp:DropDownList ID="ComboStatus" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                    </div>

                    <%--<div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center">
                            <label for="status">Ascensore: </label>
                            <asp:DropDownList ID="ComboLift" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                    </div>--%>

                    <div class="col-lg-6 col-md-6 col-12">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Tipo: </label>
                            <asp:DropDownList ID="ComboType" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                    </div>

                </div>

                <div>
                    <div class="row">
                        <div style="float: left; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12 mb-2">
                            <asp:LinkButton ID="BtnAdd" runat="server" CausesValidation="false" OnClick="BtnAdd_Click" CssClass="btn btn-warning btn-lg text-white w-100">
                            <i class="fa fa-plus mr-sm"></i> Agg.
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-6 col-md-6 col-6"></div>
                        <div style="float: right; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12">
                            <div class="input-group align-items-center">
                                <label for="product-name">Cerca: </label>
                                <asp:TextBox ID="TxtSearch" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <table class="table table-bordered table-striped text-center" id="room-table">
                        <thead>
                            <tr>
                                <th>Room ID</th>
                                <th>Cliente</th>
                                <th>Tipo</th>
                                <th>Indirizzo</th>
                                <th>Scala</th>
                                <th>Piano</th>
                                <th>Ascensore</th>
                                <th>Stato</th>
                                <th>Listino</th>
                                <th>Azione</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="StaffFooterPlaceHolder" runat="server">
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script src="Scripts/dataTables.responsive.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var datatable = $('#room-table').dataTable({
                "serverSide": true,
                "ajax": 'DataService.asmx/FindRooms',
                "dom": '<"table-responsive"t>pr',
                "autoWidth": false,
                "pageLength": 20,
                "processing": true,
                "ordering": false,
                "responsive": true,
                "columns": [{
                    "data": "Id",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                }, {
                    "data": "Owner",
                }, {
                    "data": "Type",
                }, {
                    "data": "Address",
                }, {
                    "data": "StairCases"
                }, {
                    "data": "Floor"
                }, {
                    "data": "Lift",
                    "render": function (data, type, row, meta) {
                        return data ? "<i class='fa fa-circle text-success'></i>" : "<i class='fa fa-stop text-danger'></i>";
                    }
                }, {
                    "data": "Status",
                    "render": function (data, type, row, meta) {
                        switch (data) {
                            case 1: return "<p class='text-primary mb-0'>Inserito</p>"; break;
                            case 2: return "<p class='text-success mb-0'>Verificato</p>"; break;
                            case 3: return "<p class='text-danger mb-0'>Rifiutato</p>"; break;
                        }
                    }
                }, {
                    "data": "PriceListGroup",
                }, {
                    "data": null,
                    "render": function (data, type, row, meta) {
                        return '<a href="StaffRoomEdit.aspx?id=' + row.Id + '"><i class="fa fa-edit" style="font-size:25px"></i></a>';
                    }
                }],

                "fnServerParams": function (aoData) {
                    //aoData.lift = $('#ComboLift').val();
                    aoData.lift = 0;
                    aoData.type = $('#ComboType').val();
                    aoData.status = $('#ComboStatus').val();
                    aoData.searchVal = $('#TxtSearch').val();
                }
            });

            $('#ComboStatus, #ComboLift, #ComboType').change(function () {
                datatable.fnDraw();
            });

            $('#TxtSearch').on('input', function () {
                datatable.fnDraw();
            });

            var onSuccess = function (data) {
                if (data.success) {

                    datatable.fnDraw();

                } else {
                    alert("Failed!");
                }
            };

            $("#ComboStatus").select2({ theme: 'bootstrap' }); 
            $("#ComboType").select2({ theme: 'bootstrap' }); 
        });
    </script>
</asp:Content>
