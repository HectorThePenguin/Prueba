using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using System;
using System.Reflection;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
  public  class NivelAlertaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Accion.
        /// </summary>
        /// <param name="Info">informacion del nivel del alerta que se guardara</param>
        internal int Guardar(NivelAlertaInfo Info)
        {
            try
            {
                Logger.Info();
                var NivelAlertaDAL = new NivelAlertaDAL();
                int result = Info.NivelAlertaId;
                if (Info.NivelAlertaId == 0)
                {
                    result = NivelAlertaDAL.Crear(Info);
                }
                else
                {
                    NivelAlertaDAL.Actualizar(Info);
                }
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina">informacion de paginacion que se usara en la consulta</param>
        /// <param name="filtro">filtro o condiciones de busqueda que se usaran</param>
        /// <returns>Regresa una lista de niveles de alerta encontrados</returns>
        internal ResultadoInfo<NivelAlertaInfo> ObtenerPorPagina(PaginacionInfo pagina, NivelAlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                var nivelAlertaDAL = new NivelAlertaDAL();
                ResultadoInfo<NivelAlertaInfo> result = nivelAlertaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Metodo para Obtener la lista por descripcion
        /// </summary>
        /// <param name="Descripcion">descripcion del nivel de alerta que se buscara</param>
        /// <returns>Regresa la informacion del nivel de alerta que se encontro</returns>
        public NivelAlertaInfo ObtenerPorDescripcion(String Descripcion)
        {
            try
            {
                Logger.Info();
                var nivelAlertaDAL = new NivelAlertaDAL();
                NivelAlertaInfo result = nivelAlertaDAL.ObtenerPorDescripcion(Descripcion);
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
        /// verifica si el nivel seleccionado no a sido asignado
        /// </summary>
        /// <param name="nivelAlertaId"></param>
        /// <returns></returns>
        public int VerificarAsignacionNivelAlerta(int nivelAlertaId)
        {
            try
            {
                Logger.Info();
                var verificarAsignacionNivelAlerta = new NivelAlertaDAL();
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
                var nivelesAlertaDesactivados = new NivelAlertaDAL();
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
                var nivelAlertaActivarPrimerNivelDesactivado = new NivelAlertaDAL();
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
