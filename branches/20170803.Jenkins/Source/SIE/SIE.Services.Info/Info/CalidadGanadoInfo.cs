using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class CalidadGanadoInfo : BitacoraInfo
    {
        private string descripcion;
        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int? CalidadGanadoID { get; set; }
     
        /// <summary>
        /// Identificador del sexo del ganado
        /// </summary>
        public Sexo Sexo { get; set; }

        /// <summary>
        /// Descripción de la calida de ganado
        /// </summary>
        public string Descripcion
        {
            get { return descripcion != null ? descripcion.Trim() : descripcion; }
            set
            {
                if (value != descripcion)
                {
                    descripcion = value != null ? value.Trim() : null;
                }
            }
        }

        /// <summary>
        /// Valor de la calidad de Ganado
        /// </summary>
        public string Calidad { get; set; }

        /// <summary>
        /// Valor de la clasificacion del Ganado En Linea, Produccion o Venta
        /// </summary>
        public string ClasificacionCalidad { get; set; }

        /// <summary>
        /// Id de la calidad Hembra (pantalla Calificacion de Ganado)
        /// </summary>
        public int CalidadGanadoHembraID { get; set; }

        /// <summary>
        /// Id de la calidad Macho (pantalla Calificacion de Ganado)
        /// </summary>
        public int CalidadGanadoMachoID { get; set; }
    }
}
