using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para CrearProgramacionMateriaPrimaDialogo.xaml
    /// </summary>
    public partial class CrearProgramacionMateriaPrimaDialogo
    {
        #region Atributos

        private PedidoInfo pedido;
        private PedidoDetalleInfo detallePedido;
        private ProductoInfo producto;
        private AlmacenInventarioLoteInfo almacenInventarioLote;
        private bool SalidaNormal = false;
        public ProgramacionMateriaPrimaInfo Programacion { get; set; }
        public SKAyuda<AlmacenInventarioLoteInfo> skAyudaLote;
        private int organizacionID ;
        private bool Autorizado;
        private int loteEnUso;
        #endregion Atributos

        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pedidoInfo"></param>
        /// <param name="detalleInfo"></param>
        public CrearProgramacionMateriaPrimaDialogo(PedidoInfo pedidoInfo, PedidoDetalleInfo detalleInfo)
        {
            organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            Autorizado = false;
            pedido = pedidoInfo;
            detallePedido = detalleInfo;
            producto = detallePedido.Producto;
            InitializeComponent();
        }
        

        #endregion


        #region Eventos

        /// <summary>
        /// Evento al salir de la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!SalidaNormal)
            {
                e.Cancel = Cancelar();
            }
        }

        private void VistaBase_Loaded_1(object sender, RoutedEventArgs e)
        {
            CargarAyudaLote();
            ExisteSolicitudAutorizada();
            InicializarDatos();
            txtCantidadProgramada.Focus();
        }

        /// <summary>
        /// Guardara la programación realizada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento al presionar el boton de cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Verifica que los caracteres permitidos sean numeros.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCantidadProgramada_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        #endregion Eventos


        #region Metodos

        /// <summary>
        /// Existe solicitud autorizada para folio seleccionado
        /// </summary>
        private void ExisteSolicitudAutorizada()
        {
            bool loteProgramacion = false;
            var solicitudAutorizacionPl = new SolicitudAutorizacionPL();
            var autorizacionInfo = new AutorizacionMateriaPrimaInfo
            {
                OrganizacionID = organizacionID,
                TipoAutorizacionID = TipoAutorizacionEnum.UsoLote.GetHashCode(),
                Folio = pedido.FolioPedido,
                EstatusID = Estatus.AMPAutoriz.GetHashCode()
            };
            AutorizacionMateriaPrimaInfo resultado =
                solicitudAutorizacionPl.ObtenerDatosSolicitudAutorizada(autorizacionInfo);
            if (resultado != null)
            {
                if (detallePedido.ProgramacionMateriaPrima != null && detallePedido.ProgramacionMateriaPrima.Count > 0)
                {
                    loteProgramacion =
                        detallePedido.ProgramacionMateriaPrima.Any(
                            programacionMateriaPrimaInfo =>
                            programacionMateriaPrimaInfo.InventarioLoteOrigen.Lote == resultado.Lote);
                }
                if (!loteProgramacion)
                {

                    skAyudaLote.Clave = resultado.Lote.ToString(CultureInfo.InvariantCulture);
                    skAyudaLote.Descripcion = resultado.Lote.ToString(CultureInfo.InvariantCulture);
                    txtCantidadProgramada.Text = Convert.ToString(resultado.CantidadProgramada);
                    var almacenInventarioLoteInfo = new AlmacenInventarioLoteInfo
                    {
                        ProductoId = producto.ProductoId,
                        OrganizacionId = pedido.Organizacion.OrganizacionID,
                        TipoAlmacenId = (int)TipoAlmacenEnum.MateriasPrimas,
                        Activo = EstatusEnum.Activo,
                        Lote = resultado.Lote
                    };
                    var almacenInventarioLotePL = new AlmacenInventarioLotePL();
                    AlmacenInventarioLoteInfo resultadoInfo =
                        almacenInventarioLotePL.ObtenerAlmacenInventarioLotePorFolio(almacenInventarioLoteInfo);
                    almacenInventarioLote = resultadoInfo;
                    almacenInventarioLote.ProductoId = almacenInventarioLoteInfo.ProductoId;
                    almacenInventarioLote.OrganizacionId = almacenInventarioLoteInfo.OrganizacionId;
                    almacenInventarioLote.TipoAlmacenId = almacenInventarioLoteInfo.TipoAlmacenId;
                    almacenInventarioLote.Activo = almacenInventarioLoteInfo.Activo;
                    almacenInventarioLote.Lote = almacenInventarioLoteInfo.Lote;
                    skAyudaLote.Info = resultadoInfo;
                }
            }
        }

        /// <summary>
        /// Evalua si el usuario desea cancelar la operacion y si quiere salir del proceso.
        /// </summary>
        /// <returns></returns>
        private static bool Cancelar()
        {
            bool resultado;
            try
            {
                resultado = (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearProgramacionMateriaPrimaDialogo_MsgCancelar,
                    MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.No);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Inicializa los datos en pantalla
        /// </summary>
        private void InicializarDatos()
        {
            txtNombreProducto.Text = producto.ProductoDescripcion;
            txtIdProducto.Text = producto.ProductoId.ToString(CultureInfo.InvariantCulture);
            if (Programacion != null)
            {
                txtCantidadProgramada.Value = (int?)Programacion.CantidadProgramada;
                if (Programacion.InventarioLoteOrigen != null)
                {
                    Programacion.InventarioLoteOrigen.ProductoId = producto.ProductoId;
                    Programacion.InventarioLoteOrigen.OrganizacionId = pedido.Organizacion.OrganizacionID;

                    almacenInventarioLote = Programacion.InventarioLoteOrigen;
                    skAyudaLote.Info = Programacion.InventarioLoteOrigen;
                    skAyudaLote.Clave = Programacion.InventarioLoteOrigen.Lote.ToString();
                    skAyudaLote.Descripcion = Programacion.InventarioLoteOrigen.Lote.ToString();
                    skAyudaLote.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Asigna la programacion realizada a la lista de programacion que tiene el detalle del pedido
        /// </summary>
        private bool AsignarProgramacion()
        {
            bool resultado = true;
            try
            {
                if (!skAyudaLote.Clave.Equals(string.Empty) && !txtCantidadProgramada.Text.Equals(string.Empty))
                {
                    if (Programacion != null)
                    { // Modificamos la programacion que se edita.

                        var cantidad = decimal.Parse(txtCantidadProgramada.Value.ToString());
                        var loteId = Programacion.InventarioLoteOrigen.AlmacenInventarioLoteId;
                        Programacion.UsuarioModificacion = new UsuarioInfo { UsuarioID = int.Parse(Application.Current.Properties["UsuarioID"].ToString()) };

                        if (cantidad != Programacion.CantidadProgramada)
                        {
                            Programacion.CantidadProgramada = decimal.Parse(txtCantidadProgramada.Value.ToString());
                        }

                        if (loteId != almacenInventarioLote.AlmacenInventarioLoteId)
                        {
                            Programacion.InventarioLoteOrigen = almacenInventarioLote;
                        }
                    }
                    else
                    {
                        // Agregamos una nueva programacion
                        Programacion = new ProgramacionMateriaPrimaInfo();
                        Programacion.Almacen = pedido.Almacen;
                        Programacion.InventarioLoteOrigen = almacenInventarioLote;
                        Programacion.PedidoDetalleId = detallePedido.PedidoDetalleId;
                        Programacion.Organizacion = new OrganizacionInfo { OrganizacionID = int.Parse(Application.Current.Properties["OrganizacionID"].ToString()) };
                        Programacion.CantidadProgramada = decimal.Parse(txtCantidadProgramada.Value.ToString());
                        Programacion.UsuarioCreacion = new UsuarioInfo { UsuarioID = int.Parse(Application.Current.Properties["UsuarioID"].ToString()) };

                        if (detallePedido.ProgramacionMateriaPrima == null)
                        {
                            detallePedido.ProgramacionMateriaPrima = new List<ProgramacionMateriaPrimaInfo>();
                        }

                        detallePedido.ProgramacionMateriaPrima.Add(Programacion);
                    }
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                resultado = false;
                Logger.Error(ex);
            }

            return resultado;
        }

        /// <summary>
        /// Guarda los valores capturados a las variables de pedido detalle.
        /// </summary>
        private void Guardar()
        {
            if (ValidaDatos())
            {
                if (AsignarProgramacion())
                {
                    SalidaNormal = true;
                    Close();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CrearProgramacionMateriaPrimaDialogo_ErrorAsignacion,
                            MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Valida que los datos requeridos esten capturados.
        /// </summary>
        /// <returns></returns>
        private bool ValidaDatos()
        {
            if (txtCantidadProgramada.Value > 0)
            {
                if (skAyudaLote.Clave == string.Empty)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CrearProgramacionMateriaPrimaDialogo_MsgLote,
                        MessageBoxButton.OK, MessageImage.Warning);
                    skAyudaLote.AsignarFoco();
                    return false;
                }
                if (ExisteAutorizacion())
                {
                    if (!ValidaCantidadProgramada() || !ValidaDisponibilidadLote() || !ValidarLoteEnUso())
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearProgramacionMateriaPrimaDialogo_MsgCantidadProgramada,
                    MessageBoxButton.OK, MessageImage.Warning);
                txtCantidadProgramada.Focus();
                return false;
            }

            return true;
        }
        /// <summary>
        /// Existe autorizacion para lote y folio seleccionado
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
                    Lote = Convert.ToInt32(skAyudaLote.Clave),
                    TipoAutorizacionID = TipoAutorizacionEnum.UsoLote.GetHashCode(),
                    Folio = pedido.FolioPedido
                };
                AutorizacionMateriaPrimaInfo resultado = solicitudAutorizacionPl.ObtenerDatosSolicitudAutorizacionProgramacionMP(autorizacionInfo);
                if (resultado != null)
                {
                    if (resultado.EstatusID == Estatus.AMPAutoriz.GetHashCode())
                    {
                        regreso = true;
                        Autorizado = true;
                    }
                    else if (resultado.EstatusID == Estatus.AMPPendien.GetHashCode())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      string.Format(Properties.Resources.SolicitudProgramacionMateriaPrima_MsjSolicitudPendiente,
                                                    pedido.FolioPedido), MessageBoxButton.OK,
                                      MessageImage.Warning);
                        skAyudaLote.Focus();
                    }
                    else if (resultado.EstatusID == Estatus.AMPRechaza.GetHashCode())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      string.Format(Properties.Resources.SolicitudProgramacionMateriaPrima_MsjSolicitudRechazado,
                                                    skAyudaLote.Clave,pedido.FolioPedido), MessageBoxButton.OK,
                                      MessageImage.Warning);
                        skAyudaLote.Focus();
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
        /// <summary>
        /// Validar lote en uso
        /// </summary>
        /// <returns></returns>
        private bool ValidarLoteEnUso()
        {
            decimal cantidadProgramadaAux = 0;
            var datosLote = new AlmacenInventarioLoteInfo
            {
                ProductoId = producto.ProductoId,
                OrganizacionId = pedido.Organizacion.OrganizacionID,
                TipoAlmacenId = (int) TipoAlmacenEnum.MateriasPrimas,
                Activo = EstatusEnum.Activo
            };
            var almacenInventarioLotePL = new AlmacenInventarioLotePL();
            IList<AlmacenInventarioLoteInfo> resultado = almacenInventarioLotePL.ObtenerLotesUso(datosLote);
            if (resultado != null)
            {
                if (Convert.ToInt32(skAyudaLote.Clave) == resultado[0].Lote || Autorizado)
                {
                    Autorizado = false;
                    return true;
                }
                if (Convert.ToInt32(skAyudaLote.Clave) == resultado[1].Lote)
                {
                    var pedidoPL = new PedidosPL();
                    int cantidadProgramada =
                        pedidoPL.ObtenerPedidosProgramadosPorLoteCantidadProgramada(resultado[0].Lote);
                    if (detallePedido.ProgramacionMateriaPrima != null)
                    {
                        foreach (
                            var programacionMateriaPrimaInfo in
                                detallePedido.ProgramacionMateriaPrima.Where(
                                    programacionMateriaPrimaInfo =>
                                    programacionMateriaPrimaInfo.InventarioLoteOrigen.Lote == resultado[0].Lote))
                        {
                            cantidadProgramadaAux = programacionMateriaPrimaInfo.CantidadProgramada;
                        }
                    }
                    if ((resultado[0].Cantidad - (cantidadProgramada + cantidadProgramadaAux)) == 0)
                    {
                        return true;
                    }
                }
                loteEnUso = resultado[0].Lote;
            }
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      string.Format(Properties.Resources.SolicitudProgramacionMateriaPrima_RequiereAutorizacion,
                                                    skAyudaLote.Clave), MessageBoxButton.OK,
                                      MessageImage.Warning);
            SolicitarAutorizacionLoteUso();
            return false;
        }

        /// <summary>
        /// Solicitar lote en uso
        /// </summary>
        private void SolicitarAutorizacionLoteUso()
        {
            almacenInventarioLote.CantidadProgramada = Convert.ToDecimal(txtCantidadProgramada.Text);
            detallePedido.LoteSelecionado = Convert.ToInt32(skAyudaLote.Clave);
            detallePedido.LoteUso = loteEnUso;
            var edicion = new SolicitudProgramacionMateriaPrima(pedido, detallePedido, almacenInventarioLote);
            MostrarCentrado(edicion);
        }

        /// <summary>
        /// Valida que la cantidad programada no pase de la cantidad solicitada.
        /// </summary>
        private bool ValidaCantidadProgramada()
        {
            bool resultado = true;
            var cantidadProgramada = decimal.Parse(txtCantidadProgramada.Value.ToString());

            cantidadProgramada = cantidadProgramada + detallePedido.TotalCantidadProgramada;

            if (Programacion != null) // Si estan editando restamos la cantidad anterior
            {
                cantidadProgramada = cantidadProgramada - Programacion.CantidadProgramada;
            }

            if (cantidadProgramada > detallePedido.CantidadSolicitada)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         Properties.Resources.CrearProgramacionMateriaPrimaDialogo_MsgCantidadSolicitada,
                         MessageBoxButton.OK, MessageImage.Warning);
                txtCantidadProgramada.Focus();
                resultado = false;
            }

            var pedidoPL = new PedidosPL();
            List<PedidoPendienteLoteModel> listaPedidoPendientes =
                pedidoPL.ObtenerPedidosProgramadosPorLote(almacenInventarioLote.AlmacenInventarioLoteId);
            decimal cantidadPendienteProgramada = 0;
            if (listaPedidoPendientes != null && listaPedidoPendientes.Any())
            {
                List<PedidoPendienteLoteModel> pedidosNoCompletos =
                    listaPedidoPendientes.Where(pedi => pedi.AlmacenMovimientoOrigenID == 0).ToList();
                if (pedidosNoCompletos.Any())
                {
                    cantidadPendienteProgramada = pedidosNoCompletos.Sum(pedi => pedi.CantidadProgramada);
                }
            }
            if (cantidadPendienteProgramada > 0)
            {
                if ((cantidadProgramada + cantidadPendienteProgramada) > almacenInventarioLote.Cantidad)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format(Properties.Resources.CrearProgramacionMateriaPrimaDialogo_MsgTotalProgramacion, (almacenInventarioLote.Cantidad - cantidadProgramada )),
                        MessageBoxButton.OK, MessageImage.Warning);
                    txtCantidadProgramada.Focus();
                    resultado = false;
                }
            }
            return resultado;
        }
        /// <summary>
        /// Cargar ayuda del lote
        /// </summary>

        private void CargarAyudaLote()
        {
            skAyudaLote = new SKAyuda<AlmacenInventarioLoteInfo>(0,
                    false,
                    new AlmacenInventarioLoteInfo
                    {
                        ProductoId = producto.ProductoId,
                        OrganizacionId = pedido.Organizacion.OrganizacionID,
                        TipoAlmacenId = (int)TipoAlmacenEnum.MateriasPrimas,
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadLote",
                    "PropiedadObtenerLoteCantidadMayorACero",
                    false,
                    70,
                    true)
            {
                AyudaPL = new AlmacenInventarioLotePL(),
                MensajeClaveInexistente = Properties.Resources.AyudaLote_MsgLoteInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaLote_MsgSalirSinSeleccionarLote,
                MensajeBusqueda = Properties.Resources.AyudaLote_MsgBusqueda,
                MensajeAgregar = Properties.Resources.AyudaLote_MsgDebeSeleccionarLote,
                TituloEtiqueta = Properties.Resources.AyudaLote_EtiquetaBuscar,
                TituloPantalla = Properties.Resources.AyudaLote_Titulo
            };

            skAyudaLote.ObtenerDatos += ObtenerLote;
            skAyudaLote.AsignaTabIndex(1);
            SplAyudaLote.Children.Clear();
            SplAyudaLote.Children.Add(skAyudaLote);
        }
        /// <summary>
        /// Obtener lote
        /// </summary>
        /// <param name="filtro"></param>
        public void ObtenerLote(string filtro)
        {
            try
            {
                if (skAyudaLote.Clave != string.Empty)
                {
                    if (ValidarLote(skAyudaLote.Info))
                    {
                        almacenInventarioLote = skAyudaLote.Info;
                    }
                    else
                    {
                        skAyudaLote.Clave = string.Empty;
                        skAyudaLote.Descripcion = string.Empty;
                    }

                    skAyudaLote.Info = new AlmacenInventarioLoteInfo
                                           {
                                               ProductoId = producto.ProductoId,
                                               OrganizacionId = pedido.Organizacion.OrganizacionID,
                                               TipoAlmacenId = (int) TipoAlmacenEnum.MateriasPrimas,
                                               Activo = EstatusEnum.Activo
                                           };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Validar lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        private bool ValidarLote(AlmacenInventarioLoteInfo lote)
        {
            if (Programacion == null)
            {
                if (detallePedido.ProgramacionMateriaPrima != null)
                {
                    foreach (var programacionInfo in detallePedido.ProgramacionMateriaPrima)
                    {
                        if (programacionInfo.InventarioLoteOrigen != null)
                        {
                            if (lote.AlmacenInventarioLoteId ==
                                programacionInfo.InventarioLoteOrigen.AlmacenInventarioLoteId)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CrearProgramacionMateriaPrimaDialogo_MsgLoteCapturado,
                                    MessageBoxButton.OK, MessageImage.Warning);
                                skAyudaLote.AsignarFoco();
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Valida la disponibiidad del lote
        /// </summary>
        /// <returns></returns>
        private bool ValidaDisponibilidadLote()
        {
            if (almacenInventarioLote != null)
            {
                decimal cantidadProgramada = decimal.Parse(txtCantidadProgramada.Value.ToString());
                if (!(almacenInventarioLote.Cantidad >= cantidadProgramada))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CreaProgramacionMateriaPrima_MsgLoteSinExistencia,
                        MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CreaProgramacionMateriaPrima_ErrorCantidadLote,
                    MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
            return true;
        }

        #endregion Metodos

    }
}
