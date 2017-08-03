using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para ProblemaSintomaEdicionSintoma.xaml
    /// </summary>
    public partial class ProblemaSintomaEdicionSintoma
    {

        #region CONSTRUCTORES
        /// <summary>
        /// Constructor parametrizado
        /// </summary>
        public ProblemaSintomaEdicionSintoma(ProblemaSintomaInfo problemaSintoma)
        {
            try
            {
                InitializeComponent();
                ProblemaSintoma = problemaSintoma;
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //SkMessageBox.Show(this, Properties.Resources.ProblemaSintomaEdicionSintoma_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion CONSTRUCTORES

        #region PROPIEDADES

        /// <summary>
        /// Propiedad donde se almacenan los objetos que utiliza la pantalla
        /// </summary>
        private ProblemaSintomaInfo ProblemaSintoma
        {
            get
            {
                return (ProblemaSintomaInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        public bool ConfirmaSalir = true;

        #endregion PROPIEDADES

        #region VARIABLES

        #endregion VARIABLES

        #region EVENTOS

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (ConfirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.ProblemaSintomaEdicionSintoma_SalirSinAgregar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion EVENTOS

        #region METODOS

        #endregion METODOS

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarSintoma())
            {
                return;
            }
            ConfirmaSalir = false;
            Close();
        }

        private bool ValidarSintoma()
        {
            if(ProblemaSintoma.Sintoma == null || ProblemaSintoma.Sintoma.SintomaID == 0)
            {

                SkMessageBox.Show(this, Properties.Resources.ProblemaSintomaEdicionSintoma_MsgCapturarSintoma, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            return true;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            skAyudaSintoma.ObjetoNegocio = new SintomaBL();
        }
    }
}
