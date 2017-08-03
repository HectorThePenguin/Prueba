using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using SIE.Services.Info.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace SIE.WinForm.Auxiliar
{
    public static class Extensor
    {
        /// <summary>
        /// Convierte un dato string a entero
        /// </summary>
        /// <returns></returns>
        public static int ValorEntero(string valor)
        {
            int resultado;
            int.TryParse(valor, out resultado);
            return resultado;
        }

        /// <summary>
        /// Regresa un valor fecha en formato de SQL server
        /// </summary>
        /// <returns></returns>
        public static string ObtenerFormatoFecha(string valor)
        {
            string returnValue = string.Empty;

            if (!string.IsNullOrWhiteSpace(valor))
            {
                var culture = new CultureInfo("es-MX");
                returnValue = Convert.ToDateTime(valor, culture).ToString("yyyy-MM-dd");
            }
            return returnValue;
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo números
        /// </summary>
        /// <param name="valor">entrada de texto</param>
        /// <returns>bool</returns>
        public static bool ValidarSoloNumeros(string valor)
        {
            
            int ascci = (valor != string.Empty) ?  Convert.ToInt32(Convert.ToChar(valor)) : 0;
            return !string.IsNullOrWhiteSpace(valor) && (ascci < 48 || ascci > 57);
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo letras
        /// </summary>
        /// <param name="valor">entrada de texto</param>
        /// <returns>bool</returns>
        public static bool ValidarSoloLetras(string valor)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(valor));

            return (ascci < 65 || ascci > 90) && (ascci < 97 || ascci > 122);
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo letras y números
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarSoloLetrasYNumeros(string valor)
        {
            if (valor != "")
            {
                int ascci = Convert.ToInt32(Convert.ToChar(valor));
                if ((ascci >= 65 && ascci <= 90) || (ascci >= 97 && ascci <= 122) || (ascci >= 48 && ascci <= 57))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Convierte un objeto en un ObservableCollection
        /// </summary>
        /// <param name="objetoConvertir">Objeto el Cual se convertirá a ObservableCollection</param>
        /// <returns>ObservableCollection T</returns>
        public static ObservableCollection<T> ConvertirAObservable<T>(this IEnumerable<T> objetoConvertir)
        {
            if (objetoConvertir == null)
            {
                return null;
            }
            var coleccionObservable = new ObservableCollection<T>();
            foreach (var item in objetoConvertir)
            {
                coleccionObservable.Add(item);
            }
            return coleccionObservable;
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo numeros y decimales
        /// </summary>
        /// <param name="valor">entrada de texto</param>
        /// /// <param name="cadena">entrada de texto</param>
        /// <returns>bool</returns>
        public static bool ValidarSoloNumerosDecimales(string valor, String cadena)
        {
            var ascci = Convert.ToInt32(Convert.ToChar(valor));
            if (ascci == 8)
            {
                return false;
            }
            var isDec = false;
            var nroDec = 0;

            foreach (var t in cadena)
            {
                if (t == '.')
                    isDec = true;

                if (isDec && nroDec++ >= 2)
                {
                    return true;
                }
            }

            if (ascci >= 48 && ascci <= 57)
                return false;

            return ascci != 46 || (isDec);
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo numeros y decimales
        /// </summary>
        /// <param name="valor">entrada de texto</param>
        /// /// <param name="cadena">entrada de texto</param>
        /// <returns>bool</returns>
        public static bool ValidarSoloNumerosNegativosDecimales(string valor, String cadena)
        {
            cadena = string.Format("{0}{1}", valor, cadena);
            int vecesSigno = cadena.Count(x => x.Equals('-'));
            if (cadena.Contains("-"))
            {
                if (!cadena.StartsWith("-"))
                {
                    if (cadena.Length > 1)
                    {
                        return true;
                    }
                }
                if (vecesSigno > 1)
                {
                    return true;
                }
            }
            vecesSigno = cadena.Count(x => x.Equals('.'));
            if (cadena.Contains("."))
            {
                if (vecesSigno > 1)
                {
                    return true;
                }
            }
            var caracteresValidos = new[] {'.', '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
            return cadena.Any(caracter => !caracteresValidos.Contains(caracter));
        }

        /// <summary>
        /// Obtiene el valor booleano del Enumerador
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValorBooleanoDesdeEnum(this EstatusEnum valor)
        {
            return (valor == EstatusEnum.Activo);
        }

        /// <summary>
        /// Genera un Clone de la Instancia que se le envia
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Object ClonarInfo(Object info)
        {
            Object clone = Activator.CreateInstance(info.GetType(), null);
            Type ignorable = typeof(SIE.Services.Info.Atributos.AtributoIgnoraPropiedad);
            PropertyInfo[] properties =
                info.GetType().GetProperties().Where(e => e.GetCustomAttributes(ignorable, true).Count() == 0).ToArray();
            foreach (var propertyInfo in properties)
            {
                MethodInfo getMethod = info.GetType().GetProperty(propertyInfo.Name).GetGetMethod();
                if (getMethod != null)
                {
                    object valorPropiedad = info.GetType().GetProperty(propertyInfo.Name).GetValue(info, null);
                    if (valorPropiedad != null && valorPropiedad.GetType().Namespace.Contains(".Info.Info"))
                    {
                        valorPropiedad = ClonarInfo(valorPropiedad);
                    }
                    MethodInfo setMethod = clone.GetType().GetProperty(propertyInfo.Name).GetSetMethod();
                    if (setMethod != null)
                    {
                        clone.GetType().GetProperty(propertyInfo.Name).SetValue(clone, valorPropiedad, null);
                    }
                }
            }
            return clone;
        }

        public static bool ValidarNumeroYletras(string cadena)
        {
            var expresion = "^([a-z A-Z-9]*\\d*\\s*)*$";
            if (cadena.Length == 0)
            {
                return false;
            }
            var ex_reg = new Regex(expresion);
            return (ex_reg.IsMatch(cadena));
        }

        /// <summary>
        /// Valida para que solo permita capturar números.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static bool ValidarNumeros(string cadena)
        {
            const string expresion = "^([0-9]*\\d*\\s*)*$";
            if (cadena.Length == 0)
            {
                return false;
            }
            var exReg = new Regex(expresion);
            return (exReg.IsMatch(cadena));
        }

        public static bool ValidarLetrasConAcentos(string valor)
        {
            bool result = true;
            var caracter = Convert.ToChar(valor);
            int ascci = Convert.ToInt32(Convert.ToChar(valor));

            if (Char.IsLetter(caracter))
            {
                result = false;
            }
            else if (Char.IsControl(caracter))
            {
                result = false;
            }
            else if (Char.IsSeparator(caracter))
            {
                result = false;
            }
            else if ((ascci >= 97 && ascci <= 122)
                     || (ascci >= 240 && ascci <= 245)
                     || ascci == 265
                     || ascci == 220
                     || ascci == 326
                     || ascci == 340
                     || ascci == 351
                     || ascci == 244
                     || ascci == 345
                //|| ascci == 39
                )
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarNumerosLetrasConAcentos(string valor)
        {
            bool result = true;
            var caracter = Convert.ToChar(valor);
            int ascci = Convert.ToInt32(Convert.ToChar(valor));

            if (Char.IsLetter(caracter))
            {
                result = false;
            }
            else if (Char.IsControl(caracter))
            {
                result = false;
            }
            else if (Char.IsSeparator(caracter))
            {
                result = false;
            }
            else if ((ascci >= 97 && ascci <= 122)
                     || (ascci >= 240 && ascci <= 245)
                     || ascci == 265
                     || ascci == 220
                     || ascci == 326
                     || ascci == 340
                     || ascci == 351
                     || ascci == 244
                     || ascci == 345
                     || (ascci > 47 && ascci < 58)
                )
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarNumerosLetrasSinAcentos(string valor)
        {
            bool result = true;
            var caracter = Convert.ToChar(valor);
            int ascci = Convert.ToInt32(Convert.ToChar(valor));
            
            if (Char.IsControl(caracter))
            {
                result = false;
            }
            else if (Char.IsSeparator(caracter))
            {
                result = false;
            }
            else if ((ascci >= 97 && ascci <= 122)
                     || (ascci >= 65 && ascci <= 90)
                     || ascci == 265
                     || ascci == 220
                     || ascci == 326
                     || ascci == 340
                     || ascci == 351
                     || ascci == 244
                     || ascci == 345
                     || (ascci > 47 && ascci < 58)
                )
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo letras y números y guiones
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarSoloLetrasYNumerosConGuion(string valor)
        {
            if (valor != "")
            {
                int ascci = Convert.ToInt32(Convert.ToChar(valor));
                if ((ascci >= 65 && ascci <= 90) || (ascci >= 97 && ascci <= 122) || (ascci >= 48 && ascci <= 57)
                    || (ascci == 45))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo letras y números y guiones
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarSoloLetrasYNumerosConGuiones(string valor)
        {
            if (valor != "")
            {
                int ascci = Convert.ToInt32(Convert.ToChar(valor));
                if ((ascci >= 65 && ascci <= 90) || (ascci >= 97 && ascci <= 122) || (ascci >= 48 && ascci <= 57)
                    || (ascci == 45) || (ascci == 95))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo letras y números y guiones
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarSoloLetrasYNumerosConGuionParentesis(string valor)
        {
            if (valor != "")
            {
                int ascci = Convert.ToInt32(Convert.ToChar(valor));
                if ((ascci >= 65 && ascci <= 90) || (ascci >= 97 && ascci <= 122) || (ascci >= 48 && ascci <= 57)
                    || (ascci == 45) || (ascci == 40 || ascci == 41) || ascci == 46)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo letras y numeros y guiones
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarSoloLetrasYNumerosConPunto(string valor)
        {
            if (valor != "")
            {
                int ascci = Convert.ToInt32(Convert.ToChar(valor));
                if ((ascci >= 65 && ascci <= 90) || (ascci >= 97 && ascci <= 122) || (ascci >= 48 && ascci <= 57)
                    || (ascci == 46))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo letras y numeros y guiones
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarSoloNumerosConPunto(string valor)
        {
            if (valor != "")
            {
                int ascci = Convert.ToInt32(Convert.ToChar(valor));
                if ((ascci >= 48 && ascci <= 57)
                    || (ascci == 46))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Obtiene el nombre del mes en base a un nnumero entre 1 y 12
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string ObtenerNombreMes(int month)
        {
            if(month < 1 || month > 12)
                return string.Empty;

            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            string source = dtinfo.GetMonthName(month);

            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }

        public static DateTime SoloFecha(this DateTime me)
        {
            return DateTime.Parse(me.ToShortDateString(), System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat);
        }
        /// <summary>
        /// Valida que solo se ingresen numero letras con acento y guines
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarNumerosLetrasConAcentosGuion(string valor)
        {
            bool result = true;
            var caracter = Convert.ToChar(valor);
            int ascci = Convert.ToInt32(Convert.ToChar(valor));
            if (ascci == 231 || ascci==199)
            {
                return result;
            }

            if (Char.IsLetter(caracter))
            {
                result = false;
            }
            else if (Char.IsControl(caracter))
            {
                result = false;
            }
            else if (Char.IsSeparator(caracter))
            {
                result = false;
            }
            else if ((ascci >= 97 && ascci <= 122)
                     || (ascci >= 240 && ascci <= 245)
                     || ascci == 265
                     || ascci == 220
                     || ascci == 326
                     || ascci == 340
                     || ascci == 351
                     || ascci == 244
                     || ascci == 345
                     || ascci == 45
                     || (ascci > 47 && ascci < 58)
                )
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 
        /// Solo se permiten letras con acentos numeros . , : -
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarCaracterValidoTexo(string valor)
        {
            bool result = true;
            var caracter = Convert.ToChar(valor);
            int ascci = Convert.ToInt32(Convert.ToChar(valor));
            if (ascci == 231 || ascci == 199 || ascci == 44 || ascci == 58 || ascci == 46)
            {
                return false;
            }

            if (Char.IsLetter(caracter))
            {
                result = false;
            }
            else if (Char.IsControl(caracter))
            {
                result = false;
            }
            else if (Char.IsSeparator(caracter))
            {
                result = false;
            }
            else if ((ascci >= 97 && ascci <= 122)
                     || (ascci >= 240 && ascci <= 245)
                     || ascci == 265
                     || ascci == 220
                     || ascci == 326
                     || ascci == 340
                     || ascci == 351
                     || ascci == 244
                     || ascci == 345
                     || ascci == 45
                     || (ascci > 47 && ascci < 58)
                )
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Solo permite letra números puntos comas y guiones.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool ValidarLetraNumeroPuntoComaGuion(string valor)
        {
            const string expresion = @"[A-Za-z0-9ÁÉÍÓÚáéíóú.,-]";
            if (valor.Length == 0)
            {
                return false;
            }
            var exReg = new Regex(expresion);
            return (exReg.IsMatch(valor));
        }

        /// <summary>
        /// evento que evalua que la entrada del texto sea solo numeros y decimales
        /// </summary>
        /// <param name="valor">entrada de texto</param>
        /// /// <param name="cadena">entrada de texto</param>
        /// <returns>bool</returns>
        public static bool ValidarSoloNumerosDiagonal(string valor, String cadena)
        {
            cadena = string.Format("{0}{1}", valor, cadena);
            int vecesSigno = cadena.Count(x => x.Equals('/'));
            if (cadena.Contains("/"))
            {
                if (!cadena.StartsWith("/"))
                {
                    if (cadena.Length > 2)
                    {
                        return true;
                    }
                }
                if (vecesSigno > 2)
                {
                    return true;
                }
            }
            var caracteresValidos = new[] { '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            return cadena.Any(caracter => !caracteresValidos.Contains(caracter));
        }

        public static bool CompararObjetos(object contextoActual, object contextoAnterior, bool validarLista)
        {
            PropertyInfo[] propertiesActuales = contextoActual.GetType().GetProperties();
            var iguales = true;
            foreach (var propertyInfo in propertiesActuales)
            {
                MethodInfo getMethod = contextoActual.GetType().GetProperty(propertyInfo.Name).GetGetMethod();
                if (getMethod != null)
                {
                    object valorPropiedadActual = contextoActual.GetType().GetProperty(propertyInfo.Name).GetValue(contextoActual, null);
                    object valorPropiedadAnterior = contextoAnterior.GetType().GetProperty(propertyInfo.Name).GetValue(contextoAnterior, null);
                    if (valorPropiedadActual != null && valorPropiedadActual.GetType().Namespace.Contains(".Info.Info"))
                    {
                        valorPropiedadActual = CompararObjetos(valorPropiedadActual, valorPropiedadAnterior, validarLista);
                        valorPropiedadAnterior = CompararObjetos(valorPropiedadActual, valorPropiedadAnterior, validarLista);
                    }
                    TypeCode tipo = Type.GetTypeCode(propertyInfo.PropertyType);
                    switch (tipo)
                    {
                        case TypeCode.String:
                            if (string.IsNullOrWhiteSpace(Convert.ToString(valorPropiedadActual))
                                && string.IsNullOrWhiteSpace(Convert.ToString(valorPropiedadAnterior)))
                            {
                                continue;
                            }
                            break;
                    }
                    if (valorPropiedadActual == null && valorPropiedadAnterior == null)
                    {
                        continue;
                    }
                    if (valorPropiedadActual == null && valorPropiedadAnterior != null)
                    {
                        iguales = false;
                        break;
                    }
                    if (valorPropiedadActual != null && valorPropiedadAnterior == null)
                    {
                        iguales = false;
                        break;
                    }
                    if (!validarLista && (valorPropiedadActual is IList && valorPropiedadActual.GetType().IsGenericType))
                    {
                        continue;
                    }
                    if (!valorPropiedadActual.Equals(valorPropiedadAnterior))
                    {
                        iguales = false;
                        break;
                    }
                }
            }
            return iguales;
        }
    }
}