using System;
using System.Collections.Generic;
namespace SIE.Services.Info.Info
{
    public class ContenedorEmbarqueInfo
    {  public EmbarqueInfo Embarque { get; set; }
        public EmbarqueDetalleInfo EmbarqueDetalle { get; set; }

        public ContenedorEmbarqueInfo()
        {
            Embarque = new EmbarqueInfo
                {
                    TipoEmbarque = new TipoEmbarqueInfo(),
                    Organizacion = new OrganizacionInfo(),
                    ListaEscala = new List<EmbarqueDetalleInfo>()
                };
            EmbarqueDetalle = new EmbarqueDetalleInfo
                {
                    FechaSalida = DateTime.Now,
                    FechaLlegada = DateTime.Now,
                    ListaCostoEmbarqueDetalle = new List<CostoEmbarqueDetalleInfo>()
                };

        }
    }
}
