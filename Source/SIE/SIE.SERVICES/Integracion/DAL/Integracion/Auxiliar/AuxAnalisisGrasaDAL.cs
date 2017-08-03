using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxAnalisisGrasaDAL
    {
        // Devuelve el conjunto de Parametros a guardar
        internal static Dictionary<string, object> ObtenerParametrosGuardar(AnalisisGrasaInfo analisisGrasaInfo)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary <string,object>
                {
                    {"@OrganizacionID",analisisGrasaInfo.Organizacion.OrganizacionID},
	                {"@EntradaProductoID",analisisGrasaInfo.EntradaProdructo.EntradaProductoId},
	                {"@TipoMuestra",analisisGrasaInfo.TipoMuestra},
	                {"@PesoMuestra",analisisGrasaInfo.PesoMuestra},
	                {"@PesoTuboSeco",analisisGrasaInfo.PesoTuboSeco},
	                {"@PesoTuboMuestra",analisisGrasaInfo.PesoTuboMuestra},
	                {"@Impurezas",analisisGrasaInfo.Impurezas},
	                {"@Observaciones",analisisGrasaInfo.Observaciones},
	                {"@Activo",analisisGrasaInfo.Activo},
	                {"@UsuarioCreacionID",analisisGrasaInfo.UsuarioCreacion.UsuarioID}
                };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
        }
    }
}
