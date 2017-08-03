using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using System.IO;

namespace SIE.Services.Servicios.PL
{
    public class DiferenciasDeInventarioPL
    {
        /// <summary>
        /// Guarda los ajustes
        /// </summary>
        /// <param name="listaDiferenciasInventario"></param>
        /// <param name="usuarioInfo"></param>
        public IList<MemoryStream> Guardar(List<DiferenciasDeInventariosInfo> listaDiferenciasInventario, UsuarioInfo usuarioInfo)
        {
            try
            {
                Logger.Info();
                var diferenciasDeInventarioBl = new DiferenciasDeInventarioBL();
                return diferenciasDeInventarioBl.Guardar(listaDiferenciasInventario, usuarioInfo);
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de ajustes pendientes
        /// </summary>
        /// <returns></returns>
        public List<DiferenciasDeInventariosInfo> ObtenerAjustesPendientesPorUsuario(List<EstatusInfo> listaEstatusInfo, List<TipoMovimientoInfo> listaTiposMovimiento, UsuarioInfo usuarioInfo)
        {
            List<DiferenciasDeInventariosInfo> info;
            try
            {
                Logger.Info();
                var diferenciasDeInventarioBl = new DiferenciasDeInventarioBL();
                info = diferenciasDeInventarioBl.ObtenerAjustesPendientesPorUsuario(listaEstatusInfo, listaTiposMovimiento, usuarioInfo);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }
    }
}
