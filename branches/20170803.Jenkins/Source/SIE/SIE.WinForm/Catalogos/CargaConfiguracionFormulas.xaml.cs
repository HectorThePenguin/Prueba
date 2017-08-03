using System;
using System.Collections.Generic;
using System.Windows;

using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;


namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CargaConfiguracionFormulas.xaml
    /// </summary>
    public partial class CargaConfiguracionFormulas
    {
        #region Atributos
            /// <summary>
            /// Control para la ayuda de Organización
            /// </summary>
            private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
            /// <summary>
            /// Vairiable para manejar el Usuario Logueado
            /// </summary>
            private readonly int usuarioLogueadoID;
            /// <summary>
            /// Vairiable para uso global
            /// </summary>
            private ConfiguracionFormulaInfo ConfiguracionFormula;
        
        #endregion Atributos

        #region Costructor

            /// <summary>
            /// Constructor
            /// </summary>
            public CargaConfiguracionFormulas()
            {
                InitializeComponent();
                usuarioLogueadoID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString());
                ConfiguracionFormula = new ConfiguracionFormulaInfo();
                AgregarAyudaOrigen();
            }

        #endregion Costructor

        #region Metodos
            /// <summary>
            /// Metodo para dejar utilizable la pantalla de nuevo
            /// </summary>
            private void LimpiarControles()
            {
                skAyudaOrganizacion.LimpiarCampos();
                txtArchivo.Text = "";
                ConfiguracionFormula = new ConfiguracionFormulaInfo();
            }
            /// <summary>
            /// Configura la ayuda para ligarlo con la organización origen
            /// </summary>
            private void AgregarAyudaOrigen()
            {
                skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(160, false, new OrganizacionInfo()
                                                        , "PropiedadClaveRegistroProgramacionEmbarqueAyudaOrigen"
                                                        , "PropiedadDescripcionRegistroProgramacionEmbarqueAyudaOrigen", true)
                {
                    //PropiedadClaveRegistroProgramacionEmbarqueAyudaOrigen
                    AyudaPL = new OrganizacionPL(),
                    MensajeClaveInexistente = Properties.Resources.ConfigurarFormula_OrganizacionInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.ConfigurarFormula_SalirSinSeleccionarOrganizacion,
                    MensajeBusqueda = Properties.Resources.ConfigurarFormula_Busqueda,
                    MensajeAgregar = Properties.Resources.ConfigurarFormula_Seleccionar,
                    TituloEtiqueta = Properties.Resources.ConfigurarFormula_LeyendaAyudaBusqueda,
                    TituloPantalla = Properties.Resources.ConfigurarFormula_BusquedaOrganizacionTitulo,
                };
                skAyudaOrganizacion.AsignaTabIndex(1);

                StpAyudaOrigen.Children.Clear();
                StpAyudaOrigen.Children.Add(skAyudaOrganizacion);

                skAyudaOrganizacion.MensajeDependencias = null;
                IDictionary<String, String> mensajeDependencias = new Dictionary<String, String>();
                mensajeDependencias.Add("TipoOrganizacionID",
                                        Properties.Resources.RegistroProgramacionEmbarques_SeleccionarTipoOrganizacion);
                skAyudaOrganizacion.MensajeDependencias = mensajeDependencias;
                

                AsignaDependenciasAyudaOrganizacion(skAyudaOrganizacion);

            }

        /// <summary>
        /// Metodo para agregar las dependencias a las ayudas de Organización Origen y Destino
        /// </summary>
        private void AsignaDependenciasAyudaOrganizacion(SKAyuda<OrganizacionInfo> controlAyuda)
        {
            controlAyuda.Dependencias = null;

            IList<IDictionary<IList<String>, Object>> dependencias = new List<IDictionary<IList<String>, Object>>();
            IDictionary<IList<String>, Object> dependecia = new Dictionary<IList<String>, Object>();

            var dependenciasGanado = new EntradaGanadoInfo();
            IList<String> camposDependientes = new List<String>();
            camposDependientes.Add("EmbarqueID");
            dependecia.Add(camposDependientes, dependenciasGanado);
            dependencias.Add(dependecia);

            dependecia = new Dictionary<IList<String>, Object>();
            camposDependientes = new List<String> {"TipoOrganizacionID"};
            dependecia.Add(camposDependientes, new TipoOrganizacionInfo
                    {
                        TipoOrganizacionID = (int)Services.Info.Enums.TipoOrganizacion.Ganadera,
                        Descripcion = Services.Info.Enums.TipoOrganizacion.Ganadera.ToString()
                    });
                dependencias.Add(dependecia);

                controlAyuda.Dependencias = dependencias;
            }

            /// <summary>
            /// Metodo para validar los datos obligatorios
            /// </summary>
            /// <returns></returns>
            private bool ValidarDatosObligatorios()
            {

                if (!String.IsNullOrEmpty(skAyudaOrganizacion.Clave))
                {
                    if (!String.IsNullOrEmpty(ConfiguracionFormula.NombreArchivo))
                    {
                        return true;
                    }
                    //Seleccionar un archivo
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ConfigurarFormula_SeleccionarArchivoImportar,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    return false;
                }
                //Favor de seleccionar una organizacion
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfigurarFormula_SeleccionarOrganizacion,
                    MessageBoxButton.OK,
                    MessageImage.Stop);
                return false;
            }
        #endregion Metodos

        #region Eventos

            /// <summary>
            /// Evento para importar archivo de excel a la base de datos
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void btnImportar_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (!ValidarDatosObligatorios()) return;

                    var configuracionFormulaPL = new ConfiguracionFormulaPL();

                    var configuracionImportar = new ConfiguracionFormulaInfo
                    {
                        NombreArchivo = ConfiguracionFormula.NombreArchivo,
                        OrganizacionID = Extensor.ValorEntero(skAyudaOrganizacion.Clave),
                        UsuarioCreacionID = usuarioLogueadoID,
                        Activo = EstatusEnum.Inactivo
                    };

                    bool configuracionFormulaInfo =
                        configuracionFormulaPL.ImportarArchivo(configuracionImportar);

                    if (!configuracionFormulaInfo) return;
                    //Guardado correctamente
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ConfigurarFormula_CargaCorrectamente,
                        MessageBoxButton.OK, MessageImage.Correct);
                    LimpiarControles();
                }
                catch (ExcepcionDesconocida ex)
                {
                    Logger.Error(ex);
                    //Favor de seleccionar una organizacion
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    //Favor de seleccionar una organizacion
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ConfigurarFormula_ImportarError,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
            }

            /// <summary>
            /// Evento para exportar la configuracion de las formulas a excel
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void btnExportar_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (!String.IsNullOrEmpty(skAyudaOrganizacion.Clave))
                    {
                        var dlg = new Microsoft.Win32.SaveFileDialog
                        {
                            FileName = "ConfiguracionFormulas",
                            DefaultExt = ".xlsx",
                            Filter = "Excel files (.xlsx)|*.xlsx"
                        };

                        // Show save file dialog box
                        var result = dlg.ShowDialog();

                        // Process save file dialog box results
                        if (result != true) return;

                        // Save document
                        string nombreArchivo = dlg.FileName;
                        var configuracionFormulaPL = new ConfiguracionFormulaPL();
                        var configuracionImportar = new ConfiguracionFormulaInfo
                        {
                            NombreArchivo = nombreArchivo,
                            OrganizacionID = Extensor.ValorEntero(skAyudaOrganizacion.Clave)
                        };

                        bool configuracionFormulaInfo =
                            configuracionFormulaPL.ExportarArchivo(configuracionImportar);

                        if (!configuracionFormulaInfo) return;

                        //Guardado correctamente
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ConfigurarFormula_ExportoCorrectamente,
                            MessageBoxButton.OK, MessageImage.Correct);
                        LimpiarControles();
                    }
                    else
                    {
                        //Favor de seleccionar una organizacion
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ConfigurarFormula_SeleccionarOrganizacion,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                    }
                }
                catch (ExcepcionDesconocida ex)
                {
                    Logger.Error(ex);
                    //Favor de seleccionar una organizacion
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    //Favor de seleccionar una organizacion
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ConfigurarFormula_ExportarError,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
            }

            /// <summary>
            /// Evento para obtener el archivo q se va a importar
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnNavegar_Click(object sender, RoutedEventArgs e)
            {
                var dlg = new Microsoft.Win32.OpenFileDialog
                {
                    FileName = "ConfiguracionFormulas",
                    DefaultExt = ".xlsx",
                    Filter = "Excel files (.xlsx)|*.xlsx"
                };

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result != true) return;

                txtArchivo.Text = dlg.SafeFileName;
                ConfiguracionFormula = new ConfiguracionFormulaInfo {NombreArchivo = dlg.FileName};
            }

            /// <summary>
            /// Evento para el boton Cancelar
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnCancelar_Click(object sender, RoutedEventArgs e)
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.ConfigurarFormula_CancelarPregunta,
                   MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    LimpiarControles();
                    ConfiguracionFormula = new ConfiguracionFormulaInfo();
                }
                
            }
        #endregion Eventos

       
    }
}
