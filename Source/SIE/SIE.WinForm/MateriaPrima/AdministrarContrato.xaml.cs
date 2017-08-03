using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
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
    /// Lógica de interacción para AdministrarContrato.xaml
    /// </summary>
    public partial class AdministrarContrato
    {

        #region Propiedades
        private ContratoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ContratoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private SKAyuda<ProveedorInfo> skAyudaProveedores;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AdministrarContrato()
        {
            InitializeComponent();
            InicializaContexto();
            ObtenerProductos();
            //ObtenerProveedores();
            AgregarAyudaProveedor();
            ObtenerTipoContrato();
            ObtenerTipoFlete();
            CargarCboEstatus();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento loaded de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdministrarContrato_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
                skAyudaOrganizacion.AyudaConDatos += (o, args) =>
                {
                    Contexto.Organizacion.ListaTiposOrganizacion =
                                                    new List<TipoOrganizacionInfo>
                                                         {
                                                             new TipoOrganizacionInfo
                                                                 {
                                                                     TipoOrganizacionID =
                                                                         TipoOrganizacion.Ganadera.GetHashCode()
                                                                 }
                                                         };
                    
                };
                skAyudaProducto.ObjetoNegocio = new ProductoPL();
                skAyudaProducto.AyudaConDatos += (o, args) =>
                {
                    Contexto.Producto.Familias =
                                                    new List<FamiliaInfo>
                                                         {
                                                             new FamiliaInfo
                                                                 {
                                                                     FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()
                                                                 },
                                                                 new FamiliaInfo
                                                                 {
                                                                     FamiliaID = FamiliasEnum.Premezclas.GetHashCode()
                                                                 }
                                                         };

                };
                
                
                    
            
            ucPaginacion.DatosDelegado += ObtenerContratos;
                ucPaginacion.AsignarValoresIniciales();
                
                //CboProducto.SelectedIndex = 0;
                //CboProveedor.SelectedIndex = 0;
                CboTipoCompra.SelectedIndex = 0;
                CboFlete.SelectedIndex = 0;
                //CboEstatus.SelectedIndex = 0;
                Buscar();
                TxtFolioContrato.ClearValue(IntegerUpDown.ValueProperty);
                TxtFolioContrato.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.AdministrarContrato_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.AdministrarContrato_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento click de BtnEditar ubicado en gridcontratos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var contratoInfo = (ContratoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);

                if (contratoInfo == null) return;
                var nuevoContrato = new CrearContrato(contratoInfo);
                MostrarCentrado(nuevoContrato);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento click de btnLimpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //CboProducto.SelectedIndex = 0;
                //CboProveedor.SelectedIndex = 0;
                skAyudaProveedores.LimpiarCampos();
                CboTipoCompra.SelectedIndex = 0;
                CboFlete.SelectedIndex = 0;
                CboEstatus.SelectedIndex = 0;
                Contexto.Folio = 0;
                TxtFolioContrato.ClearValue(IntegerUpDown.ValueProperty);
                TxtFolioContrato.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento click de btnNuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var nuevoContrato = new CrearContrato(true);
                MostrarCentrado(nuevoContrato);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento click de btnBuscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Contexto.Folio = TxtFolioContrato.Value.HasValue ? TxtFolioContrato.Value.Value : 0;
            //Contexto.Folio = TxtFolioContrato.Text == String.Empty ? 0 : Convert.ToInt32(TxtFolioContrato.Text.Trim());

            if (String.IsNullOrEmpty(skAyudaProveedores.Clave.Trim()) &&
                String.IsNullOrEmpty(skAyudaProveedores.Descripcion.Trim()))
            {
                Contexto.Proveedor = new ProveedorInfo();
            }
            else
            {
                var proveedorPl = new ProveedorPL();
                var proveedorInfo = new ProveedorInfo()
                {
                    CodigoSAP = skAyudaProveedores.Clave
                };
                proveedorInfo = proveedorPl.ObtenerPorCodigoSAP(proveedorInfo);
                Contexto.Proveedor = proveedorInfo;
            }
            Buscar();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Iniciliaza el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ContratoInfo
                {
                    Organizacion = new OrganizacionInfo
                        {
                            ListaTiposOrganizacion = new List<TipoOrganizacionInfo>
                                {
                                    new TipoOrganizacionInfo
                                        {
                                            TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode()
                                        }
                                },
                        },
                        Producto = new ProductoInfo
                            {
                                  Familias =
                                                    new List<FamiliaInfo>
                                                         {
                                                             new FamiliaInfo
                                                                 {
                                                                     FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()
                                                                 },
                                                                 new FamiliaInfo
                                                                 {
                                                                     FamiliaID = FamiliasEnum.Premezclas.GetHashCode()
                                                                 }
                                                         }
                            }

                };
        }

        /// <summary>
        /// Obtiene los productos
        /// </summary>
        private void ObtenerProductos()
        {
            try
            {
                var productoPl = new ProductoPL();
                IList<ProductoInfo> productos = productoPl.ObtenerPorEstados(EstatusEnum.Activo);

                IList<ProductoInfo> productosFiltrados = null;

                //Se filtran productos para las subfamilias especificadas en el requerimiento Administrar Contrato
                if (productos != null)
                {
                    productosFiltrados = productos.Where(productoInfoT => productoInfoT.FamiliaId == FamiliasEnum.MateriaPrimas.GetHashCode() || productoInfoT.SubfamiliaId == (int)SubFamiliasEnum.Pacas).ToList();
                }
                //

                if (productosFiltrados != null && productosFiltrados.Any())
                {
                    AgregarElementoInicialProducto(productosFiltrados);
                    Contexto.ListaProductos = productosFiltrados;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.AdministrarContrato_MensajeErrorProductos, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Agrega un elemento a lista de producto
        /// en la posicion cero
        /// </summary>
        private void AgregarElementoInicialProducto(IList<ProductoInfo> productos)
        {
            var productoInicial = new ProductoInfo { ProductoId = 0, ProductoDescripcion = Properties.Resources.cbo_Seleccionar };
            if (!productos.Contains(productoInicial))
            {
                productos.Insert(0, productoInicial);
            }
        }

        /// <summary>
        /// Carga los datos para la ayuda del proveedor
        /// </summary>
        private void AgregarAyudaProveedor()
        {
            var proveedorInfo = new ProveedorInfo
            {
                ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode() },///Descomentar
                    },
                Activo = EstatusEnum.Activo
            };
            skAyudaProveedores = new SKAyuda<ProveedorInfo>(200, false, proveedorInfo
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
            skAyudaProveedores.ObtenerDatos += ObtenerDatosProveedor;
            skAyudaProveedores.AsignaTabIndex(1);
            SplAyudaProveedor.Children.Clear();
            SplAyudaProveedor.Children.Add(skAyudaProveedores);
            Contexto.Proveedor = skAyudaProveedores.Info;
        }

        /// <summary>
        /// Carga los datos para la ayuda del proveedor
        /// </summary>
        private void AgregarAyudaProveedorAlmacen()
        {
            var proveedorInfo = new ProveedorInfo
            {
                ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode() },///Descomentar*/
                    },
                Activo = EstatusEnum.Activo
            };
            skAyudaProveedores = new SKAyuda<ProveedorInfo>(200, false, proveedorInfo
                                                   , "PropiedadClaveCrearContrato"
                                                   , "PropiedadDescripcionCrearContratoAlmacen",
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
            skAyudaProveedores.ObtenerDatos += ObtenerDatosProveedorAlmacen;
            skAyudaProveedores.AsignaTabIndex(1);
            SplAyudaProveedor.Children.Clear();
            SplAyudaProveedor.Children.Add(skAyudaProveedores);
            Contexto.Proveedor = skAyudaProveedores.Info;
        }

        private void ObtenerDatosProveedorAlmacen(string filtro)
        {
            try
            {
                if (string.IsNullOrEmpty(filtro))
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    return;
                }
                if (skAyudaProveedores.Info == null)
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    return;
                }
                if (skAyudaProveedores.Info.TipoProveedor.TipoProveedorID !=
                    TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() && skAyudaProveedores.Info.TipoProveedor.TipoProveedorID !=
                    TipoProveedorEnum.ProveedoresFletes.GetHashCode())///Descomentar
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };

                    skAyudaProveedores.LimpiarCampos();
                    skAyudaProveedores.AsignarFoco();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CrearContrato_MensajeProveedorInvalido,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                else
                {
                    var proveedorAlmacen = new ProveedorAlmacenInfo();
                    var proveedorAlmacenPl = new ProveedorAlmacenPL();
                    var proveedor = new ProveedorInfo();
                    var proveedorPl = new ProveedorPL();
                    proveedor = proveedorPl.ObtenerPorCodigoSAP(new ProveedorInfo() { CodigoSAP = filtro });

                    if (proveedor != null)
                    {
                        proveedorAlmacen = proveedorAlmacenPl.ObtenerPorProveedorId(proveedor);
                        if (proveedorAlmacen != null)
                        {
                            skAyudaProveedores.Info = new ProveedorInfo
                            {
                                ListaTiposProveedor = new List<TipoProveedorInfo>
                                {
                                    new TipoProveedorInfo
                                    {
                                        TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                                    },
                                    new TipoProveedorInfo
                                    {
                                        TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                                    }
                                },
                                Activo = EstatusEnum.Activo
                            };
                        }
                        else
                        {
                            skAyudaProveedores.Info = new ProveedorInfo
                            {
                                ListaTiposProveedor = new List<TipoProveedorInfo>
                                {
                                    new TipoProveedorInfo
                                    {
                                        TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                                    },
                                    new TipoProveedorInfo
                                    {
                                        TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                                    }
                                },
                                Activo = EstatusEnum.Activo
                            };
                            skAyudaProveedores.LimpiarCampos();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CrearContrato_MensajeProveedorSinAlmacen,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                        }
                    }
                    else
                    {
                        skAyudaProveedores.Info = new ProveedorInfo
                        {
                            ListaTiposProveedor = new List<TipoProveedorInfo>
                            {
                                new TipoProveedorInfo
                                {
                                    TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                                },
                                new TipoProveedorInfo
                                {
                                    TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                                }
                            },
                            Activo = EstatusEnum.Activo
                        };
                        skAyudaProveedores.LimpiarCampos();
                        skAyudaProveedores.AsignarFoco();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CrearContrato_MensajeProveedorInvalido,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene los datos del proveedor con la clave obtenida en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProveedor(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                 TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    return;
                }
                if (skAyudaProveedores.Info == null)
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                 TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    return;
                }
                if (skAyudaProveedores.Info.TipoProveedor.TipoProveedorID !=
                    TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() && skAyudaProveedores.Info.TipoProveedor.TipoProveedorID !=
                    TipoProveedorEnum.ProveedoresFletes.GetHashCode())///Descomentar
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                 TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };

                    skAyudaProveedores.LimpiarCampos();
                    skAyudaProveedores.AsignarFoco();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CrearContrato_MensajeProveedorInvalido,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                else
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                 TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
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
        /// Obtiene los tipo contrato
        /// </summary>
        private void ObtenerTipoContrato()
        {
            try
            {
                var tipoContratoPl = new TipoContratoPL();
                IList<TipoContratoInfo> tipoContrato = tipoContratoPl.ObtenerTodos();
                IList<TipoContratoInfo> tipoContratoFiltrado = null;
                //
                if (tipoContrato != null)
                {
                    tipoContratoFiltrado = tipoContrato.Where(tipoContratoInfoT => tipoContratoInfoT.TipoContratoId == (int)TipoContratoEnum.BodegaNormal || tipoContratoInfoT.TipoContratoId == (int)TipoContratoEnum.BodegaTercero ||
                        tipoContratoInfoT.TipoContratoId == (int)TipoContratoEnum.EnTransito).ToList();
                }

                if (tipoContratoFiltrado != null && tipoContratoFiltrado.Any())
                {
                    AgregarElementoInicialTipoContrato(tipoContratoFiltrado);
                    Contexto.ListaTipoContrato = tipoContratoFiltrado;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.AdministrarContrato_MensajeErrorTipoContrato, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Agrega un elemento a lista de tipo contrato
        /// en la posicion cero
        /// </summary>
        private void AgregarElementoInicialTipoContrato(IList<TipoContratoInfo> tipoContrato)
        {
            var tipoContratoInicial = new TipoContratoInfo { TipoContratoId = 0, Descripcion = Properties.Resources.cbo_Seleccionar };
            if (!tipoContrato.Contains(tipoContratoInicial))
            {
                tipoContrato.Insert(0, tipoContratoInicial);
            }
        }

        /// <summary>
        /// Obtiene los tipos de flete
        /// </summary>
        private void ObtenerTipoFlete()
        {
            try
            {
                var tipoFletePl = new TipoFletePL();
                IList<TipoFleteInfo> tipoFlete = tipoFletePl.ObtenerTiposFleteTodos(EstatusEnum.Activo);
                IList<TipoFleteInfo> tipoFleteFiltrado = null;
                //
                if (tipoFlete != null)
                {
                    tipoFleteFiltrado = tipoFlete.Where(tipoFleteInfoT => tipoFleteInfoT.TipoFleteId == (int)TipoFleteEnum.LibreAbordo ||
                                        tipoFleteInfoT.TipoFleteId == (int)TipoFleteEnum.PagoenGanadera).ToList();
                }
                //
                if (tipoFleteFiltrado != null && tipoFleteFiltrado.Any())
                {
                    AgregarElementoInicialTipoFlete(tipoFleteFiltrado);
                    Contexto.ListaTipoFlete = tipoFleteFiltrado;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.AdministrarContrato_MensajeErrorTipoFlete, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Agrega un elemento a lista de tipo flete
        /// en la posicion cero
        /// </summary>
        private void AgregarElementoInicialTipoFlete(IList<TipoFleteInfo> tipoFlete)
        {
            var tipoFleteInicial = new TipoFleteInfo { TipoFleteId = 0, Descripcion = Properties.Resources.cbo_Seleccionar };
            if (!tipoFlete.Contains(tipoFleteInicial))
            {
                tipoFlete.Insert(0, tipoFleteInicial);
            }
        }

        /// <summary>
        /// Carga los valores del combo de estatus
        /// </summary>
        private void CargarCboEstatus()
        {
            try
            {
                //IList<EstatusContratoEnum> estatusEnums = Enum.GetValues(typeof(EstatusContratoEnum)).Cast<EstatusContratoEnum>().ToList();
                //CboEstatus.ItemsSource = estatusEnums;
                //CboEstatus.SelectedItem = EstatusContratoEnum.Activo;

                var estatusPl = new EstatusPL();
                var listaEstatus = estatusPl.ObtenerEstatusTipoEstatus(TipoEstatusEnum.Contratos.GetHashCode());
                var estatusSeleccione = new EstatusInfo
                    {
                        EstatusId = 0,
                        Descripcion = Properties.Resources.cbo_Seleccionar
                    };
                listaEstatus.Insert(0, estatusSeleccione);
                CboEstatus.ItemsSource = listaEstatus;
                Contexto.Estatus = new EstatusInfo
                    {
                        EstatusId = Estatus.ConActivo.GetHashCode()
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //Agregar excepcion
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Buscar una lista de productos
        /// </summary>
        public void Buscar()
        {
            ObtenerContratos(1, 15);
        }

        /// <summary>
        /// Obtiene los contratos
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerContratos(int inicio, int limite)
        {
            try
            {
                var contratoPl = new ContratoPL();
                if (inicio == 1)
                {
                    ucPaginacion.AsignarValoresIniciales();
                }
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<ContratoInfo> resultadoInfo = contratoPl.ObtenerPorPagina(pagina, Contexto);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridContratos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    ucPaginacion.AsignarValoresIniciales();
                    gridContratos.ItemsSource = new List<ContratoInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.AdministrarContrato_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.AdministrarContrato_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion

        private void CboTipoCompra_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CboTipoCompra.SelectedItem == null || CboTipoCompra.SelectedIndex == 0)
                {
                    AgregarAyudaProveedor();
                }
                else
                {
                    switch ((int)CboTipoCompra.SelectedValue)
                    {
                        case (int)TipoContratoEnum.BodegaNormal:
                            AgregarAyudaProveedor();
                            break;
                        case (int)TipoContratoEnum.BodegaTercero:
                            AgregarAyudaProveedorAlmacen();
                            break;
                        case (int)TipoContratoEnum.EnTransito:
                            AgregarAyudaProveedorAlmacen();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
