using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para RegistrarProgramaciondeFletes.xaml
    /// </summary>
    public partial class RegistrarProgramaciondeFletes
    {
        #region Atributos
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

        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        private SKAyuda<ProveedorInfo> skAyudaProveedor;
        private SKAyuda<ContratoInfo> skAyudaContrato; 
        private List<ProgramaciondeFletesInfo> datosProgramaciondeFletes;
        private List<ProgramaciondeFletesInfo> listaFletesBorrados; 
        private ContratoInfo contratoSeleccionado;
        private List<FleteDetalleInfo> regresoCostosFletes; 
        private int fleteID;
        private string mermaMinimaDefault;
        private FleteMermaPermitidaInfo fleteMermaPermitidaInfo;
        private TipoTarifaInfo tipoTarifaAnterior;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor registrar programacion de fletes
        /// </summary>
        public RegistrarProgramaciondeFletes()
        {
            listaFletesBorrados = new List<ProgramaciondeFletesInfo>();
            contratoSeleccionado=new ContratoInfo();
            datosProgramaciondeFletes=new List<ProgramaciondeFletesInfo>();
            InitializeComponent();
            CargarAyudas();
            CargarComboTipoTarifa();
            btnCostos.IsEnabled=false;
            btnGuardar.Content = Properties.Resources.RegistrarProgramacionFlete_btnGuardar;
            btnCancelarFlete.IsEnabled = false;
            ucTitulo.TextoTitulo = Properties.Resources.RegistrarProgramacionFlete_Titulo;
            skAyudaContrato.IsEnabled = false;
        }

        private void CargarComboTipoTarifa()
        {
            var tipoTarifaPl = new TipoTarifaPL();
            var listaTipoTarifa = new List<TipoTarifaInfo>();
            listaTipoTarifa.AddRange(tipoTarifaPl.ObtenerTodos());

            cmbTipoTarifa.ItemsSource = listaTipoTarifa;
            cmbTipoTarifa.DisplayMemberPath = "Descripcion";
            cmbTipoTarifa.SelectedValuePath = "TipoTarifaId";
            cmbTipoTarifa.SelectedValue = 0;
        }

        /// <summary>
        /// Constructor registrar programacion de fletes con parametro
        /// </summary>
        /// <param name="contratoSeleccionado"></param>
        public RegistrarProgramaciondeFletes(ContratoInfo contratoSeleccionado)
        {
            this.contratoSeleccionado = contratoSeleccionado;
            datosProgramaciondeFletes = new List<ProgramaciondeFletesInfo>();
            contratoSeleccionado.UsuarioCreacionId = AuxConfiguracion.ObtenerUsuarioLogueado();
            InitializeComponent();
            CargarParametroMerma();
            CargarComboTipoTarifa();
            CargarAyudas();
            Contexto = contratoSeleccionado;
            skAyudaOrganizacion.IsEnabled = false;
            CargarGridProgramacionFletes(contratoSeleccionado);
            fleteID = 0;
            skAyudaContrato.Clave = Convert.ToString(contratoSeleccionado.Folio);
            skAyudaContrato.IsEnabled = false;
            btnCostos.IsEnabled = false;
            ucTitulo.TextoTitulo = Properties.Resources.RegistrarProgramacionFlete_TituloActualizar;
            listaFletesBorrados = new List<ProgramaciondeFletesInfo>();
        }

        #endregion

        #region Eventos
        /// <summary>
        /// Click boton agregar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultadoValidacion = ValidarCamposBlanco();
                if (resultadoValidacion.Resultado)
                {
                    if (ValidarMermaPermitida())
                    {
                        if (txtMermaPermitida.Value != null)
                        {
                            var programacionFletesInfo = new ProgramaciondeFletesInfo()
                            {
                                Flete = new FleteInfo()
                                {

                                    Proveedor = new ProveedorInfo()
                                    {
                                        CodigoSAP = skAyudaProveedor.Info.CodigoSAP,
                                        Descripcion = skAyudaProveedor.Descripcion,
                                        ProveedorID = skAyudaProveedor.Info.ProveedorID,
                                    },
                                    TipoTarifa = (TipoTarifaInfo)cmbTipoTarifa.SelectedItem,
                                    MermaPermitida = (decimal)txtMermaPermitida.Value,
                                    Observaciones = txtObservaciones.Text,
                                    LisaFleteDetalleInfo = regresoCostosFletes
                                }
                            };

                            if (!ExisteProveedor(programacionFletesInfo) ||
                                (string) btnAgregar.Content == Properties.Resources.RegistrarProgramacionFlete_btnActualizar)
                            {
                                if ((string) btnAgregar.Content ==
                                    Properties.Resources.RegistrarProgramacionFlete_btnAgregar)
                                {
                                    if (regresoCostosFletes == null || regresoCostosFletes.Count <= 0)
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.RegistrarProgramaciondeFlete_InsertarCostos,
                                            MessageBoxButton.OK, MessageImage.Warning);
                                    }
                                    else
                                    {
                                        ModificacionGridFletes(programacionFletesInfo);
                                    }
                                }
                                else
                                {
                                    ModificacionGridFletes(programacionFletesInfo);
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.RegistrarProgramaciondeFlete_ProvedorExistente,
                                    MessageBoxButton.OK, MessageImage.Stop);
                            }
                        }
                    }
                }
                else
                {
                    var mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                        ? Properties.Resources.RegistrarProgramacionFlete_Error
                        : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }

                e.Handled = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }


        #endregion
        #region Metodos
        /// <summary>
        /// Existe proveedor en grid
        /// </summary>
        /// <param name="programacionFletesInfo"></param>
        /// <returns></returns>
        private bool ExisteProveedor(ProgramaciondeFletesInfo programacionFletesInfo)
        {
            if(datosProgramaciondeFletes != null)
            return datosProgramaciondeFletes.Any(datosProgramaciondeFlete 
                => datosProgramaciondeFlete.Flete.Proveedor.ProveedorID 
                == programacionFletesInfo.Flete.Proveedor.ProveedorID
                );

            return false;
        }

        /// <summary>
        /// Inicializar contexto
        /// </summary>
        private void InicializaContexto()
        {
            var organizacionInfo = new OrganizacionInfo()
            {
                OrganizacionID = Contexto.Organizacion.OrganizacionID,
            };
            Contexto = new ContratoInfo()
            {
                Organizacion = organizacionInfo
            };

        }
        
        /// <summary>
        /// Cargar grid programacion fletes
        /// </summary>
        /// <param name="contratoInfo"></param>
        private void CargarGridProgramacionFletes(ContratoInfo contratoInfo)
        {
            try
            {
                var programacionFletesPl = new ProgramaciondeFletesPL();
                datosProgramaciondeFletes = programacionFletesPl.ObtenerFletes(contratoInfo);
                gridDatosFletes.ItemsSource = datosProgramaciondeFletes;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Limpiar datos
        /// </summary>
        /// <param name="parcial"></param>
        private void LimpiarDatos(bool parcial)
        {
            if (parcial)
            {
                skAyudaOrganizacion.LimpiarCampos();
            }
            AgregarAyudaProveedor();
            skAyudaProveedor.LimpiarCampos();
            tipoTarifaAnterior = null;
            txtMermaPermitida.Value = Convert.ToDecimal(mermaMinimaDefault);
            txtObservaciones.Clear();
            regresoCostosFletes= new List<FleteDetalleInfo>();
            cmbTipoTarifa.SelectedIndex = 0;
            cmbTipoTarifa.IsEnabled = true;
            btnCostos.IsEnabled = false;
        }
        /// <summary>
        /// Validar campos en blanco
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCamposBlanco()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(skAyudaProveedor.Clave.Trim()) && String.IsNullOrEmpty(skAyudaProveedor.Descripcion.Trim()))
            {
                skAyudaProveedor.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFlete_ProveedorRequerido;
                resultado.Resultado = false;
                return resultado;
            }
            if (String.IsNullOrEmpty(txtMermaPermitida.Text.Trim()))
            {
                txtMermaPermitida.Focus();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFlete_MermaPermitidaRequerido;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(txtObservaciones.Text.Trim()) && txtObservaciones.IsEnabled)
            {
                txtObservaciones.Focus();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFlete_ObservacionesRequerido;
                resultado.Resultado = false;
                return resultado;
            }
            if (skAyudaContrato.Clave == "")
            {
                skAyudaContrato.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFlete_ContratoRequerido;
                resultado.Resultado = false;
                return resultado;
            }

            var tipoTarifa = (TipoTarifaInfo) cmbTipoTarifa.SelectedItem;
            if (tipoTarifa.TipoTarifaId <= 0)
            {
                cmbTipoTarifa.Focus();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFlete_TipoTarifaRequerido;
                resultado.Resultado = false;
                return resultado;
            }
            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Modificacion del grid fletes (agregarActualizar)
        /// </summary>
        private void ModificacionGridFletes(ProgramaciondeFletesInfo programacionFletesInfo)
        {


            if ((string)btnAgregar.Content == Properties.Resources.RegistrarProgramacionFlete_btnActualizar && (string)btnGuardar.Content != Properties.Resources.RegistrarProgramacionFlete_btnGuardar)
            {
                foreach (var datosProgramaciondeFlete in datosProgramaciondeFletes.Where(datosProgramaciondeFlete => datosProgramaciondeFlete.Flete.Proveedor.ProveedorID ==
                                                                                                                     programacionFletesInfo.Flete.Proveedor.ProveedorID))
                {
                    datosProgramaciondeFlete.Flete.MermaPermitida = programacionFletesInfo.Flete.MermaPermitida;
                    datosProgramaciondeFlete.Flete.Observaciones = programacionFletesInfo.Flete.Observaciones;
                    datosProgramaciondeFlete.Flete.TipoTarifa = programacionFletesInfo.Flete.TipoTarifa;
                    if (ExisteProveedor(datosProgramaciondeFlete))
                    {
                        datosProgramaciondeFlete.Flete.Opcion = 2;
                    }
                    else
                    {
                        datosProgramaciondeFlete.Flete.Opcion = 1;
                    }
                    break;
                }
                gridDatosFletes.ItemsSource = null;
                if (regresoCostosFletes != null && regresoCostosFletes.Count>0)
                {
                    AgregarCostosLista(programacionFletesInfo);
                }
                gridDatosFletes.ItemsSource = datosProgramaciondeFletes;
                skAyudaProveedor.IsEnabled = true;
                btnAgregar.Content = Properties.Resources.RegistrarProgramacionFlete_btnAgregar;
                LimpiarDatos(false);
            }
            else
            {
                if (!ExisteProveedor(programacionFletesInfo) || (string)btnGuardar.Content == Properties.Resources.RegistrarProgramacionFlete_btnGuardar)
                {
                    gridDatosFletes.ItemsSource = null;
                    programacionFletesInfo.Flete.Opcion = 1;
                    if (datosProgramaciondeFletes != null)
                    datosProgramaciondeFletes =
                        datosProgramaciondeFletes.Where(
                            datosProgramaciondeFlete => datosProgramaciondeFlete.Flete.Proveedor.ProveedorID !=
                                                        programacionFletesInfo.Flete.Proveedor.ProveedorID).ToList();
                    if (datosProgramaciondeFletes != null)
                    {
                        datosProgramaciondeFletes.Add(programacionFletesInfo);
                        AgregarCostosLista(programacionFletesInfo);
                        gridDatosFletes.ItemsSource = datosProgramaciondeFletes;
                    }
                    skAyudaProveedor.IsEnabled = true;
                    btnAgregar.Content = Properties.Resources.RegistrarProgramacionFlete_btnAgregar;
                    LimpiarDatos(false);
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarProgramaciondeFlete_InsertarCostos,
                        MessageBoxButton.OK, MessageImage.Warning);
                    skAyudaProveedor.AsignarFoco();
                }
            }

        }


        /// <summary>
        /// Eliminar proveedor seleccionado
        /// </summary>
        /// <param name="proveedorSeleccionado"></param>
        private void EliminarProveedorSeleccionado(ProgramaciondeFletesInfo proveedorSeleccionado)
        {
            try
            {
                var programaciondeFletePl = new ProgramaciondeFletesPL();
                if (proveedorSeleccionado.Flete.FleteID > 0)
                {
                    listaFletesBorrados.Add(proveedorSeleccionado);
                }
                datosProgramaciondeFletes = programaciondeFletePl.ElimnarProveedorSeleccionado(proveedorSeleccionado, datosProgramaciondeFletes);
                gridDatosFletes.ItemsSource = null;
                gridDatosFletes.ItemsSource = datosProgramaciondeFletes;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion();
            AgregarAyudaContrato();
            AgregarAyudaProveedor();
        }

        /// <summary>
        /// Método para agregar el control ayuda organización origen
        /// </summary>

        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, new OrganizacionInfo() { TipoOrganizacion = new TipoOrganizacionInfo() { TipoOrganizacionID = (int)TipoOrganizacion.Ganadera } }
                                                          , "PropiedadClaveCatalogoAyudaTipoOrnanizacion"
                                                          , "PropiedadDescripcionCatalogoAyudaTipoOrganizacion", true, 50, true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaGanadera_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaGanadera_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaGanadera_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaGanadera_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaGanadera_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaGanadera_Busqueda_Titulo,
            };
            skAyudaOrganizacion.ObtenerDatos += ObtenerDatosOrganizacion;
            skAyudaOrganizacion.LlamadaMetodosNoExistenDatos += LimpiarDatosOrganizacion;
            skAyudaOrganizacion.AsignaTabIndex(0);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }

        private void LimpiarDatosOrganizacion()
        {
            skAyudaProveedor.LimpiarCampos();
            skAyudaContrato.LimpiarCampos();
            skAyudaContrato.IsEnabled = false;
            txtMermaPermitida.Text = "";
            txtObservaciones.Clear();
            regresoCostosFletes = null;
        }

        private void ObtenerDatosOrganizacion(string filtro)
        {
            try
            {
                var organizacionPl = new OrganizacionPL();
                var organizacion = organizacionPl.ObtenerPorID(new OrganizacionInfo() {OrganizacionID = Convert.ToInt32(filtro)});

                if (organizacion != null)
                {
                    if (organizacion.TipoOrganizacion.TipoOrganizacionID == (int) TipoOrganizacion.Ganadera)
                    {
                        skAyudaOrganizacion.Info.TipoOrganizacion.TipoOrganizacionID = (int) TipoOrganizacion.Ganadera;
                        AgregarAyudaContrato();
                    }
                    else
                    {
                        skAyudaOrganizacion.Info.TipoOrganizacion.TipoOrganizacionID = (int)TipoOrganizacion.Ganadera;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.AyudaGanadera_CodigoInvalido, MessageBoxButton.OK,
                            MessageImage.Stop);
                        skAyudaOrganizacion.AsignarFoco();
                        LimpiarDatosOrganizacion();
                    }
                }
                else
                {
                    skAyudaOrganizacion.Info.TipoOrganizacion.TipoOrganizacionID = (int)TipoOrganizacion.Ganadera;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.AyudaGanadera_CodigoInvalido, MessageBoxButton.OK,
                        MessageImage.Stop);
                    skAyudaOrganizacion.AsignarFoco();
                    LimpiarDatosOrganizacion();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void AgregarAyudaContrato()
        {
            int organizacion = 0;
            if (skAyudaOrganizacion.Clave != "")
                organizacion = Convert.ToInt32(skAyudaOrganizacion.Clave);
            skAyudaContrato = new SKAyuda<ContratoInfo>(0, false,
                new ContratoInfo()
                {
                    Organizacion = new OrganizacionInfo() { OrganizacionID = organizacion },
                    Activo = EstatusEnum.Activo,
                    TipoFlete = new TipoFleteInfo() {TipoFleteId = (int) TipoFleteEnum.PagoenGanadera}
                },
                "PropiedadFolioContratoFlete", "PropiedadProductoContratoFlete", false, 50, true)
            {
                MensajeBusquedaCerrar = Properties.Resources.AyudaContrato_btnCancelarSalir,
                MensajeClaveInexistente = Properties.Resources.RegistrarProgramaciondeFlete_contratoNoExiste
            };
            skAyudaContrato.AyudaPL = new ContratoPL();
            skAyudaContrato.ObtenerDatos = ObtenerDatosContrato;
            skAyudaContrato.AsignaTabIndex(1);
            SplAyudaContrato.Children.Clear();
            SplAyudaContrato.Children.Add(skAyudaContrato);
        }

        private void ObtenerDatosContrato(string filtro)
        {
            try
            {
                var contratoPl = new ContratoPL();
                contratoSeleccionado =
                    contratoPl.ObtenerPorFolio(new ContratoInfo()
                    {
                        Organizacion =
                            new OrganizacionInfo() {OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave)},
                        Folio = Convert.ToInt32(skAyudaContrato.Clave)
                    });

                if (contratoSeleccionado != null)
                {
                    if (contratoSeleccionado.TipoFlete.TipoFleteId == (int)TipoFleteEnum.PagoenGanadera)
                    {
                        CargarParametroMerma();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarProgramaciondeFlete_contratoNoExiste, MessageBoxButton.OK,
                            MessageImage.Stop);
                        skAyudaContrato.LimpiarCampos();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarProgramaciondeFlete_contratoNoExiste, MessageBoxButton.OK,
                            MessageImage.Stop);
                    skAyudaContrato.LimpiarCampos();
                }
                skAyudaContrato.Info = new ContratoInfo()
                {
                    Organizacion =
                        new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave) },
                    Activo = EstatusEnum.Activo,
                    TipoFlete = new TipoFleteInfo() { TipoFleteId = (int)TipoFleteEnum.PagoenGanadera }
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Transportista
        /// </summary>
        private void AgregarAyudaProveedor()
        {

            skAyudaProveedor =
                new SKAyuda<ProveedorInfo>(200, false,new ProveedorInfo()
                                                          , "PropiedadClaveCatalogo"
                                                          , "PropiedadDescripcionCatalogoAyudaTipoProveedor","", true,80, 10, true)
                {
                    AyudaPL = new ProveedorPL(),
                    Info = new ProveedorInfo { Activo = EstatusEnum.Activo },
                    MensajeClaveInexistente = Properties.Resources.Proveedor_CodigoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.Proveedor_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                    MensajeAgregar = Properties.Resources.Proveedor_Seleccionar,
                    TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                    TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo,
                };
            skAyudaProveedor.AyudaConDatos += skAyudaProveedor_AyudaConDatos;
            SplAyudaProveedor.Children.Clear();
            skAyudaProveedor.LlamadaMetodosNoExistenDatos += AyudaSinDatos;
            SplAyudaProveedor.Children.Add(skAyudaProveedor);
            skAyudaProveedor.AsignaTabIndex(3);
        }

        private void skAyudaProveedor_AyudaConDatos(object sender, EventArgs e)
        {
            btnCostos.IsEnabled = true;

            if (skAyudaProveedor.Info != null && skAyudaProveedor.Info.TipoProveedor != null &&
               (skAyudaProveedor.Info.TipoProveedor.TipoProveedorID != 2 && skAyudaProveedor.Info.TipoProveedor.TipoProveedorID != 5))
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarProgramacionFlete_MsgErrorTipoProveedor,
                        MessageBoxButton.OK, MessageImage.Stop) == MessageBoxResult.OK)
                {
                    skAyudaProveedor.LimpiarCampos();
                    skAyudaProveedor.AsignarFoco();
                }
            }
        }

        /// <summary>
        /// Reinicia los filtros
        /// </summary>
        private void AyudaSinDatos()
        {
            skAyudaProveedor.Info = new ProveedorInfo {Activo = EstatusEnum.Activo};
        }

        /// <summary>
        /// Click boton Costos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCostos_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var tipoTarifa = (TipoTarifaInfo)cmbTipoTarifa.SelectedItem;
                if (tipoTarifa != null && tipoTarifa.TipoTarifaId > 0)
                {
                    var costosDeFletes = new CostosdeFletes(regresoCostosFletes, tipoTarifa);
                    MostrarCentrado(costosDeFletes);
                    if (costosDeFletes.RegresoFleteDetalleInfos != null)
                    {
                        if (costosDeFletes.RegresoFleteDetalleInfos.Count > 0)
                        {
                            regresoCostosFletes = costosDeFletes.RegresoFleteDetalleInfos;
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarProgramaciondeFlete_CapturarTipoTarifa, MessageBoxButton.OK,
                        MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarProgramaciondeFlete_ErrorRegistrar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Agrega costo a la lista
        /// </summary>
        /// <param name="programaciondeFletesInfo"></param>
        private void AgregarCostosLista(ProgramaciondeFletesInfo programaciondeFletesInfo)
        {
            try
            {
                foreach (var datosProgramaciondeFlete in datosProgramaciondeFletes)
                {
                    if (datosProgramaciondeFlete.Flete.Proveedor.ProveedorID ==
                        programaciondeFletesInfo.Flete.Proveedor.ProveedorID)
                    {
                        if (regresoCostosFletes != null)
                        {
                            datosProgramaciondeFlete.Flete.LisaFleteDetalleInfo = regresoCostosFletes;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// click boton eliminar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var botonEliminar = (Button)e.Source;
            try
            {
                var proveedorSeleccionado = (ProgramaciondeFletesInfo)Extensor.ClonarInfo(botonEliminar.CommandParameter);
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                string.Format("{0} {1} {2}",Properties.Resources.RegistrarProgramaciondeFlete_EliminarProveedor, proveedorSeleccionado.Flete.Proveedor.Descripcion,"?"),
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    if (proveedorSeleccionado != null)
                    {
                        EliminarProveedorSeleccionado(proveedorSeleccionado);
                        LimpiarDatos(false);
                        btnAgregar.Content = Properties.Resources.RegistrarProgramacionFlete_btnAgregar;

                        if (listaFletesBorrados.Count > 0)
                        {
                            var programaciondeFletesPl = new ProgramaciondeFletesPL();
                            if (programaciondeFletesPl.EliminarProveedorFlete(listaFletesBorrados,AuxConfiguracion.ObtenerUsuarioLogueado()))
                            {
                                listaFletesBorrados.Clear();
                                
                            }
                        }
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.RegistrarProgramaciondeFlete_ProveedorEliminado,
                                    MessageBoxButton.OK, MessageImage.Correct);

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarProgramaciondeFlete_ErrorRegistrar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Click boton editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtbEdidar_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var proveedorSelecciondo = (ProgramaciondeFletesInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                btnAgregar.Content = Properties.Resources.RegistrarProgramacionFlete_btnActualizar;
                if (proveedorSelecciondo != null)
                {
                    fleteID = proveedorSelecciondo.Flete.FleteID;
                 
                    skAyudaProveedor.Info = proveedorSelecciondo.Flete.Proveedor;
                    skAyudaProveedor.Clave = proveedorSelecciondo.Flete.Proveedor.CodigoSAP;
                    skAyudaProveedor.Descripcion = proveedorSelecciondo.Flete.Proveedor.Descripcion;
                    skAyudaProveedor.IsEnabled = false;
                    tipoTarifaAnterior = proveedorSelecciondo.Flete.TipoTarifa;
                    cmbTipoTarifa.IsEnabled = false;
                    cmbTipoTarifa.SelectedValue = proveedorSelecciondo.Flete.TipoTarifa.TipoTarifaId;
                    txtMermaPermitida.Value = proveedorSelecciondo.Flete.MermaPermitida;
                    txtObservaciones.Text = proveedorSelecciondo.Flete.Observaciones;
                    btnCostos.IsEnabled = true;

                    try
                    {
                        regresoCostosFletes = proveedorSelecciondo.Flete.LisaFleteDetalleInfo;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarProgramaciondeFlete_ErrorRegistrar, MessageBoxButton.OK, MessageImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarProgramaciondeFlete_ErrorRegistrar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (skAyudaContrato.Clave != string.Empty && skAyudaOrganizacion.Clave != string.Empty)
                {
                    var listaProgramaciondeFletesInfos = new List<ProgramaciondeFletesInfo>();
                    var contratoPL = new ContratoPL();
                    var contrato = new ContratoInfo();
                    if (datosProgramaciondeFletes.Count > 0)
                    {
                        foreach (var datosProgramaciondeFlete in datosProgramaciondeFletes)
                        {
                            contrato.Folio = int.Parse(skAyudaContrato.Clave);
                            contrato.Organizacion = new OrganizacionInfo {OrganizacionID = int.Parse(skAyudaOrganizacion.Clave)};
                            datosProgramaciondeFlete.Flete.ContratoID = contratoPL.ObtenerPorFolio(contrato).ContratoId;
                            datosProgramaciondeFlete.Flete.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                            datosProgramaciondeFlete.Flete.UsuarioModificacionID =
                                AuxConfiguracion.ObtenerUsuarioLogueado();
                            datosProgramaciondeFlete.Organizacion = new OrganizacionInfo()
                            {
                                OrganizacionID = int.Parse(skAyudaOrganizacion.Clave)
                            };
                        }
                        listaProgramaciondeFletesInfos = datosProgramaciondeFletes;

                        if (listaProgramaciondeFletesInfos != null)
                        {
                            var programaciondeFletesPl = new ProgramaciondeFletesPL();
                            if (programaciondeFletesPl.GuardarProgramaciondeFlete(listaProgramaciondeFletesInfos))
                            {

                                
                                var mensaje = btnGuardar.Content.ToString() == Properties.Resources.RegistrarProgramacionFlete_btnGuardar 
                                    ? Properties.Resources.RegistrarProgramaciondeFlete_GuardarExito 
                                    : Properties.Resources.RegistrarProgramaciondeFlete_ActualizarExito;
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    mensaje,
                                    MessageBoxButton.OK, MessageImage.Correct);
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarProgramaciondeFlete_FaltanDatos,
                            MessageBoxButton.OK, MessageImage.Stop);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarProgramaciondeFlete_DatosBlanco,
                        MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        ex.Message,
                        MessageBoxButton.OK, MessageImage.Stop);
            }
        }


        /// <summary>
        /// Lost focus de splayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplAyudaProveedor_OnLostFocus(object sender, RoutedEventArgs e)
        {
        //    if (skAyudaProveedor.Clave != String.Empty && skAyudaProveedor.Descripcion != String.Empty)
        //    {
        //        btnCostos.IsEnabled = true;
        //    }
        //    else
        //    {
        //        btnCostos.IsEnabled = false;
        //        return;
        //    }

        //    if (skAyudaProveedor.Info != null && skAyudaProveedor.Info.TipoProveedor != null &&
        //       (skAyudaProveedor.Info.TipoProveedor.TipoProveedorID != 2 && skAyudaProveedor.Info.TipoProveedor.TipoProveedorID != 5))
        //    {
        //        if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
        //                Properties.Resources.RegistrarProgramacionFlete_MsgErrorTipoProveedor,
        //                MessageBoxButton.OK, MessageImage.Stop) == MessageBoxResult.OK) 
        //        {
        //            skAyudaProveedor.LimpiarCampos();
        //            e.Handled = true;
        //            skAyudaProveedor.AsignarFoco();
        //            return;
        //        }
        //    }
        }

        #endregion
        /// <summary>
        /// key down de merma permitida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtMermaPermitida_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (txtMermaPermitida.Text != String.Empty)
                {
                    if (ValidarDecimal(txtMermaPermitida.Text))
                    {
                        ValidarMermaPermitida();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarProgramaciondeFlete_MermaNoValida,
                            MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFlete_MermaPermitidaRequerido,
                    MessageBoxButton.OK, MessageImage.Warning);
                }
            }
        }

        /// <summary>
        /// Validar decimal
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool ValidarDecimal(string text)
        {
            try
            {
                var dato = text.Split('.');
                if (dato.Count() > 2)
                {
                    return false;
                }
                return true;
            }
            catch (Exception Ex)
            {
                Logger.Error(Ex);
                return false;
            }
        }
        /// <summary>
        /// Validar merma permitida
        /// </summary>
        private bool ValidarMermaPermitida()
        {

            bool resultado = true;
            try
            {
                if (fleteMermaPermitidaInfo != null)
                {
                    if (txtMermaPermitida.Value != null)
                    {
                        var mermaTecleada = (decimal)txtMermaPermitida.Value;
                        decimal mermaMaxima = fleteMermaPermitidaInfo.MermaPermitida;

                        if (mermaTecleada > mermaMaxima)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format("{0} {1} {2}",Properties.Resources.RegistrarProgramaciondeFlete_MenorMermaMayor, mermaMaxima.ToString(CultureInfo.InvariantCulture)
                                    , Properties.Resources.RegistrarProgramaciondeFlete_FavorVerificar),
                                MessageBoxButton.OK, MessageImage.Warning);
                            txtMermaPermitida.Focus();
                            resultado = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            
            return resultado;
        }

        /// <summary>
        /// Cargar parametros 
        /// </summary>
        private void CargarParametroMerma()
        {
            if (contratoSeleccionado != null)
            {
                if (contratoSeleccionado.Producto == null)
                {
                    ContratoPL contratoPl = new ContratoPL();
                    contratoSeleccionado = contratoPl.ObtenerPorFolio(contratoSeleccionado);
                }
                if (contratoSeleccionado.Producto == null)
                {
                    return;
                }
                var producto = new ProductoInfo {ProductoId = (int) contratoSeleccionado.Producto.ProductoId};
                var productoPl = new ProductoPL();
                producto = productoPl.ObtenerPorID(producto);

                if (producto != null)
                {

                    if (producto.SubfamiliaId == SubFamiliasEnum.Granos.GetHashCode() ||
                        producto.SubfamiliaId == SubFamiliasEnum.Harinas.GetHashCode())
                    {
                        var organizacion = AuxConfiguracion.ObtenerOrganizacionUsuario();
                        fleteMermaPermitidaInfo = new FleteMermaPermitidaInfo()
                        {
                            Organizacion = new OrganizacionInfo() {OrganizacionID = organizacion},
                            SubFamilia = new SubFamiliaInfo() {SubFamiliaID = producto.SubfamiliaId},
                            Activo = EstatusEnum.Activo
                        };
                        var fleteMermaPermitidaPl = new FleteMermaPermitidaPL();
                        fleteMermaPermitidaInfo = fleteMermaPermitidaPl.ObtenerConfiguracion(fleteMermaPermitidaInfo);
                        if (fleteMermaPermitidaInfo == null)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources
                                    .RegistrarProgramacionFletesInterna_MensajeMermaPermitidaNoConfigurada
                                , MessageBoxButton.OK,
                                MessageImage.Stop);
                        }
                        else
                        {
                            mermaMinimaDefault = fleteMermaPermitidaInfo.MermaPermitida.ToString();
                            txtMermaPermitida.Value = fleteMermaPermitidaInfo.MermaPermitida;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Validar solo numero y puntos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtMermaPermitida_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumerosConPunto(e.Text);
        }
        /// <summary>
        /// Cancelar el registro de programacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
            Properties.Resources.RegistrarProgramacionFlete_btnCancelarSalir,
            MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                 Close();
            }
        }

        /// <summary>
        /// Evento para cancelar el registro de fletes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelarFlete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFlete_btnCancelarFleteMensaje,
                    MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    CancelarFlete();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private bool CancelarFlete()
        {
            bool regreso = false;
            try
            {
                var listaProgramaciondeFletesInfos = new List<ProgramaciondeFletesInfo>();
                foreach (var datosProgramaciondeFlete in datosProgramaciondeFletes)
                {
                    datosProgramaciondeFlete.Flete.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    datosProgramaciondeFlete.Flete.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                }
                listaProgramaciondeFletesInfos = datosProgramaciondeFletes;

                if (listaProgramaciondeFletesInfos != null)
                {
                    var programaciondeFletesPl = new ProgramaciondeFletesPL();
                    if (programaciondeFletesPl.CancelarProgramacionFlete(listaProgramaciondeFletesInfos))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         Properties.Resources.RegistrarProgramaciondeFlete_CancelarExito,
                         MessageBoxButton.OK, MessageImage.Correct);
                        Close();
                        regreso = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return regreso;
        }

        private void TxtMermaPermitida_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var txt = (DecimalUpDown)sender;
            if (txt.Text != mermaMinimaDefault)
            {
                txtObservaciones.IsEnabled = true;
            }
            else
            {
                txtObservaciones.Text = string.Empty;
                txtObservaciones.IsEnabled = false;
            }
        }

        private void CmbTipoTarifa_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tipoTarifa = (TipoTarifaInfo) cmbTipoTarifa.SelectedItem;
            if (tipoTarifaAnterior != null)
            {
                if (tipoTarifa.TipoTarifaId != tipoTarifaAnterior.TipoTarifaId)
                {
                    if (tipoTarifa.TipoTarifaId == (int) TipoTarifaEnum.Tonelada)
                    {
                        if (regresoCostosFletes != null)
                            foreach (var fleteDetalleInfo in regresoCostosFletes)
                            {
                                fleteDetalleInfo.Tarifa = fleteDetalleInfo.Tarifa/1000;
                            }
                        tipoTarifaAnterior = tipoTarifa;
                    }
                    else if (tipoTarifa.TipoTarifaId == (int) TipoTarifaEnum.Viaje)
                    {
                        if (regresoCostosFletes != null)
                            foreach (var fleteDetalleInfo in regresoCostosFletes)
                            {
                                fleteDetalleInfo.Tarifa = fleteDetalleInfo.Tarifa*1000;
                            }
                        tipoTarifaAnterior = tipoTarifa;
                    }
                }
            }
            else
            {
                tipoTarifaAnterior = tipoTarifa;
            }
        }
    }
}
