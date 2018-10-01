<%@ Page Title="Cartões de crédito" Language="C#" MasterPageFile="~/SiteSistema.Master" AutoEventWireup="true" CodeBehind="Cartoes.aspx.cs" Inherits="GranaCurtaWeb.Sistema.Cartoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {

            //-------------------------------
            //Global variables
            //alert($("#<%= Master.FindControl("hdnToken").ClientID %>").val());

            var uriPadrao = '/api/cartoes/';

            //-------------------------------
            //Load data
            $("#modalWaiting").modal('show');
            loadCardsCartoes(null);

            //-------------------------------
            //Bind methods

            $("#btnNew").click(function () {
                showModalDetail(null);
            });

            $("#btnSalvar").click(function (card) {

                if (validate()) {
                    var objJson = {
                        id_cartao: $("#hdnIdCartao").val(),
                        nm_cartao: $("#txtCartao").val().trim(),
                        vl_limite: $("#txtLimite").val().trim().replace('.', '').replace(',', '.'),
                        nm_bandeira: $("#txtBandeira").val().trim(),
                        nm_4_ult_dig: $("#txt4UltDig").val().trim(),
                        vl_vencimento_dia: $("#txtVencimentoDia").val().trim()
                    };

                    var intId = $("#hdnIdCartao").val();

                    var strOperationType = intId === "" ? "POST" : "PUT";
                    var uri = uriPadrao + (intId === "" ? "" : "" + intId);

                    $.ajax({
                        url: uri,
                        type: strOperationType,
                        data: objJson,
                        headers: {
                            'Authorization': 'Bearer ' + $("#hdnToken").val()
                        },
                        success: function (result) {
                            $("#modalNew").modal('toggle');
                            $("#modalWaiting").modal('show');
                            loadCardsCartoes(null);
                        }
                    });
                }
            });

            $("#btnExcluir").click(function () {
                $('#modalDelete').modal({ backdrop: 'static', keyboard: true });

                var uri = uriPadrao + $("#hdnIdCartaoExcluir").val();

                $.ajax({
                    url: uri,
                    type: 'DELETE',
                    headers: {
                        'Authorization': 'Bearer ' + $("#hdnToken").val()
                    },
                    success: function (result) {

                        $("#modalDelete").modal('toggle');
                        $("#modalWaiting").modal('show');
                        loadCardsCartoes(null);
                    }
                });
            });

            //-------------------------------
            //User functions

            function loadCardsCartoes(intIdCartao) {
                //$("#modalWaiting").modal('show');

                if (!intIdCartao) {
                    $('#lstCartoes').empty();
                    //$("#modalWaiting").modal('hide');
                }

                $.ajax({
                    url: uriPadrao + (intIdCartao ? "" + intIdCartao : ""),
                    type: 'GET',
                    headers: {
                        'Authorization': 'Bearer ' + $("#hdnToken").val()
                    },
                    success: function (result) {
                        $.each(result, function (key, item) {
                            var card = $('.card-template').clone();
                            card.removeClass("card-template");

                            loadCardData(card, item)

                            card.removeClass("d-none");
                            card.find(".excluir").click(function () {
                                showModalExcluir(card);
                                $("#hdnIdCartaoExcluir").val(item.id_cartao);
                            });
                            card.find(".editar").click(function () {
                                showModalDetail(item.id_cartao);
                            });
                            card.appendTo($('#lstCartoes'));
                            //$("#modalWaiting").modal('toggle');
                        });

                        $("#modalWaiting").modal('hide');
                    }
                });
            };

            function showModalExcluir(card) {
                //resetar o nome do botão excluir
                //Setar o nome da conta
                $("#spnCartao").text(card.find(".nm_cartao").text());

                //visualisar a modal
                $("#modalDelete").modal('toggle');
            }

            function showModalDetail(intId) {

                var objJson = null;

                if (intId) {

                    $.ajax({
                        url: uriPadrao + "" + intId,
                        type: 'GET',
                        headers: {
                            'Authorization': 'Bearer ' + $("#hdnToken").val()
                        },
                        success: function (result) {
                            objJson = new Object();
                            $.each(result, function (key, item) {
                                objJson = {
                                    id_cartao: item.id_cartao,
                                    nm_cartao: item.nm_cartao,
                                    vl_limite: item.vl_limite,
                                    nm_bandeira: item.nm_bandeira,
                                    nm_4_ult_dig: item.nm_4_ult_dig,
                                    vl_vencimento_dia: item.vl_vencimento_dia
                                }
                            });

                            loadModalData(objJson);
                            $("#modalNew").modal('toggle');
                        }
                    });
                } else {
                    loadModalData(objJson);
                    $("#modalNew").modal('toggle');
                }
            }

            function loadModalData(objJson) {

                $('#msgCartao').addClass('d-none');
                $('#msgLimite').addClass('d-none');
                $('#msgBandeira').addClass('d-none');
                $('#msg4UltDig').addClass('d-none');
                $('#msgVencimentoDia').addClass('d-none');

                if (objJson) {
                    $("#exampleModalLabel").text("Editar Cartão");
                    $("#hdnIdCartao").val(objJson.id_cartao);
                    $("#txtCartao").val(objJson.nm_cartao);
                    $("#txtLimite").val(objJson.vl_limite);
                    $("#txtBandeira").val(objJson.nm_bandeira);
                    $("#txt4UltDig").val(objJson.nm_4_ult_dig);
                    $("#txtVencimentoDia").val(objJson.vl_vencimento_dia);
                } else {
                    //reset modal
                    $("#exampleModalLabel").text("Novo Cartão");
                    $("#hdnIdCartao").val("");
                    $("#txtCartao").val("");
                    $("#txtLimite").val("");
                    $("#txtBandeira").val("");
                    $("#txt4UltDig").val("");
                    $("#txtVencimentoDia").val("");
                }
            }

            function loadCardData(card, item) {
                card.find(".id_cartao").val(item.id_cartao);
                card.find(".nm_cartao").text(item.nm_cartao);
                card.find(".nm_bandeira").text(item.nm_bandeira);
                card.find(".nm_4_ult_dig").text(item.nm_4_ult_dig);
                card.find(".vl_limite").text(item.vl_limite);
                card.find(".vl_vencimento_dia").text(item.vl_vencimento_dia);
            }

            function validate() {
                var blnValidated = true;
                var pattFormatedNumbers = /^[0-9]{1,3}(.[0-9]{3})*(\,[0-9]+)?$/;
                var pattNumbers = /[^0-9]/;

                if ($('#txtCartao').val().trim() == '') {
                    $('#msgCartao').removeClass('d-none');
                    blnValidated = false;
                } else {
                    $('#msgCartao').addClass('d-none');
                }

                if (!pattFormatedNumbers.test($('#txtLimite').val().trim())) {
                    $('#msgLimite').removeClass('d-none');
                    blnValidated = false;
                } else {
                    $('#msgLimite').addClass('d-none');
                }

                if ($('#txtBandeira').val().trim() == '') {
                    $('#msgBandeira').removeClass('d-none');
                    blnValidated = false;
                } else {
                    $('#msgBandeira').addClass('d-none');
                }

                var str4UltDig = $('#txt4UltDig').val().trim();
                if (
                    str4UltDig != '' && str4UltDig.length != 4
                ) {
                    $('#msg4UltDig').removeClass('d-none');
                    blnValidated = false;
                } else {
                    if (pattNumbers.test(str4UltDig)) {
                        $('#msg4UltDig').removeClass('d-none');
                        blnValidated = false;
                    } else {
                        $('#msg4UltDig').addClass('d-none');
                    }
                }

                var strVencimentoDia = $('#txtVencimentoDia').val().trim();
                if (
                    strVencimentoDia == '' ||
                    pattNumbers.test(strVencimentoDia)
                ) {
                    $('#msgVencimentoDia').removeClass('d-none');
                    blnValidated = false;
                } else {
                    if (
                        parseInt(strVencimentoDia, 10) < 1 ||
                        parseInt(strVencimentoDia, 10) > 30
                    ) {
                        $('#msgVencimentoDia').removeClass('d-none');
                        blnValidated = false;
                    } else {
                        $('#msgVencimentoDia').addClass('d-none');
                    }
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
            <input type="button" class="btn btn-outline-success" value="Novo Cartão" id="btnNew" />
        </div>
    </div>
    <div class="row flex-xl-nowrap">
        <div class="col-12">
            <hr />
        </div>
    </div>
    <div class="row" id="lstCartoes">
    </div>
    <div class="modal fade" id="modalNew" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Novo Cartão</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group col-12">
                        <input type="hidden" id="hdnIdCartao" />
                        <label for="txtCartao">Nome da Cartão</label>
                        <input type="text" class="form-control col-12" id="txtCartao" aria-describedby="emailHelp" placeholder="Exemplo NuBank, Bradesco">
                        <div class="text-danger d-none" id="msgCartao">Informe o nome do Cartão.</div>
                    </div>
                    <div class="form-group col-12">
                        <label for="txtLimite">Limite (R$)</label>
                        <input type="text" class="form-control col-12" id="txtLimite" aria-describedby="emailHelp" placeholder="0,00">
                        <div class="text-danger d-none" id="msgLimite">Informe o valor no formato correto. Ex.: 1.111,11.</div>
                    </div>

                    <div class="form-group col-12">
                        <label for="txtBandeira">Bandeira</label>
                        <input type="text" class="form-control col-12" id="txtBandeira" aria-describedby="emailHelp" placeholder="Visa, Master, Cielo">
                        <div class="text-danger d-none" id="msgBandeira">Informe o nome da bandeira do cartão.</div>
                    </div>
                    <div class="form-group col-12">
                        <label for="txt4UltDig">4 últimos dígitos do cartão (opcional)</label>
                        <input type="text" class="form-control col-12" id="txt4UltDig" aria-describedby="emailHelp" placeholder="####">
                        <div class="text-danger d-none" id="msg4UltDig">Informe somente números. 4 dígitos.</div>
                    </div>
                    <div class="form-group col-12">
                        <label for="txtVencimentoDia">Dia de vencimento</label>
                        <input type="text" class="form-control col-12" id="txtVencimentoDia" aria-describedby="emailHelp" placeholder="Exemplo 5, 10, 20">
                        <div class="text-danger d-none" id="msgVencimentoDia">Informe o valor de 1 a 30.</div>
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
                    <h5 class="modal-title">Excluir Cartão</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group col-12">
                        <div class="alert alert-danger" role="alert">
                            Você realmente deseja excluir o cartão <span class="alert-link" id="spnCartao">Nome da Cartão</span>?
                        </div>
                        <input type="hidden" id="hdnIdCartaoExcluir" />
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
                <input type="hidden" class="id_cartao" />
                <h4 class="card-title nm_cartao">Nome da cartão</h4>
                <h6 class="card-title text-secondary"><span class="nm_bandeira">Visa</span> (#### #### #### <span class="nm_4_ult_dig">6789</span>)</h6>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <p class="card-text">Limite (R$):</p>
                    </div>
                    <div class="col-6">
                        <p class="card-text text-right vl_limite">####,##</p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <hr />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <p class="card-text">Dia de vencimento: </p>
                    </div>
                    <div class="col-6">
                        <p class="card-text text-right vl_vencimento_dia">DD/MM/AAAA</p>
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
