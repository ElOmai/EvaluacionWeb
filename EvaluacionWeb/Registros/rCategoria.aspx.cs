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
    public partial class rCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
            }
        }
        public void Limpiar()
        {
            CategoriaIdTextBox.Text = 0.ToString();
            FechaTextBox.Text = DateTime.Now.ToFormatDate();
            DescripcionTextBox.Text = string.Empty;
        }
        public Categoria LlenarClase()
        {
            return new Categoria(CategoriaIdTextBox.Text.ToInt(),
                DescripcionTextBox.Text,
                0,
                FechaTextBox.Text.ToDatetime());
        }
        public void LlenaCampos(Categoria categorias)
        {
            CategoriaIdTextBox.Text = categorias.CategoriaId.ToString();
            FechaTextBox.Text = categorias.Fecha.ToFormatDate();
            DescripcionTextBox.Text = categorias.Descripcion;
        }
        public bool Validar()
        {
            bool paso = true;
            if (string.IsNullOrWhiteSpace(DescripcionTextBox.Text))
                paso = false;
            return paso;

        }
        public bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Categoria> repositorio = new RepositorioBase<Categoria>();
            return !(repositorio.Buscar(CategoriaIdTextBox.Text.ToInt()).EsNulo());
        }
        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            if (!Validar())
                return;
            RepositorioBase<Categoria> repositorio = new RepositorioBase<Categoria>();
            Categoria categorias = LlenarClase();
            bool paso = false;
            TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
            TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
            IconType iconType = IconType.error;

            if (categorias.CategoriaId == 0)
                paso = repositorio.Guardar(categorias);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
                }
                else
                    paso = repositorio.Modificar(categorias);
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
            RepositorioBase<Categoria> repositorio = new RepositorioBase<Categoria>();

            if (!ExisteEnLaBaseDeDatos())
            {
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
            {
                if (repositorio.Eliminar(CategoriaIdTextBox.Text.ToInt()))
                {
                    Utils.ToastSweet(this, IconType.success, TiposMensajes.RegistroEliminado);
                }
            }
            repositorio.Dispose();
        }

        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            RepositorioBase<Categoria> repositorio = new RepositorioBase<Categoria>();
            if (!ExisteEnLaBaseDeDatos())
            {
                Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            else
            {
                Categoria categorias = repositorio.Buscar(CategoriaIdTextBox.Text.ToInt());
                if (!categorias.EsNulo())
                    LlenaCampos(categorias);
                else
                    Utils.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            repositorio.Dispose();
        }
    }
}