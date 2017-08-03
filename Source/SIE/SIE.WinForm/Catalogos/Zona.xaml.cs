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
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para Zona.xaml
    /// </summary>
    public partial class Zona
    {
        #region CONSTRUCTORES

        public Zona()
        {
            InitializeComponent();
            InicializaContexto();
        }

        #endregion CONSTRUCTORES

        #region PROPIEDADES

        private ZonaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ZonaInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion PROPIEDADES

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
                InicializaContexto();
                skAyudaPais.ObjetoNegocio = new PaisBL();
                skAyudaPais.Contexto = Contexto.Pais;

                ucPaginacion.DatosDelegado += ObtenerZonas;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Zona_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Zona_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetras(e.Text);
        }

        /// <summary>
        /// Evento para buscar Zonas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerZonas(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Evento para editar un registro 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var zonaInfoSelecionado = (ZonaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (zonaInfoSelecionado != null)
                {
                    var zonaEdicion = new ZonaEdicion(zonaInfoSelecionado)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.Zona_Editar_Titulo }
                    };
                    MostrarCentrado(zonaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Zona_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
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
                var zonaEdicion = new ZonaEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.Zona_Nuevo_Titulo }
                };
                MostrarCentrado(zonaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Zona_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }

        }


        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ZonaInfo { Activo = EstatusEnum.Activo };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerZonas(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerZonas(int inicio, int limite)
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

                var zonaBL = new ZonaBL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ZonaInfo filtros = ObtenerFiltros();
                ResultadoInfo<ZonaInfo> resultadoInfo = zonaBL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Zona>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Zona_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Zona_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ZonaInfo ObtenerFiltros()
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

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion METODOS

    }
}
