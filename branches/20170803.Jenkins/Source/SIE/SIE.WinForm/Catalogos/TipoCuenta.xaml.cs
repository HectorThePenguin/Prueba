using System;                      
using System.Collections.Generic;
using System.Windows;              
using System.Windows.Controls;
using SIE.Base.Exepciones;         
using SIE.Base.Infos;              
using SIE.Base.Log;                
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.WinForm.Auxiliar;        
using SuKarne.Controls.Enum;       
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.BL; 

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for TipoCuenta.xaml
    /// </summary>
    public partial class TipoCuenta
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TipoCuentaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (TipoCuentaInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public TipoCuenta()
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
                ucPaginacion.DatosDelegado += ObtenerListaTipoCuenta;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoCuenta_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoCuenta_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
          Buscar();
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
                var tipoCuentaEdicion = new TipoCuentaEdicion
                {
                    ucTitulo = {TextoTitulo = Properties.Resources.TipoCuenta_Nuevo_Titulo }
                };
                MostrarCentrado(tipoCuentaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoCuenta_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button) e.Source;
            try
            {
                var tipoCuentaInfoSelecionado = (TipoCuentaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (tipoCuentaInfoSelecionado != null)
                {
                    var tipoCuentaEdicion = new TipoCuentaEdicion(tipoCuentaInfoSelecionado)
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.TipoCuenta_Editar_Titulo }
                        };
                    MostrarCentrado(tipoCuentaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.TipoCuenta_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
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
        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new TipoCuentaInfo
            {
                Descripcion = string.Empty,
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Activo = EstatusEnum.Activo
            };
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaTipoCuenta(ucPaginacion.Inicio, ucPaginacion.Limite);
        }        

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaTipoCuenta(int inicio, int limite)
        {
            try
            {
                using (var tipoCuentaBL = new TipoCuentaBL())
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
                    var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                    ResultadoInfo<TipoCuentaInfo> resultadoInfo = tipoCuentaBL.ObtenerPorPagina(pagina, Contexto);
                    if (resultadoInfo != null && resultadoInfo.Lista != null &&
                        resultadoInfo.Lista.Count > 0)
                    {
                        gridDatos.ItemsSource = resultadoInfo.Lista;
                        ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                    }
                    else
                    {
                        ucPaginacion.TotalRegistros = 0;
                        gridDatos.ItemsSource = new List<TipoCuenta>();
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCuenta_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCuenta_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}
