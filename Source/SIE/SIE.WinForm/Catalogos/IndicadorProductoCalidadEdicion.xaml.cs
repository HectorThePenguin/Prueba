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
    /// Interaction logic for IndicadorProductoCalidadEdicion.xaml
    /// </summary>
    public partial class IndicadorProductoCalidadEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private IndicadorProductoCalidadInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (IndicadorProductoCalidadInfo) DataContext;
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
        public IndicadorProductoCalidadEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad IndicadorProductoCalidad Existente
        /// </summary>
        /// <param name="indicadorProductoCalidadInfo"></param>
        public IndicadorProductoCalidadEdicion(IndicadorProductoCalidadInfo indicadorProductoCalidadInfo)
        {
           InitializeComponent();
           indicadorProductoCalidadInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = indicadorProductoCalidadInfo;
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
            skAyudaProducto.ObjetoNegocio = new ProductoPL();
            skAyudaIndicador.ObjetoNegocio = new IndicadorBL();
            skAyudaIndicador.AsignarFoco();
            skAyudaProducto.AyudaConDatos += (o, args) =>
                                                 {
                                                     Contexto.Producto.UnidadId = 0;
                                                     Contexto.Producto.SubfamiliaId = 0;
                                                     Contexto.Producto.FamiliaId = 0;
                                                 };
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
            Contexto = new IndicadorProductoCalidadInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Producto = new ProductoInfo(),
                Indicador = new IndicadorInfo()
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
                if (Contexto.Indicador == null || Contexto.Indicador.IndicadorId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoCalidadEdicion_MsgIndicadorIDRequerida;
                    skAyudaIndicador.AsignarFoco();
                }
                else if (Contexto.Producto == null || Contexto.Producto.ProductoId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoCalidadEdicion_MsgProductoIDRequerida;
                    skAyudaProducto.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoCalidadEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int indicadorProductoCalidadId = Contexto.IndicadorProductoCalidadID;

                    var indicadorProductoCalidadPL = new IndicadorProductoCalidadBL();
                    IndicadorProductoCalidadInfo indicadorProductoCalidad =
                        indicadorProductoCalidadPL.ObtenerPorIndicadorProducto(Contexto);
                    if (indicadorProductoCalidad != null 
                            && (indicadorProductoCalidadId == 0 || indicadorProductoCalidadId != indicadorProductoCalidad.IndicadorProductoCalidadID))
                    {
                        resultado = false;
                        mensaje =
                            string.Format(Properties.Resources.IndicadorProductoCalidadEdicion_MsgDescripcionExistente,
                                          indicadorProductoCalidad.IndicadorProductoCalidadID);
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
                    var indicadorProductoCalidadPL = new IndicadorProductoCalidadBL();
                    indicadorProductoCalidadPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.IndicadorProductoCalidadID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.IndicadorProductoCalidad_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.IndicadorProductoCalidad_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos

    }
}

