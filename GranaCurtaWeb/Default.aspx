<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GranaCurtaWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Controle suas despesas</h1>
        <p class="lead">Nós iremos ajudá-lo a controlar melhor os seus gastos.<br />Experimente o Grana Curta!</p>
        <p><a href="#" class="btn btn-success btn-lg">Cadastre-se &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Importe extratos</h2>
            <p>
                Com o Grana Curta você pode importar os extratos gerados no seu banco ou cartão de crédito. Explore!
            </p>
            <p>
                <a class="btn btn-secondary" href="#">Quero começar &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Crie metas</h2>
            <p>
                É possíve ser controlado e colocar um limite nos seus gastos é um início.
            </p>
            <p>
                <a class="btn btn-secondary" href="#">Quero começar &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Acompanhe tudo</h2>
            <p>
                Faça consultas. Verifique os gastos. Veja suas metas.
            </p>
            <p>
                <a class="btn btn-secondary" href="#">Quero começar &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
