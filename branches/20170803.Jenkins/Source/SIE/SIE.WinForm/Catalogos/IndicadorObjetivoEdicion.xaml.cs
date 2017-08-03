using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for IndicadorObjetivoEdicion.xaml
    /// </summary>
    public partial class IndicadorObjetivoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private IndicadorObjetivoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (IndicadorObjetivoInfo)DataContext;
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
        public IndicadorObjetivoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            //CargaComboIndicadorProductoCalidad();
            CargaComboTipoObjetivoCalidad();
            //CargaComboOrganizacion();
            CargaComboIndicador();
        }

        /// <summary>
        /// Constructor para editar una entidad IndicadorObjetivo Existente
        /// </summary>
        /// <param name="indicadorObjetivoInfo"></param>
        public IndicadorObjetivoEdicion(IndicadorObjetivoInfo indicadorObjetivoInfo)
        {
            InitializeComponent();
            //CargaComboIndicadorProductoCalidad();
            CargaComboTipoObjetivoCalidad();
            //CargaComboOrganizacion();
            indicadorObjetivoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            CargaComboIndicador();
            Contexto = indicadorObjetivoInfo;

            cmbIndicador.IsEnabled = false;
            cmbProductoIndicador.IsEnabled = false;
            //cmbTipoObjetivoCalidad.IsEnabled = false;
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

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new IndicadorObjetivoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo
                {
                    OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                },
                Indicador = new IndicadorInfo(),
                IndicadorProductoCalidad = new IndicadorProductoCalidadInfo(),
                TipoObjetivoCalidad = new TipoObjetivoCalidadInfo()

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
                if (string.IsNullOrWhiteSpace(txtIndicadorObjetivoID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorObjetivoEdicion_MsgIndicadorObjetivoIDRequerida;
                    txtIndicadorObjetivoID.Focus();
                }
                else if (cmbIndicador.SelectedItem == null || cmbIndicador.SelectedIndex == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorObjetivoEdicion_MsgIndicadorProductoCalidadIDRequerida;
                    cmbIndicador.Focus();
                }
                else if (cmbProductoIndicador.SelectedItem == null || cmbProductoIndicador.SelectedIndex == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorObjetivoEdicion_MsgProductoRequerida;
                    cmbProductoIndicador.Focus();
                }
                else if (cmbTipoObjetivoCalidad.SelectedItem == null || Contexto.TipoObjetivoCalidad.TipoObjetivoCalidadID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorObjetivoEdicion_MsgTipoObjetivoCalidadIDRequerida;
                    cmbTipoObjetivoCalidad.Focus();
                }
                else if (!dtuObjetivoMinimo.Value.HasValue || Contexto.ObjetivoMinimo == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorObjetivoEdicion_MsgObjetivoMinimoRequerida;
                    dtuObjetivoMinimo.Focus();
                }
                else if (!dtuObjetivoMaximo.Value.HasValue || Contexto.ObjetivoMaximo == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorObjetivoEdicion_MsgObjetivoMaximoRequerida;
                    dtuObjetivoMaximo.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.IndicadorObjetivoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    var indicadorProductoCalidad = ObtenerProductoCalidad();

                    var indicadorObjetivo = new IndicadorObjetivoInfo
                        {
                            TipoObjetivoCalidad = Contexto.TipoObjetivoCalidad,
                            IndicadorProductoCalidad = indicadorProductoCalidad,
                            Organizacion = Contexto.Organizacion,
                            Activo = EstatusEnum.Activo
                        };

                    var indicadorObjetivoBL = new IndicadorObjetivoBL();

                    IndicadorObjetivoInfo indicadorObjetivoExiste = indicadorObjetivoBL.ObtenerPorFiltros(indicadorObjetivo);

                    if (indicadorObjetivoExiste != null && (Contexto.IndicadorObjetivoID == 0 || Contexto.IndicadorObjetivoID != indicadorObjetivoExiste.IndicadorObjetivoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.IndicadorObjetivoEdicion_MsgIndicadorExistente, indicadorObjetivoExiste.IndicadorObjetivoID);
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
                    Contexto.IndicadorProductoCalidad = ObtenerProductoCalidad();

                    var indicadorObjetivoBL = new IndicadorObjetivoBL();
                    indicadorObjetivoBL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.IndicadorObjetivoID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        cmbProductoIndicador.ItemsSource = null;
                        InicializaContexto();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.IndicadorObjetivo_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.IndicadorObjetivo_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        ///// <summary>
        ///// Carga los datos de la entidad Indicador Producto Calidad 
        ///// </summary>
        //private void CargaComboIndicadorProductoCalidad()
        //{
        //    var indicadorProductoCalidadBL = new IndicadorProductoCalidadBL();
        //    var indicadorProductoCalidad = new IndicadorProductoCalidadInfo
        //    {
        //        IndicadorProductoCalidadID = 0,
        //        //Descripcion = Properties.Resources.cbo_Seleccione,
        //    };
        //    IList<IndicadorProductoCalidadInfo> listaIndicadorProductoCalidad = indicadorProductoCalidadBL.ObtenerTodos(EstatusEnum.Activo);
        //    listaIndicadorProductoCalidad.Insert(0, indicadorProductoCalidad);
        //    cmbIndicadorProductoCalidad.ItemsSource = listaIndicadorProductoCalidad;
        //    cmbIndicadorProductoCalidad.SelectedItem = indicadorProductoCalidad;
        //}

        /// <summary>
        /// Carga los datos de la entidad Tipo Objetivo Calidad 
        /// </summary>
        private void CargaComboTipoObjetivoCalidad()
        {
            var tipoObjetivoCalidadBL = new TipoObjetivoCalidadBL();
            var tipoObjetivoCalidad = new TipoObjetivoCalidadInfo
            {
                TipoObjetivoCalidadID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<TipoObjetivoCalidadInfo> listaTipoObjetivoCalidad = tipoObjetivoCalidadBL.ObtenerTodos(EstatusEnum.Activo);
            listaTipoObjetivoCalidad.Insert(0, tipoObjetivoCalidad);
            cmbTipoObjetivoCalidad.ItemsSource = listaTipoObjetivoCalidad;
            cmbTipoObjetivoCalidad.SelectedItem = tipoObjetivoCalidad;
        }

        ///// <summary>
        ///// Carga los datos de la entidad Organización 
        ///// </summary>
        //private void CargaComboOrganizacion()
        //{
        //    var organizacionPL = new OrganizacionPL();
        //    var organizacion = new OrganizacionInfo
        //    {
        //        OrganizacionID = 0,
        //        Descripcion = Properties.Resources.cbo_Seleccione,
        //    };
        //    IList<OrganizacionInfo> listaOrganizacion = organizacionPL.ObtenerTodos(EstatusEnum.Activo);
        //    listaOrganizacion.Insert(0, organizacion);
        //    cmbOrganizacion.ItemsSource = listaOrganizacion;
        //    cmbOrganizacion.SelectedItem = organizacion;
        //}

        /// <summary>
        /// Carga los datos de la entidad Organización 
        /// </summary>
        private void CargaComboIndicador()
        {
            var indicadorBL = new IndicadorBL();
            var indicadorDefault = new IndicadorInfo
                {
                    IndicadorId = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione,
                };

            IList<IndicadorInfo> listaIndicador = indicadorBL.ObtenerTodos(EstatusEnum.Activo);
            listaIndicador.Insert(0, indicadorDefault);
            cmbIndicador.ItemsSource = listaIndicador;
            cmbIndicador.SelectedIndex = 0;
        }

        /// <summary>
        /// Carga los datos de la entidad Organización 
        /// </summary>
        private void CargaComboIndicadorProducto(int indicadorID)
        {
            var indicadorProductoCalidaBL = new IndicadorProductoCalidadBL();
            var indicadorProductoCalidadDefault = new ProductoInfo
            {
                ProductoId = 0,
                ProductoDescripcion = Properties.Resources.cbo_Seleccione,
            };

            IList<ProductoInfo> listaIndicador = indicadorProductoCalidaBL.ObtenerProductosPorIndicador(indicadorID);
            if (listaIndicador == null)
            {
                return;
            }
            listaIndicador.Insert(0, indicadorProductoCalidadDefault);
            cmbProductoIndicador.ItemsSource = listaIndicador;
            if (Contexto.IndicadorProductoCalidad.Producto == null || Contexto.IndicadorProductoCalidad.Producto.ProductoId == 0)
            {
                cmbProductoIndicador.SelectedIndex = 0;
            }
            else
            {
                cmbProductoIndicador.SelectedValue = Contexto.IndicadorProductoCalidad.Producto.ProductoId;
                cmbProductoIndicador.SelectedItem = Contexto.IndicadorProductoCalidad.Producto;
            }
        }

        #endregion Métodos

        private void CmbIndicador_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbIndicador.SelectedIndex > 0)
            {
                int indicadorID = Convert.ToInt32(cmbIndicador.SelectedValue);
                CargaComboIndicadorProducto(indicadorID);
            }
        }

        private IndicadorProductoCalidadInfo ObtenerProductoCalidad()
        {
            var indicadorProductoCalidadBL = new IndicadorProductoCalidadBL();
            var indicador = (IndicadorInfo)cmbIndicador.SelectedItem;
            var producto = (ProductoInfo)cmbProductoIndicador.SelectedItem;
            var indicadorFiltro = new IndicadorProductoCalidadInfo
                {
                    Indicador = indicador,
                    Producto = producto
                };
            var indicadorProductoCalidad = indicadorProductoCalidadBL.ObtenerPorIndicadorProducto(indicadorFiltro);

            return indicadorProductoCalidad;

        }
    }
}

