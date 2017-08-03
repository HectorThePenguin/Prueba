using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Controles.Ayuda;
using SIE.Services.Info.Enums;
namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CuentaGastos.xaml
    /// </summary>
    public partial class CuentaGastos
    {
        #region Propiedades
         /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CuentaGastosInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (CuentaGastosInfo) DataContext;
                }
                set { DataContext = value; }
        }

       
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
         public CuentaGastos()
        {
            InitializeComponent();
            InicializaContexto();
            
        }
        #endregion
       

        #region Metodos

         /// <summary>
         /// Evento para buscar
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void Buscar_Click_1(object sender, RoutedEventArgs e)
         {
             Buscar();
         }
         /// <summary>
         /// Evento para Editar
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void BtnEditar_Click(object sender, RoutedEventArgs e)
         {
             var botonEditar = (Button)e.Source;
             try
             {
                 var cuentaGastosInfoSelecionado = (CuentaGastosInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                 if (cuentaGastosInfoSelecionado != null)
                 {
                     var cuentaGastosInfoSelecionadoEdicion = new CuentaGastosEdicion(cuentaGastosInfoSelecionado)
                     {
                         ucTitulo = { TextoTitulo = Properties.Resources.CuentasGasto_Edicion_Titulo }
                     };
                     MostrarCentrado(cuentaGastosInfoSelecionadoEdicion);
                     ReiniciarValoresPaginador();
                     Buscar();
                 }
             }
             catch (Exception ex)
             {
                 Logger.Error(ex);
                 SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Trampa_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
             }
         }


         /// <summary>
         /// Metodo para buscar por el filtro seleccionado
         /// </summary>
         private void Buscar()
         {
             ObtenerListaCuentaGasto(ucPaginacion.Inicio, ucPaginacion.Limite);
         }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CuentaGastosInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo(),
                CuentaSAP = new CuentaSAPInfo(),
                Costos = new CostoInfo(),
                Activo = EstatusEnum.Activo
            };
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
        }

      
        /// <summary>
        /// Obtiene la lista de cuenta de gastos de ganado
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerListaCuentaGasto(int inicio, int limite)
        {
            try
            {
                var cuentaGastoPL = new CuentaGastosPL();
                CuentaGastosInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<CuentaGastosInfo> resultadoInfo = cuentaGastoPL.ObtenerPorPagina(pagina, filtros);
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
                    gridDatos.ItemsSource = new List<CuentaGastosInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Cuenta_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Cuenta_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private CuentaGastosInfo ObtenerFiltros()
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


        #endregion
        #region Eventos
        /// <summary>
        /// Reinicia el paginado
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
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
                var cuentaGastoEdicion = new CuentaGastosEdicion()
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.CuentaValor_Nuevo_Titulo }
                };
                MostrarCentrado(cuentaGastoEdicion);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CuentaValor_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento de carga de inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlBase_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerListaCuentaGasto;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Cuenta_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Cuenta_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        
       
        #endregion
        

       

       

        
    }
}
