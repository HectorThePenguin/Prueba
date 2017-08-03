using System;
using System.Globalization;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Modelos;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for EnfermeriaEdicion.xaml
    /// </summary>
    public partial class EnfermeriaEdicion
    {
        readonly CorralPL plCorral = new CorralPL();
        ObservableCollection<SeleccionInfoModelo<CorralInfo>> corralesDisponibles;
        private List<SeleccionInfoModelo<CorralInfo>> listaCorralesFinal = new List<SeleccionInfoModelo<CorralInfo>>();

        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private EnfermeriaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (EnfermeriaInfo) DataContext;
                }
                set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public EnfermeriaEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad Enfermeria Existente
        /// </summary>
        /// <param name="enfermeriaInfo"></param>
        public EnfermeriaEdicion(EnfermeriaInfo enfermeriaInfo)
        {
            InitializeComponent();
            enfermeriaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            skAyudaOrganizacion.IsEnabled = false;
            Contexto = enfermeriaInfo;
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
            Inicializar();
            ObtenerCorralesEnfermeria();
        }

        /// <summary>
        /// Evento para cuando se selecciona un tipo de corral, obtiene los corrales de este tipo y genera el CorralesDisponibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lbTipoCorral_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            corralesDisponibles.Clear();
            if (lbTipoCorral.SelectedItem == null)
            {
                return;
            }
            if (Contexto.OrganizacionInfo.OrganizacionID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.EnfermeriaEdicion_MsgOrganizacionIDRequerida, MessageBoxButton.OK, MessageImage.Warning);
                lbTipoCorral.SelectedItem = null;
                skAyudaOrganizacion.Focus();
                return;
            }
            var tipoCorral = lbTipoCorral.SelectedItem as TipoCorralInfo;
            ObtenerCorralesPorTipoCorral(tipoCorral);
            ComplementarCorralesDisponiblesConCorralesEnfermeria();
            MarcarCorralesSeleccionados();
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
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Valida la entrada de caracteres cuando se escriben en los cuadros de textos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDescripcion_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }
        #endregion Eventos

        #region Métodos

        private void MarcarCorralesSeleccionados()
        {
            if(listaCorralesFinal.Any())
            {
                foreach (var corral in listaCorralesFinal)
                {
                    SeleccionInfoModelo<CorralInfo> corralMarcar =
                        corralesDisponibles.FirstOrDefault(cor => cor.Elemento.CorralID == corral.Elemento.CorralID);
                    if(corralMarcar == null)
                    {
                        continue;
                    }
                    corralMarcar.Marcado = corral.Marcado;
                }
            }
        }
        /// <summary>
        /// Inicializar variables
        /// </summary>
        private void Inicializar()
        {
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            //skAyudaOrganizacion.AsignarFoco();
            lbTipoCorral.ItemsSource = obtenerTiposCorral();
            lbTipoCorral.DisplayMemberPath = "Descripcion";
            lbTipoCorral.SelectionChanged += lbTipoCorral_SelectionChanged;
            corralesDisponibles = new ObservableCollection<SeleccionInfoModelo<CorralInfo>>();
            dgCorral.ItemsSource = corralesDisponibles;
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Obtiene los corrales que estan asignados actualmente a la enfermeria
        /// </summary>
        private void ObtenerCorralesEnfermeria()
        {
            if (Contexto.EnfermeriaID == 0)
                return;

            List<CorralInfo> listaEnfermeriaCorrales = plCorral.ObtenerCorralesPorEnfermeriaId(Contexto.EnfermeriaID);
            listaCorralesFinal = (from corral in listaEnfermeriaCorrales
                                  select new SeleccionInfoModelo<CorralInfo>
                                      {
                                          Elemento = new CorralInfo
                                              {
                                                  CorralID = corral.CorralID,
                                                  Codigo = corral.Codigo,
                                                  TipoCorralId = corral.TipoCorralId
                                              },
                                          Marcado = true
                                      }).ToList();
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new EnfermeriaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                OrganizacionInfo = new OrganizacionInfo()
            };
            dgCorral.ItemsSource = null;
            lbTipoCorral.SelectedItem = null;
            txtDescripcion.Focus();
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
                if (Contexto.OrganizacionInfo == null || Contexto.OrganizacionInfo.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.EnfermeriaEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (string.IsNullOrWhiteSpace(Contexto.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.EnfermeriaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.EnfermeriaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int enfermeriaId = Contexto.EnfermeriaID;
                    string descripcion = Contexto.Descripcion;
                    int organizacionID = Contexto.OrganizacionInfo.OrganizacionID;

                    var enfermeriaPL = new EnfermeriaPL();
                    EnfermeriaInfo enfermeria = enfermeriaPL.ObtenerPorDescripcion(descripcion, organizacionID);

                    if (enfermeria != null && (enfermeriaId == 0 || enfermeriaId != enfermeria.EnfermeriaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.EnfermeriaEdicion_MsgDescripcionExistente, enfermeria.EnfermeriaID);
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
                    ObtenerListaCorrales();
                    var enfermeriaPL = new EnfermeriaPL();
                    var organizacion = skAyudaOrganizacion.Contexto as OrganizacionInfo;
                    if (organizacion != null) Contexto.Organizacion = organizacion.OrganizacionID.ToString(CultureInfo.InvariantCulture);
                    enfermeriaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.EnfermeriaID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Enfermeria_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.Enfermeria_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Obtiene los tipos de corral para esta forma
        /// </summary>
        /// <returns></returns>
        private IEnumerable<TipoCorralInfo> obtenerTiposCorral()
        {
            var tiposPL = new SIE.Services.Servicios.PL.TipoCorralPL();
            var tiposCorral = tiposPL.ObtenerTodos().Where(e => e.Activo == EstatusEnum.Activo).ToList();
            return tiposCorral;
        }

        /// <summary>
        /// Obtiene los corrales por tipo de corral y la organizacion seleccionada en el contexto
        /// </summary>
        /// <param name="tipoCorral"></param>
        private void ObtenerCorralesPorTipoCorral(TipoCorralInfo tipoCorral)
        {
            if (Contexto.EnfermeriaID > 0 && (listaCorralesFinal != null && listaCorralesFinal.Any()))
            {
                AgregarCorralesGuardados(tipoCorral);
            }
            GenerarSeleccionModelo(plCorral.ObtenerCorralesPorTipoCorralSinEnfermeriaAsignada(tipoCorral, Contexto.OrganizacionInfo.OrganizacionID), false);
            
        }

        private void AgregarCorralesGuardados(TipoCorralInfo tipoCorral)
        {
            List<SeleccionInfoModelo<CorralInfo>> listaCorralesTipo =
                listaCorralesFinal.Where(cor => cor.Elemento.TipoCorralId == tipoCorral.TipoCorralID).ToList();
            listaCorralesTipo.ForEach(cor=> corralesDisponibles.Add(cor));
        }

        /// <summary>
        /// Completa al CorralesDisponibles la informacion agregando los corrales que ya estan asignados a la enfermeria
        /// </summary>
        private void ComplementarCorralesDisponiblesConCorralesEnfermeria()
        {
            GenerarSeleccionModelo(Contexto.Corrales, true);
        }

        /// <summary>
        /// Convierte una lista de corrales a una lista de SeleccionModeloInfo de Corrales y los agregar a corrales disponibles.
        /// </summary>
        /// <param name="corrales"></param>
        /// <param name="marcado"></param>
        private void GenerarSeleccionModelo(IEnumerable<CorralInfo> corrales, bool marcado)
        {
            corrales.Select(x => new SeleccionInfoModelo<CorralInfo>
                {
                    Elemento = x,
                    Marcado = marcado
                }).ToList().ForEach(l =>
                    {
                        if (listaCorralesFinal == null || !listaCorralesFinal.Any())
                        {
                            corralesDisponibles.Add(l);
                        }
                        else
                        {
                            SeleccionInfoModelo<CorralInfo> validarCorralAgregado =
                                listaCorralesFinal.FirstOrDefault(co => co.Elemento.CorralID == l.Elemento.CorralID);
                            if (validarCorralAgregado == null)
                            {
                                corralesDisponibles.Add(l);
                            }
                            else
                            {
                                l.Marcado = true;
                                corralesDisponibles.Add(l);
                            }
                        }

                    });
        }
        #endregion Métodos

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var corralSeleccionado = dgCorral.SelectedItem as SeleccionInfoModelo<CorralInfo>;
            if (corralSeleccionado == null)
            {
                return;
            }
            SeleccionInfoModelo<CorralInfo> validarCorralAgregado = listaCorralesFinal.FirstOrDefault(co => co.Elemento.CorralID == corralSeleccionado.Elemento.CorralID);
            if(validarCorralAgregado == null)
            {
                listaCorralesFinal.Add(corralSeleccionado);
            }
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var btn = (e.OriginalSource as System.Windows.Controls.Primitives.ButtonBase);
            if (btn != null && !btn.IsPressed)
            {
                e.Handled = true;
                return;
            }
            var corralSeleccionado = dgCorral.SelectedItem as SeleccionInfoModelo<CorralInfo>;
            if (corralSeleccionado == null)
            {
                return;
            }
            SeleccionInfoModelo<CorralInfo> corralDesmarcar = listaCorralesFinal.FirstOrDefault(co => co.Elemento.CorralID == corralSeleccionado.Elemento.CorralID);
            if (corralDesmarcar == null)
            {
                if (Contexto.EnfermeriaID > 0)
                {
                    listaCorralesFinal.Add(corralSeleccionado);
                }
            }
            else
            {
                if (Contexto.EnfermeriaID == 0)
                {
                    listaCorralesFinal.Remove(corralDesmarcar);
                }
                corralDesmarcar.Marcado = false;
            }
        }

        private void ObtenerListaCorrales()
        {
            Contexto.Corrales = (from corral in listaCorralesFinal
                                 select new CorralInfo
                                     {
                                         CorralID = corral.Elemento.CorralID,
                                         Activo = corral.Marcado ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                                         UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                                     }).ToList();

        }
    }
}
