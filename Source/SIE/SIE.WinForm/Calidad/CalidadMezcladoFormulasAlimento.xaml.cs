using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using BLToolkit.Data.Linq;
using NPOI.SS.Formula.Functions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace SIE.WinForm.Calidad
{
    /// <summary>
    /// Lógica de interacción para CalidadMezcladoFormulasAlimento.xaml
    /// </summary>
    public partial class CalidadMezcladoFormulasAlimento
    {

        
        #region Propiedades
        /// <summary>
        /// Lista  Total de Registros
        /// </summary>
        IList<CalidadMezcladoFormulasAlimentoInfo> ListaTotalRegistros = new List<CalidadMezcladoFormulasAlimentoInfo>();
        /// <summary>
        /// Lista Total de Registros para Guardar
        /// </summary>
        IList<CalidadMezcladoFormulasAlimentoInfo> ListaTotalRegistrosGuardar = new List<CalidadMezcladoFormulasAlimentoInfo>();
        /// <summary>
        /// Lista para el grid principal
        /// </summary>
        IList<CalidadMezcladoFormulasAlimentoPpalInfo> GridPrincipal = new List<CalidadMezcladoFormulasAlimentoPpalInfo>();
        /// <summary>
        /// Resultado de datos en calidad mezclado
        /// </summary>
        private CalidadMezcladoFormulasAlimentoInfo resultadoCalidadMezcladoFormulaAlimentoInfo;
        /// <summary>
        /// Control de ayuda para la busqueda pr organizacion tipo ganadera
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        /// <summary>
        /// Control para la ayuda de carro mezclador
        /// </summary>
        private SKAyuda<CamionRepartoInfo> skAyudaCarroMezclador;
        /// <summary>
        /// Control para la ayuda de Camion
        /// </summary>
        private SKAyuda<MezcladoraInfo> skAyudaMezcladora;
        /// <summary>
        /// Control para la ayuda de Corral
        /// </summary>
        private SKAyuda<CorralInfo> skAyudaCorral;
        /// <summary>
        /// 
        /// </summary>
        private CamionRepartoInfo camionReparto { get; set; }
        /// <summary>
        /// Control para la ayuda de Chofer
        /// </summary>
        private SKAyuda<ChoferInfo> skAyudaChofer;
        /// <summary>
        /// 
        /// </summary>
        private SKAyuda<OperadorInfo> skAyudaPersonaMuestreo;



        private SKAyuda<OperadorInfo> skAyudaEncargado; 
        /// <summary>
        /// 
        /// </summary>
        private OrganizacionInfo organizacionSeleccionada;

        private FormulaInfo formula;
        /// <summary>
        /// 
        /// </summary>
        private int usuarioId;

        /// <summary>
        /// 
        /// </summary>
        private CorralInfo Corral;
        private int organizacionID;

        private IList<FormulaInfo> formulasInfo;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CalidadMezcladoFormulasAlimento()
        {
            Corral = new CorralInfo();
            resultadoCalidadMezcladoFormulaAlimentoInfo =  new CalidadMezcladoFormulasAlimentoInfo();
            InitializeComponent();
            formula = new FormulaInfo();
            DptLaboratorista.Text = Application.Current.Properties["Nombre"].ToString();
            cargarFecha();
            organizacionSeleccionada = new OrganizacionInfo();
            formulasInfo = new List<FormulaInfo>();
            usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            
            CargarAyudas();
            CargarCampos();
            DeshabilitarControlesMuestreo(false);
            DeshabilitarControlesMuestreo(false);
            skAyudaOrganizacion.Focus();
         }
        #endregion

        #region Metodos


        /// <summary>
        /// limpia los campos del checklista a excepcion del mismo control, cada que se detecta un cambio 
        /// </summary>
        private void LimpiarDatosGrid()
        {
            AgregarAyudaCarroMezclador();
            AgregarAyudaMezcladora();
            AgregarAyudaCorral();
            AgregarAyudaChofer();
            AgregarAyudaPersonaMuestreo();
            CargarCampos();
            cmbSeleccionarTecnica.SelectedIndex = 0;
            DeshabilitarControlesMuestreo(false);
            DeshabilitarControlesMuestreo(false);
            CboFormulaMuestrear.Focus();
            BorrarCamposGrid();
            limpiarDatosMuestreo();
            GridPrincipal.Clear();
            DG_AnalisisMuestra.ItemsSource = new List<CalidadMezcladoFormulasAlimentoInfo>();
            DG_AnalisisMuestra.ItemsSource = GridPrincipal;
            ListaTotalRegistros.Clear();

        }
        /// <summary>
        /// Limpia campos de seccion muestreo
        /// </summary>
        public void limpiarDatosMuestreo()
        {
            TxtGramosMicrotPorTon.Clear();
            TxtTiempoMezclado.Clear();
            TxtGramosMicrotPorTon.Clear();
            FechaBatch.SelectedDate = null;
            FechaPremezcla.SelectedDate = null;
            TxtBatch.Clear();
            ListaTotalRegistrosGuardar = new List<CalidadMezcladoFormulasAlimentoInfo>();
        }

        /// <summary>
        /// Valida solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtBatch_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Valida solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTiempoMezclado_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = SkMessageBox.Show(
                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.
                        CalidadMezcladoFormulasAlimento_TablaResumen_MsgConfirmaCancelar,
                    MessageBoxButton.YesNo, MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    CargarAyudas();
                    CargarCampos();
                    DeshabilitarControlesMuestreo(false);
                    DeshabilitarControlesMuestreo(false);
                    skAyudaOrganizacion.AsignarFoco();
                    BorrarCamposGrid();
                    cmbSeleccionarTecnica.SelectedIndex = 0;
                    limpiarDatosMuestreo();
                    GridPrincipal.Clear();
                    DG_AnalisisMuestra.ItemsSource = new List<CalidadMezcladoFormulasAlimentoInfo>();
                    DG_AnalisisMuestra.ItemsSource = GridPrincipal;
                    ListaTotalRegistros.Clear();
                    SeleccionarRadioButton(false);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        /// <summary>
        /// Valida cada que cambian de seleccion el tipo combobox "tecnicaID" para cargar en el grid "Analisis Muestras" los datos que correpondan a la nueva tecnica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSeleccionarTecnica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AgregarAyudaCarroMezclador();
                AgregarAyudaMezcladora();
                AgregarAyudaCorral();
                AgregarAyudaChofer();
                AgregarAyudaPersonaMuestreo();
                CargarCampos();
                DeshabilitarControlesMuestreo(false);
                CboFormulaMuestrear.Focus();
                BorrarCamposGrid();
                limpiarDatosMuestreo();
                GridPrincipal.Clear();
                DG_AnalisisMuestra.ItemsSource = new List<CalidadMezcladoFormulasAlimentoInfo>();
                DG_AnalisisMuestra.ItemsSource = GridPrincipal;
                ListaTotalRegistros.Clear();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        /// <summary>
        /// Cargar datos para guardar
        /// </summary>
        /// <returns></returns>
        private CalidadMezcladoFormulasAlimentoInfo CargarDatosGuardar()
        {
            var regreso = new CalidadMezcladoFormulasAlimentoInfo()
            {
                Organizacion = new OrganizacionInfo
                {
                    OrganizacionID = organizacionSeleccionada.OrganizacionID
                },
                TipoTecnicaID = (int)cmbSeleccionarTecnica.SelectedValue,
                UsuarioLaboratotorista = new UsuarioInfo
                {
                    UsuarioID = usuarioId
                },
                Fecha = Convert.ToDateTime(DtpFechaEntrada.Text),
                Formula = new FormulaInfo
                {
                    FormulaId = formula.FormulaId
                },
                FechaPremezcla = (DateTime)FechaPremezcla.SelectedDate,
                FechaBatch = (DateTime)FechaBatch.SelectedDate,

                Batch = Convert.ToInt32(TxtBatch.Text),
                TiempoMezclado = Convert.ToInt32(TxtTiempoMezclado.Text),
                GramosMicrot = Convert.ToInt32(TxtGramosMicrotPorTon.Text),
                PersonaMuestreo = new OperadorInfo
                {
                    OperadorID = Convert.ToInt32(skAyudaPersonaMuestreo.Clave)
                }
            };

            regreso = cargarDatosAyuda(regreso);

            return regreso;
        }
        /// <summary>
        /// Carga los datos de ayudas
        /// </summary>
        /// <param name="regreso"></param>
        /// <returns></returns>
        private CalidadMezcladoFormulasAlimentoInfo cargarDatosAyuda(CalidadMezcladoFormulasAlimentoInfo regreso)
        {
            if (CarroMezcladoRadioButton.IsChecked != null && (bool)CarroMezcladoRadioButton.IsChecked)
            {
                regreso.LugarToma = TipoLugarMuestraEnum.CarroMezclador.GetHashCode();
                regreso.CamionReparto = new CamionRepartoInfo
                {
                    CamionRepartoID = Convert.ToInt32(skAyudaCarroMezclador.Clave)
                };
                regreso.Chofer = new ChoferInfo
                {
                    ChoferID = Convert.ToInt32(skAyudaChofer.Clave)
                };
            }
            if (MezcladoraRadioButton.IsChecked != null && (bool)MezcladoraRadioButton.IsChecked)
            {
                regreso.LugarToma = TipoLugarMuestraEnum.Mezcladora.GetHashCode();
                regreso.Mezcladora = new MezcladoraInfo
                {
                    mezcladoraID = Convert.ToInt32(skAyudaMezcladora.Clave)
                };
                regreso.Operador = new OperadorInfo
                {
                    OperadorID = Convert.ToInt32(skAyudaEncargado.Clave)
                };
            }
            if (CorralRadioButton.IsChecked != null && (bool)CorralRadioButton.IsChecked)
            {
                regreso.LugarToma = TipoLugarMuestraEnum.Corral.GetHashCode();
                regreso.CorralInfo = new CorralInfo
                {
                    CorralID = Convert.ToInt32(skAyudaCorral.Clave)
                };
            }
            return regreso;
        }
        /// <summary>
        /// Valida registros en blanco
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarRegistros()
        {
            var resultado = new ResultadoValidacion();
            if (String.IsNullOrEmpty(skAyudaOrganizacion.Clave))
            {
                skAyudaOrganizacion.AsignarFoco();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaOrganizacion;
                return resultado;
            }
            if (String.IsNullOrEmpty(cmbSeleccionarTecnica.Text) || cmbSeleccionarTecnica.Text == "Seleccionar:")
            {
                cmbSeleccionarTecnica.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaTecnica;
                return resultado;
            }
            if (String.IsNullOrEmpty(CboFormulaMuestrear.Text) || CboFormulaMuestrear.Text == "Seleccione")
            {
                CboFormulaMuestrear.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaFormulaMuestrear;
                return resultado;
            }
            if (String.IsNullOrEmpty(FechaPremezcla.Text))
            {
                FechaPremezcla.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaFechaPremezcla;
                return resultado;
            }
            if (String.IsNullOrEmpty(FechaBatch.Text))
            {
                FechaBatch.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaFechaBatch;
                return resultado;
            }
            if (CarroMezcladoRadioButton.IsChecked != null && (bool)CarroMezcladoRadioButton.IsChecked)
            {
                if (String.IsNullOrEmpty(skAyudaCarroMezclador.Clave))
                {
                    skAyudaCarroMezclador.AsignarFoco();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaCarroMezclador;
                    return resultado;
                }
                if (String.IsNullOrEmpty(skAyudaChofer.Clave))
                {
                    skAyudaChofer.AsignarFoco();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaChoferEncargado;
                    return resultado;
                }
            }
            if (MezcladoraRadioButton.IsChecked != null && (bool)MezcladoraRadioButton.IsChecked)
            {
                if (String.IsNullOrEmpty(skAyudaMezcladora.Clave))
                {
                    skAyudaMezcladora.AsignarFoco();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaMezcladora;
                    return resultado;
                }
                if (String.IsNullOrEmpty(skAyudaEncargado.Clave))
                {
                    skAyudaEncargado.AsignarFoco();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaChoferEncargado;
                    return resultado;
                }
            }
            if (CorralRadioButton.IsChecked != null && (bool)CorralRadioButton.IsChecked)
            {
                if (String.IsNullOrEmpty(skAyudaCorral.Clave))
                {
                    skAyudaCorral.AsignarFoco();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaCorral;
                    return resultado;
                }
            }
            if (String.IsNullOrEmpty(TxtBatch.Text))
            {
                TxtBatch.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaBatch;
                return resultado;
            }
            if (String.IsNullOrEmpty(TxtTiempoMezclado.Text))
            {
                TxtTiempoMezclado.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaTiempoMezclado;
                return resultado;
            }
            if (String.IsNullOrEmpty(skAyudaPersonaMuestreo.Clave))
            {
                skAyudaPersonaMuestreo.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaPersonaMuestreo;
                return resultado;
            }
            if (String.IsNullOrEmpty(TxtGramosMicrotPorTon.Text))
            {
                TxtGramosMicrotPorTon.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaMicrotPorTon;
                return resultado;
            }
            if (ListaTotalRegistrosGuardar.Count < 1)
            {
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_CapturaRegistroGrid;
                return resultado;
            }
            resultado.Resultado = true;
            return resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CorralRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (skAyudaCarroMezclador.IsEnabled)
            {
                if (skAyudaChofer.Info != null)
                {
                    skAyudaChofer.LimpiarCampos();
                    skAyudaChofer.IsEnabled = false;
                }
                skAyudaCarroMezclador.LimpiarCampos();
                skAyudaCarroMezclador.IsEnabled = false;

            }
            if (skAyudaMezcladora.IsEnabled)
            {
                if (skAyudaEncargado.Info != null)
                {
                    skAyudaEncargado.LimpiarCampos();
                    skAyudaEncargado.IsEnabled = false;
                }
                skAyudaMezcladora.LimpiarCampos();
                skAyudaMezcladora.IsEnabled = false;

            }
            AgregarAyudaCorral();
        }
        /// <summary>
        /// Boton para guardar calidad mezclado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {

                var resultadoValidacion = ValidarRegistros();
                if (resultadoValidacion.Resultado)
                {
                    if (resultadoCalidadMezcladoFormulaAlimentoInfo.CalidadMezcladoID == 0)
                    {
                        resultadoCalidadMezcladoFormulaAlimentoInfo = CargarDatosGuardar();
                    }

                    var calidadMezcladoraPL = new CalidadMezcladoFormulasAlimentoPL();
                    calidadMezcladoraPL.GuardarCalidadMezcladoFormulaAlimento(resultadoCalidadMezcladoFormulaAlimentoInfo, ListaTotalRegistrosGuardar);
                    string mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_DatosGuardarExito;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                      MessageBoxButton.OK, MessageImage.Correct);
                    CargarAyudas();
                    CargarCampos();
                    DeshabilitarControlesMuestreo(false);
                    DeshabilitarControlesMuestreo(false);
                    skAyudaOrganizacion.AsignarFoco();
                    BorrarCamposGrid();
                    cmbSeleccionarTecnica.SelectedIndex = 0;
                    TxtBatch.Clear();
                    TxtTiempoMezclado.Clear();
                    TxtGramosMicrotPorTon.Clear();
                    GridPrincipal.Clear();
                    DG_AnalisisMuestra.ItemsSource = new List<CalidadMezcladoFormulasAlimentoInfo>();
                    DG_AnalisisMuestra.ItemsSource = GridPrincipal;
                    ListaTotalRegistros.Clear();
                    SeleccionarRadioButton(false);
                    ListaTotalRegistrosGuardar = new List<CalidadMezcladoFormulasAlimentoInfo>();

                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje) ? Properties.Resources.DatosBlancos_CorteGanado : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (InvalidPortNameException ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConsultarFactores_GuardarError + ex.Message,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConsultarFactores_GuardarError, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConsultarFactores_GuardarError, MessageBoxButton.OK, MessageImage.Error);
            }

        }


        /// <summary>
        /// Carga datos de registro de formula
        /// </summary>
        /// <param name="resultadoCalidadMezcladoFormulaAlimentoInfo"></param>
        private void CargarCheckList(CalidadMezcladoFormulasAlimentoInfo resultadoCalidadMezcladoFormulaAlimentoInfo)
        {
            limpiarDatosMuestreo();
            FechaPremezcla.SelectedDate = resultadoCalidadMezcladoFormulaAlimentoInfo.FechaPremezcla;
            FechaBatch.SelectedDate = resultadoCalidadMezcladoFormulaAlimentoInfo.FechaBatch;
            var operadorPl = new OperadorPL();
            TipoLugarMuestraEnum enumTemporal;
            var res = TipoLugarMuestraEnum.TryParse(resultadoCalidadMezcladoFormulaAlimentoInfo.LugarToma.ToString(CultureInfo.InvariantCulture), out enumTemporal);
            if (res)
            {
                switch (enumTemporal)
                {
                    case TipoLugarMuestraEnum.CarroMezclador:
                        var camionRepartoPl = new CamionRepartoPL();
                        var camionReparto = new CamionRepartoInfo
                        {
                            CamionRepartoID = resultadoCalidadMezcladoFormulaAlimentoInfo.CamionReparto.CamionRepartoID,
                            Organizacion = new OrganizacionInfo
                            {
                                OrganizacionID = resultadoCalidadMezcladoFormulaAlimentoInfo.Organizacion.OrganizacionID
                            }
                        };
                        var camion = camionRepartoPl.ObtenerCamionRepartoPorID(camionReparto);
                        CarroMezcladoRadioButton.IsChecked = true;
                        skAyudaCarroMezclador.Clave =
                            resultadoCalidadMezcladoFormulaAlimentoInfo.CamionReparto.CamionRepartoID.ToString(CultureInfo.InvariantCulture);
                        AgregarAyudaChofer();
                        skAyudaCarroMezclador.Descripcion = camion.NumeroEconomico;
                        var choferPl = new ChoferPL();
                        var chofer = choferPl.ObtenerPorID(resultadoCalidadMezcladoFormulaAlimentoInfo.Chofer);
                        skAyudaChofer.Clave = resultadoCalidadMezcladoFormulaAlimentoInfo.Chofer.ChoferID.ToString(CultureInfo.InvariantCulture);
                        skAyudaChofer.Descripcion = chofer.NombreCompleto;
                        break;
                    case TipoLugarMuestraEnum.Mezcladora:
                        AgregarAyudaMezcladora();
                        MezcladoraRadioButton.IsChecked = true;
                        skAyudaMezcladora.Clave =
                            resultadoCalidadMezcladoFormulaAlimentoInfo.Mezcladora.mezcladoraID.ToString(CultureInfo.InvariantCulture);
                        var mezcladoraPl = new MezcladoraPL();
                        var mezcladora = new MezcladoraInfo
                        {
                            mezcladoraID = resultadoCalidadMezcladoFormulaAlimentoInfo.Mezcladora.mezcladoraID,
                            organizaionID = resultadoCalidadMezcladoFormulaAlimentoInfo.Organizacion.OrganizacionID
                        };
                        mezcladora = mezcladoraPl.ObtenerPorID(mezcladora);
                        skAyudaMezcladora.Descripcion = mezcladora.numeroEconomico;
                        AgregarAyudaEncargado();
                        skAyudaEncargado.Clave =
                            resultadoCalidadMezcladoFormulaAlimentoInfo.Operador.OperadorID.ToString(CultureInfo.InvariantCulture);
                        var operadorMezcladora = operadorPl.ObtenerPorID(resultadoCalidadMezcladoFormulaAlimentoInfo.Operador);
                        skAyudaEncargado.IsEnabled = false;
                        skAyudaEncargado.Descripcion = operadorMezcladora.NombreCompleto;
                        break;
                    case TipoLugarMuestraEnum.Corral:
                        var corralPl = new CorralPL();
                        var corral = corralPl.ObtenerCorralPorLoteID(
                            resultadoCalidadMezcladoFormulaAlimentoInfo.LoteID,
                            resultadoCalidadMezcladoFormulaAlimentoInfo.Organizacion.OrganizacionID);
                        skAyudaCorral.Clave = corral.Codigo;
                        break;

                }
            }

            TxtBatch.Text = resultadoCalidadMezcladoFormulaAlimentoInfo.Batch.ToString(CultureInfo.InvariantCulture);
            TxtTiempoMezclado.Text =
                resultadoCalidadMezcladoFormulaAlimentoInfo.TiempoMezclado.ToString(CultureInfo.InvariantCulture);
            skAyudaPersonaMuestreo.Clave =
                resultadoCalidadMezcladoFormulaAlimentoInfo.PersonaMuestreo.OperadorID.ToString(CultureInfo.InvariantCulture);

            var operador = operadorPl.ObtenerPorID(resultadoCalidadMezcladoFormulaAlimentoInfo.PersonaMuestreo.OperadorID);
            skAyudaPersonaMuestreo.Descripcion = operador.NombreCompleto;
            TxtGramosMicrotPorTon.Text = resultadoCalidadMezcladoFormulaAlimentoInfo.GramosMicrot.ToString(CultureInfo.InvariantCulture);
            DeshabilitarControlesMuestreo(false);
        }

        /// <summary>
        /// Validar si exites registro formula
        /// </summary>
        /// <returns></returns>
        private bool ExisteRegistroFormula()
        {


            var calidadMezcladoFormulaAlimentoInfo = new CalidadMezcladoFormulasAlimentoInfo
            {
                Organizacion = new OrganizacionInfo
                {
                    OrganizacionID = skAyudaOrganizacion.Clave != string.Empty ? Convert.ToInt32(skAyudaOrganizacion.Clave) : 0
                    //primerReimplante != null ? primerReimplante.PesoReal : 0
                },
                TipoTecnicaID = cmbSeleccionarTecnica.SelectedValue.GetHashCode(),
                Formula = new FormulaInfo
                {
                    FormulaId = formula.FormulaId
                }

            };
            var mezcladoraPl = new MezcladoraPL();
            LimpiarCamposMuestreo();
            resultadoCalidadMezcladoFormulaAlimentoInfo =
                mezcladoraPl.ObtenerCalidadMezcladoFormulaAlimento(calidadMezcladoFormulaAlimentoInfo);
            if (resultadoCalidadMezcladoFormulaAlimentoInfo != null)
            {

                CargarCheckList(resultadoCalidadMezcladoFormulaAlimentoInfo);
                return true;
            }

            return false;
        }
        /// <summary>
        /// Limpiar controles muestreo
        /// </summary>
        private void LimpiarCamposMuestreo()
        {
            if (CarroMezcladoRadioButton.IsChecked == true)
            {
                skAyudaCarroMezclador.LimpiarCampos();
                skAyudaChofer.LimpiarCampos();
            }
            if (CorralRadioButton.IsChecked == true)
            {
                skAyudaCorral.LimpiarCampos();
            }
            if (MezcladoraRadioButton.IsChecked == true)
            {
                skAyudaMezcladora.LimpiarCampos();
                skAyudaEncargado.LimpiarCampos();
            }
            if (skAyudaPersonaMuestreo.Info != null)
            {
                skAyudaPersonaMuestreo.LimpiarCampos();
            }

            FechaBatch.SelectedDate = null;
            FechaPremezcla.SelectedDate = null;
            TxtBatch.Text = string.Empty;
            TxtGramosMicrotPorTon.Text = string.Empty;
            TxtTiempoMezclado.Text = string.Empty;
            resultadoCalidadMezcladoFormulaAlimentoInfo = new CalidadMezcladoFormulasAlimentoInfo();

        }


        /// <summary>
        /// Deshabilita radio button
        /// </summary>
        /// <param name="habilitado"></param>
        private void DeshabilitarRadioButton(bool habilitado)
        {
            CarroMezcladoRadioButton.IsEnabled = habilitado;
            MezcladoraRadioButton.IsEnabled = habilitado;
            CorralRadioButton.IsEnabled = habilitado;
        }
        /// <summary>
        /// Seleccionar radio button
        /// </summary>
        /// <param name="seleccion"></param>
        public void SeleccionarRadioButton(bool seleccion)
        {
            CarroMezcladoRadioButton.IsChecked = seleccion;
            MezcladoraRadioButton.IsChecked = seleccion;
            CorralRadioButton.IsChecked = seleccion;
        }
        /// <summary>
        /// Carga ayudas de seccion muestreo
        /// </summary>
        public void CargarAyudasSeccionMuestreo()
        {
            AgregarAyudaCarroMezclador();
            AgregarAyudaMezcladora();
            AgregarAyudaCorral();
            AgregarAyudaChofer();
            AgregarAyudaPersonaMuestreo();
        }
        /// <summary>
        /// Deshabulita controles de muestreo
        /// </summary>
        /// <param name="habilitado"></param>
        public void DeshabilitarControlesMuestreo(bool habilitado)
        {
            CarroMezcladoRadioButton.IsEnabled = habilitado;
            MezcladoraRadioButton.IsEnabled = habilitado;
            CorralRadioButton.IsEnabled = habilitado;
            TxtBatch.IsEnabled = habilitado;
            TxtTiempoMezclado.IsEnabled = habilitado;
            FechaBatch.IsEnabled = habilitado;
            FechaPremezcla.IsEnabled = habilitado;
            TxtGramosMicrotPorTon.IsEnabled = habilitado;
            if (skAyudaCarroMezclador.Info != null)
            {
                skAyudaCarroMezclador.IsEnabled = habilitado;
            }
            if (skAyudaMezcladora.Info != null)
            {
                skAyudaMezcladora.IsEnabled = habilitado;
            }
            if (skAyudaCorral.Info != null)
            {
                skAyudaCorral.IsEnabled = habilitado;
            }
            if (skAyudaChofer.Info != null)
            {
                skAyudaChofer.IsEnabled = habilitado;
            }
            if (skAyudaPersonaMuestreo.Info != null)
            {
                skAyudaPersonaMuestreo.IsEnabled = habilitado;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        private void CargarCampos()
        {

            cargarTecnicaAnalisis();
            CargarFormulaMuestrear();
            cargarNumeroMuestras();
            cargarAnalisisMuestra();
        }

        /// <summary>
        /// Carga el contenido de la tabla "TipoMuestra” campo <Descripción> dentro del combobox de nombre "Analisis de muestras" 
        /// </summary>
        private void cargarAnalisisMuestra()
        {
            try
            {
                cmbAnalisisMuestra.Items.Clear();

                IList<CalidadMezcladoFormulasAlimentoInfo> calMezForAliInfo = new List<CalidadMezcladoFormulasAlimentoInfo>();
                var calMezForAliPL = new CalidadMezcladoFormulasAlimentoPL();
                calMezForAliInfo = calMezForAliPL.CargarComboboxAnalisis();
                var AnalisisMuestraSeleccione = new CalidadMezcladoFormulasAlimentoInfo
                {
                    AnalisisMuestraCombobox = Properties.Resources.CalidadMezcladoFormulasAlimento_AnalisisMuestras_AnalisisMuestra_seleccionar
                };
                calMezForAliInfo.Insert(0, AnalisisMuestraSeleccione);
                foreach (CalidadMezcladoFormulasAlimentoInfo Elementos in calMezForAliInfo)
                {
                    cmbAnalisisMuestra.Items.Add(Elementos.AnalisisMuestraCombobox);
                }
                cmbAnalisisMuestra.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void CargarFormulaMuestrear()
        {
            var formulaPL = new FormulaPL();

            try
            {
                formulasInfo = formulaPL.ObtenerTodos();
                if (formulasInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.CalidadMezcladoFormulasAlimento_NoExistenFormulas,
                       MessageBoxButton.OK, MessageImage.Warning);

                }
                else
                {
                    var formulaMuestrear = new FormulaInfo();
                    formulaMuestrear.FormulaId = 0;
                    formulaMuestrear.Descripcion = "Seleccione";
                    formulasInfo.Insert(0, formulaMuestrear);
                    CboFormulaMuestrear.ItemsSource = formulasInfo;
                    CboFormulaMuestrear.SelectedValue = 0;
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Carga los 6 números de muestras que se incluyen en el combobox de noombre "Número de Muestras"
        /// </summary>
        private void cargarNumeroMuestras()
        {
        try
            {
                cmbNumeroMuestra.Items.Clear();
                List<NumeroMuestrasEnum> ListaNumeroMuestras = Enum.GetValues(typeof(NumeroMuestrasEnum)).Cast<NumeroMuestrasEnum>().ToList();
                foreach (int Elementos in ListaNumeroMuestras)
                    {
                        cmbNumeroMuestra.Items.Add(ListaNumeroMuestras.ElementAt(Elementos));
                    }
                cmbNumeroMuestra.Items.Insert(0, Properties.Resources.CalidadMezcladoFormulasAlimento_AnalisisMuestras_NumeroMuestra_seleccionar);
                cmbNumeroMuestra.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        
        }
        /// <summary>
        /// Hace referencia a la técnica a utilizar para el análisis.
        /// Se toman los datos de la tabla: “TipoTecnica”, campo <Descripcion>. y se carga en el combobox "Tecnica"
        /// </summary>
        private void cargarTecnicaAnalisis()
        {
            try
            {
                cmbSeleccionarTecnica.Items.Clear();

                IList<CalidadMezcladoFormulasAlimentoInfo> calMezForAliInfo = new List<CalidadMezcladoFormulasAlimentoInfo>();
                var calMezForAliPL = new CalidadMezcladoFormulasAlimentoPL();
                calMezForAliInfo = calMezForAliPL.CargarComboboxTecnica();
                var TecnicaSeleccione = new CalidadMezcladoFormulasAlimentoInfo
                {
                    Tecnica = Properties.Resources.CalidadMezcladoFormulasAlimento_DatosGenerales_Tecnica_seleccionar
                };
                calMezForAliInfo.Insert(0, TecnicaSeleccione);
                //foreach (CalidadMezcladoFormulasAlimentoInfo Elementos in calMezForAliInfo)
                //{
                //    cmbSeleccionarTecnica.Items.Add(Elementos.Tecnica);
                //}
                cmbSeleccionarTecnica.ItemsSource = calMezForAliInfo;
                cmbSeleccionarTecnica.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Trae la fecha del servidor de la base de datos para cargarlo en el campo fecha del formulario.
        /// </summary>
        private void cargarFecha()
        {
            FechaInfo fechaBD;
            FechaPL fechaPL = new FechaPL();
            fechaBD = fechaPL.ObtenerFechaActual();
            DtpFechaEntrada.Text = fechaBD.FechaActual.ToShortDateString().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Funcion que sirve para cargar los controles de ayuda
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion(splAyudaOrganizacion);
            AgregarAyudaCarroMezclador();
            AgregarAyudaMezcladora();
            AgregarAyudaCorral();
            AgregarAyudaChofer();
            AgregarAyudaPersonaMuestreo();
        }

        /// <summary>
        /// Funcionalidad del control de ayuda para búsqueda de organización
        /// </summary>
        private void AgregarAyudaOrganizacion(StackPanel stackPanel)
        {
            try
            {
                var organizacionInfo = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode() },
                    Activo = EstatusEnum.Activo
                };

                skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(280, false, organizacionInfo
                                                   , "PropiedadClaveConfiguracionPremezclas"
                                                   , "PropiedadDescripcionConfiguracionPremezclas",
                                                   "", false, 50, 9, true)
                {
                    AyudaPL = new OrganizacionPL(),
                    MensajeClaveInexistente = Properties.Resources.GastoInventario_AyudaOrganizacionClaveInexistente,
                    MensajeBusquedaCerrar = Properties.Resources.GastoInventario_AyudaOrganizacionSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.GastoInventario_AyudaOrganizacionMensajeBusqueda,
                    MensajeAgregar = Properties.Resources.GastoInventario_AyudaOrganizacionMensajeAgregar,
                    TituloEtiqueta = Properties.Resources.GastoInventario_AyudaOrganizacionTituloEtiqueta,
                    TituloPantalla = Properties.Resources.GastoInventario_AyudaOrganizacionTituloPantalla
                };


                skAyudaOrganizacion.ObtenerDatos += ObtenerDatosOrganizacion;
                skAyudaOrganizacion.LlamadaMetodosNoExistenDatos += LimpiarOrganizacion;

                skAyudaOrganizacion.AsignaTabIndex(0);
                stackPanel.Children.Clear();
                stackPanel.Children.Add(skAyudaOrganizacion);
                skAyudaOrganizacion.TabIndex = 1;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AgregarAyudaCarroMezclador()
        {
            skAyudaCarroMezclador = new SKAyuda<CamionRepartoInfo>(140, false, new CamionRepartoInfo()
            {
                Organizacion = new OrganizacionInfo()
                {
                    OrganizacionID = organizacionSeleccionada.OrganizacionID
                },
                Activo = EstatusEnum.Activo
            }, "PropiedadClaveCalidadMezcladoFormulasAlimento"
             , "PropiedadDescripcionCalidadMezcladoFormulasAlimento", false, 40, true)
            {
                AyudaPL = new CamionRepartoPL(),
                MensajeClaveInexistente = Properties.Resources.CarroMezclador_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.CarroMezclador_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.CarroMezclador_Busqueda,
                MensajeAgregar = Properties.Resources.CarroMezclador_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaCarroMezclador,
                TituloPantalla = Properties.Resources.BusquedaCarroMezclador_Titulo,
            };
            stpCarroMezclador.Children.Clear();
            stpCarroMezclador.Children.Add(skAyudaCarroMezclador);
            skAyudaCarroMezclador.AsignaTabIndex(5);
        }

        /// <summary>
        /// 
        /// </summary>

        private void AgregarAyudaMezcladora()
        {
            skAyudaMezcladora = new SKAyuda<MezcladoraInfo>(140, false, new MezcladoraInfo()
            {
                organizaionID = organizacionSeleccionada.OrganizacionID,
                Activo = EstatusEnum.Activo
            }, "PropiedadClaveCalidadMezcladoFormulasAlimento"
                                                             , "PropiedadDescripcionCalidadMezcladoFormulasAlimento", false, 40, true)
            {
                AyudaPL = new MezcladoraPL(),
                MensajeClaveInexistente = Properties.Resources.Mezcladora_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Mezcladora_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Mezcladora_Busqueda,
                MensajeAgregar = Properties.Resources.Mezcladora_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaMezcladora,
                TituloPantalla = Properties.Resources.BusquedaMezcladora_Titulo,
            };
            stpMezcladora.Children.Clear();
            stpMezcladora.Children.Add(skAyudaMezcladora);
            skAyudaMezcladora.AsignaTabIndex(6);
        }


        /// <summary>
        /// Agrega control de Ayuda para Jaula
        /// </summary>
        private void AgregarAyudaCorral()
        {
            var corral = new CorralInfo();
            try
            {
                skAyudaCorral = new SKAyuda<CorralInfo>(0, false,
                                                  Corral = new CorralInfo
                                                  {
                                                      Organizacion = new OrganizacionInfo
                                                      {
                                                          OrganizacionID = organizacionSeleccionada.OrganizacionID
                                                      },
                                                      Activo = EstatusEnum.Activo,
                                                      TipoCorral = new TipoCorralInfo
                                                      {
                                                          TipoCorralID = 1
                                                      },
                                                      FormulaInfo = new FormulaInfo
                                                      {
                                                          FormulaId = formula.FormulaId
                                                      }

                                                  },
                                                  "PropiedadClaveCaliadadMezcladoraFormulaAlimento"
                                                  , "PropiedadDescripcionCalidadMezcladoraFormulaAlimento", false, 175, true)

                {
                    AyudaPL = new CorralPL(),
                    MensajeClaveInexistente = Properties.Resources.CorralCalidad_CodigoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.CorralCalidad_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.CorralCalidad_Busqueda,
                    MensajeAgregar = Properties.Resources.CorralCalidad_Seleccionar,
                    TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaCorralCalidad,
                    TituloPantalla = Properties.Resources.BusquedaCorralCalidad_Titulo
                };

                skAyudaCorral.LlamadaMetodos += IniciarCorral;

                stpCorral.Children.Clear();
                stpCorral.Children.Add(skAyudaCorral);
                skAyudaCorral.AsignaTabIndex(9);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void IniciarCorral()
        {
            skAyudaCorral.Info  = new CorralInfo
            {
                Organizacion = new OrganizacionInfo
                {
                    OrganizacionID = organizacionSeleccionada.OrganizacionID
                },
                Activo = EstatusEnum.Activo,
                TipoCorral = new TipoCorralInfo
                {
                    TipoCorralID = 1
                },
                FormulaInfo = new FormulaInfo
                {
                    FormulaId = formula.FormulaId
                }

            };
        }


        /// <summary>
        /// Metodo para agregar el Control Ayuda Chofer
        /// </summary>
        private void AgregarAyudaChofer()
        {
            skAyudaChofer = new SKAyuda<ChoferInfo>(140, false, new ChoferInfo(), "PropiedadClaveEntradaGanado"
                                                  , "PropiedadDescripcionCaliadadMezcladoraFormulaAlimento", false, 40, true)
            {
                AyudaPL = new ChoferPL(),
                MensajeClaveInexistente = Properties.Resources.Chofer_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Chofer_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Chofer_Busqueda,
                MensajeAgregar = Properties.Resources.Chofer_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaChofer,
                TituloPantalla = Properties.Resources.BusquedaChofer_Titulo,
            };
            skAyudaChofer.AsignaTabIndex(9);
            SplAyudaChoferEncargado.Children.Clear();
            SplAyudaChoferEncargado.Children.Add(skAyudaChofer);
            
        }


        /// <summary>
        /// Metodo para agregar el Control Ayuda Chofer
        /// </summary>
        private void AgregarAyudaEncargado()
        {

            skAyudaEncargado = new SKAyuda<OperadorInfo>(140, false,
                                               new OperadorInfo
                                               {
                                                   Organizacion =
                                                       new OrganizacionInfo { OrganizacionID = organizacionSeleccionada.OrganizacionID },
                                                   Rol = new RolInfo { RolID = Roles.Mezclador.GetHashCode() }
                                               }
                                               , "PropiedadClaveEntradaGanado",
                                               "PropiedadDescripcionEntradaGanado"
                                               , true)
            {
                AyudaPL = new OperadorPL(),
                MensajeClaveInexistente = Properties.Resources.QuienRecibe_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.QuienRecibe_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.QuienRecibe_Busqueda,
                MensajeAgregar = Properties.Resources.QuienRecibe_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaChofer,
                TituloPantalla = Properties.Resources.BusquedaOperador_Titulo,
            };
            skAyudaEncargado.AsignaTabIndex(9);
            SplAyudaChoferEncargado.Children.Clear();
            SplAyudaChoferEncargado.Children.Add(skAyudaEncargado);
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Chofer
        /// </summary>
        private void AgregarAyudaPersonaMuestreo()
        {
            skAyudaPersonaMuestreo = new SKAyuda<OperadorInfo>(140, false,
                                    new OperadorInfo
                                    {
                                        Organizacion =
                                            new OrganizacionInfo { OrganizacionID = organizacionSeleccionada.OrganizacionID },
                                        Rol = new RolInfo { RolID = Roles.Analista.GetHashCode() }
                                    }
                                    , "PropiedadClaveEntradaGanado",
                                    "PropiedadDescripcionEntradaGanado"
                                    , true)
            {
                AyudaPL = new OperadorPL(),
                MensajeClaveInexistente = Properties.Resources.QuienRecibe_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.QuienRecibe_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.QuienRecibe_Busqueda,
                MensajeAgregar = Properties.Resources.QuienRecibe_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaChofer,
                TituloPantalla = Properties.Resources.BusquedaOperador_Titulo,
            };
            skAyudaPersonaMuestreo.AsignaTabIndex(10);
            SplAyudaPersonaMuestreo.Children.Clear();
            SplAyudaPersonaMuestreo.Children.Add(skAyudaPersonaMuestreo);
        }

        /// <summary>
        /// Limpia el control
        /// </summary>
        private void LimpiarOrganizacion()
        {
            skAyudaOrganizacion.LimpiarCampos();
            LimpiarDatosGrid();
        }
        /// <summary>
        /// Obtiene la informacion de la clave seleccionada
        /// </summary>
        /// <param name="clave"></param>
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
                organizacionSeleccionada = skAyudaOrganizacion.Info;

                LimpiarDatosGrid();
                //AgregarAyudaCarroMezclador();
                //AgregarAyudaMezcladora();
                //AgregarAyudaCorral();
                //DeshabilitarControlesMuestreo(false);
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        /// <summary>
        /// Valida si en la sección "Analisis de las muestras Inicial-Media-Final alguno de los campos obligatorios están vacios
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCamposVacios_seccionAnalisis()
        {
            ResultadoValidacion resultado = new ResultadoValidacion();
            string a = null;

            if (cmbAnalisisMuestra.SelectedIndex == 0)
            {
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_AnalisisMuestras_AnalisisMuestra_ValidarCampoVacio;
                resultado.Resultado = false;
                return resultado;
            }
            if (cmbNumeroMuestra.SelectedIndex == 0)
            {
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_AnalisisMuestras_NumeroMuestra_ValidarCampoVacio;
                resultado.Resultado = false;
                return resultado;
            }
            a = Peso.Value.ToString();
            if (string.IsNullOrEmpty(a) || a == "0")
            {
                Peso.Focus();
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_AnalisisMuestras_Peso_ValidarCampoVacio;
                resultado.Resultado = false;
                return resultado;
            }
            a = Particulas.Value.ToString();
            if (string.IsNullOrEmpty(a))
            {
                Particulas.Focus();
                resultado.Mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_AnalisisMuestras_ParticulasEncontradas_ValidarCampoVacio;
                resultado.Resultado = false;
                return resultado;
            }
            resultado.Resultado = true;
            return resultado;
        }
        /// <summary>
        /// Este metodo realiza las validaciones necesarias para cargar el grid con la informacion correspondiente
        /// </summary>
        /// <param name="nuevoRegistro"></param>
        private void CargarGrid (CalidadMezcladoFormulasAlimentoInfo nuevoRegistro)
        {
            try
            {
                bool correcto;
                bool sinAgregar= true;
                correcto = ValidarNuevoRegistro(nuevoRegistro);
                if (correcto)
                {
                    //Guarda en la lista donde se estaj registrando todos los regisstros el nuevo registro
                    ListaTotalRegistros.Add(nuevoRegistro);
                    if (!nuevoRegistro.BD)
                    {
                        ListaTotalRegistrosGuardar.Add(nuevoRegistro);
                    }
                    
                    //Determina si ya existe el GridInterior para agregrar el nuevo registro
                    foreach (CalidadMezcladoFormulasAlimentoPpalInfo elemento in GridPrincipal)
                    {
                        if (elemento.AnalisisMuestras == nuevoRegistro.AnalisisMuestras)
                        {
                            //calcula el promedio de particulas encontradas
                            decimal sum=0;
                            decimal cont = 0;
                            string promedioParticulas = "";
                            foreach (CalidadMezcladoFormulasAlimentoInfo renglon in elemento.GridInterior)
                            {
                                sum = sum + renglon.ParticulasEncontradas;
                                cont = cont + 1;
                            }
                            promedioParticulas = Convert.ToString(Math.Round((sum + nuevoRegistro.ParticulasEncontradas) / (cont + 1)));
                            elemento.GridInterior.Add(nuevoRegistro);
                            elemento.GridInterior = elemento.GridInterior.OrderBy(x => x.NumeroMuestras).ToList();
                            //asigna el promedio a todos los renglones
                            foreach (CalidadMezcladoFormulasAlimentoInfo renglon in elemento.GridInterior)
                            {
                                renglon.Promedio = promedioParticulas;
                            }
                            //borra el promedio a todos lo renglones menos al ultimo
                            CalidadMezcladoFormulasAlimentoInfo GI = new CalidadMezcladoFormulasAlimentoInfo();
                            for (int k = 0; k < elemento.GridInterior.Count-1; k++)
                            {
                                GI = elemento.GridInterior.ElementAt(k);
                                GI.Promedio = "";
                            }
                            sinAgregar = false;
                            break;
                        }
                    }
                    //Si en el bloque anterior de verifico que no existe el GridInterior, entonces procede a crearlo
                    if (sinAgregar)
                    {
                        CalidadMezcladoFormulasAlimentoPpalInfo NuevoElemento = new CalidadMezcladoFormulasAlimentoPpalInfo();
                        NuevoElemento.AnalisisMuestras = nuevoRegistro.AnalisisMuestras;
                        NuevoElemento.GridInterior = new List<CalidadMezcladoFormulasAlimentoInfo>();
                        nuevoRegistro.Promedio = Convert.ToString(nuevoRegistro.ParticulasEncontradas);
                        NuevoElemento.GridInterior.Add(nuevoRegistro);
                        GridPrincipal.Add(NuevoElemento);
                    }

                    DG_AnalisisMuestra.ItemsSource = new List<CalidadMezcladoFormulasAlimentoInfo>();
                    DG_AnalisisMuestra.ItemsSource = GridPrincipal;
                    BorrarCamposGrid();
                }
                else
                {
                    //imprimir porqué no se puede agregar al grid
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CalidadMezcladoFormulasAlimento_AnalisisMuestras_RegistroRepetido_1 +
                        nuevoRegistro.NumeroMuestras + Properties.Resources.CalidadMezcladoFormulasAlimento_AnalisisMuestras_RegistroRepetido_2 +
                        nuevoRegistro.AnalisisMuestras + Properties.Resources.CalidadMezcladoFormulasAlimento_AnalisisMuestras_RegistroRepetido_3,
                         MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// valida es el nuevo registo no esta repetido
        /// </summary>
        private bool ValidarNuevoRegistro (CalidadMezcladoFormulasAlimentoInfo nuevoRegistro)
        {
            bool correcto;
            foreach (var element in ListaTotalRegistros)                
            {
                if (element.AnalisisMuestras == nuevoRegistro.AnalisisMuestras && element.NumeroMuestras == nuevoRegistro.NumeroMuestras)
                {
                    correcto = false;
                    return correcto;
                }
            }
            correcto = true;
            return correcto;
        }

        private decimal CalcularParticulasEsperadas(int peso, string AnaMuestra)
        {
            decimal resultado = 1;
            decimal Fac=1;
            decimal porMatSec = 1;
            IList<CalidadMezcladoFactorInfo> DatosFactor = new List<CalidadMezcladoFactorInfo>();
            CalidadMezcladoFormulasAlimentoPL calidadMezcladoFormulasAlimentoPL = new CalidadMezcladoFormulasAlimentoPL();
            DatosFactor = calidadMezcladoFormulasAlimentoPL.ObtenerTablaFactor();
            //obtiene el valor de factor correcto dependiendo del tipo de "Analisis de Muestra" seleccionada en pantalla
            foreach (CalidadMezcladoFactorInfo elemento in DatosFactor)
            {
                if (AnaMuestra == elemento.Muestra)
                {
                    Fac = elemento.Factor;
                    //calcula % de Materia Sesa
                    porMatSec = (Convert.ToDecimal(elemento.PesoBS) /Convert.ToDecimal(elemento.PesoBH)) * 100;
                    break;
                }
            }
            resultado = ((Fac * 100) / porMatSec) * peso;
            return resultado; 
        }
        /// <summary>
        /// borra los campos que se usan para llenar un nuevo registro en el grid
        /// </summary>
        private void BorrarCamposGrid()
        {
            cmbAnalisisMuestra.SelectedIndex = 0;
            cmbNumeroMuestra.SelectedIndex = 0;
            Peso.Text = String.Empty;
            Particulas.Text = string.Empty;            
        }

        #endregion
        
        #region Eventos

        /// <summary>
        /// Seleccionar radio button mezcladora
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MezcladoraRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (skAyudaCarroMezclador.IsEnabled)
            {
                if (skAyudaChofer.Info != null)
                {
                    skAyudaChofer.LimpiarCampos();
                    skAyudaChofer.IsEnabled = false;
                }
                skAyudaCarroMezclador.LimpiarCampos();
                skAyudaCarroMezclador.IsEnabled = false;
            }
            if (skAyudaCorral.IsEnabled)
            {
                skAyudaCorral.LimpiarCampos();
                skAyudaCorral.IsEnabled = false;

            }
            AgregarAyudaMezcladora();
            AgregarAyudaEncargado();
        }
        /// <summary>
        /// Consultar factores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsultarFactores_OnClick(object sender, RoutedEventArgs e)
        {
            var medicamientoDialog = new ConsultarFactores(usuarioId)
            {
                Left = (ActualWidth - Width) / 2,
                Top = ((ActualHeight - Height) / 2) + 132,
                Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
            };
            medicamientoDialog.ShowDialog();
        }
        /// <summary>
        /// Seleccionar factores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboFormulaMuestrear_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CboFormulaMuestrear.SelectedIndex != 0 && CboFormulaMuestrear.SelectedItem != null)
            {
                if (skAyudaOrganizacion.Clave != string.Empty)
                {
                    foreach (var info in formulasInfo.Where(info => info.FormulaId == CboFormulaMuestrear.SelectedValue.GetHashCode()))
                    {
                        formula = new FormulaInfo
                        {
                            FormulaId = info.FormulaId
                        };
                        if (ExisteRegistroFormula())
                        {
                            //e.Handled = true; 
                            var calidadMezcladoFormulaAlimentoInfo = new CalidadMezcladoFormulasAlimentoInfo
                            {
                                Organizacion = new OrganizacionInfo
                                {
                                    OrganizacionID = skAyudaOrganizacion.Clave != string.Empty ? Convert.ToInt32(skAyudaOrganizacion.Clave) : 0
                                    //primerReimplante != null ? primerReimplante.PesoReal : 0
                                },
                                TipoTecnicaID = cmbSeleccionarTecnica.SelectedValue.GetHashCode(),
                                Formula = new FormulaInfo
                                {
                                    FormulaId = formula.FormulaId
                                }

                            };
                            IList<CalidadMezcladoFormulasAlimentoInfo> listaBd = new List<CalidadMezcladoFormulasAlimentoInfo>();
                            var calidadMezcladoFormulasAlimentoPl = new CalidadMezcladoFormulasAlimentoPL();
                            listaBd = calidadMezcladoFormulasAlimentoPl.CargarTablaMezcladoDetalle(calidadMezcladoFormulaAlimentoInfo);

                            ListaTotalRegistros.Clear();
                            GridPrincipal.Clear();
                            DG_AnalisisMuestra.ItemsSource = new List<CalidadMezcladoFormulasAlimentoInfo>();
                            DG_AnalisisMuestra.ItemsSource = GridPrincipal;


                            foreach (var elementos in listaBd)
                            {
                                CalidadMezcladoFormulasAlimentoInfo nuevoRegistro =
                                new CalidadMezcladoFormulasAlimentoInfo();

                                nuevoRegistro.AnalisisMuestras = elementos.AnalisisMuestras;
                                nuevoRegistro.NumeroMuestras = elementos.NumeroMuestras;
                                nuevoRegistro.PesoGramos = elementos.PesoGramos;
                                //calcula particulas esperadas
                                nuevoRegistro.ParticulasEsperadas = CalcularParticulasEsperadas(elementos.PesoGramos, elementos.AnalisisMuestras);
                                //carga el info con los datos ingresados en la seccion analiis de muestras Inicial-Media-Final
                                nuevoRegistro.ParticulasEncontradas = elementos.ParticulasEncontradas;
                                nuevoRegistro.BD = true;
                                CargarGrid(nuevoRegistro);
                            }

                        }
                        else
                        {
                            LimpiarCamposMuestreo();
                            DeshabilitarControlesMuestreo(false);
                            SeleccionarRadioButton(false);
                            FechaPremezcla.IsEnabled = true;
                            FechaBatch.IsEnabled = true;

                            ListaTotalRegistros.Clear();
                            GridPrincipal.Clear();
                            DG_AnalisisMuestra.ItemsSource = new List<CalidadMezcladoFormulasAlimentoInfo>();
                            DG_AnalisisMuestra.ItemsSource = GridPrincipal;
                        }
                    }

                }
                else
                {
                    string mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_msgCapturaOrganizacion;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    CboFormulaMuestrear.SelectedIndex = 0;
                    skAyudaOrganizacion.AsignarFoco();
                }

            }

        }
        /// <summary>
        /// Maneja contraer expandir del datagrid agrupados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowHeaderToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = (DependencyObject)e.OriginalSource;
            while (!(obj is DataGridRow) && obj != null) obj = VisualTreeHelper.GetParent(obj);
            if (obj is DataGridRow)
            {
                (obj as DataGridRow).IsSelected = (obj as DataGridRow).DetailsVisibility != Visibility.Visible;
            }
        }
        /// <summary>
        /// Se valida fecha premezcla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FechaPremezcla_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (FechaPremezcla.SelectedDate > DateTime.Today)
            {
                string mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_msgFechaPremezclaIncorrecta;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                  MessageBoxButton.OK, MessageImage.Warning);
                FechaPremezcla.SelectedDate = null;
            }
            e.Handled = true;


        }
        /// <summary>
        /// Se valida fecha batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FechaBatch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (FechaBatch.SelectedDate > DateTime.Today || FechaBatch.SelectedDate < DateTime.Today)
            {
                string mensaje = Properties.Resources.CalidadMezcladoFormulasAlimento_msgFechaBathIncorrecta;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                  MessageBoxButton.OK, MessageImage.Warning);
                FechaBatch.SelectedDate = null;
                DeshabilitarRadioButton(false);
                SeleccionarRadioButton(false);
            }
            else
            {

                if (FechaBatch.SelectedDate != null)
                {
                    DeshabilitarRadioButton(true);
                    CarroMezcladoRadioButton.IsChecked = true;
                    AgregarAyudaCarroMezclador();
                    AgregarAyudaChofer();
                    AgregarAyudaPersonaMuestreo();
                    DeshabilitarControlesMuestreo(true);
                    skAyudaMezcladora.IsEnabled = false;
                    skAyudaCorral.IsEnabled = false;
                    skAyudaCarroMezclador.AsignarFoco();
                }

            }
            e.Handled = true;
        }
        /// <summary>
        /// Seleccionar radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarroMezcladoRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (skAyudaCorral.IsEnabled)
            {
                skAyudaCorral.LimpiarCampos();
                skAyudaCorral.IsEnabled = false;
            }
            if (skAyudaMezcladora.IsEnabled)
            {
                skAyudaMezcladora.LimpiarCampos();
                if (skAyudaEncargado.Info != null)
                {
                    skAyudaEncargado.LimpiarCampos();
                    skAyudaEncargado.IsEnabled = false;
                }
                skAyudaMezcladora.IsEnabled = false;
            }
            AgregarAyudaCarroMezclador();
            AgregarAyudaChofer();
        }
        /// <summary>
        /// Agrega al grid, los datos ingresados en la seccion Analisis de las Muestras Inicial-Media-Final previas validaciones.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntAgregar_Click(object sender, RoutedEventArgs e)
        {
            ResultadoValidacion validar = ValidarCamposVacios_seccionAnalisis();
            //valida si hah campos obligatorios vacios
            if (validar.Resultado)
            {
                CalidadMezcladoFormulasAlimentoInfo nuevoRegistro = new CalidadMezcladoFormulasAlimentoInfo();
                nuevoRegistro.AnalisisMuestras = cmbAnalisisMuestra.Text.Trim();
                nuevoRegistro.NumeroMuestras = cmbNumeroMuestra.Text.Trim();
                nuevoRegistro.PesoGramos = Peso.Value.GetValueOrDefault();
                //calcula particulas esperadas
                nuevoRegistro.ParticulasEsperadas = CalcularParticulasEsperadas(nuevoRegistro.PesoGramos, cmbAnalisisMuestra.Text.Trim());
                //carga el info con los datos ingresados en la seccion analiis de muestras Inicial-Media-Final
                nuevoRegistro.ParticulasEncontradas = Particulas.Value.GetValueOrDefault();
                nuevoRegistro.BD = false;
                CargarGrid(nuevoRegistro);
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    validar.Mensaje, MessageBoxButton.OK, MessageImage.Stop);
            }
        }

        /// <summary>
        /// Evento que despliega una tabla con el resumen de las caputras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResumen_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!((organizacionSeleccionada.OrganizacionID == 0) || (formula.FormulaId == 0)))
                {
                    var ventanaResumen =
                        new CalidadMezcladoFormulasResumen(organizacionSeleccionada.OrganizacionID,
                            formula.FormulaId)
                        {
                            Left = (ActualWidth - Width)/2,
                            Top = (ActualHeight - Height)/2,
                            Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                        };
                    ventanaResumen.ShowDialog();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CalidadMezcladoFormulasAlimento_ValidacionBotonResumen,
                        MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Valida solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void TxtGramosMicrotPorTon_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        #endregion

    }
}
