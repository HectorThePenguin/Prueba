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
    /// Interaction logic for Jaula.xaml
    /// </summary>
    public partial class Jaula
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private JaulaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (JaulaInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Jaula()
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
                ucPaginacion.DatosDelegado += ObtenerListaJaula;
                ucPaginacion.Contexto = Contexto;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
                txtDescripcion.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Jaula_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.Jaula_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para buscar Jaula
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
                var jaulaEdicion =
                    new JaulaEdicion(new ProveedorInfo(), new MarcasInfo())
                        {
                            ucTitulo = {TextoTitulo = Properties.Resources.Jaula_Nuevo_Titulo}
                        };
                if (jaulaEdicion.existenMarcas)
                {
                    MostrarCentrado(jaulaEdicion);
                }            
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Jaula_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var jaulaInfoSelecionado = (JaulaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (jaulaInfoSelecionado != null)
                {
                    
                    var jaulaEdicion = new JaulaEdicion(jaulaInfoSelecionado)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.Jaula_Editar_Titulo }
                    };
                    MostrarCentrado(jaulaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Jaula_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TxtDescripcionPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConGuiones(e.Text);
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new JaulaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Proveedor = new ProveedorInfo
                    {
                        TipoProveedor = new TipoProveedorInfo()
                    },
            };
        }        

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaJaula(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaJaula(int inicio, int limite)
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
                var jaulaPL = new JaulaPL();
                JaulaInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo {Inicio = inicio, Limite = limite};
                ResultadoInfo<JaulaInfo> resultadoInfo = jaulaPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Jaula>();
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
        private JaulaInfo ObtenerFiltros()
        {
            JaulaInfo filtro;
            try
            {
                string descripcion = txtDescripcion.Text.Trim();
                filtro = new JaulaInfo
                             {
                                 JaulaID  =  0, 
                                 PlacaJaula = descripcion, 
                                 Proveedor =  new ProveedorInfo(),
                                 Activo = (EstatusEnum) cboEstatus.SelectedValue
                             };
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return filtro;
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion
    }
}
