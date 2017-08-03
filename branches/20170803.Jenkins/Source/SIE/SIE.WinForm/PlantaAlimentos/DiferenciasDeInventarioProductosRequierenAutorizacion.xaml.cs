using System.Collections.Generic;
using System.Windows;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para DiferenciasDeInventarioProductosRequierenAutorizacion.xaml
    /// </summary>
    public partial class DiferenciasDeInventarioProductosRequierenAutorizacion
    {
        #region Propiedades
        List<DiferenciasDeInventariosInfo> listaDiferenciasDeInventario = new List<DiferenciasDeInventariosInfo>();
        #endregion

        #region Constructor
        public DiferenciasDeInventarioProductosRequierenAutorizacion(List<DiferenciasDeInventariosInfo> listaDiferenciaInventariosAutorizado)
        {
            InitializeComponent();
            listaDiferenciasDeInventario = listaDiferenciaInventariosAutorizado;
            CargarGrid();
        }
        #endregion

        #region
        /// <summary>
        /// Cierra la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCerrar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Se carga el grid principal
        /// </summary>
        public void CargarGrid()
        {
            foreach (var diferenciasDeInventariosInfo in listaDiferenciasDeInventario)
            {
                if (diferenciasDeInventariosInfo.AlmacenMovimiento.TipoMovimientoID ==
                    TipoMovimiento.EntradaPorAjuste.GetHashCode())
                {
                    diferenciasDeInventariosInfo.PorcentajeAjustePermitido =
                        diferenciasDeInventariosInfo.PorcentajeAjustePermitidoSuperavit;
                    diferenciasDeInventariosInfo.PorcentajeAjuste =
                        diferenciasDeInventariosInfo.PorcentajeAjuste*-1;
                }
                else
                {
                    diferenciasDeInventariosInfo.PorcentajeAjustePermitido =
                        diferenciasDeInventariosInfo.PorcentajeAjustePermitidoMerma;
                }
            }
            GridDiferenciasDeInventarios.ItemsSource = listaDiferenciasDeInventario;
        }
        #endregion
    }
}
