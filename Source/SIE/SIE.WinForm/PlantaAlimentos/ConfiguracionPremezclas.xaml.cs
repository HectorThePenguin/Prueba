using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;
using TipoOrganizacion = SIE.Services.Info.Enums.TipoOrganizacion;

namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionPremezclas.xaml
    /// </summary>
    public partial class ConfiguracionPremezclas
    {
        #region Propiedades
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        private SKAyuda<ProductoInfo> skAyudaPremezcla;
        //private PremezclaInfo premezclaInfo;
        private SKAyuda<ProductoInfo> skAyudaProducto;
        private List<PremezclaDetalleInfo> listaPremezclaDetalle = new List<PremezclaDetalleInfo>();
        private List<PremezclaDetalleInfo> listaPremezclasEliminadas = new List<PremezclaDetalleInfo>();
        private int usuario;
        private bool nuevaPremezcla;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public ConfiguracionPremezclas()
        {
            InitializeComponent();
            CargarAyudaOrganizacion();
            CargarAyudaPremezclas();
            CargarAyudaProductos();
            usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Agrega una premezcla al grid principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bool actualizar = (string)BtnAgregar.Content == Properties.Resources.DiferenciasDeInventario_LblActualizar;
                //Validar pesos antes de guardar
                var resultadoValidacion = ValidarAgregar(actualizar);
                if (resultadoValidacion.Resultado)
                {
                    if ((string)BtnAgregar.Content == Properties.Resources.DiferenciasDeInventario_LblActualizar)
                    {
                        foreach (var premezclaDetalleInfoP in listaPremezclaDetalle.Where(premezclaDetalleInfoP => premezclaDetalleInfoP.Producto.ProductoId ==
                                                                                                                                             Convert.ToInt32(skAyudaProducto.Clave)))
                        {
                            premezclaDetalleInfoP.Porcentaje = Convert.ToDecimal(TxtPorcentajeAjuste.Value.ToString());
                        }
                        GridPremezclaDetalle.ItemsSource = null;
                        GridPremezclaDetalle.ItemsSource = listaPremezclaDetalle;
                        BtnAgregar.Content = Properties.Resources.DiferenciasDeInventario_BtnAgregar;
                        LimpiaProductos();
                        skAyudaProducto.IsEnabled = true;

                        //Agregar bandera
                        if (nuevaPremezcla)
                        {
                            skAyudaOrganizacion.IsEnabled = true;
                            skAyudaPremezcla.IsEnabled = true;
                        }

                        skAyudaProducto.AsignarFoco();
                    }
                    else
                    {
                        AgregarProducto();
                    }
                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                        ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco
                        : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionPremezcla_MensajeErrorAgregarProducto, MessageBoxButton.OK,
                    MessageImage.Error);
                BtnGuardar.Focus();
            }
        }

        /// <summary>
        /// Elimina un elemento del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEliminar = (Button)e.Source;
            try
            {
                var listaPremezclas = new List<PremezclaDetalleInfo>();
                var premezclaDetalleInfo = (PremezclaDetalleInfo)Extensor.ClonarInfo(botonEliminar.CommandParameter);
                if (premezclaDetalleInfo != null)
                {
                    if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ConfiguracionPremezclaAgregarProducto_MensajeEliminarProducto,
                       MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                    {
                        //Eliminar ajuste
                        listaPremezclas.AddRange(listaPremezclaDetalle.Where(detalleInfo => detalleInfo.PremezclaDetalleID != premezclaDetalleInfo.PremezclaDetalleID));

                        //Se agrega la premezcla a la lista de eliminadas
                        if(premezclaDetalleInfo.Guardado)
                        {
                            premezclaDetalleInfo.UsuarioModificacionId = usuario;
                            listaPremezclasEliminadas.Add(premezclaDetalleInfo);
                        }
                        //Se agregan las premezclas resultantes a la lista principal
                        listaPremezclaDetalle.Clear();
                        listaPremezclaDetalle.AddRange(listaPremezclas);
                        GridPremezclaDetalle.ItemsSource = null;
                        GridPremezclaDetalle.ItemsSource = listaPremezclaDetalle;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionPremezcla_MensajeErrorEliminarProducto, MessageBoxButton.OK,
                    MessageImage.Error);
                //Excepcion
            }
        }

        /// <summary>
        /// Editar premezcla detalle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var premezclaDetalleInfo = (PremezclaDetalleInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                BtnAgregar.Content = Properties.Resources.OtrosCostos_MensajeCosto;
                if (premezclaDetalleInfo == null) return;
                skAyudaProducto.Clave = premezclaDetalleInfo.Producto.ProductoId.ToString(CultureInfo.InvariantCulture);
                skAyudaProducto.Descripcion = premezclaDetalleInfo.Producto.Descripcion;
                skAyudaProducto.IsEnabled = false;
                skAyudaOrganizacion.IsEnabled = false;
                skAyudaPremezcla.IsEnabled = false;
                TxtPorcentajeAjuste.Value = premezclaDetalleInfo.Porcentaje;
                BtnAgregar.Content = Properties.Resources.DiferenciasDeInventario_LblActualizar;
                TxtPorcentajeAjuste.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeErrorObtenerDatosEditar, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Limpia la ayuda de producto y el txtporcentaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            LimpiaProductos();
            skAyudaProducto.AsignarFoco();
        }

        /// <summary>
        /// Limpiar toda la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezclaAgregarProducto_MensajeCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                LimpiaProductos();
                LimpiarControles();
                skAyudaOrganizacion.AsignarFoco();
            }
        }

        /// <summary>
        /// Asignar foco a ayuda organizacion al cargar formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfiguracionPremezclas_OnLoaded(object sender, RoutedEventArgs e)
        {
            skAyudaOrganizacion.AsignarFoco();
        }

        /// <summary>
        /// Guarda los productos seleccionados o modificados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultadoValidacion = ValidarGuardar();
                if (resultadoValidacion.Resultado)
                {
                    var configuracionPremezclasPl = new ConfiguracionPremezclasPL();
                    var premezclaPl = new PremezclaPL();
                    var premezclaInfo = new PremezclaInfo()
                    {
                        Organizacion =
                            new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave) },
                        Producto = new ProductoInfo() { ProductoId = Convert.ToInt32(skAyudaPremezcla.Clave) },
                        Activo = EstatusEnum.Activo
                    };
                    premezclaInfo = premezclaPl.ObtenerPorProductoIdOrganizacionId(premezclaInfo);
                    if (premezclaInfo == null)
                    {
                        premezclaInfo = new PremezclaInfo()
                        {
                            Organizacion =
                                new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave) },
                                Descripcion = skAyudaPremezcla.Descripcion,
                            Producto =
                                new ProductoInfo()
                                {
                                    ProductoId = Convert.ToInt32(skAyudaPremezcla.Clave),
                                    Descripcion = skAyudaProducto.Info.Descripcion
                                },
                            Activo = EstatusEnum.Activo,
                            UsuarioCreacion = new UsuarioInfo() { UsuarioCreacionID = usuario }
                        };
                    }
                    else
                    {
                        premezclaInfo.Guardado = true;
                        premezclaInfo.UsuarioCreacion = new UsuarioInfo() { UsuarioCreacionID = usuario };
                        premezclaInfo.UsuarioModificacion = new UsuarioInfo() { UsuarioModificacionID = usuario };
                    }
                    configuracionPremezclasPl.Guardar(premezclaInfo, listaPremezclaDetalle, listaPremezclasEliminadas, usuario);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_DatosGuardadosExito,
                    MessageBoxButton.OK, MessageImage.Correct);
                    LimpiaProductos();
                    LimpiarControles();
                    skAyudaOrganizacion.AsignarFoco();
                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                        ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco
                        : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorGuardar, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Carga ayuda organizacion
        /// </summary>
        private void CargarAyudaOrganizacion()
        {
            var organizacionInfo = new OrganizacionInfo
            {
                TipoOrganizacion = new TipoOrganizacionInfo() { TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode() },
                Activo = EstatusEnum.Activo
            };
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, organizacionInfo
                                                   , "PropiedadClaveConfiguracionPremezclas"
                                                   , "PropiedadDescripcionConfiguracionPremezclas",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaOrganizacionInvalida,
                MensajeBusquedaCerrar = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaOrganizacionSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaOrganizacionSeleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrganizacion,
                TituloPantalla = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaOrganizacionTitulo,
            };
            skAyudaOrganizacion.LlamadaMetodosNoExistenDatos += LimpiarTodo;
            skAyudaOrganizacion.ObtenerDatos += ObtenerDatosOrganizacion;
            skAyudaOrganizacion.AsignaTabIndex(0);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }

        //Limpiar toda la pantalla
        private void LimpiarTodo()
        {
            LimpiaProductos();
            LimpiarControles();
        }

        /// <summary>
        /// Obtiene la organizacion seleccionada en el filtro
        /// </summary>
        private void ObtenerDatosOrganizacion(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaOrganizacion.Info == null)
                {
                    return;
                }
                if (skAyudaOrganizacion.Info.TipoOrganizacion.TipoOrganizacionID !=
                    TipoOrganizacion.Ganadera.GetHashCode())
                {
                    skAyudaOrganizacion.Info = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo() { TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode() },
                        Activo = EstatusEnum.Activo
                    };
                    skAyudaOrganizacion.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CrearContrato_MensajeProveedorInvalido,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                else
                {
                    skAyudaOrganizacion.Info = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo() { TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode() },
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
        /// Cargar ayuda premezclas
        /// </summary>
        private void CargarAyudaPremezclas()
        {
            var productoInfo = new ProductoInfo
            {
                FamiliaId = FamiliasEnum.Premezclas.GetHashCode(),
                SubfamiliaId = SubFamiliasEnum.MicroIngredientes.GetHashCode(),
                SubFamilia = new SubFamiliaInfo() { SubFamiliaID = SubFamiliasEnum.MicroIngredientes.GetHashCode() },
                Activo = EstatusEnum.Activo
            };
            skAyudaPremezcla = new SKAyuda<ProductoInfo>(200, false, productoInfo
                                                   , "PropiedadClaveConfiguracionPremezclas"
                                                   , "PropiedadDescripcionConfiguracionPremezclas",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new ProductoPL(),
                MensajeClaveInexistente = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaPremezclaInvalida,
                MensajeBusquedaCerrar = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaPremezclaSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaPremezclaSeleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaPremezcla,
                TituloPantalla = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaPremezclaTitulo,
            };

            skAyudaPremezcla.LlamadaMetodosNoExistenDatos += LimpiaParcial;
            skAyudaPremezcla.ObtenerDatos += ObtenerDatosPremezcla;
            skAyudaPremezcla.AsignaTabIndex(0);
            SplAyudaPremezcla.Children.Clear();
            SplAyudaPremezcla.Children.Add(skAyudaPremezcla);
        }

        /// <summary>
        /// Limpiar controles excepto organizacion
        /// </summary>
        private void LimpiaParcial()
        {
            LimpiaProductos();
            skAyudaPremezcla.LimpiarCampos();
            listaPremezclasEliminadas.Clear();
            listaPremezclaDetalle.Clear();
            GridPremezclaDetalle.ItemsSource = null;
            GridPremezclaDetalle.ItemsSource = listaPremezclaDetalle;
        }

        /// <summary>
        /// Obtiene los datos de la premezcla y determina si esta configurada o no
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosPremezcla(string clave)
        {
            try
            {
                if (skAyudaOrganizacion.Clave != string.Empty && skAyudaOrganizacion.Descripcion != string.Empty)
                {
                    var premezclaPl = new PremezclaPL();
                    var premezclaInfo = new PremezclaInfo()
                    {
                        Organizacion =
                            new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave) },
                        Producto = new ProductoInfo() { ProductoId = Convert.ToInt32(skAyudaPremezcla.Clave) },
                        Activo = EstatusEnum.Activo
                    };
                    premezclaInfo = premezclaPl.ObtenerPorProductoIdOrganizacionId(premezclaInfo);
                    if (premezclaInfo != null)
                    {
                        //Llenar grid premezcla
                        listaPremezclaDetalle.Clear();
                        listaPremezclasEliminadas.Clear();
                        listaPremezclaDetalle.AddRange(premezclaInfo.ListaPremezclaDetalleInfos);
                        GridPremezclaDetalle.ItemsSource = null;
                        GridPremezclaDetalle.ItemsSource = premezclaInfo.ListaPremezclaDetalleInfos;
                        skAyudaOrganizacion.IsEnabled = false;
                        skAyudaPremezcla.IsEnabled = false;
                        skAyudaProducto.AsignarFoco();
                    }
                    else
                    {
                        //Enviar mensaje
                        if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ConfiguracionDePremezclas_MensajePremezclaSinConfiguracion,
                            MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                        {
                            listaPremezclaDetalle.Clear();
                            listaPremezclasEliminadas.Clear();
                            GridPremezclaDetalle.ItemsSource = null;
                            GridPremezclaDetalle.ItemsSource = listaPremezclaDetalle;
                            skAyudaProducto.LimpiarCampos();
                            skAyudaProducto.AsignarFoco();
                            nuevaPremezcla = true;
                        }
                        else
                        {
                            listaPremezclaDetalle.Clear();
                            listaPremezclasEliminadas.Clear();
                            GridPremezclaDetalle.ItemsSource = null;
                            GridPremezclaDetalle.ItemsSource = listaPremezclaDetalle;
                            skAyudaPremezcla.LimpiarCampos();
                            skAyudaPremezcla.AsignarFoco();
                        }
                    }
                }
                else
                {
                    //Enviar mensaje
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionPremezclaAgregarProducto_MensajeOrganizacion, MessageBoxButton.OK,
                    MessageImage.Warning);
                    skAyudaPremezcla.LimpiarCampos();
                    skAyudaOrganizacion.AsignarFoco();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosPremezcla, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga ayudas de productos
        /// </summary>
        private void CargarAyudaProductos()
        {
            var productoInfo = new ProductoInfo
            {
                FamiliaId = FamiliasEnum.Premezclas.GetHashCode(),
                SubfamiliaId = SubFamiliasEnum.SubProductos.GetHashCode(),
                SubFamilia = new SubFamiliaInfo() { SubFamiliaID = SubFamiliasEnum.SubProductos.GetHashCode() },
                Activo = EstatusEnum.Activo
            };
            skAyudaProducto = new SKAyuda<ProductoInfo>(200, false, productoInfo
                                                   , "PropiedadClaveConfiguracionPremezclas"
                                                   , "PropiedadDescripcionConfiguracionPremezclas",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new ProductoPL(),
                MensajeClaveInexistente = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaProductoInvalida,
                MensajeBusquedaCerrar = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaProductoSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaProductoSeleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProducto,
                TituloPantalla = Properties.Resources.ConfiguracionPremezclaAgregarProducto_AyudaProductoTitulo,
            };
            skAyudaProducto.ObtenerDatos += ObtenerDatosProducto;
            SplAyudaProducto.Children.Clear();
            SplAyudaProducto.Children.Add(skAyudaProducto);
        }

        /// <summary>
        /// Obtiene los datos del proveedor con la clave obtenida en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProducto(string clave)
        {
            try
            {
                if (skAyudaPremezcla.Clave != string.Empty && skAyudaPremezcla.Descripcion != string.Empty)
                {
                    if (skAyudaProducto.Info.SubfamiliaId !=
                        SubFamiliasEnum.SubProductos.GetHashCode())
                    {
                        skAyudaProducto.Info = new ProductoInfo
                        {
                            FamiliaId = FamiliasEnum.Premezclas.GetHashCode(),
                            SubfamiliaId = SubFamiliasEnum.SubProductos.GetHashCode(),
                            SubFamilia =
                                new SubFamiliaInfo() {SubFamiliaID = SubFamiliasEnum.SubProductos.GetHashCode()},
                            Activo = EstatusEnum.Activo
                        };
                        skAyudaProducto.LimpiarCampos();
                    }
                    else
                    {
                        skAyudaProducto.Info = new ProductoInfo
                        {
                            FamiliaId = FamiliasEnum.Premezclas.GetHashCode(),
                            SubfamiliaId = SubFamiliasEnum.SubProductos.GetHashCode(),
                            SubFamilia =
                                new SubFamiliaInfo() {SubFamiliaID = SubFamiliasEnum.SubProductos.GetHashCode()},
                            Activo = EstatusEnum.Activo
                        };
                    }
                }
                else
                {
                    //Enviar mensaje
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionPremezclaAgregarProducto_MensajePremezcla, MessageBoxButton.OK,
                    MessageImage.Warning);
                    skAyudaProducto.LimpiarCampos();
                    skAyudaPremezcla.AsignarFoco();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosProducto, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        //Limpia los controles de producto
        private void LimpiaProductos()
        {
            skAyudaProducto.LimpiarCampos();
            skAyudaProducto.IsEnabled = true;
            TxtPorcentajeAjuste.Value = 0;
            BtnAgregar.Content = Properties.Resources.ConfiguracionPremezclaAgregarProducto_BtnAgregar;
        }

        /// <summary>
        /// Validacion antes de agregar el ajuste al grid
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarAgregar(bool actualizar)
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(skAyudaOrganizacion.Clave.Trim()) && String.IsNullOrEmpty(skAyudaOrganizacion.Descripcion.Trim()))
            {
                skAyudaOrganizacion.AsignarFoco();
                resultado.Mensaje = Properties.Resources.ConfiguracionDePremezclas_MensajeValidacionAgregarOrganizacion;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaPremezcla.Clave.Trim()) && String.IsNullOrEmpty(skAyudaPremezcla.Descripcion.Trim()))
            {
                skAyudaPremezcla.AsignarFoco();
                resultado.Mensaje = Properties.Resources.ConfiguracionDePremezclas_MensajeValidacionAgregarPremezcla;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaProducto.Clave.Trim()) && String.IsNullOrEmpty(skAyudaProducto.Descripcion.Trim()))
            {
                skAyudaProducto.AsignarFoco();
                resultado.Mensaje = Properties.Resources.ConfiguracionDePremezclas_MensajeValidacionAgregarProducto;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtPorcentajeAjuste.Text) || TxtPorcentajeAjuste.Value == 0)
            {
                TxtPorcentajeAjuste.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.ConfiguracionDePremezclas_MensajeValidacionAgregarPorcentaje;
                return resultado;
            }

            //Verificar si el producto ya esta agregado
            if (!actualizar)
            {
                if (listaPremezclaDetalle != null)
                {
                    if ((from premezclaDetalle in listaPremezclaDetalle
                         where premezclaDetalle.Producto.ProductoId == Convert.ToInt32(skAyudaProducto.Clave)
                         select premezclaDetalle).Any())
                    {
                        skAyudaProducto.AsignarFoco();
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.Resources.ConfiguracionDePremezclas_MensajeValidacionProductoAgregado;
                        return resultado;
                    }
                }
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Agrega un producto al grid principal
        /// </summary>
        private void AgregarProducto()
        {
            //Falta try catch
            try
            {
                var random = new Random();
                var productoPl = new ProductoPL();
                var productoInfo = new ProductoInfo() { ProductoId = Convert.ToInt32(skAyudaProducto.Clave) };
                productoInfo = productoPl.ObtenerPorID(productoInfo);
                var premezclaDetalleInfo = new PremezclaDetalleInfo()
                {
                    PremezclaDetalleID = random.Next(999999999),
                    Producto = productoInfo,
                    Porcentaje = Convert.ToDecimal(TxtPorcentajeAjuste.Value.ToString()),
                    UsuarioCreacionId = usuario
                };
                listaPremezclaDetalle.Add(premezclaDetalleInfo);
                GridPremezclaDetalle.ItemsSource = null;
                GridPremezclaDetalle.ItemsSource = listaPremezclaDetalle;
                skAyudaProducto.LimpiarCampos();
                TxtPorcentajeAjuste.Value = 0;
                skAyudaProducto.AsignarFoco();
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
            }
        }

        /// <summary>
        /// Validacion antes de agregar el ajuste al grid
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarGuardar()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(skAyudaOrganizacion.Clave.Trim()) && String.IsNullOrEmpty(skAyudaOrganizacion.Descripcion.Trim()))
            {
                skAyudaOrganizacion.AsignarFoco();
                resultado.Mensaje = Properties.Resources.ConfiguracionDePremezclas_MensajeValidacionAgregarOrganizacion;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaPremezcla.Clave.Trim()) && String.IsNullOrEmpty(skAyudaPremezcla.Descripcion.Trim()))
            {
                skAyudaPremezcla.AsignarFoco();
                resultado.Mensaje = Properties.Resources.ConfiguracionDePremezclas_MensajeValidacionAgregarPremezcla;
                resultado.Resultado = false;
                return resultado;
            }

            //Verificar si el producto ya esta agregado
            if (GridPremezclaDetalle.Items.Count == 0)
            {
                BtnGuardar.Focus();
                resultado.Mensaje = Properties.Resources.ConfiguracionPremezclaAgregarProducto_MensajeGuardarPremezcla;
                resultado.Resultado = false;
                return resultado;
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Limpiar controles de la pantalla
        /// </summary>
        private void LimpiarControles()
        {
            skAyudaOrganizacion.LimpiarCampos();
            skAyudaPremezcla.LimpiarCampos();
            listaPremezclasEliminadas.Clear();
            listaPremezclaDetalle.Clear();
            GridPremezclaDetalle.ItemsSource = null;
            GridPremezclaDetalle.ItemsSource = listaPremezclaDetalle;
            nuevaPremezcla = false;
            skAyudaOrganizacion.IsEnabled = true;
            skAyudaPremezcla.IsEnabled = true;
        }
        #endregion
    }
}
