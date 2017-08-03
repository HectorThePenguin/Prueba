using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para DatosProgramacionBasculaMateriaPrima.xaml
    /// </summary>
    public partial class DatosProgramacionBasculaMateriaPrima
    {
        #region Propiedades
        private SKAyuda<ProveedorInfo> skAyudaProveedores;
        private SKAyuda<ChoferInfo> skAyudaChofer;
        private SKAyuda<CamionInfo> skAyudaCamion;
        private ProveedorInfo proveedor = new ProveedorInfo();
        private ChoferInfo chofer = new ChoferInfo();
        private CamionInfo camion = new CamionInfo();
        public bool banderaBascula;
        public PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo = new PesajeMateriaPrimaInfo();
        private ParametrosDetallePedidoInfo parametrosDetallePedidoInfo;

        public bool BanderaPesaje
        {
            get { return banderaBascula; }
            set { banderaBascula = value; }
        }

        public PesajeMateriaPrimaInfo PesajeMateriaPrimaInfoP
        {
            get { return pesajeMateriaPrimaInfo; }
            set { pesajeMateriaPrimaInfo = value; }
        }

        public int productoID;

        #endregion

        #region Constructor
        public DatosProgramacionBasculaMateriaPrima(ParametrosDetallePedidoInfo parametrosDetallePedidoInfoParam)
        {
            InitializeComponent();
            parametrosDetallePedidoInfo = parametrosDetallePedidoInfoParam;
            productoID = parametrosDetallePedidoInfo.Producto.ProductoId;
            AgregarAyudaProveedor();
            AgregarAyudaChofer();
            AgregarAyudaCamion();
            CargarDatosProducto();
            CargarDatosGrid();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento onloaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatosProgramacionBasculaMateriaPrima_OnLoaded(object sender, RoutedEventArgs e)
        {
            skAyudaProveedores.AsignarFoco();
        }

        /// <summary>
        /// Evento click de editar en grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPesar_Click(object sender, RoutedEventArgs e)
        {
            var botonPesar = (Button)e.Source;
            try
            {
                var resultadoValidacion = ValidarDatosProgramacion();
                if (resultadoValidacion.Resultado)
                {
                    banderaBascula = true;
                    pesajeMateriaPrimaInfo.CamionID = Convert.ToInt32(skAyudaCamion.Clave);
                    var proveedorChoferPl = new ProveedorChoferPL();
                    var proveedorPl = new ProveedorPL();
                    proveedor =
                        proveedorPl.ObtenerPorCodigoSAP(new ProveedorInfo() { CodigoSAP = skAyudaProveedores.Clave });
                    var proveedorChoferInfo =
                        proveedorChoferPl.ObtenerProveedorChoferPorProveedorIdChoferId(proveedor.ProveedorID, Convert.ToInt32(skAyudaChofer.Clave));
                    pesajeMateriaPrimaInfo.ProveedorChoferID = proveedorChoferInfo.ProveedorChoferID;
                    var programacionMateriaPrimaInfo =
                        (ProgramacionMateriaPrimaInfo)Extensor.ClonarInfo(botonPesar.CommandParameter);
                    pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID =
                        programacionMateriaPrimaInfo.ProgramacionMateriaPrimaId;
                    Close();
                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje) ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                    botonPesar.Focus();
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorPesar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorPesar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorPesar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo que carga los datos de la ayuda
        /// </summary>
        private void AgregarAyudaProveedor()
        {
            var proveedorInfo = new ProveedorInfo
            { 
                CodigoSAP = "",
                Descripcion = "",
                ProductoID = productoID,
                OrganizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"])
            };
            skAyudaProveedores = new SKAyuda<ProveedorInfo>(200, false, proveedorInfo
                                                   , "PropiedadClaveBasculaFleteInternoProducto"
                                                   , "PropiedadDescripcionBasculaFleteInternoProducto"
                                                   , "", false, 80, 10, true)
            {
                AyudaPL = new ProveedorPL(),
                MensajeClaveInexistente = Properties.Resources.DatosProgramacionBasculaMateriaPrima_AyudaTransportistaCodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.DatosProgramacionBasculaMateriaPrima_AyudaTransportistaSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.DatosProgramacionBasculaMateriaPrima_AyudaTransportistaSeleccionar,
                TituloEtiqueta = Properties.Resources.DatosProgramacionBasculaMateriaPrima_AyudaTransportistaLeyendaAyuda,
                TituloPantalla = Properties.Resources.DatosProgramacionBasculaMateriaPrima_AyudaTransportistaTitulo,
                MetodoPorDescripcion = "ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus"
            };
            skAyudaProveedores.ObtenerDatos += ObtenerDatosProveedor;
            SplAyudaProveedor.Children.Clear();
            SplAyudaProveedor.Children.Add(skAyudaProveedores);
        }

        /// <summary>
        /// Obtiene los datos del proveedor con la clave obtenida en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProveedor(string clave)
        {
            try
            {
                skAyudaChofer.LimpiarCampos();
                skAyudaCamion.LimpiarCampos();
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaProveedores.Info == null)
                {
                    return;
                }
                skAyudaProveedores.Info = new ProveedorInfo
                {
                    CodigoSAP = "",
                    Descripcion = "",
                    ProductoID = productoID,
                    OrganizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"])
                };
                var proveedorPl = new ProveedorPL();
                proveedor = proveedorPl.ObtenerPorCodigoSAP(new ProveedorInfo() {CodigoSAP = skAyudaProveedores.Clave});
                chofer.ProveedorChoferID = proveedor.ProveedorID;
                camion.Proveedor = proveedor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene los choferes para el proveedor seleccionado
        /// </summary>
        private void AgregarAyudaChofer()
        {
            chofer = new ChoferInfo
            {
                ChoferID = 0,
                ProveedorChoferID = proveedor.ProveedorID
            };
            skAyudaChofer = new SKAyuda<ChoferInfo>(200, false, chofer
                                                   , "PropiedadClaveEntradaGanado"
                                                    , "PropiedadDescripcionEntradaGanado", 
                                                    "", false, 80, 9, true)
            {
                AyudaPL = new ChoferVigilanciaPL(),
                MensajeClaveInexistente = Properties.Resources.BasculaDeMateriaPrima_AyudaChoferInvalido,
                MensajeBusquedaCerrar = Properties.Resources.BasculaDeMateriaPrima_AyudaChoferSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.BasculaDeMateriaPrima_AyudaChoferBusqueda,
                MensajeAgregar = Properties.Resources.BasculaDeMateriaPrima_AyudaChoferSeleccionar,
                TituloEtiqueta = Properties.Resources.AyudaProveedorChofer_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaProveedorChofer_Busqueda_Titulo,
            };
            skAyudaChofer.ObtenerDatos += ObtenerDatosChofer;
            SplAyudaChofer.Children.Clear();
            SplAyudaChofer.Children.Add(skAyudaChofer);
        }

        /// <summary>
        /// Obtiene los datos del chofer obtenido en la ayuda de chofer
        /// </summary>
        /// <param name="valor"></param>
        private void ObtenerDatosChofer(String valor)
        {
            try
            {
                skAyudaChofer.Info.ProveedorChoferID = skAyudaProveedores.Info.ProveedorID;
                chofer.ProveedorChoferID = proveedor.ProveedorID;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Control ayuda camion
        /// </summary>
        private void AgregarAyudaCamion()
        {
            camion = new CamionInfo
            {
                CamionID = 0,
                Proveedor = new ProveedorInfo() { ProveedorID = proveedor.ProveedorID }
            };

            skAyudaCamion = new SKAyuda<CamionInfo>(200, false, camion
                                                   , "PropiedadClaveBasculaMateriaPrima"
                                                   , "PropiedadDescripcionBasculaMateriaPrima", 
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new CamionPL(),
                MensajeClaveInexistente = Properties.Resources.BasculaDeMateriaPrima_AyudaCamionInvalido,
                MensajeBusquedaCerrar = Properties.Resources.BasculaDeMateriaPrima_AyudaCamionSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaProveedorPlacasCamion_Busqueda,
                MensajeAgregar = Properties.Resources.BasculaDeMateriaPrima_AyudaCamionSeleccionar,
                TituloEtiqueta = Properties.Resources.AyudaProveedorPlacasCamion_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaProveedorPlacasCamion_Busqueda_Titulo,
            };
            skAyudaCamion.ObtenerDatos += ObtenerDatosCamion;
            SplAyudaCamion.Children.Clear();
            SplAyudaCamion.Children.Add(skAyudaCamion);
        }

        /// <summary>
        /// Obtiene los datos del camion obtenido en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCamion(String clave)
        {
            try
            {
                skAyudaCamion.Info.Proveedor = skAyudaProveedores.Info;
                camion.Proveedor = proveedor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Valida que se tenga seleccionado proveedor, chofer y camion
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarDatosProgramacion()
        {
            var resultado = new ResultadoValidacion();
            if (String.IsNullOrEmpty(skAyudaProveedores.Clave.Trim()) && String.IsNullOrEmpty(skAyudaProveedores.Descripcion.Trim()))
            {
                skAyudaProveedores.AsignarFoco();
                resultado.Mensaje = Properties.Resources.DatosProgramacionBasculaMateriaPrima_MensajeSeleccionarProveedor;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaChofer.Clave.Trim()) && String.IsNullOrEmpty(skAyudaChofer.Descripcion.Trim()))
            {
                skAyudaChofer.AsignarFoco();
                resultado.Mensaje = Properties.Resources.DatosProgramacionBasculaMateriaPrima_MensajeSeleccionarChofer;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaCamion.Clave.Trim()) && String.IsNullOrEmpty(skAyudaCamion.Descripcion.Trim()))
            {
                skAyudaCamion.AsignarFoco();
                resultado.Mensaje = Properties.Resources.DatosProgramacionBasculaMateriaPrima_MensajeSeleccionarCamion;
                resultado.Resultado = false;
                return resultado;
            }
            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Se cargan los datos del producto en la primer pestania
        /// </summary>
        private void CargarDatosProducto()
        {
            try
            {
                TxtDescripcionSubfamilia.Text =
                    Convert.ToString(parametrosDetallePedidoInfo.Producto.SubFamilia.Descripcion);
                TxtProducto.Text = Convert.ToString(parametrosDetallePedidoInfo.Producto.ProductoDescripcion);
                TxtCantidadSolicitada.Value = (int?) parametrosDetallePedidoInfo.CantidadSolicitada;
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DatosProgramacionBasculaMateriaPrima_ErrorCargarDatos, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DatosProgramacionBasculaMateriaPrima_ErrorCargarDatos, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DatosProgramacionBasculaMateriaPrima_ErrorCargarDatos, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga los datos del grid
        /// </summary>
        private void CargarDatosGrid()
        {
            try
            {
                gridDetallePedido.ItemsSource = parametrosDetallePedidoInfo.ProgramacionMateriaPrima;
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DatosProgramacionBasculaMateriaPrima_ErrorCargarGridProgramacion, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DatosProgramacionBasculaMateriaPrima_ErrorCargarGridProgramacion, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DatosProgramacionBasculaMateriaPrima_ErrorCargarGridProgramacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion
    }
}
