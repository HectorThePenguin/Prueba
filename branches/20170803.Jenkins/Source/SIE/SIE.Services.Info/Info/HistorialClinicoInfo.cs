using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class HistorialClinicoInfo
    {
        /// <summary>
        /// Identificador de la deteccion
        /// </summary>
        public int DeteccionId { get; set; }
        /// <summary>
        /// Identificador del animal
        /// </summary>
        public long AnimalMovimientoId { get; set; }
        /// <summary>
        /// Fecha de entrada a enfermeria
        /// </summary>
        public DateTime FechaEntrada { get; set; }
        /// <summary>
        /// Fecha salida de enfermeria
        /// </summary>
        public DateTime FechaSalida { get; set; }
        /// <summary>
        /// Fecha de salida con formato
        /// </summary>
        public string FechaSalidaFormateada
        {
            get
            {
                var fechaDefault = new DateTime(0001, 1, 1);
                return FechaSalida.Equals(fechaDefault) ? "" : FechaSalida.ToShortDateString();
            }
            
        }
        /// <summary>
        /// Lista de problemas detectados
        /// </summary>
        public IList<ProblemaInfo> ListaProblemas { get; set; }

        /// <summary>
        /// Cadena con la descripcion de los problemas detectados
        /// </summary>
        public string Problemas
        {
            get
            {
                var resultado = "";
                if (ListaProblemas != null)
                {
                    resultado = ListaProblemas.Where(problema => problema.isCheked).Aggregate(resultado, (current, problema) => current + (problema.Descripcion + ", "));
                    if (resultado.Length > 0)
                    {
                        resultado = resultado.Remove(resultado.Length - 2, 2);
                    }
                    if (Tratamiento.Length > 0)
                    {
                        Tratamiento = Tratamiento.Remove(Tratamiento.Length - 2, 2);
                    }
                    if (CodigoTratamiento.Length > 0)
                    {
                        CodigoTratamiento = CodigoTratamiento.Remove(CodigoTratamiento.Length - 2, 2);
                    }
                    
                }
                return resultado;
            }
           
        }

        /// <summary>
        /// Tratamiento aplicado
        /// </summary>
        public string Tratamiento { get; set; }

        /// <summary>
        /// Tratamiento aplicado
        /// </summary>
        public string CodigoTratamiento { get; set; }

        /// <summary>
        /// Costo general de los tratamientos aplicados
        /// </summary>
        public decimal Costo { get; set; }

        /// <summary>
        /// Dias que permanecio en la enfermeria
        /// </summary>
        public int DiasInstancia
        {
            get
            {
                var resultado = 0;
                if (FechaEntrada == null || FechaSalida == null) return resultado;
                var fechaDefault = new DateTime(0001, 1, 1);
                if (FechaSalida.Equals(fechaDefault)) return resultado;
                var ts = FechaSalida -FechaEntrada ;
                resultado = ts.Days;

                return resultado;
            }
        }

        /// <summary>
        /// Peso en la entrada de enfermeria
        /// </summary>
        public int Peso { get; set; }
        /// <summary>
        /// Temperatura en la entrada de enfermeria
        /// </summary>
        public decimal Temperatura { get; set; }
        /// <summary>
        /// Grado de enfermedad en la deteccion
        /// </summary>
        public GradoInfo GradoEnfermedad { get; set; }
        /// <summary>
        /// Identificador de la organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
    }
}
