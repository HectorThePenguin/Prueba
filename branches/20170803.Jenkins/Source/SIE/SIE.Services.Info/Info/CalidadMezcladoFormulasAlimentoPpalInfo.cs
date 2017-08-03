using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class CalidadMezcladoFormulasAlimentoPpalInfo
    {
    /// <summary>
    /// Hace referencia al tipo de muestras (inicial, media o final) que se van a realizar
    /// </summary>
    public string AnalisisMuestras { get; set; }

    /// <summary>
    /// lista (principal) que contiene una sublista (detalles)
    /// </summary>
    /// 
    public IList<CalidadMezcladoFormulasAlimentoInfo> GridInterior {get; set; }
    }
}

