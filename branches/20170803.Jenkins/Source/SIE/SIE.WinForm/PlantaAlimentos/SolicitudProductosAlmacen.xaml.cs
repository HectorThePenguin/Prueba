using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.PlantaAlimentos
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

        private UsuarioInfo solictante;
        private AlmacenInfo almacen;
        private int autorizadorId; 
        private readonly SolicitudProductoBL solicitudProductoBL;

        #endregion

        #region Constructor
        public SolicitudProductosAlmacen()
        {
            solicitudProductoBL = new SolicitudProductoBL();
            InitializeComponent();
            InicializaContexto();
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
            skAyudaFolio.ObjetoNegocio = solicitudProductoBL;
            skAyudaFolio.AyudaConDatos += (o, args) => AyudaConDatosFolio();

            skAyudaProducto.Contexto = new ProductoInfo();
            skAyudaProducto.ObjetoNegocio = new ProductoPL();
            skAyudaProducto.AyudaConDatos += (o, args) => AyudaConDatosProducto();
            skAyudaProducto.AyudaLimpia += (o, args) => AyudaLimpiaProducto();

            skAyudaDestino.Contexto = new CentroCostoInfo();
            skAyudaDestino.ObjetoNegocio = new CentroCostoBL();
            skAyudaDestino.AyudaConDatos += (o, args) => AyudaConDatosDestino();
            skAyudaDestino.AyudaLimpia += (o, args) => AyudaLimpiaDestino();

            Limpiar();  
            Dispatcher.BeginInvoke(new Action(ValidaTipoAlmacen), DispatcherPriority.ContextIdle, null);
        }
        
        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar números letras sin acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtValidarSoloNumerosDecimalesPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string valor = ((DecimalUpDown) sender).Value.HasValue
                               ? ((DecimalUpDown) sender).Value.ToString()
                               : string.Empty;
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, valor);
        }

        private void TxtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                if (txtCantidad.IsEnabled)
                {
                    if (ValidaDisponibilidad())
                    {
                        skAyudaDestino.AsignarFoco();
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Almacen_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
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
            solictante = new UsuarioPL().ObtenerPorID(AuxConfiguracion.ObtenerUsuarioLogueado());
            solictante.OrganizacionID = solictante.Organizacion.OrganizacionID;
            almacen = ObtenerAlmacenGenerarl(solictante.Organizacion.OrganizacionID);
            autorizadorId = 0;
            Contexto =
                new SolicitudProductoInfo
                    {
                        OrganizacionID = solictante.OrganizacionID,
                        UsuarioSolicita = solictante,
                        UsuarioIDSolicita = solictante.UsuarioID,
                        Solicitud = InicializaContextoSolicitud(),
                        Detalle = new List<SolicitudProductoDetalleInfo>(),
                        DetalleGrid = new ObservableCollection<SolicitudProductoDetalleInfo>(),
                        FechaCreacion = DateTime.Today,
                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                        EstatusID = Estatus.SolicitudProductoPendiente.GetHashCode(),
                    };
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
                                    Usuario = solictante,
                                    OrganizacionID = solictante.OrganizacionID,
                                    Producto = new ProductoInfo(),
                                    Cantidad = decimal.Zero,
                                    CentroCosto = new CentroCostoInfo{ AutorizadorID = autorizadorId},
                                    ClaseCostoProducto = new ClaseCostoProductoInfo(),
                                    AlmacenID = almacen != null
                                                    ? almacen.AlmacenID
                                                    : 0
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

                Contexto.Detalle.ForEach(e => e.Eliminar = !isAutorizado);
                Contexto.DetalleGrid = Contexto.Detalle.ConvertirAObservable();

                gridDatos.ItemsSource = Contexto.Detalle.Where(e => e.Activo == EstatusEnum.Activo);
                skAyudaFolio.IsEnabled = false;
                btnGuardar.Content = Properties.Resources.btnActualizar;

                var centroCosto =  Contexto.Detalle.FirstOrDefault(c => c.Activo == EstatusEnum.Activo);
                if(centroCosto != null)
                {
                    autorizadorId = ObtenerCentroCostoAutorizador(centroCosto.CentroCostoID);
                    Contexto.Solicitud.CentroCosto.AutorizadorID = autorizadorId;
                }
            }
        }

        /// <summary>
        /// Asigna el valor al campo folio
        /// </summary>
        private void AyudaConDatosProducto()
        {
            var producto = skAyudaProducto.Contexto as ProductoInfo;
            if (producto != null && producto.ManejaLote)
            {
                skAyudaProducto.LimpiarCampos();
                skAyudaProducto.AsignarFoco();
                Contexto.Solicitud.ClaseCostoProducto = new ClaseCostoProductoInfo();
                Contexto.Solicitud.SinProducto = true;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SolicitudProductosAlmacen_MsgNoManejeLote,
                                MessageBoxButton.OK, MessageImage.Warning);
            }
            else
            {
                Contexto.Solicitud.ClaseCostoProducto = new ClaseCostoProductoBL().ObtenerPorProductoAlmacen(producto.ProductoId, almacen.AlmacenID);
                Contexto.Solicitud.SinProducto = false;
            }
            Contexto.Solicitud.Producto.SubfamiliaId = 0;
            Contexto.Solicitud.Producto.FamiliaId = 0;
            Contexto.Solicitud.Producto.UnidadId = 0;
        }

        /// <summary>
        /// Asigna el valor al campo folio
        /// </summary>
        private void AyudaConDatosDestino()
        {
            btnAgregar.IsEnabled = true;

            if (Contexto.Solicitud.CentroCosto.AutorizadorID == 0)
           {
               var centroCosto = skAyudaDestino.Contexto as CentroCostoInfo;
               int centroCostoId = centroCosto != null
                                       ? centroCosto.CentroCostoID
                                       : 0;
               autorizadorId = ObtenerCentroCostoAutorizador(centroCostoId);
               Contexto.Solicitud.CentroCosto.AutorizadorID = autorizadorId;
           }
            btnAgregar.IsEnabled = ActivaBotonAgregar();
        }

        /// <summary>
        /// Limpiar los valores de la ayuda de productos
        /// </summary>
        private void AyudaLimpiaProducto()
        {
            Contexto.Solicitud.SinProducto = true;
            Contexto.Solicitud.Cantidad = 0;
        }
        
        /// <summary>
        /// Limpiar los valores de la ayuda de destinos
        /// </summary>
        private void AyudaLimpiaDestino()
        {
            btnAgregar.IsEnabled = !string.IsNullOrWhiteSpace(skAyudaDestino.Clave);
            Contexto.Solicitud.CentroCosto.AutorizadorID = autorizadorId;
        }

        /// <summary>
        /// Obtiene una solicitud de productos por su folio de solicitud.
        /// </summary>
        /// <param name="solicitudId"></param>
        /// <returns></returns>
        private SolicitudProductoInfo ObtenerSolicitudPorId(int solicitudId)
        {
            SolicitudProductoInfo solicitud =
                solicitudProductoBL.ObtenerPorID(new SolicitudProductoInfo {SolicitudProductoID = solicitudId});
            solicitud.Solicitud = InicializaContextoSolicitud();
            solicitud.Solicitud.FolioID = solicitud.SolicitudProductoID;
            solicitud.Solicitud.FolioSolicitud = solicitud.FolioSolicitud;
            solicitud.Solicitud.Descripcion = string.Format("{0}", solicitud.FolioSolicitud);
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
                resultado = Contexto.Detalle.Any();
                if(!resultado)
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje, MessageBoxButton.OK, MessageImage.Warning);
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
                var centroCosto = skAyudaDestino.Contexto as CentroCostoInfo;
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
                else if (centroCosto == null || centroCosto.CentroCostoID == 0)
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
                    int folio = Contexto.FolioSolicitud;
                    solicitudProductoBL.Guardar(Contexto);
                    string mensaje = folio == 0
                                       ? string.Format(Properties.Resources.SolicitudProductosAlmacen_GuardadoConExito,
                                           Contexto.FolioSolicitud)
                                       : Properties.Resources.SolicitudProductosAlmacen_ActualizadoConExito;


                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje, MessageBoxButton.OK,
                                      MessageImage.Correct);

                    Limpiar();               
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SolicitudProductosAlmacen_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SolicitudProductosAlmacen_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
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
                InicializaContexto();               
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
                var producto =  Contexto.Solicitud.Producto ?? new ProductoInfo();
                var centroCosto = (CentroCostoInfo)Extensor.ClonarInfo(Contexto.Solicitud.CentroCosto);
                var claseCostoProducto = Contexto.Solicitud.ClaseCostoProducto ?? new  ClaseCostoProductoInfo();

                int productoId = producto == null ? 0 : producto.ProductoId;
                int centroCostoId = centroCosto == null ? 0 : centroCosto.CentroCostoID;
                decimal cantidad = txtCantidad.Value.HasValue
                                        ? txtCantidad.Value.Value
                                        : decimal.Zero;

                Contexto.DetalleGrid = Contexto.DetalleGrid ?? new ObservableCollection<SolicitudProductoDetalleInfo>(); 

                var registro = Contexto.DetalleGrid.FirstOrDefault(e => e.ProductoID == productoId && e.Activo == EstatusEnum.Activo);
                if (registro == null)
                {
                    registro = new SolicitudProductoDetalleInfo
                                   {
                                       SolicitudProductoID = Contexto.SolicitudProductoID,
                                       ProductoID = productoId,
                                       Producto = producto,
                                       Cantidad = cantidad,
                                       ClaseCostoProducto = claseCostoProducto,
                                       CentroCostoID = centroCostoId,
                                       CentroCosto =  centroCosto,
                                       EstatusID = Estatus.SolicitudProductoPendiente.GetHashCode(),
                                       Eliminar = !isAutorizado,
                                       Activo = EstatusEnum.Activo
                                   };

                    Contexto.DetalleGrid.Add(registro);
                }
                else
                {
                    registro.ProductoID = producto.ProductoId;
                    registro.Producto = producto;
                    registro.Cantidad = cantidad;
                    registro.CentroCostoID = centroCostoId;
                    registro.CentroCosto = centroCosto;
                    registro.ClaseCostoProducto =claseCostoProducto;
                }
                Contexto.Solicitud.Producto = new ProductoInfo();
                Contexto.Solicitud.Cantidad = 0;
                Contexto.Solicitud.CentroCosto = new CentroCostoInfo();
                Contexto.Detalle = Contexto.DetalleGrid.ToList();
                gridDatos.ItemsSource = Contexto.Detalle.Where(e=> e.Activo == EstatusEnum.Activo);
                Contexto.Guardar = true;
                LimpiarParcial();
            }
        }

        /// <summary>
        /// Limpia todos los controles de la pantalla.
        /// </summary>
        private void Limpiar()
        {
            InicializaContexto();
            btnImprimir.IsEnabled = false;
            Contexto.Guardar = false;
            btnGuardar.Content = Properties.Resources.btnGuardar;
            skAyudaFolio.IsEnabled = true;
            LimpiarParcial();
            gridDatos.ItemsSource = Contexto.DetalleGrid;
        }
        
        /// <summary>
        /// Limpia los datos de captura para el grid.
        /// </summary>
        private void LimpiarParcial()
        {
            skAyudaProducto.LimpiarCampos();
            skAyudaDestino.LimpiarCampos();
            skAyudaProducto.AsignarFoco();
            btnAgregar.IsEnabled = false;
        }

        /// <summary>
        /// Asigna Inactivo al registro para ocultarlo del grid.
        /// </summary>
        /// <param name="info"></param>
        private void Eliminar(SolicitudProductoDetalleInfo info)
        {
            info.Activo = EstatusEnum.Inactivo;
            Contexto.Detalle = Contexto.DetalleGrid.ToList();
            gridDatos.ItemsSource = Contexto.Detalle.Where(e => e.Activo == EstatusEnum.Activo);
            LimpiarParcial();
            Contexto.Guardar = true;
        }

        /// <summary>
        /// Obtiene el almacén general que tenga configurado
        /// el usuario.
        /// </summary>
        private AlmacenInfo ObtenerAlmacenGenerarl(int almacenId)
        {
            var almacenDaL = new AlmacenPL();
            AlmacenInfo almacenInfo = null;
            IList<AlmacenInfo> almacenes = almacenDaL.ObtenerAlmacenPorOrganizacion(almacenId);
            if(almacenes!= null && almacenes.Count > 0)
            {
                almacenInfo = almacenes.FirstOrDefault(a => a.TipoAlmacenID == (int) TipoAlmacenEnum.GeneralGanadera
                                                            && a.Activo == EstatusEnum.Activo);
            }
            return almacenInfo;
        }

        /// <summary>
        /// Valida que el almacén sea de tipo Almacén General
        /// </summary>
        private void ValidaTipoAlmacen()
        {
            if (almacen == null)
            {
                skAyudaFolio.IsEnabled = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                     Properties.Resources.
                                                         SolicitudProductosAlmacen_MsgNoExisteAlmacen,
                                                     MessageBoxButton.OK, MessageImage.Warning);
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
                var reporte = new System.Data.DataSet("ReporteSolicitudProductos");
                var solicitud = new System.Data.DataTable("Solicitud");

                var columnasSolicitud = new System.Data.DataColumn[]
                                            {
                                                new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtFolio, typeof (int)),
                                                new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtFechaSolicitud, typeof (DateTime)),
                                                new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtSolicito, typeof (string)),
                                                new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtAutorizo, typeof (string)),
                                            };

                solicitud.Columns.AddRange(columnasSolicitud);
                var rowSolicitud = solicitud.NewRow();
                rowSolicitud[Properties.Resources.SolicitudProductosAlmacen_DtFolio] = Contexto.FolioSolicitud;
                rowSolicitud[Properties.Resources.SolicitudProductosAlmacen_DtFechaSolicitud] = Contexto.FechaSolicitud;
                rowSolicitud[Properties.Resources.SolicitudProductosAlmacen_DtSolicito] = Contexto.UsuarioSolicita.Nombre;
                rowSolicitud[Properties.Resources.SolicitudProductosAlmacen_DtAutorizo] = Contexto.UsuarioAutoriza.Nombre;
                solicitud.Rows.Add(rowSolicitud);

                var detalle = new System.Data.DataTable("Detalle");
                var columnasDetalle = new System.Data.DataColumn[]
                                          {
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtFolio, typeof (int)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtCodigo, typeof (int)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtArticulo, typeof (string)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtCantidad, typeof (decimal)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtUnidadMedicion, typeof (string)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtDescripcion, typeof (string)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtClaseCosto, typeof (string)),
                                              new DataColumn(Properties.Resources.SolicitudProductosAlmacen_DtCentroCosto, typeof (string)),
                                          };
                detalle.Columns.AddRange(columnasDetalle);
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
                        newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtDescripcion] =
                            row.CentroCosto.Descripcion;
                        newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtClaseCosto] =
                            row.ClaseCostoProducto != null
                                ? row.ClaseCostoProducto.CuentaSAP.CuentaSAP
                                : string.Empty;
                        newDetalle[Properties.Resources.SolicitudProductosAlmacen_DtCentroCosto] =
                            row.CentroCosto.Descripcion;
                        detalle.Rows.Add(newDetalle);
                    }
                }

                reporte.Tables.Add(solicitud);
                reporte.Tables.Add(detalle);

                string nombreReporte = Properties.Resources.SolicitudProductosAlmacen_NombreReporte;
                var nombre = string.Format(@"{0}-{1:d8}.pdf", nombreReporte, Contexto.FolioSolicitud);

                //string archivoXML = string.Format("{0}.xml", nombreArchivo);
                //reporte.WriteXml(string.Format(@"c:\Reporte\{0}", archivoXML), XmlWriteMode.WriteSchema);

                using (var documento = new ReportDocument())
                {
                    using (var streaming = this.GetType().Assembly.GetManifestResourceStream(
                           string.Format("SIE.WinForm.Reporte.{0}.rpt",nombreReporte)))
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

                    var archivo = new FileStream(nombre, FileMode.Open);
                    archivo.Close();

                    documento.ExportToDisk(ExportFormatType.PortableDocFormat, nombreReporte);
                    documento.Close();
                }

                var dialogo = new SaveFileDialog
                                  {
                                      FileName = nombre,
                                      DefaultExt = ".pdf",
                                      Filter = "Documentos Pdf (*.pdf)|*.pdf"
                                  };

                bool? resultado = dialogo.ShowDialog();
                if (resultado == true)
                {
                    nombre = dialogo.FileName;
                    if (File.Exists(nombre))
                    {
                        try
                        {
                            var archivo = new FileStream(nombre, FileMode.Open);
                            archivo.Close();
                        }
                        catch (IOException)
                        {
                            //throw new ExcepcionServicio(Properties.Resources.ExportarExcel_ArchivoAbierto);
                        }
                    }
                }
                Mouse.SetCursor(Cursors.Arrow);
                string mensaje = Properties.Resources.SolicitudProductosAlmacen_MsgReporteGenerado;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje, MessageBoxButton.OK,
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
        /// Obtener el autorizador del centro de costo
        /// </summary>
        /// <param name="centroCostoId"></param>
        /// <returns></returns>
        private int ObtenerCentroCostoAutorizador(int centroCostoId)
        {
            var centroCostoAutoriadorBL = new CentroCostoAutorizadorBL();
            int result = 0;
            var info = centroCostoAutoriadorBL.ObtenerPorCentroCostoID(centroCostoId);
            if (info!= null)
            {
                result = info.UsuarioIDAutorizador;
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
                IList<AlmacenInventarioInfo> almacenInventario = new ProductoPL().ObtenerExistencia(almacen.AlmacenID,
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
                                     EstatusID = (int) Estatus.SolicitudProductoAutorizado,
                                     Activo = EstatusEnum.Activo
                                 };

                IList<SolicitudProductoInfo> solicitudes = solicitudProductoBL.ObtenerSolicitudesAutorizadas(filtro);

                if (solicitudes != null)
                {
                    disponibilidad =
                        solicitudes.SelectMany(sd => sd.Detalle)
                            .Where(sd => sd.ProductoID == productoId
                                         && sd.EstatusID == (int) Estatus.SolicitudProductoAutorizado
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
                decimal cantidad = Contexto.Solicitud.Cantidad;
                decimal existencia = ObtenerExistencia(producto.ProductoId);
                decimal disponibilidad = ObtenerDisponibilidad(producto.ProductoId);
                return (existencia - disponibilidad) > cantidad;
            }
            return false;
        }

        private bool ActivaBotonAgregar()
        {
            return ValidaAgregar(false);
        }
        
        #endregion
    }
}
