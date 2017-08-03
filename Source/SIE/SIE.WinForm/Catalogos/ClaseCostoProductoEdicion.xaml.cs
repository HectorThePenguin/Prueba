using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ClaseCostoProductoEdicion.xaml
    /// </summary>
    public partial class ClaseCostoProductoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ClaseCostoProductoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ClaseCostoProductoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ClaseCostoProductoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad ClaseCostoProducto Existente
        /// </summary>
        /// <param name="claseCostoProductoInfo"></param>
        public ClaseCostoProductoEdicion(ClaseCostoProductoInfo claseCostoProductoInfo)
        {
            InitializeComponent();
            claseCostoProductoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = claseCostoProductoInfo;
        }

        #endregion Constructores

        #region Eventos
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            skAyudaAlmacen.ObjetoNegocio = new AlmacenPL();
            skAyudaProducto.ObjetoNegocio = new ProductoPL();
            skAyudaCuentaSAP.ObjetoNegocio = new CuentaSAPPL();

            skAyudaAlmacen.AyudaConDatos += skAyudaAlmacen_AyudaConDatos;
            skAyudaProducto.AyudaConDatos += skAyudaProducto_AyudaConDatos;
        }

        private void skAyudaProducto_AyudaConDatos(object sender, EventArgs e)
        {
            Contexto.Producto.SubfamiliaId = 0;
            Contexto.Producto.FamiliaId = 0;
            Contexto.Producto.UnidadId = 0;
        }

        private void skAyudaAlmacen_AyudaConDatos(object sender, EventArgs e)
        {
            Contexto.Almacen.TipoAlmacenID = 0;
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ClaseCostoProductoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Almacen = new AlmacenInfo
                    {
                        Organizacion = new OrganizacionInfo
                            {
                                OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                            },
                        TipoAlmacen = new TipoAlmacenInfo()
                    },
                Producto = new ProductoInfo(),
                CuentaSAP = new CuentaSAPInfo()
            };
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(txtClaseCostoProductoID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ClaseCostoProductoEdicion_MsgClaseCostoProductoIDRequerida;
                    txtClaseCostoProductoID.Focus();
                }
                else if (Contexto.Almacen == null || Contexto.Almacen.AlmacenID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ClaseCostoProductoEdicion_MsgAlmacenIDRequerida;
                    skAyudaAlmacen.AsignarFoco();
                }
                else if (Contexto.Producto == null || Contexto.Producto.ProductoId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ClaseCostoProductoEdicion_MsgProductoIDRequerida;
                    skAyudaProducto.AsignarFoco();
                }
                else if (Contexto.CuentaSAP == null || Contexto.CuentaSAP.CuentaSAPID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ClaseCostoProductoEdicion_MsgCuentaSAPIDRequerida;
                    skAyudaCuentaSAP.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ClaseCostoProductoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    var claseCostoProductoBL = new ClaseCostoProductoBL();
                    IList<ClaseCostoProductoInfo> claseCostoProductoAlmacen =
                        claseCostoProductoBL.ObtenerPorAlmacen(Contexto.Almacen.AlmacenID);

                    ClaseCostoProductoInfo claseCostoProducto =
                        claseCostoProductoAlmacen.FirstOrDefault(
                            clase =>
                            clase.Producto.ProductoId == Contexto.Producto.ProductoId);

                    if (claseCostoProducto != null && (Contexto.ClaseCostoProductoID == 0 || Contexto.ClaseCostoProductoID != claseCostoProducto.ClaseCostoProductoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.ClaseCostoProductoEdicion_MsgRegistroDuplicado, claseCostoProducto.ClaseCostoProductoID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }


        /// <summary>
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var claseCostoProductoBL = new ClaseCostoProductoBL();
                    claseCostoProductoBL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.ClaseCostoProductoID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.ClaseCostoProducto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.ClaseCostoProducto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        #endregion Métodos

    }
}

