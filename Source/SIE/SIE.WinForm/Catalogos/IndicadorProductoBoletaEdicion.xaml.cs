using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Enums;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for IndicadorProductoBoletaEdicion.xaml
    /// </summary>
    public partial class IndicadorProductoBoletaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private IndicadorProductoBoletaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (IndicadorProductoBoletaInfo) DataContext;
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
        public IndicadorProductoBoletaEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad IndicadorProductoBoleta Existente
        /// </summary>
        /// <param name="indicadorProductoBoletaInfo"></param>
        public IndicadorProductoBoletaEdicion(IndicadorProductoBoletaInfo indicadorProductoBoletaInfo)
        {
           InitializeComponent();
           indicadorProductoBoletaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = indicadorProductoBoletaInfo;
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
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            
            skAyudaIndicador.AyudaConDatos += (o, args) =>
                                                  {
                                                      Contexto.IndicadorProducto.Producto.IndicadorID =
                                                          Contexto.IndicadorProducto.IndicadorInfo.IndicadorId;
                                                  };
            skAyudaIndicador.AyudaLimpia += (o, args) =>
                                                {
                                                    Contexto.IndicadorProducto.Producto = new ProductoInfo
                                                                                              {
                                                                                                  Familia = new FamiliaInfo
                                                                                                                {
                                                                                                                    FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()
                                                                                                                },
                                                                                              };
                                                };

            skAyudaProducto.PuedeBuscar += () =>
                                               {
                                                   bool puedeBuscar =
                                                       Contexto.IndicadorProducto.IndicadorInfo.IndicadorId > 0;
                                                   if (!puedeBuscar)
                                                   {
                                                       skAyudaIndicador.AsignarFoco();
                                                   }
                                                   return puedeBuscar;
                                               };
            skAyudaProducto.AyudaConDatos += (o, args) => ObtenerIndicadorProducto();

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

        private void RangoMinimoKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void RangoMaximoKeyDown(object sender, KeyEventArgs e)
        {

        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new IndicadorProductoBoletaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                IndicadorProducto = new IndicadorProductoInfo
                                        {
                                            IndicadorInfo = new IndicadorInfo(),
                                            Producto = new ProductoInfo
                                                           {
                                                               Familia = new FamiliaInfo
                                                                             {
                                                                                 FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()
                                                                             }
                                                           },
                                        },
                Organizacion = new OrganizacionInfo()
            };
            skAyudaIndicador.AsignarFoco();
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
                if (Contexto.IndicadorProducto.IndicadorInfo.IndicadorId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoBoletaEdicion_MsgIndicadorProductoBoletaIDRequerida;
                    skAyudaIndicador.AsignarFoco();
                }
                else if (Contexto.IndicadorProducto.Producto.ProductoId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoBoletaEdicion_MsgIndicadorProductoIDRequerida;
                    skAyudaProducto.AsignarFoco();
                }
                else if (Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoBoletaEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                //else if (!dtuRangoMinimo.Value.HasValue || Contexto.RangoMinimo == 0)
                //{
                //    resultado = false;
                //    mensaje = Properties.Resources.IndicadorProductoBoletaEdicion_MsgRangoMinimoRequerida;
                //    dtuRangoMinimo.Focus();
                //}
                //else if (!dtuRangoMaximo.Value.HasValue || Contexto.RangoMaximo == 0)
                //{
                //    resultado = false;
                //    mensaje = Properties.Resources.IndicadorProductoBoletaEdicion_MsgRangoMaximoRequerida;
                //    dtuRangoMaximo.Focus();
                //}
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorProductoBoletaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int indicadorProductoBoletaId = Contexto.IndicadorProductoBoletaID;
                    var indicadorProductoBoletaBL = new IndicadorProductoBoletaBL();
                    IndicadorProductoBoletaInfo indicadorProductoBoleta =
                        indicadorProductoBoletaBL.ObtenerPorIndicadorProductoOrganizacion(
                            Contexto.IndicadorProducto.IndicadorProductoId, Contexto.Organizacion.OrganizacionID);
                    if (indicadorProductoBoleta != null && (indicadorProductoBoletaId == 0 || indicadorProductoBoletaId != indicadorProductoBoleta.IndicadorProductoBoletaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.IndicadorProductoBoletaEdicion_MsgDescripcionExistente, indicadorProductoBoleta.IndicadorProductoBoletaID);
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
                    var indicadorProductoBoletaBL = new IndicadorProductoBoletaBL();
                    indicadorProductoBoletaBL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (Contexto.IndicadorProductoBoletaID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.IndicadorProductoBoleta_ErrorGuardar,
                                      MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.IndicadorProductoBoleta_ErrorGuardar,
                                      MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        private void ObtenerIndicadorProducto()
        {
            try
            {
                var indicadorProductoPL = new IndicadorProductoPL();
                IndicadorProductoInfo indicadorProducto =
                    indicadorProductoPL.ObtenerPorIndicadorProducto(Contexto.IndicadorProducto);
                if (indicadorProducto != null)
                {
                    Contexto.IndicadorProducto.IndicadorProductoId = indicadorProducto.IndicadorProductoId;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.IndicadorProductoBoleta_ErrorGuardar,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion Métodos
    }
}
