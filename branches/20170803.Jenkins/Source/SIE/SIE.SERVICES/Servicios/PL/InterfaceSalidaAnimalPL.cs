using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class InterfaceSalidaAnimalPL
    {
        /// <summary>
        /// Metodo para obtener el arete de la salida.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public InterfaceSalidaAnimalInfo ObtenerNumeroAreteIndividual(string arete, int organizacionID)
        {
            InterfaceSalidaAnimalInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalBL = new InterfaceSalidaAnimalBL();
                interfaceSalidaInfo = interfaceSalidaAnimalBL.ObtenerNumeroAreteIndividual(arete, organizacionID);
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
        /// Metodo para obtener el arete metalico de la salida
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public InterfaceSalidaAnimalInfo ObtenerNumeroAreteMetalico(string arete, int organizacionID)
        {
            InterfaceSalidaAnimalInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalBL = new InterfaceSalidaAnimalBL();
                interfaceSalidaInfo = interfaceSalidaAnimalBL.ObtenerNumeroAreteMetalico(arete, organizacionID);
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

        public List<InterfaceSalidaAnimalInfo> ObtenerAretesInterfazSalidaAnimal(string codigoCorral, int organizacionID)
        {
            List<InterfaceSalidaAnimalInfo> interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalBL = new InterfaceSalidaAnimalBL();
                interfaceSalidaInfo = interfaceSalidaAnimalBL.ObtenerAretesInterfazSalidaAnimal(codigoCorral, organizacionID);
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
        public List<InterfaceSalidaAnimalInfo> ObtenerPesosGanado(List<EntradaGanadoInfo> entradas)
        {
            List<InterfaceSalidaAnimalInfo> interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalBL = new InterfaceSalidaAnimalBL();
                interfaceSalidaInfo = interfaceSalidaAnimalBL.ObtenerPesosGanado(entradas);
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
        /// Metodo para obtener el arete metalico de la salida
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public InterfaceSalidaAnimalInfo GuardarAnimalID(InterfaceSalidaAnimalInfo animalInterface, long AnimalID)
        {
            InterfaceSalidaAnimalInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalBL = new InterfaceSalidaAnimalBL();
                interfaceSalidaInfo = interfaceSalidaAnimalBL.GuardarAnimalID(animalInterface, AnimalID);
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
        /// Metodo para obtener el arete de la salida en una partida Activa.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public InterfaceSalidaAnimalInfo ObtenerNumeroAreteIndividualPartidaActiva(string arete, int organizacionID)
        {
            InterfaceSalidaAnimalInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaAnimalBL = new InterfaceSalidaAnimalBL();
                interfaceSalidaInfo = interfaceSalidaAnimalBL.ObtenerNumeroAreteIndividual(arete, organizacionID);
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
