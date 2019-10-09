<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cEvaluacion.aspx.cs" Inherits="EvaluacionWeb.Consultas.cEvaluaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<script type="text/javascript">
        function ShowPopup(title) {
            $("#ModalDetalle .modal-title").html(title);
            $("#ModalDetalle").modal("show");
        }
    </script>
    <script type="text/javascript">
        function ShowReporte(title) {
            $("#ModalReporte .modal-title").html(title);
            $("#ModalReporte").modal("show");
        }
    </script>
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
        Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <asp:ScriptManager runat="server" ID="ScriptManager"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="card text-center bg-light mb-3">
            <div class="card-header"><%:Page.Title %></div>
            <div class="card-body">
                <div class="form-horizontal col-md-12" role="form">
                    <div>
                        <div class="row justify-content-between">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="FiltroDropDownList">Filtro </span>
                                </div>
                                <div class="col-md-4 col-lg-4 col-xl-4 col-sm-4">
                                    <asp:DropDownList ID="BuscarPorDropDownList" AutoPostBack="true" runat="server" CssClass="form-control ">
                                        <asp:ListItem>Todos</asp:ListItem>
                                        <asp:ListItem>EvaluacionID</asp:ListItem>
                                        <asp:ListItem>EstudianteID</asp:ListItem>
                                        <asp:ListItem>Total Perdido</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="CriterioLB">Criterio </span>
                                </div>
                                <asp:TextBox ID="FiltroTextBox" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <div class="input-group-append" aria-describedby="FiltroTextBox">
                                    <asp:LinkButton ID="BuscarButton" runat="server" CssClass="btn btn-success input-sm" OnClick="BuscarButton_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <asp:CheckBox AutoPostBack="true" Checked="true" OnCheckedChanged="FechaCheckBox_CheckedChanged" ID="FechaCheckBox" runat="server" Text="Filtrar por fecha" />
                        </div>
                    </div>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="FechaDesde">Desde </span>
                        </div>
                        <div class="input-group-append" aria-describedby="FechaDesdeTextBox">
                            <asp:TextBox ID="FechaDesdeTextBox" TextMode="Date" runat="server" class="form-control input-sm" Visible="true"></asp:TextBox>
                        </div>
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="FechaHasta">Hasta </span>
                        </div>
                        <div class="input-group-append" aria-describedby="FechaHastaTextBox">
                            <asp:TextBox ID="FechaHastaTextBox" TextMode="Date" runat="server" class="form-control input-sm" Visible="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="DatosGridView"
                                    runat="server" AutoGenerateColumns="false" HtmlEncode="False"
                                    CssClass="table table-condensed table-hover table-responsive-md table-responsive-sm table-responsive-lg table-responsive-m"
                                    CellPadding="4" ForeColor="#333333" GridLines="None"
                                    AllowPaging="true" PageSize="6"
                                    OnPageIndexChanging="DatosGridView_PageIndexChanging">

                                    <AlternatingRowStyle BackColor="LightBlue" />
                                    <Columns>
                                        <asp:HyperLinkField ControlStyle-CssClass="btn btn-info"
                                            DataNavigateUrlFields="EvaluacionID"
                                            DataNavigateUrlFormatString="~/Registros/RegistroEvaluaciones.aspx?EvaluacionID={0}"
                                            Text="Editar"></asp:HyperLinkField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Opciones">
                                            <ItemTemplate>
                                                <asp:Button ID="VerDetalleButton" runat="server" CausesValidation="false" CommandName="Select"
                                                    Text="Ver Detalle" CssClass="btn btn-danger" OnClick="VerDetalleButton_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="EvaluacionID" DataField="EvaluacionID" />
                                        <asp:BoundField HeaderText="EstudianteId" DataField="EstudianteId" Visible="false" />
                                        <asp:BoundField HeaderText="Nombre" DataField="NombreEstudiante" />
                                        <asp:BoundField HeaderText="Total Perdido" DataField="TotalPerdido" />
                                        <asp:BoundField HeaderText="Fecha Registro" DataField="Fecha" DataFormatString="{0:dd-MM-yyyy}" />
                                    </Columns>

                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DatosGridView" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <asp:Button ID="ImprimirButton" runat="server" CssClass="btn btn-success input-sm" Text="Imprimir" OnClick="ImprimirButton_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalDetalle" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog ml-sm-auto" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="AgregarPacientesLB"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="table table-responsive">
                                    <asp:GridView ID="DetalleDatosGridView"
                                        runat="server"
                                        CssClass="table table-condensed table-hover table-responsive"
                                        CellPadding="4" ForeColor="#333333"
                                        OnPageIndexChanging="DatosGridView_PageIndexChanging"
                                        AllowPaging="true" PageSize="6"
                                        GridLines="None" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField HeaderText="Detalle ID" DataField="DetalleID" Visible="false" />
                                            <asp:BoundField HeaderText="EvaluacionID" DataField="EvaluacionID" Visible="false" />
                                            <asp:BoundField HeaderText="CategoriaId" DataField="CategoriaId" Visible="false" />
                                            <asp:BoundField HeaderText="Categoria" DataField="Categoria" />
                                            <asp:BoundField HeaderText="Valor" DataField="Valor" />
                                            <asp:BoundField HeaderText="Logrado" DataField="Logrado" />
                                            <asp:BoundField HeaderText="Perdido" DataField="Perdido" />
                                        </Columns>
                                        <AlternatingRowStyle BackColor="LightBlue" />

                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DatosGridView" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="ModalReporte" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm" style="max-width: 1000px!important; min-width: 550px!important">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalLebel">Reporte de Estudiantes</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <rsweb:ReportViewer ID="EvaluacionesReportViewer" runat="server" ProcessingMode="Remote" Height="100%" Width="100%">
                    </rsweb:ReportViewer>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
