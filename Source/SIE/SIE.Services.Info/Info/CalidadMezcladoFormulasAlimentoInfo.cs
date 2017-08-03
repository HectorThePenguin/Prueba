using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class CalidadMezcladoFormulasAlimentoInfo
    {
        /// <summary>
        /// Lista donde se carga de la base de datos las opciones del combobox "Analisis de muestras"
        /// </summary>
        public string AnalisisMuestraCombobox { get; set; }

        /// <summary>
        /// Lista donce se carga de la base de datos, las opciones del combobox "Tecnica"
        /// </summary>
        public string Tecnica { get; set; }

        /// <summary>
        /// Hace referencia al tipo de muestras (inicial, media o final) que se van a realizar
        /// </summary>
        public string AnalisisMuestras { get; set; }

        /// <summary>
        /// Hace referencia a la descripción de la muestra analizada.
        /// </summary>
        public string NumeroMuestras { get; set; }

        /// <summary>
        /// Hace referencia al peso en gramos de la muestra analizada.
        /// </summary>
        public int PesoGramos { get; set; }

        /// <summary>
        /// vañor calculado de acurdo a la formula especificada en el analiis
        /// </summary>
        public decimal ParticulasEsperadas { get; set; }

        /// <summary>
        /// Hace referencia al número de partículas encontradas durante el análisis por número de muestra.
        /// </summary>
        public decimal ParticulasEncontradas { get; set; }

        /// <summary>
        /// Hace referencia al promedio de las partículas encontradas en los números de muestra analizados
        /// </summary>
        public string Promedio { get; set; }

        /// <summary>
        /// Identificacion de calidad de mezclado
        /// </summary>
        public int CalidadMezcladoID { get; set; }
        /// <summary>
        /// Organizacion
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// identificador tipo tecnica
        /// </summary>
        public int TipoTecnicaID { get; set; }
        /// <summary>
        /// Laboratorista 
        /// </summary>
        public UsuarioInfo UsuarioLaboratotorista { get; set; }
        /// <summary>
        /// Fecha del check list
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Formulas
        /// </summary>
        public FormulaInfo Formula { get; set; }
        /// <summary>
        /// Fecha premezcla
        /// </summary>
        public DateTime FechaPremezcla { get; set; }
        /// <summary>
        /// Fecha batch
        /// </summary>
        public DateTime FechaBatch { get; set; }
        /// <summary>
        /// Lugar de toma
        /// </summary>
        public int LugarToma { get; set; }
        /// <summary>
        /// Camion Reparto
        /// </summary>
        public CamionRepartoInfo CamionReparto { get; set; }
        /// <summary>
        /// Chofer
        /// </summary>
        public ChoferInfo Chofer { get; set; }
        /// <summary>
        /// Mezcladora
        /// </summary>
        public MezcladoraInfo Mezcladora { get; set; }
        /// <summary>
        /// Operador
        /// </summary>
        public OperadorInfo Operador { get; set; }
        /// <summary>
        /// Bach
        /// </summary>
        public int Batch { get; set; }
        /// <summary>
        /// Tiempo mezclado
        /// </summary>
        public int TiempoMezclado { get; set; }
        /// <summary>
        /// Persona muestreo
        /// </summary>
        public OperadorInfo PersonaMuestreo { get; set; }
        /// <summary>
        /// gramos mricrot por ton
        /// </summary>
        public int GramosMicrot { get; set; }
        /// <summary>
        /// Info corral
        /// </summary>
        public CorralInfo CorralInfo { get; set; }
        /// <summary>
        /// Lote id
        /// </summary>
        public int LoteID { get; set; }
        /// <summary>
        /// Se usa para indicar si el registro proviene de la Base de datos o proviene del Grid en pantlla (memoria)
        /// </summary>
        public bool BD { get; set;  }
    }
}
