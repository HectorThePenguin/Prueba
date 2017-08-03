using System;
using System.Reflection;
using SIE.Base.Exepciones;

namespace SIE.Services.Integracion.DAL.Excepciones
{
    public class ExcepcionServicio : ExcepcionGenerica
    {
        public ExcepcionServicio(string mensaje,
                                 Exception excepcion)
            : base(mensaje, excepcion)
        {
        }

        public ExcepcionServicio(MethodBase method, Exception excepcion)
            : base(method.Name, excepcion)
        {
        }

        public ExcepcionServicio(string mensaje)
            : base(mensaje)
        {
        }
    }
}