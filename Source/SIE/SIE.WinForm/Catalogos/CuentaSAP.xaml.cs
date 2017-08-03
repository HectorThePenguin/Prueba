using System;
using System.Collections.Generic;
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
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CuentaSAP.xaml
    /// </summary>
    public partial class CuentaSAP
    {
        #region PROPIEDADES

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CuentaSAPInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CuentaSAPInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion PROPIEDADES

        #region Metodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CuentaSAPInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                TipoCuenta = new TipoCuentaInfo()
            };
        }

        /// <summary>
        /// Carga los datos de la entidad Tipo Cuenta 
        /// </summary>
        private void CargaComboTipoCuenta()
        {
            try
            {
                var tipoCuentaPL = new TipoCuentaBL();
                var tipoCuenta = new TipoCuentaInfo
                {
                    TipoCuentaID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccionar,
                };
                IList<TipoCuentaInfo> listaTipoCuenta = tipoCuentaPL.ObtenerTodos(EstatusEnum.Activo);
                listaTipoCuenta.Insert(0, tipoCuenta);
                cmbTipoCuenta.ItemsSource = listaTipoCuenta;
                cmbTipoCuenta.SelectedItem = tipoCuenta;
                if (Contexto.TipoCuenta == null || Contexto.TipoCuenta.TipoCuentaID == 0)
                {
                    cmbTipoCuenta.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaCuenta(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaCuenta(int inicio, int limite)
        {
            try
            {
                var cuentaSAPPL = new CuentaSAPPL();
                CuentaSAPInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<CuentaSAPInfo> resultadoInfo = cuentaSAPPL.ObtenerPorPaginaCuentasSap(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    ucPaginacion.AsignarValoresIniciales();
                    gridDatos.ItemsSource = new List<Cuenta>();
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CuentaSAP_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CuentaSAP_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private CuentaSAPInfo ObtenerFiltros()
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

        #endregion Metodos

        #region Constructores

        public CuentaSAP()
        {
            InitializeComponent();
        }

        #endregion Constructores

        #region Eventos
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                InicializaContexto();
                CargaComboTipoCuenta();
                ucPaginacion.DatosDelegado += ObtenerListaCuenta;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CuentaSAP_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void txtCodigoSap_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cuentaEdicion = new CuentaSAPNuevo
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.CuentaSAP_Nuevo_Titulo }
                };
                MostrarCentrado(cuentaEdicion);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CuentaSAP_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var cuentaInfoSelecionado = (CuentaSAPInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (cuentaInfoSelecionado != null)
                {
                    var cuentaEdicion = new CuentaSAPEdicion(cuentaInfoSelecionado)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.CuentaSAP_Editar_Titulo }
                    };
                    MostrarCentrado(cuentaEdicion);
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CuentaSAP_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion Eventos
    }
}
