using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class EntradaGanadoTipoInfo
    {
        /// <summary>
        ///     Identificador de la entrada ganado tipo 
        /// </summary>
        public int EntradaGanadoTipoID { get; set; }

        /// <summary>
        ///     Identificador de la entrada ganado costeo
        /// </summary>
        public int EntradaGanadoCosteoID { get; set; }

        /// <summary>
        ///     Peso de origen
        /// </summary>
        public decimal PesoOrigen { get; set; }

        /// <summary>
        ///     Identificador del tipo de ganado
        /// </summary>
        public int TipoGanadoID { get; set; }

        /// <summary>
        ///     Número de cabezas
        /// </summary>
        public int Cabezas { get; set; }

        /// <summary>
        ///     Precio
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        ///     Importe del costo
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        ///    Indica si el registro  se encuentra Activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        ///     Usuario que creo el registro.
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        ///     Usuario que modifica el registro .
        /// </summary>
        public int UsuarioModificacionID { get; set; }

    }
}
