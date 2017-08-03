using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SIE.Services.Servicios.BL;
using SuKarne.Controls.MessageBox;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SuKarne.Controls.Enum;
using Application = System.Windows.Application;
using Ionic.Zip;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ReimpresionPoliza.xaml
    /// </summary>
    public partial class ReimpresionPoliza
    {
        #region PROPIEDADES

        private ReimpresionPolizaModel Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ReimpresionPolizaModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        #endregion PROPIEDADES

        #region VARIABLES PRIVADAS

        #endregion VARIABLES PRIVADAS

        #region CONSTRUCTORES

        public ReimpresionPoliza()
        {
            InitializeComponent();
            InicializaContexto();
        }

        #endregion CONSTRUCTORES

        #region METODOS

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            int usuarioID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = new ReimpresionPolizaModel
                           {
                               Animal = new AnimalInfo
                                            {
                                                OrganizacionIDEntrada = organizacionID,
                                                UsuarioCreacionID = usuarioID
                                            },
                               EntradaGanado = new EntradaGanadoInfo
                                                   {
                                                       OrganizacionID = organizacionID,
                                                       UsuarioCreacionID = usuarioID
                                                   },
                               VentaGanado = new VentaGanadoInfo
                                                 {
                                                     Activo = false,
                                                     Lote = new LoteInfo
                                                                {
                                                                    OrganizacionID = organizacionID
                                                                },
                                                     UsuarioCreacionID = usuarioID
                                                 },
                               Almacen = new AlmacenInfo
                                             {
                                                 Organizacion = new OrganizacionInfo
                                                                    {
                                                                        OrganizacionID = organizacionID
                                                                    },
                                                 TipoAlmacen = new TipoAlmacenInfo
                                                                   {
                                                                       TipoAlmacenID =
                                                                           TipoAlmacenEnum.MateriasPrimas.GetHashCode()
                                                                   },
                                                 UsuarioCreacionID = usuarioID
                                             },
                               ContenedorEntradaMateriaPrima = new ContenedorEntradaMateriaPrimaInfo
                                                                   {
                                                                       Contrato = new ContratoInfo
                                                                                      {
                                                                                          Proveedor =
                                                                                              new ProveedorInfo(),
                                                                                          Organizacion =
                                                                                              new OrganizacionInfo
                                                                                                  {
                                                                                                      OrganizacionID =
                                                                                                          organizacionID
                                                                                                  }
                                                                                      },
                                                                       EntradaProducto = new EntradaProductoInfo
                                                                                             {
                                                                                                 Organizacion =
                                                                                                     new OrganizacionInfo
                                                                                                         {
                                                                                                             OrganizacionID
                                                                                                                 =
                                                                                                                 organizacionID
                                                                                                         }
                                                                                             },
                                                                       Producto = new ProductoInfo(),
                                                                   },
                               Pedido = new PedidoInfo
                                            {
                                                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                                                ListaEstatusPedido = new List<EstatusInfo>
                                                                         {
                                                                             new EstatusInfo
                                                                                 {
                                                                                     EstatusId =
                                                                                         (int) Estatus.PedidoProgramado
                                                                                 },
                                                                             new EstatusInfo
                                                                                 {
                                                                                     EstatusId =
                                                                                         (int) Estatus.PedidoParcial
                                                                                 },
                                                                         },
                                                Activo = EstatusEnum.Activo
                                            },
                               Fecha = DateTime.Today,
                               SolicitudProducto = new FolioSolicitudInfo
                                   {
                                       OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario(),
                                       EstatusID = Estatus.SolicitudProductoRecibido.GetHashCode()
                                   },
                               ProduccionFormula = new ProduccionFormulaInfo
                                   {
                                       Organizacion = new OrganizacionInfo
                                           {
                                               OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                                           },
                                       Formula = new FormulaInfo(),
                                       Activo = EstatusEnum.Activo
                                   },
                               SalidaProducto = new SalidaProductoInfo
                                                    {
                                                        Organizacion = new OrganizacionInfo
                                                                           {
                                                                               OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                                                                           }
                                                    }
                           };
        }

        /// <summary>
        /// Obtiene los Tipos de Poliza
        /// </summary>
        private void ObtenerTiposPoliza()
        {
            var tiposPolizaPL = new TipoPolizaPL();
            IList<TipoPolizaInfo> tiposPoliza = tiposPolizaPL.ObtenerTodos(EstatusEnum.Activo);
            if (tiposPoliza == null)
            {
                tiposPoliza = new List<TipoPolizaInfo>();
            }
            var tipoPoliza = new TipoPolizaInfo
                                 {
                                     TipoPolizaID = 0,
                                     Descripcion = Properties.Resources.cbo_Seleccione,
                                     ImprimePoliza = true
                                 };
            tiposPoliza.Insert(0, tipoPoliza);
            Contexto.TiposPoliza = tiposPoliza.Where(imp => imp.ImprimePoliza).ToList();
        }

        /// <summary>
        /// Agrega los datos necesarios para la 
        /// ejecucion de ayuda de entrada de ganado
        /// </summary>
        private void GenerarAyudaEntradaGanado()
        {
            Contexto.EntradaGanado = new EntradaGanadoInfo
                                         {
                                             OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario(),
                                             UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                                         };
            skAyuda = new ControlAyuda
                          {
                              ObjetoNegocio = new EntradaGanadoPL(),
                              MetodoInvocacionBusqueda = "ObtenerEntradaPaginado",
                              MetodoInvocacion = "ObtenerEntradasGanadoRecibidas",
                              MensajeAgregarBusqueda = Properties.Resources.Folio_Seleccionar,
                              MensajeCerrarBusqueda = Properties.Resources.Folio_SalirSinSeleccionar,
                              MensajeClaveInexistenteBusqueda = Properties.Resources.Folio_Inexistente,
                              TituloBusqueda = Properties.Resources.BusquedaEntradaGanado_Titulo,
                              EncabezadoClaveBusqueda = Properties.Resources.AyudaEntradaGanado_Grid_Clave,
                              EncabezadoDescripcionBusqueda = Properties.Resources.AyudaEntradaGanado_Grid_Descripcion,
                              ConceptoBusqueda = Properties.Resources.LeyehdaAyudaBusquedaFolio,
                              DataContext = Contexto.EntradaGanado,
                              EsAyudaSimple = true,
                              EsBindeable = true,
                              CampoClave = "FolioEntrada",
                              CampoDescripcion = "Observacion",
                              CampoLlaveOcultaClave = "EntradaGanadoID",
                              AnchoDescripcion = 80,
                              MaximoCaracteres = 10,
                              ControlIndex = 1,
                          };
            lblAyuda.Content = Properties.Resources.LeyehdaAyudaBusquedaFolio;
            skAyuda.AyudaConDatos += (sender, args) =>
                                         {
                                             skAyuda.Descripcion = skAyuda.Clave;
                                             Contexto.EntradaGanado.OrganizacionID =
                                                 AuxConfiguracion.ObtenerOrganizacionUsuario();
                                         };
        }

        /// <summary>
        /// Agrega los datos necesarios para la 
        /// ejecucion de ayuda de consumo de producto
        /// </summary>
        private void GenerarAyudaConsumoProducto()
        {
            Contexto.Almacen = new AlmacenInfo
                                   {
                                       Organizacion = new OrganizacionInfo
                                                          {
                                                              OrganizacionID =
                                                                  AuxConfiguracion.ObtenerOrganizacionUsuario()
                                                          },
                                       TipoAlmacen = new TipoAlmacenInfo
                                                         {
                                                             TipoAlmacenID =
                                                                 TipoAlmacenEnum.MateriasPrimas.GetHashCode()
                                                         },
                                       UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                                   };
            skAyuda = new ControlAyuda
                          {
                              ObjetoNegocio = new AlmacenPL(),
                              MetodoInvocacionBusqueda = "ObtenerPorPaginaPoliza",
                              MetodoInvocacion = "ObtenerPorAlmacenPoliza",
                              MensajeAgregarBusqueda = Properties.Resources.Almacen_Seleccionar,
                              MensajeCerrarBusqueda = Properties.Resources.Almacen_SalirSinSeleccionar,
                              MensajeClaveInexistenteBusqueda = Properties.Resources.Almacen_Inexistente,
                              TituloBusqueda = Properties.Resources.BusquedaAlmacen_Titulo,
                              EncabezadoClaveBusqueda = Properties.Resources.AyudaAlmacen_Grid_Clave,
                              EncabezadoDescripcionBusqueda = Properties.Resources.AyudaAlmacen_Grid_Descripcion,
                              ConceptoBusqueda = Properties.Resources.LeyehdaAyudaBusquedaAlmacen,
                              DataContext = Contexto.Almacen,
                              CampoClave = "AlmacenID",
                              EsBindeable = true,
                              MaximoCaracteres = 10,
                              CampoDescripcion = "Descripcion",
                              ControlIndex = 1,
                          };
            lblAyuda.Content = Properties.Resources.LeyehdaAyudaBusquedaAlmacen;
        }

        /// <summary>
        /// Agrega los datos necesarios para la 
        /// ejecucion de ayuda de salida por venta
        /// </summary>
        private void GenerarAyudaSalidaVenta()
        {
            Contexto.VentaGanado = new VentaGanadoInfo
                                       {
                                           Activo = false,
                                           Lote = new LoteInfo
                                                      {
                                                          OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                                                      },
                                           UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                                       };
            skAyuda = new ControlAyuda
            {
                ObjetoNegocio = new VentaGanadoPL(),
                MetodoInvocacionBusqueda = "ObtenerVentaGanadoPorPagina",
                MetodoInvocacion = "ObtenerPorFolioTicket",
                MensajeAgregarBusqueda = Properties.Resources.VentaGanado_Seleccionar,
                MensajeCerrarBusqueda = Properties.Resources.VentaGanado_SalirSinSeleccionar,
                MensajeClaveInexistenteBusqueda = Properties.Resources.VentaGanado_Inexistente,
                TituloBusqueda = Properties.Resources.BusquedaVentaGanado_Titulo,
                EncabezadoClaveBusqueda = Properties.Resources.AyudaVentaGanado_Grid_Clave,
                EncabezadoDescripcionBusqueda = Properties.Resources.AyudaVentaGanado_Grid_Descripcion,
                ConceptoBusqueda = Properties.Resources.LeyehdaAyudaBusquedaVentaGanado,
                DataContext = Contexto.VentaGanado,
                CampoClave = "FolioTicket",
                EsBindeable = true,
                CampoDescripcion = "NombreCliente",
                CampoLlaveOcultaClave = "VentaGanadoID",
                MaximoCaracteres = 10,
                ControlIndex = 1,
            };
            skAyuda.AyudaConDatos += (sender, args) =>
                                         {
                                             Contexto.VentaGanado.Lote = new LoteInfo
                                                                             {
                                                                                 OrganizacionID =
                                                                                     AuxConfiguracion.
                                                                                     ObtenerOrganizacionUsuario(),
                                                                             };
                                             Contexto.VentaGanado.Activo = false;
                                         };
            lblAyuda.Content = Properties.Resources.LeyehdaAyudaBusquedaFolioTicket;
        }

        /// <summary>
        /// Agrega los datos necesarios para la 
        /// ejecucion de ayuda de salida por muerte
        /// </summary>
        private void GenerarAyudaSalidaMuerte()
        {
            Contexto.Animal = new AnimalInfo
                                  {
                                      OrganizacionIDEntrada = AuxConfiguracion.ObtenerOrganizacionUsuario(),
                                      UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                                  };
            skAyuda = new ControlAyuda
            {
                ObjetoNegocio = new AnimalPL(),
                MetodoInvocacionBusqueda = "ObtenerAnimalesMuertosPorPagina",
                MetodoInvocacion = "ObtenerAnimalesMuertosPorAnimal",
                MensajeAgregarBusqueda = Properties.Resources.Animal_Seleccionar,
                MensajeCerrarBusqueda = Properties.Resources.Animal_SalirSinSeleccionar,
                MensajeClaveInexistenteBusqueda = Properties.Resources.Animal_Inexistente,
                TituloBusqueda = Properties.Resources.BusquedaAnimal_Titulo,
                EncabezadoClaveBusqueda = Properties.Resources.AyudaAnimal_Grid_Clave,
                EncabezadoDescripcionBusqueda = Properties.Resources.AyudaAnimal_Grid_Descripcion,
                ConceptoBusqueda = Properties.Resources.LeyehdaAyudaBusquedaAnimal,
                DataContext = Contexto.Animal,
                EsAyudaSimple = true,
                EsBindeable = true,
                CampoClave = "Arete",
                CampoDescripcion = "AreteMetalico",
                CampoLlaveOcultaClave = "AnimalID",
                MaximoCaracteres = 16,
                AnchoDescripcion = 100,
                ControlIndex = 1,
            };
            lblAyuda.Content = Properties.Resources.LeyehdaAyudaBusquedaAnimal;
            skAyuda.AyudaConDatos += (sender, args) =>
            {
                skAyuda.Descripcion = skAyuda.Clave;
                Contexto.Animal.OrganizacionIDEntrada = AuxConfiguracion.ObtenerOrganizacionUsuario();
                Contexto.Animal.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            };
        }

        /// <summary>
        /// Agrega ayuda de entrada por compra
        /// </summary>
        private void GenerarAyudaEntradaCompra()
        {
            Contexto.ContenedorEntradaMateriaPrima = new ContenedorEntradaMateriaPrimaInfo
                                                         {
                                                             Contrato = new ContratoInfo
                                                                            {
                                                                                Proveedor =
                                                                                    new ProveedorInfo(),
                                                                                Organizacion =
                                                                                    new OrganizacionInfo
                                                                                        {
                                                                                            OrganizacionID =
                                                                                                AuxConfiguracion.
                                                                                                ObtenerOrganizacionUsuario
                                                                                                ()
                                                                                        }
                                                                            },
                                                             EntradaProducto = new EntradaProductoInfo
                                                                                   {
                                                                                       Organizacion =
                                                                                           new OrganizacionInfo
                                                                                               {
                                                                                                   OrganizacionID
                                                                                                       =
                                                                                                       AuxConfiguracion.
                                                                                                       ObtenerOrganizacionUsuario
                                                                                                       ()
                                                                                               }
                                                                                   },
                                                             Producto = new ProductoInfo(),
                                                         };
            skAyuda = new ControlAyuda
            {
                ObjetoNegocio = new ContratoPL(),
                MetodoInvocacionBusqueda = "ObtenerPorContenedorPaginado",
                MetodoInvocacion = "ObtenerPorContenedor",
                MensajeAgregarBusqueda = Properties.Resources.Contrato_Seleccionar,
                MensajeCerrarBusqueda = Properties.Resources.Contrato_SalirSinSeleccionar,
                MensajeClaveInexistenteBusqueda = Properties.Resources.Contrato_Inexistente,
                TituloBusqueda = Properties.Resources.BusquedaContrato_Titulo,
                EncabezadoClaveBusqueda = Properties.Resources.AyudaContrato_Grid_Clave,
                EncabezadoDescripcionBusqueda = Properties.Resources.AyudaContrato_Grid_Descripcion,
                ConceptoBusqueda = Properties.Resources.LeyehdaAyudaBusquedaContrato,
                DataContext = Contexto.ContenedorEntradaMateriaPrima.Contrato,
                EsBindeable = true,
                CampoClave = "Folio",
                CampoDescripcion = "PesoNegociar",
                CampoLlaveOcultaClave = "ContratoId",
                MaximoCaracteres = 16,
                AnchoDescripcion = 180,
                ControlIndex = 1,
            };
            skAyuda.AyudaConDatos += (sender, args) =>
                                         {
                                             var contrato = skAyuda.Contexto as ContratoInfo;
                                             Contexto.ContenedorEntradaMateriaPrima.EntradaProducto.Producto =
                                                 contrato.Producto;
                                         };
            lblAyuda.Content = Properties.Resources.LeyehdaAyudaBusquedaContrato;
        }

        /// <summary>
        /// Generar ayuda para la reimpresion de
        /// la poliza de pase a proceso
        /// </summary>
        private void GenerarAyudaPaseProceso()
        {
            Contexto.Pedido = new PedidoInfo
                                  {
                                      Organizacion =
                                          new OrganizacionInfo { OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario() },
                                      ListaEstatusPedido = new List<EstatusInfo>
                                                               {
                                                                   new EstatusInfo
                                                                       {
                                                                           EstatusId =
                                                                               (int) Estatus.PedidoProgramado
                                                                       },
                                                                   new EstatusInfo
                                                                       {
                                                                           EstatusId =
                                                                               (int) Estatus.PedidoParcial
                                                                       },
                                                               },
                                      Activo = EstatusEnum.Activo
                                  };
            skAyuda = new ControlAyuda
            {
                ObjetoNegocio = new PedidosPL(),
                MetodoInvocacionBusqueda = "ObtenerPedidosCompletoPaginado",
                MetodoInvocacion = "ObtenerPedidosCompleto",
                MensajeAgregarBusqueda = Properties.Resources.ProgramacionMateriaPrima_Seleccionar,
                MensajeCerrarBusqueda = Properties.Resources.BasculaDeMateriaPrima_AyudaPedidoSalirSinSeleccionar,
                MensajeClaveInexistenteBusqueda = Properties.Resources.BasculaDeMateriaPrima_AyudaPedidoInvalidado,
                TituloBusqueda = Properties.Resources.BasculaDeMateriaPrima_AyudaPedidosTitulo,
                EncabezadoClaveBusqueda = Properties.Resources.AyudaPedido_Grid_Clave,
                EncabezadoDescripcionBusqueda = Properties.Resources.AyudaPedido_Grid_Descripcion,
                ConceptoBusqueda = Properties.Resources.ProgramacionMateriaPrima_lblFolio,
                DataContext = Contexto.Pedido,
                EsBindeable = true,
                CampoClave = "FolioPedido",
                CampoDescripcion = "Observaciones",
                CampoLlaveOcultaClave = "PedidoID",
                MaximoCaracteres = 9,
                AnchoDescripcion = 0,
                AnchoClave = 80,
                ControlIndex = 1,
            };
            lblAyuda.Content = Properties.Resources.ProgramacionMateriaPrima_lblFolio;
            skAyuda.AyudaConDatos += (sender, args) =>
                                         {
                                             Contexto.Pedido.Organizacion = new OrganizacionInfo
                                                                                {
                                                                                    OrganizacionID =
                                                                                        AuxConfiguracion.
                                                                                        ObtenerOrganizacionUsuario()
                                                                                };
                                             Contexto.Pedido.ListaEstatusPedido = new List<EstatusInfo>
                                                                                      {
                                                                                          new EstatusInfo
                                                                                              {
                                                                                                  EstatusId =
                                                                                                      (int)
                                                                                                      Estatus.
                                                                                                          PedidoProgramado
                                                                                              },
                                                                                          new EstatusInfo
                                                                                              {
                                                                                                  EstatusId =
                                                                                                      (int)
                                                                                                      Estatus.
                                                                                                          PedidoParcial
                                                                                              },
                                                                                      };
                                             Contexto.Pedido.Activo = EstatusEnum.Activo;
                                         };
        }

        /// <summary>
        /// Agrega los datos necesarios para la 
        /// ejecucion de ayuda de entrada de ganado
        /// </summary>
        private void GenerarAyudaFolioSolicitudProducto()
        {
            skAyuda = new ControlAyuda
            {
                ObjetoNegocio = new SolicitudProductoBL(),
                MetodoInvocacionBusqueda = "ObtenerPorPagina",
                MetodoInvocacion = "ObtenerPorFolioSolicitud",
                MensajeAgregarBusqueda = Properties.Resources.AyudaSolicitudProductosAlmacen_Seleccionar,
                MensajeCerrarBusqueda = Properties.Resources.AyudaSolicitudProductosAlmacen_SalirSinSeleccionar,
                MensajeClaveInexistenteBusqueda = Properties.Resources.AyudaSolicitudProductosAlmacen_CodigoInvalido,
                TituloBusqueda = Properties.Resources.AyudaSolicitudProductosAlmacen_Busqueda_Titulo,
                EncabezadoClaveBusqueda = Properties.Resources.AyudaSolicitudProductosAlmacen_Grid_Clave,
                EncabezadoDescripcionBusqueda = Properties.Resources.AyudaSolicitudProductosAlmacen_Grid_Descripcion,
                ConceptoBusqueda = Properties.Resources.AyudaSolicitudProductosAlmacen_LeyendaBusqueda,
                DataContext = Contexto.SolicitudProducto,
                EsAyudaSimple = false,
                EsBindeable = true,
                AceptaSoloNumeros = true,
                CampoClave = "FolioSolicitud",
                CampoDescripcion = "Autoriza",
                CampoLlaveOcultaClave = "FolioID",
                AnchoDescripcion = 0,
                AnchoClave = 100,
                MaximoCaracteres = 10,
                ControlIndex = 1,
            };
            lblAyuda.Content = Properties.Resources.LeyehdaAyudaBusquedaFolio;
            skAyuda.AyudaConDatos += (sender, args) =>
            {
                skAyuda.Descripcion = skAyuda.Clave;
                Contexto.EntradaGanado.OrganizacionID =
                    AuxConfiguracion.ObtenerOrganizacionUsuario();
            };
        }

        /// <summary>
        /// Agrega los datos necesarios para la 
        /// ejecucion de ayuda de entrada de ganado
        /// </summary>
        private void GenerarAyudaProduccionFormula()
        {
            skAyuda = new ControlAyuda
            {
                ObjetoNegocio = new ProduccionFormulaPL(),
                MetodoInvocacionBusqueda = "ObtenerPorPagina",
                MetodoInvocacion = "ObtenerPorFolioMovimiento",
                MensajeAgregarBusqueda = Properties.Resources.AyudaProduccionFormula_Seleccionar,
                MensajeCerrarBusqueda = Properties.Resources.AyudaProduccionFormula_SalirSinSeleccionar,
                MensajeClaveInexistenteBusqueda = Properties.Resources.AyudaProduccionFormula_CodigoInvalido,
                TituloBusqueda = Properties.Resources.AyudaProduccionFormula_Busqueda_Titulo,
                EncabezadoClaveBusqueda = Properties.Resources.AyudaProduccionFormula_Grid_Clave,
                EncabezadoDescripcionBusqueda = Properties.Resources.AyudaProduccionFormula_Grid_Descripcion,
                ConceptoBusqueda = Properties.Resources.AyudaProduccionFormula_LeyendaBusqueda,
                DataContext = Contexto.ProduccionFormula,
                EsAyudaSimple = false,
                EsBindeable = true,
                AceptaSoloNumeros = true,
                CampoClave = "FolioMovimiento",
                CampoDescripcion = "DescripcionFormula",
                CampoLlaveOcultaClave = "ProduccionFormulaID",
                MaximoCaracteres = 10,
                ControlIndex = 1,
            };
            lblAyuda.Content = Properties.Resources.LeyehdaAyudaBusquedaFolio;
        }

        /// <summary>
        /// Generar ayuda para salida venta producto
        /// </summary>
        private void GenerarAyudaSalidaVentaProducto()
        {
            Contexto.SalidaProducto = new SalidaProductoInfo
                                          {
                                              Organizacion = new OrganizacionInfo
                                                                 {
                                                                     OrganizacionID =
                                                                         AuxConfiguracion.ObtenerOrganizacionUsuario()
                                                                 }
                                          };
            skAyuda = new ControlAyuda
                          {
                              ObjetoNegocio = new SalidaProductoPL(),
                              MetodoInvocacionBusqueda = "ObtenerFolioPorPaginaReimpresion",
                              MetodoInvocacion = "ObtenerFolioPorReimpresion",
                              MensajeAgregarBusqueda = Properties.Resources.SalidaVentaTraspaso_Seleccionar,
                              MensajeCerrarBusqueda = Properties.Resources.SalidaVentaTraspaso_SalirSinSeleccionar,
                              MensajeClaveInexistenteBusqueda =
                                  Properties.Resources.SalidaVentaTraspaso_AyudaFolioInvalido,
                              TituloBusqueda = Properties.Resources.SalidaVentaTraspaso_Busqueda_Titulo,
                              EncabezadoClaveBusqueda = Properties.Resources.AyudaSalidaEncabezado_Clave,
                              EncabezadoDescripcionBusqueda = Properties.Resources.AyudaSalidaEncabezado_Descripcion,
                              ConceptoBusqueda = Properties.Resources.SalidaVentaTraspaso_LeyendaBusqueda,
                              DataContext = Contexto.SalidaProducto,
                              EsAyudaSimple = false,
                              EsBindeable = true,
                              AceptaSoloNumeros = true,
                              CampoClave = "FolioSalida",
                              CampoDescripcion = "Descripcion",
                              CampoLlaveOcultaClave = "SalidaProductoId",
                              MaximoCaracteres = 10,
                              ControlIndex = 1,
                          };
            lblAyuda.Content = Properties.Resources.SalidaVentaTraspaso_LeyendaBusqueda;
        }

        /// <summary>
        /// Inicializa las propiedades del control de
        /// ayuda
        /// </summary>
        private void LimpiarAyuda()
        {
            skAyuda.LimpiarCampos();
        }

        #endregion METODOS

        #region EVENTOS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObtenerTiposPoliza();
            cmbTipoPoliza.SelectedIndex = 0;
            skAyuda.IsEnabled = false;
        }

        /// <summary>
        /// Genera ayuda de entrada por ajuste
        /// </summary>
        private void GenerarAyudaSalidaPorAjuste()
        {
            //throw new NotImplementedException();
        }

        private void cmbTipoPolizaSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            LimpiarAyuda();
            skAyuda.IsEnabled = cmbTipoPoliza.SelectedIndex > 0;
            var tipoPoliza = cmbTipoPoliza.SelectedItem as TipoPolizaInfo;
            if (tipoPoliza != null)
            {
                var tipo = (TipoPoliza)tipoPoliza.TipoPolizaID;
                switch (tipo)
                {
                    case TipoPoliza.EntradaGanado:
                        GenerarAyudaEntradaGanado();
                        dtpFecha.IsEnabled = false;
                        break;
                    case TipoPoliza.ConsumoProducto:
                        GenerarAyudaConsumoProducto();
                        break;
                    case TipoPoliza.SalidaVenta:
                        GenerarAyudaSalidaVenta();
                        dtpFecha.IsEnabled = false;
                        break;
                    case TipoPoliza.SalidaMuerte:
                        GenerarAyudaSalidaMuerte();
                        dtpFecha.IsEnabled = false;
                        break;
                    case TipoPoliza.EntradaCompra:
                        GenerarAyudaEntradaCompra();
                        dtpFecha.IsEnabled = false;
                        break;
                    case TipoPoliza.PaseProceso:
                        GenerarAyudaPaseProceso();
                        break;
                    case TipoPoliza.SalidaTraspaso:
                    case TipoPoliza.SalidaConsumo:
                        GenerarAyudaFolioSolicitudProducto();
                        break;
                    case TipoPoliza.ProduccionAlimento:
                        GenerarAyudaProduccionFormula();
                        dtpFecha.IsEnabled = false;
                        break;
                    case TipoPoliza.SalidaVentaProducto:
                        GenerarAyudaSalidaVentaProducto();
                        dtpFecha.IsEnabled = false;
                        break;
                    case TipoPoliza.SalidaAjuste:
                        GenerarAyudaSalidaPorAjuste();
                        break;
                }
                stpAyuda.Children.Clear();
                stpAyuda.Children.Add(skAyuda);
            }
        }

        private void btnBuscarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Contexto.TipoPoliza = (TipoPolizaInfo)cmbTipoPoliza.SelectedItem;
                Contexto.Fecha = dtpFecha.SelectedDate ?? DateTime.MinValue;
                var procesoBuscarPoliza = new Thread(delegate(object ctx)
                {
                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        gbPrincipal.IsEnabled = false;
                        imgloading.Visibility = Visibility.Visible;
                    }, DispatcherPriority.Background, null);
                    try
                    {
                        var context = ctx as ReimpresionPolizaModel;
                        if (context != null)
                        {
                            Buscar(context);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        Dispatcher.BeginInvoke((Action)delegate
                        {
                            SkMessageBox.Show(
                                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.ReimpresionPoliza_ErrorReimprimirPoliza,
                                    MessageBoxButton.OK, MessageImage.Error);
                            gbPrincipal.IsEnabled = true;
                            imgloading.Visibility = Visibility.Hidden;
                        }, DispatcherPriority.Background, null);
                    }
                });
                procesoBuscarPoliza.IsBackground = true;
                procesoBuscarPoliza.Start(Contexto);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReimpresionPoliza_ErrorReimprimirPoliza, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void Buscar(ReimpresionPolizaModel contexto)
        {
            var reimpresionPolizaBL = new ReimpresionPolizaBL();
            var tipoPolizaSeleccionada = contexto.TipoPoliza;
            var tipoPoliza = TipoPoliza.EntradaGanado;
            if (tipoPolizaSeleccionada != null)
            {
                tipoPoliza = (TipoPoliza)tipoPolizaSeleccionada.TipoPolizaID;
            }
            object pdf;
            switch (tipoPoliza)
            {
                case TipoPoliza.PaseProceso:
                    if (contexto.Pedido.FolioPedido == 0)
                    {
                        if (contexto.Fecha != DateTime.MinValue)
                            contexto.Pedido.FechaPedido = contexto.Fecha;
                    }
                    pdf = reimpresionPolizaBL.ReimprimirMultiplePoliza(tipoPoliza, contexto,
                                                                       tipoPolizaSeleccionada);
                    break;
                default:
                    pdf = reimpresionPolizaBL.ReimprimirPoliza(tipoPoliza, contexto,
                                                                    tipoPolizaSeleccionada);
                    break;
            }
            if (pdf == null)
            {
                Dispatcher.BeginInvoke((Action)delegate
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                   Properties.Resources.ReimpresionPoliza_SinDatos, MessageBoxButton.OK,
                                   MessageImage.Warning);
                    Limpiar();
                }, DispatcherPriority.Background, null);

            }
            else
            {
                Dispatcher.BeginInvoke((Action)delegate
                                                {
                                                    if (pdf is MemoryStream)
                                                    {
                                                        ImprimePoliza(pdf as MemoryStream, tipoPoliza);
                                                    }
                                                    else
                                                    {
                                                        ImprimePoliza(pdf as IList<ResultadoPolizaModel>, tipoPoliza);
                                                    }
                                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.ReimpresionPoliza_ImpresionExitosa, MessageBoxButton.OK,
                                                    MessageImage.Correct);
                                                    Limpiar();
                                                }, DispatcherPriority.Background, null);
            }
        }



        private void ImprimePoliza(MemoryStream pdf, TipoPoliza tipoPoliza)
        {
            var exportarPoliza = new ExportarPoliza();
            exportarPoliza.ImprimirPoliza(pdf, string.Format("{0} {1}", "Poliza", tipoPoliza));
        }

        private void ImprimePoliza(IList<ResultadoPolizaModel> pdf, TipoPoliza tipoPoliza)
        {
            if (tipoPoliza == TipoPoliza.PaseProceso)
            {
                ExportarPolizaPaseProceso(pdf);
            }
            else
            {
                var exportarPoliza = new ExportarPoliza();
                for (var indexPolizas = 0; indexPolizas < pdf.Count; indexPolizas++)
                {
                    exportarPoliza.ImprimirPoliza(pdf[indexPolizas].PDF, string.Format("{0} {1}", "Poliza", tipoPoliza));
                }
            }
        }

        private void ExportarPolizaPaseProceso(IList<ResultadoPolizaModel> pdf)
        {
            try
            {
                var file = new SaveFileDialog { FileName = string.Format("Polizas {0}", dtpFecha.SelectedDate.Value.ToString("dd-MM-yyyy")), Filter = @"Archivos Zip|*.zip", Title = @"Guardar Archivo Zip" };
                var result = file.ShowDialog();

                if (result == DialogResult.OK)
                {
                    using (var zip = new ZipFile())
                    {
                        foreach (var resultadoPolizaModel in pdf)
                        {
                            zip.AddEntry(string.Format("PP {0}.pdf", resultadoPolizaModel.NomenclaturaArchivo),
                                         resultadoPolizaModel.PDF.ToArray());
                        }
                        zip.Save(file.FileName);
                    }
                }

            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReimpresionPoliza_ErrorReimprimirPoliza, MessageBoxButton.OK,
                                  MessageImage.Error);
            }

        }

        private void btnLimpiarClick(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            var tiposPoliza = new List<TipoPolizaInfo>(Contexto.TiposPoliza);
            InicializaContexto();
            LimpiarAyuda();
            Contexto.TiposPoliza = tiposPoliza;
            cmbTipoPoliza.SelectedIndex = 0;
            gbPrincipal.IsEnabled = true;
            imgloading.Visibility = Visibility.Hidden;
        }

        #endregion EVENTOS
    }
}
