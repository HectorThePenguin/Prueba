using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CorralDetectorEdicion.xaml
    /// </summary>
    public partial class CorralDetectorEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CorralDetectorInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CorralDetectorInfo)DataContext;
            }
            set { DataContext = value; }
        }

        ObservableCollection<SeleccionInfoModelo<CorralInfo>> corralesDisponibles;
        private List<SeleccionInfoModelo<CorralInfo>> listaCorralesFinal = new List<SeleccionInfoModelo<CorralInfo>>();
        private SeleccionInfoModelo<CorralInfo> corralInfoTodos =
            new SeleccionInfoModelo<CorralInfo> { Elemento = new CorralInfo { 
                                                        Codigo = "Todos", 
                                                        TipoCorral = new TipoCorralInfo()
                                                        }, 
                                                        Marcado = false };

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Ayuda de Operador
        /// </summary>
        private SKAyuda<OperadorInfo> skAyudaOperador;

        /// <summary>
        /// Organizacion
        /// </summary>
        private int organizacionID;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CorralDetectorEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            Contexto.Operador = new OperadorInfo()
            {
                OperadorID = 0,
                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                Rol = new RolInfo() { RolID = (int)Roles.Detector },
                Activo = EstatusEnum.Activo
            };
            GeneraAyudaOperador();
        }

        /// <summary>
        /// Constructor para editar una entidad CorralDetector Existente
        /// </summary>
        /// <param name="operadorInfo"></param>
        public CorralDetectorEdicion(OperadorInfo operadorInfo)
        {
            InitializeComponent();
            operadorInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto.Operador = operadorInfo;
            GeneraAyudaOperador();
            skAyudaOperador.IsEnabled = false;
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
            ObtenerListaCorralesPorDetector();
            skAyudaOperador.AsignarFoco();
        }

        /// <summary>
        /// Evento para cuando se selecciona un tipo de corral, obtiene los corrales de este tipo y genera el CorralesDisponibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lbTipoCorral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            corralesDisponibles.Clear();
            if (lbTipoCorral.SelectedItem == null || Contexto.Operador == null || Contexto.Operador.OperadorID<=0)
            {
                return;
            }
            
            var tipoCorral = lbTipoCorral.SelectedItem as TipoCorralInfo;

            corralInfoTodos.Marcado = false;

            corralesDisponibles.Add(corralInfoTodos);
            listaCorralesFinal.Remove(corralInfoTodos);

            ObtenerCorralesPorTipoCorral(tipoCorral);
            ComplementarCorralesDisponiblesConCorralesEnfermeria(tipoCorral);

            MarcarCorralesSeleccionados();

            corralInfoTodos.Marcado = false;

            if (corralesDisponibles.Count>1)
            {
                var listaMarcada = corralesDisponibles.Where(corral => !corral.Marcado && corral != corralInfoTodos).ToList();
                if (listaMarcada == null || !listaMarcada.Any())
                {
                    corralInfoTodos.Marcado = true;
                }
            }
        }
        
        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                                    Properties.Resources.Msg_CerrarSinGuardar, 
                                    MessageBoxButton.YesNo,
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
        /// Metodo para el evento del Checked del check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var corralSeleccionado = dgCorral.SelectedItem as SeleccionInfoModelo<CorralInfo>;
            if (corralSeleccionado == null)
            {
                return;
            }
            SeleccionInfoModelo<CorralInfo> validarCorralAgregado = listaCorralesFinal.FirstOrDefault(co => co.Elemento.CorralID == corralSeleccionado.Elemento.CorralID);
            if (validarCorralAgregado == null)
            {
                listaCorralesFinal.Add(corralSeleccionado);
            }
            ValidaCheckTodos(corralSeleccionado);
        }

        /// <summary>
        /// Metodo para el evento UnChecked del check de los corrales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var btn = (e.OriginalSource as System.Windows.Controls.Primitives.ButtonBase);

            var corralSeleccionado = dgCorral.SelectedItem as SeleccionInfoModelo<CorralInfo>;
            if (corralSeleccionado == null)
            {
                return;
            }

            ValidaCheckTodos(corralSeleccionado);

            if (btn != null && !btn.IsPressed)
            {
                e.Handled = true;
                return;
            }
            
            SeleccionInfoModelo<CorralInfo> corralDesmarcar = listaCorralesFinal.FirstOrDefault(co => co.Elemento.CorralID == corralSeleccionado.Elemento.CorralID);
            if (corralDesmarcar == null)
            {
                if (Contexto.Operador.OperadorID > 0)
                {
                    listaCorralesFinal.Add(corralSeleccionado);
                }
            }
            else
            {
                if (Contexto.Operador.OperadorID == 0)
                {
                    listaCorralesFinal.Remove(corralDesmarcar);
                }
                corralDesmarcar.Marcado = false;
            }
        }

        /// <summary>
        /// Valdiar si se checkeo al check de todos
        /// </summary>
        /// <param name="corralSeleccionado"></param>
        private void ValidaCheckTodos(SeleccionInfoModelo<CorralInfo> corralSeleccionado)
        {
            if (corralSeleccionado == corralInfoTodos)
            {
                if (corralInfoTodos.Marcado)
                {
                    foreach (var corral in corralesDisponibles.Where(corral => corral != corralInfoTodos))
                    {
                        SeleccionInfoModelo<CorralInfo> validarCorralAgregado =
                            listaCorralesFinal.FirstOrDefault(co => co.Elemento.CorralID == corral.Elemento.CorralID);
                        if (validarCorralAgregado == null)
                        {
                            listaCorralesFinal.Add(corral);
                            corral.Marcado = true;
                        }
                        else
                        {
                            validarCorralAgregado.Marcado = true;
                        }
                    }
                }
                else {
                    foreach (var corral in corralesDisponibles.Where(corral => corral != corralInfoTodos))
                    {
                        SeleccionInfoModelo<CorralInfo> validarCorralAgregado =
                            listaCorralesFinal.FirstOrDefault(co => co.Elemento.CorralID == corral.Elemento.CorralID);
                        if (validarCorralAgregado == null)
                        {
                            listaCorralesFinal.Add(corral);
                        }
                        else
                        {
                            validarCorralAgregado.Marcado = false;
                        }
                    }
                }
            }

        }

        #endregion Eventos

        #region Métodos

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
                    SeleccionInfoModelo<CorralInfo> validarCorralAgregado =
                            corralesDisponibles.FirstOrDefault(co => co.Elemento.CorralID == l.Elemento.CorralID);
                    if (validarCorralAgregado == null)
                    {
                        corralesDisponibles.Add(l);
                    }
                }
                else
                {
                    SeleccionInfoModelo<CorralInfo> validarCorralAgregado =
                        listaCorralesFinal.FirstOrDefault(co => co.Elemento.CorralID == l.Elemento.CorralID);
                    if (validarCorralAgregado == null)
                    {
                        corralesDisponibles.Add(l);
                    }
                    /*else
                    {
                        l.Marcado = true;
                        corralesDisponibles.Add(l);
                    }*/
                }

            });
        }

        /// <summary>
        /// Agregar a la lista los corrales q se encuentran almacenados para el detector
        /// </summary>
        /// <param name="tipoCorral"></param>
        private void AgregarCorralesGuardados(TipoCorralInfo tipoCorral)
        {
            List<SeleccionInfoModelo<CorralInfo>> listaCorralesTipo =
                listaCorralesFinal.Where(cor => cor.Elemento.TipoCorral.TipoCorralID == tipoCorral.TipoCorralID).ToList();
            listaCorralesTipo.ForEach(cor => corralesDisponibles.Add(cor));
        }

        /// <summary>
        /// Metodo para remarcar los corrales ya seleccionados
        /// </summary>
        private void MarcarCorralesSeleccionados()
        {
            if (listaCorralesFinal.Any())
            {
                foreach (var corral in listaCorralesFinal)
                {
                    SeleccionInfoModelo<CorralInfo> corralMarcar =
                        corralesDisponibles.FirstOrDefault(cor => cor.Elemento.CorralID == corral.Elemento.CorralID);
                    if (corralMarcar == null)
                    {
                        continue;
                    }
                    corralMarcar.Marcado = corral.Marcado;
                }
            }
        }

        /// <summary>
        /// Completa al CorralesDisponibles la informacion agregando los corrales que ya estan asignados a la enfermeria
        /// </summary>
        private void ComplementarCorralesDisponiblesConCorralesEnfermeria(TipoCorralInfo tipoCorral)
        {
            if (Contexto.Corrales != null)
            {
                List<CorralInfo> corrales = Contexto.Corrales.Where(
                    cor => cor.Elemento.TipoCorral.TipoCorralID == tipoCorral.TipoCorralID)
                    .Select(corral => corral.Elemento).ToList();
                GenerarSeleccionModelo(corrales, true);
            }
        }

        /// <summary>
        /// Obtiene los corrales por tipo de corral y la organizacion seleccionada en el contexto
        /// </summary>
        /// <param name="tipoCorral"></param>
        private void ObtenerCorralesPorTipoCorral(TipoCorralInfo tipoCorral)
        {
            if (Contexto.Operador.OperadorID > 0 && (listaCorralesFinal != null && listaCorralesFinal.Any()))
            {
                AgregarCorralesGuardados(tipoCorral);
            }

            var corralPL = new CorralPL();
            var corral = new CorralInfo
            {
                Organizacion = new OrganizacionInfo() { OrganizacionID = organizacionID },
                Operador = Contexto.Operador,
                TipoCorral = tipoCorral
            };

            ResultadoInfo<CorralInfo> listaCorral = corralPL.ObtenerCorralesPorTipoCorralDetector(corral);

            if (listaCorral != null && listaCorral.Lista != null)
            {
                GenerarSeleccionModelo(listaCorral.Lista, false);
            }

        }

        /// <summary>
        /// Obtiene los tipos de corral para esta forma
        /// </summary>
        /// <returns></returns>
        private IEnumerable<TipoCorralInfo> obtenerTiposCorral()
        {
            var tiposPL = new TipoCorralPL();
            var tiposCorral = tiposPL.ObtenerTodos().Where(e => e.Activo == EstatusEnum.Activo).ToList();
            return tiposCorral;
        }

        /// <summary>
        /// Inicializar variables
        /// </summary>
        private void Inicializar()
        {
            lbTipoCorral.ItemsSource = obtenerTiposCorral();
            lbTipoCorral.DisplayMemberPath = "Descripcion";
            lbTipoCorral.SelectionChanged += lbTipoCorral_SelectionChanged;
            corralesDisponibles = new ObservableCollection<SeleccionInfoModelo<CorralInfo>>();
            dgCorral.ItemsSource = corralesDisponibles;
        }

        /// <summary>
        /// Obtiene los corrales que estan asignados actualmente a la enfermeria
        /// </summary>
        private void ObtenerListaCorralesPorDetector()
        {
            if (Contexto.Operador.OperadorID == 0)
                return;

            var corralDetectorPL = new CorralDetectorPL();

            CorralDetectorInfo corralDetector =
                corralDetectorPL.ObtenerTodosPorDetector(Contexto.Operador.OperadorID);
            if (corralDetector != null && corralDetector.Corrales != null && corralDetector.Corrales.Any())
            {
                listaCorralesFinal = (from corral in corralDetector.Corrales
                                      select new SeleccionInfoModelo<CorralInfo>
                                      {
                                          Elemento = new CorralInfo
                                          {
                                              CorralID = corral.Elemento.CorralID,
                                              Codigo = corral.Elemento.Codigo,
                                              TipoCorral = corral.Elemento.TipoCorral,
                                              CorralDetectorID = corral.Elemento.CorralDetectorID
                                          },
                                          Marcado = true
                                      }).ToList();
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            Contexto =
                new CorralDetectorInfo
                {
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Operador = new OperadorInfo
                    {
                        Organizacion =
                            new OrganizacionInfo
                            {
                                OrganizacionID =
                                    organizacionID
                            },
                        Rol = new RolInfo()
                    },
                    Corrales = new List<SeleccionInfoModelo<CorralInfo>>()
                };
            corralesDisponibles = new ObservableCollection<SeleccionInfoModelo<CorralInfo>>();
            dgCorral.ItemsSource = corralesDisponibles;
            lbTipoCorral.SelectedItem = null;
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
                if (Contexto.Operador.OperadorID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CorralDetectorEdicion_MsgOperadorIDRequerida;
                    skAyudaOperador.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CorralDetectorEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {

                    if (!(listaCorralesFinal != null && listaCorralesFinal.Any()))
                    {
                        resultado = false;
                        mensaje = Properties.Resources.CorralDetectorEdicion_MsgNoHayCorrales;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    mensaje, 
                    MessageBoxButton.OK, 
                    MessageImage.Warning);
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
                    //ObtenerListaCorrales();
                    var corralDetectorPL = new CorralDetectorPL();
                    Contexto.Corrales = listaCorralesFinal.Where(corral => corral != corralInfoTodos).ToList();
                    corralDetectorPL.Guardar(Contexto);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                            Properties.Resources.GuardadoConExito, 
                            MessageBoxButton.OK, 
                            MessageImage.Correct);
                    /*if (Contexto.CorralDetectorID == 0)
                    {
                        InicializaContexto();
                        skAyudaOperador.AsignarFoco();                        
                    }
                    else
                    {*/
                        confirmaSalir = false;
                        Close();
                    //}
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                        Properties.Resources.CorralDetector_ErrorGuardar, 
                        MessageBoxButton.OK, 
                        MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                        Properties.Resources.CorralDetector_ErrorGuardar, 
                        MessageBoxButton.OK, 
                        MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Genera un control de ayuda
        /// para el Operador
        /// </summary>
        private void GeneraAyudaOperador()
        {
            skAyudaOperador = new SKAyuda<OperadorInfo>(300, false,
                                                        Contexto.Operador, 
                                                        "PropiedadClaveDetector",
                                                        "PropiedadDescripcionDetector", 
                                                        true, 
                                                        true)
                                  {
                                      AyudaPL = new OperadorPL(),
                                      MensajeClaveInexistente = Properties.Resources.QuienRecibe_CodigoInvalido,
                                      MensajeBusquedaCerrar = Properties.Resources.QuienRecibe_SalirSinSeleccionar,
                                      MensajeBusqueda = Properties.Resources.QuienRecibe_Busqueda,
                                      MensajeAgregar = Properties.Resources.QuienRecibe_Seleccionar,
                                      TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaChofer,
                                      TituloPantalla = Properties.Resources.BusquedaOperador_Titulo,
                                  };
            skAyudaOperador.LlamadaMetodos += InicializarRolOperador;
            skAyudaOperador.ObtenerDatos += ObtenerDatos;
            skAyudaOperador.AsignaTabIndex(1);
            stpOperador.Children.Add(skAyudaOperador);
        }

        /// <summary>
        /// Metodo para obtener datos una vez que la ayuda encuente al proveedor
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatos(string filtro)
        {

            if (skAyudaOperador.Info.Rol.RolID != (int) Roles.Detector)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorralDetector_OperadorNoDetector, 
                    MessageBoxButton.OK,
                    MessageImage.Stop);

                skAyudaOperador.LimpiarCampos();
                skAyudaOperador.Info = new OperadorInfo()
                {
                    OperadorID = 0,
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                    Rol = new RolInfo(){RolID = (int)Roles.Detector},
                    Activo = EstatusEnum.Activo
                };
                return;
            }
            Contexto.Operador=skAyudaOperador.Info;
            ObtenerListaCorralesPorDetector();
        }

        /// <summary>
        /// Inicializa el Rol del Operador
        /// </summary>
        private void InicializarRolOperador()
        {
            Contexto.Operador.Rol = new RolInfo(){RolID = (int)Roles.Detector};
        }

        #endregion Métodos

        
        
    }
}
