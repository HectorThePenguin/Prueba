using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class IncidenciaSeguimientoInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int IncidenciaSeguimientoID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FechaVencimientoAnterior { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AccionInfo Accion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AccionInfo AccionAnterior { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comentarios { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EstatusInfo EstatusAnterior { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UsuarioInfo UsuarioResponsableAnterior { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UsuarioInfo UsuarioResponsable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NivelAlertaInfo NivelAlertaAnterior { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NivelAlertaInfo NivelAlertaActual { get; set; }
    }
}
