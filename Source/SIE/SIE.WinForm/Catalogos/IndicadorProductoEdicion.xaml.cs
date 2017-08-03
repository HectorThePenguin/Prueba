using System;
using System.ComponentModel;
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
    /// Interaction logic for IndicadorProductoEdicion.xaml
    /// </summary>
    public partial class IndicadorProductoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private IndicadorProductoInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (IndicadorProductoInfo) DataContext;
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
        public IndicadorProductoEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad IndicadorProducto Existente
        /// </summary>
        /// <param name="indicadorProductoInfo"></param>
        public IndicadorProductoEdicion(IndicadorProductoInfo indicadorProductoInfo)
        {
           InitializeComponent();
           indicadorProductoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = indicadorProductoInfo;
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
            skAyudaIndicador.ObjetoNegocio = new IndicadorBL();
            skAyudaProducto.ObjetoNegocio = new ProductoPL();
            skAyudaProducto.AyudaConDatos += (o, args) =>
                                                 {
                                                     Contexto.Producto.UnidadId = 0;
                                                     Contexto.Producto.FamiliaId = 0;
                                                     Contexto.Producto.SubfamiliaId = 0;
                                                 };
            skAyudaIndicador.AsignarFoco();
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
            Contexto = new IndicadorProductoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Producto = new ProductoInfo(),
                IndicadorInfo = new IndicadorInfo()
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
                if (Contexto.IndicadorInfo == null || Contexto.IndicadorInfo.IndicadorId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoEdicion_MsgIndicadorIDRequerida;
                    skAyudaIndicador.Focus();
                }
                else if (Contexto.Producto == null || Contexto.Producto.ProductoId == 0 )
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoEdicion_MsgProductoIDRequerida;
                    skAyudaProducto.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int indicadorProductoId = Contexto.IndicadorProductoId;

                    var indicadorProductoPL = new IndicadorProductoPL();
                    IndicadorProductoInfo indicadorProducto =
                        indicadorProductoPL.ObtenerPorIndicadorProducto(Contexto);
                    if (indicadorProducto != null 
                            && (indicadorProductoId == 0 || indicadorProductoId != indicadorProducto.IndicadorProductoId))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.IndicadorProductoEdicion_MsgDescripcionExistente, indicadorProducto.IndicadorProductoId);
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
                    var indicadorProductoPL = new IndicadorProductoPL();
                    indicadorProductoPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.IndicadorProductoId != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                        skAyudaIndicador.AsignarFoco();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.IndicadorProducto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.IndicadorProducto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos

    }
}

