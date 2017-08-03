using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ConciliaciionParametros
    {
        /// <summary>
        /// 
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime fechaInicio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime fechaFin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> diviciones { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> claveDocumento { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  List<string> clavesPolizas { get; set; } 


        public List<string>Cuentas { get; set; } 

        /// <summary>
        /// Prefijo del numero de cuenta
        /// </summary>
        public string Prefijo { get; set; }
        
    }
}
