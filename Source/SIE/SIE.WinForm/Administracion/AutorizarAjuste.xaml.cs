using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Catalogos;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using TipoMovimiento = SIE.Services.Info.Enums.TipoMovimiento;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para AutorizarAjuste.xaml
    /// </summary>
    public partial class AutorizarAjuste
    {
        #region Atributos
        private IList<AlmacenInfo> listaAlmacenInfo;
        private bool bandFocoAlmacen;
        private int organizacionId;
        #endregion
        #region Constructor
        public AutorizarAjuste()
        {
            bandFocoAlmacen = false;
            listaAlmacenInfo=new List<AlmacenInfo>();
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            InitializeComponent();
            LimpiarCaptura();
        }
        #endregion
        #region Eventos
        /// <summary>
        /// Evento para llamar el dialogo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetalle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridDatos.SelectedIndex >= 0)
                {
                    CargarajusteDeInventarioLog();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.AutorizarAjuste_NoSeccionadoDatoGrid,
                            MessageBoxButton.OK, MessageImage.Warning);
                    e.Handled = true;
                    gridDatos.Focus();
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            
               
                
          }

        /// <summary>
        /// cargar grid cuando se pierde el foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAlmacenes_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!bandFocoAlmacen)
            {
                if (cboAlmacenes.SelectedIndex > 0)
                {
                    CargarGrid();

                    e.Handled = true;

                    bandFocoAlmacen = true;
                }

            }
            else
            {
                bandFocoAlmacen = false;
            }

        }
        /// <summary>
        /// Carga ajuste de inverario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridDatos_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridDatos.SelectedIndex >= 0)
            {
                CargarajusteDeInventarioLog();
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.AutorizarAjuste_NoSeccionadoDatoGrid,
                        MessageBoxButton.OK, MessageImage.Warning);
                e.Handled = true;
                gridDatos.Focus();
            }
        }

        /// <summary>
        /// Cargar ajuste inventario
        /// </summary>
        public void CargarajusteDeInventarioLog()
        {
            try
            {
                AlmacenMovimientoInfo seleccion =
                  (AlmacenMovimientoInfo)gridDatos.SelectedItem;


                var ajusteDeInventarioLog = new AjusteDeInventario(seleccion)
                {
                    Left = (ActualWidth - Width) / 2,
                    Top = ((ActualHeight - Height) / 2) + 132,
                    Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                };
                ajusteDeInventarioLog.ShowDialog();
                CargarGrid();
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
           
        }

       
        /// <summary>
        /// Evento para cargar de inicio el combo de almacenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutorizarAjuste_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (cboAlmacenes.SelectedIndex <= 0)
            {
                CargarCboAlmacenes();
                cboAlmacenes.Focus();
            }
        }

       
        /// <summary>
        /// Evento para cargar el grid al teclear entet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAlmacenes_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (cboAlmacenes.SelectedIndex > 0)
                {
                    CargarGrid();
                    e.Handled = true;
                    gridDatos.Focus();
                }
                
            }
        }
       

        #endregion
        #region Metodos
        /// <summary>
        /// Cargar el grid
        /// </summary>
        public void CargarGrid()
        {
            var almacenId = (int)cboAlmacenes.SelectedValue;
            var almacenPl = new AlmacenPL();
            AlmacenMovimientoInfo almacenMovimientoInfo = new AlmacenMovimientoInfo
            {
                AlmacenID = almacenId,
                TipoMovimientoID = (int)TipoMovimiento.InventarioFisico,
                Status = (int)EstatusInventario.Pendiente
            };

            IList<AlmacenMovimientoInfo> resultadoGrid = almacenPl.ObtenerListaAlmacenMovimiento(almacenMovimientoInfo, (int)EstatusEnum.Activo);
            if (resultadoGrid.Count > 0)
            {
                gridDatos.ItemsSource = resultadoGrid;
                btnDetalle.IsEnabled = true;
                
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.AutorizarAjuste_NoMovimientosDiferencia,
                       MessageBoxButton.OK, MessageImage.Warning);
                LimpiarCaptura();
            }
        }
        /// <summary>
        /// Limpiar la captura
        /// </summary>
        public void LimpiarCaptura()
        {
            cboAlmacenes.SelectedIndex = 0;
            gridDatos.ItemsSource = null;
            btnDetalle.IsEnabled = false;

        }
        /// <summary>
        /// Cargar el combo almacenes
        /// </summary>
        private void CargarCboAlmacenes()
        {
            try
            {
            AlmacenPL almacenPl = new AlmacenPL();
            listaAlmacenInfo = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionId);
            if (listaAlmacenInfo != null)
            {
                var almacenes = new AlmacenInfo { Descripcion = "Seleccione", AlmacenID = 0 };
                listaAlmacenInfo.Insert(0, almacenes);
                cboAlmacenes.ItemsSource = listaAlmacenInfo;
                cboAlmacenes.SelectedValue = 0;
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CierreDiaInventario_NoAlmacenesOrganizacionUsuario,
                        MessageBoxButton.OK, MessageImage.Warning);
                LimpiarCaptura();
                cboAlmacenes.IsEnabled = false;
            }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        #endregion

       
    }
}
