using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para CapturaHumedadBusquedaFolio.xaml
    /// </summary>
    public partial class CapturaHumedadBusquedaFolio
    {
        #region  ATRIBUTOS

        public RegistroVigilanciaInfo RegistroVigilancia { get; set; }
        public List<int> ProductosHumedad;



        private bool confirmaSalir = true;
        public bool Cancelado { get; set; }

        #endregion ATRIBUTOS

        #region CONSTRUCTOR
        public CapturaHumedadBusquedaFolio()
        {
            InitializeComponent();
        }
        #endregion CONSTRUCTOR

        #region EVENTOS

        private void CapturaHumedadBusquedaFolio_OnLoaded(object sender, RoutedEventArgs e)
        {
            CargarProductosValidosHumedad();
            ObtenerRegistrosVigilancia(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void BtnBuscarClick(object sender, RoutedEventArgs e)
        {
            ObtenerRegistrosVigilancia(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void BtnAgregarClick(object sender, RoutedEventArgs e)
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

        private void BtnCancelarClick(object sender, RoutedEventArgs e)
        {
            confirmaSalir = true;
            Close();
        }

        private void GridBusquedaFolioMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AsignarValoresSeleccionadosGrid();
        }

        private void GridTrapasosPreviewKeyDown(object sender, KeyEventArgs e)
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
        /// <summary>
        /// Carga el Parametro por Organizacion de los productos que aplican para capturar muestra de Humedad
        /// </summary>
        private void CargarProductosValidosHumedad()
        {
            var parametroOrganizacionPL = new ParametroOrganizacionPL();

            var parametroOrganizacionInfo = new ParametroOrganizacionInfo
                                                {
                                                    Parametro = new ParametroInfo
                                                                    {
                                                                        Clave =
                                                                            ParametrosEnum.ProductosMuestraHumedad.
                                                                            ToString()
                                                                    },
                                                    Organizacion = new OrganizacionInfo
                                                                       {
                                                                           OrganizacionID =
                                                                               Auxiliar.AuxConfiguracion.
                                                                               ObtenerOrganizacionUsuario()
                                                                       }
                                                };

            ParametroOrganizacionInfo parametro =
                parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(parametroOrganizacionInfo.Organizacion.OrganizacionID,parametroOrganizacionInfo.Parametro.Clave);

            if(parametro== null)
            {
                SkMessageBox.Show(this, Properties.Resources.CapturaHumedadBusquedaFolio_SinParametro, MessageBoxButton.OK, MessageImage.Warning);
                confirmaSalir = false;
                Close();
                return;
            }
            if (parametro.Valor.Contains('|'))
            {
                ProductosHumedad = (from tipos in parametro.Valor.Split('|')
                                       select Convert.ToInt32(tipos)).ToList();
            }
            else
            {
                int producto = Convert.ToInt32(parametro.Valor);
                ProductosHumedad.Add(producto);
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
            var renglonSeleccionado = gridTrapasos.SelectedItem as RegistroVigilanciaInfo;
            if (renglonSeleccionado != null)
            {
                elementoSeleccionado = true;
                RegistroVigilancia = renglonSeleccionado;
            }
            return elementoSeleccionado;
        }

        internal void InicializaPaginador()
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerRegistrosVigilancia;
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
            var renglonSeleccionado = gridTrapasos.SelectedItem as RegistroVigilanciaInfo;
            if (renglonSeleccionado != null)
            {
                RegistroVigilancia = renglonSeleccionado;
                confirmaSalir = false;
                Close();
            }
        }

        private void ObtenerRegistrosVigilancia(int inicio, int limite)
        {
            try
            {
                var registroVigilanciaPL = new RegistroVigilanciaPL();
                RegistroVigilanciaInfo filtro = ObtenerFiltros();
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
                ResultadoInfo<RegistroVigilanciaInfo> resultadoTraspasoMateriaPrimaInfo =
                    registroVigilanciaPL.ObtenerPorPagina(pagina, filtro);

                if (resultadoTraspasoMateriaPrimaInfo == null)
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridTrapasos.ItemsSource = new List<EntradaEmbarqueInfo>();
                    string mensajeNoHayDatos = Properties.Resources.CapturaHumedadBusquedaFolio_SinFolios;
                    SkMessageBox.Show(this, mensajeNoHayDatos, MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }

                List<RegistroVigilanciaInfo> registrosValidos =
                    resultadoTraspasoMateriaPrimaInfo.Lista.Where(
                        rev => ProductosHumedad.Contains(rev.Producto.ProductoId)).ToList();

                resultadoTraspasoMateriaPrimaInfo.Lista = registrosValidos;

                if (resultadoTraspasoMateriaPrimaInfo.Lista != null &&
                    resultadoTraspasoMateriaPrimaInfo.Lista.Count > 0)
                {
                    gridTrapasos.ItemsSource = resultadoTraspasoMateriaPrimaInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoTraspasoMateriaPrimaInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridTrapasos.ItemsSource = new List<EntradaEmbarqueInfo>();
                    string mensajeNoHayDatos = Properties.Resources.CapturaHumedadBusquedaFolio_SinFolios;
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

        private RegistroVigilanciaInfo ObtenerFiltros()
        {
            var filtros = new RegistroVigilanciaInfo
            {
                FolioTurno = iudFolio.Value.HasValue ? iudFolio.Value.Value : 0,
                Organizacion = new OrganizacionInfo
                {
                    OrganizacionID = Auxiliar.AuxConfiguracion.ObtenerOrganizacionUsuario()
                },
                Activo = EstatusEnum.Activo
            };
            return filtros;
        }

        #endregion METODOS
    }
}
