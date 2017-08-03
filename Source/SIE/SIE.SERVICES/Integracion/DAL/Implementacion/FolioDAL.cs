using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class FolioDAL : DALBase
    {
        internal long ObtenerFolio(int organizacionID, TipoFolio tipoFolio)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@OrganizacionID", organizacionID},
                                         {"@TipoFolioID", tipoFolio.GetHashCode()}
                                     };
                long folio = 0;
                using (IDataReader reader = RetrieveReader("Folio_ObtenerPorOrganizacionTipoFolio", parameters))
                {
                    if (ValidateDataReader(reader))
                    {
                        while (reader.Read())
                        {
                            folio = Convert.ToInt64(reader["Folio"]);
                        }
                    }
                }
                return folio;
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
        }
    }
}
