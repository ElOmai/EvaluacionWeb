<%@ Page Title="Registro de Estudiantes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rEstudiante.aspx.cs" Inherits="EvaluacionWeb.Registros.rEstudiante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<div class="container">
        <div class="card">
            <div class="card-header bg-dark text-white text-center"><strong><%:Page.Title %></strong></div>
            <div class="card-body justify-content-center align-items-center">
                <div class="row ">
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <asp:Label ID="EstudianteIdLB" runat="server" CssClass="input-group-text">Estudiante ID</asp:Label>
                        </div>
                        <asp:TextBox ID="EstudianteIdTextBox" TextMode="Number" PlaceHolder="0" runat="server" CssClass="form-control col-xl-3 col-lg-3 col-md-3 col-sm-3 col-3" aria-describedby="EstudianteIdLB"></asp:TextBox>
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
                            <asp:Label ID="NombreLb" runat="server" CssClass="input-group-text">Nombre</asp:Label>
                        </div>
                        <asp:TextBox ID="NombreTextBox" runat="server" CssClass="form-control col-xl-4" aria-describedby="NombreLb" Visible="true"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            runat="server" ID="RFVNombreTextBox"
                            ControlToValidate="NombreTextBox" ForeColor="Red"
                            ErrorMessage="*" Enabled="false"
                            Display="Dynamic" SetFocusOnError="true"
                            ToolTip="Por favor llenar el campo Nombre!">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <asp:Label ID="ApellidoLb" runat="server" CssClass="input-group-text">Apellido</asp:Label>
                        </div>
                        <asp:TextBox ID="ApellidoTextBox" runat="server" CssClass="form-control col-xl-4" aria-describedby="ApellidoLb" Visible="true"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            runat="server" ID="RFVApellidoTextBox"
                            ControlToValidate="ApellidoTextBox" ForeColor="Red"
                            ErrorMessage="*" Enabled="false"
                            Display="Dynamic" SetFocusOnError="true"
                            ToolTip="Por favor llenar el campo Apellido!">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <asp:Label ID="PTPLb" runat="server" CssClass="input-group-text">Puntos totales Perdidos</asp:Label>
                        </div>
                        <asp:TextBox ID="PuntosTotalesPerdidosTextBox" Text="0" TextMode="Number" runat="server" Enabled="false" CssClass="form-control col-xl-4" aria-describedby="PTPLb" Visible="true"></asp:TextBox>
                        
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="text-center">
                    <div class="form-group" display: inline-block>
                        <asp:Button Text="Nuevo" CssClass="btn btn-warning btn-lg-4 btn-sm-4" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click"   />
                        <asp:Button Text="Guardar" CssClass="btn btn-success btn-lg-4 btn-sm-4" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click"  />
                        <asp:Button Text="Eliminar" CssClass="btn btn-danger btn-lg-4 btn-sm-4" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
