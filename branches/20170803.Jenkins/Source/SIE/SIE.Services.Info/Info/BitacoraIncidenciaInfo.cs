using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class BitacoraIncidenciaInfo
    {
        /// <summary>
        /// Identificador unico de la bitacora
        /// </summary>
        public int BitacoraIncidenciaID { get; set; }

        /// <summary>
        /// Identificador de la alerta
        /// </summary>
        public AlertaInfo Alerta { get; set; }

        /// <summary>
        /// Folio generado de la incidencia
        /// </summary>
        public int Folio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Error { get; set; }

    }
}