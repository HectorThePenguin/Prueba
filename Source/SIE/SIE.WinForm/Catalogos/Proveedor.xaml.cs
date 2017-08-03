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
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for Proveedor.xaml
    /// </summary>
    public partial class Proveedor
    {
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ProveedorInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ProveedorInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #region Constructores
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Proveedor()
        {
            InitializeComponent();
            InicializaContexto();
        }
        #endregion

        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CargarCboEstatus();
                CargarTipoProveedor();
                ucPaginacion.DatosDelegado += ObtenerListaProveedor;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                ObtenerListaProveedor(ucPaginacion.Inicio, ucPaginacion.Limite);
                txtDescripcion.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Proveedor_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Proveedor_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para buscar Proveedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObtenerListaProveedor(ucPaginacion.Inicio, ucPaginacion.Limite);
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Proveedor_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Proveedor_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para un nuevo registro 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var proveedorNuevo =
                    new ProveedorNuevo();

                MostrarCentrado(proveedorNuevo);
                //ReiniciarValoresPaginador();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Proveedor_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var proveedorInfoSelecionado = (ProveedorInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (proveedorInfoSelecionado != null)
                {
                    var proveedorEdicion =
                        new ProveedorEdicion(proveedorInfoSelecionado)
                            {
                                ucTitulo = {TextoTitulo = Properties.Resources.Proveedor_Editar_Titulo}
                            };
                    MostrarCentrado(proveedorEdicion);
                    ReiniciarValoresPaginador();
                    ObtenerListaProveedor(ucPaginacion.Inicio, ucPaginacion.Limite);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Proveedor_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Valida solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCodigoSap_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// Valida que el control solo acepte números y letras.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }		

        #endregion 

        #region Métodos

        /// <summary>
        /// 
        /// </summary>
        private void CargarTipoProveedor()
        {
            try
            {
                var proveedorPL = new TipoProveedorPL();
                IList<TipoProveedorInfo> listaTipoProveedor = proveedorPL.ObtenerTodos(EstatusEnum.Activo);
                var tipoProveedorTodos = new TipoProveedorInfo
                    {
                        TipoProveedorID = 0,
                        Descripcion = Properties.Resources.cbo_Seleccionar
                    };
                listaTipoProveedor.Insert(0, tipoProveedorTodos);
                cmbTipoProveedor.ItemsSource = listaTipoProveedor;
                cmbTipoProveedor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Proveedor_ErrorCargarTipoProveedor, MessageBoxButton.OK,
                                  MessageImage.Error);
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
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ProveedorInfo
                           {
                               Descripcion = txtDescripcion.Text.Trim(),
                               CodigoSAP = txtCodigoSap.Text.Trim(),
                               Activo = EstatusEnum.Activo
                           };
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaProveedor(int inicio, int limite)
        {
            try
            {
                var proveedorPL = new ProveedorPL();
                ProveedorInfo filtros = ObtenerFiltros();
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<ProveedorInfo> resultadoInfo = proveedorPL.ObtenerPorPaginaFiltros(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Proveedor>();
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

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ProveedorInfo ObtenerFiltros()
        {
            try
            {
                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Reinicia el valor del inicio de la página.
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion
    }
}

