using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class OrdenSacrificioDetalleInfo
    {

        /// <summary>
        /// Identificador del detalle de la orden Sacrificio
        /// </summary>
        public int OrdenSacrificioDetalleID { get; set; }
        /// <summary>
        /// Identificado de la orden de sacrificio
        /// </summary>
        public int OrdenSacrificioID { get; set; }
        /// <summary>
        /// Folio de la orden de sacrificio
        /// </summary>
        public int FolioOrdenSacrificio { get; set; }
        /// <summary>
        /// Corraleta
        /// </summary>
        public CorralInfo Corraleta { get; set; }
        /// <summary>
        /// Codigo de la corraleta
        /// </summary>
        public string CorraletaCodigo { get; set; }
        /// <summary>
        /// corral donde se encuentra el ganado
        /// </summary>
        public CorralInfo Corral { get; set; } 
        /// <summary>
        /// Lote
        /// </summary>
        public LoteInfo Lote { get; set; }
        /// <summary>
        /// Cabezas disponible
        /// </summary>
        public int Cabezas { get; set; }
        /// <summary>
        /// Dias de engorda en grano
        /// </summary>
        public int DiasEngordaGrano { get; set; }
        /// <summary>
        /// Tipo de ganado y calidad
        /// </summary>
        public string Clasificacion { get; set; }
        /// <summary>
        /// Dias de retiro
        /// </summary>
        public int DiasRetiro { get; set; }
        /// <summary>
        /// Cabezas a sacrificar
        /// </summary>
        public int CabezasASacrificar { get; set; }
        /// <summary>
        /// Proveedor dependiendo del tipo de ganado intensivo o enfermería
        /// </summary>
        public ProveedorInfo Proveedor { get; set; } 
        /// <summary>
        /// Turno actual
        /// </summary>
        public TurnoEnum Turno { get; set; }
        /// <summary>
        /// Lista de los turnos
        /// </summary>
        public IList<TurnoEnum> Turnos { get; set; }

        /// <summary>
        ///     Identificador FechaCreacion .
        /// </summary>
        public DateTime? FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo la orden.
        /// </summary>
        public UsuarioInfo Usuario { get; set; } 

        public bool OrigenCorraleta { get; set; }
        /// <summary>
        /// Estatus del detalle
        /// </summary>
        public string Estatus {
            get
            {
                var estatus = "";

                if (Cabezas == CabezasASacrificar)
                {
                    estatus = "FIN";
                }
                else
                {
                    estatus = (Cabezas - CabezasASacrificar).ToString();
                }
                return estatus;
            }
        }

		/// <summary>
        /// Proporciona el orden dentro del grid
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Indica si los botones de edicion se activiran
        /// </summary>
        public bool Activo { get; set; }
        /// <summary>
        /// Indica el Tipo de Ganado
        /// </summary>
        public int TipoGanadoID { get; set; }

        /// <summary>
        /// Folio Salida
        /// </summary>
        public int FolioSalida { get; set; }

        /// <summary>
        /// Proveedor del detalle de la orden Sacrificio
        /// </summary>
        public string ProveedorID { get; set; }

        /// <summary>
        /// Usuario del detalle  de la orden Sacrificio
        /// </summary>
        public int? UsuarioCreacion { get; set; }

        /// <summary>
        /// Última fecha  que se modificó el registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

        /// <summary>
        /// Último usuario que modificó el registro
        /// </summary>
        public int? UsuarioModificacion { get; set; }

        public override string ToString()
        {
            return Turno.ToString();
        }
        /// <summary>
        /// Indica si contempla guardarse
        /// </summary>
        public int Seleccionar { get; set; }

        /// <summary>
        /// Indica si contempla guardarse
        /// </summary>
        public bool Seleccionable { get; set; }

        /// <summary>
        /// Indica las cabezas actuales del Lote
        /// </summary>
        public int CabezasActuales { get; set; }
    }
}
