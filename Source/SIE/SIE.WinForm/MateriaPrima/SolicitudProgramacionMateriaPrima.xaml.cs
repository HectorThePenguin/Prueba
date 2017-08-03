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
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para SolicitudProgramacionMateriaPrima.xaml
    /// </summary>
    public partial class SolicitudProgramacionMateriaPrima : Window
    {
        #region Atributos
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        private int usuarioId;
        /// <summary>
        /// Identificador de la organizacion del usuario 
        /// </summary>
        private int organizacionID;
        /// <summary>
        /// autorizacionMateriaPrimaInfo
        /// </summary>
        private AutorizacionMateriaPrimaInfo autorizacionMateriaPrimaInfo;
        /// <summary>
        /// Info de pedido
        /// </summary>
        private PedidoInfo pedido;
        /// <summary>
        /// Detalle de pedido
        /// </summary>
        private PedidoDetalleInfo detallePedido;

        private ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo;


        private AlmacenInventarioLoteInfo almacenInventarioLote;
        #endregion
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoInfo"></param>
        /// <param name="detalleInfo"></param>
        /// <param name="almacenInventarioLoteinfo"></param>
        public SolicitudProgramacionMateriaPrima(PedidoInfo pedidoInfo, PedidoDetalleInfo detalleInfo,AlmacenInventarioLoteInfo almacenInventarioLoteinfo)
        {
            programacionMateriaPrimaInfo= new ProgramacionMateriaPrimaInfo();
            almacenInventarioLote = almacenInventarioLoteinfo;
            pedido = pedidoInfo;
            detallePedido = detalleInfo;
            organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            autorizacionMateriaPrimaInfo =  new AutorizacionMateriaPrimaInfo();
            InitializeComponent();
            Instrucciones.Content =
                string.Format(Properties.Resources.SolicitudProgramacionMateriaPrima_RequiereJustificacion
                    ,detallePedido.LoteUso,detallePedido.LoteSelecionado);
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Enviar solicitud de autorizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnviar_OnClick(object sender, RoutedEventArgs e)
        {
            try{
                if (txtJustificacion.Text.Trim() != string.Empty)
                {
                    var almacenInventarioPl = new AlmacenInventarioLotePL();
                    var autorizacionMateriaPrimaInfo = CargarDatosAutorizacion();
                     programacionMateriaPrimaInfo = CargarDatosProgramacion();

                     almacenInventarioPl.GuardarAutorizacionMateriaPrimaProgramacion(autorizacionMateriaPrimaInfo, programacionMateriaPrimaInfo);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          string.Format(Properties.Resources.SolicitudProgramacionMateriaPrima_MsjSolicitudEnviada,pedido.FolioPedido),
                                          MessageBoxButton.OK,
                                          MessageImage.Correct);
                    Close();

                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          string.Format(Properties.Resources.SolicitudProgramacionMateriaPrima_MsjRequiereJustificacion), 
                                          MessageBoxButton.OK,
                                          MessageImage.Warning);
                    txtJustificacion.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.SolicitudProgramacionMateriaPrima_errorEnviarSolicitud,
                       MessageBoxButton.OK, MessageImage.Error);
            }
            
        }

        private ProgramacionMateriaPrimaInfo CargarDatosProgramacion()
        {
            return programacionMateriaPrimaInfo = new ProgramacionMateriaPrimaInfo
            {
                Almacen = pedido.Almacen,
                InventarioLoteOrigen = almacenInventarioLote,
                PedidoDetalleId = detallePedido.PedidoDetalleId,
                Organizacion = new OrganizacionInfo { OrganizacionID = int.Parse(Application.Current.Properties["OrganizacionID"].ToString()) },
                CantidadProgramada = almacenInventarioLote.CantidadProgramada,
                UsuarioCreacion = new UsuarioInfo { UsuarioID = int.Parse(Application.Current.Properties["UsuarioID"].ToString()) }
            };
        }

        private AutorizacionMateriaPrimaInfo CargarDatosAutorizacion()
        {
           return autorizacionMateriaPrimaInfo = new AutorizacionMateriaPrimaInfo
            {
                OrganizacionID = organizacionID,
                TipoAutorizacionID = TipoAutorizacionEnum.UsoLote.GetHashCode(),
                Folio = pedido.FolioPedido,
                Justificacion = txtJustificacion.Text,
                Lote = detallePedido.LoteSelecionado,
                Precio = detallePedido.InventarioLoteDestino.PrecioPromedio,
                Cantidad = detallePedido.InventarioLoteDestino.Cantidad,
                ProductoID = detallePedido.Producto.ProductoId,
                AlmacenID = detallePedido.InventarioLoteDestino.AlmacenInventario.AlmacenID,
                EstatusID = Estatus.AMPPendien.GetHashCode(),
                UsuarioCreacion = usuarioId,
                Activo = EstatusEnum.Activo.GetHashCode()
            };
        }

        /// <summary>
        /// Cancelar la solicitud de autorizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.SolicitudProgramacionMateriaPrima_MsjCancelar,
               MessageBoxButton.YesNo, MessageImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        #endregion
    }
}
