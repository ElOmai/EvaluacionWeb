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
    public partial class rEstudiante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
            }
        }
        private void Limpiar()
        {
            EstudianteIdTextBox.Text = 0.ToString();
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
            NombreTextBox.Text = string.Empty;
            ApellidoTextBox.Text = string.Empty;
            PuntosTotalesPerdidosTextBox.Text = 0.ToString();
        }
        private void LlenaCampos(Estudiante estudiantes)
        {
            EstudianteIdTextBox.Text = estudiantes.EstudianteId.ToString();
            FechaTextBox.Text = estudiantes.Fecha.ToFormatDate();
            NombreTextBox.Text = estudiantes.Nombre;
            ApellidoTextBox.Text = estudiantes.Apellido;
            PuntosTotalesPerdidosTextBox.Text = estudiantes.PuntosPerdidos.ToString();
        }
        private Estudiante LlenaClase()
        {
            return new Estudiante(EstudianteIdTextBox.Text.ToInt(),
                NombreTextBox.Text,
                ApellidoTextBox.Text,
                PuntosTotalesPerdidosTextBox.Text.ToDecimal(),
                FechaTextBox.Text.ToDatetime());
        }
        private bool Validar()
        {
            bool paso = true;
            if (string.IsNullOrWhiteSpace(NombreTextBox.Text))
                paso = false;
            if (string.IsNullOrWhiteSpace(ApellidoTextBox.Text))
                paso = false;

            return paso;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Estudiante> repositorio = new RepositorioBase<Estudiante>();
            return !(repositorio.Buscar(EstudianteIdTextBox.Text.ToInt()).EsNulo());
        }
        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            RepositorioBase<Estudiante> repositorio = new RepositorioBase<Estudiante>();
            if (!ExisteEnLaBaseDeDatos())
            {
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
            {
                Estudiante estudiantes = repositorio.Buscar(EstudianteIdTextBox.Text.ToInt());
                if (!estudiantes.EsNulo())
                    LlenaCampos(estudiantes);
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
            RepositorioBase<Estudiante> repositorio = new RepositorioBase<Estudiante>();
            Estudiante estudiantes = LlenaClase();
            bool paso = false;
            TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
            TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
            IconType iconType = IconType.error;

            if (estudiantes.EstudianteId == 0)
                paso = repositorio.Guardar(estudiantes);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
                }
                else
                    paso = repositorio.Modificar(estudiantes);
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
            RepositorioBase<Estudiante> repositorio = new RepositorioBase<Estudiante>();

            if (!ExisteEnLaBaseDeDatos())
            {
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
            {
                if (repositorio.Eliminar(EstudianteIdTextBox.Text.ToInt()))
                {
                    Utils.ToastSweet(this, IconType.success, TiposMensajes.RegistroEliminado);
                }
            }
            repositorio.Dispose();
        }
    }
}