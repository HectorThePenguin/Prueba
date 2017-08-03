using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Linq;
using System.ComponentModel;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para RecibirProductoAlmacenReplicaSesion.xaml
    /// </summary>
    public partial class RecibirProductoAlmacenReplicaSesion : Window
    {
        #region VARIABLES

        public List<string> Aretes;
        private bool _confirmar = true;

        #endregion VARIABLES

        #region CONSTRUCTOR

        public RecibirProductoAlmacenReplicaSesion()
        {
            InitializeComponent();
            Aretes = new List<string>();
        }

        #endregion CONSTRUCTOR

        #region METODOS

        private void Salir()
        {
            MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _confirmar = false;
                Close();
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //if (ValidarAretes())
            //{
            //    Guardar();
            //}
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Salir();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_confirmar)
            {
                MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                          MessageImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion EVENTOS


    }
}
