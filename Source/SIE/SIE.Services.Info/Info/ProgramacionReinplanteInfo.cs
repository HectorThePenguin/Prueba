using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ProgramacionReinplanteInfo : BitacoraInfo
    {
        public int FolioProgramacionID{get;set;}
		public int OrganizacionID{get;set;}
		public DateTime Fecha{get;set;}
		public int LoteID{get;set;}
		public List<CorralInfo> CorralDestino{get;set;}
		public int ProductoID{get;set;}
    }
}
