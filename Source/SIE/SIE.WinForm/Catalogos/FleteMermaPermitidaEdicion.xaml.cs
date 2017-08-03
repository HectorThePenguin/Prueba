using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for FleteMermaPermitidaEdicion.xaml
    /// </summary>
    public partial class FleteMermaPermitidaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FleteMermaPermitidaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (FleteMermaPermitidaInfo) DataContext;
                }
                set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public FleteMermaPermitidaEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad FleteMermaPermitida Existente
        /// </summary>
        /// <param name="fleteMermaPermitidaInfo"></param>
        public FleteMermaPermitidaEdicion(FleteMermaPermitidaInfo fleteMermaPermitidaInfo)
        {
           InitializeComponent();
           fleteMermaPermitidaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = fleteMermaPermitidaInfo;
        }

        #endregion Constructores

        #region Eventos

        private void FleteMermaPermitidaLoaded(object sender, RoutedEventArgs e)
        {
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaSubFamilia.ObjetoNegocio = new SubFamiliaPL();
            skAyudaSubFamilia.AyudaConDatos += (o, args) =>
                                                   {
                                                       var subFamilia = skAyudaSubFamilia.Contexto as SubFamiliaInfo;
                                                       subFamilia.Familias = new List<FamiliaInfo>
                                                                                 {
                                                                                     new FamiliaInfo
                                                                                         {
                                                                                             FamiliaID =
                                                                                                 FamiliasEnum.
                                                                                                 MateriaPrimas.
                                                                                                 GetHashCode()
                                                                                         },
                                                                                     new FamiliaInfo
                                                                                         {
                                                                                             FamiliaID =
                                                                                                 FamiliasEnum.Premezclas
                                                                                                 .
                                                                                                 GetHashCode()
                                                                                         }
                                                                                 };
                                                   };
            skAyudaOrganizacion.AsignarFoco();
        }
        
        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MermaPermitida(object sender, TextCompositionEventArgs e)
        {
            string texto = ((TextBox)sender).Text;
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, texto ?? string.Empty);
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FleteMermaPermitidaInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Organizacion = new OrganizacionInfo
                                                  {
                                                      TipoOrganizacion = new TipoOrganizacionInfo()
                                                  },
                               SubFamilia = new SubFamiliaInfo
                                                {
                                                    Familias = new List<FamiliaInfo>
                                                                   {
                                                                       new FamiliaInfo
                                                                           {
                                                                               FamiliaID =
                                                                                   FamiliasEnum.MateriaPrimas.
                                                                                   GetHashCode()
                                                                           },
                                                                       new FamiliaInfo
                                                                           {
                                                                               FamiliaID =
                                                                                   FamiliasEnum.Premezclas.
                                                                                   GetHashCode()
                                                                           }
                                                                   }
                                                }
                           };
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(txtFleteMermaPermitidaID.Text) )
                {
                    resultado = false;
                    mensaje = Properties.Resources.FleteMermaPermitidaEdicion_MsgFleteMermaPermitidaIDRequerida;
                    txtFleteMermaPermitidaID.Focus();
                }
                else if (Contexto.Organizacion.OrganizacionID == 0 )
                {
                    resultado = false;
                    mensaje = Properties.Resources.FleteMermaPermitidaEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (Contexto.SubFamilia.SubFamiliaID == 0 )
                {
                    resultado = false;
                    mensaje = Properties.Resources.FleteMermaPermitidaEdicion_MsgSubFamiliaIDRequerida;
                    skAyudaSubFamilia.AsignarFoco();
                }
                else if (Contexto.MermaPermitida == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.FleteMermaPermitidaEdicion_MsgMermaPermitidaRequerida;
                    dtuMermaPermitida.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.FleteMermaPermitidaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int fleteMermaPermitidaId = Contexto.FleteMermaPermitidaID;

                    var fleteMermaPermitidaBL = new FleteMermaPermitidaBL();
                    FleteMermaPermitidaInfo fleteMermaPermitida = fleteMermaPermitidaBL.ObtenerPorDescripcion(Contexto);

                    if (fleteMermaPermitida != null && (fleteMermaPermitidaId == 0 || fleteMermaPermitidaId != fleteMermaPermitida.FleteMermaPermitidaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.FleteMermaPermitidaEdicion_MsgDescripcionExistente, fleteMermaPermitida.FleteMermaPermitidaID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                      SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }


        /// <summary>
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var fleteMermaPermitidaBL = new FleteMermaPermitidaBL();
                    fleteMermaPermitidaBL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.FleteMermaPermitidaID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.FleteMermaPermitida_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.FleteMermaPermitida_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos
    }
}
