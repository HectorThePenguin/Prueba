using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Constantes;
using System.Collections.Generic;
using SuKarne.Controls.Enum;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para PrecioProductoEdicion.xaml
    /// </summary>
    public partial class PrecioProductoEdicion
    {
        #region propiedades
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private PrecioProductoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (PrecioProductoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PrecioProductoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        public PrecioProductoEdicion(PrecioProductoInfo precioProductoInfoSelecionado)
        {
            InitializeComponent();
            precioProductoInfoSelecionado.UsuarioModificaID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = precioProductoInfoSelecionado;
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaProducto.ObjetoNegocio = new ProductoPL();
            skAyudaProducto.IsEnabled = false;
            skAyudaOrganizacion.IsEnabled = false;
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        /// Boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var guardar = validarGuardar();

            if (guardar)
            {
                try
                {
                    var precioProductoPL = new PrecioProductoPL();
                    precioProductoPL.Guardar(Contexto);

                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    
                    if (ucTitulo.TextoTitulo == Properties.Resources.PrecioProducto_Editar_Titulo)
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
                    SkMessageBox.Show(this, Properties.Resources.PrecioProducto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.PrecioProducto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new PrecioProductoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo(),
                PrecioMaximo = 0,
                Producto = new ProductoInfo()
            };
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaProducto.ObjetoNegocio = new ProductoPL();
        }

        /// <summary>
        /// Valida guardar
        /// </summary>
        /// <returns></returns>
        public bool validarGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioProducto_msgOrganizacionRequerido;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (Contexto.PrecioMaximo <= 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioProducto_msgPrecioRequerido;
                    txtPrecio.Focus();
                }
                else if (Contexto.Producto == null || Contexto.Producto.ProductoId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioProducto_msgProductoRequerido;
                    skAyudaProducto.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioProducto_msgEstatusRequerido;
                    cmbActivo.Focus();
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
        #endregion
    }
}
