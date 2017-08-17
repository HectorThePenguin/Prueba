﻿using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class ChequeraEtapasPL
    {
        public IList<ChequeraEtapasInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var bl = new ChequeraEtapasBL();
                IList<ChequeraEtapasInfo> result = bl.ObtenerTodos();
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