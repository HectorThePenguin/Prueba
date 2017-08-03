using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para NivelAlerta.xaml
    /// </summary>
    public partial class NivelAlerta 
    {
        /// <summary>
        /// Inicializa InitializeComponent
        /// </summary>
        public NivelAlerta()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento de Carga al entrar a Administrar Nivel Alertas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerListaNivelAlerta;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
                txtDescripcion.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.NivelAlerta_ErrorCargar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.NivelAlerta_ErrorCargar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }
       
        /// <summary>
        /// Se inicializa el contexto..
        /// Contenerdor de la clase
        /// </summary>
        private NivelAlertaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (NivelAlertaInfo)DataContext;
            }
            set { DataContext = value; }
        }
        
        /// <summary>
        /// Se toma el usuario el cual creo y se encuentra logeado.
        /// </summary>
        public void InicializaContexto()
        {

            Contexto = new NivelAlertaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };

        }
      
        /// <summary>
        /// Buscar la accion dentro de las registrados para evitar duplicados.
        /// </summary>
        public void Buscar()
        {
            ObtenerListaNivelAlerta(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene lista de nivel de alerta
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerListaNivelAlerta(int inicio, int limite)
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
                // se crean los filtros para le paginado  para posterior mandar llamar el SP.
                var nivelAlertaPL = new NivelAlertaPL();
                NivelAlertaInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<NivelAlertaInfo> resultadoInfo = nivelAlertaPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<NivelAlerta>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.NivelAlerta_ErrorBuscar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.NivelAlerta_ErrorBuscar,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }
        
        /// <summary>
        /// filtro de contexto 
        /// </summary>
        /// <returns></returns>
        private NivelAlertaInfo ObtenerFiltros()
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
        /// Evento del Boton Buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }
        
        /// <summary>
        /// se manda llamar la ventana de registro de nuevo nivel de alerta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var NivelAlertaNuevo = new NivelAlertaNuevo
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.NivelAlertaNuevo_titulo}
                };
                MostrarCentrado(NivelAlertaNuevo);
                Buscar();
            }

            catch (Exception Ex)
            {
                Logger.Error(Ex);

            }
        }

        /// <summary>
        /// Metodo para Editar el nivel de alerta clonando la informacion y alamacenandola en en una variable la cual se enviaria a la pantalla de edicion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var nivelSeleccionada = (NivelAlertaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (nivelSeleccionada != null)
                {
                    var nivelEdicion = new NivelAlertaNuevo(nivelSeleccionada)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.NivelAlertaEdicion_titulo }
                    };

                    MostrarCentrado(nivelEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.NivelAlerta_ErrorEditar, 
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }
        
        /// <summary>
        /// Reiniciar paginado
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }
    }
}

