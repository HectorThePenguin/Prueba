using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class EntradaCondicionInfo
    {
        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int EntradaCondicionID { get; set; }

        /// <summary>
        /// Campo padre llave de la entrada
        /// </summary>
        public int EntradaGanadoID { get; set; }

        /// <summary>
        /// Identificador de la Condicion de ganado 
        /// </summary>
        public int CondicionID { get; set; }

        /// <summary>
        /// Descripcion de la condicion de ganado
        /// </summary>
        public string CondicionDescripcion { get; set; }

        /// <summary>
        /// Numero de cabezas
        /// </summary>
        public int Cabezas { get; set; }

        /// <summary>
        /// bandera que indica que esta activo el registro
        /// </summary>
        public EstatusEnum Activo { get; set; }        

        /// <summary>
        /// ID del usuario que modifico el registro 
        /// </summary>
        public int UsuarioID { get; set; }

        /// <summary>
        /// Registro modificado
        /// </summary>
        public bool RegistroModificado { get; set; }

    }
}