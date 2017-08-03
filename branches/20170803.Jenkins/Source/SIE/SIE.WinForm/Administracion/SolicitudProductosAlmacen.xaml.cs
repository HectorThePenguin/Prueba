using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Win32;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para SolicitudProductosAlmacen.xaml
    /// </summary>
    public partial class SolicitudProductosAlmacen
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private SolicitudProductoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (SolicitudProductoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private UsuarioInfo usuario;
        private AlmacenInfo almacenGeneral;
        private FamiliasEnum familia;
        //private int autorizadorId;
        private int centroCostoId;
        private string filtroFamilias;
        private string filtroTipoAlmacen;
        private bool ayudaConfigurada = false;

        private bool requiereAyuda = true;

        private readonly SolicitudProductoBL solicitudProductoBL;

        #endregion

        #region Constructor

        public SolicitudProductosAlmacen()
        {
            solicitudProductoBL = new SolicitudProductoBL();
            InitializeComponent();
            //InicializaContexto();
        }

        ~SolicitudProductosAlmacen()
        {
            if (skAyudaDestino != null)
            {
                skAyudaDestino.Dispose();
                skAyudaDestino = null;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            usuario = new UsuarioPL().ObtenerPorID(AuxConfiguracion.ObtenerUsuarioLogueado());
            skAyudaFolio.ObjetoNegocio = solicitudProductoBL;
            skAyudaFolio.AyudaConDatos += (o, args) => AyudaConDatosFolio();

            skAyudaProducto.Contexto = new ProductoInfo();
            skAyudaProducto.ObjetoNegocio = new ProductoPL();
            skAyudaProducto.AyudaConDatos += (o, args) => AyudaConDatosProducto();
            skAyudaProducto.AyudaLimpia += (o, args) => AyudaLimpiaProducto();

            Limpiar();
            Dispatcher.BeginInvoke(new Action(ValidaTipoAlmacen), DispatcherPriority.ContextIdle, null);
            Dispatcher.BeginInvoke(new Action(ValidaCentroCostoUsuario), DispatcherPriority.ContextIdle, null);
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar números letras sin acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtValidarSoloNumerosDecimalesPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string valor = ((DecimalUpDown)sender).Value.HasValue
                               ? ((DecimalUpDown)sender).Value.ToString()
                               : string.Empty;
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, valor);
        }

        //private void TxtCantidad_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Tab || e.Key == Key.Enter)
        //    {
        //        if (ValidaDisponibilidad())
        //        {
        //            if (ValidaAgregar(false))
        //            {
        //                //ConfiguraAyudaDestino(familia);
        //                //skAyudaDestino.IsEnabled = true;
        //                btnAgregar.IsEnabled = true;
        //            }
        //        }
        //        else
        //        {
        //            string mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgSinDisponibilidad;
        //            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
        //                              MessageBoxButton.OK, MessageImage.Warning);
        //            txtCantidad.Focus();
        //        }
        //    }
        //}

        private void TxtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                if (txtCantidad.IsEnabled)
                {
                    if (ValidaDisponibilidad())
                    {
                        if (ValidaAgregar(false))
                        {
                            //ConfiguraAyudaDestino(familia);
                            //skAyudaDestino.IsEnabled = true;
                            //btnAgregar.IsEnabled = true;
                            //skAyudaDestino.AsignarFoco();
                            if (requiereAyuda)
                            {
                                Dispatcher.BeginInvoke(new Action(HabilitaDestino), DispatcherPriority.ContextIdle, null);
                            }
                            else
                            {
                                btnAgregar.IsEnabled = true;
                            }
                        }
                    }
                    else
                    {
                        string mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgSinDisponibilidad;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                          MessageBoxButton.OK, MessageImage.Warning);
                        txtCantidad.Focus();
                    }
                }

                else
                {
                    if (txtCantidad.IsEnabled)
                    {
                        txtCantidad.Focus();
                    }
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var botonEliminar = (Button)e.Source;
            try
            {
                var infoSelecionado = (SolicitudProductoDetalleInfo)botonEliminar.CommandParameter;
                if (infoSelecionado != null)
                {
                    Eliminar(infoSelecionado);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Almacen_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para agregar elementos ala grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Agregar();
        }

        /// <summary>
        /// Evento para limpiar los controle de la pantalla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarParcial();
        }

        /// <summary>
        /// Evento para imprimir la solicitud de productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            Imprimir();
        }

        /// <summary>
        /// Evento guardar la solicitud de productos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento para  cancelar la la solicitd de productos.   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Cancelar();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            try
            {
                ayudaConfigurada = false;
                skAyudaDestino.IsEnabled = false;
                filtroFamilias = string.Join("|",
                                             solicitudProductoBL.ObtenerFamiliasFiltrar().Select(f => f.GetHashCode()).
                                                 ToArray());
                filtroTipoAlmacen = string.Join("|",
                                                solicitudProductoBL.ObtenerTipoAlmacenFiltrar().Select(
                                                    e => e.GetHashCode())
                                                    .ToArray());

                //usuario = new UsuarioPL().ObtenerPorID(AuxConfiguracion.ObtenerUsuarioLogueado());
                usuario.OrganizacionID = usuario.Organizacion.OrganizacionID;
                almacenGeneral = ObtenerAlmacenGeneral(usuario.Organizacion.OrganizacionID);
                centroCostoId = ObtenerPorUsuarioAccion(usuario.UsuarioID, false);

                Contexto =
                    new SolicitudProductoInfo
                        {
                            OrganizacionID = usuario.OrganizacionID,
                            UsuarioSolicita = usuario,
                            UsuarioIDSolicita = usuario.UsuarioID,
                            CentroCostoID = centroCostoId,
                            Solicitud = InicializaContextoSolicitud(),
                            Detalle = new List<SolicitudProductoDetalleInfo>(),
                            DetalleGrid = new ObservableCollection<SolicitudProductoDetalleInfo>(),
                            FechaCreacion = DateTime.Today,
                            UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                            EstatusID = Estatus.SolicitudProductoPendiente.GetHashCode(),
                        };
            }
            catch (Exception err)
            {
                Logger.Error(err);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SolicitudProductosAlmacen_Error, MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
        }

        /// <summary>
        /// Obtiene el Contexto de la solicitud
        /// </summary>
        /// <returns></returns>
        private FolioSolicitudInfo InicializaContextoSolicitud()
        {
            var solicitud = new FolioSolicitudInfo
                                {
                                    Descripcion = string.Empty,
                                    Usuario = usuario,
                                    OrganizacionID = usuario.OrganizacionID,
                                    UsuarioIDSolicita = usuario.UsuarioID,
                                    Producto = new ProductoInfo { FiltroFamilia = filtroFamilias },
                                    Cantidad = decimal.Zero,
                                    //CentroCosto = new CentroCostoInfo{ AutorizadorID = autorizadorId},
                                    ClaseCostoProducto = new ClaseCostoProductoInfo(),
                                    AlmacenID = 0
                                };
            return solicitud;
        }

        /// <summary>
        /// Asigna el valor al campo folio
        /// </summary>
        private void AyudaConDatosFolio()
        {
            if (Contexto.Solicitud.FolioID > 0)
            {
                Contexto = ObtenerSolicitudPorId(Contexto.Solicitud.FolioID);
                bool isAutorizado = Contexto.IsAutorizado;

                var familiaDefault = Contexto.Detalle.Select(p => p).FirstOrDefault();

                if (familiaDefault == null)
                {
                    familia = FamiliasEnum.Medicamento;
                }
                else
                {
                    familia = (FamiliasEnum)familiaDefault.Producto.FamiliaId;
                }

                ConfiguraAyudaDestino(familia);

                if (Contexto.AlmacenID.HasValue)
                {
                    Contexto.Almacen = ObtenerAlmacen(Contexto.AlmacenID.Value);
                    skAyudaDestino.IsEnabled = false;
                    skAyudaDestino.Contexto = Contexto.Almacen;
                    //skAyudaDestino.Clave = Contexto.Almacen.AlmacenID.ToString();
                    //skAyudaDestino.Descripcion = Contexto.Almacen.Descripcion;
                }

                Contexto.Detalle.ForEach(e => e.Eliminar = !isAutorizado);
                Contexto.DetalleGrid = Contexto.Detalle.ConvertirAObservable();

                gridDatos.ItemsSource = Contexto.Detalle.Where(e => e.Activo == EstatusEnum.Activo);
                skAyudaFolio.IsEnabled = false;
                btnGuardar.Content = Properties.Resources.btnActualizar;
                skAyudaProducto.AsignarFoco();
            }
        }

        /// <summary>
        /// Asigna el valor al campo folio
        /// </summary>
        private void AyudaConDatosProducto()
        {
            var producto = skAyudaProducto.Contexto as ProductoInfo;
            string mensaje = string.Empty;

            if (producto != null)
            {
                familia = (FamiliasEnum)Contexto.Solicitud.Producto.FamiliaId;

                if (producto.ManejaLote)
                {
                    mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgNoManejeLote;
                }
                else if (!filtroFamilias.Contains(producto.FamiliaId.ToString()))
                {
                    mensaje = Properties.Resources.AyudaProducto_CodigoInvalido;
                }
                else
                {
                    Contexto.Solicitud.ClaseCostoProducto =
                        new ClaseCostoProductoBL().ObtenerPorProductoAlmacen(producto.ProductoId, almacenGeneral.AlmacenID);
                    Contexto.Solicitud.SinProducto = false;
                    if (!ayudaConfigurada)
                    {
                        ConfiguraAyudaDestino(familia);
                    }
                }
            }
            else
            {
                mensaje = Properties.Resources.AyudaProducto_CodigoInvalido;
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                skAyudaProducto.LimpiarCampos();
                skAyudaProducto.AsignarFoco();
                //Contexto.Solicitud.ClaseCostoProducto = new ClaseCostoProductoInfo();
                //Contexto.Solicitud.Producto.Descripcion = string.Empty;
                //Contexto.Solicitud.SinProducto = true;
                //Contexto.Solicitud.Producto.SubfamiliaId = 0;
                //Contexto.Solicitud.Producto.FamiliaId = 0;
                //Contexto.Solicitud.Producto.UnidadId = 0;
                //Contexto.Solicitud.Producto.FiltroFamilia = filtroFamilias;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  mensaje,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }

        }

        /// <summary>
        /// Asigna el valor al campo folio
        /// </summary>
        private void AyudaConDatosDestino()
        {
            btnAgregar.IsEnabled = true;
            btnAgregar.IsEnabled = ActivaBotonAgregar();

            if (Contexto.Almacen != null)
            {
                skAyudaDestino.IsEnabled = false;
            }
        }

        /// <summary>
        /// Limpiar los valores de la ayuda de productos
        /// </summary>
        private void AyudaLimpiaProducto()
        {
            Contexto.Solicitud.Producto.ProductoId = 0;
            Contexto.Solicitud.Producto.Descripcion = string.Empty;
            Contexto.Solicitud.Producto.ProductoDescripcion = string.Empty;
            Contexto.Solicitud.Producto.SubfamiliaId = 0;
            Contexto.Solicitud.Producto.FamiliaId = 0;
            Contexto.Solicitud.Producto.UnidadId = 0;
            Contexto.Solicitud.SinProducto = true;
            Contexto.Solicitud.Cantidad = 0;
            Contexto.Solicitud.Producto.FiltroFamilia = filtroFamilias;
            Contexto.Solicitud.ClaseCostoProducto = new ClaseCostoProductoInfo();
        }

        /// <summary>
        /// Limpiar los valores de la ayuda de destinos
        /// </summary>
        private void AyudaLimpiaDestino()
        {
            //btnAgregar.IsEnabled = !string.IsNullOrWhiteSpace(skAyudaDestino.Clave);
            //Contexto.Solicitud.CentroCosto.AutorizadorID = autorizadorId;
        }

        /// <summary>
        /// Obtiene una solicitud de productos por su folio de solicitud.
        /// </summary>
        /// <param name="solicitudId"></param>
        /// <returns></returns>
        private SolicitudProductoInfo ObtenerSolicitudPorId(int solicitudId)
        {
            SolicitudProductoInfo solicitud =
                solicitudProductoBL.ObtenerPorID(new SolicitudProductoInfo { SolicitudProductoID = solicitudId });
            solicitud.Solicitud = InicializaContextoSolicitud();
            solicitud.Solicitud.FolioID = solicitud.SolicitudProductoID;
            solicitud.Solicitud.FolioSolicitud = solicitud.FolioSolicitud;
            solicitud.Solicitud.Autoriza = string.Format("{0}", solicitud.FolioSolicitud);

            if (solicitud.AlmacenID.HasValue)
            {
                solicitud.Almacen = ObtenerAlmacen(solicitud.AlmacenID.Value);
            }

            return solicitud;
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
                var primeroActivo = Contexto.Detalle.FirstOrDefault(d => d.Activo == EstatusEnum.Activo);
                if (primeroActivo == null)
                {
                    mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgProductoRequerido;
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                resultado = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidaAgregar(bool mostrarMensaje)
        {
            bool resultado = true;
            string mensaje = string.Empty;

            try
            {
                var producto = skAyudaProducto.Contexto as ProductoInfo;



                // Si es un producto De la familia Herramientas no se valida existencia
                if (producto != null && (producto.FamiliaId == (int)FamiliasEnum.HerramientaYEquipo || producto.FamiliaId == (int)FamiliasEnum.Combustibles))
                {
                    ProductoCodigoParteInfo codigoParte = solicitudProductoBL.ExisteCodigoParteDeproducto(producto);
                    if (codigoParte == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                String.Format("{0}{1}{2}",
                                Properties.Resources.SolicitudProductosAlmacen_MsgSinCodigoParte,
                                producto.Descripcion,
                                Properties.Resources.SolicitudProductosAlmacen_MsgSinCodigoParte2),
                                MessageBoxButton.OK, MessageImage.Warning);
                        txtCantidad.Focus();
                        return false;
                    }

                    /*
                    if (mostrarMensaje)
                    {
                        skAyudaDestino.Descripcion = codigoParte.CodigoParte;
                        var centroCostoAutoriadorBL = new CentroCostoUsuarioBL();
                        var info = centroCostoAutoriadorBL.ObtenerPorUsuarioAccion(usuario.UsuarioID, false);
                        if (info != null)
                        {
                            Contexto.Solicitud.ClaseCostoProducto = info;
                        }
                    }*/
                    btnAgregar.Focus();
                    return resultado;
                }


                decimal? cantidad = txtCantidad.Value.HasValue
                                        ? txtCantidad.Value
                                        : decimal.Zero;

                int productoId = producto != null
                                     ? producto.ProductoId
                                     : 0;

                decimal existencia = ObtenerExistencia(productoId);
                decimal disponibilidad = ObtenerDisponibilidad(productoId);

                if (producto == null || producto.ProductoId == 0)
                {
                    mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgProductoRequerido;
                    skAyudaProducto.AsignarFoco();
                }
                else if (cantidad == 0)
                {
                    mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgCantidadRequerida;
                    txtCantidad.Focus();
                }
                else if ((existencia - disponibilidad) < cantidad)
                {
                    mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgSinDisponibilidad;
                    txtCantidad.Focus();
                }
                else if (skAyudaDestino.IsEnabled && (string.IsNullOrWhiteSpace(skAyudaDestino.Clave) || skAyudaDestino.Clave == "0"))
                {
                    mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgDestinoRequerido;
                    skAyudaDestino.AsignarFoco();
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                resultado = false;
                if (mostrarMensaje)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            return resultado;
        }

        /// <summary>
        /// Método para cancelar
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();
            if (guardar)
            {
                try
                {
                    long folio = Contexto.FolioSolicitud;
                    Contexto.CentroCostoID = centroCostoId;

                    bool inactivar = !Contexto.Detalle.Select(e => e.Activo == EstatusEnum.Activo).Any();
                    if (inactivar)
                    {
                        Contexto.Activo = EstatusEnum.Inactivo;
                    }

                    if (!Contexto.AlmacenID.HasValue)
                    {
                        if (familia != FamiliasEnum.Combustibles)
                        {
                            if (requiereAyuda)
                            {
                                var almacenDestino = (AlmacenInfo)skAyudaDestino.Contexto;
                                Contexto.AlmacenID = almacenDestino.AlmacenID;
                            }
                        }
                    }

                    solicitudProductoBL.Guardar(Contexto);
                    string mensaje = folio == 0
                                         ? string.Format(
                                             Properties.Resources.SolicitudProductosAlmacen_GuardadoConExito,
                                             Contexto.FolioSolicitud)
                                         : Properties.Resources.SolicitudProductosAlmacen_ActualizadoConExito;


                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                      MessageBoxButton.OK,
                                      MessageImage.Correct);
                    Limpiar();
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.SolicitudProductosAlmacen_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.SolicitudProductosAlmacen_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Método para cancelar
        /// </summary>
        private void Cancelar()
        {
            MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.
                                                            SolicitudProductosAlmacen_MsgConfirmaCancelar,
                                                        MessageBoxButton.YesNo, MessageImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        /// <summary>
        /// Método para Agregar
        /// </summary>
        private void Agregar()
        {
            bool validaAgregar = ValidaAgregar(true);
            if (validaAgregar)
            {
                bool isAutorizado = Contexto.IsAutorizado;
                var producto = (ProductoInfo)Extensor.ClonarInfo(Contexto.Solicitud.Producto ?? new ProductoInfo());
                //var centroCosto = (CentroCostoInfo)Extensor.ClonarInfo(Contexto.Solicitud.CentroCosto);
                var claseCostoProducto = Contexto.Solicitud.ClaseCostoProducto ?? new ClaseCostoProductoInfo();

                int productoId = producto == null ? 0 : producto.ProductoId;
                //int centroCostoId = centroCosto == null ? 0 : centroCosto.CentroCostoID;
                decimal cantidad = txtCantidad.Value.HasValue
                                       ? txtCantidad.Value.Value
                                       : decimal.Zero;

                string descripcion = skAyudaDestino.Descripcion;


                CamionRepartoInfo camionReparto = null;
                if (familia == FamiliasEnum.Combustibles)
                {
                    camionReparto = (CamionRepartoInfo)skAyudaDestino.Contexto;
                    descripcion = skAyudaDestino.Descripcion.Trim(); //camionReparto.NumeroEconomico.Trim();
                    camionReparto.CamionRepartoID = int.Parse(skAyudaDestino.Clave);
                }

                Contexto.DetalleGrid = Contexto.DetalleGrid ?? new ObservableCollection<SolicitudProductoDetalleInfo>();

                //var registro = Contexto.DetalleGrid.FirstOrDefault(e => e.ProductoID == productoId && e.Activo == EstatusEnum.Activo);
                //if (registro == null)
                //{
                var registro = new SolicitudProductoDetalleInfo
                                   {
                                       SolicitudProductoID = Contexto.SolicitudProductoID,
                                       ProductoID = productoId,
                                       Producto = producto,
                                       Cantidad = cantidad,
                                       ClaseCostoProducto = claseCostoProducto,
                                       CamionRepartoID =
                                           camionReparto == null ? null : (int?)camionReparto.CamionRepartoID,
                                       Concepto = descripcion,
                                       //CentroCosto =  centroCosto,
                                       EstatusID = Estatus.SolicitudProductoPendiente.GetHashCode(),
                                       Eliminar = !isAutorizado,
                                       Activo = EstatusEnum.Activo
                                   };

                Contexto.DetalleGrid.Add(registro);
                //}
                //else
                //{
                //    registro.ProductoID = producto.ProductoId;
                //    registro.Producto = producto;
                //    registro.Cantidad = cantidad;
                //    registro.ClaseCostoProducto =claseCostoProducto;
                //}
                Contexto.Solicitud.Producto = new ProductoInfo { FiltroFamilia = filtroFamilias };
                Contexto.Solicitud.Cantidad = 0;
                Contexto.Solicitud.CentroCosto = new CentroCostoInfo();
                Contexto.Detalle = Contexto.DetalleGrid.ToList();
                gridDatos.ItemsSource = Contexto.Detalle.Where(e => e.Activo == EstatusEnum.Activo);
                Contexto.Guardar = true;
                LimpiarParcial();
            }
        }

        /// <summary>
        /// Limpia todos los controles de la pantalla.
        /// </summary>
        private void Limpiar()
        {
            try
            {
                InicializaContexto();
                btnImprimir.IsEnabled = false;
                Contexto.Guardar = false;
                btnGuardar.Content = Properties.Resources.btnGuardar;
                skAyudaFolio.IsEnabled = true;
                LimpiarParcial();
                gridDatos.ItemsSource = Contexto.DetalleGrid;
                //skAyudaDestino.IsEnabled = false;
                skAyudaDestino.LimpiarCampos();
                ayudaConfigurada = false;
                //skAyudaAlmacen.Visibility = Visibility.Hidden;
                //skAyudaCamionReparto.Visibility = Visibility.Hidden;
            }
            catch (Exception err)
            {
                Logger.Error(err);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SolicitudProductosAlmacen_Error, MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
        }

        /// <summary>
        /// Limpia los datos de captura para el grid.
        /// </summary>
        private void LimpiarParcial()
        {
            skAyudaProducto.LimpiarCampos();
            skAyudaProducto.AsignarFoco();
            btnAgregar.IsEnabled = false;

            if (Contexto.Almacen != null)
            {
                skAyudaDestino.IsEnabled = false;
                filtroFamilias = string.Join("|",
                                             solicitudProductoBL.ObtenerFamiliasFiltrar().Where(
                                                 e => e != FamiliasEnum.Combustibles).Select(f => f.GetHashCode()).
                                                 ToArray());
            }
            else
            {
                skAyudaDestino.LimpiarCampos();
                skAyudaDestino.IsEnabled = false;
            }
        }

        /// <summary>
        /// Asigna Inactivo al registro para ocultarlo del grid.
        /// </summary>
        /// <param name="info"></param>
        private void Eliminar(SolicitudProductoDetalleInfo info)
        {

            MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.
                                                            SolicitudProductosAlmacen_MsgConfirmaEliminar,
                                                        MessageBoxButton.YesNo, MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                info.Activo = EstatusEnum.Inactivo;
                Contexto.Detalle = Contexto.DetalleGrid.ToList();
                gridDatos.ItemsSource = Contexto.Detalle.Where(e => e.Activo == EstatusEnum.Activo);
                LimpiarParcial();
                Contexto.Guardar = true;
            }
        }

        /// <summary>
        /// Obtiene el almacén del destino
        /// el usuario.
        /// </summary>
        private AlmacenInfo ObtenerAlmacen(int almacenId)
        {
            var almacenDaL = new AlmacenPL();
            AlmacenInfo almacenInfo = almacenDaL.ObtenerPorID(almacenId);
            almacenInfo.FiltroTipoAlmacen = filtroTipoAlmacen;
            almacenInfo.Organizacion = new OrganizacionInfo
                                           {
                                               OrganizacionID =
                                                   usuario.OrganizacionID
                                           };

            return almacenInfo;
        }

        /// <summary>
        /// Obtiene el almacén general que tenga configurado
        /// el usuario.
        /// </summary>
        private AlmacenInfo ObtenerAlmacenGeneral(int almacenId)
        {
            var almacenDaL = new AlmacenPL();
            AlmacenInfo almacenInfo = null;
            IList<AlmacenInfo> almacenes = almacenDaL.ObtenerAlmacenPorOrganizacion(almacenId);
            if (almacenes != null && almacenes.Count > 0)
            {
                almacenInfo = almacenes.FirstOrDefault(a => a.TipoAlmacenID == (int)TipoAlmacenEnum.GeneralGanadera
                                                            && a.Activo == EstatusEnum.Activo);
            }
            return almacenInfo;
        }

        /// <summary>
        /// Valida que el almacén sea de tipo Almacén General
        /// </summary>
        private void ValidaTipoAlmacen()
        {
            if (almacenGeneral == null)
            {
                skAyudaFolio.IsEnabled = false;
                skAyudaProducto.IsEnabled = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.
                                      SolicitudProductosAlmacen_MsgNoExisteAlmacen,
                                  MessageBoxButton.OK, MessageImage.Stop);
            }
        }

        /// <summary>
        /// Método para Imprimir
        /// </summary>
        private void Imprimir()
        {
            try
            {
                Mouse.SetCursor(Cursors.Wait);
                var reporte = new DataSet("ReporteSolicitudProductos");
                var solicitud = new DataTable("Solicitud");

                string division = AuxDivision.ObtenerDivision(Contexto.OrganizacionID);
                string tituloReporte = string.Format(Properties.Resources.Reporte_NombreEmpresa, division);
                string organizacion = string.Format("{0}", usuario.Organizacion.Descripcion);

                var columnasSolicitud = new[]
                                            {
                                                new DataColumn("Empresa", typeof (string)),
                                                new DataColumn("Organizacion", typeof (string)),
                                                new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtFolio,
                                                               typeof (int)),
                                                new DataColumn(
                                                    Properties.Resources.SolicitudProductosAlmacen_DtFechaSolicitud,
                                                    typeof (DateTime)),
                                                new DataColumn(
                                                    Properties.Resources.SolicitudProductosAlmacen_DtSolicito,
                                                    typeof (string)),
                                                new DataColumn(
                                                    Properties.Resources.SolicitudProductosAlmacen_DtAutorizo,
                                                    typeof (string)),
                                            };

                solicitud.Columns.AddRange(columnasSolicitud);
                var rowSolicitud = solicitud.NewRow();
                rowSolicitud["Empresa"] = tituloReporte;
                rowSolicitud["Organizacion"] = organizacion;
                rowSolicitud[Properties.Resources.SolicitudProductosAlmacen_DtFolio] = Contexto.FolioSolicitud;
                rowSolicitud[Properties.Resources.SolicitudProductosAlmacen_DtFechaSolicitud] = Contexto.FechaSolicitud;
                rowSolicitud[Properties.Resources.SolicitudProductosAlmacen_DtSolicito] =
                    Contexto.UsuarioSolicita.Nombre;
                rowSolicitud[Properties.Resources.SolicitudProductosAlmacen_DtAutorizo] =
                    Contexto.UsuarioAutoriza.Nombre;
                solicitud.Rows.Add(rowSolicitud);

                var detalle = new System.Data.DataTable("Detalle");
                var columnasDetalle = new System.Data.DataColumn[]
                                          {
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtFolio,
                                                             typeof (int)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtCodigo,
                                                             typeof (int)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtArticulo,
                                                             typeof (string)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtCantidad,
                                                             typeof (decimal)),
                                              new DataColumn(
                                                  Properties.Resources.SolicitudProductosAlmacen_DtUnidadMedicion,
                                                  typeof (string)),
                                              new DataColumn(
                                                  Properties.Resources.SolicitudProductosAlmacen_DtDescripcion,
                                                  typeof (string)),
                                              new DataColumn(
                                                  Properties.Resources.SolicitudProductosAlmacen_DtClaseCosto,
                                                  typeof (string)),
                                              new DataColumn(
                                                  Properties.Resources.SolicitudProductosAlmacen_DtCentroCosto,
                                                  typeof (string)),
                                          };
                detalle.Columns.AddRange(columnasDetalle);

                var familaHerramientas =
                    Contexto.Detalle.Where(row => row.Producto.FamiliaId == (int)FamiliasEnum.HerramientaYEquipo)
                        .ToList();

                var centroCostoUsuarioInfo = new CentroCostoInfo();
                if (familaHerramientas != null && familaHerramientas.Any())
                {
                    var centroCostoAutoriadorBL = new CentroCostoBL();
                    centroCostoUsuarioInfo = centroCostoAutoriadorBL.ObtenerPorID(Contexto.CentroCostoID);
                }



                foreach (var row in Contexto.Detalle)
                {
                    if (row.Activo == EstatusEnum.Activo)
                    {
                        var newDetalle = detalle.NewRow();
                        newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtFolio] = Contexto.FolioSolicitud;
                        newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtCodigo] = row.Producto.ProductoId;
                        newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtArticulo] = row.Producto.Descripcion;
                        newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtCantidad] = row.Cantidad;
                        newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtUnidadMedicion] =
                            row.Producto.UnidadMedicion.Descripcion;

                        //Si el producto es de la familia de herramientas se muestra codigo parte
                        if (row.Producto != null && row.Producto.FamiliaId == (int)FamiliasEnum.HerramientaYEquipo)
                        {
                            ProductoCodigoParteInfo codigoParte = solicitudProductoBL.ExisteCodigoParteDeproducto(row.Producto);
                            newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtDescripcion] = codigoParte.CodigoParte;
                            newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtClaseCosto] =
                            centroCostoUsuarioInfo != null
                                ? centroCostoUsuarioInfo.CentroCostoSAP
                                : string.Empty;
                        }
                        else
                        {
                            newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtDescripcion] = row.Concepto;
                            newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtClaseCosto] =
                            row.ClaseCostoProducto != null
                                ? row.ClaseCostoProducto.CuentaSAP.CuentaSAP
                                : string.Empty;
                        }

                        detalle.Rows.Add(newDetalle);
                    }
                }

                reporte.Tables.Add(solicitud);
                reporte.Tables.Add(detalle);

                string nombreReporte = Properties.Resources.SolicitudProductosAlmacen_NombreReporte;
                var nombre = string.Format(@"{0}-{1:d8}.pdf", nombreReporte, Contexto.FolioSolicitud);


                //string archivoXML = string.Format("{0}.xml", nombreReporte);
                //reporte.WriteXml(string.Format(@"c:\Reporte\{0}", archivoXML), XmlWriteMode.WriteSchema);

                using (var documento = new ReportDocument())
                {
                    using (var streaming = this.GetType().Assembly.GetManifestResourceStream(
                        string.Format("SIE.WinForm.Reporte.{0}.rpt", nombreReporte)))
                    {
                        using (var fileStream = File.Create(nombre))
                        {
                            if (streaming != null) streaming.CopyTo(fileStream);
                        }
                    }


                    documento.Load(nombre);
                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(reporte);
                    documento.Refresh();

                    //Establecemos el nombre correcto de la impresora para enviar directamente el informe
                    //documento.PrintOptions.PrinterName = configuracion.ImpresoraRecepcionGanado;

                    //Iniciando el proceso de impresion ponemos la configuracion de la impresion
                    //documento.PrintToPrinter(1, false, 0, 0);

                    var dialogo = new SaveFileDialog
                                      {
                                          FileName = nombre,
                                          DefaultExt = ".pdf",
                                          Filter = "Documentos Pdf (*.pdf)|*.pdf"
                                      };

                    bool? resultado = dialogo.ShowDialog();
                    if (resultado == true)
                    {
                        try
                        {
                            nombre = dialogo.FileName;
                            documento.ExportToDisk(ExportFormatType.PortableDocFormat, nombre);
                            var archivo = new FileStream(nombre, FileMode.Open);
                            archivo.Close();
                            if (File.Exists(nombre))
                            {
                                Process.Start(nombre);
                            }
                        }
                        catch (IOException)
                        {
                            //throw new ExcepcionServicio(Properties.Resources.ExportarExcel_ArchivoAbierto);
                        }
                        finally
                        {
                            documento.Close();
                        }
                    }
                }


                Mouse.SetCursor(Cursors.Arrow);
                string mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgReporteGenerado;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                  MessageBoxButton.OK,
                                  MessageImage.Correct);
            }
            catch (Exception err)
            {
                Logger.Error(err);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ProgramacionCorte_ErrorImpresora, MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
        }

        /// <summary>
        /// Obtener el centro de costo
        /// del usuario que solicita.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="accion">falto india que se traera 
        /// la persona que solicita </param>
        /// <returns></returns>
        private int ObtenerPorUsuarioAccion(int usuarioId, bool accion)
        {
            int result = 0;
            try
            {
                var centroCostoAutoriadorBL = new CentroCostoUsuarioBL();
                
                var info = centroCostoAutoriadorBL.ObtenerPorUsuarioAccion(usuarioId, accion);
                if (info != null)
                {
                    result = info.CentroCostoID;
                }
            }
            catch (Exception err)
            {
                Logger.Error(err);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.SolicitudProductosAlmacen_Error, MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
            return result;
        }

        /// <summary>
        /// Obtiene la existencia de productos.
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>
        private decimal ObtenerExistencia(int productoId)
        {
            decimal existencia = decimal.Zero;
            if (productoId > 0)
            {
                var productos = new List<ProductoInfo>
                                    {
                                        new ProductoInfo
                                            {
                                                ProductoId = productoId
                                            }
                                    };
                IList<AlmacenInventarioInfo> almacenInventario = new ProductoPL().ObtenerExistencia(almacenGeneral.AlmacenID,
                                                                                                    productos);
                if (almacenInventario != null)
                {
                    var almacenInventarioInfo = almacenInventario.FirstOrDefault();
                    if (almacenInventarioInfo != null)
                        existencia = almacenInventarioInfo.Cantidad;
                }
            }
            return existencia;
        }

        /// <summary>
        /// Obtiene la existencia de productos.
        /// </summary>
        /// <returns></returns>
        private decimal ObtenerDisponibilidad(int productoId)
        {
            decimal disponibilidad = decimal.Zero;
            if (productoId > 0)
            {
                var filtro = new FolioSolicitudInfo
                                 {
                                     OrganizacionID = Contexto.OrganizacionID,
                                     Producto = Contexto.Solicitud.Producto,
                                     EstatusID = (int)Estatus.SolicitudProductoAutorizado,
                                     Activo = EstatusEnum.Activo
                                 };

                IList<SolicitudProductoInfo> solicitudes = solicitudProductoBL.ObtenerSolicitudesAutorizadas(filtro);

                if (solicitudes != null)
                {
                    disponibilidad =
                        solicitudes.SelectMany(sd => sd.Detalle)
                            .Where(sd => sd.ProductoID == productoId
                                         && sd.EstatusID == Estatus.SolicitudProductoAutorizado.GetHashCode()
                                         && sd.Activo == EstatusEnum.Activo)
                            .Sum(c => c.Cantidad);
                }

                filtro = new FolioSolicitudInfo
                {
                    OrganizacionID = Contexto.OrganizacionID,
                    Producto = Contexto.Solicitud.Producto,
                    EstatusID = (int)Estatus.SolicitudProductoEntregado,
                    Activo = EstatusEnum.Activo
                };

                solicitudes = solicitudProductoBL.ObtenerSolicitudesAutorizadas(filtro);
                if (solicitudes != null)
                {
                    disponibilidad +=
                        solicitudes.SelectMany(sd => sd.Detalle)
                            .Where(sd => sd.ProductoID == productoId
                                         && sd.EstatusID == Estatus.SolicitudProductoEntregado.GetHashCode()
                                         && sd.Activo == EstatusEnum.Activo)
                            .Sum(c => c.Cantidad);
                }
            }
            return disponibilidad;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidaDisponibilidad()
        {
            var producto = skAyudaProducto.Contexto as ProductoInfo;
            if (producto != null && producto.ProductoId > 0)
            {
                if (producto.FamiliaId == (int)FamiliasEnum.HerramientaYEquipo || producto.FamiliaId == (int)FamiliasEnum.Combustibles)
                {
                    return true;
                }

                decimal cantidad = Contexto.Solicitud.Cantidad;
                decimal existencia = ObtenerExistencia(producto.ProductoId);
                decimal disponibilidad = ObtenerDisponibilidad(producto.ProductoId);
                return (existencia - disponibilidad) >= cantidad;

            }
            return false;
        }

        /// <summary>
        /// Valida si se activará el botón de agregar
        /// </summary>
        /// <returns></returns>
        private bool ActivaBotonAgregar()
        {
            return ValidaAgregar(false);
        }

        private void ConfiguraAyudaDestino(FamiliasEnum flujo)
        {
            ayudaConfigurada = true;
            switch (flujo)
            {
                case FamiliasEnum.MaterialEmpaque:
                case FamiliasEnum.Medicamento:
                    {
                        var listaTiposAlmacenes = solicitudProductoBL.ObtenerTipoAlmacenFiltrar();

                        filtroTipoAlmacen = flujo == FamiliasEnum.Medicamento
                                                ? filtroTipoAlmacen
                                                : string.Format("{0}", TipoAlmacenEnum.AlmacenMEI.GetHashCode());

                        skAyudaDestino = new ControlAyuda();
                        Contexto.Almacen = Contexto.Almacen ?? new AlmacenInfo
                                                                   {
                                                                       Organizacion = new OrganizacionInfo
                                                                                          {
                                                                                              OrganizacionID =
                                                                                                  usuario.OrganizacionID
                                                                                          },
                                                                       FiltroTipoAlmacen = filtroTipoAlmacen,
                                                                       ListaTipoAlmacen = (from tip in listaTiposAlmacenes
                                                                                           select new TipoAlmacenInfo
                                                                                                      {
                                                                                                          TipoAlmacenID = tip.GetHashCode()
                                                                                                      }).ToList()
                                                                   };

                        skAyudaDestino.Contexto = Contexto.Almacen;
                        skAyudaDestino.ObjetoNegocio = new AlmacenPL();
                        skAyudaDestino.AyudaLimpia += (o, args) => AyudaLimpiaDestino();
                        //skAyudaDestino.IsEnabled = true;

                        skAyudaDestino.AceptaSoloNumeros = true;
                        skAyudaDestino.MaximoCaracteres = 6;
                        skAyudaDestino.AnchoDescripcion = 150;
                        skAyudaDestino.MensajeClaveInexistenteBusqueda =
                            Properties.Resources.AyudaAlmacen_CodigoInvalido;
                        skAyudaDestino.ConceptoBusqueda = Properties.Resources.AyudaAlmacen_LeyendaBusqueda;
                        skAyudaDestino.TituloBusqueda = Properties.Resources.AyudaAlmacen_Busqueda_Titulo;
                        skAyudaDestino.MensajeAgregarBusqueda = Properties.Resources.AyudaAlmacen_Seleccionar;
                        skAyudaDestino.MensajeCerrarBusqueda = Properties.Resources.AyudaAlmacen_SalirSinSeleccionar;
                        skAyudaDestino.EncabezadoClaveBusqueda = Properties.Resources.AyudaAlmacen_Grid_Clave;
                        skAyudaDestino.EncabezadoDescripcionBusqueda =
                            Properties.Resources.AyudaAlmacen_Grid_Descripcion;
                        skAyudaDestino.MetodoInvocacion = "ObtenerPorIdFiltroTipoAlmacen";
                        skAyudaDestino.MetodoInvocacionBusqueda = "ObtenerPorOrganizacionTipoAlmacen";
                        skAyudaDestino.CampoDescripcion = "Descripcion";
                        skAyudaDestino.CampoClave = "AlmacenID";
                        skAyudaDestino.CampoLlaveOcultaClave = "AlmacenID";
                        skAyudaDestino.EsBindeable = true;

                        skAyudaDestino.AsignarFoco();
                    }
                    break;
                case FamiliasEnum.Combustibles:

                    skAyudaDestino = new ControlAyuda();
                    filtroFamilias = string.Format("{0}", FamiliasEnum.Combustibles.GetHashCode());
                    Contexto.CamionReparto = new CamionRepartoInfo
                                                 {
                                                     Organizacion =
                                                         new OrganizacionInfo { OrganizacionID = usuario.OrganizacionID }
                                                 };
                    skAyudaDestino.Contexto = Contexto.CamionReparto;
                    skAyudaDestino.ObjetoNegocio = new CamionRepartoBL();
                    skAyudaDestino.AyudaLimpia += (o, args) => AyudaLimpiaDestino();
                    //skAyudaDestino.IsEnabled = true;

                    skAyudaDestino.AceptaSoloNumeros = true;
                    skAyudaDestino.MaximoCaracteres = 8;
                    skAyudaDestino.AnchoDescripcion = 0;
                    skAyudaDestino.MensajeClaveInexistenteBusqueda =
                        Properties.Resources.AyudaCamionReparto_CodigoInvalido;
                    skAyudaDestino.ConceptoBusqueda = Properties.Resources.AyudaCamionReparto_LeyendaBusqueda;
                    skAyudaDestino.TituloBusqueda = Properties.Resources.AyudaCamionReparto_Busqueda_Titulo;
                    skAyudaDestino.MensajeAgregarBusqueda = Properties.Resources.AyudaCamionReparto_Seleccionar;
                    skAyudaDestino.MensajeCerrarBusqueda = Properties.Resources.AyudaCamionReparto_SalirSinSeleccionar;
                    skAyudaDestino.EncabezadoClaveBusqueda = Properties.Resources.AyudaCamionReparto_Grid_Clave;
                    skAyudaDestino.EncabezadoDescripcionBusqueda =
                        Properties.Resources.AyudaCamionReparto_Grid_Descripcion;
                    skAyudaDestino.MetodoInvocacion = "ObtenerPorID";
                    skAyudaDestino.MetodoInvocacionBusqueda = "ObtenerPorPagina";
                    skAyudaDestino.CampoDescripcion = "NumeroEconomico";
                    skAyudaDestino.CampoClave = "CamionRepartoID";
                    skAyudaDestino.CampoLlaveOcultaClave = "CamionRepartoID";
                    break;
                default:
                    skAyudaDestino = new ControlAyuda { IsEnabled = false };
                    requiereAyuda = false;
                    break;
            }
            stpAyuda.Children.Clear();
            stpAyuda.Children.Add(skAyudaDestino);
            skAyudaDestino.AyudaConDatos += (o, args) => AyudaConDatosDestino();
            skAyudaDestino.TabIndex = 4;
            skAyudaDestino.AsignarFoco();
            skAyudaDestino.IsEnabled = false;
        }

        private void HabilitaDestino()
        {
            skAyudaDestino.IsEnabled = (Contexto.Almacen == null || Contexto.Almacen.AlmacenID == 0);
            skAyudaDestino.AsignarFoco();
            btnAgregar.IsEnabled = true;
        }


        /// <summary>
        /// Valida que el almacén sea de tipo Almacén General
        /// </summary>
        private void ValidaCentroCostoUsuario()
        {
            centroCostoId = ObtenerPorUsuarioAccion(usuario.UsuarioID, false);
            if (centroCostoId == 0)
            {
                skAyudaFolio.IsEnabled = false;
                skAyudaProducto.IsEnabled = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.
                                      SolicitudProductosAlmacen_MsgNoExisteCentroCosto,
                                  MessageBoxButton.OK, MessageImage.Stop);
            }
        }


        #endregion
    }
}
