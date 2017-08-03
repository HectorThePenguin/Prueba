using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Interaction logic for RecepcionGanado.xaml
    /// </summary>
    public partial class RecepcionGanadoBusqueda
    {
        #region  ATRIBUTOS

        public EntradaEmbarqueInfo EntradaEmbarque { get; set; }

        private bool confirmaSalir = true;
        public bool Cancelado { get; set; }

        #endregion ATRIBUTOS
        
        #region CONSTRUCTOR

        public RecepcionGanadoBusqueda()
        {
            InitializeComponent();
        }
        #endregion CONSTRUCTOR

        #region EVENTOS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObtenerEmbarques(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerEmbarques(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            bool elementoSeleccionando = AsignarValoresSeleccionados();
            if (elementoSeleccionando)
            {
                confirmaSalir = false;
                Close();
            }
            else
            {
                string mensajeCerrar = Properties.Resources.RecepcionGanadoBusqueda_MensajeAgregar;
                SkMessageBox.Show(this, mensajeCerrar, MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            confirmaSalir = true;
            Close();
        }

        private void gridEmbarques_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AsignarValoresSeleccionadosGrid();            
        }
        private void gridEmbarques_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.SystemKey == Key.Enter)
            {
                AsignarValoresSeleccionadosGrid();
            }
        }

        private void txtIdEmbarque_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ValidarSoloNumeros(e.Text);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Folio_SalirSinSeleccionar
                                                            , MessageBoxButton.YesNo, MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Cancelado = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion EVENTOS

        #region METODOS

        internal void LlenaCombos()
        {
            ObtenerTipoOrigen();
            ObtenerOrigenes();
        }

        internal void InicializaPaginador()
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerEmbarques;
                ucPaginacion.AsignarValoresIniciales();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private void ObtenerTipoOrigen()
        {
            try
            {
                var tipoOrganizacionPL = new TipoOrganizacionPL();
                IList<TipoOrganizacionInfo> tiposOrganizacion = tipoOrganizacionPL.ObtenerTodos();
                
                tiposOrganizacion.Insert(0, new TipoOrganizacionInfo { TipoOrganizacionID = 0, Descripcion = Properties.Resources.Seleccione_Todos });

                cmbTipoMovimiento.ItemsSource = tiposOrganizacion;
                cmbTipoMovimiento.SelectedIndex = 0;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private void ObtenerOrigenes()
        {
            try
            {
                var organizacionPL = new OrganizacionPL();
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();

                IList<OrganizacionInfo> listaOrganizaciones = organizacionPL.ObtenerPendientesRecibir(organizacionId, Estatus.Pendiente.GetHashCode()) ?? new List<OrganizacionInfo>();

                listaOrganizaciones.Insert(0, new OrganizacionInfo {OrganizacionID = 0, Descripcion = Properties.Resources.Seleccione_Todos });

                cmbOrigen.ItemsSource = listaOrganizaciones;
                cmbOrigen.SelectedIndex = 0;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private void ObtenerEmbarques(int inicio, int limite)
        {
            try
            {
                var embarquePL = new EmbarquePL();
                FiltroEmbarqueInfo filtro = ObtenerFiltros();
                if (ucPaginacion.Contexto == null)
                {
                    ucPaginacion.Contexto = filtro;
                }
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(filtro, ucPaginacion.Contexto) &&
                                            ucPaginacion.CompararObjetos(filtro, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo {Inicio = inicio, Limite = limite};
                ResultadoInfo<EntradaEmbarqueInfo> resultadoEmbarqueInfo =
                    embarquePL.ObtenerEmbarquesPedientesPorPagina(pagina, filtro);
                if (resultadoEmbarqueInfo != null && resultadoEmbarqueInfo.Lista != null &&
                    resultadoEmbarqueInfo.Lista.Count > 0)
                {
                    gridEmbarques.ItemsSource = resultadoEmbarqueInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoEmbarqueInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridEmbarques.ItemsSource = new List<EntradaEmbarqueInfo>();
                    string mensajeNoHayDatos = Properties.Resources.RecepcionGanadoBusqueda_MensajeNoHayDatos;
                    SkMessageBox.Show(this, mensajeNoHayDatos, MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private FiltroEmbarqueInfo ObtenerFiltros()
        {
            var filtroEmbarque = new FiltroEmbarqueInfo();

            try
            {
                int embarque;
                int organizacionOrigen;
                int tipoOrganizacionOrigen;

                int.TryParse(txtIdEmbarque.Text, out embarque);
                int.TryParse(Convert.ToString(cmbOrigen.SelectedValue), out organizacionOrigen);
                int.TryParse(Convert.ToString(cmbTipoMovimiento.SelectedValue), out tipoOrganizacionOrigen);

                filtroEmbarque.FolioEmbarque = embarque;
                filtroEmbarque.OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario(); //Convert.ToInt32(Application.Current.Properties["OrganizacionID"]);
                filtroEmbarque.OrganizacionOrigenID = organizacionOrigen;
                filtroEmbarque.TipoOrganizacionOrigenID = tipoOrganizacionOrigen;
                filtroEmbarque.Estatus = Estatus.Pendiente.GetHashCode();
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return filtroEmbarque;
        }

        private void AsignarValoresSeleccionadosGrid()
        {
            var renglonSeleccionado = gridEmbarques.SelectedItem as EntradaEmbarqueInfo;
            if (renglonSeleccionado != null)
            {
                EntradaEmbarque = renglonSeleccionado;
                confirmaSalir = false;
                Close();
            }
        }

        /// <summary>
        /// Asiga los Valores que se han Seleccionado
        /// en el Grid
        /// </summary>
        /// <returns></returns>
        private bool AsignarValoresSeleccionados()
        {
            bool elementoSeleccionado = false;
            var renglonSeleccionado = gridEmbarques.SelectedItem as EntradaEmbarqueInfo;
            if (renglonSeleccionado != null)
            {
                elementoSeleccionado = true;
                EntradaEmbarque = renglonSeleccionado;
            }
            return elementoSeleccionado;
        }

        private bool ValidarSoloNumeros(string valor)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(valor));

            return !string.IsNullOrWhiteSpace(valor) && (ascci < 48 || ascci > 57);
        }

        #endregion METODOS
    }
}