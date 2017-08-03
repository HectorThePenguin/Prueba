using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Facturas;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para OtrosCostos.xaml
    /// </summary>
    public partial class OtrosCostos
    {
        #region Atributos
        private decimal toneladasContrato = 0;
        private decimal toleranciaContrato = 0;
        public List<CostoInfo> listaCostos = new List<CostoInfo>();
        public ContratoInfo contratoInfo = new ContratoInfo();
        public bool Nuevo;
        public int CostoId = 0;
        #endregion

        #region Propiedades
        public List<CostoInfo> listaCostosR
        {
            get { return listaCostos; }
            set { listaCostos = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor que recibe un parametro decimal
        /// </summary>
        public OtrosCostos(decimal toneladas, List<CostoInfo> listaOtrosCostos, ContratoInfo contratoInfoP, bool NuevoP, decimal tolerancia)
        {
            InitializeComponent();
            toneladasContrato = toneladas;
            CargarAyudas();
            listaCostos = listaOtrosCostos;
            contratoInfo = contratoInfoP;
            Nuevo = NuevoP;
            toleranciaContrato = tolerancia;
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Cargar costos guardados anteriormente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowOtrosCostos_Loaded(object sender, RoutedEventArgs e)
        {
            DefinirPantalla();
            skAyudaCostos.AsignarFoco();
            skAyudaProveedor.IsEnabled = false;
        }

        /// <summary>
        /// Editar costo info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var costoInfo = (CostoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                BtnAgregar.Content = Properties.Resources.OtrosCostos_MensajeCosto;

                if (costoInfo == null) return;
                CostoId = costoInfo.CostoID;
                skAyudaCostos.Clave = costoInfo.ClaveContable;
                skAyudaCostos.Descripcion = costoInfo.Descripcion;
                skAyudaCostos.IsEnabled = false;
                TxtImporte.Value = costoInfo.ImporteCosto;
                TxtToneladas.Value = costoInfo.ToneladasCosto;
                if (costoInfo.Proveedor != null)
                {
                    RbProveedor.IsChecked = true;
                    skAyudaProveedor.Clave = costoInfo.Proveedor.CodigoSAP;
                    skAyudaProveedor.Descripcion = costoInfo.Proveedor.Descripcion;
                }
                if (costoInfo.CuentaSap != null)
                {
                    RbCuenta.IsChecked = true;
                    skayudaCuentaSAP.Clave = costoInfo.CuentaSap.CuentaSAP;
                    skayudaCuentaSAP.Descripcion = costoInfo.CuentaSap.Descripcion;
                }
                TxtImporte.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento click de btnCerrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

         ///<summary>
         /// Agrega un costo al grid
         ///</summary>
         ///<param name="sender"></param>
         ///<param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultadoValidacion = ValidarCampos();
                if (resultadoValidacion.Resultado)
                {
                    var cuentapl = new CuentaSAPPL();
                    var proveedorPl = new ProveedorPL();
                    var importeIngresado = TxtImporte.Value.HasValue ? TxtImporte.Value.Value : 0;
                    var toneladasIngresadas = TxtToneladas.Value.HasValue ? TxtToneladas.Value.Value : 0;
                    var cuentaInfo = new CuentaSAPInfo();
                    var proveedorInfoP = new ProveedorInfo();
                    if (RbCuenta.IsChecked == true)
                    {
                        cuentaInfo =
                            cuentapl.ObtenerPorCuentaSAP(new CuentaSAPInfo() {CuentaSAP = skayudaCuentaSAP.Clave});
                    }
                    else
                    {
                        proveedorInfoP =
                            proveedorPl.ObtenerPorCodigoSAP(new ProveedorInfo() { CodigoSAP = skAyudaProveedor.Clave });
                    }

                    if ((string)BtnAgregar.Content == Properties.Resources.OtrosCostos_MensajeCosto)
                    {
                        if (Nuevo)
                        {
                            var toneladasPermitidas = toneladasContrato + ((toneladasContrato / 100) * toleranciaContrato);
                            if (toneladasPermitidas < toneladasIngresadas)
                            {
                                SkMessageBox.Show(
                                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.OtrosCostos_MensajeToneladas,
                                    MessageBoxButton.OK, MessageImage.Warning);
                                TxtToneladas.Focus();
                            }
                            else
                            {
                                foreach (var costoInfo in listaCostos)
                                {
                                    if (costoInfo.Editable && costoInfo.ClaveContable==skAyudaCostos.Clave)
                                    {
                                        costoInfo.ImporteCosto = TxtImporte.Value.HasValue ? TxtImporte.Value.Value : 0;
                                        costoInfo.ToneladasCosto = TxtToneladas.Value.HasValue
                                                                       ? TxtToneladas.Value.Value
                                                                       : 0;
                                        if (RbCuenta.IsChecked == true)
                                        {
                                            costoInfo.CuentaSap = cuentaInfo;
                                            costoInfo.TieneCuenta = true;
                                        }
                                        else
                                        {
                                            costoInfo.Proveedor = proveedorInfoP;
                                            costoInfo.TieneCuenta = false;
                                            costoInfo.AplicaIva = chkIva.IsChecked.HasValue && chkIva.IsChecked.Value;
                                            costoInfo.AplicaRetencion = chkRetencion.IsChecked.HasValue && chkRetencion.IsChecked.Value;

                                        }
                                    }
                                }
                                GridCostos.ItemsSource = null;
                                GridCostos.ItemsSource = listaCostos;
                                skAyudaCostos.IsEnabled = true;
                                BtnAgregar.Content = Properties.Resources.OtrosCostos_BtnAgregar;
                                LimpiarCampos();
                            }
                        }
                        else
                        {
                            var toneladasPermitidas = toneladasContrato + ((toneladasContrato / 100) * toleranciaContrato);
                            if (toneladasPermitidas < toneladasIngresadas)
                            {
                                SkMessageBox.Show(
                                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.OtrosCostos_MensajeToneladas,
                                    MessageBoxButton.OK, MessageImage.Warning);
                                TxtToneladas.Focus();
                            }
                            else
                            {

                                var almacenInventarioInfo = new AlmacenInventarioInfo();
                                //Cuando el contrato ya fue guardado anteriormente se debe validar existencia
                                //Se obtiene el almacen id por medio del proveedor
                                var proveedorAlmacenPl = new ProveedorAlmacenPL();
                                var proveedorInfo = new ProveedorInfo() { ProveedorID = contratoInfo.Proveedor.ProveedorID };
                                var almacenProveedor = proveedorAlmacenPl.ObtenerPorProveedorId(proveedorInfo);

                                if (almacenProveedor == null) throw new ExcepcionDesconocida(Properties.Resources.OtrosCostos_MensajeProveedorAlmacen);
                                //Se verifica si existe producto para ese almacen
                                var almacenInventarioPl = new AlmacenInventarioPL();
                                var almacenInfo = new AlmacenInfo() { AlmacenID = almacenProveedor.AlmacenId };
                                List<AlmacenInventarioInfo> listaProductosAlmacen =
                                    almacenInventarioPl.ObtienePorAlmacenId(almacenInfo);

                                if (listaProductosAlmacen != null)
                                {
                                    foreach (
                                        var almacenInventarioInfoP in
                                            listaProductosAlmacen.Where(
                                                almacenInventarioInfoP =>
                                                    contratoInfo.Producto.ProductoId == almacenInventarioInfoP.ProductoID))
                                    {
                                        almacenInventarioInfo = almacenInventarioInfoP;
                                        break;
                                    }
                                }
                                var toneladasInventarioActual = almacenInventarioInfo.Cantidad / 1000;
                                //

                                if (toneladasInventarioActual < toneladasIngresadas)
                                {
                                    SkMessageBox.Show(
                                        Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.OtrosCostos_MensajeToneladasMayorInventario,
                                        MessageBoxButton.OK, MessageImage.Warning);
                                    TxtToneladas.Focus();
                                }
                                else
                                { 
                                    foreach (var costoInfo in listaCostos)
                                    {
                                        if (costoInfo.Editable)
                                        {
                                            costoInfo.ImporteCosto = TxtImporte.Value.HasValue ? TxtImporte.Value.Value : 0;
                                            costoInfo.ToneladasCosto = TxtToneladas.Value.HasValue
                                                                           ? TxtToneladas.Value.Value
                                                                           : 0;
                                            if (RbCuenta.IsChecked == true)
                                            {
                                                costoInfo.CuentaSap = cuentaInfo;
                                                costoInfo.TieneCuenta = true;
                                            }
                                            else
                                            {
                                                costoInfo.Proveedor = proveedorInfoP;
                                                costoInfo.TieneCuenta = false;
                                                costoInfo.AplicaIva = chkIva.IsChecked.HasValue && chkIva.IsChecked.Value;
                                                costoInfo.AplicaRetencion = chkRetencion.IsChecked.HasValue && chkRetencion.IsChecked.Value;
                                            }
                                        }
                                    }
                                    GridCostos.ItemsSource = null;
                                    GridCostos.ItemsSource = listaCostos;
                                    skAyudaCostos.IsEnabled = true;
                                    BtnAgregar.Content = Properties.Resources.OtrosCostos_BtnAgregar;
                                    LimpiarCampos();
                                }
                            }
                        }
                    }
                    else
                    {
                        bool agregado = false;
                        //Se valida si el costo ya esta agregado a la lista
                        foreach (
                            var listaCosto in
                                listaCostos.Where(
                                    listaCosto =>
                                        (listaCosto.ClaveContable == skAyudaCostos.Clave) && listaCosto.Editable))
                        {
                            agregado = true;
                        }

                        if (!agregado)
                        {
                            //Se valida que el costo sea de materia prima
                            var costoPl = new CostoPL();
                            var tipoCosto = new TipoCostoInfo()
                            {
                                TipoCostoID = (int)TipoCostoEnum.MateriaPrima,
                                Activo = EstatusEnum.Activo
                            };
                            var listaTipoCosto = new List<TipoCostoInfo> { tipoCosto };
                            var costoInfoP = new CostoInfo { ListaTipoCostos = listaTipoCosto, ClaveContable = skAyudaCostos.Clave};
                            costoInfoP = costoPl.ObtenerPorClaveContableTipoCosto(costoInfoP);

                            if (costoInfoP != null)
                            {
                                if (Nuevo)
                                {
                                    var toneladasPermitidas = toneladasContrato + ((toneladasContrato / 100) * toleranciaContrato);
                                    if (toneladasPermitidas < toneladasIngresadas)
                                    {
                                        SkMessageBox.Show(
                                            Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.OtrosCostos_MensajeToneladas,
                                            MessageBoxButton.OK, MessageImage.Warning);
                                        TxtToneladas.Focus();
                                    }
                                    else
                                    {

                                        var costoInfo = new CostoInfo
                                        {
                                            CostoID = costoInfoP.CostoID,
                                            Descripcion = costoInfoP.Descripcion,
                                            ClaveContable = costoInfoP.ClaveContable,
                                            ImporteCosto = importeIngresado,
                                            ToneladasCosto = toneladasIngresadas,
                                            Editable = true
                                        };
                                        if (RbCuenta.IsChecked == true)
                                        {
                                            costoInfo.CuentaSap = cuentaInfo;
                                            costoInfo.TieneCuenta = true;
                                        }
                                        else
                                        {
                                            costoInfo.Proveedor = proveedorInfoP;
                                            costoInfo.TieneCuenta = false;
                                            costoInfo.AplicaIva = chkIva.IsChecked.HasValue && chkIva.IsChecked.Value;
                                            costoInfo.AplicaRetencion = chkRetencion.IsChecked.HasValue && chkRetencion.IsChecked.Value;
                                        }
                                        listaCostos.Add(costoInfo);
                                        GridCostos.ItemsSource = null;
                                        GridCostos.ItemsSource = listaCostos;
                                        LimpiarCampos();
                                        skAyudaCostos.AsignarFoco();
                                    }
                                }
                                else
                                {
                                    var toneladasPermitidas = toneladasContrato + ((toneladasContrato / 100) * toleranciaContrato);
                                    if (toneladasPermitidas < toneladasIngresadas)
                                    {
                                        SkMessageBox.Show(
                                            Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.OtrosCostos_MensajeToneladas,
                                            MessageBoxButton.OK, MessageImage.Warning);
                                        TxtToneladas.Focus();
                                    }
                                    else
                                    {
                                    var almacenInventarioInfo = new AlmacenInventarioInfo();
                                    //Cuando el contrato ya fue guardado anteriormente se debe validar existencia
                                    //Se obtiene el almacen id por medio del proveedor
                                    var proveedorAlmacenPl = new ProveedorAlmacenPL();
                                    var proveedorInfo = new ProveedorInfo() { ProveedorID = contratoInfo.Proveedor.ProveedorID };
                                    var almacenProveedor = proveedorAlmacenPl.ObtenerPorProveedorId(proveedorInfo);

                                    if (almacenProveedor == null) throw new ExcepcionDesconocida(Properties.Resources.OtrosCostos_MensajeProveedorAlmacen);
                                    //Se verifica si existe producto para ese almacen
                                    var almacenInventarioPl = new AlmacenInventarioPL();
                                    var almacenInfo = new AlmacenInfo() { AlmacenID = almacenProveedor.AlmacenId };
                                    List<AlmacenInventarioInfo> listaProductosAlmacen =
                                        almacenInventarioPl.ObtienePorAlmacenId(almacenInfo);

                                    if (listaProductosAlmacen != null)
                                    {
                                        foreach (
                                            var almacenInventarioInfoP in
                                                listaProductosAlmacen.Where(
                                                    almacenInventarioInfoP =>
                                                        contratoInfo.Producto.ProductoId == almacenInventarioInfoP.ProductoID))
                                        {
                                            almacenInventarioInfo = almacenInventarioInfoP;
                                            break;
                                        }
                                    }
                                    var toneladasInventarioActual = almacenInventarioInfo.Cantidad / 1000;

                                    if (toneladasInventarioActual < toneladasIngresadas)
                                    {
                                        SkMessageBox.Show(
                                            Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.OtrosCostos_MensajeToneladasMayorInventario,
                                            MessageBoxButton.OK, MessageImage.Warning);
                                        TxtToneladas.Focus();
                                    }
                                    else
                                    {
                                        var costoInfo = new CostoInfo
                                        {
                                            CostoID = costoInfoP.CostoID,
                                            Descripcion = costoInfoP.Descripcion,
                                            ClaveContable = costoInfoP.ClaveContable,
                                            ImporteCosto = importeIngresado,
                                            ToneladasCosto = toneladasIngresadas,
                                            Editable = true
                                        };
                                        if (RbCuenta.IsChecked == true)
                                        {
                                            costoInfo.CuentaSap = cuentaInfo;
                                            costoInfo.TieneCuenta = true;
                                        }
                                        else
                                        {
                                            costoInfo.Proveedor = proveedorInfoP;
                                            costoInfo.TieneCuenta = false;
                                            costoInfo.AplicaIva = chkIva.IsChecked.HasValue && chkIva.IsChecked.Value;
                                            costoInfo.AplicaRetencion = chkRetencion.IsChecked.HasValue && chkRetencion.IsChecked.Value;
                                        }
                                        listaCostos.Add(costoInfo);
                                        GridCostos.ItemsSource = null;
                                        GridCostos.ItemsSource = listaCostos;
                                        LimpiarCampos();
                                        skAyudaCostos.AsignarFoco();
                                    }
                                  }
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.OtrosCostos_CostoValido,
                                    MessageBoxButton.OK, MessageImage.Warning);
                                skAyudaCostos.LimpiarCampos();
                                skAyudaCostos.AsignarFoco();
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.OtrosCostos_MensajeCostoAgregado,
                                MessageBoxButton.OK, MessageImage.Warning);
                        }
                    }
                    //Termina validacion
                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje) ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Se define lo que se mostrara en el grid y que controles estaran deshabilitados
        /// </summary>
        private void DefinirPantalla()
        {
            if (listaCostos.Count > 0)
            {
                GridCostos.ItemsSource = null;
                GridCostos.ItemsSource = listaCostos;
            }

            if (Nuevo) return;
            if (contratoInfo.Activo != EstatusEnum.Inactivo) return;
            skAyudaCostos.IsEnabled = false;
            TxtImporte.IsEnabled = false;
            TxtToneladas.IsEnabled = false;
            BtnAgregar.IsEnabled = false;
        }

        /// <summary>
        /// Variable ayuda
        /// </summary>
        private SKAyuda<CostoInfo> skAyudaCostos;
        private SKAyuda<ProveedorInfo> skAyudaProveedor;
        private SKAyuda<CuentaSAPInfo> skayudaCuentaSAP; 

        /// <summary>
        /// Carga las ayudas de la pantalla
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaCostos();
            AgregarAyudaProveedor();
        }

        /// <summary>
        /// Agrega la ayuda para obtener el costo
        /// </summary>
        private void AgregarAyudaCostos()
        {
            var tipoCosto = new TipoCostoInfo()
            {
                TipoCostoID = (int)TipoCostoEnum.MateriaPrima,
                Activo = EstatusEnum.Activo
            };
            var listaTipoCosto = new List<TipoCostoInfo> {tipoCosto};
            skAyudaCostos = new SKAyuda<CostoInfo>(200, false, new CostoInfo { ListaTipoCostos = listaTipoCosto}
                                                   , "PropiedadClaveOtrosCostos"
                                                   , "PropiedadDescripcionOtrosCostos", true)
            {
                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.OtrosCostos_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.OtrosCostos_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaCostos_Busqueda,
                MensajeAgregar = Properties.Resources.OtrosCostos_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaCostos_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaCostos_BusquedaTitulo,
            };
            skAyudaCostos.ObtenerDatos += ObtenerDatosCostos;
            skAyudaCostos.AsignaTabIndex(0);
            SplAyudaCostos.Children.Clear();
            SplAyudaCostos.Children.Add(skAyudaCostos);
        }

        /// <summary>
        /// Verifica que el costo sea de materia prima
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCostos(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaCostos.Info == null)
                {
                    return;
                }
                if (skAyudaCostos.Info.TipoCosto.TipoCostoID !=
                    TipoCostoEnum.MateriaPrima.GetHashCode())
                {
                    skAyudaCostos.Info = new CostoInfo
                    {
                        ListaTipoCostos = new List<TipoCostoInfo>
                        {
                            new TipoCostoInfo
                            {
                                TipoCostoID = TipoCostoEnum.MateriaPrima.GetHashCode()
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };

                    skAyudaCostos.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.OtrosCostos_MensajeCostoInvalido,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                else
                {
                    skAyudaCostos.Info = new CostoInfo
                    {
                        ListaTipoCostos = new List<TipoCostoInfo>
                        {
                            new TipoCostoInfo
                            {
                                TipoCostoID = TipoCostoEnum.MateriaPrima.GetHashCode()
                            }
                        },
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
        /// Agrega la ayuda cuentas
        /// </summary>
        private void AgregarAyudaCuentas()
        {
            var cuentaSapInfo = new CuentaSAPInfo
            {
                ListaTiposCuenta = new List<TipoCuentaInfo>() { new TipoCuentaInfo() { TipoCuentaID = TipoCuenta.Provision.GetHashCode() } },
                Activo = EstatusEnum.Activo
            };
            skayudaCuentaSAP = new SKAyuda<CuentaSAPInfo>(200, false, cuentaSapInfo
                                                   , "PropiedadClaveCrearContrato"
                                                   , "PropiedadDescripcionCrearContrato",
                                                   "", false, 80, 10, true)
            {
                AyudaPL = new CuentaSAPPL(),
                MensajeClaveInexistente = Properties.Resources.CrearContrato_AyudaCuentaInvalido,
                MensajeBusquedaCerrar = Properties.Resources.CrearContrato_AyudaCuentaSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.CrearContrato_AyudaCuentaSeleccionar,
                TituloEtiqueta = Properties.Resources.CrearContrato_AyudaCuentaEtiquetaCuenta,
                TituloPantalla = Properties.Resources.CrearContrato_AyudaCuentaTitulo,
            };
            skayudaCuentaSAP.ObtenerDatos += ObtenerDatosCuenta;
            skayudaCuentaSAP.AsignaTabIndex(3);
            SplAyudaCuentaProveedor.Children.Clear();
            SplAyudaCuentaProveedor.Children.Add(skayudaCuentaSAP);
        }

        /// <summary>
        /// Se obtienen los datos de la cuenta
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCuenta(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skayudaCuentaSAP.Info == null)
                {
                    return;
                }
                //Obtener indicadores
                skayudaCuentaSAP.Info = new CuentaSAPInfo()
                {
                    ListaTiposCuenta = new List<TipoCuentaInfo>() { new TipoCuentaInfo() { TipoCuentaID = TipoCuenta.Provision.GetHashCode() } },
                    Activo = EstatusEnum.Activo
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosOrganizacion, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo que carga los datos de la ayuda
        /// </summary>
        private void AgregarAyudaProveedor()
        {
            var proveedorInfo = new ProveedorInfo
            {
                ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode() },
                      },
                Activo = EstatusEnum.Activo
            };
            skAyudaProveedor = new SKAyuda<ProveedorInfo>(200, false, proveedorInfo
                                                   , "PropiedadClaveCrearContrato"
                                                   , "PropiedadDescripcionCrearContrato",
                                                   "", false, 80, 10, true)
            {
                AyudaPL = new ProveedorPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaProveedorAdministrarContrato_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaProveedorAdministrarContrato_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaProveedorAdministrarContrato_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo,
            };
            skAyudaProveedor.ObtenerDatos += ObtenerDatosProveedor;
            SplAyudaCuentaProveedor.Children.Clear();
            SplAyudaCuentaProveedor.Children.Add(skAyudaProveedor);
        }

        /// <summary>
        /// Se obtienen los datos de la cuenta
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProveedor(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaProveedor.Info == null)
                {
                    return;
                }

                if (skAyudaProveedor.Info.TipoProveedor.TipoProveedorID != TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() && skAyudaProveedor.Info.TipoProveedor.TipoProveedorID != TipoProveedorEnum.ProveedoresFletes.GetHashCode())
                {
                      SkMessageBox.Show(
                                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CrearContrato_MensajeProveedorInvalido,
                                    MessageBoxButton.OK, MessageImage.Warning);
                      skAyudaProveedor.LimpiarCampos();
                    skAyudaProveedor.Focus();

               
                    skAyudaProveedor.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosOrganizacion, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Limpia ayuda, txtImporte y txtToneladas
        /// </summary>
        private void LimpiarCampos()
        {
            skAyudaCostos.LimpiarCampos();
            TxtImporte.ClearValue(DecimalUpDown.ValueProperty);
            TxtToneladas.ClearValue(DecimalUpDown.ValueProperty);
            RbCuenta.IsChecked = false;
            RbProveedor.IsChecked = false;
            chkIva.IsChecked = false;
            chkRetencion.IsChecked = false;
            if (skayudaCuentaSAP != null)
            {
                skayudaCuentaSAP.LimpiarCampos();
                skayudaCuentaSAP.IsEnabled = false;
            }
            if (skAyudaProveedor != null)
            {
                skAyudaProveedor.LimpiarCampos();
                skAyudaProveedor.IsEnabled = false;
            }
        }

        /// <summary>
        /// Validar importe y toneladas
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCampos()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(skAyudaCostos.Clave.Trim()) && String.IsNullOrEmpty(skAyudaCostos.Descripcion.Trim()))
            {
                skAyudaCostos.AsignarFoco();
                resultado.Mensaje = Properties.Resources.OtrosCostos_MensajeValidacionCosto;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtImporte.Text) || !TxtImporte.Value.HasValue)
            {
                TxtImporte.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OtrosCostos_MensajeValidacionImporte;
                return resultado;
            }

            if ((String.IsNullOrEmpty(TxtToneladas.Text) || !TxtToneladas.Value.HasValue))
            {
                TxtToneladas.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OtrosCostos_MensajeValidacionToneladas;
                return resultado;
            }

            if (RbCuenta.IsChecked == false && RbProveedor.IsChecked == false)
            {
                BtnAgregar.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OtrosCostos_ValidacionAgregarCuentaProveedor;
                return resultado;
            }

            if (RbCuenta.IsChecked == true && String.IsNullOrEmpty(skayudaCuentaSAP.Clave.Trim()) &&
                String.IsNullOrEmpty(skayudaCuentaSAP.Descripcion.Trim()))
            {
                skayudaCuentaSAP.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OtrosCostos_ValidacionAgregarRbCuenta;
                return resultado;
            }

            if (RbProveedor.IsChecked == true && String.IsNullOrEmpty(skAyudaProveedor.Clave.Trim()) &&
                String.IsNullOrEmpty(skAyudaProveedor.Descripcion.Trim()))
            {
                skAyudaProveedor.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OtrosCostos_ValidacionAgregarRbProveedor;
                return resultado;
            }

            resultado.Resultado = true;
            return resultado;
        }
        #endregion

        /// <summary>
        /// Evento KeyDown para la forma en general
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtrosCostos_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Avanza al siguiente control en la pantalla
        /// </summary>
        /// <param name="e"></param>
        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }

        /// <summary>
        /// Se activa al checkear el rb cuenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbCuenta_OnChecked(object sender, RoutedEventArgs e)
        {
            RbProveedor.IsChecked = false;
            chkIva.IsChecked = false;
            chkIva.IsEnabled = false;
            chkRetencion.IsChecked = false;
            chkRetencion.IsEnabled = false;
            AgregarAyudaCuentas();
        }

        /// <summary>
        /// Se activa al checkear el rb proveedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbProveedor_OnChecked(object sender, RoutedEventArgs e)
        {
            RbCuenta.IsChecked = false;
            chkIva.IsChecked = false;
            chkIva.IsEnabled = true;
            chkRetencion.IsChecked = false;
            chkRetencion.IsEnabled = true;
            AgregarAyudaProveedor();
        }
    }
}
