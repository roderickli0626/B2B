<%@ Page Title="" Language="C#" MasterPageFile="~/StaffPage.master" AutoEventWireup="true" CodeBehind="StaffService.aspx.cs" Inherits="B2B.StaffService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StaffHeaderPlaceHolder" runat="server">
    <link rel="stylesheet" href="Content/CSS/datatables.css" />
    <link rel="stylesheet" href="Content/CSS/responsive.dataTables.min.css" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="StaffContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-10 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4">SERVIZI</h2>
            </header>
            <div class="container">
                <div>
                    <div class="row">
                        <div style="float: left; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12 mb-2">
                            <asp:LinkButton ID="BtnAdd" runat="server" CausesValidation="false" OnClick="BtnAdd_Click" CssClass="btn btn-warning btn-lg text-white w-100">
                            <i class="fa fa-plus mr-sm"></i> Agg.
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-2 col-md-2 col-0"></div>
                        <div style="float: right; position: relative; z-index: 1;" class="col-lg-4 col-md-4 col-12">
                            <div class="input-group align-items-center" style="height: 52px">
                                <label for="status">Categoria Servizio: </label>
                                <asp:DropDownList ID="ComboGrandService" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div style="float: right; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12">
                            <div class="input-group align-items-center">
                                <label for="product-name">Cerca: </label>
                                <asp:TextBox ID="TxtSearch" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <table class="table table-bordered table-striped text-center" id="service-table">
                        <thead>
                            <tr>
                                <th>Foto</th>
                                <th>Categoria Servizio</th>
                                <th>Servizio</th>
                                <th>Descrizione</th>
                                <th>Prezzo</th>
                                <th>Listino Prezzi</th>
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
    <script src="Scripts/select2.js"></script>
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script src="Scripts/dataTables.responsive.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var datatable = $('#service-table').dataTable({
                "serverSide": true,
                "ajax": 'DataService.asmx/FindServices',
                "dom": '<"table-responsive"t>pr',
                "autoWidth": false,
                "pageLength": 20,
                "processing": true,
                "ordering": false,
                "responsive": true,
                "columns": [{
                    "data": "Image",
                    "render": function (data, type, row, meta) {
                        return '<image src="' + ((data == "" || data == null) ? "Content/Images/service_default.jpg" : "Upload/Service/" + data) + '" style="height: 100px;width:100px;" />';
                    }
                }, {
                    "data": "GrandService",
                }, {
                    "data": "DescriptionShort",
                    "width": "10%"
                }, {
                    "data": "DescriptionLong",
                    "width": "30%",
                    "render": function (data, type, row, meta) {
                        return '<label class="text-black" title="' + data + '">' + data.substring(0, 50) + '</label>';
                    }
                }, {
                    "data": "Price",
                    "render": function (data, type, row, meta) {
                        return data + " €";
                    }
                }, {
                    "data": "HaveGroupPrice",
                    "render": function (data, type, row, meta) {
                        return data == true ? '<label class="text-success">SI</label>' : '<label class="text-danger">NO</label>';
                    }
                }, {
                    "data": null,
                    "render": function (data, type, row, meta) {
                        return '<a href="StaffServiceEdit.aspx?id=' + row.Id + '"><i class="fa fa-edit" style="font-size:25px"></i></a>';
                    }
                }], 

                "fnServerParams": function (aoData) {
                    aoData.searchVal = $('#TxtSearch').val();
                    aoData.grandServiceID = $('#ComboGrandService').val();
                }
            });

            $('#ComboGrandService').change(function () {
                datatable.fnDraw();
            });

            $('#TxtSearch').on('input', function () {
                datatable.fnDraw();
            });

            $("#ComboGrandService").select2({ theme: 'bootstrap' }); 
        });
    </script>
</asp:Content>
