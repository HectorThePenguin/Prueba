using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class InterfaceSalidaAnimalBL
    {
        /// <summary>
        /// Metodo para obtener el arete de la salida.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaAnimalInfo ObtenerNumeroAreteIndividual(string arete, int organizacionID)
        {
            InterfaceSalidaAnimalInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalDal = new InterfaceSalidaAnimalDAL();
                interfaceSalidaInfo = interfaceSalidaAnimalDal.ObtenerNumeroAreteIndividual(arete, organizacionID);
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
            return interfaceSalidaInfo;
        }

        /// <summary>
        ///  Metodo para obtener el arete metalico de la salida.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaAnimalInfo ObtenerNumeroAreteMetalico(string arete, int organizacionID)
        {
            InterfaceSalidaAnimalInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalDal = new InterfaceSalidaAnimalDAL();
                interfaceSalidaInfo = interfaceSalidaAnimalDal.ObtenerNumeroAreteMetalico(arete, organizacionID);
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
            return interfaceSalidaInfo;
        }

        /// <summary>
        /// Obtiene todos los aretes en base a la salida.
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaAnimalInfo> ObtenerAretesInterfazSalidaAnimal(string codigoCorral, int organizacionID)
        {
            List<InterfaceSalidaAnimalInfo> interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalDal = new InterfaceSalidaAnimalDAL();
                interfaceSalidaInfo = interfaceSalidaAnimalDal.ObtenerAretesInterfazSalidaAnimal(codigoCorral, organizacionID);
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
            return interfaceSalidaInfo;
        }

        /// <summary>
        /// Obtiene todos los aretes en base a la salida.
        /// </summary>
        /// <param name="salida"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaAnimalInfo> ObtenerInterfazSalidaAnimal(int salida, int organizacionID)
        {
            List<InterfaceSalidaAnimalInfo> interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalDal = new InterfaceSalidaAnimalDAL();
                interfaceSalidaInfo = interfaceSalidaAnimalDal.ObtenerInterfazSalidaAnimal(salida, organizacionID);
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
            return interfaceSalidaInfo;
        }

        /// <summary>
        /// Obtiene una lista de interface salida animal
        /// </summary>
        /// <param name="entradas"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaAnimalInfo> ObtenerPesosGanado(List<EntradaGanadoInfo> entradas)
        {
            List<InterfaceSalidaAnimalInfo> interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalDal = new InterfaceSalidaAnimalDAL();
                interfaceSalidaInfo = interfaceSalidaAnimalDal.ObtenerPesosGanado(entradas);
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
            return interfaceSalidaInfo;
        }
        /// <summary>
        ///  Metodo para obtener el arete metalico de la salida.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaAnimalInfo GuardarAnimalID(InterfaceSalidaAnimalInfo animalInterface, long AnimalID)
        {
            InterfaceSalidaAnimalInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalDal = new InterfaceSalidaAnimalDAL();
                interfaceSalidaInfo = interfaceSalidaAnimalDal.GuardarAnimalID(animalInterface, AnimalID);
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
            return interfaceSalidaInfo;
        }
        /// <summary>
        /// Obtener la interfaz salida animal por entrada ganado
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaAnimalInfo> ObtenerInterfazSalidaAnimalPorEntradaGanado(EntradaGanadoInfo entradaGanadoInfo)
        {
            List<InterfaceSalidaAnimalInfo> result;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalDal = new InterfaceSalidaAnimalDAL();
                result = interfaceSalidaAnimalDal.ObtenerInterfazSalidaAnimalPorEntradaGanado(entradaGanadoInfo);
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
        /// Metodo para obtener el arete de la salida en una partida Activa.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaAnimalInfo ObtenerNumeroAreteIndividualPartidaActiva(string arete, int organizacionID)
        {
            InterfaceSalidaAnimalInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalDal = new InterfaceSalidaAnimalDAL();
                interfaceSalidaInfo = interfaceSalidaAnimalDal.ObtenerNumeroAreteIndividual(arete, organizacionID);
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
            return interfaceSalidaInfo;
        }
    }
}
