using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Properties;


namespace SIE.Services.Servicios.BL
{
    /// <summary>
    /// The revision implante bl.
    /// </summary>
    internal class RevisionImplanteBL
    {
        /// <summary>
        /// The obtener lugares validacion.
        /// </summary>
        /// <returns>
        /// The <see cref="ResultadoInfo"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal ResultadoInfo<AreaRevisionInfo> ObtenerLugaresValidacion()
        {
            ResultadoInfo<AreaRevisionInfo> resultado;
            try
            {
                Logger.Info();
                var revicionImplanteDal= new RevisionImplanteDAL();
                resultado = revicionImplanteDal.ObtenerLugaresValidacion();
                if (resultado != null)
                {
                    resultado.Lista.Insert(0, new AreaRevisionInfo { AreaRevisionId = 0, Descripcion = ResourceServices.RevisionImplante_Seleccione });
                }
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

            return resultado;
        }

        /// <summary>
        /// Obtiene las causas para la revision de implante.
        /// </summary>
        /// <returns>
        /// The <see cref="ResultadoInfo"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal ResultadoInfo<CausaRevisionImplanteInfo> ObtenerCausas()
        {
            ResultadoInfo<CausaRevisionImplanteInfo> resultado;
            try
            {
                Logger.Info();
                var revicionImplanteDal = new RevisionImplanteDAL();
                resultado = revicionImplanteDal.ObtenerCausas();
                if (resultado != null)
                {
                    resultado.Lista.Insert(0, new CausaRevisionImplanteInfo { CausaId = 0, Descripcion = ResourceServices.RevisionImplante_Seleccione });
                }
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

            return resultado;
        }

        /// <summary>
        /// Valida el corral que se le validara el reimplante
        /// </summary>
        /// <param name="corral">
        /// The corral.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal ResultadoValidacion ValidarCorral(CorralInfo corral)
        {
            try
            {
                var resultado = new ResultadoValidacion();
                var corralBl = new CorralBL();
                CorralInfo corralResultado = corralBl.ObtenerCorralPorCodigo(corral.OrganizacionId, corral.Codigo);
                var revisionActual = new RevisionImplanteInfo();
                if (corralResultado == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgCorralNoExiste;
                    return resultado;
                }

                revisionActual.Corral = corralResultado;
                var loteBl = new LoteBL();
                var loteInfo = new LoteInfo
                {
                    OrganizacionID = corral.OrganizacionId,
                    CorralID = corralResultado.CorralID
                };

                var loteResultado = loteBl.ObtenerPorCorralID(loteInfo);
                if (loteResultado == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgLoteInactivo;
                    return resultado;
                }
                revisionActual.Lote = loteResultado;

                if (loteResultado.TipoCorralID != (int)TipoCorral.Produccion)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgCorralNoProductivo;
                    return resultado;
                } 

                var animalBl = new AnimalBL();
                List<AnimalInfo> animales = animalBl.ObtenerAnimalesPorLote(corral.OrganizacionId, loteResultado.LoteID);

                if (animales == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgCorralSinMovimientos;
                    return resultado;
                }

                if (animales.Count == 0)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgCorralSinMovimientos;
                    return resultado;
                }

                resultado.Resultado = true;
                resultado.Control = revisionActual;
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
        /// Valida el arete al cual se revisara el reimplante
        /// </summary>
        /// <param name="corral">
        /// The corral.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal ResultadoValidacion ValidarArete(AnimalInfo animal, CorralInfo corral)
        {
            try
            {
                var resultado = new ResultadoValidacion();
                var animalBl = new AnimalBL();

                if (corral == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgFaltaCorral;
                    return resultado;
                }

                AnimalInfo animalResultado = animalBl.ObtenerAnimalPorArete(animal.Arete, animal.OrganizacionIDEntrada);

                if (animalResultado == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgAreteInvalido;
                    return resultado;
                }

                AnimalMovimientoInfo movimientos = animalBl.ObtenerUltimoMovimientoAnimal(animalResultado);

                if (movimientos == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgSinMovimientos;
                    return resultado;
                }

                if (movimientos.CorralID != corral.CorralID)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgCorralDiferente;
                    return resultado;
                }

                RevisionImplanteInfo revision = ObtenerRevisionDelDia(animalResultado);

                if (revision != null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgYaRevisado.Replace("(ARETE)",animal.Arete);
                    return resultado;
                }

                resultado.Resultado = true;
                resultado.Control = animalResultado;
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
        /// <param name="animal">
        /// The animal.
        /// </param>
        /// <param name="corral">
        /// The corral.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoValidacion"/>.
        /// </returns>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal ResultadoValidacion ValidarAgregarArete(AnimalInfo animal, CorralInfo corral)
        {
            try
            {
                var resultado = new ResultadoValidacion();
                var animalBl = new AnimalBL();

                if (corral == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgFaltaCorral;
                    return resultado;
                }

                AnimalInfo animalResultado = animalBl.ObtenerAnimalPorArete(animal.Arete, animal.OrganizacionIDEntrada);

                if (animalResultado == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgAreteInvalido;
                    return resultado;
                }

                AnimalMovimientoInfo movimientos = animalBl.ObtenerUltimoMovimientoAnimal(animalResultado);

                if (movimientos == null)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgSinMovimientos;
                    return resultado;
                }

                if (movimientos.CorralID != corral.CorralID)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgFaltaCorral;
                    return resultado;
                }

                resultado.Resultado = true;
                resultado.Control = animalResultado;
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
        internal ResultadoValidacion ValidarAgregarArete(RevisionImplanteInfo revision, List<RevisionImplanteInfo> lista)
        {
            try
            {
                var resultado = new ResultadoValidacion{Resultado = true};

                if (lista.Any(revisionImplanteInfo => revisionImplanteInfo.Animal.Arete == revision.Animal.Arete))
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgAreteExiste;
                    return resultado;
                }

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
        internal ResultadoValidacion GuardarRevision(List<RevisionImplanteInfo> listaRevision, UsuarioInfo usuario)
        {
            try
            {
                var resultado = new ResultadoValidacion{ Resultado = true };
                var revisionImplanteDal = new RevisionImplanteDAL();
                if (listaRevision == null || listaRevision.Count == 0)
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.RevisionImplante_msgFaltanAretes;
                    return resultado;
                }

                revisionImplanteDal.GuardarRevision(listaRevision, usuario);
                resultado.Mensaje = ResourceServices.RevisionImplante_msgGuardado;
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
        /// Obtiene la revision de un animal del dia actual
        /// </summary>
        /// <param name="listaRevision">
        /// The lista revision.
        /// </param>
        /// <param name="usuario">
        /// The usuario.
        /// </param>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal RevisionImplanteInfo ObtenerRevisionDelDia(AnimalInfo animal)
        {
            RevisionImplanteInfo resultado;
            try
            {
                Logger.Info();
                var revicionImplanteDal = new RevisionImplanteDAL();
                resultado = revicionImplanteDal.ObtenerRevisonActual(animal);
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

            return resultado;
        }
    }
}
