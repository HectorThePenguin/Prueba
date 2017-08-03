using System;
using System.Collections.Generic;
using System.Security;

namespace SIE.Services.Info.Info
{
    public class EmbarqueInfo : BitacoraInfo
    {
        /// <summary>
        ///   Identificador del embarque.
        /// </summary>
        public int EmbarqueID { set; get; }

        /// <summary>
        ///     Identificador de la organización.
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        ///     Folio del embarque
        /// </summary>
        public int FolioEmbarque { set; get; }

        /// <summary>
        ///     Identificador del tipo de embarque
        /// </summary>
        public TipoEmbarqueInfo TipoEmbarque { set; get; }
        
        /// <summary>
        ///     Identificador del estatus del embarque
        /// </summary>
        public int Estatus { set; get; }
        
        /// <summary>
        ///     Detalle de los embarques
        /// </summary>
        public IList<EmbarqueDetalleInfo> ListaEscala { set; get; }

        /// <summary>
        ///     Campo donde se almacena la descripción del estatus en el que se encuentra el embarque.
        /// </summary>
        public string DescripcionEstatus { get; set; }

        /// <summary>
        ///     Comprador del embarque.
        /// </summary>
        public string ResponsableEmbarque { get; set; }

        /// <summary>
        ///     Cita de carga del embarque.
        /// </summary>
        public DateTime CitaCarga { get; set; }

        /// <summary>
        ///     Cita de Descarga del embarque.
        /// </summary>
        public DateTime CitaDescarga { get; set; }

        /// <summary>
        ///    Acceso FechaCitaDescargaString
        /// </summary>
        public string FechaCitaDescargaString
        {
            get
            {
                string regreso = String.Empty;
                if (CitaDescarga.Year != 1900)
                    regreso = CitaDescarga.ToString("dd/MM/yyyy HH:mm");
                return regreso;
            }
        }


        /// <summary>
        ///     Campo donde se almacena el total de jaulas programadas.
        /// </summary>
        public int JaulasProgramadas { set; get; }

        /// <summary>
        ///    Acceso FechaCitaCargaString
        /// </summary>
        public string FechaCitaCargaString
        {
            get
            {
                string regreso = String.Empty;
                if (CitaCarga.Year != 1900)
                    regreso = CitaCarga.ToString("dd/MM/yyyy HH:mm");
                return regreso;
            }
        }

        /// <summary>
        ///    Acceso FechaCitaCargaStringJaula
        /// </summary>
        public string FechaCitaCargaStringJaula
        {
            get
            {
                string regreso = String.Empty;
                if (CitaCarga.Year != 1900)
                    regreso = CitaCarga.ToString("yyyy/MM/dd HH:mm");
                return regreso;
            }
        }

        /// <summary>
        /// Hora de carga del embarque.
        /// </summary>
        public DateTime HoraCarga { get; set; }
        
        /// <summary>
        /// Ruteo que lleva el embarque.
        /// </summary>
        public RuteoInfo Ruteo { get; set; }

        /// <summary>
        /// Observaciones referentes al embarque 
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Fecha de inicio
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Hora de Transito del embarque.
        /// </summary>
        public int? HorasTransito { get; set; }

        /// <summary>
        /// Observaciones del embarque.
        /// </summary>
        public ObservacionInfo Observacion { get; set; }

        /// <summary>
        /// Observaciones del embarque.
        /// </summary>
        public List<EmbarqueRuteoInfo> EmbarqueRuteoDetalle { get; set; }

        /// <summary>
        /// Transportista del embarque
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Configuracion con la que cuenta el embarque
        /// </summary>
        public  ConfiguracionEmbarqueInfo ConfiguracionEmbarque { get; set; }

        /// <summary>
        /// Campo para validar si no tiene datos de transporte
        /// </summary>
        public bool PendienteTransporte { get; set; }

        /// <summary>
        /// Lista de los costos del embarque
        /// </summary>
        public List<EmbarqueCostoInfo>  Costos { get; set; }

        /// <summary>
        /// Lista de los gastos fijos del embarque
        /// </summary>
        public List<EmbarqueGastosFijosInfo> GastosFijos { get; set; }

        /// <summary>
        /// Campo para validar si cuenta con uno o dos transportistas
        /// </summary>
        public bool DobleTransportista { get; set; }

        /// <summary>
        /// Campo que almacena el chofer que conducirá el embarque.
        /// </summary>
        public ChoferInfo Operador1 { get; set; }

        /// <summary>
        /// Campo que almacena el segundo chofer que conducirá en caso de tener doble transportista
        /// </summary>
        public ChoferInfo Operador2 { get; set; }

        /// <summary>
        /// Campo que almacena la información del camión perteneciente al embarque.
        /// </summary>
        public CamionInfo Tracto { get; set; }

        /// <summary>
        /// Campo que almacena la información de la jaula perteneciente al embarque.
        /// </summary>
        public JaulaInfo Jaula { get; set; }

        /// <summary>
        /// Campo que indica si hay informacion capturada en la seccion de datos del embarque.
        /// </summary>
        public bool DatosCapturados { get; set; }
    }
}
