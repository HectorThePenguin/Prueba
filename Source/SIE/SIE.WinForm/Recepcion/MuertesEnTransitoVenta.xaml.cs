using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Controles.Ayuda;
using SIE.Services.Servicios.PL;
using System.IO;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Modelos;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Lógica de interacción para MuertesEnTransitoVenta.xaml
    /// </summary>
    public partial class MuertesEnTransitoVenta
    {

     #region Atributos

        SKAyuda <MuertesEnTransitoInfo> skAyudaFolioEntrada;
        
        MuertesEnTransitoInfo muertesEnTransitoInfoSeleccionado;
        //Lista de animales que se muestra en el grid de Aretes.
        List<AnimalInfo> Animales = new List<AnimalInfo>();
        //Lista de animales que pertenecen a la entrada de ganado.
        List<AnimalInfo> AnimalesDelCorral = new List<AnimalInfo>();
        //Contiene la organizacion del usuario logueadeo.
        int organizacionID;
        //Contiene el Usuario Logueado
        int usuarioID;
        //Lleva el conteo para los aretes genericos por entrada de ganado.
        int countAreteGenerico;
        

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FiltroMuerteTransitoVenta Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroMuerteTransitoVenta)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto = new FiltroMuerteTransitoVenta
                       {
                           Proveedor = new ProveedorInfo(),
                           Cliente = new ClienteInfo()
                       };
        }

      #endregion

      #region Constructor

        public MuertesEnTransitoVenta()
        {

            
            InitializeComponent();
            ClientePL clientepl = new ClientePL();
            MuertesEnTransitoPL muertesPl = new MuertesEnTransitoPL();
            if (ValidarInicioProceso())
            {
                organizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]);
                usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                AgregarAyudaFolioEntrada();
                dgAretes.ItemsSource = Animales;
                InicializaContexto();
                skAyudaCliente.ObjetoNegocio = new ClientePL();
                InicializarFormulario();
                skAyudaCliente.EsBindeable = false;
                skAyudaCliente.AyudaConDatos += (sender, args) =>
                                                   {
                                                       if (muertesEnTransitoInfoSeleccionado != null)
                                                       {
                                                           Contexto.Proveedor.EmbarqueID = muertesEnTransitoInfoSeleccionado.EmbarqueID;
                                                           
                                                           ClienteInfo cliente = clientepl.ObtenerClientePorCliente(Contexto.Cliente);
                                                           
                                                           if (cliente != null)
                                                           {
                                                               if (cliente.Activo == EstatusEnum.Activo)
                                                               {
                                                                   Contexto.Cliente = cliente;
                                                                   muertesEnTransitoInfoSeleccionado.Cliente = Contexto.Cliente;
                                                                   skAyudaCliente.Contexto = cliente;
                                                                   skAyudaCliente.txtClave.Text = cliente.CodigoSAP;
                                                                   skAyudaCliente.txtDescripcion.Text = cliente.Descripcion;
                                                               }
                                                               else
                                                               {
                                                                   SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Cliente_ClienteNoActivo, MessageBoxButton.OK, MessageImage.Error);
                                                                   skAyudaCliente.Contexto = new ClienteInfo();
                                                                   skAyudaCliente.txtClave.Text = string.Empty;
                                                                   skAyudaCliente.txtDescripcion.Text = string.Empty;
                                                               }
                                                           }
                                                       }
                                                   };
                skAyudaFolioEntrada.AsignarFoco();
            }
            
        }

      #endregion

      #region Metodos

        private bool ValidarInicioProceso()
        {
            bool resultado = false;
            ClientePL clientepl = new ClientePL();
            MuertesEnTransitoPL muertesPl = new MuertesEnTransitoPL();
            string mensaje = string.Empty;
            int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
            if (clientepl.ObtenerTotalClientesActivos() > 0)
            {
                if (muertesPl.ObtenerTotalFoliosValidos(organizacionId) > 0)
                {
                    resultado = true;
                }
                else
                {
                    mensaje = Properties.Resources.MuertesEnTransito_NoExistenFoliosMuerteGanadoRegistrados;
                }
            }
            else
            {
                mensaje = Properties.Resources.MuertesEnTransito_NoExistenClientesActivos;
            }

            if(!resultado)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje, MessageBoxButton.OK, MessageImage.Error);
                deshabilitaControles();
            }
            return resultado;
        }
        /// <summary>
        /// Inicializa los valores de inicio y limpia el formulario.
        /// </summary>
        private void InicializarFormulario()
        {
            muertesEnTransitoInfoSeleccionado = null;
            Animales.Clear();
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtOrigen.Text = string.Empty;
            txtTipoOrigen.Text = string.Empty;
            skAyudaCliente.LimpiarCampos();
            txtCorral.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtCabezas.Text = string.Empty;
            txtMuertesEnTransito.Text = string.Empty;
            txtArete.Text = string.Empty;
            txtArete.IsEnabled = true;
            chkSinArete.IsChecked = false;
            skAyudaFolioEntrada.LimpiarCampos();
            skAyudaCliente.IsEnabled = false;
            dgAretes.Items.Refresh();
            InicializarAyudaFolioEntrada();
        }

        private void deshabilitaControles()
        {
            muertesEnTransitoInfoSeleccionado = null;
            Animales.Clear();
            txtFecha.IsEnabled = false;
            txtOrigen.IsEnabled = false;
            txtTipoOrigen.IsEnabled = false;
            skAyudaCliente.IsEnabled = false;
            txtCorral.IsEnabled = false;
            txtLote.IsEnabled = false;
            txtCabezas.IsEnabled = false;
            txtMuertesEnTransito.IsEnabled = false;
            txtArete.IsEnabled = false;
            chkSinArete.IsEnabled = false;
            //skAyudaFolioEntrada.IsEnabled = false;
            skAyudaCliente.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            btnAgregarActualizar.IsEnabled = false;
            btnLimpiar.IsEnabled = false;
        }

        /// <summary>
        /// Agrega la ayuda para el campo Folio de Entrada.
        /// </summary>
        private void AgregarAyudaFolioEntrada()
        {
            try
            {
                skAyudaFolioEntrada = new SKAyuda<MuertesEnTransitoInfo>(0,
                    false,
                    new MuertesEnTransitoInfo
                    {
                        Origen = string.Empty, // Se almacenara el Folio Entrada a Filtrar
                        OrganizacionID = organizacionID
                    },
                    "PropiedadFolioEntrada",
                    "PropiedadDescripcion",
                    true,
                    133,
                    true)
                {
                    AyudaPL = new MuertesEnTransitoPL(),
                    MensajeClaveInexistente = Properties.Resources.MuertesEnTransito_msgAyudaFolioEntrada_ClaveInvalida,
                    MensajeBusquedaCerrar = Properties.Resources.MuertesEnTransito_msgAyudaFolioEntrada_Cerrar,
                    MensajeBusqueda = Properties.Resources.MuertesEnTransito_msgAyudaFolioEntrada_Buscar,
                    MensajeAgregar = Properties.Resources.MuertesEnTransito_msgAyudaFolioEntrada_Agregar,
                    TituloEtiqueta = Properties.Resources.MuertesEnTransito_lblFolioEntrada,
                    TituloPantalla = Properties.Resources.MuertesEnTransito_msgAyudaFolioEntrada_Titulo,
                    MetodoPorDescripcion = "ObtenerPorPagina"
                };

                skAyudaFolioEntrada.ObtenerDatos += ObtenerDatosAyudaFolioEntrada;
                skAyudaFolioEntrada.LlamadaMetodosNoExistenDatos += LimpiarTodoAyudaFolioEntrada;

                skAyudaFolioEntrada.AsignaTabIndex(0);
                splAyudaFolioEntrada.Children.Clear();
                splAyudaFolioEntrada.Children.Add(skAyudaFolioEntrada);
                skAyudaFolioEntrada.TabIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            } 
        }

        


        /// <summary>
        /// Inicializa los valores de la ayuda.
        /// </summary>
        private void InicializarAyudaFolioEntrada()
        {
            skAyudaFolioEntrada.Info =  new MuertesEnTransitoInfo
            {
                Origen = string.Empty, // Se almacenara el Folio Entrada a Filtrar
                OrganizacionID = organizacionID 
            };
        }

        /// <summary>
        /// Obtiene los datos del elemento seleccionado en la ayuda Folio de Entrada.
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatosAyudaFolioEntrada(string filtro)
        {
            List<string> aretes = new List<string>();
            muertesEnTransitoInfoSeleccionado = skAyudaFolioEntrada.Info;
            InicializarAyudaFolioEntrada();
            skAyudaCliente.LimpiarCampos();
            MuertesEnTransitoPL muertePl = new MuertesEnTransitoPL();
            ValidacionesFolioVentaMuerte validaciones = new ValidacionesFolioVentaMuerte();
            if (muertesEnTransitoInfoSeleccionado != null && muertesEnTransitoInfoSeleccionado.EntradaGanadoID != 0)
            {
                foreach (AnimalInfo arete in dgAretes.Items)
                {
                    aretes.Add(arete.Arete);
                }
                validaciones = muertePl.ValidarFolio(muertesEnTransitoInfoSeleccionado.FolioEntrada, organizacionID, aretes);
                if(!validaciones.RegistroCondicionMuerte)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuertesEnTransito_FolioSinCondicionMuerte,
                       MessageBoxButton.OK, MessageImage.Warning);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                }
                else if (validaciones.FolioConMuertesRegistradas)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuertesEnTransito_FolioConRegistroMuertes,
                       MessageBoxButton.OK, MessageImage.Warning);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                }
                else if (muertesEnTransitoInfoSeleccionado.MuertesTransito == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuertesEnTransito_msgNoTieneMuertes,
                       MessageBoxButton.OK, MessageImage.Warning);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                }
                else if(muertesEnTransitoInfoSeleccionado.MuertesRegistradas != 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuertesEnTransito_msgMuertesRegistradas,
                       MessageBoxButton.OK, MessageImage.Warning);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                }
                else if(!validaciones.EstatusLote)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_LoteInactivo,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                }
                else if(validaciones.Cabezas == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_LoteSinCabezas,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                }
                else
                {
                    muertesEnTransitoInfoSeleccionado.OrganizacionID = organizacionID;
                    txtArete.IsEnabled = true;
                    chkSinArete.IsChecked = false;
                    if (muertesEnTransitoInfoSeleccionado.TipoOrigenID == (int)TipoOrganizacion.CompraDirecta)
                    {
                        txtArete.IsEnabled = false;
                        chkSinArete.IsChecked = true;
                    }
                    else
                    {
                        AnimalesDelCorral = new List<AnimalInfo>();
                        var muertesEnTransitoPl = new MuertesEnTransitoPL();
                        AnimalesDelCorral = muertesEnTransitoPl.ObtenerAretesPorFolioEntrada(muertesEnTransitoInfoSeleccionado);
                    }

                    if (ProveedoresAsignadosAlEmbarque(muertesEnTransitoInfoSeleccionado.EmbarqueID) <= 1)
                    {
                        var pl = new ProveedorPL();
                        var cl = new ClientePL();
                        var clienteInfo = new ClienteInfo();
                        var info = new ProveedorInfo() { CodigoSAP = muertesEnTransitoInfoSeleccionado.CodigoProveedor };
                        
                        clienteInfo  = cl.ObtenerPorDescripcion(muertesEnTransitoInfoSeleccionado.Proveedor);
                        Contexto.Proveedor = pl.ObtenerPorCodigoSAP(info);
                        if(clienteInfo != null)
                        {
                            skAyudaCliente.IsEnabled = false;
                            muertesEnTransitoInfoSeleccionado.Cliente = clienteInfo;
                            skAyudaCliente.Clave = muertesEnTransitoInfoSeleccionado.Cliente.CodigoSAP;
                            skAyudaCliente.Descripcion = muertesEnTransitoInfoSeleccionado.Cliente.Descripcion;
                            Contexto.Cliente = clienteInfo;
                        }
                        else
                        {
                            skAyudaCliente.IsEnabled = true;
                            skAyudaCliente.Clave = string.Empty;
                            skAyudaCliente.Descripcion = string.Empty;
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.MuertesEnTransito_ProveedorNoEsCliente,
                            MessageBoxButton.OK, MessageImage.Warning);
                            skAyudaCliente.Focus();
                        }
                    }
                    else
                    {
                        skAyudaCliente.IsEnabled = true;
                    }

                    Contexto.Proveedor.EmbarqueID = muertesEnTransitoInfoSeleccionado.EmbarqueID;
                    countAreteGenerico = muertesEnTransitoInfoSeleccionado.MuertesRegistradas + 1;
                    txtOrigen.Text = muertesEnTransitoInfoSeleccionado.Origen;
                    txtTipoOrigen.Text = muertesEnTransitoInfoSeleccionado.TipoOrigen;
                    txtCorral.Text = muertesEnTransitoInfoSeleccionado.Corral;
                    txtLote.Text = muertesEnTransitoInfoSeleccionado.Lote;
                    txtCabezas.Text = muertesEnTransitoInfoSeleccionado.Cabezas.ToString();
                    txtMuertesEnTransito.Text = muertesEnTransitoInfoSeleccionado.MuertesTransito.ToString();
                    Animales.Clear();
                    dgAretes.Items.Refresh();
                    txtArete.Clear();
                    txtArete.Focus();
                }
            }
        }

        /// <summary>
        /// Limpia los valores de la ayuda del campo Folio de Entrada.
        /// </summary>
        private void LimpiarTodoAyudaFolioEntrada()
        {
            InicializarFormulario();
        }

        /// <summary>
        /// Genera un cadena, que representa un arete generico.
        /// </summary>
        /// <returns></returns>
        private string GenerarAreteGenerico()
        {
            var arete = organizacionID.ToString().PadLeft(4,'0');
            arete += muertesEnTransitoInfoSeleccionado.FolioEntrada.ToString().PadLeft(8,'0');
            arete += countAreteGenerico.ToString().PadLeft(3,'0');
            countAreteGenerico++;
            return arete;
        }

        /// <summary>
        /// Obtiene la cantidad de proveedores asignados a un embarque.
        /// </summary>
        /// <returns></returns>
        private int ProveedoresAsignadosAlEmbarque(int embarqueID)
        {
            var totalProveedores = 0;
            try
            {
                var proveedorPL = new ProveedorPL();
                var pagina = new SIE.Base.Infos.PaginacionInfo() { Inicio = 1, Limite = 15 };
                var filtro = new ProveedorInfo() { Descripcion = "", Activo = EstatusEnum.Activo, EmbarqueID = embarqueID };
                var result = proveedorPL.ObtenerPorPaginaEmbarque(pagina, filtro);
                totalProveedores = result.TotalRegistros;
            }
            catch(Exception)
            {
                totalProveedores = 0;
            }

            return totalProveedores;
        }

      #endregion

      #region Eventos
        //Se activa cuando dan click sobre el boton
        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {

            if(muertesEnTransitoInfoSeleccionado == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuertesEnTransito_msgFolioEntrada,
                       MessageBoxButton.OK, MessageImage.Warning);
                txtArete.Clear();
                skAyudaFolioEntrada.AsignarFoco();
                return;
            }
            if (chkSinArete.IsChecked == false && txtArete.Text.Trim() == "")
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuertesEnTransito_msgIngresarArete,
                       MessageBoxButton.OK, MessageImage.Warning);
                txtArete.Focus();
                return;
            }
            if (Animales.Count >= muertesEnTransitoInfoSeleccionado.MuertesTransito)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.MuertesEnTransito_msgMuertesAlLimite,
                       MessageBoxButton.OK, MessageImage.Warning);
                txtArete.Clear();
                txtArete.Focus();
                return;
            }

            var arete = string.Empty;
            if (chkSinArete.IsChecked == false && muertesEnTransitoInfoSeleccionado.TipoOrigenID != (int)TipoOrganizacion.CompraDirecta)
            {
                var animalesfiltro = new List<AnimalInfo>();
                if (AnimalesDelCorral != null)
                {
                    animalesfiltro = AnimalesDelCorral.Where(animalDelCorral => animalDelCorral.Arete == txtArete.Text).ToList();
                }
                if (animalesfiltro.Count == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_msgAreteNoPertenese,
                           MessageBoxButton.OK, MessageImage.Warning);
                    txtArete.Clear();
                    txtArete.Focus();
                    return;
                }
                arete = txtArete.Text;
            }
            else if (chkSinArete.IsChecked == true && muertesEnTransitoInfoSeleccionado.TipoOrigenID != (int)TipoOrganizacion.CompraDirecta)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_msgNecesitaArete,
                           MessageBoxButton.OK, MessageImage.Warning);
                txtArete.Clear();
                txtArete.Focus();
                chkSinArete.IsChecked = false;
                return;
            }
            else
            {
                arete = GenerarAreteGenerico();
                if (muertesEnTransitoInfoSeleccionado.TipoOrigenID != (int)TipoOrganizacion.CompraDirecta)
                {
                    chkSinArete.IsChecked = false;
                }
            }

            var animal = new AnimalInfo {AnimalID = Animales.Count + 1, Arete = arete};
            Animales.Add(animal);
            txtArete.Clear();
            txtArete.Focus();
            dgAretes.Items.Refresh();
        }

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            txtArete.Clear();
            txtArete.Focus();
        }

        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MuertesEnTransitoPL muertePl = new MuertesEnTransitoPL();
                ValidacionesFolioVentaMuerte validaciones = new ValidacionesFolioVentaMuerte();
                List<string> aretes = new List<string>();
                if (muertesEnTransitoInfoSeleccionado == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_msgFolioEntrada,
                           MessageBoxButton.OK, MessageImage.Warning);
                    txtArete.Clear();
                    skAyudaFolioEntrada.AsignarFoco();
                    return;
                }
                if (Animales.Count < muertesEnTransitoInfoSeleccionado.MuertesTransito)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_msgNroAretesEsMenor,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    txtArete.Focus();
                    return;
                }
                if (muertesEnTransitoInfoSeleccionado.MuertesTransito > muertesEnTransitoInfoSeleccionado.CabezasLote)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_msgNoTieneExistencia,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    return;
                }
                if (Contexto.Cliente == null || Contexto.Cliente.CodigoSAP.Length == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_msgSeleccionarProveedor,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    return;
                }
                foreach(AnimalInfo arete in dgAretes.ItemsSource)
                {
                    aretes.Add(arete.Arete);
                }
                validaciones = muertePl.ValidarFolio(muertesEnTransitoInfoSeleccionado.FolioEntrada, organizacionID, aretes);

                if (!validaciones.RegistroCondicionMuerte)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_FolioSinCondicionMuerte,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                    return;
                }

                if (validaciones.FolioConMuertesRegistradas)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_FolioConRegistroMuertes,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                    return;
                }

                if(!validaciones.EstatusLote)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_LoteInactivo,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                    return;
                }

                if (validaciones.Cabezas == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.MuertesEnTransito_LoteSinCabezas,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    InicializarFormulario();
                    skAyudaFolioEntrada.AsignarFoco();
                    return;
                }
                if (validaciones.AretesInvalidos.Length > 0)
                {
                    var mensajeError = string.Empty;
                    mensajeError = string.Format(Properties.Resources.MuertesEnTransito_AreteConRegistroVentaMuerteTransito, validaciones.AretesInvalidos);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           mensajeError,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    return;
                }
                if (validaciones.AretesActivos.Length > 0)
                {
                    var mensajeError = string.Empty;
                    mensajeError = string.Format(Properties.Resources.MuertesEnTransito_AreteActivoEnInventario, validaciones.AretesActivos);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           mensajeError,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    return;
                }

                muertesEnTransitoInfoSeleccionado.Proveedor = muertesEnTransitoInfoSeleccionado.Cliente.Descripcion;
                muertesEnTransitoInfoSeleccionado.CodigoProveedor = muertesEnTransitoInfoSeleccionado.Cliente.CodigoSAP;

                var muertesEnTrasnsitoPl = new MuertesEnTransitoPL();
                var folio = skAyudaFolioEntrada.Clave;
                string mensaje = string.Empty;
                muertesEnTransitoInfoSeleccionado.UsuarioCreacionID = usuarioID;
                muertesEnTransitoInfoSeleccionado.OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                MemoryStream stream = muertesEnTrasnsitoPl.Guardar(muertesEnTransitoInfoSeleccionado, Animales);
                mensaje = string.Format(Properties.Resources.MuertesEnTransito_msgGuardar, folio.ToString());

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      mensaje, MessageBoxButton.OK, MessageImage.Correct);

                try
                {
                    if (stream != null)
                    {
                        var exportarPoliza = new ExportarPoliza();
                        exportarPoliza.ImprimirPoliza(stream,
                                                      string.Format("{0} {1}", "Poliza Salida Folio No.",
                                                                    folio));
                    }
                }
                catch
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.SalidaIndividualGanado_MensajeErrorAlImprimirPoliza,
                                       MessageBoxButton.OK, MessageImage.Warning);
                }

                InicializarFormulario();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                    String.Format("{0}: {1}",Properties.Resources.MuertesEnTransito_msgErrorGuardar,ex.Message),
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.MuertesEnTransito_msgCancelar,
               MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                InicializarFormulario();
            }
        }

      #endregion
    }
}
