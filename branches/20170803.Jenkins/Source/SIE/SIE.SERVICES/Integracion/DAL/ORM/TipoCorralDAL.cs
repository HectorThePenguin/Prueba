using System;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class TipoCorralDAL : BaseDAL
    {
        TipoCorralAccessor tipoCorralAccessor;

        protected override void inicializar()
        {
            tipoCorralAccessor = da.inicializarAccessor<TipoCorralAccessor>();
        }

        protected override void destruir()
        {
            tipoCorralAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista de TipoCorral
        /// </summary>
        /// <returns></returns>
        public IQueryable<TipoCorralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<TipoCorralInfo>();
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
        /// Valida si el Grupo Corral Pertenece a un Tipo Corral
        /// </summary>
        /// <param name="grupoCorralID"></param>
        /// <returns></returns>
        internal bool TieneAsignadoGruposCorral(int grupoCorralID)
        {
            return ObtenerTodos().Count(grupo => grupo.GrupoCorralID == grupoCorralID) > 0;
        }
    }
}
