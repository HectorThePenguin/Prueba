using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using SIE.Base.Log;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxGeneral
    {
        /// <summary>
        /// Convierte un valor boleando a un valor de tipo EstatusEnum
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static EstatusEnum BoolAEnum(this bool valor)
        {
            return valor ? EstatusEnum.Activo : EstatusEnum.Inactivo;
        }

        /// <summary>
        /// Convierte un valor boleano a un valor de tipo CompraParcialEnum
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static CompraParcialEnum BoolCompraParcialAEnum(this bool valor)
        {
            return valor ? CompraParcialEnum.Parcial : CompraParcialEnum.Total;
        }

        /// <summary>
        /// Convierte un valor booleano a un valor tipo FleteMermaPermitidaEnum
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static FleteMermaPermitidaEnum BoolFleteMermaPeritidaAEnum(this bool valor)
        {
            return valor ? FleteMermaPermitidaEnum.Interno : FleteMermaPermitidaEnum.Externo;
        }

        internal static string Serialize<T>(IList<T> info)
        {
            // serialize data
            var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(info.GetType(), new XmlRootAttribute("ROOT"));
            var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            try
            {
                using (var stream = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(stream, settings))
                    {
                        serializer.Serialize(writer, info, emptyNamepsaces);

                        var encoding = new System.Text.UnicodeEncoding();
                        byte[] bytestosend = encoding.GetBytes(stream.ToString());
                        string sText = encoding.GetString(bytestosend);

                        return sText;
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
        /// Convierte un valor boleando a un valor de tipo EstatusEnum
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static AbonoA StringAEnum(this string valor)
        {
            var resultado = AbonoA.AMBOS;

            if (String.Compare(valor.Trim(), AbonoA.CUENTA.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                resultado = AbonoA.CUENTA;
            }
            else
            {
                if (String.Compare(valor.Trim(), AbonoA.PROVEEDOR.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    resultado = AbonoA.PROVEEDOR;
                }
            }
            return resultado;
        }

        /// <summary>
        /// Convierte un valor boleando a un valor de tipo EstatusEnum
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static DiasSemana DiaSemanaAEnum(this string valor)
        {
            var resultado = (DiasSemana)Enum.Parse(typeof (DiasSemana), valor.ToUpper());            
            return resultado;
        }

        /// <summary>
        /// Convierte un valor boleando a un valor de tipo EstatusEnum
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static OpcionGastoInventarioEnum TipoGastoAEnum(this string valor)
        {
            var resultado = (OpcionGastoInventarioEnum)Enum.Parse(typeof(OpcionGastoInventarioEnum), valor);
            return resultado;
        }

        /// <summary>
        /// Convierte un entero al enumerador NivelGarrapata
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static NivelGarrapata NivelGarrapataAEnum(this int valor)
        {
            var resultado = (NivelGarrapata)valor;
            return resultado;
        }

        /// <summary>
        /// Convierte un valor boleando a un valor de tipo Automatico
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static Automatico BoolAutomaticoEnum(this bool valor)
        {
            return valor ? Automatico.Si : Automatico.No;
        }

        /// <summary>
        /// Convierte un valor string a un Enumerador de Sexo
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static Sexo StringASexoEnum(this string valor)
        {
            return valor.Trim() == "M" ? Sexo.Macho : Sexo.Hembra;
        }

        /// <summary>
        /// Convierte un valor string a un Enumerador de Tendencia
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static Tendencia StringATendenciaEnum(this string valor)
        {
            var tendencia = Tendencia.Igual;
            if (String.Compare(valor, ">", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
               tendencia = Tendencia.Mayor; 
            }
            else
            {
                if (String.Compare(valor, "<", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    tendencia = Tendencia.Menor;
                }
            }
            return tendencia;
        }

        /// <summary>
        /// Devuelve solamente la parte de fecha de un objeto DateTime, quita horas, minutos, segundos y fracciones de segundos.
        /// </summary>
        /// <param name="me">Fecha con tiempo</param>
        /// <returns>Fecha sin tiempo</returns>
        internal static DateTime SoloFecha(this DateTime me)
        {
            return new DateTime(me.Year, me.Month, me.Day);
        }

        [BLToolkit.Data.Linq.SqlFunction(ServerSideOnly = true, SqlProvider = "System.Data.SqlClient")]
        internal static DateTime GetDate()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Convierte un valor boleando a un valor de tipo Automatico
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static ManejaLoteEnum BoolManejaLoteEnum(this bool valor)
        {
            return valor ? ManejaLoteEnum.Si : ManejaLoteEnum.No;
        }

        /// <summary>
        /// Convierte un valor boleando a un valor de tipo EstatusEnum
        /// </summary>
        /// <param name="cosecha"></param>
        /// <returns></returns>
        internal static Cosecha StringACosechaEnum(this string cosecha)
        {
            return cosecha.Equals("O-I") ? Cosecha.O_I : Cosecha.P_V;
        }

        /// <summary>
        /// Convierte un valor boleando a un valor de tipo EstatusEnum
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static NivelAccesoEnum NivelAccesoEnum(this int valor)
        {
            var resultado = (NivelAccesoEnum)valor;
            return resultado;
        }

        /// <summary>
        /// Convierte un valor boleano a un valor de tipo EnumTracto
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        internal static TractoEnum TractoAEnum(this bool valor)
        {
            return valor ? TractoEnum.Tracto : TractoEnum.Jaula;
        }

    }
}
