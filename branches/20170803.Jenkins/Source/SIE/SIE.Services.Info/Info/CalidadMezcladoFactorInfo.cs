using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class CalidadMezcladoFactorInfo
    {
        /// <summary>
        /// Identificador de tipo muestra
        /// </summary>
        public int TipoMuestraID { get; set; }
        /// <summary>
        /// Muestra
        /// </summary>
        public string Muestra { get; set; }
        /// <summary>
        /// Particulas esperadas
        /// </summary>
        public decimal Factor { get; set; }
        /// <summary>
        /// Peso BH
        /// </summary>
        public int PesoBH { get; set; }
        /// <summary>
        /// Peso BH
        /// </summary>
        public int PesoBS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal MateriaSeca { get; set; }
        /// <summary>
        /// Porcentaje de humedad
        /// </summary>
        public decimal Humedad { get; set; }
        /// <summary>
        /// Habilitar peso bh
        /// </summary>
        public bool PesoBHHabilitado { get; set; }
        /// <summary>
        /// habilitar peso sh 
        /// </summary>
        public bool PesoSHHabilitado { get; set; }
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public int usuarioCreacion { get; set; }
        /// <summary>
        /// Fecha creacion
        /// </summary>
         public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario modifica
        /// </summary>
        public int UsuarioModifica { get; set; }
      }
}
