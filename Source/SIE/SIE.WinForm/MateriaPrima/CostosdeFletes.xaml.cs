using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
    /// Lógica de interacción para CostosdeFletes.xaml
    /// </summary>
    public partial class CostosdeFletes
    {
        #region Propiedades

        private List<FleteDetalleInfo> fleteDetalleInfos;
        private List<FleteDetalleInfo> eliminadosFleteDetalle;
        private TipoTarifaInfo tipoTarifaLocal;
        private int indiceActual;
        public bool Cancelado { get; set; }
        public List<FleteDetalleInfo> RegresoFleteDetalleInfos
        {
            get { return fleteDetalleInfos; }
            set { fleteDetalleInfos = value; }
        } 
        private CostoOrganizacionInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CostoOrganizacionInfo)DataContext;
            }
            set { DataContext = value; }
        }        
                /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
                {
                    Contexto = new CostoOrganizacionInfo();
                }
        #endregion

        #region Atributos

        private SKAyuda<CostoInfo> skAyudaCosto;
        private List<FleteDetalleInfo> listaCostosFletesDetalleInfo;
    //    private int FleteID;
        #endregion

        #region Costructor

        public CostosdeFletes(List<FleteDetalleInfo> listaDetalleInfos,TipoTarifaInfo tipoTarifa)
        {

            Cancelado = false;
            tipoTarifaLocal = tipoTarifa;
            indiceActual = 0;
            InitializeComponent();
            eliminadosFleteDetalle = new List<FleteDetalleInfo>();
            listaCostosFletesDetalleInfo = new List<FleteDetalleInfo>();
            AgregarAyudaCosto();
            List<FleteDetalleInfo> tmpList = new List<FleteDetalleInfo>();

            lblTipoTarifa.Content = tipoTarifa.Descripcion;

            if (listaDetalleInfos != null)
            {
                if (listaDetalleInfos.Count > 0)
                {
                    cargarGridCostos(listaDetalleInfos);
                }
            }
            
           
        }
        /// <summary>
        /// Llenar el grid de costos
        /// </summary>
        /// <param name="fleteDetalleInfos"></param>
        private void cargarGridCostos(List<FleteDetalleInfo> fleteDetalleInfos)
        {
            try 
            {

                gridDatosCostos.ItemsSource = null;
                listaCostosFletesDetalleInfo = fleteDetalleInfos;
                gridDatosCostos.ItemsSource = listaCostosFletesDetalleInfo;
                
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
                    if (txtTarifa.Value != null)
                    {
                        var fleteDetalleInfo = new FleteDetalleInfo
                        {
                            CostoID = Convert.ToInt32(skAyudaCosto.Clave.Trim()),
                            Tarifa = (decimal) txtTarifa.Value,
                            Costo = skAyudaCosto.Descripcion.Trim(),
                            UsuarioCreacion = AuxConfiguracion.ObtenerUsuarioLogueado(),
                        };
                        gridDatosCostos.ItemsSource = null;
                        if ((string) btnAgregar.Content == Properties.Resources.RegistrarProgramacionFlete_btnActualizar)
                        {
                            if (listaCostosFletesDetalleInfo != null)
                            {
                                if (listaCostosFletesDetalleInfo.Count >= indiceActual)
                                {
                                    FleteDetalleInfo detalleInfo = listaCostosFletesDetalleInfo[indiceActual];
                                    detalleInfo.CostoID = int.Parse(skAyudaCosto.Clave);
                                    detalleInfo.Costo = skAyudaCosto.Descripcion;
                                    detalleInfo.Tarifa = (decimal) txtTarifa.Value;
                                    if (detalleInfo.Opcion != 1)
                                    {
                                        detalleInfo.Opcion = 2;
                                        detalleInfo.UsuarioModificacion = AuxConfiguracion.ObtenerOrganizacionUsuario();
                                    }
                                }
                
                            }
                            RegresoFleteDetalleInfos = listaCostosFletesDetalleInfo;
                            gridDatosCostos.ItemsSource = listaCostosFletesDetalleInfo;
                        }
                        else
                        {
                            fleteDetalleInfo.Opcion = 1;
                            RegresoFleteDetalleInfos = listaCostosFletesDetalleInfo;
                            listaCostosFletesDetalleInfo.Add(fleteDetalleInfo);
                            gridDatosCostos.ItemsSource = listaCostosFletesDetalleInfo;
                        }
                    }

                    btnAgregar.Content = Properties.Resources.RegistrarProgramacionFlete_btnAgregar;
                    LimpiarCampos();
                }
                else
                {
                    string mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                        ? Properties.Resources.DatosBlancos_CorteGanado
                        : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                txtTarifa.Focus();
            }
        }
        /// <summary>
        /// Click boton limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSalir_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Valida el cerrado del formulario
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {

            if (!string.IsNullOrEmpty(skAyudaCosto.Clave) || !string.IsNullOrEmpty(txtTarifa.Text))
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramaciondeFlete_SalirCostos,
                    MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.No)
                {
                    Cancelado = true;
                    e.Cancel = true;

                }
            }
        }

        /// <summary>
        /// Click boton eliminar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var botonEliminar = (Button)e.Source;
            try
            {
                var costoSeleccionado = (FleteDetalleInfo)Extensor.ClonarInfo(botonEliminar.CommandParameter);
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format("{0} {1} {2}", Properties.Resources.CostodeFlte_EliminarCosto, costoSeleccionado.Costo, "?"),
                    MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    if (costoSeleccionado != null)
                    {
                        EliminarProveedorSeleccionado(costoSeleccionado);
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
        /// Boton editar costo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var costoSeleccionado = (FleteDetalleInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);

                if (costoSeleccionado != null)
                {
                    indiceActual = gridDatosCostos.SelectedIndex;
                    Contexto = new CostoOrganizacionInfo()
                    {
                        Costo = new CostoInfo()
                        {
                            
                            CostoID = costoSeleccionado.CostoID,
                            TipoCosto =
                                new TipoCostoInfo()
                                {
                                    TipoCostoID = (int)TipoCostoEnum.Flete
                                },
                            Descripcion = costoSeleccionado.Costo,
                        }

                    };
                    skAyudaCosto.Info = Contexto.Costo;
                    skAyudaCosto.Clave = Contexto.Costo.CostoID.ToString(CultureInfo.InvariantCulture);
                    skAyudaCosto.Descripcion = Contexto.Costo.Descripcion;
                    txtTarifa.Text = (costoSeleccionado.Tarifa).ToString(CultureInfo.InvariantCulture);
                    btnAgregar.Content = Properties.Resources.RegistrarProgramacionFlete_btnActualizar;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistrarProgramaciondeFlete_ErrorRegistrar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Solo teclear numero con puntos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTarifa_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumerosConPunto(e.Text);
        }
        /// <summary>
        /// Asigna foco de inicio a la ayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CostosdeFletes_OnLoaded(object sender, RoutedEventArgs e)
        {
            skAyudaCosto.AsignarFoco();
        }
        /// <summary>
        /// Obtiene el foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTarifa_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (skAyudaCosto.Info == null || skAyudaCosto.Info.CostoID == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CostosdeFletes_CostoFaltante, MessageBoxButton.OK, MessageImage.Stop);
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Agregar ayuda de costos
        /// </summary>
        private void AgregarAyudaCosto()
        {
            skAyudaCosto = new SKAyuda<CostoInfo>(200, false, new CostoInfo()
                                                          , "PropiedadClaveCatalogoAyudaTipoCosto"
                                                          , "PropiedadDescripcionCatalogoAyudaTipoCosto", false, 50, true)
            {
                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.CostoOrganizacion_CodigoInvalidoCosteo,
                MensajeBusquedaCerrar = Properties.Resources.Costo_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
            };

            skAyudaCosto.AsignaTabIndex(1);
            skAyudaCosto.IsTabStop = false;

            stpCosto.Children.Clear();
            stpCosto.Children.Add(skAyudaCosto);
        }

        /// <summary>
        /// Limpiar campos
        /// </summary>
        private void LimpiarCampos()
        {
            txtTarifa.Text = "";
            skAyudaCosto.LimpiarCampos();
            skAyudaCosto.Clave = string.Empty;
        }
        /// <summary>
        /// Validar campos en blanco
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCamposBlanco()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(skAyudaCosto.Clave.Trim()) && String.IsNullOrEmpty(skAyudaCosto.Descripcion.Trim()))
            {
                skAyudaCosto.AsignarFoco();
                resultado.Mensaje = Properties.Resources.CostosdeFletes_CostoRequerido;
                resultado.Resultado = false;
                return resultado;
            }
            if (String.IsNullOrEmpty(txtTarifa.Text.Trim()))
            {
                txtTarifa.Focus();
                resultado.Mensaje = Properties.Resources.CostosdeFletes_TarifaRequerido;
                resultado.Resultado = false;
                return resultado;
            }
            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Eliminar proveedor seleccionado
        /// </summary>
        /// <param name="costoSeleccionado"></param>
        private void EliminarProveedorSeleccionado(FleteDetalleInfo costoSeleccionado)
        {
            try
            {
                var programaciondeFletePl = new ProgramaciondeFletesPL();
                int indice = gridDatosCostos.SelectedIndex;
                listaCostosFletesDetalleInfo.RemoveAt(indice);
                if (costoSeleccionado.FleteDetalleID > 0)
                {
                    eliminadosFleteDetalle.Add(costoSeleccionado);
                    if (eliminadosFleteDetalle.Count > 0)
                    {
                        if (programaciondeFletePl.EliminarFleteDetalle(eliminadosFleteDetalle, AuxConfiguracion.ObtenerUsuarioLogueado()))
                        {
                            eliminadosFleteDetalle.Clear();
                        }
                    }

                }

                gridDatosCostos.ItemsSource = null;
                gridDatosCostos.ItemsSource = listaCostosFletesDetalleInfo;
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
        #endregion

        
    }

    

}
