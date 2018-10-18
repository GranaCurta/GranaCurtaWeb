<%@ Page Title="Cartões de crédito" Language="C#" MasterPageFile="~/SiteSistema.Master" AutoEventWireup="true" CodeBehind="Movimentacoes.aspx.cs" Inherits="GranaCurtaWeb.Sistema.Movimentacoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {
            //-------------------------------
            //Global variables

            var uriPadrao = '/api/movimentacoes/';

            //-------------------------------
            //Load data

            $("#modalWaiting").modal('show');
            loadTableMov(null);

            //-------------------------------
            //Bind methods


            //-------------------------------
            //User functions

            function loadTableMov(strDtReferencia) {

                if (!strDtReferencia) {
                    $('#tbdMovimentacoes').empty();
                }

                $.ajax({
                    url: uriPadrao,
                    type: 'GET',
                    headers: {
                        'Authorization': 'Bearer ' + $("#hdnToken").val()
                    },
                    success: function (result) {
                        $.each(result, function (key, item) {
                            var tableRow = $('<tr>');

                            loadRowData(tableRow, item)

                            tableRow.appendTo($('#tbdMovimentacoes'));
                        });

                        $("#modalWaiting").modal('hide');
                    }
                });
            };

            function loadRowData(tableRow, item) {
                $('<th scope="row" class="d-none">' + item.id_movimentacao + '</th>').appendTo(tableRow);
                $('<td>' + item.dt_movimentacao_f + '</td>').appendTo(tableRow);
                $('<td>' + item.dt_referencia_f + '</td>').appendTo(tableRow);
                $('<td>' + item.ds_movimentacao_c + '</td>').appendTo(tableRow);
                $('<td>' + item.nm_categoria + '</td>').appendTo(tableRow);
                $('<td>' + item.nm_conta_c + '</td>').appendTo(tableRow);
                $('<td>' + item.nm_cartao_c + '</td>').appendTo(tableRow);
                $('<td>' + item.vl_movimentacao_f + '</td>').appendTo(tableRow);
            }

        });
    </script>
    <div class="row flex-xl-nowrap">
        <div class="col-12 d-md-none d-lg-none d-xl-none">
            <hr />
        </div>
    </div>
    <div class="row" id="lstMovimentacoes">
        <div class="col-12">
            <div class="table-responsive table-hover table-sm">
                <table class="table">
                    <thead class="table-success">
                        <tr>
                            <th scope="col" class="d-none">#</th>
                            <th scope="col" class="align-middle">Data</th>
                            <th scope="col" class="align-middle">Mês de lançamento</th>
                            <th scope="col" class="align-middle">Descrição</th>
                            <th scope="col" class="align-middle">Categoria</th>
                            <th scope="col" class="align-middle">Conta bancária</th>
                            <th scope="col" class="align-middle">Cartão de crédito</th>
                            <th scope="col" class="align-middle">Valor</th>
                        </tr>
                    </thead>
                    <tbody id="tbdMovimentacoes">
                        <tr>
                            <th scope="row" class="d-none"></th>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
