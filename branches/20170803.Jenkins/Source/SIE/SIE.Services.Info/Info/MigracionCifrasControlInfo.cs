using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class MigracionCifrasControlInfo
    {
        /// <summary>
        /// Descripcion del paso en la migracion 1 --> Carga Resumen,  2 --> Carga Inicial,  3 --> Migracion
        /// </summary>
        public int Paso { get; set; }

        /// <summary>
        /// Descripcion del paso en la migracion 1 --> Carga Resumen,  2 --> Carga Inicial,  3 --> Migracion
        /// </summary>
        public string DescripcionPaso
        {
            get { return Paso == 1 ? "Carga Archivo Resumen." : 
                            Paso == 2 ? "Carga Control Individual --  SIAP temp." :
                                Paso == 3 ? "Carga SIAP temp --  SIAP." : "";
            }
            /*set
            {
                if (value != descripcion)
                {
                    descripcion = value != null ? value.Trim() : null;
                }
            }*/
        }

        /// <summary>
        /// Total de cabezas
        /// </summary>
        public long TotalCabezas { get; set; }

        /// <summary>
        /// Total de costos
        /// </summary>
        public double TotalCostos { get; set; }

        
    }
}
