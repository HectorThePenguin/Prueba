using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class InterfaceSalidaDetalleInfo
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
        /// Tipo de Ganado de la Salida
        /// </summary>
        public TipoGanadoInfo TipoGanado { get; set; }

        /// <summary>
        /// Cantidad de Cabezas de la Salida
        /// </summary>
        public int Cabezas { get; set; }

        /// <summary>
        /// Precio por Kilo
        /// </summary>
        public Decimal PrecioKG { get; set; }

        /// <summary>
        /// Importe de la Salida
        /// </summary>
        public Decimal Importe { get; set; }

        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Usuario de Registro
        /// </summary>
        public String UsuarioRegistro { get; set; }

        /// <summary>
        /// Interface Salida de Animal
        /// </summary>
        public List<InterfaceSalidaAnimalInfo> ListaInterfaceSalidaAnimal { get; set; }
    }
}
