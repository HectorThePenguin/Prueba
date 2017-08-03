using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Modelos
{
    public class ImpresionCalidadMezcladoModel
    {
        /// <summary>
        /// Nomenclatura del formato
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// id de la calidad de mezclado
        /// </summary>
        public int CalidadMezcladoID { get; set; }
        /// <summary>
        /// Tecnica utilizada para el mezclado
        /// </summary>
        public string Tecnica { get; set; }
        /// <summary>
        /// Organizacion donde se genero el mezclado
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Formula que se mezclo
        /// </summary>
        public string Formula { get; set; }
        /// <summary>
        /// Fecha de la Premezcla
        /// </summary>
        public DateTime FechaPremezcla { get; set; }
        /// <summary>
        /// Fecha del Batch
        /// </summary>
        public DateTime FechaBatch { get; set; }
        /// <summary>
        /// Corral del mezclado
        /// </summary>
        public string Corral { get; set; }
        /// <summary>
        /// Tipo de lugar donde se genero la muestra 
        /// </summary>
        public string TipoLugarMuestra { get; set; }
        /// <summary>
        /// Chofer del mezclador
        /// </summary>
        public string Chofer { get; set; }
        /// <summary>
        /// Encargado de la Mezcladora
        /// </summary>
        public string EncargadoMezcladora { get; set; }
        /// <summary>
        /// Batch del mezclado
        /// </summary>
        public int Batch { get; set; }
        /// <summary>
        /// Tiempo que duro el mezclado
        /// </summary>
        public int TiempoMezclado { get; set; }
        /// <summary>
        /// Encargado de hacer sacar la muestra
        /// </summary>
        public string EncargadoMuestreo { get; set; }
        /// <summary>
        /// Gramos de Microtoxina
        /// </summary>
        public int GramosMicrotoxina { get; set; }
        /// <summary>
        /// Obtiene los detalles del mezclado
        /// </summary>
        public List<ImpresionCalidadMezcladoDetalleModel> Detalle { get; set; }

        public double PesoBHInicial { get; set; }
        public double PesoBSInicial { get; set; }
        public double MateriaSecaInicial { get; set; }
        public double HumedadInicial { get; set; }

        public double PesoBHMedia { get; set; }
        public double PesoBSMedia { get; set; }
        public double MateriaSecaMedia { get; set; }
        public double HumedadMedia { get; set; }

        public double PesoBHFinal { get; set; }
        public double PesoBSFinal { get; set; }
        public double MateriaSecaFinal { get; set; }
        public double HumedadFinal { get; set; }

        public double PorcentajeMateriaSecaInicial { get; set; }
        public double PorcentajeMateriaSecaMedia { get; set; }
        public double PorcentajeMateriaSecaFinal { get; set; }

        public double PorcentajeHumedadInicial { get; set; }
        public double PorcentajeHumedadMedia { get; set; }
        public double PorcentajeHumedadFinal { get; set; }

        public double PromedioPesoBh { get; set; }
        public double PromedioPesoBs { get; set; }
        public double PromedioMateriaSeca { get; set; }
        public double PromedioHumedad { get; set; }

        public double AnalisisInicialM1 { get; set; }
        public double AnalisisInicialM2 { get; set; }
        public double AnalisisInicialM3 { get; set; }

        public double AnalisisMediaM1 { get; set; }
        public double AnalisisMediaM2 { get; set; }
        public double AnalisisMediaM3 { get; set; }

        public double AnalisisFinalM1 { get; set; }
        public double AnalisisFinalM2 { get; set; }
        public double AnalisisFinalM3 { get; set; }

        public double ParticulasEsperadasInicialM1 { get; set; }
        public double ParticulasEsperadasInicialM2 { get; set; }
        public double ParticulasEsperadasInicialM3 { get; set; }

        public double ParticulasEsperadasMediaM1 { get; set; }
        public double ParticulasEsperadasMediaM2 { get; set; }
        public double ParticulasEsperadasMediaM3 { get; set; }

        public double ParticulasEsperadasFinalM1 { get; set; }
        public double ParticulasEsperadasFinalM2 { get; set; }
        public double ParticulasEsperadasFinalM3 { get; set; }

        public double ParticulasEncontradasInicialM1 { get; set; }
        public double ParticulasEncontradasInicialM2 { get; set; }
        public double ParticulasEncontradasInicialM3 { get; set; }

        public double ParticulasEncontradasMediaM1 { get; set; }
        public double ParticulasEncontradasMediaM2 { get; set; }
        public double ParticulasEncontradasMediaM3 { get; set; }

        public double ParticulasEncontradasFinalM1 { get; set; }
        public double ParticulasEncontradasFinalM2 { get; set; }
        public double ParticulasEncontradasFinalM3 { get; set; }

        public double PromedioMInicial { get; set; }
        public double PromedioMMedia { get; set; }
        public double PromedioMFinal { get; set; }

        public double ParticulasEsperadasMInicial { get; set; }
        public double ParticulasEsperadasMMedia { get; set; }
        public double ParticulasEsperadasMFinal { get; set; }

        public double PorcentajeEficienciaRecuperacionMInicial { get; set; }
        public double PorcentajeEficienciaRecuperacionMMedia { get; set; }
        public double PorcentajeEficienciaRecuperacionMFinal { get; set; }

        public double Promedio { get; set; }
        public double DesviacionEstandar { get; set; }
        public double CoeficienteVariacion { get; set; }
        public double EficanciaMezclado { get; set; }

    }
}
