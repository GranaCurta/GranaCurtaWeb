<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GranaCurtaWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login - Grana Curta</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body style="background-color: #e9ecef">
    <form id="frmLogin" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <script>
            $(document).ready(function () {
                $(".close").click(function () {
                    $(".message-login-alert").toggleClass("d-none");
                });

                $("#btnLogin").click(function (card) {
                    $("#btnLogin").text("Entrando...");
                    $("#btnLogin").addClass("disabled");

                    $(".message-login-alert").addClass("d-none");
                    var objJson = {
                        email: $("#inpEmail").val(),
                        senha: $("#inpSenha").val()
                    };

                    var uri = "/api/login/";

                    $.ajax({
                        url: uri,
                        type: "POST",
                        data: objJson,
                        success: function (result) {
                            $("#btnLogin").removeClass("disabled");
                            $("#btnLogin").text("Entrar");
                            $("#<% =hdnToken.ClientID %>").val(result);
                            $("#frmLogin").submit();
                        },
                        error: function (result) {
                            $(".message-login-alert").removeClass("d-none");
                            $(".message-login-text").text(result.responseJSON);
                            $("#btnLogin").removeClass("disabled");
                            $("#btnLogin").text("Entrar");
                        }
                    });
                });
            });
        </script>
        <asp:HiddenField ID="hdnToken" runat="server" />
        <div class="container body-content p-4">
            <div class="row justify-content-center">
                <div class="col-auto">
                    <a class="navbar-brand font-weight-normal text-secondary" runat="server" href="~/">
                        <img class="mb-4" src="favicon.ico" alt="" width="72" height="72" />
                    </a>
                </div>
            </div>
            <div class="row justify-content-center">
                <div class="col-auto">
                    <h5 class="mb-4">Entrar no Grana Curta</h5>
                </div>
            </div>

            <div class="row justify-content-center">
                <div class="alert alert-danger alert-dismissible show message-login-alert d-none" role="alert">
                    <span class="message-login-text">[E-mail ou senha inválidos.]</span>
                    <button type="button" class="close" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </div>

            <div class="row justify-content-center">
                <div class="card border-success" style="min-width: 330px">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label for="inputEmail">Email</label>
                                <input type="email" id="inpEmail" class="form-control" placeholder="email@exemplo.com" />
                            </div>
                        </div>
                        <div class="row pt-2">
                            <div class="col">
                                <label for="inpSenha">Senha</label>
                                <input type="password" id="inpSenha" class="form-control" placeholder="Senha" style="width: 100%" />
                            </div>
                        </div>
                        <div class="row justify-content-center">
                            <div class="col-auto">
                                <button id="btnLogin" class="btn btn-success mt-4 btn-block" type="button">Entrar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row justify-content-center">
                <div class="col-auto">
                    <p class="mt-5 mb-3">&copy; <%: DateTime.Now.Year %> - Grana Curta</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
