<%@ Page Title="Contas Bancárias" Language="C#" MasterPageFile="~/SiteSistema.Master" AutoEventWireup="true" CodeBehind="Contas.aspx.cs" Inherits="GranaCurtaWeb.Sistema.Contas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {

            //-------------------------------
            //Global variables

            var uri = '/api/contas/';

            //-------------------------------
            //Load data
            $("#modalWaiting").modal('show');
            loadCardsContas(null);

            //Busca os tipos de contas
            loadModalComboTiposContas();

            //-------------------------------
            //Bind methods

            $("#btnNew").click(function () {
                showModalDetail(null);
            });

            $("#btnSalvar").click(function (card) {

                if (validate()) {
                    //$("#modalWaiting").modal('show');

                    var objJson = {
                        id_conta: $("#idConta").val(),
                        nm_conta: $("#txtNomeConta").val().trim(),
                        vl_limite_ce: $("#txtLimiteCE").val().replace('.', '').replace(',', '.'),
                        id_tipo_conta: $("#cmbTiposContas").val()
                    };

                    var intIdConta = $("#idConta").val();

                    var strOperationType = intIdConta === "" ? "POST" : "PUT";
                    var uri = "/api/contas" + (intIdConta === "" ? "" : "/" + intIdConta);

                    $.ajax({
                        url: uri,
                        type: strOperationType,
                        data: objJson,
                        success: function (result) {
                            $("#modalNew").modal('toggle');
                            $("#modalWaiting").modal('show');
                            loadCardsContas(null);
                        }
                    });
                }
            });

            $("#btnExcluir").click(function () {
                $('#modalDelete').modal({ backdrop: 'static', keyboard: true });

                var uri = "/api/contas/" + $("#idContaExcluir").val();

                $.ajax({
                    url: uri,
                    type: 'DELETE',
                    success: function (result) {
                        $("#modalDelete").modal('toggle');
                        $("#modalWaiting").modal('show');
                        loadCardsContas(null);
                        //$("#modalWaiting").modal('hide');
                    }
                });
            });

            //-------------------------------
            //User functions

            function loadCardsContas(intIdConta) {
                //$("#modalWaiting").modal('show');

                if (!intIdConta) {
                    $('#lstContas').empty();
                    //$("#modalWaiting").modal('hide');
                }
                $.getJSON(uri + (intIdConta ? "" + intIdConta : ""))
                    .done(function (data) {
                        // On success, 'data' contains a list of products.
                        $.each(data, function (key, item) {
                            var card = $('.card-template').clone();
                            card.removeClass("card-template");

                            loadCardData(card, item)

                            card.removeClass("d-none");
                            card.find(".excluir").click(function () {
                                showModalExcluir(card);
                                $("#idContaExcluir").val(item.id_conta);
                            });
                            card.find(".editar").click(function () {
                                showModalDetail(item.id_conta);
                            });
                            card.appendTo($('#lstContas'));
                            //$("#modalWaiting").modal('toggle');
                        });

                        $("#modalWaiting").modal('hide');
                    });
            };

            function loadModalComboTiposContas() {
                var uri = "/api/tiposcontas";

                $.getJSON(uri)
                    .done(function (data) {
                        $("#cmbTiposContas").empty();
                        $("#cmbTiposContas").append("<option value=\"\">Selecione...</option>");
                        $.each(data, function (key, item) {
                            $("#cmbTiposContas")
                                .append("<option value=\"" + item.id_tipo_conta + "\">" + item.nm_tipo_conta + "</option>");
                        });
                    });
            };

            function showModalExcluir(card) {
                //resetar o nome do botão excluir
                //Setar o nome da conta
                $("#spnConta").text(card.find(".nm_conta").text());

                //visualisar a modal
                $("#modalDelete").modal('toggle');
            }

            function showModalDetail(intIdConta) {

                var objJson = null;

                if (intIdConta) {
                    //$('#modalWaiting').modal('toggle');
                    $.getJSON(uri + "" + intIdConta)
                        .done(function (data) {
                            objJson = new Object();
                            $.each(data, function (key, item) {
                                objJson = {
                                    id_conta: item.id_conta,
                                    nm_conta: item.nm_conta,
                                    vl_limite_ce: item.vl_limite_ce,
                                    id_tipo_conta: item.id_tipo_conta
                                }
                            });

                            loadModalData(objJson);
                            //$('#modalWaiting').modal('toggle');
                            $("#modalNew").modal('toggle');
                        });
                } else {
                    loadModalData(objJson);
                    $("#modalNew").modal('toggle');
                }
            }

            function loadModalData(objJson) {

                $('#msgInvNomeConta').addClass('d-none');
                $('#msgInvLimiteCE').addClass('d-none');
                $('#msgInvTiposContas').addClass('d-none');

                if (objJson) {
                    $("#exampleModalLabel").text("Editar Conta");
                    $("#idConta").val(objJson.id_conta);
                    $("#txtNomeConta").val(objJson.nm_conta);
                    $("#txtLimiteCE").val(objJson.vl_limite_ce);
                    $("#cmbTiposContas").val(objJson.id_tipo_conta);
                } else {
                    //reset modal
                    $("#exampleModalLabel").text("Nova Conta");
                    $("#idConta").val("");
                    $("#txtNomeConta").val("");
                    $("#txtLimiteCE").val("");
                    $("#cmbTiposContas").val("");
                }
            }

            function loadCardData(card, item) {
                card.find(".id_conta").val(item.id_conta);
                card.find(".nm_conta").text(item.nm_conta);
                card.find(".id_tipo_conta").val(item.id_tipo_conta);
                card.find(".nm_tipo_conta").append(item.nm_tipo_conta);
                card.find(".vl_saldo").text(item.vl_saldo);
                card.find(".vl_limite_ce").text(item.vl_limite_ce);
            }

            function validate() {
                var blnValidated = true;

                if ($('#txtNomeConta').val().trim() == '') {
                    $('#msgInvNomeConta').removeClass('d-none');
                    blnValidated = false;
                } else {
                    $('#msgInvNomeConta').addClass('d-none');
                }

                var pattFormatedNumbers = /^[0-9]{1,3}(.[0-9]{3})*(\,[0-9]+)?$/;

                if (!pattFormatedNumbers.test($('#txtLimiteCE').val().trim())) {
                    $('#msgInvLimiteCE').removeClass('d-none');
                    blnValidated = false;
                } else {
                    $('#msgInvLimiteCE').addClass('d-none');
                }

                if ($('#cmbTiposContas').val().trim() == '') {
                    $('#msgInvTiposContas').removeClass('d-none');
                    blnValidated = false;
                } else {
                    $('#msgInvTiposContas').addClass('d-none');
                }

                return blnValidated;
            }
        });
    </script>
    <div class="row flex-xl-nowrap">
        <div class="col-12 d-md-none d-lg-none d-xl-none">
            <hr />
        </div>
    </div>
    <div class="row flex-xl-nowrap">
        <div class="col-12">
            <input type="button" class="btn btn-outline-success" value="Nova Conta" id="btnNew" />
        </div>
    </div>
    <div class="row flex-xl-nowrap">
        <div class="col-12">
            <hr />
        </div>
    </div>
    <div class="row" id="lstContas">
    </div>
    <div class="modal fade" id="modalNew" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Nova Conta</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group col-12">
                        <input type="hidden" id="idConta" />
                        <label for="exampleInputEmail1">Nome da Conta</label>
                        <input type="text" class="form-control col-12" id="txtNomeConta" aria-describedby="emailHelp" placeholder="Exemplo Banrisul, Bradesco">
                        <div class="text-danger d-none" id="msgInvNomeConta">Informe o nome da conta.</div>
                    </div>
                    <div class="form-group col-12">
                        <label for="exampleInputPassword1">Limite Cheque Especial (R$)</label>
                        <input type="text" class="form-control col-12" id="txtLimiteCE" placeholder="0,00">
                        <div class="text-danger d-none" id="msgInvLimiteCE">Informe o valor no formato correto. Ex.: 1.111,11.</div>
                    </div>
                    <div class="form-group col-12">
                        <label for="exampleInputPassword1">Tipo de Conta</label>
                        <select class="form-control col-12 custom-select" id="cmbTiposContas">
                            <option value="">Selecione...</option>
                        </select>
                        <div class="text-danger d-none" id="msgInvTiposContas">Selecione um tipo de conta.</div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-success" id="btnSalvar">Salvar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalDelete" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Excluir Conta</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group col-12">
                        <div class="alert alert-danger" role="alert">
                            Você realmente deseja excluir a conta <span class="alert-link" id="spnConta">Nome da Conta</span>?
                        </div>
                        <input type="hidden" id="idContaExcluir" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-outline-danger" id="btnExcluir">Excluir!</button>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-6 d-none card-template">
        <div class="card m-1 border-success">
            <div class="card-header">
                <input type="hidden" class="id_conta" />
                <h4 class="card-title nm_conta">Nome da conta</h4>
                <input type="hidden" class="id_tipo_conta" />
                <h6 class="card-title text-secondary nm_tipo_conta">Conta </h6>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <p class="card-text">Saldo (R$):</p>
                    </div>
                    <div class="col-6">
                        <p class="card-text text-right vl_saldo">####,##</p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <hr />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <p class="card-text">Cheque especial (R$): </p>
                    </div>
                    <div class="col-6">
                        <p class="card-text text-right vl_limite_ce">####,##</p>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <input type="button" class="btn btn-outline-secondary excluir" value="Excluir" />
                <input type="button" class="btn btn-outline-success editar" value="Editar" />
            </div>
        </div>
    </div>
</asp:Content>
