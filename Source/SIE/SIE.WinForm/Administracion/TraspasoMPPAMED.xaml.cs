using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para TraspasoMPPAMED.xaml
    /// </summary>
    public partial class TraspasoMPPAMED
    {

        #region PROPIEDADES

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TraspasoMpPaMedInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TraspasoMpPaMedInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion PROPIEDADES

        #region VARIABLES
        /// <summary>
        /// Obtene la configuracion de los tipos de almacen
        /// </summary>
        private IList<ConfiguracionTraspasoAlmacenInfo> configuracionTraspasoAlmacen;

        /// <summary>
        /// identifica los tipos de almacen que aplican para medicamento
        /// </summary>
        private readonly IList<int> tiposAlmacenMedicamentos = new List<int> { 1, 2, 3, 4 };

        #endregion VARIABLES

        #region CONSTRUCTORES

        public TraspasoMPPAMED()
        {
            InitializeComponent();
        }
        #endregion CONSTRUCTORES

        #region EVENTOS

        /// <summary>
        /// Evento que se ejecuta al cargar la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ObtenerConfiguracionTraspasoAlmacen();
                InicializaContexto();
                BloqueaDesbloqueaControlesDestino(false);
                skAyudaTipoAlmacenOrigen.ObjetoNegocio = new TipoAlmacenPL();
                skAyudaTipoAlmacenDestino.ObjetoNegocio = new TipoAlmacenPL();
                skAyudaLoteMPOrigen.ObjetoNegocio = new AlmacenInventarioLotePL();
                skAyudaLoteMPDestino.ObjetoNegocio = new AlmacenInventarioLotePL();
                skAyudaAlmacenOrigen.ObjetoNegocio = new AlmacenPL();
                skAyudaAlmacenDestino.ObjetoNegocio = new AlmacenPL();
                skAyudaCuentaContableContratoDestino.ObjetoNegocio = new CuentaSAPPL();
                skAyudaOrganizacionOrigen.ObjetoNegocio = new OrganizacionPL();
                skAyudaOrganizacionDestino.ObjetoNegocio = new OrganizacionPL();
                skAyudaProductoOrigen.ObjetoNegocio = new ProductoPL();
                skAyudaProductoDestino.ObjetoNegocio = new ProductoPL();
                AsignarEventosAyudas();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_ErrorCargarConfiguracionTraspaso, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al dar clic al boton Guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento que se ejecuta al dar clic en el boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TraspasoMPPAMED_Cancelar, MessageBoxButton.YesNo,
                                                           MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Limpiar();
            }

        }

        /// <summary>
        /// Evento que se ejecuta al dar clic al boton Nuevo Lote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonNuevoLote_OnClick(object sender, RoutedEventArgs e)
        {
            CrearNuevoLote();
        }

        /// <summary>
        /// Evento que se ejecuta al dar clic en el boton de Busqueda de Folio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscarFolioEmbarque_OnClick(object sender, RoutedEventArgs e)
        {
            AyudaFolioTraspaso();
        }

        /// <summary>
        /// Evento que se ejecuta al dar clic en el boton Cancelar Folio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelarFolio_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TraspasoMPPAMED_CancelarFolio, MessageBoxButton.YesNo,
                                                          MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                CancelarTraspaso();
            }
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Carga los tipos de cuenta de la cuenta de destino
        /// </summary>
        private void CargarCuentasContables()
        {
            try
            {
                var tiposCuenta = new List<TipoCuentaInfo>
                {
                    new TipoCuentaInfo
                        {
                            TipoCuentaID = TipoCuenta.Provision.GetHashCode()
                        },
                    new TipoCuentaInfo
                        {
                            TipoCuentaID = TipoCuenta.Inventario.GetHashCode()
                        },
                    new TipoCuentaInfo
                        {
                            TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode()
                        },
                    new TipoCuentaInfo
                        {
                            TipoCuentaID = TipoCuenta.Producto.GetHashCode()
                        },
                    new TipoCuentaInfo
                        {
                            TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode()
                        },
                };
                Contexto.CuentaContable.ListaTiposCuenta = tiposCuenta;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_ErrorCargarCuentasContables, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Llama ventana para ayuda de folio de traspaso
        /// </summary>
        private void AyudaFolioTraspaso()
        {
            try
            {
                var traspasoMPPAMEDBusquedaFolio = new TraspasoMPPAMEDBusquedaFolio();
                traspasoMPPAMEDBusquedaFolio.InicializaPaginador();
                traspasoMPPAMEDBusquedaFolio.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                MostrarCentrado(traspasoMPPAMEDBusquedaFolio);

                if (traspasoMPPAMEDBusquedaFolio.TraspasoMpPaMed != null)
                {
                    Contexto = traspasoMPPAMEDBusquedaFolio.TraspasoMpPaMed;
                    iudFolio.Value = Convert.ToInt32(Contexto.FolioTraspaso);
                    CargarDatosCancelacion();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_ErrorCargarAyudaFolios, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Cargar los datos del folio para la cancelacion
        /// </summary>
        private void CargarDatosCancelacion()
        {
            try
            {
                var organizacionPL = new OrganizacionPL();
                var tipoAlmacenPL = new TipoAlmacenPL();
                var productoPL = new ProductoPL();


                Contexto.OrganizacionOrigen = organizacionPL.ObtenerPorID(Contexto.AlmacenOrigen.Organizacion.OrganizacionID);
                Contexto.OrganizacionDestino =
                    organizacionPL.ObtenerPorID(Contexto.AlmacenDestino.Organizacion.OrganizacionID);
                Contexto.TipoAlmacenOrigen = tipoAlmacenPL.ObtenerPorID(Contexto.AlmacenOrigen.TipoAlmacen.TipoAlmacenID);
                Contexto.TipoAlmacenDestino = tipoAlmacenPL.ObtenerPorID(Contexto.AlmacenDestino.TipoAlmacen.TipoAlmacenID);
                Contexto.ProductoOrigen = productoPL.ObtenerPorID(Contexto.ProductoOrigen);
                Contexto.ProductoDestino = Contexto.ProductoOrigen;

                if (tiposAlmacenMedicamentos.Contains(Contexto.TipoAlmacenOrigen.TipoAlmacenID))
                {
                    var almacenInventarioPL = new AlmacenInventarioPL();
                    List<AlmacenInventarioInfo> inventarios =
                        almacenInventarioPL.ObtienePorAlmacenIdLlenaProductoInfo(Contexto.AlmacenOrigen);

                    Contexto.AlmacenInventarioOrigen =
                        inventarios.FirstOrDefault(
                            inven => inven.Producto.ProductoId == Contexto.ProductoOrigen.ProductoId);

                }

                stpControles.IsEnabled = false;
                btnGuardar.IsEnabled = false;
                btnCancelarFolio.IsEnabled = true;
                MostrarControlesCantidades();
                if (stpControlesMEDOrigen.IsVisible)
                {
                    var almacenInventarioPL = new AlmacenInventarioPL();
                    List<AlmacenInventarioInfo> inventariosDestino =
                        almacenInventarioPL.ObtienePorAlmacenIdLlenaProductoInfo(Contexto.AlmacenDestino);

                    Contexto.AlmacenInventarioDestino =
                        inventariosDestino.FirstOrDefault(
                            inven => inven.Producto.ProductoId == Contexto.ProductoOrigen.ProductoId);
                    if (Contexto.AlmacenInventarioDestino != null)
                    {
                        if (Contexto.CantidadTraspasarDestino > Contexto.AlmacenInventarioDestino.Cantidad)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_CancelacionSinInventario, MessageBoxButton.OK,
                                  MessageImage.Warning);
                            btnCancelarFolio.IsEnabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_ErrorCargarDatosCancelacion, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto =
                new TraspasoMpPaMedInfo
                {
                    TipoAlmacenOrigen = new TipoAlmacenInfo
                        {
                            ListaTipoAlmacen = configuracionTraspasoAlmacen.Select(tipo => tipo.TipoAlmacenOrigen.TipoAlmacenID).Distinct().ToList()
                        },
                    TipoAlmacenDestino = new TipoAlmacenInfo(),
                    OrganizacionOrigen = new OrganizacionInfo
                        {
                            TipoOrganizacion = new TipoOrganizacionInfo
                                {
                                    TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode()
                                }
                        },
                    OrganizacionDestino = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo
                        {
                            TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode()
                        }
                    },
                    AlmacenOrigen = new AlmacenInfo(),
                    AlmacenDestino = new AlmacenInfo(),
                    ProductoOrigen = new ProductoInfo
                        {
                            Familia = new FamiliaInfo()
                        },
                    ProductoDestino = new ProductoInfo
                    {
                        Familia = new FamiliaInfo()
                    },
                    ProveedorOrigen = new ProveedorInfo(),
                    ProveedorDestino = new ProveedorInfo(),
                    ContratoOrigen = new ContratoInfo(),
                    ContratoDestino = new ContratoInfo(),
                    LoteMpOrigen = new AlmacenInventarioLoteInfo(),
                    LoteMpDestino = new AlmacenInventarioLoteInfo(),
                    AlmacenInventarioOrigen = new AlmacenInventarioInfo(),
                    AlmacenInventarioDestino = new AlmacenInventarioInfo(),
                    CuentaContable = new CuentaSAPInfo()
                };
            CargarCuentasContables();
        }

        /// <summary>
        /// Asigna los eventos a los controles de ayuda
        /// </summary>
        private void AsignarEventosAyudas()
        {
            skAyudaLoteMPOrigen.AyudaConDatos += (sender, args) =>
                                                     {
                                                         Contexto.AlmacenInventarioOrigen.Almacen =
                                                             Contexto.AlmacenOrigen;
                                                         Contexto.LoteMpOrigen.AlmacenInventario.Almacen =
                                                             Contexto.AlmacenOrigen;
                                                     };

            skAyudaLoteMPDestino.AyudaConDatos += (sender, args) =>
            {
                Contexto.AlmacenInventarioDestino.Almacen =
                    Contexto.AlmacenDestino;
                Contexto.LoteMpDestino.AlmacenInventario.Almacen = Contexto.AlmacenDestino;
            };

            #region AyudasOrigen

            skAyudaTipoAlmacenOrigen.AyudaConDatos += (o, args) =>
                {
                    iudFolio.IsEnabled = false;
                    btnBuscarFolioEmbarque.IsEnabled = false;
                    Contexto.AlmacenOrigen.TipoAlmacen = Contexto.TipoAlmacenOrigen;
                    Contexto.AlmacenOrigen.TipoAlmacenID = Contexto.TipoAlmacenOrigen.TipoAlmacenID;
                    Contexto.TipoAlmacenOrigen.ListaTipoAlmacen =
                        configuracionTraspasoAlmacen.Select(tipo => tipo.TipoAlmacenOrigen.TipoAlmacenID).Distinct().
                            ToList();
                    var tiposDestino =
                        configuracionTraspasoAlmacen.Where(
                            tipo => tipo.TipoAlmacenOrigen.TipoAlmacenID == Contexto.TipoAlmacenOrigen.TipoAlmacenID);
                    if (tiposDestino.Any())
                    {
                        Contexto.TipoAlmacenDestino.ListaTipoAlmacen = tiposDestino.Select(tipo => tipo.TipoAlmacenDestino.TipoAlmacenID).Distinct().
                            ToList();
                    }
                    Contexto.AlmacenOrigen.ListaTipoAlmacen = new List<TipoAlmacenInfo>
                        {
                            new TipoAlmacenInfo
                                {
                                    TipoAlmacenID = Contexto.TipoAlmacenOrigen.TipoAlmacenID
                                }
                        };
                };

            skAyudaOrganizacionOrigen.AyudaConDatos += (o, args) =>
            {
                Contexto.AlmacenOrigen.Organizacion = Contexto.OrganizacionOrigen;
                Contexto.OrganizacionOrigen.TipoOrganizacion = new TipoOrganizacionInfo
                {
                    TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode()
                };
            };

            skAyudaAlmacenOrigen.AyudaConDatos += (o, args) =>
            {
                Contexto.ProductoOrigen.AlmacenID = Contexto.AlmacenOrigen.AlmacenID;
                Contexto.AlmacenOrigen.TipoAlmacenID = Contexto.TipoAlmacenOrigen.TipoAlmacenID;
                Contexto.ProveedorOrigen.AlmacenID = Contexto.AlmacenOrigen.AlmacenID;

                if (Contexto.LoteMpOrigen == null)
                {
                    Contexto.LoteMpOrigen = new AlmacenInventarioLoteInfo
                                                {
                                                    AlmacenInventario = new AlmacenInventarioInfo()
                                                                            {
                                                                                Almacen = Contexto.AlmacenOrigen
                                                                            }
                                                };
                }
                else
                {
                    if (Contexto.LoteMpOrigen.AlmacenInventario == null)
                    {
                        Contexto.LoteMpOrigen.AlmacenInventario = new AlmacenInventarioInfo
                                                                      {
                                                                          Almacen = Contexto.AlmacenOrigen
                                                                      };
                    }
                    else
                    {
                        Contexto.LoteMpOrigen.AlmacenInventario.Almacen = Contexto.AlmacenOrigen;
                    }
                }
            };

            skAyudaAlmacenOrigen.PuedeBuscar = () =>
            {
                if ((Contexto.TipoAlmacenOrigen == null || Contexto.TipoAlmacenOrigen.TipoAlmacenID == 0) || (Contexto.OrganizacionOrigen == null || Contexto.OrganizacionOrigen.OrganizacionID == 0))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TraspasoMPPAMED_SeleccionarTipoOrganizacion, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
                return true;
            };

            skAyudaProductoOrigen.PuedeBuscar = () =>
            {
                if (Contexto.AlmacenOrigen == null || Contexto.AlmacenOrigen.AlmacenID == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TraspasoMPPAMED_SeleccionarAlmacen, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
                return true;
            };

            skAyudaProductoOrigen.AyudaConDatos += (o, args) =>
            {
                Contexto.ProductoOrigen.AlmacenID = Contexto.AlmacenOrigen.AlmacenID;
                BloqueaDesbloqueaControlesDestino(true);
                Contexto.ProductoDestino = Contexto.ProductoOrigen;
                skAyudaProductoDestino.IsEnabled = false;
                Contexto.ProveedorOrigen.ProductoID = Contexto.ProductoOrigen.ProductoId;
                Contexto.ProveedorOrigen.OrganizacionID = Contexto.OrganizacionOrigen.OrganizacionID;

                Contexto.ProveedorDestino.ProductoID = Contexto.ProductoOrigen.ProductoId;
                Contexto.ProveedorDestino.OrganizacionID = Contexto.OrganizacionOrigen.OrganizacionID;


                Contexto.LoteMpOrigen.OrganizacionId = Contexto.OrganizacionOrigen.OrganizacionID;
                Contexto.LoteMpOrigen.TipoAlmacenId = Contexto.TipoAlmacenOrigen.TipoAlmacenID;
                Contexto.LoteMpOrigen.ProductoId = Contexto.ProductoOrigen.ProductoId;
                Contexto.LoteMpOrigen.Activo = EstatusEnum.Activo;

                if (tiposAlmacenMedicamentos.Contains(Contexto.TipoAlmacenOrigen.TipoAlmacenID))
                {
                    var almacenInventarioPL = new AlmacenInventarioPL();
                    List<AlmacenInventarioInfo> inventarios =
                        almacenInventarioPL.ObtienePorAlmacenIdLlenaProductoInfo(Contexto.AlmacenOrigen);

                    Contexto.AlmacenInventarioOrigen =
                        inventarios.FirstOrDefault(
                            inven => inven.Producto.ProductoId == Contexto.ProductoOrigen.ProductoId);

                }

            };


            #endregion Ayudas Origen

            #region AyudasDestino

            skAyudaTipoAlmacenDestino.PuedeBuscar += () =>
            {
                if ((Contexto.TipoAlmacenOrigen == null || Contexto.TipoAlmacenOrigen.TipoAlmacenID == 0) || (Contexto.OrganizacionOrigen == null || Contexto.OrganizacionOrigen.OrganizacionID == 0))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TraspasoMPPAMED_SeleccionarTipoOrganizacion, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
                return true;
            };

            skAyudaTipoAlmacenDestino.AyudaConDatos += (o, args) =>
            {
                Contexto.AlmacenDestino.TipoAlmacenID = Contexto.TipoAlmacenDestino.TipoAlmacenID;

                ConfiguracionTraspasoAlmacenInfo configuracionAlmacen =
                    configuracionTraspasoAlmacen.FirstOrDefault(
                        config =>
                        config.TipoAlmacenOrigen.TipoAlmacenID == Contexto.TipoAlmacenOrigen.TipoAlmacenID &&
                        config.TipoAlmacenDestino.TipoAlmacenID == Contexto.TipoAlmacenDestino.TipoAlmacenID);
                if (configuracionAlmacen != null)
                {
                    if (!configuracionAlmacen.DiferenteOrganizacion)
                    {
                        Contexto.OrganizacionDestino = Contexto.OrganizacionOrigen;
                        skAyudaOrganizacionDestino.IsEnabled = false;
                    }
                }
                Contexto.AlmacenDestino.Organizacion = Contexto.OrganizacionDestino;
                MostrarControlesCantidades();

                var tiposDestino =
                  configuracionTraspasoAlmacen.Where(
                      tipo => tipo.TipoAlmacenOrigen.TipoAlmacenID == Contexto.TipoAlmacenOrigen.TipoAlmacenID);
                if (tiposDestino.Any())
                {
                    Contexto.TipoAlmacenDestino.ListaTipoAlmacen = tiposDestino.Select(tipo => tipo.TipoAlmacenDestino.TipoAlmacenID).Distinct().
                        ToList();
                    Contexto.AlmacenDestino.ListaTipoAlmacen = tiposDestino.Select(tipo => tipo.TipoAlmacenDestino).Distinct().
                        ToList();
                }
            };

            skAyudaOrganizacionDestino.AyudaConDatos += (o, args) =>
            {
                Contexto.AlmacenDestino.Organizacion = Contexto.OrganizacionDestino;
            };

            skAyudaAlmacenDestino.AyudaConDatos += (o, args) =>
            {
                Contexto.ProductoDestino.AlmacenID = Contexto.AlmacenDestino.AlmacenID;
                Contexto.AlmacenDestino.TipoAlmacenID = Contexto.TipoAlmacenDestino.TipoAlmacenID;
                Contexto.ProveedorDestino.AlmacenID = Contexto.AlmacenDestino.AlmacenID;

                Contexto.LoteMpDestino.OrganizacionId = Contexto.OrganizacionDestino.OrganizacionID;
                Contexto.LoteMpDestino.TipoAlmacenId = Contexto.TipoAlmacenDestino.TipoAlmacenID;
                Contexto.LoteMpDestino.ProductoId = Contexto.ProductoDestino.ProductoId;
                Contexto.LoteMpDestino.Activo = EstatusEnum.Activo;

                var tiposDestino =
                 configuracionTraspasoAlmacen.Where(
                     tipo => tipo.TipoAlmacenOrigen.TipoAlmacenID == Contexto.TipoAlmacenOrigen.TipoAlmacenID);
                Contexto.TipoAlmacenDestino.ListaTipoAlmacen = tiposDestino.Select(tipo => tipo.TipoAlmacenDestino.TipoAlmacenID).Distinct().
                        ToList();
                Contexto.AlmacenDestino.ListaTipoAlmacen = tiposDestino.Select(tipo => tipo.TipoAlmacenDestino).Distinct().
                       ToList();

                if (Contexto.LoteMpDestino == null)
                {
                    Contexto.LoteMpDestino = new AlmacenInventarioLoteInfo
                    {
                        AlmacenInventario = new AlmacenInventarioInfo()
                        {
                            Almacen = Contexto.AlmacenDestino
                        }
                    };
                }
                else
                {
                    if (Contexto.LoteMpDestino.AlmacenInventario == null)
                    {
                        Contexto.LoteMpDestino.AlmacenInventario = new AlmacenInventarioInfo
                        {
                            Almacen = Contexto.AlmacenDestino
                        };
                    }
                    else
                    {
                        Contexto.LoteMpDestino.AlmacenInventario.Almacen = Contexto.AlmacenDestino;
                    }
                }
            };


            skAyudaCuentaContableContratoDestino.AyudaConDatos += (sender, args) => CargarCuentasContables();

            #endregion AyudasDestino
        }

        /// <summary>
        /// Obtiene la configuracion de traspaso entre almacenes
        /// </summary>
        private void ObtenerConfiguracionTraspasoAlmacen()
        {
            try
            {
                var configuracionTraspasoAlmacenBL = new ConfiguracionTraspasoAlmacenBL();
                configuracionTraspasoAlmacen = configuracionTraspasoAlmacenBL.ObtenerTodos(EstatusEnum.Activo);
                if (configuracionTraspasoAlmacen == null || !configuracionTraspasoAlmacen.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TraspasoMPPAMED_SinConfiguracion, MessageBoxButton.OK, MessageImage.Warning);
                    configuracionTraspasoAlmacen = new List<ConfiguracionTraspasoAlmacenInfo>();
                    stpPrincipal.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_ErrorCargarConfiguracionTraspaso, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Bloquea o desbloquea los controles de destino
        /// </summary>
        /// <param name="desbloquea"></param>
        private void BloqueaDesbloqueaControlesDestino(bool desbloquea)
        {
            skAyudaTipoAlmacenDestino.IsEnabled = desbloquea;
            skAyudaOrganizacionDestino.IsEnabled = desbloquea;
            skAyudaAlmacenDestino.IsEnabled = desbloquea;
            skAyudaProductoDestino.IsEnabled = desbloquea;
        }

        /// <summary>
        /// Muestra los controles adicionales para el traspaso
        /// </summary>
        private void MostrarControlesCantidades()
        {
            OcultarControlesAdicionales();

            if (tiposAlmacenMedicamentos.Contains(Contexto.TipoAlmacenOrigen.TipoAlmacenID))
            {
                stpControlesMEDOrigen.Visibility = Visibility.Visible;
                stpControlesMEDDestino.Visibility = Visibility.Visible;
            }
            else

                stpControlesMPOrigen.Visibility = Visibility.Visible;
            stpControlesMPDestino.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Oculta los controles adicionales para el traspaso
        /// </summary>
        private void OcultarControlesAdicionales()
        {
            stpControlesMEDDestino.Visibility = Visibility.Hidden;
            stpControlesMEDOrigen.Visibility = Visibility.Hidden;
            stpControlesMPDestino.Visibility = Visibility.Hidden;
            stpControlesMPOrigen.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Guardar el traspaso 
        /// </summary>
        private void Guardar()
        {
            try
            {
                string mensajeValidar = ValidaGuardar();

                if (!string.IsNullOrWhiteSpace(mensajeValidar))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensajeValidar, MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }

                var traspasoMateriaPrimaBL = new TraspasoMateriaPrimaBL();
                var usuario = new UsuarioInfo
                {
                    UsuarioID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                    }

                };
                Contexto.Usuario = usuario;
                var resultado = traspasoMateriaPrimaBL.GuardarTraspaso(Contexto);
                long folio = 0;
                if (resultado != null)
                {
                    foreach (var poliza in resultado)
                    {
                        folio = poliza.Key;
                        if (poliza.Value != null)
                        {
                            var exportarPoliza = new ExportarPoliza();
                            exportarPoliza.ImprimirPoliza(poliza.Value,
                                                          string.Format("{0} {1} Folio - {2}", "Poliza",
                                                                        TipoPoliza.EntradaTraspaso, folio));
                        }

                    }
                }
                string mensaje = string.Format(Properties.Resources.TraspasoMPPAMED_GuardadoConExito, folio);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                  MessageBoxButton.OK,
                                  MessageImage.Correct);
                Limpiar();
            }
            catch (ExcepcionServicio ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_ErrorGuardar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Limpia la pantalla para su uso
        /// </summary>
        private void Limpiar()
        {
            InicializaContexto();
            OcultarControlesAdicionales();
            stpPrincipal.IsEnabled = true;
            stpControles.IsEnabled = true;
            iudFolio.IsEnabled = true;
            btnCancelarFolio.IsEnabled = true;
            btnGuardar.IsEnabled = true;
            iudFolio.Value = 0;
            btnBuscarFolioEmbarque.IsEnabled = true;
            BloqueaDesbloqueaControlesDestino(false);
        }

        /// <summary>
        /// Crear un nuevo lote en AlamcenInventarioLote
        /// </summary>
        private void CrearNuevoLote()
        {
            try
            {
                AlmacenInventarioLoteInfo almacenInventarioLote = null;

                int usuarioId = AuxConfiguracion.ObtenerUsuarioLogueado();
                var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                var almacenInventarioPl = new AlmacenInventarioPL();

                var almacenInventario = new AlmacenInventarioInfo
                                            {
                                                AlmacenID = Contexto.AlmacenDestino.AlmacenID,
                                                ProductoID = Contexto.ProductoDestino.ProductoId
                                            };

                almacenInventario = almacenInventarioPl.ObtenerPorAlmacenIdProductoId(almacenInventario);

                // Si el producto no se encuentra en el almacen inventario, lo insertamos
                if (almacenInventario == null)
                {
                    almacenInventario = new AlmacenInventarioInfo
                                   {
                                       AlmacenInventarioID =
                                           almacenInventarioPl.Crear(new AlmacenInventarioInfo
                                           {
                                               AlmacenID = Contexto.AlmacenDestino.AlmacenID,
                                               ProductoID = Contexto.ProductoDestino.ProductoId,
                                               UsuarioCreacionID = usuarioId
                                           }),
                                       AlmacenID = Contexto.AlmacenDestino.AlmacenID
                                   };
                }

                int loteIdCreado = almacenInventarioLotePl.Crear(new AlmacenInventarioLoteInfo
                {
                    AlmacenInventarioLoteId = 0,
                    AlmacenInventario =
                        new AlmacenInventarioInfo { AlmacenInventarioID = almacenInventario.AlmacenInventarioID },
                    Cantidad = 0,
                    PrecioPromedio = 0,
                    Piezas = 0,
                    Importe = 0,
                    Activo = EstatusEnum.Activo,
                    UsuarioCreacionId = usuarioId,
                }, new AlmacenInventarioInfo
                {
                    AlmacenID = almacenInventario.AlmacenID,
                    ProductoID = Contexto.ProductoDestino.ProductoId
                });

                almacenInventarioLote = almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorId(loteIdCreado);


                Contexto.LoteMpDestino = almacenInventarioLote;
                skAyudaLoteMPDestino.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_ErrorCrearLote, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Cancela un folio de traspaso
        /// </summary>
        private void CancelarTraspaso()
        {
            try
            {
                string mensajeValidar = ValidaGuardar();

                if (!string.IsNullOrWhiteSpace(mensajeValidar))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensajeValidar, MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }

                var traspasoMateriaPrimaBL = new TraspasoMateriaPrimaBL();
                var usuario = new UsuarioInfo
                {
                    UsuarioID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                    }

                };
                Contexto.Usuario = usuario;
                var resultado = traspasoMateriaPrimaBL.CancelarTraspaso(Contexto);
                long folio = 0;
                if (resultado != null)
                {
                    foreach (var poliza in resultado)
                    {
                        folio = poliza.Key;
                        if (poliza.Value != null)
                        {
                            var exportarPoliza = new ExportarPoliza();
                            exportarPoliza.ImprimirPoliza(poliza.Value,
                                                          string.Format("{0} {1} Folio - {2} Cancelación", "Póliza",
                                                                        TipoPoliza.EntradaTraspaso, folio));
                        }

                    }
                }
                string mensaje = string.Format(Properties.Resources.TraspasoMPPAMED_FolioCancelado, folio);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                  MessageBoxButton.OK,
                                  MessageImage.Correct);
                Limpiar();
            }
            catch (ExcepcionServicio ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_ErrorCancelar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida los valores antes de guardarlos
        /// </summary>
        /// <returns></returns>
        private string ValidaGuardar()
        {
            //bool valido = true;
            string mensaje = string.Empty;

            #region Validar Controles Comunes

            if (Contexto.TipoAlmacenOrigen == null || Contexto.TipoAlmacenOrigen.TipoAlmacenID == 0)
            {
                skAyudaTipoAlmacenOrigen.AsignarFoco();
                return Properties.Resources.TraspasoMPPAMED_TipoAlmacenOrigenRequerido;
            }

            if (Contexto.OrganizacionOrigen == null || Contexto.OrganizacionOrigen.OrganizacionID == 0)
            {
                skAyudaOrganizacionOrigen.AsignarFoco();
                return Properties.Resources.TraspasoMPPAMED_OrganizacionOrigenRequerido;
            }

            if (Contexto.AlmacenOrigen == null || Contexto.AlmacenOrigen.AlmacenID == 0)
            {
                skAyudaAlmacenOrigen.AsignarFoco();
                return Properties.Resources.TraspasoMPPAMED_AlmacenOrigenRequerido;
            }

            if (Contexto.ProductoOrigen == null || Contexto.ProductoOrigen.ProductoId == 0)
            {
                skAyudaProductoOrigen.AsignarFoco();
                return Properties.Resources.TraspasoMPPAMED_ProductoOrigenRequerido;
            }

            if (Contexto.TipoAlmacenDestino == null || Contexto.TipoAlmacenDestino.TipoAlmacenID == 0)
            {
                skAyudaTipoAlmacenDestino.AsignarFoco();
                mensaje = Properties.Resources.TraspasoMPPAMED_TipoAlmacenDestinoRequerido;
            }

            if (Contexto.OrganizacionDestino == null || Contexto.OrganizacionDestino.OrganizacionID == 0)
            {
                skAyudaOrganizacionDestino.AsignarFoco();
                mensaje = Properties.Resources.TraspasoMPPAMED_OrganizacionDestinoRequerido;
            }

            if (Contexto.AlmacenDestino == null || Contexto.AlmacenDestino.AlmacenID == 0)
            {
                skAyudaAlmacenDestino.AsignarFoco();
                mensaje = Properties.Resources.TraspasoMPPAMED_AlmacenDestinoRequerido;
            }

            if (Contexto.ProductoDestino == null || Contexto.ProductoDestino.ProductoId == 0)
            {
                skAyudaProductoDestino.AsignarFoco();
                return Properties.Resources.TraspasoMPPAMED_ProductoDestinoRequerido;
            }



            #endregion Validar Controles Comunes

            #region Validar Controles Materia Prima
            if (stpControlesMPOrigen.IsVisible)
            {
                if (Contexto.LoteMpOrigen == null || Contexto.LoteMpOrigen.AlmacenInventarioLoteId == 0)
                {
                    skAyudaLoteMPOrigen.AsignarFoco();
                    return Properties.Resources.TraspasoMPPAMED_LoteOrigenRequerido;
                }

                if (Contexto.LoteMpDestino == null || Contexto.LoteMpDestino.AlmacenInventarioLoteId == 0)
                {
                    skAyudaLoteMPDestino.AsignarFoco();
                    return Properties.Resources.TraspasoMPPAMED_LoteDestinoRequerido;
                }
                if (Contexto.CantidadTraspasarOrigen == 0 || Contexto.CantidadTraspasarDestino == 0)
                {
                    dudCantidadMPOrigen.Focus();
                    return Properties.Resources.TraspasoMPPAMED_CantidadRequerido;
                }
                if (Contexto.CantidadTraspasarOrigen > Contexto.LoteMpOrigen.Cantidad)
                {
                    return Properties.Resources.TraspasoMPPAMED_CantidadSuperaInventario;
                }
                if (string.IsNullOrWhiteSpace(Contexto.JustificacionDestino))
                {
                    txtJustificacionMPDestino.Focus();
                    return Properties.Resources.TraspasoMPPAMED_JustificacionDestinoRequerido;
                }
            }

            #endregion Validar Controles Materia Prima

            #region Validar Controles Medicamento
            if (stpControlesMEDOrigen.IsVisible)
            {
                if (Contexto.CantidadTraspasarOrigen == 0 || Contexto.CantidadTraspasarDestino == 0)
                {
                    dudCantidadMEDOrigen.Focus();
                    return Properties.Resources.TraspasoMPPAMED_CantidadRequerido;
                }
                if (Contexto.CantidadTraspasarOrigen > Contexto.AlmacenInventarioOrigen.Cantidad)
                {
                    return Properties.Resources.TraspasoMPPAMED_CantidadSuperaInventario;
                }
                if (string.IsNullOrWhiteSpace(Contexto.JustificacionDestino))
                {
                    txtJustificacionMEDDestino.Focus();
                    return Properties.Resources.TraspasoMPPAMED_JustificacionDestinoRequerido;
                }
            }

            #endregion Validar Controles Medicamento

            return mensaje;
        }

        /// <summary>
        /// Consulta un traspaso para su cancelacion por su Folio
        /// </summary>
        private void ConsultarTraspasoPorFolio()
        {
            try
            {
                var traspasoMateriaPrimaBL = new TraspasoMateriaPrimaBL();
                if (iudFolio.Value != null)
                {
                    var filtro = new FiltroTraspasoMpPaMed
                        {
                            Organizacion = new OrganizacionInfo
                                {
                                    OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                                },
                            Folio = iudFolio.Value.Value,
                            Activo = EstatusEnum.Activo
                        };
                    TraspasoMpPaMedInfo traspasoMp = traspasoMateriaPrimaBL.ObtenerPorFolioTraspaso(filtro);
                    if (traspasoMp == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.TraspasoMPPAMED_NoSeEncuentraFolio, MessageBoxButton.OK,
                                          MessageImage.Error);
                        return;
                    }
                    Contexto = traspasoMp;
                    CargarDatosCancelacion();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.TraspasoMPPAMED_ErrorCargarDatosCancelacion, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        #endregion METODOS

        private void IudFolio_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ConsultarTraspasoPorFolio();
        }
    }
}
