using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para ProgramacionMateriaPrima.xaml
    /// </summary>
    public partial class ProgramacionMateriaPrima
    {
        private PedidoInfo pedido;
        SKAyuda<PedidoInfo> skAyudaPedidos;
        private int organizacionID;
        public ProgramacionMateriaPrima()
        {
            organizacionID = int.Parse(Application.Current.Properties["OrganizacionID"].ToString());
            InitializeComponent();
        }
        #region Eventos
        
            /// <summary>
            /// Guarda la programacion capturada
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnGuardar_Click(object sender, RoutedEventArgs e)
            {
                Guardar();
            }


            /// <summary>
            /// Cancela la operación
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnCancelar_Click(object sender, RoutedEventArgs e)
            {
                Cancelar();
            }

            /// <summary>
            /// Edita el producto seleccionado
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void BtnEditar_Click(object sender, RoutedEventArgs e)
            {
                EditarProducto();
            }

            private void ProgramacionMateriaPrima_OnLoaded(object sender, RoutedEventArgs e)
            {
                InicializarDatos();
            }
            
        #endregion

        #region Metodos
            /// <summary>
            /// Valida si el usuario quiere salir de la ventana.
            /// </summary>
            private void Cancelar()
            {
                try
                {
                    if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ProgramacionMateriaPrima_MsgCancelar,
                       MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                    {
                        InicializarDatos();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                }
            }

            /// <summary>
            /// Inicializa los valores en la pantalla para una captura nueva.
            /// </summary>
            private void InicializarDatos()
            {
                txtObservaciones.Text = string.Empty;
                dgProductos.ItemsSource = null;
                pedido = null;
                AyudaBuscarFolios();
                skAyudaPedidos.AsignarFoco();
            }

            /// <summary>
            /// Inicializa el grid con la lista del detalle del pedido
            /// </summary>
            private void InicializarGrid()
            {
                dgProductos.ItemsSource = null;
                if (pedido != null && (pedido.DetallePedido != null && pedido.DetallePedido.Count > 0) )
                {
                    dgProductos.ItemsSource = pedido.DetallePedido;
                }
                
            }

            /// <summary>
            /// Muestra la pantalla para buscar un folio.
            /// </summary>
            private void AyudaBuscarFolios()
            {
                skAyudaPedidos = new SKAyuda<PedidoInfo>(0, false, new PedidoInfo
                                        {
                                            FolioPedido = 0,
                                            Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                                            EstatusPedido = new EstatusInfo{EstatusId = (int)Estatus.PedidoSolicitado},
                                            Activo = EstatusEnum.Activo
                                        }
                                        , "FolioPedidoBusqueda"
                                        , "PropiedadDescripcionOrganizacion", true, 100, true)
                {
                    AyudaPL = new PedidosPL(),
                    MensajeClaveInexistente = Properties.Resources.ProgramacionMateriaPrima_FolioInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.ProgramacionMateriaPrima_PedidoSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.ProgramacionMateriaPrima_Busqueda,
                    MensajeAgregar = Properties.Resources.ProgramacionMateriaPrima_Seleccionar,
                    TituloEtiqueta = Properties.Resources.ProgramacionMateriaPrima_lblFolio,
                    TituloPantalla = Properties.Resources.BusquedaPedido_Titulo
                };
                skAyudaPedidos.ObtenerDatos += ObtenerDatosPedido;
                skAyudaPedidos.LlamadaMetodosNoExistenDatos += LimpiarTodoPedido;
                skAyudaPedidos.AsignaTabIndex(0);
                SplAyudaPedidos.Children.Clear();
                SplAyudaPedidos.Children.Add(skAyudaPedidos);
            }

            private void LimpiarTodoPedido()
            {
                try
                {
                    skAyudaPedidos.LimpiarCampos();
                    skAyudaPedidos.Info = new PedidoInfo
                    {
                        FolioPedido = 0,
                        Organizacion = new OrganizacionInfo {OrganizacionID = organizacionID},
                        EstatusPedido = new EstatusInfo {EstatusId = (int) Estatus.PedidoSolicitado},
                        Activo = EstatusEnum.Activo
                    };
                    InicializarGrid();
                    pedido = skAyudaPedidos.Info;
                    skAyudaPedidos.LimpiarCampos();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }

            /// <summary>
            /// Obtiene los datos del pedido que el usuario selecciono en la ayuda.
            /// </summary>
            /// <param name="clave"></param>
            private void ObtenerDatosPedido(String clave)
            {
                pedido = skAyudaPedidos.Info;

                skAyudaPedidos.Info = new PedidoInfo
                {
                    FolioPedido = 0,
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                    EstatusPedido = new EstatusInfo { EstatusId = (int)Estatus.PedidoSolicitado },
                    Activo = EstatusEnum.Activo
                };

                if (pedido != null)
                {
                    if (pedido.EstatusPedido.EstatusId == (int)Estatus.PedidoSolicitado)
                    {
                        if (pedido.DetallePedido != null && pedido.DetallePedido.Count > 0)
                        {
                            InicializarGrid();
                        }
                        else
                        {
                            InicializarDatos();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ProgramacionMateriaPrima_PedidoSinDetalle,
                                MessageBoxButton.OK, MessageImage.Warning);
                            skAyudaPedidos.Clave = "";
                            skAyudaPedidos.AsignarFoco();
                        }
                    }
                    else
                    {
                        pedido = null;
                        InicializarGrid();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ProgramacionMateriaPrima_FolioInvalido,
                                MessageBoxButton.OK, MessageImage.Warning);
                        skAyudaPedidos.Clave = "";
                        skAyudaPedidos.AsignarFoco();
                    }
                }
                else
                {
                    pedido = null;
                    InicializarGrid();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ProgramacionMateriaPrima_FolioInvalido,
                            MessageBoxButton.OK, MessageImage.Warning);
                    skAyudaPedidos.Clave = "";
                    skAyudaPedidos.AsignarFoco();
                }
            }
            
            /// <summary>
            /// Edita el producto seleccionado del grid
            /// </summary>
            private void EditarProducto()
            {
                var detalle = (PedidoDetalleInfo)dgProductos.SelectedItem;
                if (detalle != null)
                {
                   var edicion = new EdicionProgramacionMateriaPrimaDialogo(pedido,detalle);
                   MostrarCentrado(edicion);
                   InicializarGrid();
                }
            }

            /// <summary>
            /// Valida que los datos obligatorios esten capturados.
            /// </summary>
            /// <returns></returns>
            private bool ValidaDatos()
            {
                bool resultado = true;

                if (pedido.FolioPedido == 0 || txtObservaciones.Text.Equals(string.Empty) )
                {
                    resultado = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ProgramacionMateriaPrima_CamposObligatorios,
                       MessageBoxButton.OK, MessageImage.Warning);
                }
                else if (!ValidaCantidadProgramada())
                {
                    resultado = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                          Properties.Resources.ProgramacionMateriaPrima_MsgProductosPendientesProgramar,
                          MessageBoxButton.OK, MessageImage.Warning);
                }

                return resultado;
            }
            

            /// <summary>
            /// Almacena todas las programaciones realizadas
            /// </summary>
            private void Guardar()
            {
                if (ValidaDatos())
                {
                    if (ExisteAutorizacion())
                    {
                        var programacionBl = new ProgramacionMateriaPrimaPL();

                        try
                        {
                            var listaProgramacion = ObtenerProgramacion(pedido.DetallePedido);
                            pedido.EstatusPedido.EstatusId = (int)Estatus.PedidoProgramado;
                            pedido.UsuarioModificacion = new UsuarioInfo { UsuarioID = int.Parse(Application.Current.Properties["UsuarioID"].ToString()) };

                            bool resultado = programacionBl.GuardarProgramacionMateriaPrima(pedido, listaProgramacion);

                            if (resultado)
                            {
                                InicializarDatos();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                   Properties.Resources.ProgramacionMateriaPrima_MsgGuardarExito,
                                   MessageBoxButton.OK, MessageImage.Correct);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.ProgramacionMateriaPrima_ErrorInterno,
                             MessageBoxButton.OK, MessageImage.Error);
                        }
                    }
                }
            }

        /// <summary>
        /// Obtiene todas las programaciones realizadas en los diferentes productos.
        /// </summary>
        /// <param name="pedidoDetalle"></param>
        /// <returns></returns>
        private List<ProgramacionMateriaPrimaInfo> ObtenerProgramacion(List<PedidoDetalleInfo> pedidoDetalle )
        {
            List<ProgramacionMateriaPrimaInfo> listaProgramacion = null;

            if (pedidoDetalle != null && pedidoDetalle.Count > 0)
            {
                listaProgramacion = new List<ProgramacionMateriaPrimaInfo>();
                foreach (var detalle in pedidoDetalle)
                {
                    foreach (var programacion in detalle.ProgramacionMateriaPrima)
                    {
                        //if (programacion.ProgramacionMateriaPrimaId == 0)
                        //{
                            programacion.Observaciones = txtObservaciones.Text;
                            listaProgramacion.Add(programacion);
                        //}
                    }
                }         
            }

            return listaProgramacion;
        }


        /// <summary>
        /// Valida que todos los productos tengan la cantidad programada igual a la solicitada.
        /// </summary>
        /// <returns></returns>
        private bool ValidaCantidadProgramada()
        {
            foreach (var detallePedido in dgProductos.ItemsSource)
            {
                if (detallePedido is PedidoDetalleInfo)
                {
                    var detalle = (PedidoDetalleInfo)detallePedido;
                    if (detalle.CantidadSolicitada != detalle.TotalCantidadProgramada)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// Existe autorizacion para folio seleccionado
        /// </summary>
        /// <returns></returns>
        private bool ExisteAutorizacion()
        {
            bool regreso = false;
            try
            {
                var solicitudAutorizacionPl = new SolicitudAutorizacionPL();
                var autorizacionInfo = new AutorizacionMateriaPrimaInfo
                {
                    OrganizacionID = organizacionID,
                    TipoAutorizacionID = TipoAutorizacionEnum.UsoLote.GetHashCode(),
                    Folio = pedido.FolioPedido
                };
                AutorizacionMateriaPrimaInfo resultado = solicitudAutorizacionPl.ObtenerSolicitudAutorizacion(autorizacionInfo);
                if (resultado != null)
                {
                    if (resultado.EstatusID == Estatus.AMPAutoriz.GetHashCode())
                    {
                        regreso = true;
                    }
                    else if (resultado.EstatusID == Estatus.AMPPendien.GetHashCode())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      string.Format(Properties.Resources.SolicitudProgramacionMateriaPrima_MsjSolicitudPendiente,
                                                    resultado.Folio), MessageBoxButton.OK,
                                      MessageImage.Warning);
                    }
                    else if (resultado.EstatusID == Estatus.AMPRechaza.GetHashCode())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      string.Format(Properties.Resources.SolicitudProgramacionMateriaPrima_MsjSolicitudRechazado,
                                                    resultado.Lote, resultado.Folio), MessageBoxButton.OK,
                                      MessageImage.Warning);
                    }
                }
                else
                {
                    regreso = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.SolicitudProgramacionMateriaPrima_errorEnviarSolicitud,
                       MessageBoxButton.OK, MessageImage.Error);
            }
            return regreso;
        }
        #endregion
    }
}
