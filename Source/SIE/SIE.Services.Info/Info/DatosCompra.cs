using System;


namespace SIE.Services.Info.Info
{
    public class DatosCompra
    {
        /// <summary>
        ///     Fecha de inicio .
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        ///     Origen.
        /// </summary>
        public String Origen { get; set; }

        /// <summary>
        ///     Proveedor.
        /// </summary>
        public String Proveedor { get; set; }

        /// <summary>
        ///     Tipo animal.
        /// </summary>
        public String TipoAnimal { get; set; }
        /// <summary>
        /// Tipo de origen
        /// </summary>
        public int TipoOrigen { get; set; }
    }
}
