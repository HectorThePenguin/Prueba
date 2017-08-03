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
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Button = System.Windows.Controls.Button;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Interaction logic for ProgramacionEmbarque.xaml
    /// </summary>
    public partial class ProgramacionEmbarques
    {
        #region Atributos

        /// <summary>
        /// Control para la ayuda de Organización de Origen
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrigen;

        /// <summary>
        /// Control para la ayuda de Organización de Destino
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaDestino;

        #endregion Atributos

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProgramacionEmbarques()
        {
            InitializeComponent();
            AgregarControles();
            LimpiarCampos();
            CargarComboTiposEmbarque();
            Buscar();
        }

        #endregion Constructor

        #region Eventos

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Evento para buscar los datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Buscar();
                if (DgEmbarques.Items.Count == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ProgramacionEmbarque_NoExistenDatos, MessageBoxButton.OK,
                                      MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProgramacionEmbarque_ErrorNuevo, MessageBoxButton.OK,
                                  MessageImage.Error);
            }

        }

        /// <summary>
        /// Evento para invocar la pantalla de registro de programación de embarque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevoClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistroProgramacion(0, 0);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProgramacionEmbarque_ErrorNuevo, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiarClick(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            ObtenerListaProgramacionInicial(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fechas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
                e.Handled = true;
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento para editar un registro llamando a la pantalla RegistroProgramacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditarClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            dynamic detalle = btn.CommandParameter;

            if (detalle != null)
            {
                RegistroProgramacion(detalle.FolioEmbarque, detalle.OrganizacionID);
            }
        }

        /// <summary>
        /// Evento para asignar lasdependencias a la organización origen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbTipoOrganizacionSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AsignaDependenciasAyudaOrganizacionOrigen();
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar la entrada de solo números
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Método para agregar los controles de ayuda
        /// </summary>
        private void AgregarControles()
        {
            AgregarAyudaOrigen();
            AgregarAyudaDestino();
            AgregarPaginador();
        }

        /// <summary>
        /// Método que valida las reglas de negocio de las organizaciones de Destino
        /// </summary>
        private bool ValidaOrigenYdestino()
        {
            if (string.IsNullOrWhiteSpace(skAyudaOrigen.Clave))
            {
                return true;
            }


            if (skAyudaOrigen.Clave.Equals(skAyudaDestino.Clave))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistroProgramacionEmbarque_DestinoDuplicado,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaDestino.LimpiarCampos();
                skAyudaDestino.AsignarFoco();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Método que valida las reglas de negocio de las organizaciones de Destino
        /// </summary>
        private void ValidaDestino()
        {
            ValidaOrigenYdestino();
        }

        /// <summary>
        /// Métodp para cargar el combo TiposEmbarque con los tipos de organización
        /// </summary>
        private void CargarComboTiposEmbarque()
        {
            var tipoOrganizacionPL = new TipoOrganizacionPL();

            var tipoOrganizacion = new TipoOrganizacionInfo
                                       {
                                           TipoOrganizacionID = 0,
                                           Descripcion = Properties.Resources.cbo_Seleccione,
                                       };

            IList<TipoOrganizacionInfo> listaTipoOrganizacion = tipoOrganizacionPL.ObtenerTodos(EstatusEnum.Activo);
            listaTipoOrganizacion.Insert(0, tipoOrganizacion);
            CmbTipoOrganizacion.ItemsSource = listaTipoOrganizacion;
            CmbTipoOrganizacion.SelectedItem = tipoOrganizacion;
        }

        /// <summary>
        /// Configura la ayuda para ligarlo con la organización origen
        /// </summary>
        private void AgregarAyudaOrigen()
        {
            skAyudaOrigen = new SKAyuda<OrganizacionInfo>(160, false, new OrganizacionInfo()
                                                        , "PropiedadClaveProgramacionEmbarque"
                                                        , "PropiedadDescripcionProgramacionEmbarque", true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.Origen_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Origen_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Origen_Busqueda,
                MensajeAgregar = Properties.Resources.Origen_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrigen,
                TituloPantalla = Properties.Resources.BusquedaOrigen_Titulo,
            };
            skAyudaOrigen.AsignaTabIndex(3);

            SplAyudaOrganizacionOrigen.Children.Clear();
            SplAyudaOrganizacionOrigen.Children.Add(skAyudaOrigen);

        }

        /// <summary>
        /// Configura la ayuda para ligarlo con la organización destino
        /// </summary>
        private void AgregarAyudaDestino()
        {
            skAyudaDestino = new SKAyuda<OrganizacionInfo>(160, false, new OrganizacionInfo()
                                                        , "PropiedadClaveProgramacionEmbarque"
                                                        , "PropiedadDescripcionProgramacionEmbarque", true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.Destino_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Destino_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Destino_Busqueda,
                MensajeAgregar = Properties.Resources.Destino_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaDestino,
                TituloPantalla = Properties.Resources.BusquedaDestino_Titulo,
            };
            skAyudaDestino.LlamadaMetodos += ValidaDestino;
            skAyudaDestino.ObtenerDatos += CargarOrganizacionesDestino;

            skAyudaDestino.AsignaTabIndex(4);
            SplAyudaOrganizacionDestino.Children.Clear();
            SplAyudaOrganizacionDestino.Children.Add(skAyudaDestino);
        }

        /// <summary>
        /// Configura los eventos del paginador
        /// </summary>
        private void AgregarPaginador()
        {
            ucPaginacion.DatosDelegado += ObtenerListaProgramacion;
            ucPaginacion.AsignarValoresIniciales();
        }

        /// <summary>
        ///  Obtiene la lista para mostrar en el grid
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerListaProgramacion(int inicio, int limite)
        {
            try
            {
                var embarquePL = new EmbarquePL();
                FiltroEmbarqueInfo filtros = ObtenerFiltrosInicial();
                if (ucPaginacion.Contexto == null)
                {
                    ucPaginacion.Contexto = filtros;
                }
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(filtros, ucPaginacion.Contexto) &&
                                            ucPaginacion.CompararObjetos(filtros, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<EmbarqueInfo> resultadoEmbarqueInfo = embarquePL.ObtenerPorPagina(pagina, filtros);

                if (resultadoEmbarqueInfo != null && resultadoEmbarqueInfo.Lista != null &&
                    resultadoEmbarqueInfo.Lista.Count > 0)
                {
                    var source = (from item in resultadoEmbarqueInfo.Lista
                                  let escala = item.ListaEscala.FirstOrDefault()
                                  where escala != null
                                  select new
                                             {
                                                 item.FolioEmbarque,
                                                 TipoOrganizacion =
                                                escala.OrganizacionOrigen.TipoOrganizacion.Descripcion,
                                                 OrganizacionOrigen = escala.OrganizacionOrigen.Descripcion,
                                                 OrganizacionDestino = escala.OrganizacionDestino.Descripcion,
                                                 escala.FechaSalida,
                                                 escala.FechaLlegada,
                                                 TipoEmbarque = item.TipoEmbarque.Descripcion,
                                                 escala.Chofer.NombreCompleto,
                                                 escala.Camion.PlacaCamion,
                                                 Estatus = ((Estatus)item.Estatus).ToString(),
                                                 escala.OrganizacionDestino.OrganizacionID
                                             }
                                 ).ToList();

                    DgEmbarques.ItemsSource = source;
                    ucPaginacion.TotalRegistros = resultadoEmbarqueInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    DgEmbarques.ItemsSource = new List<EmbarqueInfo>();
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
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaProgramacionInicial(int inicio, int limite)
        {
            try
            {
                var embarquePL = new EmbarquePL();
                FiltroEmbarqueInfo filtros = ObtenerFiltrosInicial();

                if (filtros != null)
                {
                    var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                    ResultadoInfo<EmbarqueInfo> resultadoEmbarqueInfo = embarquePL.ObtenerPorPagina(pagina, filtros);

                    if (resultadoEmbarqueInfo != null && resultadoEmbarqueInfo.Lista != null &&
                        resultadoEmbarqueInfo.Lista.Count > 0)
                    {
                        var source = (from item in resultadoEmbarqueInfo.Lista
                                      let escala = item.ListaEscala.FirstOrDefault()
                                      where escala != null
                                      select new
                                      {
                                          item.FolioEmbarque,
                                          TipoOrganizacion =
                                          escala.OrganizacionOrigen.TipoOrganizacion.Descripcion,
                                          OrganizacionOrigen = escala.OrganizacionOrigen.Descripcion,
                                          OrganizacionDestino = escala.OrganizacionDestino.Descripcion,
                                          escala.FechaSalida,
                                          escala.FechaLlegada,
                                          TipoEmbarque = item.TipoEmbarque.Descripcion,
                                          escala.Chofer.NombreCompleto,
                                          escala.Camion.PlacaCamion,
                                          Estatus = ((Estatus)item.Estatus).ToString(),
                                          escala.OrganizacionDestino.OrganizacionID
                                      }
                                     ).ToList();

                        DgEmbarques.ItemsSource = source;
                        ucPaginacion.TotalRegistros = resultadoEmbarqueInfo.TotalRegistros;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProgramacionEmbarque_SinProgramacion, MessageBoxButton.OK,
                                          MessageImage.Warning);

                        ucPaginacion.TotalRegistros = 0;
                        DgEmbarques.ItemsSource = new List<EmbarqueInfo>();
                    }
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
        private FiltroEmbarqueInfo ObtenerFiltrosInicial()
        {
            try
            {
                var tipoOrganizacion = (TipoOrganizacionInfo)CmbTipoOrganizacion.SelectedItem ??
                                       new TipoOrganizacionInfo();

                int folioEmbarque = Extensor.ValorEntero(TxtFolioEmbarque.Text);
                int organizacionOrigenId = Extensor.ValorEntero(skAyudaOrigen.Clave);
                int organizacionDestinoId = Extensor.ValorEntero(skAyudaDestino.Clave);
                DateTime? fechaSalida = null;
                DateTime? fechaLlegada = null;

                if (DtpFechaSalida.SelectedDate.HasValue)
                {
                    fechaSalida = DtpFechaSalida.SelectedDate.Value;
                }

                if (DtpFechaLlegada.SelectedDate.HasValue)
                {
                    fechaLlegada = DtpFechaLlegada.SelectedDate.Value;
                }

                var filtro = new FiltroEmbarqueInfo
                {
                    FolioEmbarque = folioEmbarque,
                    OrganizacionOrigenID = organizacionOrigenId,
                    OrganizacionDestinoID = organizacionDestinoId,
                    TipoOrganizacionOrigenID = tipoOrganizacion.TipoOrganizacionID,
                    FechaSalida = fechaSalida,
                    FechaLlegada = fechaLlegada,
                    Estatus = Estatus.Pendiente.GetHashCode(),
                };

                return filtro;
            }
            catch
                (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Configura la ayuda organización origen para que tenga dependencia con la el tipo de movimiento
        /// </summary>
        /// <returns></returns>
        private void AsignaDependenciasAyudaOrganizacionOrigen()
        {
        }

        /// <summary>
        /// Carga la organización destino dependiendo del tipo de movimiento
        /// </summary>
        /// <returns></returns>
        private void CargarOrganizacionesDestino(string clave)
        {
            bool destino = skAyudaOrigen.Clave == clave;

            if (destino)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProgramacionEmbarque_DestinoIgualOrigen, MessageBoxButton.OK,
                                  MessageImage.Stop);

                skAyudaDestino.LimpiarCampos();
            }
        }

        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos()
        {
            TxtFolioEmbarque.Text = string.Empty;
            DgEmbarques.ItemsSource = null;
            CmbTipoOrganizacion.SelectedIndex = 0;
            AgregarAyudaOrigen();
            AgregarAyudaDestino();
            LimpiarFecha(DtpFechaSalida);
            LimpiarFecha(DtpFechaLlegada);
            ucPaginacion.AsignarValoresIniciales();
        }

        /// <summary>
        /// invoca a la pantalla RegistroProgramacionEmbarques para dar de alta un nuevo folio de embarque o para editar
        /// </summary>
        /// <param name="folioEmbarque"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        private void RegistroProgramacion(int folioEmbarque, int organizacionId)
        {
            var programacion = folioEmbarque == 0
                                   ? new RegistroProgramacionEmbarques()
                                   : new RegistroProgramacionEmbarques(folioEmbarque, organizacionId);
            MostrarCentrado(programacion);
            ReiniciarValoresPaginador();
            Buscar();
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Método que habilita o desabilita los controles de fecha
        /// </summary>
        private void LimpiarFecha(DatePicker fecha)
        {
            fecha.SelectedDate = null;
        }

        /// <summary>
        /// Método que habilita o desabilita los controles de fecha
        /// </summary>
        private void Buscar()
        {
            try
            {
                if (ValidaOrigenYdestino())
                {
                    ObtenerListaProgramacion(ucPaginacion.Inicio, ucPaginacion.Limite);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProgramacionEmbarque_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProgramacionEmbarque_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        #endregion Métodos
    }
}
