using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class AnimalMovimientoInfo : BitacoraInfo
    {
        /// <summary>
        ///     Identificador AnimalID .
        /// </summary>
        public long AnimalID { get; set; }

        /// <summary>
        ///     Identificador AnimalMovimientoID .
        /// </summary>
        public long AnimalMovimientoID { get; set; }

        /// <summary>
        ///     Identificador OrganizacionID .
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        ///     Identificador CorralID .
        /// </summary>
        public int CorralID { get; set; }

        /// <summary>
        ///     Identificador CorralID .
        /// </summary>
        public int CorralIDOrigen { get; set; }

        /// <summary>
        ///     Identificador LoteID .
        /// </summary>
        public int LoteID { get; set; }

        /// <summary>
        ///     Identificador LoteID .
        /// </summary>
        public int LoteIDOrigen { get; set; }

        /// <summary>
        ///     Identificador FechaMovimiento .
        /// </summary>
        public DateTime FechaMovimiento { get; set; }

        /// <summary>
        ///     Identificador Peso .
        /// </summary>
        public int Peso { get; set; }

        /// <summary>
        ///     Identificador Temperatura .
        /// </summary>
        public double Temperatura { get; set; }

        /// <summary>
        ///     Identificador TipoMovimientoID .
        /// </summary>
        public int TipoMovimientoID { get; set; }

        /// <summary>
        ///     Identificador TipoMovimiento.
        /// </summary>
        public TipoMovimientoInfo TipoMovimiento { get; set; }

        /// <summary>
        ///     Identificador TrampaID .
        /// </summary>
        public int TrampaID { get; set; }

        /// <summary>
        ///     Identificador Trampa.
        /// </summary>
        public TrampaInfo Trampa { get; set; }

        /// <summary>
        ///     Identificador OperadorID .
        /// </summary>
        public int OperadorID { get; set; }

        /// <summary>
        ///     Identificador Observaciones .
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        ///     Identificador FechaCreacion .
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        ///     Identificador FechaModificacion .
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Folio de entrada del Animal
        /// </summary>
        public long FolioEntrada { get; set; }

        /// <summary>
        /// Tipo de la Organizacion
        /// </summary>
        public int TipoOrganizacionID { get; set; }

        //DescripcionLoteOrigen
        /// <summary>
        ///     Identificador Observaciones .
        /// </summary>
        public string DescripcionLoteOrigen { get; set; }

        /// <summary>
        /// Movimiento ID anterior
        /// </summary>
        public long AnimalMovimientoIDAnterior { get; set; }

        /// <summary>
        /// Tipo de Corral del Lote
        /// </summary>
        public int TipoCorralID { get; set; }

        /// <summary>
        /// Grupo de Corral del Lote
        /// </summary>
        public long GrupoCorralID { get; set; }

        /// <summary>
        /// Grupo de Corral del Lote
        /// </summary>
        public GrupoCorralEnum GrupoCorralEnum { get; set; }

        /// <summary>
        /// Corral de Origen
        /// </summary>
        public string CorralOrigen { get; set; }

        /// <summary>
        /// Arete del Animal
        /// </summary>
        public string Arete { get; set; }

        /// <summary>
        /// Lote de Origen
        /// </summary>
        public LoteInfo LoteOrigen { get; set; }

        /// <summary>
        /// Lote de Origen
        /// </summary>
        public LoteInfo LoteDestino { get; set; }

        /// <summary>
        /// Se agrego para almacenar la lista de productos aplicados en cada movimiento
        /// </summary>
        public List<TratamientoAplicadoInfo> ListaTratamientosAplicados { get; set; }

        /// <summary>
        /// Usuario Responsable del movimiento
        /// </summary>
        public UsuarioInfo Usuario { get; set; }

    }
}
