using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para TraspasoMPPAMEDBusquedaFolio.xaml
    /// </summary>
    public partial class TraspasoMPPAMEDBusquedaFolio
    {
        #region  ATRIBUTOS

        public TraspasoMpPaMedInfo TraspasoMpPaMed { get; set; }

        private bool confirmaSalir = true;
        public bool Cancelado { get; set; }

        #endregion ATRIBUTOS

        #region CONSTRUCTOR

        public TraspasoMPPAMEDBusquedaFolio()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTOR

        #region EVENTOS

        private void TraspasoMPPAMEDBusquedaFolio_OnLoaded(object sender, RoutedEventArgs e)
        {
            ObtenerTraspasos(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerTraspasos(ucPaginacion.Inicio, ucPaginacion.Limite);
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

        private void gridTrapasos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AsignarValoresSeleccionadosGrid();            
        }

        private void gridTrapasos_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.SystemKey == Key.Enter)
            {
                AsignarValoresSeleccionadosGrid();
            }
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

        private int ObtenerDiasPermitidosCancelacion()
        {
            int diasPermitidos = 0;
            try
            {
                var tipoCancelacionPL = new TipoCancelacionPL();
                List<TipoCancelacionInfo> tiposCancelaciones = tipoCancelacionPL.ObtenerTodos();
                if(tiposCancelaciones == null)
                {
                    return 0;
                }
                TipoCancelacionInfo cancelacionTrapaso =
                    tiposCancelaciones.FirstOrDefault(
                        tipo => tipo.TipoCancelacionId == TipoCancelacionEnum.TraspasoMpPaMed.GetHashCode());
                if (cancelacionTrapaso != null)
                {
                    diasPermitidos = cancelacionTrapaso.DiasPermitidos;
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TraspasoMPPAMEDBusquedaFolio_ErrorConsultarDias, MessageBoxButton.OK, MessageImage.Warning);
            }
            return diasPermitidos;
        }

        /// <summary>
        /// Asiga los Valores que se han Seleccionado
        /// en el Grid
        /// </summary>
        /// <returns></returns>
        private bool AsignarValoresSeleccionados()
        {
            bool elementoSeleccionado = false;
            var renglonSeleccionado = gridTrapasos.SelectedItem as TraspasoMpPaMedInfo;
            if (renglonSeleccionado != null)
            {
                elementoSeleccionado = true;
                TraspasoMpPaMed = renglonSeleccionado;
            }
            return elementoSeleccionado;
        }

        internal void InicializaPaginador()
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerTraspasos;
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

        private void AsignarValoresSeleccionadosGrid()
        {
            var renglonSeleccionado = gridTrapasos.SelectedItem as TraspasoMpPaMedInfo;
            if (renglonSeleccionado != null)
            {
                TraspasoMpPaMed = renglonSeleccionado;
                confirmaSalir = false;
                Close();
            }
        }

        private void ObtenerTraspasos(int inicio, int limite)
        {
            try
            {
                var traspasoMateriaPrimaBL = new TraspasoMateriaPrimaBL();
                FiltroTraspasoMpPaMed filtro = ObtenerFiltros();
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
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<TraspasoMpPaMedInfo> resultadoTraspasoMateriaPrimaInfo =
                    traspasoMateriaPrimaBL.ObtenerPorPagina(pagina, filtro);
                if (resultadoTraspasoMateriaPrimaInfo != null && resultadoTraspasoMateriaPrimaInfo.Lista != null &&
                    resultadoTraspasoMateriaPrimaInfo.Lista.Count > 0)
                {
                    gridTrapasos.ItemsSource = resultadoTraspasoMateriaPrimaInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoTraspasoMateriaPrimaInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridTrapasos.ItemsSource = new List<EntradaEmbarqueInfo>();
                    string mensajeNoHayDatos = Properties.Resources.TraspasoMPPAMEDBusquedaFolio_NoHayFolios;
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

        private FiltroTraspasoMpPaMed ObtenerFiltros()
        {
            var filtros = new FiltroTraspasoMpPaMed
                {
                    DescripcionProducto = txtProductoBusqueda.Text,
                    Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID = Auxiliar.AuxConfiguracion.ObtenerOrganizacionUsuario()
                        },
                        DiasPermitidos = ObtenerDiasPermitidosCancelacion()
                };
            return filtros;
        }

        #endregion METODOS

      
    }
}
