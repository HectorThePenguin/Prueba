using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for MermaSuperavitEdicion.xaml
    /// </summary>
    public partial class MermaSuperavitEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private MermaSuperavitInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (MermaSuperavitInfo)DataContext;
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
        public MermaSuperavitEdicion()
        {
            InitializeComponent();
            CargarOrganizaciones();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad MermaSuperavit Existente
        /// </summary>
        /// <param name="mermaSuperavitInfo"></param>
        public MermaSuperavitEdicion(MermaSuperavitInfo mermaSuperavitInfo)
        {
            InitializeComponent();
            CargarOrganizaciones();
            mermaSuperavitInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            cmbOrganizacion.IsEnabled = false;
            Contexto = mermaSuperavitInfo;
        }

        #endregion Constructores

        #region Eventos
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            skAyudaProducto.ObjetoNegocio = new ProductoPL();
            
            skAyudaAlmacen.ObjetoNegocio = new AlmacenPL();
            skAyudaProducto.AyudaConDatos += (o, args) =>
                                                 {
                                                     Contexto.Producto.AlmacenID = Contexto.Almacen.AlmacenID;
                                                 };
            skAyudaAlmacen.AyudaConDatos += AsignarValoresAlmacen;

            skAyudaProducto.PuedeBuscar += () => Contexto.Almacen.AlmacenID > 0;
            skAyudaAlmacen.AsignarFoco();
        }

        private void AsignarValoresAlmacen(object sender, EventArgs e)
        {
            int organizacionID =
                AuxConfiguracion.ObtenerOrganizacionUsuario();
            var almacen = skAyudaAlmacen.Contexto as AlmacenInfo;
            almacen.Organizacion = new OrganizacionInfo
                                       {
                                           OrganizacionID = organizacionID
                                       };
            almacen.FiltroTipoAlmacen =
                string.Format("{0}|{1}|{2}|{3}",
                              TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode(),
                              TipoAlmacenEnum.MateriasPrimas.GetHashCode(),
                              TipoAlmacenEnum.BodegaDeTerceros.GetHashCode(),
                              TipoAlmacenEnum.BodegaExterna.GetHashCode());
            almacen.ListaTipoAlmacen = new List<TipoAlmacenInfo>
                                           {
                                               new TipoAlmacenInfo
                                                   {
                                                       TipoAlmacenID =
                                                           TipoAlmacenEnum.
                                                           PlantaDeAlimentos
                                                           .GetHashCode()
                                                   },
                                               new TipoAlmacenInfo
                                                   {
                                                       TipoAlmacenID =
                                                           TipoAlmacenEnum.
                                                           MateriasPrimas.
                                                           GetHashCode()
                                                   },
                                                    new TipoAlmacenInfo
                                                    {
                                                        TipoAlmacenID = TipoAlmacenEnum.BodegaDeTerceros.GetHashCode()
                                                    },
                                                    new TipoAlmacenInfo
                                                    {
                                                        TipoAlmacenID = TipoAlmacenEnum.BodegaExterna.GetHashCode()
                                                    }
                                           };
            Contexto.Producto.AlmacenID = almacen.AlmacenID;
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

        private void DecimalUpDownPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string valor = ((DecimalUpDown)sender).Value.HasValue
                   ? ((DecimalUpDown)sender).Value.ToString()
                   : string.Empty;
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, valor);
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            Contexto = new MermaSuperavitInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Almacen = new AlmacenInfo
                               {
                                   Organizacion = new OrganizacionInfo
                                   {
                                       OrganizacionID = organizacionID
                                   },
                                   FiltroTipoAlmacen =
                                       string.Format("{0}|{1}|{2}|{3}",
                                                     TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode(),
                                                     TipoAlmacenEnum.MateriasPrimas.GetHashCode(),
                                                     TipoAlmacenEnum.BodegaDeTerceros.GetHashCode(),
                                                     TipoAlmacenEnum.BodegaExterna.GetHashCode()),
                                   ListaTipoAlmacen = new List<TipoAlmacenInfo>
                                                                        {
                                                                            new TipoAlmacenInfo
                                                                                {
                                                                                    TipoAlmacenID =
                                                                                        TipoAlmacenEnum.
                                                                                        PlantaDeAlimentos.GetHashCode()
                                                                                },
                                                                            new TipoAlmacenInfo
                                                                                {
                                                                                    TipoAlmacenID =
                                                                                        TipoAlmacenEnum.MateriasPrimas.
                                                                                        GetHashCode()
                                                                                },
                                                                                new TipoAlmacenInfo
                                                                                    {
                                                                                        TipoAlmacenID = TipoAlmacenEnum.BodegaDeTerceros.GetHashCode()
                                                                                    },
                                                                                    new TipoAlmacenInfo
                                                                                        {
                                                                                            TipoAlmacenID = TipoAlmacenEnum.BodegaExterna.GetHashCode()
                                                                                        }
                                                                        }
                               },
                               Producto = new ProductoInfo(),
                               Activo = EstatusEnum.Activo
                           };
            skAyudaAlmacen.AsignarFoco();
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
                if (Contexto.Almacen.AlmacenID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.MermaSuperavitEdicion_MsgAlmacenIDRequerida;
                    skAyudaAlmacen.AsignarFoco();
                }
                else if (Contexto.Producto.ProductoId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.MermaSuperavitEdicion_MsgProductoIDRequerida;
                    skAyudaProducto.AsignarFoco();
                }
                //else if (Contexto.Merma == 0)
                //{
                //    resultado = false;
                //    mensaje = Properties.Resources.MermaSuperavitEdicion_MsgMermaRequerida;
                //    dtuMerma.Focus();
                //}
                //else if (Contexto.Superavit == 0)
                //{
                //    resultado = false;
                //    mensaje = Properties.Resources.MermaSuperavitEdicion_MsgSuperavitRequerida;
                //    dtuSuperavit.Focus();
                //}
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.MermaSuperavitEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int mermaSuperavitId = Contexto.MermaSuperavitId;

                    var mermaSuperavitPL = new MermaSuperavitPL();
                    MermaSuperavitInfo mermaSuperavit = mermaSuperavitPL.ObtenerPorDescripcion(Contexto);
                    if (mermaSuperavit != null && (mermaSuperavitId == 0 || mermaSuperavitId != mermaSuperavit.MermaSuperavitId))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.MermaSuperavitEdicion_MsgDescripcionExistente, mermaSuperavit.MermaSuperavitId);
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
                    var mermaSuperavitPL = new MermaSuperavitPL();
                    mermaSuperavitPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.MermaSuperavitId != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.MermaSuperavit_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.MermaSuperavit_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga las organizaciones
        /// </summary>
        private void CargarOrganizaciones()
        {
            try
            {
                bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
                //int tipoOrganizacion = organizacion.TipoOrganizacion.TipoOrganizacionID;
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var organizacionesPl = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPl.ObtenerTipoGanaderas();
                if (usuarioCorporativo)
                {

                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReporteConsumoProgramadovsServido_cmbSeleccione,
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    cmbOrganizacion.SelectedIndex = 0;
                    cmbOrganizacion.IsEnabled = true;

                }
                else
                {
                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = organizacionId,
                            Descripcion = nombreOrganizacion
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    cmbOrganizacion.SelectedIndex = 0;
                    cmbOrganizacion.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this,
                    Properties.Resources.ReporteMedicamentosAplicadosSanidad_ErrorCargarOrganizaciones,
                    MessageBoxButton.OK, MessageImage.Error);

            }
        }

        #endregion Métodos
    }
}

