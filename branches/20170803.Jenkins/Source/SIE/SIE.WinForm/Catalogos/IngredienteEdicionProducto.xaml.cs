using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para IngredienteEdicionProducto.xaml
    /// </summary>
    public partial class IngredienteEdicionProducto
    {

        #region CONSTRUCTORES
        /// <summary>
        /// Constructor parametrizado
        /// </summary>
        public IngredienteEdicionProducto(IngredienteInfo ingredienteProducto)
        {
            try
            {
                InitializeComponent();
                IngredienteProducto = ingredienteProducto;
                IngredienteProducto.Producto.Familias = new List<FamiliaInfo>
                                                            {
                                                                new FamiliaInfo
                                                                    {
                                                                        FamiliaID =
                                                                            FamiliasEnum.MateriaPrimas.GetHashCode()
                                                                    },
                                                                new FamiliaInfo
                                                                    {
                                                                        FamiliaID =
                                                                            FamiliasEnum.Premezclas.GetHashCode()
                                                                    },
                                                            };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.IngredienteEdicionProducto_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion CONSTRUCTORES

        #region PROPIEDADES

        /// <summary>
        /// Propiedad donde se almacenan los objetos que utiliza la pantalla
        /// </summary>
        private IngredienteInfo IngredienteProducto
        {
            get
            {
                return (IngredienteInfo)DataContext;
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
        /// Evento que se ejecuta cuando inicia la pantalla
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                skAyudaProducto.ObjetoNegocio = new ProductoPL();
                skAyudaProducto.AyudaConDatos += (o, args) =>
                    {
                        IngredienteProducto.Producto.Familias = new List<FamiliaInfo>
                                                            {
                                                                new FamiliaInfo
                                                                    {
                                                                        FamiliaID =
                                                                            FamiliasEnum.MateriaPrimas.GetHashCode()
                                                                    },
                                                                new FamiliaInfo
                                                                    {
                                                                        FamiliaID =
                                                                            FamiliasEnum.Premezclas.GetHashCode()
                                                                    },
                                                            };
                        IngredienteProducto.Producto.SubfamiliaId = 0;
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.IngredienteEdicionProducto_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el boton Aceptar
        /// </summary>
        private void btnAceptar_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ValidarProducto())
            {
                return;
            }
            ConfirmaSalir = false;
            Close();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (ConfirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el boton Cancelar
        /// </summary>
        private void btnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private bool ValidarProducto()
        {
            if (IngredienteProducto.Producto.ProductoId == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.IngredienteEdicionProducto_CampoProducto, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            if (!dtuPorcentajeProgramado.Value.HasValue || dtuPorcentajeProgramado.Value.Value == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.IngredienteEdicionProducto_CampoPorcentajeProgramado, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            return true;
        }
        #endregion METODOS
    }
}
