using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class InterfaceSalidaAnimalInfo
    {
        /// <summary>
        /// Organizacion de la Salida
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Clave de la Salida
        /// </summary>
        public int SalidaID { get; set; }

        /// <summary>
        /// Arete que indentifica el Animal
        /// </summary>
        public String Arete { get; set; }

        /// <summary>
        /// Arete Metalico que indentifica el Animal
        /// </summary>
        public String AreteMetalico { get; set; }

        /// <summary>
        /// Fecha de Compra
        /// </summary>
        public DateTime FechaCompra { get; set; }

        /// <summary>
        /// Peso de Compra
        /// </summary>
        public Decimal PesoCompra { get; set; }

        /// <summary>
        /// Tipo de Ganado
        /// </summary>
        public TipoGanadoInfo TipoGanado { get; set; }

        /// <summary>
        /// Peso de Origen
        /// </summary>
        public Decimal PesoOrigen { get; set; }

        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Usuario de Registro
        /// </summary>
        public String UsuarioRegistro { get; set; }

        /// <summary>
        /// Lista de los Costos del Animal
        /// </summary>
        public List<InterfaceSalidaCostoInfo> ListaSalidaCostos { set; get; }

        /// <summary>
        /// Clave de la Partida
        /// </summary>
        public int Partida { get; set; }

        /// <summary>
        /// Clave de la Salida
        /// </summary>
        public int CorralID { get; set; }

        /// <summary>
        /// AnimalID con el que se registro
        /// </summary>
        public long AnimalID { get; set; }
    }
}
