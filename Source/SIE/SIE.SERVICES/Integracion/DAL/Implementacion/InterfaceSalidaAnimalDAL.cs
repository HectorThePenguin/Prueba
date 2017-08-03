using System;
using System.Collections.Generic;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Data;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class InterfaceSalidaAnimalDAL : DALBase
    {
        /// <summary>
        /// Metodo para obtener el arete de la salida.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaAnimalInfo ObtenerNumeroAreteIndividual(string arete, int organizacionID)
        {
            InterfaceSalidaAnimalInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaAnimalDAL.ObtenerNumeroAreteIndividual(arete, organizacionID);
                DataSet ds = Retrieve("[dbo].[InterfaceSalidaAnimal_ObtenerArete]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaAnimalDAL.ObtenerNumeroAreteIndividual(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Metodo para obtener el arete metalico de la salida.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaAnimalInfo ObtenerNumeroAreteMetalico(string arete, int organizacionID)
        {
            InterfaceSalidaAnimalInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaAnimalDAL.ObtenerNumeroAreteMetalico(arete, organizacionID);
                DataSet ds = Retrieve("[dbo].[InterfaceSalidaAnimal_ObtenerAreteMetalico]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaAnimalDAL.ObtenerNumeroAreteIndividual(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene todos los aretes en base a la salida.
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaAnimalInfo> ObtenerAretesInterfazSalidaAnimal(string codigoCorral, int organizacionID)
        {
            List<InterfaceSalidaAnimalInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaAnimalDAL.ObtenerAretesInterfazSalidaAnimal(codigoCorral, organizacionID);
                DataSet ds = Retrieve("InterfaceSalidaAnimal_ObtenerAretesPorSalida", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaAnimalDAL.ObtenerAretesInterfazSalidaAnimal(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista de Interface Salida Animal
        /// </summary>
        /// <param name="salida"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaAnimalInfo> ObtenerInterfazSalidaAnimal(int salida, int organizacionID)
        {
            List<InterfaceSalidaAnimalInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaAnimalDAL.ObtenerInterfazSalidaAnimal(salida, organizacionID);
                DataSet ds = Retrieve("[dbo].[InterfaceSalidaAnimal_ObtenerPorSalidaOrganizacion]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaAnimalDAL.ObtenerInterfazSalidaAnimal(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista de interface salida animal
        /// </summary>
        /// <param name="entradas"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaAnimalInfo> ObtenerPesosGanado(List<EntradaGanadoInfo> entradas)
        {
            List<InterfaceSalidaAnimalInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxInterfaceSalidaAnimalDAL.ObtenerInterfazSalidaAnimalPorEntradas(entradas);
                DataSet ds = Retrieve("[dbo].[InterfaceSalidaAnimal_ObtenerPorOrganizacionOrigenDestino]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaAnimalDAL.ObtenerInterfazSalidaAnimalPorEntradas(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        /// <summary>
        /// Metodo para obtener el arete metalico de la salida.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaAnimalInfo GuardarAnimalID(InterfaceSalidaAnimalInfo animalInterface, long AnimalID)
        {
            InterfaceSalidaAnimalInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaAnimalDAL.GuardarAnimalID(animalInterface, AnimalID);
                DataSet ds = Retrieve("[dbo].[InterfaceSalidaAnimal_ActualizarAnimalID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaAnimalDAL.ObtenerNumeroAreteIndividual(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        /// <summary>
        /// Obtener interfaz salida animal por entrada ganado
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaAnimalInfo> ObtenerInterfazSalidaAnimalPorEntradaGanado(EntradaGanadoInfo entradaGanadoInfo)
        {
            List<InterfaceSalidaAnimalInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaAnimalDAL.ObtenerParametrosInterfazSalidaAnimalPorEntradaGanado(entradaGanadoInfo);
                DataSet ds = Retrieve("[dbo].[InterfaceSalidaAnimal_ObtenerInterfazSalidaAnimalPorEntradaGanado]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaAnimalDAL.ObtenerInterfazSalidaAnimalPorEntradaGanado(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Metodo para obtener el arete de la salida en una partida Activa.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaAnimalInfo ObtenerNumeroAreteIndividualPartidaActiva(string arete, int organizacionID)
        {
            InterfaceSalidaAnimalInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaAnimalDAL.ObtenerNumeroAreteIndividualPartidaActiva(arete, organizacionID);
                DataSet ds = Retrieve("[dbo].[InterfaceSalidaAnimal_ObtenerAretePartidaActiva]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaAnimalDAL.ObtenerNumeroAreteIndividualPartidaActiva(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
    }
}
