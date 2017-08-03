using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class CheckListCorralInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador la tabla CheckListCorral
        /// </summary>
        public int CheckListCorralID { get; set; }

        /// <summary>
        /// Lote al Cual pertenece el Check List
        /// </summary>
        public int LoteID { get; set; }

        /// <summary>
        /// Indica el PDF que se generó para el Check List
        /// </summary>
        public byte[] PDF { get; set; }

        /// <summary>
        /// Indica la organizacion a la que pertenece el Check List
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Indica si el corral requiere revision
        /// </summary>
        public bool RequiereRevision { get; set; }

        #region Propiedades que no se Guardan

        /// <summary>
        /// Muestra el Corral
        /// </summary>
        public string Corral { get; set; }

        /// <summary>
        /// Muestra el Lote
        /// </summary>
        public string Lote { get; set; }

        /// <summary>
        /// Muestra la Fecha en que se hace el Check List
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Muestra la Fecha en que se realizó el Corte del Lote
        /// </summary>
        public DateTime FechaCorte { get; set; }

        /// <summary>
        /// Muestra las Cabezas capturadas en el Sistema
        /// </summary>
        public int CabezasSistema { get; set; }

        /// <summary>
        /// Muestra las Cabezas maximas en el Corral
        /// </summary>
        public int CapacidadCabezas { get; set; }

        /// <summary>
        /// Muestra las Cabezas capturadas en el Sistema
        /// </summary>
        public int CabezasRestantes { get; set; }

        /// <summary>
        /// Muestra la Fecha en que se realizó el Sacrificio del Lote
        /// </summary>
        public DateTime FechaSacrificio { get; set; }

        /// <summary>
        /// Muestra la Fecha en que fue abierto el Lote
        /// </summary>
        public DateTime FechaAbierto { get; set; }

        /// <summary>
        /// Muestra las cabezas que se captura en el CheckList
        /// </summary>
        public int CabezasConteo { get; set; }

        /// <summary>
        /// Muestra la Fecha en que se realizó el primer Reimplante
        /// </summary>
        public DateTime Fecha1Reimplante { get; set; }

        /// <summary>
        /// Muestra la Fecha en que fue cerrado el Lote
        /// </summary>
        public DateTime FechaCerrado { get; set; }

        /// <summary>
        /// Muestra el Porcentaje de ocupación del Lote
        /// </summary>
        public decimal Ocupacion { get; set; }

        /// <summary>
        /// Muestra la Fecha en que se realizó el segundo Reimplante
        /// </summary>
        public DateTime Fecha2Reimplante { get; set; }

        /// <summary>
        /// Muestra los días que estuvó en engorda el Lote
        /// </summary>
        public int DiasEngorda { get; set; }

        /// <summary>
        /// Muestra el tipo de ganado en el Lote
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Muestra la Raza del ganado en el Lote
        /// </summary>
        public string Raza { get; set; }

        /// <summary>
        /// Muestra los Conceptos para el CheckList
        /// </summary>
        public List<ConceptoInfo> ListaConceptos { get; set; }

        /// <summary>
        /// Muestra las Acciones para el CheckList
        /// </summary>
        public List<AccionInfo> ListaAcciones { get; set; }

        /// <summary>
        /// Indica si se puede cerrar el Lote
        /// </summary>
        public bool AplicaCerrado { get; set; }

        /// <summary>
        /// Indica el Estatus del Lote
        /// </summary>
        public int Estatus { get; set; }

        /// <summary>
        /// Indica el Estatus del Lote
        /// </summary>
        public int CabezasActuales { get; set; }

        /// <summary>
        /// Indica el Estatus del Lote
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Indica el Estatus del Lote
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Indica el Peso total de Corte del Lote
        /// </summary>
        public int PesoCorte { get; set; }

        /// <summary>
        /// Indica el Peso promedio de Compra
        /// </summary>
        public decimal PesoPromedioCompra { get; set; }

        /// <summary>
        /// Indica el Peso promedio de Llegada
        /// </summary>
        public decimal PesoPromedioLlegada { get; set; }

        /// <summary>
        /// Indica el Sexo del Animal
        /// </summary>
        public char SexoAnimal { get; set; }

        /// <summary>
        /// Indica el Porcentaje de Merma
        /// </summary>
        public decimal PorcentajeMerma { get; set; }

        /// <summary>
        /// Indica si se puede cerrar el Lote
        /// </summary>
        public bool CorralInvalidoFecha { get; set; }
        
        #endregion Propiedades que no se Guardan
    }
}
