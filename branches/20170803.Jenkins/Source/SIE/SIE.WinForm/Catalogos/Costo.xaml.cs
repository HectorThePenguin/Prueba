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
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para Costo.xaml
    /// </summary>
    public partial class Costo
    {
        #region PROPIEDADES

        private CostoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CostoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        public Costo()
        {
            InitializeComponent();
            InicializaContexto();
            ObtenerTiposCosto();
            ObtenerTiposProrrateo();
            ObtenerRetenciones();
            ObtenerTipoCostoCentro();
            CargarCboEstatus();
        }
        

        #endregion CONSTRUCTORES

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
                ucPaginacion.DatosDelegado += ObtenerCostos;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                txtClaveContable.Focus();
                cboTipoCosto.SelectedIndex = 0;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Costo_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Costo_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
        /// Ejecuta una Busqueda con los Filtros
        /// seleccionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Levanta la ventana de edicion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editar_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as Button;
            try
            {
                var costoSelecionado = btn.CommandParameter as CostoInfo;
                if (costoSelecionado != null)
                {
                    var costo = new CostoInfo
                    {
                        ListaTipoCostos = Contexto.ListaTipoCostos,
                        ListaRetencion = Contexto.ListaRetencion,
                        ListaTipoProrrateo = Contexto.ListaTipoProrrateo,
                        CostoID = costoSelecionado.CostoID,
                        ClaveContable = costoSelecionado.ClaveContable,
                        Descripcion = costoSelecionado.Descripcion,
                        UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                        Retencion = costoSelecionado.Retencion,
                        TipoCosto = costoSelecionado.TipoCosto,
                        TipoProrrateo = costoSelecionado.TipoProrrateo,
                        AbonoA = costoSelecionado.AbonoA,
                        Activo = costoSelecionado.Activo,
                        CompraIndividual = costoSelecionado.CompraIndividual,
                        Compra = costoSelecionado.Compra,
                        Recepcion = costoSelecionado.Recepcion,
                        Gasto = costoSelecionado.Gasto,
                        Costo = costoSelecionado.Costo,
                        TipoCostoCentro = costoSelecionado.TipoCostoCentro,
                        ListaTipoCostoCentro =  Contexto.ListaTipoCostoCentro
                    };

                    var costoEdicion = new CostoEdicion(costo)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.Costo_Editar_Titulo }
                    };
                    costoEdicion.Left = (ActualWidth - costoEdicion.Width) / 2;
                    costoEdicion.Top = ((ActualHeight - costoEdicion.Height) / 2) + 132;
                    costoEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                    costoEdicion.ShowDialog();
                    CambiarLeyendaCombo();
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Costo_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
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
                var costoInfo = new CostoInfo
                                    {
                                        ListaRetencion = Contexto.ListaRetencion,
                                        ListaTipoCostos = Contexto.ListaTipoCostos,
                                        ListaTipoProrrateo = Contexto.ListaTipoProrrateo,
                                        ListaTipoCostoCentro = Contexto.ListaTipoCostoCentro,
                                        AbonoA = AbonoA.AMBOS,
                                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                                        Retencion = new RetencionInfo { RetencionID = 0 },
                                        TipoProrrateo = new TipoProrrateoInfo { TipoProrrateoID = 0 },
                                        TipoCostoCentro = new TipoCostoCentroInfo { TipoCostoCentroID = 0}
                                    };
                var costoEdicion = new CostoEdicion(costoInfo)
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.Costo_Nuevo_Titulo }
                };
                costoEdicion.Left = (ActualWidth - costoEdicion.Width) / 2;
                costoEdicion.Top = ((ActualHeight - costoEdicion.Height) / 2) + 132;
                costoEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                costoEdicion.ShowDialog();
                CambiarLeyendaCombo();
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Costo_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Valida que solo acepte numero y letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDescripcionAceptaNumerosLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }
        /// <summary>
        /// Valida solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtClaveContableAceptaNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Extensor.ValidarNumeros(e.Text);
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Iniciliaza el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CostoInfo();
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Buscar una lista de productos
        /// </summary>
        public void Buscar()
        {
            ObtenerCostos(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene una Lista de Productos
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerCostos(int inicio, int limite)
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
                var costoPL = new CostoPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<CostoInfo> resultadoInfo = costoPL.ObtenerPorPagina(pagina, Contexto);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<CostoInfo>();
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
        /// Obtiene los Tipo de Costo
        /// </summary>
        private void ObtenerTiposCosto()
        {
            try
            {
                var tipoCostoPL = new TipoCostoPL();
                IList<TipoCostoInfo> tiposCosto = tipoCostoPL.ObtenerTodos(EstatusEnum.Activo);
                if (tiposCosto != null && tiposCosto.Any())
                {
                    AgregarElementoInicialTiposCosto(tiposCosto);
                    Contexto.ListaTipoCostos = tiposCosto;
                }                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.Costo_ErrorTipoCosto, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los Tipo de Prorrateo
        /// </summary>
        private void ObtenerTiposProrrateo()
        {
            try
            {
                var tipoProrrateoPL = new TipoProrrateoPL();
                Contexto.ListaTipoProrrateo = tipoProrrateoPL.ObtenerTodos(EstatusEnum.Activo);
                AgregaTipoProrrateoVacio();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.Costo_ErrorTipoProrrateo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene las Retenciones
        /// </summary>
        private void ObtenerRetenciones()
        {
            try
            {
                var retencionPL = new RetencionPL();
                Contexto.ListaRetencion = retencionPL.ObtenerTodos(EstatusEnum.Activo);
                AgregaRetencionVacia();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.Costo_ErrorRetencion, MessageBoxButton.OK, MessageImage.Error);

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
        /// Agrega un elemento a lista de costo
        /// en la posicion cero
        /// </summary>
        private void AgregarElementoInicialTiposCosto(IList<TipoCostoInfo> tiposCosto)
        {
            var tipoCostoInicial = new TipoCostoInfo
                                {TipoCostoID = 0, Descripcion = Properties.Resources.cbo_Seleccionar};
            if (!tiposCosto.Contains(tipoCostoInicial))
            {
                tiposCosto.Insert(0, tipoCostoInicial);
            }
        }

        /// <summary>
        /// Remueve de la lista de retenciones, retencion
        /// con identificador cero
        /// </summary>
        private void AgregaRetencionVacia()
        {
            var retencionVacia = new RetencionInfo
                                     {
                                         RetencionID = 0,
                                         IndicadorRetencion = string.Empty,
                                         IndicadorImpuesto = Properties.Resources.cbo_Seleccione,
                                         TipoRetencion = string.Empty
                                     };
            if (!Contexto.ListaRetencion.Contains(retencionVacia))
            {
                Contexto.ListaRetencion.Insert(0, retencionVacia);
            }            
        }

        /// <summary>
        /// Cambiar leyenda de Todos por Seleccione
        /// </summary>
        private void CambiarLeyendaCombo()
        {
            TipoCostoInfo tipoCosto =
                Contexto.ListaTipoCostos.FirstOrDefault(desc => desc.Descripcion.Equals(Properties.Resources.cbo_Seleccione));
            if (tipoCosto != null)
            {
                tipoCosto.Descripcion = Properties.Resources.cbo_Seleccionar;
            }
        }

        /// <summary>
        /// Remueve de la lista de retenciones, retencion
        /// con identificador cero
        /// </summary>
        private void AgregaTipoProrrateoVacio()
        {
            var prorrateoVacio = new TipoProrrateoInfo()
                                     {
                                         TipoProrrateoID = 0,
                                         Descripcion = Properties.Resources.cbo_Seleccione
                                     };
            if (!Contexto.ListaTipoProrrateo.Contains(prorrateoVacio))
            {
                Contexto.ListaTipoProrrateo.Insert(0, prorrateoVacio);
            }
        }

        /// <summary>
        /// Obtener los tipos costos de centro
        /// </summary>
        private void ObtenerTipoCostoCentro()
        {
            try
            {
                var tipoCostoCentroPL = new TipoCostoCentroPL();
                List<TipoCostoCentroInfo> tipoCostoCentro = tipoCostoCentroPL.ObtenerTodos(EstatusEnum.Activo);
                if (tipoCostoCentro != null && tipoCostoCentro.Any())
                {
                    AgregarElementoInicialTiposCostoCentro(tipoCostoCentro);
                    Contexto.ListaTipoCostoCentro = tipoCostoCentro;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.Costos_msgErrorObtenerTipoCostoCentro, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Agregar elemento seleccione a tipo costo de centro
        /// </summary>
        /// <param name="tipoCostoCentro"></param>
        private void AgregarElementoInicialTiposCostoCentro(List<TipoCostoCentroInfo> tipoCostoCentro)
        {
            var tipoCostoInicial = new TipoCostoCentroInfo { TipoCostoCentroID = 0, Descripcion = Properties.Resources.cbo_Seleccione };
            if (!tipoCostoCentro.Contains(tipoCostoInicial))
            {
                tipoCostoCentro.Insert(0, tipoCostoInicial);
            }
        }


        #endregion METODOS
    }
}
