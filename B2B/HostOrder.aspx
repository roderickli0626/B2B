<%@ Page Title="" Language="C#" MasterPageFile="~/HostPage.master" AutoEventWireup="true" CodeBehind="HostOrder.aspx.cs" Inherits="B2B.HostOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HostHeaderPlaceHolder" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="HostContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-10 pb-lg-5 bg-black bg-opacity-50">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-white mt-3 mb-4">I MIEI ORDINI</h2>
            </header>
            <div class="container">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center" style="height: 52px">
                            <label for="status">Stato: </label>
                            <asp:DropDownList ID="ComboStatus" runat="server" CssClass="form-control mr-md" ClientIDMode="Static"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Dal: </label>
                            <asp:TextBox ID="TxtDateFrom" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"
                            data-plugin-datepicker="" data-plugin-options='{ "format": "dd/mm/yyyy", "todayHighlight": "true" }'></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-12">
                        <div class="input-group align-items-center">
                            <label for="product-name">Al: </label>
                            <asp:TextBox ID="TxtDateTo" CssClass="form-control" runat="server" ClientIDMode="Static"
                                data-plugin-datepicker="" data-plugin-options='{ "format": "dd/mm/yyyy", "todayHighlight": "true" }'></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div>
                    <div class="row">
                        <div style="float: left; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12 mb-2">
                            <asp:LinkButton ID="BtnAdd" runat="server" CausesValidation="false" OnClick="BtnAdd_Click" CssClass="btn btn-danger btn-lg text-white w-100">
                            <i class="fa fa-plus mr-sm"></i> Agg. Ordini
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-6 col-md-4 col-0"></div>
                        <div style="float: right; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12">
                            <div class="input-group align-items-center">
                                <label for="product-name">Cerca: </label>
                                <asp:TextBox ID="TxtSearch" CssClass="form-control mr-sm" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <table class="table table-bordered table-striped text-center table-hover bg-white" id="order-table">
                        <thead>
                            <tr>
                                <th>Nr.</th>
                                <th>Dal</th>
                                <th>Al</th>
                                <th>Indirizzo</th>
                                <th>Ospiti</th>
                                <th>Totale</th>
                                <th>Stato</th>
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
<asp:Content ID="Content3" ContentPlaceHolderID="HostFooterPlaceHolder" runat="server">
    <script src="Scripts/select2.js"></script>
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script src="Scripts/dataTables.responsive.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var datatable = $('#order-table').dataTable({
                "serverSide": true,
                "ajax": 'DataService.asmx/FindHostOrders',
                "dom": '<"table-responsive"t>pr',
                "autoWidth": false,
                "pageLength": 20,
                "processing": true,
                "ordering": false,
                "responsive": true,
                "columns": [{
                    "data": "Id",
                    "render": function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }
                }, {
                    "data": "StartDate",
                    "type": "date"
                }, {
                    "data": "EndDate",
                    "type": "date"
                }, {
                    "data": "Address"
                }, {
                    "data": "NumberOfGuests"
                }, {
                    "data": "TotalAmount",
                    "render": function (data, type, row, meta) {
                        return data + " €";
                    }
                }, {
                    "data": "Status",
                    "render": function (data, type, row, meta) {
                        switch (data) {
                            case 0: return ""; break;
                            case 1: return "<p class='text-primary mb-0'>Confermato</p>"; break;
                            case 2: return "<p class='text-success mb-0'>Pagato</p>"; break;
                            case 3: return "<p class='text-warning mb-0' title='" + row.EmployeeName + "'>Assegnato</p>"; break;
                            case 4: return "<p class='text-danger mb-0'>Chiuso</p>"; break;
                        }
                    }
                }, {
                    "data": null,
                    "render": function (data, type, row, meta) {
                        return '<a href="HostOrderEdit.aspx?id=' + row.Id + '&view=1"><i class="fa fa-eye" style="font-size:25px"></i></a>' + (row.Status == 1 ? '<a href="HostOrderEdit.aspx?id=' + row.Id + '"><i class="fa fa-edit" style="font-size:25px;margin-left:25px"></i></a>' : '');
                    }
                }], 

                "fnServerParams": function (aoData) {
                    aoData.dateFrom = $('#TxtDateFrom').val();
                    aoData.dateTo = $('#TxtDateTo').val();
                    aoData.status = $('#ComboStatus').val();
                    aoData.searchVal = $('#TxtSearch').val();
                }
            });

            $('#ComboStatus, #TxtDateFrom, #TxtDateTo').change(function () {
                datatable.fnDraw();
            });

            $('#TxtSearch').on('input', function () {
                datatable.fnDraw();
            });

            $("#ComboStatus").select2({ theme: 'bootstrap' }); 
            
        });
    </script>
</asp:Content>
