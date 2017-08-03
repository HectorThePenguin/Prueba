

using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ValidacionDataLink
    {
        /// <summary>
        /// Fecha del reparto
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Ruta del archivo
        /// </summary>
        public string RutaArchivo { get; set; }
        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string NombreArchivo { get; set; }
        /// <summary>
        /// Tipo de servicio
        /// </summary>
        public TipoServicioEnum TipoServicio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DataLinkInfo> ListaDataLink { get; set; }

        /// <summary>
        /// Ruta del respaldo archivo
        /// </summary>
        public string RutaRespaldo { get; set; }
    }
}
