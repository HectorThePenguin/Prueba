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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para Marcas.xaml
    /// </summary>
    public partial class Marcas
    {

        #region Propiedades

        /// <summary>
        /// Contexto de la interfaz.
        /// </summary>
        private MarcasInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (MarcasInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor simple de clase
        /// </summary>
        public Marcas()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta al seleccion el botón nuevo
        /// </summary>
        /// <param name="sender"> Parametro estandar sender </param>
        /// <param name="e"> Parametro estandar e </param>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var marcasEdicion = new MarcasEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.MarcasEdicion_Titulo_Nuevo }
                };
                MostrarCentrado(marcasEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Banco_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando la interfaz se ha cargado completamente
        /// </summary>
        /// <param name="sender"> Parametro estandar sender </param>
        /// <param name="e"> Parametro estandar e </param>
        private void Marcas_OnLoaded(object sender, RoutedEventArgs e)
        {
            CargarCboEstatus();
            ucPaginacion.DatosDelegado += ObtenerListaMarcas;
            ucPaginacion.AsignarValoresIniciales();
            Buscar();
        }

        /// <summary>
        /// Evento que se ejecuta al seleccionar el botón buscar.
        /// </summary>
        /// <param name="sender"> Parametro estandar sender </param>
        /// <param name="e"> Parametro estandar e </param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Evento que se ejecuta al seleccionar el botón editar.
        /// </summary>
        /// <param name="sender"> Parametro estandar sender </param>
        /// <param name="e"> Parametro estandar e </param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var marcaInfoSelecionado = (MarcasInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                var marcasEdicion = new MarcasEdicion(marcaInfoSelecionado)
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.MarcasEdicion_Titulo_Edicion }
                };
                MostrarCentrado(marcasEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Banco_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
        
        #region Metodos

        /// <summary>
        /// Método que inicializa el contexto de la interfaz.
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new MarcasInfo();
        }

        /// <summary>
        /// Método que obtiene la lista de todos las marcas registrados con ayuda el paginador
        /// </summary>
        /// <param name="inicio"> Número de inicio para la busqueda de registro </param>
        /// <param name="limite"> Tope de busqueda de registros </param>
        private void ObtenerListaMarcas(int inicio, int limite)
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

                var marcasPL = new MarcasPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                var filtro = new MarcasInfo() { Activo = Convert.ToBoolean(cboEstatus.SelectedItem) ? EstatusEnum.Activo : EstatusEnum.Inactivo, Descripcion = txtDescripcion.Text };
                ResultadoInfo<MarcasInfo> resultadoInfo = marcasPL.ObtenerPorPagina(pagina, filtro);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<Marcas>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Marcas_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Marcas_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Método que busca la lista de las marcas registradas.
        /// </summary>
        private void Buscar()
        {
            ObtenerListaMarcas(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Método que reinicia los valores de la interfaz para una nueva busqueda.
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
            cboEstatus.SelectedItem = EstatusEnum.Activo;
            txtDescripcion.Text = string.Empty;
        }

        /// <summary>
        /// Método que carga el combo de estados para mostrar en pantalla
        /// </summary>
        private void CargarCboEstatus()
        {
            IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
            cboEstatus.ItemsSource = estatusEnums;
            cboEstatus.SelectedItem = EstatusEnum.Activo;
        }

        private void SoloLetrasYNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        #endregion
    }
}
