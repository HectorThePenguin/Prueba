using System;
using BLToolkit.Mapping;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("PesajeMateriaPrima")]
    public class PesajeMateriaPrimaInfo
    {
        /// <summary>
        /// Identificador del pesaje
        /// </summary>
        public int PesajeMateriaPrimaID { get; set; }
        /// <summary>
        /// Identificacion de la programacion de materia prima
        /// </summary>
        public int ProgramacionMateriaPrimaID { get; set; }
        /// <summary>
        /// Identificador del chofer
        /// </summary>
        public int ProveedorChoferID { get; set; }
        /// <summary>
        /// Ticket
        /// </summary>
        public int Ticket { get; set; }
        /// <summary>
        /// Identificador del camion
        /// </summary>
        public int CamionID { get; set; }
        /// <summary>
        /// Peso bruto
        /// </summary>
        public int PesoBruto { get; set; }
        /// <summary>
        /// Peso tara
        /// </summary>
        public int PesoTara { get; set; }
        /// <summary>
        /// Piezas
        /// </summary>
        public int Piezas { get; set; }
        /// <summary>
        /// Identificador del tipo de pesaje
        /// </summary>
        public int TipoPesajeID { get; set; }
        /// <summary>
        /// Identificador del usuario que surtio
        /// </summary>
        public int UsuarioIDSurtido { get; set; }
        /// <summary>
        /// Fecha del surtido
        /// </summary>
        public DateTime FechaSurtido { get; set; }
        /// <summary>
        /// Identificador del usuario que recibe
        /// </summary>
        public int UsuarioIDRecibe { get; set; }
        /// <summary>
        /// Fecha que recibe
        /// </summary>
        public DateTime FechaRecibe { get; set; }
        /// <summary>
        /// Identificador del estatus del pesaje
        /// </summary>
        public int EstatusID { get; set; }
        /// <summary>
        /// Indica el estatus del registro
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Descripcion del tipo de pesaje
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore]
        [MapIgnore]
        public string TipoPesajeDescripcion { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario logueado en la aplicacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }
        /// <summary>
        /// Usuario que modifica el registro
        /// </summary>
        public int UsuarioModificacionID { get; set; }

        /// <summary>
        /// Se le asigna el proveedor que le corresponde segun el proveedor chofer
        /// </summary>
        public ProveedorChoferInfo ProveedorChofer { get; set; }

        /// <summary>
        /// Se le asigna el camion que le corresponde segun el CamionId
        /// </summary>
        public CamionInfo Camion { get; set; }

        /// <summary>
        /// Se le asigna el movimiento de Salida
        /// </summary>
        public long AlmacenMovimientoOrigenId { get; set; }

        /// <summary>
        /// Se le asigna el movimiento de Entrada
        /// </summary>
        public long AlmacenMovimientoDestinoId { get; set; }

        /// <summary>
        /// Sele asigna el PedidoID
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore]
        [MapIgnore]
        public int PedidoID { get; set; }
    }
}
