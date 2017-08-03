using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using NPOI.SS.Formula.Functions;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para CorralDestino.xaml
    /// </summary>
    public partial class CorralDestino 
    {
        public List<CorralInfo> listaCorrales = new List<CorralInfo>();
        public CorralInfo corralDisponible = new CorralInfo();
        public CorralInfo corralOrigenInfo = new CorralInfo();
        
        /// <summary>
        /// Variable ayuda
        /// </summary>
        private SKAyuda<CorralInfo> skAyudaCorral;
        private int organizacionID;

        public CorralDestino()
        {
            InitializeComponent();
        }
        public CorralDestino(CorralInfo corralOrigen, IList<CorralInfo> CorralesDestino)
        {
            InitializeComponent();
            organizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]);
            corralOrigenInfo = corralOrigen;
            CargarAyudas();
            listaCorrales = (List<CorralInfo>) CorralesDestino;
            gridCorrales.ItemsSource = null;
            gridCorrales.ItemsSource = listaCorrales;
        }


        /// <summary>
        /// Se define lo que se mostrara en el grid y que controles estaran deshabilitados
        /// </summary>
        private void DefinirPantalla()
        {
            if (listaCorrales != null && listaCorrales.Count > 0)
            {
                gridCorrales.ItemsSource = null;
                gridCorrales.ItemsSource = listaCorrales;
            }

        }

        /// <summary>
        /// Carga las ayudas de la pantalla
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaCorral(SplAyudaCorral);
        }

        /// <summary>
        /// Limpia ayuda, txtImporte y txtToneladas
        /// </summary>
        private void LimpiarCampos()
        {
            if (skAyudaCorral != null)
            {
                skAyudaCorral.LimpiarCampos();
                skAyudaCorral.Info.OrganizacionId = organizacionID;
                skAyudaCorral.Info.CodigoOrigen = corralOrigenInfo.Codigo;
                skAyudaCorral.Info.Codigo = skAyudaCorral.Descripcion;
            }
        }

        /// <summary>
        /// Obtener la ayuda para seleccionar el corral
        /// </summary>
        private void AgregarAyudaCorral(StackPanel stackPanel)
        {
            try
            {
                var corral = new CorralInfo();
                corral = InicializarInfoAyudaCorral(corral);
                if (corral != null)
                {
                    skAyudaCorral = new SKAyuda<CorralInfo>(90,
                        true,
                        corral,
                        "PropiedadClaveCorralDestino",
                        "PropiedadDescripcionCorralDestino",
                        false)
                    {
                        AyudaPL = new CorralPL(),
                        MensajeClaveInexistente = Properties.Resources.Corral_Inexistente,
                        MensajeBusquedaCerrar = Properties.Resources.Corral_SalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.Corral_Busqueda,
                        MensajeAgregar = Properties.Resources.Corral_Seleccionar,
                        TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCorral,
                        TituloPantalla = Properties.Resources.BusquedaCorral_Titulo,
                    };

                    skAyudaCorral.ObtenerDatos += ObtenerDatosCorral;
                    skAyudaCorral.LlamadaMetodosNoExistenDatos += LimpiarCampos;

                    skAyudaCorral.AsignaTabIndex(0);
                    stackPanel.Children.Clear();
                    stackPanel.Children.Add(skAyudaCorral);
                    skAyudaCorral.TabIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeFalloCargarAyudaCorral,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Inicializa los valores de corral para la ayuda
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        private CorralInfo InicializarInfoAyudaCorral(CorralInfo corral)
        {
            try
            {
                corral.CodigoOrigen = corralOrigenInfo.Codigo;
                corral.OrganizacionId = organizacionID;
                corral.Codigo = skAyudaCorral.Descripcion;

                var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };
                var pl = new CorralPL();
                var corralinfo =
                        new CorralInfo()
                        {
                            CodigoOrigen = corralOrigenInfo.Codigo,
                            OrganizacionId = organizacionID,
                            Codigo = skAyudaCorral.Descripcion
                        };
                var resultado = pl.ObtenerPorPaginaCorralDestinoReimplante(pagina, corralinfo);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return corral;

        }
       
        /// <summary>
        /// Verifica que el corral sea de materia prima
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCorral(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaCorral.Info == null)
                {
                    return;
                }
                skAyudaCorral.Info.OrganizacionId = organizacionID;
                skAyudaCorral.Info.Codigo = corralOrigenInfo.Codigo;
                var pl = new CorralPL();
                var corralinfo =
                        new CorralInfo()
                        {
                            Codigo = clave,
                            OrganizacionId = organizacionID
                        };
                var resultado = pl.ObtenerPorCodigoCorralDestinoReimplante(corralinfo);
                if (resultado != null){

                    if (ExisteCorralEnListaConfigurada(resultado))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorralesDestino_CorralDestinoContemplado,
                        MessageBoxButton.OK, MessageImage.Warning);
                        LimpiarCampos();
                        skAyudaCorral.AsignarFoco();
                    }
                    else
                    {
                        corralDisponible = resultado;
                        btnAgregar.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private bool ExisteCorralEnListaConfigurada(CorralInfo resultado)
        {
            bool resp = false;
            if (listaCorrales != null) { 
                foreach (var corral in listaCorrales.Where(
                         corral => corral.CorralID == resultado.CorralID))
                {
                    resp = true;
                }
            }
            return resp;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (corralDisponible != null && corralDisponible.CorralID>0)
            {
                if (listaCorrales==null)
                    listaCorrales = new List<CorralInfo>();
                listaCorrales.Add(corralDisponible);
                gridCorrales.ItemsSource = null;
                gridCorrales.ItemsSource = listaCorrales;
                corralDisponible = null;
                LimpiarCampos();
                skAyudaCorral.AsignarFoco();
            }
        }

        /// <summary>
        /// Evento click de btnCerrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var corralInfoSelecionado = (CorralInfo)btn.CommandParameter;

                if (corralInfoSelecionado != null)
                {
                    if (listaCorrales == null)
                    {
                        listaCorrales = new List<CorralInfo>();
                    }
                    else
                    {
                        listaCorrales.Remove(corralInfoSelecionado);
                    }

                    gridCorrales.ItemsSource = null;
                    gridCorrales.ItemsSource = listaCorrales;
                    corralDisponible = null;
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            
        }

        /// <summary>
        /// Cargar corrales guardados anteriormente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowCorralDestino_Loaded(object sender, RoutedEventArgs e)
        {
            DefinirPantalla();
            skAyudaCorral.AsignarFoco();
        }
        /// <summary>
        /// Método que maneja guardar en el info CorralInfo en "PuntaChica" el estado del checkbox
        /// </summary>
        private void HandleChange_PuntaChica(object sender, RoutedEventArgs e)
        {
            try
            {
                var b = (CorralInfo)gridCorrales.SelectedItem;

                foreach (var elementos in listaCorrales)
                {
                if (elementos.Codigo == b.Codigo)
                    if (b.PuntaChica == false)
                    {
                        elementos.PuntaChica = true;
                    }
                    else
                    {
                        elementos.PuntaChica = false;
                    }
                }
                gridCorrales.ItemsSource = null;
                gridCorrales.ItemsSource = listaCorrales;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
