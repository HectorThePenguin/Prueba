using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Log;

namespace SIE.Base.Auxiliar
{
    public static class AuxDAL
    {
        /// <summary>
        /// Metodo para Obtener los Parametros que Son Dependencias para la Ayuda
        /// </summary>
        /// <param name="parametros">Diccionario con los Parametros No Dependientes</param>
        /// <param name="dependencias">Lista con los Parametros Dependientes</param>
        public static void ObtenerDependencias(IDictionary<string, object> parametros
                                               , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                if (parametros == null)
                {
                    parametros = new Dictionary<string, object>();
                }
                if (dependencias != null)
                {
                    foreach (IDictionary<IList<string>, object> dependencia in dependencias)
                    {
                        ICollection<IList<String>> keys = dependencia.Keys;
                        foreach (var item in keys)
                        {
                            var infoDependiente = dependencia[item];
                            if (infoDependiente != null)
                            {
                                var ns = infoDependiente.GetType().Namespace;
                                if (ns != null && ns.Contains("Collection"))
                                {
                                    infoDependiente = ((IEnumerable<Object>) infoDependiente).ElementAt(0);
                                }
                                foreach (string parameter in item)
                                {
                                    if (!parametros.ContainsKey(string.Format("@{0}", parameter)))
                                    {
                                        parametros.Add(string.Format("@{0}", parameter)
                                                       ,
                                                       infoDependiente.GetType().GetProperty(parameter).GetValue(
                                                           infoDependiente, null));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo para Obtener los Parametros que Son Dependencias para la Ayuda
        /// </summary>
        /// <param name="parametros">Diccionario con los Parametros No Dependientes</param>
        /// <param name="dependencias">Lista con los Parametros Dependientes</param>
        public static void Centros_ObtenerDependencias(IDictionary<string, object> parametros
                                               , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                if (parametros == null)
                {
                    parametros = new Dictionary<string, object>();
                }
                if (dependencias != null)
                {
                    foreach (IDictionary<IList<string>, object> dependencia in dependencias)
                    {
                        ICollection<IList<String>> keys = dependencia.Keys;
                        foreach (var item in keys)
                        {
                            var infoDependiente = dependencia[item];
                            if (infoDependiente != null)
                            {
                                var ns = infoDependiente.GetType().Namespace;
                                if (ns != null && ns.Contains("Collection"))
                                {
                                    infoDependiente = ((IEnumerable<Object>)infoDependiente).ElementAt(0);
                                }
                                foreach (string parameter in item)
                                {
                                    if (!parametros.ContainsKey(string.Format("@{0}", parameter)))
                                    {
                                        if (parameter.Equals("OrganizacionID"))
                                        {
                                            parametros.Add(string.Format("@{0}", parameter),infoDependiente);
                                        }
                                        else
                                        {
                                            parametros.Add(string.Format("@{0}", parameter)
                                                          ,
                                                          infoDependiente.GetType().GetProperty(parameter).GetValue(
                                                              infoDependiente, null));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}
