using System;
using System.Collections.Generic;
using SIE.Services.Info.Atributos;
using System.Reflection;

namespace SIE.WinForm.Auxiliar
{
    public static class AuxAyuda
    {
        /// <summary>
        /// Agrega los filtros de Ayuda
        /// </summary>
        /// <param name="esNumerico">Verifica si el Valor es Numero</param>
        /// <param name="valorFiltro">Valor del Filtro</param>
        /// <param name="tiposParametros">Tipo de Datos del Parametro</param>
        /// <param name="valoresParametros">Valor del Parametro</param>
        public static void AgregarFiltros(bool esNumerico, Object valorFiltro
                                          , List<Type> tiposParametros, List<Object> valoresParametros)
        {
            if (esNumerico)
            {
                tiposParametros.Add(typeof (int));
                valoresParametros.Add(Convert.ToInt32(valorFiltro));
            }
            else
            {
                tiposParametros.Add(valorFiltro.GetType());
                valoresParametros.Add(valorFiltro);
            }
        }

        /// <summary>
        /// Verifica que las Dependencias Tengan Valores Validos
        /// </summary>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        public static HashSet<String> ValidaDependencias(IList<IDictionary<IList<String>, Object>> dependencias)
        {
            var resultado = new HashSet<String>();
            if (dependencias != null)
            {
                for (int indexDependencias = 0; indexDependencias < dependencias.Count; indexDependencias++)
                {
                    foreach (var llave in dependencias[indexDependencias].Keys)
                    {
                        for (int indexLlave = 0; indexLlave < llave.Count; indexLlave++)
                        {
                            object valor = dependencias[indexDependencias][llave];
                            if (valor == null)
                            {
                                resultado.Add(llave[indexLlave]);
                            }
                        }
                    }
                }
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene el Metodo a Ejecutar
        /// </summary>
        /// <param name="info"></param>
        /// <param name="propiedadBindeo"></param>
        /// <param name="popUp"> </param>
        /// <param name="valorAtributoPropiedad"> </param>
        /// <returns></returns>
        public static String ObtenerMetodoEjecutar(Object info, string propiedadBindeo, bool popUp, string valorAtributoPropiedad)
        {
            var propInfoResult = string.Empty;
            dynamic customAttributes = info.GetType().GetProperty(propiedadBindeo).GetCustomAttributes(typeof(AtributoAyuda), true);
            if (customAttributes.Length > 0)
            {
                for (var indexAtributos = 0; indexAtributos < customAttributes.Length; indexAtributos++)
                {
                    if (valorAtributoPropiedad.Equals(customAttributes[indexAtributos].Nombre))
                    {
                        if (customAttributes[indexAtributos].PopUp == popUp)
                        {
                            propInfoResult = customAttributes[indexAtributos].MetodoInvocacion;
                        }
                    }
                }
            }
            return propInfoResult;
        }

        /// <summary>
        /// Obtiene el Encabezado del Grid
        /// </summary>
        /// <param name="info"></param>
        /// <param name="propiedadBindeo"></param>
        /// <param name="valorAtributoPropiedad"> </param>
        /// <returns></returns>
        public static String ObtenerEncabezadoGrid(Object info, string propiedadBindeo, string valorAtributoPropiedad)
        {
            var propInfoResult = string.Empty;
            dynamic customAttributes = info.GetType().GetProperty(propiedadBindeo).GetCustomAttributes(typeof(AtributoAyuda), true);
            if (customAttributes.Length > 0)
            {
                for (var indexAtributos = 0; indexAtributos < customAttributes.Length; indexAtributos++)
                {
                    if (valorAtributoPropiedad.Equals(customAttributes[indexAtributos].Nombre))
                    {
                        propInfoResult = customAttributes[indexAtributos].EncabezadoGrid;
                        break;
                    }                    
                }
            }
            return propInfoResult;
        }

        /// <summary>
        /// Obtiene si la propiedad esta
        /// en algun contenedor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="propiedadBindeo"></param>
        /// <param name="valorAtributoPropiedad"> </param>
        /// <returns></returns>
        public static bool EstaEnContendor(Object info, string propiedadBindeo, string valorAtributoPropiedad)
        {
            bool propInfoResult = false;
            dynamic customAttributes = info.GetType().GetProperty(propiedadBindeo).GetCustomAttributes(typeof(AtributoAyuda), true);
            if (customAttributes.Length > 0)
            {
                for (var indexAtributos = 0; indexAtributos < customAttributes.Length; indexAtributos++)
                {
                    if (valorAtributoPropiedad.Equals(customAttributes[indexAtributos].Nombre))
                    {
                        propInfoResult = customAttributes[indexAtributos].EstaEnContenedor;
                        break;
                    }
                }
            }
            return propInfoResult;
        }

        /// <summary>
        /// Obtiene si la propiedad esta
        /// en algun contenedor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="propiedadBindeo"></param>
        /// <param name="valorAtributoPropiedad"> </param>
        /// <returns></returns>
        public static string ObtenerContenedor(Object info, string propiedadBindeo, string valorAtributoPropiedad)
        {
            string propInfoResult = string.Empty;
            dynamic customAttributes = info.GetType().GetProperty(propiedadBindeo).GetCustomAttributes(typeof(AtributoAyuda), true);
            if (customAttributes.Length > 0)
            {
                for (var indexAtributos = 0; indexAtributos < customAttributes.Length; indexAtributos++)
                {
                    if (valorAtributoPropiedad.Equals(customAttributes[indexAtributos].Nombre))
                    {
                        propInfoResult = customAttributes[indexAtributos].NombreContenedor;
                        break;
                    }
                }
            }
            return propInfoResult;
        }

        /// <summary>
        /// Obtiene la Propiedad de Bindeo
        /// </summary>
        /// <param name="info"></param>
        /// <param name="valorAtributoPropiedad"></param>
        /// <returns></returns>
        public static String ObtenerPropiedadBindeo(Object info, string valorAtributoPropiedad)
        {
            var propInfoResult = string.Empty;

            if (info != null)
            {
                var propiedades = info.GetType().GetProperties();
                foreach (var propInfo in propiedades)
                {
                    dynamic customAttributes = info.GetType().GetProperty(propInfo.Name).GetCustomAttributes(typeof(AtributoAyuda), true);
                    if (customAttributes.Length > 0)
                    {
                        for (var indexAtributos = 0; indexAtributos < customAttributes.Length; indexAtributos++)
                        {
                            if (valorAtributoPropiedad.Equals(customAttributes[indexAtributos].Nombre))
                            {
                                propInfoResult = propInfo.Name;
                                break;
                            }
                        }
                    }
                    if (!String.IsNullOrWhiteSpace(propInfoResult))
                    {
                        break;
                    }
                }
            }
            return propInfoResult;
        }

        /// <summary>
        /// Asgna al Info el valore de la consulta
        /// </summary>
        /// <param name="info"></param>
        /// <param name="resultado"></param>
        public static void AsignaValoresInfo(Object info, Object resultado)
        {
            var propiedades = resultado.GetType().GetProperties();
            foreach (var property in propiedades)
            {
                MethodInfo getMethod = resultado.GetType().GetProperty(property.Name).GetGetMethod();
                if (getMethod != null)
                {
                    var valorPropiedad = resultado.GetType().GetProperty(property.Name).GetValue(resultado, null);
                    MethodInfo setMethod = info.GetType().GetProperty(property.Name).GetSetMethod();
                    if (setMethod != null)
                    {
                        info.GetType().GetProperty(property.Name).SetValue(info, valorPropiedad, null);
                    }
                }
            }
        }

        /// <summary>
        /// Inicializa la propiedad
        /// </summary>
        /// <param name="info"></param>
        public static void InicializaPropiedad(Object info)
        {
            if (info != null)
            {
                var propiedades = info.GetType().GetProperties();
                foreach (var propInfo in propiedades)
                {
                    dynamic customAttributes =
                        info.GetType().GetProperty(propInfo.Name).GetCustomAttributes(
                            typeof (AtributoInicializaPropiedad), true);
                    if (customAttributes.Length > 0)
                    {
                        info.GetType().GetProperty(propInfo.Name).SetValue(info, 0, null);
                    }
                }
            }
        }
    }
}
