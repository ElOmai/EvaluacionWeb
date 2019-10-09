<%@ Page Title="Registro de Evaluaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rEvaluacion.aspx.cs" Inherits="EvaluacionWeb.Registros.rEvaluacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<div class="container">
        <div class="card">
            <div class="card-header bg-white text-dark text-center"><strong><%:Page.Title %></strong></div>
            <div class="card-body justify-content-center align-items-center">
                <div class="row ">
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <asp:Label ID="EvaluacionIdLB" runat="server" CssClass="input-group-text">Evaluacion ID</asp:Label>
                        </div>
                        <asp:TextBox ID="EvaluacionIdTextBox" TextMode="Number" PlaceHolder="0" runat="server" CssClass="form-control col-xl-3 col-lg-3 col-md-3 col-sm-3 col-3" aria-describedby="EvaluacionIdLB"></asp:TextBox>
                        <button id="BuscarButton" runat="server" onserverclick="BuscarButton_Click" title="Buscar" class="btn btn-info btn-sm-1 col-xl-1 col-lg-1 col-md-1 col-sm-1 col-1">
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
                            <asp:Label ID="EstudianteLb" runat="server" CssClass="input-group-text">Estudiante</asp:Label>
                        </div>
                        <asp:DropDownList ID="EstudianteDropdownList" CssClass=" form-control dropdown-item col-md-3" AppendDataBoundItems="true" runat="server" Height="2.5em">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <asp:Label ID="CategoriaLb" runat="server" CssClass="input-group-text">Categoria</asp:Label>
                        </div>
                        <asp:DropDownList ID="CategoriaDropDownList" CssClass=" form-control dropdown-item col-md-3" AppendDataBoundItems="true" runat="server" Height="2.5em">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <asp:Label ID="ValorLb" runat="server" CssClass="input-group-text">Valor</asp:Label>
                        </div>
                        <asp:TextBox ID="ValorTextBox" TextMode="Number" PlaceHolder="0" runat="server" CssClass="form-control col-xl-3 col-lg-3 col-md-3 col-sm-3 col-3" aria-describedby="ValorLb"></asp:TextBox>
                        <div class="input-group-prepend">
                            <asp:Label ID="LogradoLb" runat="server" CssClass="input-group-text">Logrado</asp:Label>
                        </div>
                        <asp:TextBox ID="LogradoTextBox" TextMode="Number" PlaceHolder="0" runat="server" CssClass="form-control col-xl-3 col-lg-3 col-md-3 col-sm-3 col-3" aria-describedby="LogradoLb"></asp:TextBox>
                        <asp:Button ID="AgregarButton" runat="server" OnClick="AgregarButton_Click" Text="Agregar" CssClass="btn btn-outline-info text-black-50" />
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManger" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="table table-responsive col-md-12">
                                <asp:GridView ID="DetalleGridView"
                                    runat="server" AutoGenerateColumns="false"
                                    CssClass="table table-condensed table-hover table-responsive"
                                    CellPadding="4" ForeColor="#333333" GridLines="None"
                                    AllowPaging="true" PageSize="5">
                                    <AlternatingRowStyle BackColor="LightBlue" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Opciones">
                                            <ItemTemplate>
                                                <asp:Button ID="RemoverDetalleClick" runat="server" CausesValidation="false" CommandName="Select"
                                                    Text="Remover" CssClass="btn btn-danger btn-sm" OnClick="RemoverDetalleClick_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="DetalleID" DataField="DetalleID" Visible="false" />
                                        <asp:BoundField HeaderText="EvaluacionID" DataField="EvaluacionID" Visible="false" />
                                        <asp:BoundField HeaderText="CategoriaId" DataField="CategoriaId" Visible="false" />
                                        <asp:BoundField HeaderText="Categoria" DataField="Categoria" />
                                        <asp:BoundField HeaderText="Valor" DataField="Valor" />
                                        <asp:BoundField HeaderText="Logrado" DataField="Logrado" />
                                        <asp:BoundField HeaderText="Perdido" DataField="Perdido" />
                                    </Columns>
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DetalleGridView" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                        <asp:Label ID="TotalPerdidoLB" runat="server" CssClass="input-group-text">TotalPerdido</asp:Label>
                    </div>
                    <asp:TextBox ID="TotalPerdidoTextBox" Enabled="false" AutoPostBack="true" TextMode="Number" PlaceHolder="0" runat="server" CssClass="form-control col-xl-3 col-lg-3 col-md-3 col-sm-3 col-3" aria-describedby="TotalPerdidoLB"></asp:TextBox>
                </div>
            </div>
            <div class="card-footer">
                <div class="text-center">
                    <div class="form-group" display: inline-block>
                        <asp:Button Text="Nuevo" CssClass="btn btn-warning btn-lg-4 btn-sm-4" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click" />
                        <asp:Button Text="Guardar" CssClass="btn btn-success btn-lg-4 btn-sm-4" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" />
                        <asp:Button Text="Eliminar" CssClass="btn btn-danger btn-lg-4 btn-sm-4" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>