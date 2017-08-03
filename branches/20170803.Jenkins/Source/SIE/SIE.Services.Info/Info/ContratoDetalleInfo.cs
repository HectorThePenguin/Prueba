using System;
namespace SIE.Services.Info.Info
{
    public class ContratoDetalleInfo
    {
        /// <summary>
        /// Identificador del contrato detalle
        /// </summary>
        public int ContratoDetalleId { set; get; }

        /// <summary>
        /// ContratoId al que pertenece el contrato detalle
        /// </summary>
        public int ContratoId { set; get; }

        /// <summary>
        /// Indicador del contrato detalle
        /// </summary>
        public IndicadorInfo Indicador { set; get; }

        /// <summary>
        /// Rango minimo del indicador
        /// </summary>
        public decimal PorcentajePermitido { set; get; }

        /// <summary>
        /// Indica si esta activo o no el registro
        /// </summary>
        public Enums.EstatusEnum Activo { set; get; }

        /// <summary>
        /// Id de usuario que creo el contrato detalle
        /// </summary>
        public int UsuarioCreacionId { set; get; }

        /// <summary>
        /// Usuario de creacion del contrato detalle
        /// </summary>
        public int UsuarioModificacionId { set; get; }

        
        /// <summary>
        /// Porcentaje Humedad
        /// </summary>
        public decimal PorcentajeHumedad { set; get; }
        /// <summary>
        /// Fecha Inicio
        /// </summary>
        public String FechaInicio { set; get; }
        
    }
}
