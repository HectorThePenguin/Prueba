using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class EmbarqueDetalleInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private decimal? kilometros;

        /// <summary>
        ///   Identificador del detalle de embarque.
        /// </summary>
        public int EmbarqueDetalleID { set; get; }

        /// <summary>
        ///   Identificador del embarque.
        /// </summary>
        public int EmbarqueID { set; get; }

        /// <summary>
        ///   Identificador del proveedor con tipo transportista.
        /// </summary>
        public ProveedorInfo Proveedor { set; get; }

        /// <summary>
        ///   Identificador del chofer.
        /// </summary>
        public ChoferInfo Chofer { set; get; }


        /// <summary>
        ///   Identificador de la placa de la jaula.
        /// </summary>
        public JaulaInfo Jaula { set; get; }

        /// <summary>
        ///   Identificador de la placa del camión
        /// </summary>
        public CamionInfo Camion { set; get; }

        /// <summary>
        ///   Identificador del origen del embarque
        /// </summary>
        public OrganizacionInfo OrganizacionOrigen { set; get; }

        /// <summary>
        ///   Identificador del destino del embarque
        /// </summary>
        public OrganizacionInfo OrganizacionDestino { set; get; }

        /// <summary>
        ///   Fecha de la salida del embarque
        /// </summary>
        public DateTime FechaSalida { set; get; }

        /// <summary>
        ///   Fecha tentativa de llegada
        /// </summary>
        public DateTime FechaLlegada { set; get; }

        /// <summary>
        ///   Kilometros que existen entre el origen y el destino.
        /// </summary>
        public int Orden { set; get; }

        /// <summary>
        ///   Kilometros que existen entre el origen y el destino.
        /// </summary>
        public bool Recibido { set; get; }

        /// <summary>
        ///   Horas que existen entre el origen y el destino.
        /// </summary>
        public decimal Horas { set; get; }

        /// <summary>
        ///     comentarios del embarque
        /// </summary>
        public string Comentarios { set; get; }

        /// <summary>
        ///     Lista de Costos de Embarque
        /// </summary>
        public List<CostoEmbarqueDetalleInfo> ListaCostoEmbarqueDetalle { set; get; }

        /// <summary>
        /// Kilometros que existen entre el origen y el destino
        /// </summary>
        public decimal? Kilometros
        {
            get { return kilometros; }
            set
            {
                if (value != kilometros)
                {
                    kilometros = value;
                    NotifyPropertyChanged("Kilometros");
                }
            }
        }

        #region ICloneable Members

        public EmbarqueDetalleInfo Clone()
        {
            var objetoCopiar = new EmbarqueDetalleInfo
                                   {
                                       EmbarqueDetalleID = EmbarqueDetalleID,
                                       EmbarqueID = EmbarqueID,
                                       Proveedor = Proveedor,
                                       Chofer = Chofer,
                                       Jaula = Jaula,
                                       Camion = Camion,
                                       OrganizacionOrigen = OrganizacionOrigen,
                                       OrganizacionDestino = OrganizacionDestino,
                                       FechaSalida = FechaSalida,
                                       FechaLlegada = FechaLlegada,
                                       Orden = Orden,
                                       Recibido = Recibido,
                                       Activo = Activo,
                                       UsuarioCreacionID = UsuarioCreacionID,
                                       UsuarioModificacionID = UsuarioModificacionID,
                                       Horas = Horas,
                                       Comentarios = Comentarios,
                                       ListaCostoEmbarqueDetalle = ListaCostoEmbarqueDetalle
                                   };
            return objetoCopiar;
        }
        #endregion

        #region Miembros de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
