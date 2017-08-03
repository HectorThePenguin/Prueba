using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System;
using System.Reflection;

namespace SIE.Services.Servicios.PL
{
    public class NivelAlertaPL
    {
        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina">informacion de paginacion usada para la consulta</param>
        /// <param name="filtro">filtro o condiciones de la busqueda</param>
        /// <returns>regresa la lista de alertas que cumplen con las condiciones establecidas por el filtro</returns>
        public ResultadoInfo<NivelAlertaInfo> ObtenerPorPagina(PaginacionInfo pagina, NivelAlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                var NivelAlertaBL = new NivelAlertaBL();
                ResultadoInfo<NivelAlertaInfo> result = NivelAlertaBL.ObtenerPorPagina(pagina, filtro);
                return result;
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
        /// Obtener Alertas por descripcion
        /// </summary>
        /// <param name="Descripcion">descripcion del nivel de alerta que se buscara</param>
        /// <returns>nivel de alerta encontrado con la descripcion especificada</returns>
        public NivelAlertaInfo ObtenerPorDescripcion(string Descripcion)
        {
            try
            {
                Logger.Info();
                var NivelALertaBL = new NivelAlertaBL();
                NivelAlertaInfo result = NivelALertaBL.ObtenerPorDescripcion(Descripcion);
                return result;
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
        /// Metodo para Guardar la Accion
        /// </summary>
        /// <param name="Info">informacion del nivel de alerta a registrar</param>
        public int Guardar(NivelAlertaInfo Info)
        {
            try
            {
                Logger.Info();
                var NivelAlertaBL = new NivelAlertaBL();
                int Resultado = NivelAlertaBL.Guardar(Info);
                return Resultado;
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
        /// verifica si el nivel seleccionado no a sido asignado
        /// </summary>
        /// <param name="nivelAlertaId">id del nivel de alerta que se buscara</param>
        /// <returns></returns>
        public int VerificarAsignacionNivelAlerta(int nivelAlertaId)
        {
            try
            {
                Logger.Info();
                var verificarAsignacionNivelAlerta = new NivelAlertaBL();
                int result = verificarAsignacionNivelAlerta.VerificarAsignacionNivelAlerta(nivelAlertaId);
                return result;
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
        /// Obtiene un contador de los niveles que se encuentran deshabilitados
        /// </summary>
        /// <returns></returns>
        public int NivelesAlertaDesactivados()
        {
            try
            {
                Logger.Info();
                var nivelesAlertaDesactivados = new NivelAlertaBL();
                int result = nivelesAlertaDesactivados.NivelesAlertaDesactivados();
                return result;
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
        /// SP que devuelve el primer campo inactivo de la tabla NivelAlerta 
        /// y verifica si es el mismo que se le envio.
        /// </summary>
        /// <param name="nivelAlertaId"></param>
        /// <returns>Si regresa 0 no es el primero deshabilitado si regresa > 0 es el primero deshabilitado</returns>
        public int NivelAlerta_ActivarPrimerNivelDesactivado(int nivelAlertaId)
        {
            try
            {
                Logger.Info();
                var nivelAlertaActivarPrimerNivelDesactivado = new NivelAlertaBL();
                int result = nivelAlertaActivarPrimerNivelDesactivado.NivelAlerta_ActivarPrimerNivelDesactivado(nivelAlertaId);
                return result;
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
