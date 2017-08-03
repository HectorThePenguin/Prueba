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

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for SupervisorEnfermeria.xaml
    /// </summary>
    public partial class SupervisorEnfermeria
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private SupervisorEnfermeriaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (SupervisorEnfermeriaInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Propiedad para validar si cambia la organización
        /// del contexto
        /// </summary>
        private int organizacionID = 0;

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public SupervisorEnfermeria()
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
                ucPaginacion.DatosDelegado += ObtenerListaSupervisorEnfermeria;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;

                skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
                skAyudaEnfermeria.ObjetoNegocio = new EnfermeriaPL();
                skAyudaOperador.ObjetoNegocio = new OperadorPL();

                //ayuda Organización
                skAyudaOrganizacion.AyudaConDatos += (o, args) => AyudaConDatos();
                skAyudaOrganizacion.AyudaLimpia += (o, args) => AyudaLimpia();

                skAyudaEnfermeria.PuedeBuscar = PuedeBuscar;
                skAyudaOperador.PuedeBuscar = PuedeBuscar;

                Buscar();

            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SupervisorEnfermeria_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SupervisorEnfermeria_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            AsignaValoresVaciosGrid();

            if (PuedeBuscar())
            {
                Buscar();
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
                var supervisorEnfermeriaEdicion = new SupervisorEnfermeriaEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.SupervisorEnfermeria_Nuevo_Titulo }
                };
                MostrarCentrado(supervisorEnfermeriaEdicion);
                ReiniciarValoresPaginador();
                InicializaContexto();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SupervisorEnfermeria_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var supervisorEnfermeriaInfoSelecionado = (SupervisorEnfermeriaInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (supervisorEnfermeriaInfoSelecionado != null)
                {
                    supervisorEnfermeriaInfoSelecionado.Organizacion = new OrganizacionInfo
                                                                           {
                                                                               OrganizacionID = Contexto.Organizacion.OrganizacionID,
                                                                               Descripcion = Contexto.Organizacion.Descripcion
                                                                           };

                    var supervisorEnfermeriaEdicion = new SupervisorEnfermeriaEdicion(supervisorEnfermeriaInfoSelecionado)
                        {
                            ucTitulo = { TextoTitulo = Properties.Resources.SupervisorEnfermeria_Editar_Titulo }
                        };
                    MostrarCentrado(supervisorEnfermeriaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SupervisorEnfermeria_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var organizacionPL = new OrganizacionPL();
            OrganizacionInfo organizacion = organizacionPL.ObtenerPorID(organizacionID);

            organizacion.TipoOrganizacion = new TipoOrganizacionInfo
                {
                    TipoOrganizacionID =
                        Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode()
                };

            Contexto = new SupervisorEnfermeriaInfo
            {
                Organizacion = organizacion,
                Enfermeria = InicializaEnfermeria(organizacion),
                Operador = InicializaOpeador(organizacion),
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        private EnfermeriaInfo InicializaEnfermeria(OrganizacionInfo organizacion)
        {
            return new EnfermeriaInfo
                       {
                           OrganizacionInfo = organizacion
                       };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        private OperadorInfo InicializaOpeador(OrganizacionInfo organizacion)
        {
            return new OperadorInfo
                       {
                           Rol = new RolInfo(),
                           Organizacion = organizacion
                       };

        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaSupervisorEnfermeria(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private SupervisorEnfermeriaInfo ObtenerFiltros()
        {
            try
            {
                Contexto.EnfermeriaID = Contexto.Enfermeria.EnfermeriaID;
                Contexto.Enfermeria.OrganizacionInfo = Contexto.Organizacion;
                Contexto.OperadorID = Contexto.Operador.OperadorID;

                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaSupervisorEnfermeria(int inicio, int limite)
        {
            try
            {
                if (Contexto.Organizacion.OrganizacionID == 0)
                {
                    AsignaValoresVaciosGrid();
                    return;
                }

                var supervisorEnfermeriaPL = new SupervisorEnfermeriaBL();
                SupervisorEnfermeriaInfo filtros = ObtenerFiltros();

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
                ResultadoInfo<SupervisorEnfermeriaInfo> resultadoInfo = supervisorEnfermeriaPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    AsignaValoresVaciosGrid();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SupervisorEnfermeria_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SupervisorEnfermeria_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Asigna la organización  a las  ayudas que dependan
        /// del campo organización.
        /// </summary>
        private void AyudaConDatos()
        {
            var organizacion =
                skAyudaOrganizacion.Contexto as OrganizacionInfo;

            skAyudaEnfermeria.LimpiarCampos();
            skAyudaOperador.LimpiarCampos();

            Contexto.Enfermeria = InicializaEnfermeria(organizacion);
            Contexto.Operador = InicializaOpeador(organizacion);
            BindearDependencias();
        }

        /// <summary>
        /// Limpia los valores de las ayudas que dependen de la organización.
        /// </summary>
        private void AyudaLimpia()
        {
            if (!skAyudaOrganizacion.BusquedaActivada)
            {
                int claveOrganizacion = string.IsNullOrWhiteSpace(skAyudaOrganizacion.Clave)
                                            ? 0
                                            : int.Parse(skAyudaOrganizacion.Clave);

                if (claveOrganizacion != organizacionID)
                {
                    Contexto.Enfermeria = InicializaEnfermeria(new OrganizacionInfo());
                    Contexto.Operador = InicializaOpeador(new OrganizacionInfo());
                    BindearDependencias();
                }
            }
        }

        /// <summary>
        /// Refresca los valores de las ayudas que tienen
        /// dependencia con el campo organización.
        /// </summary>
        private void BindearDependencias()
        {
            skAyudaEnfermeria.Contexto = Contexto.Enfermeria;
            skAyudaOperador.Contexto = Contexto.Operador;
        }

        /// <summary>
        /// Valida que se haya capturado una organización.
        /// </summary>
        /// <returns></returns>
        private bool PuedeBuscar()
        {
            bool puede = Contexto.Organizacion != null && Contexto.Organizacion.OrganizacionID > 0;
            if (!puede)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SupervisorEnfermeriaEdicion_MsgOrganizacicionRequerida, MessageBoxButton.OK, MessageImage.Warning);
            }
            return puede;
        }

        /// <summary>
        /// Asigna los valores por default al grid
        /// </summary>
        private void AsignaValoresVaciosGrid()
        {
            ucPaginacion.TotalRegistros = 0;
            gridDatos.ItemsSource = new List<SupervisorEnfermeria>();
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

