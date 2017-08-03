using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para ParametroTrampaClonar.xaml
    /// </summary>
    public partial class ParametroTrampaClonar
    {
        private bool confirmaSalir = true;
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FiltroClonarParametroTrampa Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroClonarParametroTrampa)DataContext;
            }
            set { DataContext = value; }
        }

        public ParametroTrampaClonar()
        {
            InitializeComponent();
            InicializaContexto();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var trampaPL = new TrampaPL();
            var organizacionPL = new OrganizacionPL();

            skAyudaTrampaOrigen.ObjetoNegocio = trampaPL;
            skAyudaTrampaDestino.ObjetoNegocio = trampaPL;
            skAyudaOrganizacionOrigen.ObjetoNegocio = organizacionPL;
            skAyudaOrganizacionDestino.ObjetoNegocio = organizacionPL;

            skAyudaOrganizacionOrigen.AyudaConDatos += (o, args) =>
            {
                Contexto.TrampaOrigen.Organizacion.ListaTiposOrganizacion = ObtenerTipoOrganizacionGanadera();
            };
            skAyudaOrganizacionDestino.AyudaConDatos += (o, args) =>
            {
                Contexto.TrampaDestino.Organizacion.ListaTiposOrganizacion = ObtenerTipoOrganizacionGanadera();
            };

            skAyudaTrampaOrigen.AyudaConDatos += (o, args) =>
            {
                Contexto.TrampaOrigen.Organizacion.ListaTiposOrganizacion = ObtenerTipoOrganizacionGanadera();
            };
            skAyudaTrampaDestino.AyudaConDatos += (o, args) =>
            {
                Contexto.TrampaDestino.Organizacion.ListaTiposOrganizacion = ObtenerTipoOrganizacionGanadera();
            };
        }

        private List<TipoOrganizacionInfo> ObtenerTipoOrganizacionGanadera()
        {
            return new List<TipoOrganizacionInfo>
                                                         {
                                                             new TipoOrganizacionInfo
                                                                 {
                                                                     TipoOrganizacionID =
                                                                         Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode()
                                                                 }
                                                         };
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guardar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ParametroTrampa_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar,
                                                            MessageBoxButton.YesNo,
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
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaClonar();

            if (guardar)
            {
                try
                {
                    var parametroTrampaPL = new ParametroTrampaPL();
                    Contexto.UsuarioCreacionID = Auxiliar.AuxConfiguracion.ObtenerUsuarioLogueado();
                    parametroTrampaPL.ClonarParametroTrampa(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    confirmaSalir = false;
                    Close();
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.ParametroTrampa_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.ParametroTrampa_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaClonar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (Contexto.TrampaOrigen == null || Contexto.TrampaOrigen.TrampaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroTrampaEdicion_MsgTipoParametroRequerido;
                    skAyudaTrampaOrigen.AsignarFoco();
                }
                else if (Contexto.TrampaDestino == null || Contexto.TrampaDestino.TrampaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroTrampaEdicion_MsgParametroIDRequerida;
                    skAyudaTrampaDestino.AsignarFoco();
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
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroClonarParametroTrampa
            {
                TrampaOrigen = new TrampaInfo
                    {
                        Organizacion = new OrganizacionInfo
                            {
                                ListaTiposOrganizacion = ObtenerTipoOrganizacionGanadera()
                            }
                    },
                TrampaDestino = new TrampaInfo
                {
                    Organizacion = new OrganizacionInfo
                    {
                        ListaTiposOrganizacion = ObtenerTipoOrganizacionGanadera()
                    }
                },
            };
        }
    }
}
