using System.Collections.Generic;
using System.Web.UI;

namespace EvaluacionWeb
{
    public class Utils
    {
        public static void Alerta(System.Web.UI.Page page, TipoTitulo Titulo, TiposMensajes Mensaje, IconType iconType)
        {
            string TituloDescripcion = Titulo.GetDescription();
            string MensajeDescripcion = Mensaje.GetDescription();
            string iconTypeDescripcion = iconType.ToString();
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert",
                            $"sweetalert('{TituloDescripcion}','{MensajeDescripcion}','{iconTypeDescripcion}')", true);
        }
        public static void ToastSweet(System.Web.UI.Page page, IconType iconType, TiposMensajes Mensaje)
        {
            string IconTypeDescripcion = iconType.ToString();
            string MensajeDescripcion = Mensaje.GetDescription();
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert",
                            $"ToastSweetAlert('{IconTypeDescripcion}','{MensajeDescripcion}')", true);
        }
        public static void MostrarModal(System.Web.UI.Page page, string NombreModal, string Titulo)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Popup", $"{NombreModal}('{ Titulo }');", true);
        }
        public static void LlenarCombo<T>(System.Web.UI.WebControls.DropDownList dropDownList, List<T> lista, string DataTextField, string DataValueField)
        {
            dropDownList.Items.Clear();
            dropDownList.DataSource = lista;
            dropDownList.DataTextField = DataTextField;
            dropDownList.DataValueField = DataValueField;
            dropDownList.DataBind();
        }
    }
}
