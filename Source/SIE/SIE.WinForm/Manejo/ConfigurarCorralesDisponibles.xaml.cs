using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Configuration;
using Button = System.Windows.Controls.Button;


namespace SIE.WinForm.Manejo
{
    public partial class ConfigurarCorralesDisponibles 
    {
        #region Constructor
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private int _organizacionId;
        private AccionConfigurarCorrales _accion;
        private IList<CorralRangoInfo> _corrales;
        private IList<CorralRangoInfo> _corralesDisponibles;
        private IList<CorralRangoInfo> _corralesEliminados;
        private int corralConfiguradoID;
        private bool bandEditar= false;
        public ConfigurarCorralesDisponibles()
        {

            _organizacionId = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]);
            InitializeComponent();
            CargarCorralesConfigurados();
            CargaCboCorral();
            CargarCboSexo();
            _corralesEliminados = new List<CorralRangoInfo>();
            
        }
        #endregion

        #region Eventos

        /// <summary>
        /// Evento onChange 
        /// 1.-Evento para cambiar de combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboRangoInicial_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnAgregar.Focus();
            }
        }

        /// <summary>
        /// Evento onChange 
        /// 1.-Evento para cambiar de combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSexo_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboRangoInicial.Focus();
            }
        }

        /// <summary>
        /// Evento onChange 
        /// 1.-Evento para cambiar de combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCorral_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key== Key.Enter)
            {
                cboSexo.Focus();
            }
        }
       

        /// <summary>
        /// Evento onChange 
        /// 1.-limpia los campos que se llenan de manera dinamica como:
        ///    Rango Inicial, Rango Final y Tipo de Ganado
        /// 2.-Pregunta si selecciono una opcion de la lista del combo de sexo de ganado 
        ///    y regresa una lista para llenar el combo de rango inicial en base al sexo seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try{
                LimpiarCampos();
                if (cboSexo.SelectedItem == null) return;
                var tipoGanadoPl = new TipoGanadoPL();
                var sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem,true);
                var rangosIniciales = tipoGanadoPl.ObtenerPorSexo(sexoGanado);
                if (rangosIniciales.Count <= 0) return;
                var rangoInicialHidden = int.Parse(txtRangoInicial.Text);
                var index = -1;
                var i = 0;
                
                    foreach (var tipoGanadoInfo in rangosIniciales)
                    {
                        var rangoInicialKg = String.Format("{0}{1}", tipoGanadoInfo.PesoMinimo, " Kg");
                        tipoGanadoInfo.PesoMinimoString = rangoInicialKg;
                        cboRangoInicial.Items.Add(tipoGanadoInfo);
                        if (rangoInicialHidden > 0 && tipoGanadoInfo.PesoMinimo == rangoInicialHidden)
                        {
                            index = i;
                        }
                        i++;
                    }

                    if (rangoInicialHidden > 0)
                    {
                        if (bandEditar)
                        {
                            cboRangoInicial.SelectedIndex = index;
                        }
                        else
                        {
                            cboRangoInicial.SelectedIndex = -1;
                        }
                             
                    }
                bandEditar = false;

            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorRangoInicial, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorRangoInicial, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }         
        }

        /// <summary>
        /// Evento onChange
        /// 1.-Prregunta si selecciona alguna opcion de combo rango inicial
        ///    y regresa un objeto de tipo CorralRangoInfo, para mostrar el rango final y el tipo de ganado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboRangoInicial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboRangoInicial.SelectedItem == null) return;
                var tipoGanadoPl = new TipoGanadoPL();
                var objTipoGanado = ConvierteTipoGanadoInfo(cboRangoInicial.SelectedItem);
                if (objTipoGanado.PesoMinimo <= 0) return;
                var sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);
                var tipoGanadoInfo = tipoGanadoPl.ObtenerPorSexoRangoInicial(sexoGanado, objTipoGanado.PesoMinimo);
                if (tipoGanadoInfo == null) return;
                cboTipoGanado.ItemsSource = null;
                
                cboRangoFinal.Text = string.Format("{0}{1}", tipoGanadoInfo.PesoMaximo," Kg.");

                lblKg.Visibility = Visibility.Visible;
                cboTipoGanado.Items.Add(tipoGanadoInfo.Descripcion);
                cboTipoGanado.SelectedItem = tipoGanadoInfo.Descripcion;
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorRangoFinalTipoGanado, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorRangoFinalTipoGanado,
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }  
        }
        /// <summary>
        /// Agrega un corral al Datagrid y a la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            
            try{
                if (cboCorral.SelectedItem != null && cboRangoInicial.SelectedItem != null && cboSexo.SelectedItem != null)
                {
                    var corralRangoPl = new CorralRangoPL();
                    var sexoGanadoString = ObtieneSexoGanado(cboSexo.SelectedValue, false);
                    var descripcionTipoGanado = cboTipoGanado.SelectedItem.ToString();
                    var usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                    //creacion de la entidad
                    var corralRangoInfo = new CorralRangoInfo
                    {

                        OrganizacionID = _organizacionId,
                        Activo = 1, //estado inicial
                        CorralID = ConvierteCorralRangoInfo(cboCorral.SelectedItem).CorralID,
                        Codigo = ConvierteCorralRangoInfo(cboCorral.SelectedItem).Codigo,
                        Sexo = ObtieneSexoGanado(cboSexo.SelectedValue, true),
                        RangoInicial = ConvierteTipoGanadoInfo(cboRangoInicial.SelectedItem).PesoMinimo,
                        RangoFinal = int.Parse(cboRangoFinal.Text.Substring(0, cboRangoFinal.Text.Length-4)),
                        DescripcionTipoGanado = descripcionTipoGanado,
                        UsuarioCreacionId = usuarioID,
                        UsuarioModificacionId = usuarioID,

                    };
                    //datos del corral
                    var datosGrid = new CorralRangoInfo
                    {
                        Codigo = corralRangoInfo.Codigo,
                        Sexo = sexoGanadoString,
                        RangoInicial = corralRangoInfo.RangoInicial,
                        RangoFinal = corralRangoInfo.RangoFinal,
                        DescripcionTipoGanado = descripcionTipoGanado
                    };

                    if (btnAgregar.Content.Equals(Properties.Resources.btnAgregar))
                    {
                        //Validamos que no tenga lote asignado
                        if (ValidaLoteAsignado(_organizacionId, corralRangoInfo.CorralID))
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                                Properties.Resources.ConfigurarCorralesDisponibles_LoteAsignado, 
                                MessageBoxButton.OK, 
                                MessageImage.Warning);
                            return;
                        }

                        corralRangoInfo.UsuarioCreacionId = usuarioID;
                        //corralRangoInfo.FechaCreacion = DateTime.Now;

                        corralRangoInfo.Accion = AccionConfigurarCorrales.Agregar;
                        
                        corralRangoInfo.Id = dgCorralesRango.Items.Count + 1;

                        _corralesDisponibles.Add(corralRangoInfo);
                        dgCorralesRango.ItemsSource = null;
                        dgCorralesRango.ItemsSource = _corralesDisponibles;
                        
                        //dgCorralesRango.Items.Add(corralRangoInfo);

                        //Agrega el corral a CorralRango
                        //corralRangoPl.Crear(corralRangoInfo);
                        btnGuardar.IsEnabled = true;


                        corralRangoInfo.Modificado = true;
                        btnCancelar.IsEnabled = true;

                    }
                    else
                    {
                        if (btnAgregar.Content.Equals(Properties.Resources.btnActualizar))
                        {
                            corralRangoInfo.CorralAnteriorID = int.Parse(txtCorralAnteriorID.Text);
                            corralRangoInfo.UsuarioModificacionId = usuarioID;
                            //corralRangoInfo.FechaModificacion = DateTime.Now;
                            
                            corralRangoInfo.Id = corralConfiguradoID;
                            corralRangoInfo.Accion = _accion == AccionConfigurarCorrales.Agregar ? _accion : AccionConfigurarCorrales.Actualizar;

                            corralRangoInfo.Modificado = true;
                            


                            ActualizarRegistroGrid(corralRangoInfo);
                            //corralRangoPl.Actualizar(corralRangoInfo);

                            LimpiarCampos();
                            btnGuardar.IsEnabled = true;
                            txtRangoInicial.Text = "0";
                            btnAgregar.Content = Properties.Resources.btnAgregar;


                        }
                  
                       
                    }

                    //TODO: Revisar carga de nuevo del combo de corrales disponibles datosGrid q no se usa

                    CargaCboCorral();
                    cboRangoInicial.Items.Clear();
                    cboSexo.SelectedIndex = -1;
                    cboTipoGanado.SelectedIndex = -1;
                    lblKg.Visibility = Visibility.Hidden;
                    //CargarCorralesConfigurados();
                }            
                else 
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                        Properties.Resources.ConfigurarCorralesDisponibles_TodosObligatorios, 
                        MessageBoxButton.OK, 
                        MessageImage.Stop);
                }
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorAgregarActualizar, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorAgregarActualizar, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
            
        }

        private void ActualizarRegistroGrid(CorralRangoInfo corralRangoInfo)
        {
            int i = 0;
            foreach (var corralGrid in _corralesDisponibles)
            {
                //if (corralGrid.Accion == AccionConfigurarCorrales.Almacenado) continue;

                if (corralRangoInfo.Id == corralGrid.Id) {

                    _corralesDisponibles.Remove(corralGrid);
                    _corralesDisponibles.Insert(i, corralRangoInfo);

                    dgCorralesRango.ItemsSource = null;
                    dgCorralesRango.ItemsSource = _corralesDisponibles;
                    break;  
                }
                i++;
            }
        }

        private void UpdateCorralesDisponibles()
        {
            cboCorral.ItemsSource = null;
            cboCorral.ItemsSource = _corrales; // cargamos de nuevo el combo
        }

        
        /// <summary>
        /// Evento para Editar un registro del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {                        
            var btn = (Button) e.Source;
            var corralInfoSelecionado = (CorralRangoInfo)btn.CommandParameter;

            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionCorralesDisponibles_SeguroEliminar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                if (corralInfoSelecionado.Accion == AccionConfigurarCorrales.Almacenado)
                {
                    _corralesEliminados.Add(corralInfoSelecionado);
                }

                 _corralesDisponibles.Remove(corralInfoSelecionado);
                dgCorralesRango.ItemsSource = null;
                dgCorralesRango.ItemsSource = _corralesDisponibles;


                cboRangoInicial.SelectedIndex = -1;
                cboSexo.SelectedIndex = -1;

                cboTipoGanado.SelectedIndex = -1;
                cboCorral.SelectedIndex = -1;
                lblKg.Visibility = Visibility.Hidden;
                LimpiarCampos();
                btnGuardar.IsEnabled = true;
                CargaCboCorral();
                btnAgregar.Content = Properties.Resources.btnAgregar;

            }

        }

        /// <summary>
        /// Evento para Editar un registro del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {                        
            var btn = (Button) e.Source;
            var corralInfoSelecionado = (CorralRangoInfo)btn.CommandParameter;

            //Validamos que no tenga lote asignado
            if (ValidaLoteAsignado(_organizacionId, corralInfoSelecionado.CorralID))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_LoteAsignado, 
                    MessageBoxButton.OK, 
                    MessageImage.Warning);
                return;
            }
            CargaCboCorral();

            btnAgregar.Content = Properties.Resources.btnActualizar;

            _accion = corralInfoSelecionado.Accion;

            txtCorralAnteriorID.Text = corralInfoSelecionado.CorralAnteriorID != 0 ? 
                                        corralInfoSelecionado.CorralAnteriorID.ToString() :
                                        corralInfoSelecionado.CorralID.ToString();
            
            _corrales.Add(corralInfoSelecionado);
           
            UpdateCorralesDisponibles(); //actualizamos la coleccion del cbo
            cboCorral.SelectedIndex = _corrales.IndexOf(corralInfoSelecionado); //seleccionamos el indice
            
            txtRangoInicial.Text = corralInfoSelecionado.RangoInicial.ToString();
            
            cboRangoFinal.Text = corralInfoSelecionado.RangoFinal + " Kg.";
            cboTipoGanado.Items.Add(corralInfoSelecionado.DescripcionTipoGanado);
            cboTipoGanado.SelectedItem = corralInfoSelecionado.DescripcionTipoGanado;

            //Se inicializa para que una vez que se asigne el valor se calcule el rango inicial.
            bandEditar = true;
            cboSexo.SelectedItem = null;
            cboSexo.SelectedItem = Sexo.Macho.ToString()[0].ToString() == (corralInfoSelecionado.Sexo) ? Sexo.Macho : Sexo.Hembra;

            corralConfiguradoID = corralInfoSelecionado.Id;

            

        }
        #endregion

        #region Metodos

        /// <summary>
        /// Valida si el corral tiene un lote asignado para no permitir su edicion
        /// </summary>
        /// <param name="organizacion"></param>
        /// <param name="corralId"></param>
        private static Boolean ValidaLoteAsignado(int organizacion, int corralId)
        {
            try
            {
                var corralRangoPl = new CorralRangoPL();
                return corralRangoPl.ObtenerLoteAsignado(organizacion, corralId);
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorCorralesDisponibles, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorCorralesDisponibles, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }  
            return false;
        }

        /// <summary>
        /// Llena el combo de corral
        /// </summary>
        private void CargaCboCorral()
        {
            try
            {
                var corralRangoPl = new CorralRangoPL();
                _corrales = corralRangoPl.ObtenerPorOrganizacionID(_organizacionId);
                if (_corrales.Count > 0)
                {
                    var corralesAEliminar = new List<CorralRangoInfo>();
                    foreach (var corral in _corrales)
                    {

                        if (ExisteCorralEnGrid(corral))
                        {
                            corralesAEliminar.Add(corral); 
                            //_corrales.Remove(corral);
                            //break;
                        }

                    }
                    //Se agregan los corrales a eliminar para q no se contemplen
                    if (_corralesEliminados != null && _corralesEliminados.Count > 0)
                    {
                        corralesAEliminar.AddRange(_corralesEliminados);
                    }

                    if(corralesAEliminar.Count > 0){

                        foreach (var corral in corralesAEliminar)
                        {
                            var listaCorrales = new List<CorralRangoInfo>();
                            listaCorrales.AddRange(_corrales);
                            foreach (var _corral in listaCorrales.Where(_corral => corral.CorralID == _corral.CorralID))
                            {
                                _corrales.Remove(_corral);
                            }
                            
                        }
                    }

                    cboCorral.ItemsSource = _corrales;


                }else
                {
                    cboCorral.ItemsSource = null;
                }

            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorCorralesDisponibles, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorCorralesDisponibles, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }  
        }

        private bool ExisteCorralEnGrid(CorralRangoInfo corral)
        {
                foreach (var rowGrid in dgCorralesRango.Items)
                {
                    var corralGrid = (CorralRangoInfo)rowGrid;
                    //if (corralGrid.Accion == AccionConfigurarCorrales.Almacenado) continue;

                    if (corral.CorralID == corralGrid.CorralID) return true;

                }
                return false;
        }

        /// <summary>
        /// Carga el grid de corrales configurados
        /// </summary>
        private void CargarCorralesConfigurados()
        {
            try
            {
                btnGuardar.IsEnabled = false;
                var corralRangoPl = new CorralRangoPL();
                dgCorralesRango.ItemsSource = null;
                var corralesConfigurados = corralRangoPl.ObtenerCorralesConfiguradosPorOrganizacionID(_organizacionId);
                
                _corralesDisponibles = corralesConfigurados ?? new List<CorralRangoInfo>();
                
                dgCorralesRango.ItemsSource = _corralesDisponibles;

            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorCorralesDisponibles, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorCorralesDisponibles, 
                    MessageBoxButton.OK, 
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Llena el combo de sexo de ganado, obtiene el sexo del ganado de un Enum dentro de la aplicacion
        /// </summary>
        private void CargarCboSexo()
        {
            IList<Sexo> sexoEnums = Enum.GetValues(typeof (Sexo)).Cast<Sexo>().ToList();
            cboSexo.ItemsSource = sexoEnums;
            LimpiarCampos();
        }
        /// <summary>
        /// Convierte el combo corral a el tipo CorralRangoInfo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CorralRangoInfo ConvierteCorralRangoInfo(Object obj)
        {
            return (CorralRangoInfo) obj;
        }
        /// <summary>
        /// Convierte el combo de tipo rango inicial a el tipo TipoGanadoInfo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TipoGanadoInfo ConvierteTipoGanadoInfo(Object obj)
        {
            return (TipoGanadoInfo)obj;
        }

        /// <summary>
        /// Obtiene el valor o el key del combo de sexo en base a la variable isKey
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="esClave"></param>
        /// <returns></returns>
        public String ObtieneSexoGanado(Object obj,Boolean esClave)
        {
            var sexoEnum = (Sexo)obj;
            if (esClave)
            {
                var sexoGanadoChar = (char)sexoEnum;
                return sexoGanadoChar.ToString();
            }
            return sexoEnum.ToString();
        }
       
        /// <summary>
        ///1.-limpia los campos que se llenan de manera dinamica como:
        ///   Rango Inicial, Rango Final y Tipo de Ganado
        /// </summary>
        public void LimpiarCampos() {
            cboRangoInicial.Items.Clear();
            cboTipoGanado.Items.Clear();
            cboTipoGanado.Items.Clear();
            cboRangoFinal.Text = "";

        }
        #endregion

        /// <summary>
        /// Evento cargar Load del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigurarCorralesDisponibles_Loaded(object sender, RoutedEventArgs e)
        {
            if (cboCorral.Items == null || cboCorral.Items.Count == 0)
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    Properties.Resources.ConfigurarCorralesDisponibles_NoHayCorralesDisponibles, 
                    MessageBoxButton.OK, 
                    MessageImage.Warning);

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.Cancelarcaptura_CorteGanado,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {

                cboRangoInicial.SelectedIndex = -1;
                cboSexo.SelectedIndex = -1;
              
                cboTipoGanado.SelectedIndex = -1;
                cboCorral.SelectedIndex = -1;
                lblKg.Visibility = Visibility.Hidden;
                LimpiarCampos();
                btnGuardar.IsEnabled = false;
                CargarCorralesConfigurados();
                CargaCboCorral();
                btnAgregar.Content = Properties.Resources.btnAgregar;
                
            }
          
        }


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var corralRangoPl = new CorralRangoPL();
                List<CorralRangoInfo> lista = dgCorralesRango.Items.Cast<CorralRangoInfo>().ToList();

                //Se Eliminan las configuraciones
                foreach (var corralGrid in _corralesEliminados)
                {
                    corralGrid.OrganizacionID = _organizacionId;
                    corralGrid.UsuarioModificacionId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                }

                corralRangoPl.Guardar(lista, _corralesEliminados);

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ConfiguracionCorrales_Guardado,
                       MessageBoxButton.OK,
                       MessageImage.Correct);
                CargarCorralesConfigurados();
                btnGuardar.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfiguracionCorrales_ErrorGuardar,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
            }

        }

      
    }

}
