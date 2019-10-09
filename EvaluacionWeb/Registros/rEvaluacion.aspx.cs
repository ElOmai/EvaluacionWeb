using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EvaluacionWeb.Registros
{
    public partial class rEvaluacion : System.Web.UI.Page
    {
        readonly string KeyViewState = "Evaluacion";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
                ViewState[KeyViewState] = new Evaluacion();
                Limpiar();
                int id = Request.QueryString["EvaluacionID"].ToInt();
                if (id > 0)
                {
                    using (RepositorioEvaluacion repositorio = new RepositorioEvaluacion())
                    {
                        Evaluacion evaluaciones = repositorio.Buscar(id);
                        if (evaluaciones.EsNulo())
                            Utils.Alerta(this, TipoTitulo.Informacion, TiposMensajes.RegistroNoEncontrado, IconType.info);
                        else
                            LlenarCampos(evaluaciones);
                    }
                }
            }
        }
        private void LlenarCombo()
        {
            RepositorioBase<Estudiante> repositorio = new RepositorioBase<Estudiante>();
            Utils.LlenarCombo<Estudiante>(EstudianteDropdownList, repositorio.GetList(x => true), "NombreCompleto", "EstudianteId");
            repositorio.Dispose();

            RepositorioBase<Categoria> repositorioCategorias = new RepositorioBase<Categoria>();
            Utils.LlenarCombo<Categoria>(CategoriaDropDownList, repositorioCategorias.GetList(x => true), "Descripcion", "CategoriaId");
            repositorioCategorias.Dispose();
        }
        private void Limpiar()
        {
            EvaluacionIdTextBox.Text = 0.ToString();
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
            ViewState[KeyViewState] = new Evaluacion();
            ValorTextBox.Text = string.Empty;
            LogradoTextBox.Text = string.Empty;
            LlenarCombo();
            this.BindGrid();
        }
        private void LlenarCampos(Evaluacion evaluaciones)
        {
            Limpiar();
            EvaluacionIdTextBox.Text = evaluaciones.EvaluacionID.ToString();
            FechaTextBox.Text = evaluaciones.Fecha.ToFormatDate();
            EstudianteDropdownList.SelectedValue = evaluaciones.EstudianteId.ToString();
            TotalPerdidoTextBox.Text = evaluaciones.TotalPerdido.ToString();
            ViewState[KeyViewState] = evaluaciones;
            this.BindGrid();
        }
        private Evaluacion LlenaClase()
        {
            Evaluacion evaluaciones = ViewStateEvaluaciones();
            evaluaciones.EvaluacionID = EvaluacionIdTextBox.Text.ToInt();
            evaluaciones.EstudianteId = EstudianteDropdownList.SelectedValue.ToInt();
            evaluaciones.Fecha = FechaTextBox.Text.ToDatetime();
            evaluaciones.TotalPerdido = 0;
            evaluaciones.EvaluacionDetalle.ForEach(x => evaluaciones.TotalPerdido += x.Perdido);
            return evaluaciones;
        }
        private bool Validar()
        {
            bool paso = true;
            if (EstudianteDropdownList.SelectedValue.ToInt() < 0)
                paso = false;
            if (DetalleGridView.Rows.Count <= 0)
                paso = false;
            return paso;
        }
        private void Calcular()
        {
            Evaluacion evaluaciones = ViewStateEvaluaciones();
            decimal TotalPerdido = 0;
            evaluaciones.EvaluacionDetalle.ForEach(x => TotalPerdido += x.Perdido);
            TotalPerdidoTextBox.Text = string.Empty;
            TotalPerdidoTextBox.Text = TotalPerdido.ToString();
        }
        private Evaluacion ViewStateEvaluaciones()
        {
            return (Evaluacion)ViewState[KeyViewState];
        }
        private void BindGrid()
        {
            Calcular();
            Evaluacion evaluaciones = ViewStateEvaluaciones();
            evaluaciones.EvaluacionDetalle.ForEach(x => x.Categoria = new RepositorioBase<Categoria>().Buscar(x.CategoriaId).Descripcion);
            DetalleGridView.DataSource = evaluaciones.EvaluacionDetalle;
            DetalleGridView.DataBind();
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            return !(repositorio.Buscar(EvaluacionIdTextBox.Text.ToInt()).EsNulo());
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            if (!ExisteEnLaBaseDeDatos())
            {
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
            {
                Evaluacion evaluaciones = repositorio.Buscar(EvaluacionIdTextBox.Text.ToInt());
                if (!evaluaciones.EsNulo())
                    LlenarCampos(evaluaciones);
                else
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            repositorio.Dispose();
        }
        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            if (!Validar())
                return;
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            Evaluacion evaluaciones = LlenaClase();
            bool paso = false;
            TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
            TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
            IconType iconType = IconType.error;

            if (evaluaciones.EvaluacionID == 0)
                paso = repositorio.Guardar(evaluaciones);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
                }
                else
                    paso = repositorio.Modificar(evaluaciones);
            }
            if (paso)
            {
                Limpiar();
                tipoTitulo = TipoTitulo.OperacionExitosa;
                tiposMensajes = TiposMensajes.RegistroGuardado;
                iconType = IconType.success;
            }
            Utils.Alerta(this, tipoTitulo, tiposMensajes, iconType);
            repositorio.Dispose();
        }
        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();

            if (!ExisteEnLaBaseDeDatos())
            {
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
            {
                if (repositorio.Eliminar(EvaluacionIdTextBox.Text.ToInt()))
                {
                    Limpiar();
                    Utils.ToastSweet(this, IconType.success, TiposMensajes.RegistroEliminado);
                }
                else
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            repositorio.Dispose();
        }
        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            Evaluacion evaluaciones = ViewStateEvaluaciones();
            decimal Valor = ValorTextBox.Text.ToDecimal();
            decimal Logrado = LogradoTextBox.Text.ToDecimal();
            evaluaciones.AgregarButton(0, evaluaciones.EvaluacionID, CategoriaDropDownList.SelectedValue.ToInt(),
                                        Valor, Logrado, Valor - Logrado);
            ViewState[KeyViewState] = evaluaciones;
            ValorTextBox.Text = string.Empty;
            LogradoTextBox.Text = string.Empty;
            this.BindGrid();
        }

        protected void RemoverDetalleClick_Click(object sender, EventArgs e)
        {
            Evaluacion evaluaciones = ViewStateEvaluaciones();
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            evaluaciones.RemoverDetalle(row.RowIndex);
            ViewState[KeyViewState] = evaluaciones;
            this.BindGrid();
        }
    }
}