﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.master" AutoEventWireup="true" CodeBehind="AdminVoucher.aspx.cs" Inherits="B2B.AdminVoucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeaderPlaceHolder" runat="server">
    <link rel="stylesheet" href="Content/CSS/datatables.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
    <form runat="server" id="from1" class="custom-form hero-form mx-auto mt-4 col-md-10 pb-lg-5">
        <section class=" mb-5">
            <header class="text-center">
                <h2 class="hero-title text-black-50 mt-3 mb-4">VOUCHERS</h2>
            </header>
            <div class="container">
                <div>
                    <div class="row">
                        <div style="float: left; position: relative; z-index: 1;" class="col-lg-3 col-md-3 col-12 mb-2">
                            <asp:LinkButton ID="BtnAdd" runat="server" CausesValidation="false" OnClick="BtnAdd_Click" CssClass="btn btn-warning btn-lg text-white w-100">
                            <i class="fa fa-plus mr-sm"></i> Agg.
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
                    
                    <table class="table table-bordered table-striped text-center" id="voucher-table">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Importo</th>
                                <th>Serial Number Generator</th>
                                <th>Note</th>
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
<asp:Content ID="Content3" ContentPlaceHolderID="AdminFooterPlaceHolder" runat="server">
    <script src="Scripts/jquery.dataTables.js"></script>
    <script src="Scripts/datatables.js"></script>
    <script type="text/javascript">
        $(function () {
            var datatable = $('#voucher-table').dataTable({
                "serverSide": true,
                "ajax": 'DataService.asmx/FindVouchers',
                "dom": '<"table-responsive"t>pr',
                "autoWidth": false,
                "pageLength": 20,
                "processing": true,
                "ordering": false,
                "columns": [{
                    "data": "Id",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                }, {
                    "data": "Amount",
                    "render": function (data, type, row, meta) {
                        return data + " €";
                    }
                }, {
                    "data": "SerialNumberGenerator",
                    "render": function (data, type, row, meta) {
                        return '<label class="text-black"' + (row.Summary != null ? ' title="' + row.Summary + '">' : '>') + data + '</label>' + (row.Owner != null ? '<i class="bg-success text-white p-1 rounded-3 ms-5">' + row.Owner + '</i>' : '');
                    }
                }, {
                    "data": "Note"
                }, {
                    "data": null,
                    "render": function (data, type, row, meta) {
                        return '<a href="#" class="btn-delete me-4"><i class="fa fa-trash" style="font-size:25px"></i></a>' + '<a href="AdminVoucherEdit.aspx?id=' + row.Id + '"><i class="fa fa-edit" style="font-size:25px"></i></a>';
                    }
                }], 

                "fnServerParams": function (aoData) {
                    aoData.searchVal = $('#TxtSearch').val();
                }
            });

            $('#TxtSearch').on('input', function () {
                datatable.fnDraw();
            });

            datatable.on('click', '.btn-delete', function (e) {
                e.preventDefault();

                var row = datatable.fnGetData($(this).closest('tr'));

                if (!confirm("Click OK per cancellare."))
                    return;

                $.ajax({
                    type: "POST",
                    url: 'DataService.asmx/AdminDeleteVoucher',
                    data: {
                        id: row.Id
                    },
                    success: function () {
                        onSuccess({ success: true });
                    }
                }).error(function () {
                    onSuccess({ success: false });
                });
            });

            var onSuccess = function (data) {
                if (data.success) {

                    datatable.fnDraw();

                } else {
                    alert("Failed!");
                }
            };
        });
    </script>
</asp:Content>
