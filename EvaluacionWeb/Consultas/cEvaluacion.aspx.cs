using BLL;
using Entidades;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EvaluacionWeb.Consultas
{
    public partial class cEvaluaciones : System.Web.UI.Page
    {
        static List<Evaluacion> Lista = new List<Evaluacion>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaDesdeTextBox.Text = DateTime.Now.ToFormatDate();
                FechaHastaTextBox.Text = DateTime.Now.ToFormatDate();
            }
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Expression<Func<Evaluacion, bool>> filtro = x => true;
            RepositorioBase<Evaluacion> repositorio = new RepositorioBase<Evaluacion>();
            int id;
            switch (BuscarPorDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = x => true;
                    break;
                case 1://ID
                    FiltroTextBox.TextMode = TextBoxMode.Number;
                    id = (FiltroTextBox.Text).ToInt();
                    filtro = x => x.EvaluacionID == id;
                    break;
                case 2:// nombre
                    FiltroTextBox.TextMode = TextBoxMode.Number;
                    id = (FiltroTextBox.Text).ToInt();
                    filtro = x => x.EstudianteId == id;
                    break;
                case 3:
                    filtro = x => x.TotalPerdido == FiltroTextBox.Text.ToDecimal();
                    break;
            }

            FiltroTextBox.TextMode = TextBoxMode.SingleLine;
            DateTime fechaDesde = FechaDesdeTextBox.Text.ToDatetime();
            DateTime FechaHasta = FechaHastaTextBox.Text.ToDatetime();
            if (FechaCheckBox.Checked)
                Lista = repositorio.GetList(filtro).Where(x => x.Fecha >= fechaDesde && x.Fecha <= FechaHasta).ToList();
            else
                Lista = repositorio.GetList(filtro);
            repositorio.Dispose();
            using (RepositorioBase<Estudiante> repositorioEstudiantes = new RepositorioBase<Estudiante>())
            {
                foreach (var item in Lista)
                {
                    item.NombreEstudiante = repositorioEstudiantes.Buscar(item.EstudianteId).NombreCompleto;
                }
            }
            this.BindGrid(Lista);
        }

        private void BindGrid(List<Evaluacion> lista)
        {
            DatosGridView.DataSource = null;
            DatosGridView.DataSource = lista;
            DatosGridView.DataBind();
        }

        protected void DatosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DatosGridView.DataSource = Lista;
            DatosGridView.PageIndex = e.NewPageIndex;
            DatosGridView.DataBind();
        }

        protected void FechaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FechaCheckBox.Checked)
            {
                FechaDesdeTextBox.Visible = true;
                FechaHastaTextBox.Visible = true;
            }
            else
            {
                FechaDesdeTextBox.Visible = false;
                FechaHastaTextBox.Visible = false;
            }
        }

        protected void VerDetalleButton_Click(object sender, EventArgs e)
        {
            string titulo = "Detalle de la Evaluación";
            Utils.MostrarModal(this.Page, "ShowPopup", titulo);
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            var Evaluacion = Lista.ElementAt(row.RowIndex);
            DetalleDatosGridView.DataSource = null;
            RepositorioEvaluacion Repositorio = new RepositorioEvaluacion();
            List<EvaluacionDetalle> Details = Repositorio.Buscar(Evaluacion.EvaluacionID).EvaluacionDetalle;
            using (RepositorioBase<Categoria> RepositorioCategorias = new RepositorioBase<Categoria>())
            {
                Details.ForEach(x => x.Categoria = RepositorioCategorias.Buscar(x.CategoriaId).Descripcion);
            }
            DetalleDatosGridView.DataSource = Details;
            DetalleDatosGridView.DataBind();
            Repositorio.Dispose();
        }

        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", $"ShowReporte('Listado de Evaluaciones');", true);

            EvaluacionesReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            EvaluacionesReportViewer.Reset();
            EvaluacionesReportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reportes\ListadoEvaluaciones.rdlc");
            EvaluacionesReportViewer.LocalReport.DataSources.Clear();

            EvaluacionesReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Evaluaciones",
                                                               Lista));
            EvaluacionesReportViewer.LocalReport.Refresh();
        }
    }
}