using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    /// <summary>
    /// The revision implantes pl.
    /// </summary>
    public class RevisionImplantesPL
    {
        /// <summary>
        /// The obtener lugares validacion.
        /// </summary>
        /// <returns>
        /// The <see cref="ResultadoInfo"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        public ResultadoInfo<AreaRevisionInfo> ObtenerLugaresValidacion()
        {
            ResultadoInfo<AreaRevisionInfo> result;
            try
            {
                Logger.Info();
                var revisionReimplanteBl = new RevisionImplanteBL();
                result = revisionReimplanteBl.ObtenerLugaresValidacion();
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

            return result;
        }

        /// <summary>
        /// The obtener causas.
        /// </summary>
        /// <returns>
        /// The <see cref="ResultadoInfo"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        public ResultadoInfo<CausaRevisionImplanteInfo> ObtenerCausas()
        {
            ResultadoInfo<CausaRevisionImplanteInfo> result;
            try
            {
                Logger.Info();
                var revisionReimplanteBl = new RevisionImplanteBL();
                result = revisionReimplanteBl.ObtenerCausas();
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

            return result;
        }

        /// <summary>
        /// Valida el corral que se revisara el implante.
        /// </summary>
        /// <param name="corral">
        /// The corral.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        public ResultadoValidacion ValidarCorral(CorralInfo corral)
        {
            try
            {
                var revisionBl = new RevisionImplanteBL();
                ResultadoValidacion resultado = revisionBl.ValidarCorral(corral);
                return resultado;
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
        /// The validar arete.
        /// </summary>
        /// <param name="animal">
        /// The animal.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        public ResultadoValidacion ValidarArete(AnimalInfo animal, CorralInfo corral)
        {
            try
            {
                var revisionBl = new RevisionImplanteBL();
                ResultadoValidacion resultado = revisionBl.ValidarArete(animal, corral);
                return resultado;
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
        /// The validar agregar arete.
        /// </summary>
        /// <param name="revision">
        /// The revision.
        /// </param>
        /// <param name="lista">
        /// The lista.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        public ResultadoValidacion ValidarAgregarArete(RevisionImplanteInfo revision, List<RevisionImplanteInfo> lista)
        {
            try
            {
                var revisionBl = new RevisionImplanteBL();
                ResultadoValidacion resultado = revisionBl.ValidarAgregarArete(revision, lista);
                return resultado;
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
        /// The guardar revision.
        /// </summary>
        /// <param name="listaRevision">
        /// The lista revision.
        /// </param>
        /// <param name="usuario">
        /// The usuario.
        /// </param>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        public ResultadoValidacion GuardarRevision(List<RevisionImplanteInfo> listaRevision, UsuarioInfo usuario)
        {
            try
            {
                var revisionBl = new RevisionImplanteBL();
                return revisionBl.GuardarRevision(listaRevision, usuario);
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
    }
}
