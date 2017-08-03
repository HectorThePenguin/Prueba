using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Controles.Ayuda;
using System.Collections.Generic;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for FormulaEdicion.xaml
    /// </summary>
    public partial class FormulaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FormulaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FormulaInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Control de ayuda para Producto
        /// </summary>
        private SKAyuda<ProductoInfo> skAyudaProducto = null;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public FormulaEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            ObtenerTipoFormula();
            GenerarAyudaProducto();
        }

        /// <summary>
        /// Constructor para editar una entidad Formula Existente
        /// </summary>
        /// <param name="formulaInfo"></param>
        public FormulaEdicion(FormulaInfo formulaInfo)
        {
            InitializeComponent();
            formulaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            ObtenerTipoFormula();            
            Contexto = formulaInfo;
            GenerarAyudaProducto();
        }

        #endregion Constructores

        #region Eventos
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }
        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FormulaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Producto = new ProductoInfo(),
                TipoFormula = new TipoFormulaInfo()
            };
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(Contexto.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.FormulaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbTipoFormula.SelectedItem == null || Contexto.TipoFormula.TipoFormulaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.FormulaEdicion_MsgTipoFormulaIDRequerida;
                    cmbTipoFormula.Focus();
                }
                else if (Contexto.Producto.ProductoId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.FormulaEdicion_MsgProductoIDRequerida;
                    skAyudaProducto.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.FormulaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int formulaId = Contexto.FormulaId;

                    var formulaPL = new FormulaPL();
                    FormulaInfo formula = formulaPL.ObtenerPorDescripcion(Contexto.Descripcion);

                    if (formula != null && (formulaId == 0 || formulaId != formula.FormulaId))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.FormulaEdicion_MsgDescripcionExistente, formula.FormulaId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }


        /// <summary>
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var formulaPL = new FormulaPL();
                    formulaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.FormulaId == 0)
                    {
                        InicializaContexto();
                        txtDescripcion.Focus();
                    }
                    else
                    {
                        confirmaSalir = false;
                        Close();   
                    }                    
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Formula_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Formula_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Obtiene los tipos de Formula
        /// </summary>
        private void ObtenerTipoFormula()
        {
            try
            {
                var tipoFormulaPL = new TipoFormulaPL();
                IList<TipoFormulaInfo> tiposFormula = tipoFormulaPL.ObtenerTodos(EstatusEnum.Activo);
                var tipoFormulaSeleccione = new TipoFormulaInfo
                                                {
                                                    Descripcion = Properties.Resources.cbo_Seleccione
                                                };
                tiposFormula.Insert(0, tipoFormulaSeleccione);
                cmbTipoFormula.ItemsSource = tiposFormula;
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.FormulaEdicion_ErrorBuscarTipoFormula, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.FormulaEdicion_ErrorBuscarTipoFormula, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Genera el control de ayuda para Producto
        /// </summary>
        private void GenerarAyudaProducto()
        {
            skAyudaProducto = new SKAyuda<ProductoInfo>(160, false, Contexto.Producto
                                                , "PropiedadClaveFormulaEdicion"
                                                , "PropiedadDescripcionFormulaEdicion", true, true)
            {
                AyudaPL = new ProductoPL(),
                MensajeClaveInexistente = Properties.Resources.FormulaEdicion_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.FormulaEdicion_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.FormulaEdicion_Busqueda,
                MensajeAgregar = Properties.Resources.FormulaEdicion_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaFormulaEdicion,
                TituloPantalla = Properties.Resources.BusquedaFormulaEdicion_Titulo,
            };

            skAyudaProducto.AsignaTabIndex(3);
            skAyudaProducto.IsTabStop = false;

            stpProducto.Children.Clear();
            stpProducto.Children.Add(skAyudaProducto);
        }

        #endregion Métodos
    }
}
