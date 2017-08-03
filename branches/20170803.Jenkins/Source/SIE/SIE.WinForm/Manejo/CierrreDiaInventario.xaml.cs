using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para CierrreDiaInventario.xaml
    /// </summary>
    public partial class CierrreDiaInventario
    {
        #region Atriburos

        private int usuarioID;
        private int organizacionId;
        private IList<AlmacenInfo> listaAlmacenInfo; 
        #endregion
        #region constructor
        public CierrreDiaInventario()
        {
            InitializeComponent();
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
        }
        #endregion end

        #region Eventos

        private void CierrreDiaInventario_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (CboAlmacenes.SelectedIndex <= 0)
            {
                CargarCboAlmacenes();
            }
        }



        #endregion
        #region Metodos

        private void CargarCboAlmacenes()
        {
          
            AlmacenPL almacenPl = new AlmacenPL();
            listaAlmacenInfo = almacenPl.ObtenerAlmacenPorUsuario(usuarioID, organizacionId);

        }

        #endregion


    }
}
