using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CostoOrganizacion.xaml
    /// </summary>
    public partial class CostoOrganizacion
    {
        #region PROPIEDADES

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

        #endregion PROPIEADES

        #region VARIABLES

        private IList<TipoOrganizacionInfo> TiposOrganizacion;
        private SKAyuda<CostoInfo> skAyudaCosto;

        #endregion VARIABLES

        #region CONSTRUCTORES

        public CostoOrganizacion()
        {
            InitializeComponent();
            InicializaContexto();
            CargarCboEstatus();
            AgregarAyudaCosto();
            ObtenerTiposOrganizacion();
        }

        #endregion CONSTRUCTORES

        #region METODOS

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CostoOrganizacionInfo();
        }

        /// <summary>
        /// Obtiene los Tipos de Organizacion
        /// </summary>
        private void ObtenerTiposOrganizacion()
        {
            try
            {
                var tipoOrganizacionPL = new TipoOrganizacionPL();
                TiposOrganizacion = tipoOrganizacionPL.ObtenerTodos(EstatusEnum.Activo);
                if (TiposOrganizacion != null && TiposOrganizacion.Any())
                {
                    TiposOrganizacion.Insert(0,
                                             new TipoOrganizacionInfo
                                                 {
                                                     TipoOrganizacionID = 0,
                                                     Descripcion = Properties.Resources.cbo_Seleccionar
                                                 });
                }
                cboTipoOrganizacion.ItemsSource = TiposOrganizacion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.CostoOrganizacion_ErrorTipoCostoOrganizacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Agrega una ayuda de costo
        /// </summary>
        private void AgregarAyudaCosto()
        {
            skAyudaCosto = new SKAyuda<CostoInfo>(160, false, Contexto.Costo, "PropiedadClaveCosteoEntradaSinDependencia"
                                                , "PropiedadDescripcionCosteoEntradaSinDependencia"
                                                , "PropiedadOcultaProgramacionEmbarqueCostos", true, 50, 3, false)
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
        /// Buscar una lista de productos
        /// </summary>
        public void Buscar()
        {
            ObtenerCostosOrganizacion(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene una Lista de Costos por Organizacion
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerCostosOrganizacion(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var costoOrganizacionPL = new CostoOrganizacionPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<CostoOrganizacionInfo> resultadoInfo = costoOrganizacionPL.ObtenerPorPagina(pagina, Contexto);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<CostoOrganizacionInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Costo_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Costo_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga los valores del combo de estatus
        /// </summary>
        private void CargarCboEstatus()
        {
            try
            {
                IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
                cboEstatus.ItemsSource = estatusEnums;
                cboEstatus.SelectedItem = EstatusEnum.Activo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Avanza el foco al siguiente control
        /// </summary>
        /// <param name="e"></param>
        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }        

        /// <summary>
        /// Cambiar leyenda de Todos por Seleccione
        /// </summary>
        private void CambiarLeyendaCombo()
        {
            TipoOrganizacionInfo tipoOrganizacion =
                TiposOrganizacion.FirstOrDefault(desc => desc.Descripcion.Equals(Properties.Resources.cbo_Seleccione));
            if (tipoOrganizacion != null)
            {
                tipoOrganizacion.Descripcion = Properties.Resources.cbo_Seleccionar;
            }
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion METODOS

        #region EVENTOS

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerCostosOrganizacion;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                cboTipoOrganizacion.Focus();
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CostoOrganizacion_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CostoOrganizacion_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// evento cuando se preciona cualquier tecla en el formulario 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Busca Costos de Organizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Manda llamar la pantalla para 
        /// Generar un Nuevo Producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var costoOrganizacionInfo = new CostoOrganizacionInfo
                {
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Activo = EstatusEnum.Activo,
                    Automatico = Automatico.Si
                };
                var costoOrganizacionEdicion = new CostoOrganizacionEdicion(costoOrganizacionInfo, TiposOrganizacion)
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.CostoOrganizacion_Nuevo_Titulo }
                };
                costoOrganizacionEdicion.Left = (ActualWidth - costoOrganizacionEdicion.Width) / 2;
                costoOrganizacionEdicion.Top = ((ActualHeight - costoOrganizacionEdicion.Height) / 2) + 132;
                costoOrganizacionEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                costoOrganizacionEdicion.ShowDialog();
                CambiarLeyendaCombo();
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CostoOrganizacion_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Llama la ventana de Edicion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edicion_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as Button;
            try
            {
                var costoOrganizacionSelecionado = btn.CommandParameter as CostoOrganizacionInfo;
                if (costoOrganizacionSelecionado != null)
                {
                    var costoOrganizacionInfo = Extensor.ClonarInfo(costoOrganizacionSelecionado) as CostoOrganizacionInfo;
                    if (costoOrganizacionInfo != null)
                    {
                        costoOrganizacionInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();

                        var costoOrganizacionEdicion = new CostoOrganizacionEdicion(costoOrganizacionInfo,
                                                                                    TiposOrganizacion)
                                                           {
                                                               ucTitulo =
                                                                   {
                                                                       TextoTitulo =
                                                                           Properties.Resources.
                                                                           CostoOrganizacion_Editar_Titulo
                                                                   }
                                                           };
                        costoOrganizacionEdicion.Left = (ActualWidth - costoOrganizacionEdicion.Width)/2;
                        costoOrganizacionEdicion.Top = ((ActualHeight - costoOrganizacionEdicion.Height)/2) + 132;
                        costoOrganizacionEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                        costoOrganizacionEdicion.ShowDialog();
                        CambiarLeyendaCombo();
                        ReiniciarValoresPaginador();
                        Buscar();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CostoOrganizacion_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion EVENTOS
    }
}
