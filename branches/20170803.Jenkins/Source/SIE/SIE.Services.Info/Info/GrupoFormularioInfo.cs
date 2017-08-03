
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class GrupoFormularioInfo
    {
        /// <summary> 
        ///	GrupoID  
        /// </summary> 
        public GrupoInfo Grupo { get; set; }

        /// <summary> 
        ///	FormularioID  
        /// </summary> 
        public FormularioInfo Formulario { get; set; }

        /// <summary> 
        ///	AccesoID  
        /// </summary> 
        public AccesoInfo Acceso { get; set; }

        /// <summary> 
        ///	FormularioID  
        /// </summary> 
        public List<FormularioInfo> ListaFormulario { get; set; }


    }
}
