using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CrystalDecisions.CrystalReports.Engine;
using SAPBusinessObjects.WPF.Viewer;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteAuxiliarInventarioMateriaPrima.xaml
    /// </summary>
    public partial class ReporteAuxiliarInventarioMateriaPrima
    {
        #region Atributos
        //private ProductoInfo productoSeleccionado;
        //private AlmacenInfo almacenSeleccionado;
        //private AlmacenInventarioLoteInfo loteSeleccionado;

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FiltroReporteAuxiliarMP Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroReporteAuxiliarMP)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto = new FiltroReporteAuxiliarMP
                           {
                               Organizacion = new OrganizacionInfo(),
                               Almacen = new AlmacenInfo
                                             {
                                                 ListaTipoAlmacen = new List<TipoAlmacenInfo>
                                                            {
                                                                //Agregar el resto de tipo almacen
                                                                new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.GeneralGanadera },
                                                                new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.MateriasPrimas },
                                                                new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.BodegaDeTerceros },
                                                                new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.PlantaDeAlimentos },
                                                                new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.EnTránsito },
                                                            },
                                             },
                               Producto = new ProductoInfo()
                           };
        }

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ReporteAuxiliarInventarioMateriaPrima()
        {
            InitializeComponent();
            skAyudaAlmacen.ObjetoNegocio = new AlmacenPL();
            skAyudaProducto.ObjetoNegocio = new ProductoPL();
            //CargarDatosUsuario();
            InicializaContexto();
            CargaOrganizaciones();
            skAyudaAlmacen.AyudaConDatos += (sender, args) =>
                                                {
                                                    Contexto.Almacen.ListaTipoAlmacen = new List<TipoAlmacenInfo>
                                                                                            {
                                                                                                //Agregar el resto de tipo almacen
                                                                                                new TipoAlmacenInfo{TipoAlmacenID =(int)TipoAlmacenEnum.GeneralGanadera},
                                                                                                new TipoAlmacenInfo{TipoAlmacenID =(int)TipoAlmacenEnum.MateriasPrimas},
                                                                                                new TipoAlmacenInfo{TipoAlmacenID =(int)TipoAlmacenEnum.BodegaDeTerceros},
                                                                                                new TipoAlmacenInfo{TipoAlmacenID =(int)TipoAlmacenEnum.PlantaDeAlimentos},
                                                                                                new TipoAlmacenInfo{TipoAlmacenID =(int)TipoAlmacenEnum.EnTránsito},
                                                                                            };

                                                };

        }
        #endregion
        #region Metodos
        ///// <summary>
        ///// Inicializad datos de usuario
        ///// </summary>
        //private void CargarDatosUsuario()
        //{
        //    //organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
        //    //loteSeleccionado = null;
        //    CargarAyudas();
        //}

        ///// <summary>
        ///// Cargar ayudas
        ///// </summary>
        //private void CargarAyudas()
        //{
        //    //CargarAyudaProducto();
        //    //CargarAyudaAlmacenes();
        //}

        ///// <summary>
        ///// Obtener inicializador de la ayuda de producto
        ///// </summary>
        ///// <returns></returns>
        //private ProductoInfo ObtenerProductoInfoAyuda()
        //{
        //    var productoInfo = new ProductoInfo()
        //    {   
        //        FamiliaId = FamiliasEnum.MateriaPrimas.GetHashCode(),
        //        Familia = new FamiliaInfo()
        //        {
        //            Activo = EstatusEnum.Activo, FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()

        //        },
        //        Activo = EstatusEnum.Activo
        //    };

        //    return productoInfo;
        //}

        ///// <summary>
        ///// Obtener inicializador de ayuda de almacen
        ///// </summary>
        ///// <returns></returns>
        //private AlmacenInfo ObtenerAlmacenInfoAyuda()
        //{
        //    var almacen = new AlmacenInfo
        //    {
        //        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
        //        ListaTipoAlmacen = new List<TipoAlmacenInfo>
        //                {
        //                    //Agregar el resto de tipo almacen
        //                    new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.GeneralGanadera },
        //                    new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.MateriasPrimas },
        //                    new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.BodegaDeTerceros },
        //                    new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.PlantaDeAlimentos },
        //                    new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.EnTránsito },
        //                },
        //        Activo = EstatusEnum.Activo
        //    };

        //    return almacen;
        //}

        ///// <summary>
        ///// Carga ayudas de almacenes
        ///// </summary>
        //private void CargarAyudaAlmacenes()
        //{

        //    skAyudaAlmacen = new SKAyuda<AlmacenInfo>(200, false, ObtenerAlmacenInfoAyuda()
        //                                           , "PropiedadClaveRegistrarProgracionFletesInterna"
        //                                           , "PropiedadDescripcionRegistrarProgramacionFletesInterna",
        //                                           "", false, 80, 9, true)
        //    {
        //        AyudaPL = new AlmacenPL(),
        //        MensajeClaveInexistente = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaAlmacenInvalido,
        //        MensajeBusquedaCerrar = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaAlmacenSalirSinSeleccionar,
        //        MensajeBusqueda = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_Busqueda,
        //        MensajeAgregar = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaAlmacenSeleccionar,
        //        TituloEtiqueta = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaLeyendaAlmacen,
        //        TituloPantalla = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaAlmacenTitulo,
        //    };

        //    skAyudaAlmacen.ObtenerDatos += ObtenerDatosAlmacen;
        //    skAyudaAlmacen.LlamadaMetodosNoExistenDatos += NoExistenAlmancen;
        //    splAyudaAlmacen.Children.Clear();
        //    splAyudaAlmacen.Children.Add(skAyudaAlmacen);
        //}

        //private void NoExistenAlmancen()
        //{
        //    almacenSeleccionado = null;
        //}

        ///// <summary>
        ///// Obtiene los datos del almacen
        ///// </summary>
        ///// <param name="clave"></param>
        //private void ObtenerDatosAlmacen(string clave)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(clave))
        //        {
        //            almacenSeleccionado = null;
        //            return;
        //        }
        //        //if (skAyudaAlmacen.Info == null)
        //        //{
        //        //    almacenSeleccionado = null;
        //        //    return;
        //        //}
        //        //if (skAyudaAlmacen.Info != null)
        //        //{
        //        //    skAyudaAlmacen.Info = ObtenerAlmacenInfoAyuda();
        //        //    var almacenPl = new AlmacenPL();

        //        //    almacenSeleccionado = almacenPl.ObtenerPorID(Convert.ToInt32(skAyudaAlmacen.Clave));

        //        //}
        //        //else
        //        //{
        //        //    skAyudaAlmacen.Info = ObtenerAlmacenInfoAyuda();
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
        //            Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MensajeErrorAyudaAlmacen
        //            , MessageBoxButton.OK,
        //            MessageImage.Error);
        //    }
        //}

        ///// <summary>
        ///// Carga la ayuda de productos
        ///// </summary>
        //private void CargarAyudaProducto()
        //{

        //skAyudaProducto = new SKAyuda<ProductoInfo>(200, false, new ProductoInfo()
        //                                       , "PropiedadClaveReporteInventarioMateriaPrima"
        //                                       , "PropiedadDescripcionReporteInventarioMateriaPrima",
        //                                       "", false, 80, 9, true)
        //    {
        //        AyudaPL = new ProductoPL(),
        //        MensajeClaveInexistente = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaProductoInvalido,
        //        MensajeBusquedaCerrar = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaProductoSalirSinSeleccionar,
        //        MensajeBusqueda = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaProductoBusqueda,
        //        MensajeAgregar = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaProductoSeleccionar, 
        //        TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProducto,
        //        TituloPantalla = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_AyudaProductoTitulo,
        //    };
        //    skAyudaProducto.ObtenerDatos += ObtenerDatosProducto;
        //    skAyudaProducto.LlamadaMetodosNoExistenDatos += NoExisteProducto;
        //    splAyudaProducto.Children.Clear();
        //    splAyudaProducto.Children.Add(skAyudaProducto);
        //}

        ///// <summary>
        ///// No existe dato producto
        ///// </summary>
        //private void NoExisteProducto()
        //{
        //    productoSeleccionado = null;
        //}

        ///// <summary>
        ///// Obtiene los datos del producto para generar inventario
        ///// </summary>
        ///// <param name="filtro"></param>
        //private void ObtenerDatosProducto(string filtro)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(filtro))
        //        {
        //            productoSeleccionado = null;
        //            return;
        //        }
        //        //if (skAyudaProducto.Info == null)
        //        //{
        //        //    productoSeleccionado = null;
        //        //    return;
        //        //}

        //        //if (skAyudaProducto.Info != null)
        //        //{
        //        //    productoSeleccionado = skAyudaProducto.Info;

        //        //    skAyudaProducto.Info = new ProductoInfo();//ObtenerProductoInfoAyuda();
        //        //}
        //        else
        //        {
        //            productoSeleccionado = null;
        //            //Enviar mensaje
        //            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
        //                Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MensajeProducto, MessageBoxButton.OK,
        //                MessageImage.Warning);
        //            skAyudaProducto.LimpiarCampos();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
        //        Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MensajeErrorObtenerDatosProducto, MessageBoxButton.OK,
        //        MessageImage.Error);
        //    }
        //}



        /// <summary>
        /// Limpiar campos del formulario
        /// </summary>
        private void LimpiarCampos()
        {
            //productoSeleccionado = null;
            //almacenSeleccionado = null;
            txtLote.Text = string.Empty;

            skAyudaAlmacen.LimpiarCampos();
            skAyudaProducto.LimpiarCampos();

            //skAyudaAlmacen.Info = ObtenerAlmacenInfoAyuda();
            //skAyudaProducto.Info = new ProductoInfo();

            DtpFechaInicial.SelectedDate = null;
            DtpFechaFinal.SelectedDate = null;

            btnGenerar.IsEnabled = false;
            //loteSeleccionado = null;
        }

        /// <summary>
        /// Método para validar que la fecha inicial no sea mayor a la actual.
        /// </summary>
        private bool ValidaFechaInicial()
        {
            bool result = true;
            DateTime? fecha = DtpFechaInicial.SelectedDate.HasValue
                                  ? DtpFechaInicial.SelectedDate
                                  : null;
            DateTime? fechaFinal = DtpFechaFinal.SelectedDate;

            if (fecha != null && (fecha > DateTime.Today || fecha > fechaFinal))
            {
                result = false;
            }

            if (!ValidarPeriodo())
            {
                MostrarMensajePeriodoInvalido();
                return false;
            }

            return result;
        }



        /// <summary>
        /// VAlidar el periodo de fechas a dos años
        /// </summary>
        /// <returns></returns>
        private bool ValidarPeriodo()
        {
            bool retValue = true;

            if (DtpFechaInicial.SelectedDate != null && DtpFechaFinal.SelectedDate != null)
            {

                var zeroTime = new DateTime(1, 1, 1);
                var fechaInicial = DtpFechaInicial.SelectedDate.Value;
                var fechaFinal = DtpFechaFinal.SelectedDate.Value;

                if (fechaFinal > fechaInicial)
                {
                    var intervalo = fechaFinal - fechaInicial;
                    int years = (zeroTime + intervalo).Year - 1;

                    if (years >= 2)
                        retValue = false;
                }
            }
            return retValue;
        }



        /// <summary>
        /// Muestra el mensaje de periodo no valido
        /// </summary>
        private void MostrarMensajePeriodoInvalido()
        {
            string mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgFechaRangoDosAnios;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaFinal()
        {
            string mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgFechaInicialMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }



        /// <summary>
        /// Método para validar los controles de la pantalla.
        /// </summary>
        /// <returns></returns>
        private bool ValidaGenerar()
        {
            bool resultado = true;
            try
            {

                string mensaje = string.Empty;

                DateTime? fechaIni = DtpFechaInicial.SelectedDate;
                DateTime? fechaFin = DtpFechaFinal.SelectedDate;

                //if (productoSeleccionado == null || productoSeleccionado.ProductoId == 0)
                if (Contexto.Producto == null || Contexto.Producto.ProductoId == 0)
                {
                    mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgSelecioneProducto;
                    skAyudaProducto.Focus();
                }
                //else if (almacenSeleccionado == null || almacenSeleccionado.AlmacenID == 0)
                else if (Contexto.Almacen == null || Contexto.Almacen.AlmacenID == 0)
                {
                    mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgSelecioneAlmacen;
                    skAyudaAlmacen.Focus();
                }
                else if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgFechaInicialMayorFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }

                else if (fechaFin > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgFechaFinalMayorFechaActual;
                    DtpFechaInicial.Focus();
                }

                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgFechaInicialMayorFechaFinal;
                    DtpFechaFinal.Focus();
                }

                if (!string.IsNullOrWhiteSpace(mensaje))
                {
                    resultado = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_FalloValidaGenerar,
                                      MessageBoxButton.OK, MessageImage.Warning);
            }

            return resultado;
        }

        /// <summary>
        /// Funcion para generar el reporte y validar
        /// </summary>
        private void Generar()
        {
            if (ValidaGenerar())
            {
                GenerarReporte();
            }
        }
        /// <summary>
        /// Genera la informacion y muestra el reporte
        /// </summary>
        private void GenerarReporte()
        {
            try
            {

                DateTime fechaIni = DtpFechaInicial.SelectedDate.HasValue
                                        ? DtpFechaInicial.SelectedDate.Value
                                        : new DateTime();
                DateTime fechaFin = DtpFechaFinal.SelectedDate.HasValue
                                        ? DtpFechaFinal.SelectedDate.Value
                                        : fechaIni;


                var loteId = 0;
                //if (txtLote.Text != string.Empty)
                if (Contexto.AlmacenInventarioLote != null)
                {
                    //loteId = int.Parse(txtLote.Text);
                    loteId = Contexto.AlmacenInventarioLote.AlmacenInventarioLoteId;
                }

                var reportePl = new ReporteInventarioMateriaPrimaPL();
                IList<ReporteInventarioMateriaPrimaInfo> resultadoInfo =
                    reportePl.GenerarReporteInventario(Contexto.Almacen.Organizacion.OrganizacionID, Contexto.Producto.ProductoId,
                                                        Contexto.Almacen.AlmacenID, loteId, fechaIni, fechaFin);

                if (resultadoInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }



                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(Contexto.Almacen.Organizacion.OrganizacionID);
                var nombreOrganizacion = organizacion != null ? organizacion.Division : String.Empty;
                var encabezado = new ReporteEncabezadoInfo
                {
                    Titulo = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_TituloReporte,
                    FechaInicio = fechaIni,
                    FechaFin = fechaFin,
                    Organizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + nombreOrganizacion + ")"
                };

                foreach (var dato in resultadoInfo)
                {
                    dato.Titulo = encabezado.Titulo;
                    dato.FechaInicio = encabezado.FechaInicio;
                    dato.FechaFin = encabezado.FechaFin;
                    dato.Organizacion = encabezado.Organizacion;
                }

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteInventarioMateriaPrimas.rpt");
                documento.Load(reporte);


                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();


                var forma = new ReportViewer(documento, encabezado.Titulo) { rptReportViewerControl = { ToggleSidePanel = Constants.SidePanelKind.None } };
                forma.MostrarReporte();
                forma.Show();


            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_FalloCargarReporte,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_FalloCargarReporte,
                                  MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Método para validar que la fecha inicial no sea mayor a la actual.
        /// </summary>
        private bool ValidaFechaFinal()
        {
            bool result = true;
            DateTime? fecha = DtpFechaFinal.SelectedDate.HasValue
                                  ? DtpFechaFinal.SelectedDate
                                  : null;
            if (fecha != null && (fecha > DateTime.Today || fecha < DtpFechaInicial.SelectedDate))
            {
                result = false;
            }



            if (!ValidarPeriodo())
            {
                MostrarMensajePeriodoInvalido();
                return false;
            }
            return result;

        }



        /// <summary>
        /// Muestra el mensaje de fecha final mayor a la actual
        /// </summary>
        private void MostrarMensajeFechaFinalMayorActual()
        {
            string mensaje = Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgFechaFinalMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Valida el lote ingresado
        /// </summary>
        private void ValidarLote()
        {
            try
            {
                if (txtLote.Text == string.Empty)
                {
                    Contexto.AlmacenInventarioLote = null;
                    return;
                }

                var loteId = int.Parse(txtLote.Text);

                var lotePl = new AlmacenInventarioLotePL();

                //Validar producto seleccionado
                //if (productoSeleccionado == null || productoSeleccionado.ProductoId == 0)
                if (Contexto.Producto == null || Contexto.Producto.ProductoId == 0)
                {

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgSelecioneProducto, MessageBoxButton.OK,
                       MessageImage.Warning);
                    skAyudaProducto.Focus();
                    txtLote.Text = string.Empty;
                    return;
                }

                //Validar almacen seleccionado
                //if (almacenSeleccionado == null || almacenSeleccionado.AlmacenID == 0)
                if (Contexto.Almacen == null || Contexto.Almacen.AlmacenID == 0)
                {

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_MsgSelecioneAlmacen, MessageBoxButton.OK,
                       MessageImage.Warning);
                    skAyudaAlmacen.Focus();
                    txtLote.Text = string.Empty;
                    return;
                }

                var lotein = new AlmacenInventarioLoteInfo
                {
                    Lote = loteId,
                    //Activo = EstatusEnum.Activo,
                    OrganizacionId = Contexto.Almacen.Organizacion.OrganizacionID,
                    ProductoId = Contexto.Producto.ProductoId,
                    TipoAlmacenId = Contexto.Almacen.TipoAlmacen.TipoAlmacenID,
                    AlmacenInventario = new AlmacenInventarioInfo
                                            {
                                                AlmacenID = Contexto.Almacen.AlmacenID
                                            }
                };

                var loteObtenido = lotePl.ObtenerAlmacenInventarioLotePorFolioLote(lotein);

                if (loteObtenido != null && Contexto.Producto != null)
                {
                    if (loteObtenido.AlmacenInventario.AlmacenID != Contexto.Almacen.AlmacenID)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_LoteNoExiste, MessageBoxButton.OK,
                            MessageImage.Warning);
                        txtLote.Text = string.Empty;
                        txtLote.Focus();
                    }
                    else
                    {
                        Contexto.AlmacenInventarioLote = loteObtenido;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_LoteNoExiste, MessageBoxButton.OK,
                        MessageImage.Warning);
                    txtLote.Text = string.Empty;
                    txtLote.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                   Properties.Resources.ReporteAuxiliarInventarioMateriaPrima_FalloValidarLote, MessageBoxButton.OK,
                                   MessageImage.Warning);
                txtLote.Text = string.Empty;
                txtLote.Focus();
            }

        }

        /// <summary>
        /// Método para cargar las organizaciones
        /// </summary>
        /// <returns></returns>
        private void CargaOrganizaciones()
        {
            try
            {
                bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var organizacionesPL = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPL.ObtenerTipoGanaderas();
                if (usuarioCorporativo)
                {

                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReporteConsumoProgramadovsServido_cmbSeleccione,
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    //cmbOrganizacion.SelectedItem = organizacion0;
                    Contexto.Almacen.Organizacion = organizacion0;
                    cmbOrganizacion.IsEnabled = true;

                }
                else
                {
                    var organizacion0 =
                       new OrganizacionInfo
                       {
                           OrganizacionID = organizacionId,
                           Descripcion = nombreOrganizacion
                       };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    //cmbOrganizacion.SelectedItem = organizacion0;
                    Contexto.Almacen.Organizacion = organizacion0;
                    cmbOrganizacion.IsEnabled = false;
                    //btnGenerar.IsEnabled = true;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteConsumoProgramadovsServido_ErrorCargarCombos,
                                  MessageBoxButton.OK, MessageImage.Error);

            }
        }

        #endregion
        #region Eventos

        /// <summary>
        /// FEchas keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fechas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
                e.Handled = true;
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handler de la fecha inicial al perder el foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaInicial_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaInicial();

            if (isValid)
            {
                return;
            }

            var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
            DtpFechaFinal.MoveFocus(tRequest);

            if (DtpFechaInicial.SelectedDate > DtpFechaFinal.SelectedDate)
            {
                MostrarMensajeFechaInicialMayorFechaFinal();
            }
            else if (DtpFechaInicial.SelectedDate > DateTime.Today)
            {
                MostrarMensajeFechaInicialMayorFechaActual();
            }
            DtpFechaInicial.SelectedDate = null;
            DtpFechaFinal.SelectedDate = null;
            e.Handled = true;
            DtpFechaInicial.Focus();
        }

        /// <summary>
        /// Handler boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        /// <summary>
        /// Handler boton generar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Generar();
        }
        /// <summary>
        /// Cuando se pierde el foco del lote para validarlo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLote_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ValidarLote();
        }

        /// <summary>
        /// Entrada de solo numeros en el lote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLote_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// Evento de entrada del txtLote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLote_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if ((e.Key == Key.Enter || e.Key == Key.Tab) && !String.IsNullOrEmpty(txtLote.Text))
                {
                    ValidarLote();
                }
                else //Si el usuario no ingresó ningún caracter.....
                {
                    txtLote.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Handler de perdida de foco de fecha final
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaFinal_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaFinal();

            if (isValid)
            {
                btnGenerar.IsEnabled = true;
                return;
            }

            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            DtpFechaFinal.MoveFocus(tRequest);
            if (DtpFechaFinal.SelectedDate < DtpFechaInicial.SelectedDate)
            {
                MostrarMensajeFechaInicialMayorFechaFinal();
            }
            else if (DtpFechaFinal.SelectedDate > DateTime.Today)
            {
                MostrarMensajeFechaFinalMayorActual();
            }
            DtpFechaFinal.SelectedDate = null;
            DtpFechaInicial.SelectedDate = null;
            e.Handled = true;
            DtpFechaInicial.Focus();
        }
        #endregion

        private void cmbOrganizacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
