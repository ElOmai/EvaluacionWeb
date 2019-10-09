<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rCategoria.aspx.cs" Inherits="EvaluacionWeb.Registros.rCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<div class="container">
        <div class="card ">
            <div class="card-header bg-dark text-white text-center "><strong><%:Page.Title %></strong></div>
            <div class="card-body">
                <div class="row justify-content-between">
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <asp:Label ID="CategoriaIdLB" runat="server" CssClass="input-group-text">Categoria ID</asp:Label>
                        </div>
                        <asp:TextBox ID="CategoriaIdTextBox" TextMode="Number" PlaceHolder="0" runat="server" CssClass="form-control col-xl-3 col-lg-3 col-md-3 col-sm-3 col-3" aria-describedby="CategoriaIdLB"></asp:TextBox>
                        <button id="BuscarButton" runat="server" onserverclick="BuscarButton_ServerClick" title="Buscar" class="btn btn-info btn-sm-1 col-xl-1 col-lg-1 col-md-1 col-sm-1 col-1">
                            <i class="fa fa-search"></i>
                        </button>
                    </div>
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <asp:Label ID="FechaLB" runat="server" CssClass="input-group-text">Fecha</asp:Label>
                        </div>
                        <asp:TextBox ID="FechaTextBox" TextMode="Date" runat="server" CssClass="form-control col-xl-4 col-lg-4 col-md-4 col-sm-4 col-4" aria-describedby="FechaLB" Visible="true"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <asp:Label ID="DescripcionLb" runat="server" CssClass="input-group-text">Descripcion</asp:Label>
                        </div>
                        <asp:TextBox ID="DescripcionTextBox" runat="server" CssClass="form-control input-sm col-xl-4 col-lg-4 col-md-4 col-sm-4 col-4" aria-describedby="DescripcionLb" Visible="true"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            runat="server" ID="RFVDescripcionTextBox"
                            ControlToValidate="DescripcionTextBox" ForeColor="Red"
                            ErrorMessage="*" Enabled="false"
                            Display="Dynamic" SetFocusOnError="true"
                            ToolTip="Por favor llenar el campo Descripcion!">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="text-center">
                    <div class="form-group" display: inline-block>
                        <asp:Button Text="Nuevo" CssClass="btn btn-warning btn-lg-4 btn-sm-4" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click"  />
                        <asp:Button Text="Guardar" CssClass="btn btn-success btn-lg-4 btn-sm-4" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" />
                        <asp:Button Text="Eliminar" CssClass="btn btn-danger btn-lg-4 btn-sm-4" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

