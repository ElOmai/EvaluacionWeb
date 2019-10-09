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
                LlenarCombo();
            }
        }
        private void LlenarCombo()
        {
            RepositorioBase<Estudiante> repositorio = new RepositorioBase<Estudiante>();
            Utils.LlenarCombo<Estudiante>(EstudianteDropdownList, repositorio.GetList(x => true), "NombreCompleto", "EstudianteId");
            repositorio.Dispose();

            RepositorioBase<Categoria> repositorioCategoria = new RepositorioBase<Categoria>();
            Utils.LlenarCombo<Categoria>(CategoriaDropDownList, repositorioCategoria.GetList(x => true), "Descripcion", "CategoriaId");
            repositorioCategoria.Dispose();
        }
        private void Limpiar()
        {
            EvaluacionIdTextBox.Text = 0.ToString();
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
            ViewState[KeyViewState] = new Evaluacion();
            LlenarCombo();
            this.BindGrid();
        }
        private void LlenaCampo(Evaluacion evaluaciones)
        {
            Limpiar();
            EvaluacionIdTextBox.Text = evaluaciones.EvaluacionID.ToString();
            FechaTextBox.Text = evaluaciones.Fecha.ToFormatDate();
            EstudianteDropdownList.SelectedValue = evaluaciones.EstudianteId.ToString();
            ViewState[KeyViewState] = evaluaciones;
            this.BindGrid();
        }
        private Evaluacion LlenaClase()
        {
            Evaluacion evaluaciones = ViewStateEvaluaciones();
            evaluaciones.EvaluacionID = EvaluacionIdTextBox.Text.ToInt();
            evaluaciones.EstudianteId = EstudianteDropdownList.SelectedValue.ToInt();
            evaluaciones.Fecha = FechaTextBox.Text.ToDatetime();
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
        private Evaluacion ViewStateEvaluaciones()
        {
            return (Evaluacion)ViewState[KeyViewState];
        }
        private void BindGrid()
        {
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
        protected void BuscarButton_ServerClick(object sender, EventArgs e)
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
                    LlenaCampo(evaluaciones);
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
                    Utils.ToastSweet(this, IconType.success, TiposMensajes.RegistroEliminado);
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