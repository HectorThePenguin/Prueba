using System.Collections.Generic;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Info.Filtros
{
   public  class FiltroGenerarArchivoDataLink
    {
       public string Mensaje { get; set; }
       public IList<GridRepartosModel>  DatosDataLink { get; set; }
       public int TotalKilosEmbarcados { get; set; }
       public int TotalKilosRepartidos { get; set; }
       public int TotalSobrante { get; set; }
       public int MermaReparto { get; set; }
       public string TotalTiempoViaje { get; set; }
    }
}
