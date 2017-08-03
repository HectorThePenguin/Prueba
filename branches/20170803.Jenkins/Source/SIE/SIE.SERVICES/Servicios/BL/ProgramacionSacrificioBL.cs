using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
namespace SIE.Services.Servicios.BL
{
    internal class ProgramacionSacrificioBL
    {

         internal AnimalInfo ObtenerExistenciaAnimal(AnimalInfo animalInfo, int loteID)
         {
             AnimalInfo result;

             try
             {
                 Logger.Info();
                 var programacionSacrificio = new ProgramacionSacrificioDAL();
                 result = programacionSacrificio.ObtenerExistenciaAnimal(animalInfo, loteID);
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

         internal int GuardarAnimalSalida(List<AnimalInfo> listaAnimales, ProgramacionSacrificioGuardadoInfo programacionSacrificioGuardadoInfo)
         {
             int result;
             try
             {
                 Logger.Info();
                 var loteBL = new LoteBL();
                 using (var transaccion = new TransactionScope())
                 {
                     var loteInfo = loteBL.ObtenerPorID(programacionSacrificioGuardadoInfo.LoteID);
                     var deteccionGrabar = new DeteccionInfo
                     {
                         CorralID = loteInfo.CorralID,
                         LoteID = loteInfo.LoteID,
                         UsuarioCreacionID = programacionSacrificioGuardadoInfo.UsuarioID
                     };

                     foreach (var animal in listaAnimales)
                     {
                         if (animal != null && animal.CargaInicial)
                         {
                             // Se intercambian aretes por encontrarse el animal en un corral distinto y ser carga inicial
                             var animalBL = new AnimalBL();
                             animalBL.ReemplazarAretes(animal, deteccionGrabar);
                         }
                     }

                     var programacionSacrificio = new ProgramacionSacrificioDAL();
                     result = programacionSacrificio.GuardarAnimalSalida(listaAnimales, programacionSacrificioGuardadoInfo);
                     transaccion.Complete();
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
             return result;
         }
    }
}
