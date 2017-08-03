using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para DiagnosticoAnalista.xaml
    /// </summary>
    public partial class DiagnosticoAnalista 
    {
        #region Declaraciones
        public List<ProblemaInfo> ListaProblemas { get; set; }
        public IList<GradoInfo> ListaGrados { get; set; }
        public IList<ProblemaInfo> ProblemasObtenidos { get; set; } 
        public GradoInfo GradoSeleccionado
        {
            get { return gradoSeleccionado; }
            set
            {
                gradoSeleccionado = value;
                lblNivelGravedad.Content = lblNivelGravedad.Content = gradoSeleccionado.NivelGravedad == "G" ? NivelGravedadEnum.Grave : NivelGravedadEnum.Leve;
            }
        }

        private GradoInfo gradoSeleccionado;
        public bool Seleccionado { get; set; }

        public String Justificacion
        {
            get { return txtJustificacion.Text; }
            set { txtJustificacion.Text = value; }
        }
        #endregion


        #region Constructor
        /// <summary>
        /// Construxtor
        /// </summary>
        public DiagnosticoAnalista()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evento de carga de pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Seleccionado)
            {
                CargarProblemas();
                LlenarGrados();
                MarcarProblemas();
                MarcarGrado();
            }
            else
            {
                lstProblemas.ItemsSource = ListaProblemas;
                lstGrados.ItemsSource = ListaGrados;
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Carga los grados disponibles
        /// </summary>
        private void LlenarGrados()
        {
            var enfermeriDal = new EnfermeriaPL();
            ListaGrados = enfermeriDal.ObtenerGrados();
            lstGrados.ItemsSource = ListaGrados;
        }
        /// <summary>
        /// Carga los problemas disponibles
        /// </summary>
        private void CargarProblemas()
        {
            var enfermeriaDal = new EnfermeriaPL();

            ListaProblemas = enfermeriaDal.ObtenerProblemas();
            lstProblemas.ItemsSource = ListaProblemas;
        }
        /// <summary>
        /// Valida si existen datos en blanco
        /// </summary>
        /// <returns></returns>
        private bool VerificarDatosEnBlanco()
        {
            var contadorProblemas = ListaProblemas.Count(problema => problema.isCheked);

            if (contadorProblemas == 0)
            {
                return false;
            }

            if (GradoSeleccionado == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtJustificacion.Text))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Seleccion de un grado
        /// </summary>
        /// <param name="gradoId"></param>
        public void SeleccionarGrado(int gradoId)
        {
            foreach (var grado in ListaGrados)
            {
                if (grado.GradoID == gradoId)
                {
                    GradoSeleccionado = grado;
                    lblNivelGravedad.Content = grado.NivelGravedad == "G" ? NivelGravedadEnum.Grave : NivelGravedadEnum.Leve;
                }
            }
        }
        /// <summary>
        /// Marca los problemas iniciales que fueron detectados
        /// </summary>
        private void MarcarProblemas()
        {
            if (ProblemasObtenidos != null && ListaProblemas != null)
            {
                foreach (var problema in ProblemasObtenidos)
                {
                    foreach (var problemaActual in ListaProblemas)
                    {
                        if (problemaActual.ProblemaID == problema.ProblemaID)
                        {
                            problemaActual.isCheked = true;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Muestra los grados
        /// </summary>
        private void MarcarGrado()
        {
            if (ListaGrados != null)
            {
                foreach (var grado in ListaGrados)
                {
                    if (grado.GradoID == gradoSeleccionado.GradoID)
                    {
                        grado.isChecked = true;
                    }
                }
            }
        }

        #endregion


        #region Eventos
        /// <summary>
        /// Evento click del boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarDatosEnBlanco())
            {
                Seleccionado = true;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                   Properties.Resources.DiagnosticoAnalista_Guardado,
                                   MessageBoxButton.OK, MessageImage.Correct);
                Close();
            }
            else
            {
                Seleccionado = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.DiagnosticoAnalista_DatosEnBlanco,
                                    MessageBoxButton.OK, MessageImage.Warning);
            }
        }
        /// <summary>
        /// Evento click del boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(
                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.EntradaGanadoEnfermeria_BusquedaPreguntaCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                Seleccionado = false;
                txtJustificacion.Text = string.Empty;
                Close();
            }


        }
        
        /// <summary>
        /// Checked del grado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbGrado_checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var rdbSeleccionado = (RadioButton)sender;
                var idGrado = (int)rdbSeleccionado.Tag;
                SeleccionarGrado(idGrado);
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento cerrado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiagnosticoAnalista_OnClosed(object sender, EventArgs e)
        {
            if (Seleccionado == false)
            {
                txtJustificacion.Text = "";
            }
        }

        #endregion
    }
}
