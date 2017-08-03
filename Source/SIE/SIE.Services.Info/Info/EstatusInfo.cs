using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
     [BLToolkit.DataAccess.TableName("Estatus")]
    public class EstatusInfo
    {
        /// <summary>
        /// Identificador del estatus
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        [BLToolkit.Mapping.MapField("EstatusID")]
        public int EstatusId { set; get; }

        /// <summary>
        /// Descripción del estatus
        /// </summary>
        public string Descripcion { set; get; }

        /// <summary>
        /// Descripción del estatus
        /// </summary>
        public TipoEstatus TipoEstatus { set; get; }
         
        /// <summary>
        /// Descripcion corta del status
        /// </summary>
        public string DescripcionCorta { set; get; }

        /// <summary>
        /// Indica si el registro  se encuentra Activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        [BLToolkit.DataAccess.NonUpdatable]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }
      
        public static List<EstatusInfo> ListFrom<T>()
        {
            var array = (T[])(Enum.GetValues(typeof(T)).Cast<T>());

            return array
              .Select(a => new EstatusInfo
              {
                  EstatusId = a.GetHashCode(),
                  Descripcion = a.ToString()
              })
                //.OrderBy(x => x.EstatusId)
               .ToList();
        }
    }
}

