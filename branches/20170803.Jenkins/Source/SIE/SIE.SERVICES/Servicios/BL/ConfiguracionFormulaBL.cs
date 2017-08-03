using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Transactions;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using SIE.Services.Info.Enums;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Style;
using System.Drawing;
using SIE.Services.Properties;

namespace SIE.Services.Servicios.BL
{
    public class ConfiguracionFormulaBL
    {
		 /// <summary>
        /// Obtener la configuracion de las formulas para una organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public IList<ConfiguracionFormulaInfo> ObtenerConfiguracionFormula(int organizacionID)
        {
            IList<ConfiguracionFormulaInfo> lista;
            try
            {
                Logger.Info();
                var configuracionFormulaDAL = new ConfiguracionFormulaDAL();
                lista = configuracionFormulaDAL.ObtenerConfiguracionFormula(organizacionID);
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
            return lista;
        }

		/// <summary>
        /// Metodo para importar archivo de excel
        /// </summary>
        /// <param name="configuracionImportar"></param>
        /// <returns></returns>
        internal bool ImportarArchivo(ConfiguracionFormulaInfo configuracionImportar)
        {
            bool importarArchivo;
            try
            {
                Logger.Info();
                var configuracionFormulaDAL = new ConfiguracionFormulaDAL();

                //Transformar archivo Excel a List<ConfiguracionFormulaInfo>
                IList<ConfiguracionFormulaInfo> listaConfiguracionImportar =
                    TransformarArchivo(configuracionImportar.NombreArchivo);

                if (listaConfiguracionImportar.Any())
                {
                    // Se inicia la transaccion
                    using (var transaction = new TransactionScope())
                    {
                        //Se manda a inactivar la configuracion anterior
                        configuracionFormulaDAL.InactivarConfiguracionAnterior(configuracionImportar);

                        foreach (var configuracion in listaConfiguracionImportar)
                        {
                            //Se manda a almacenar la configuracion
                            configuracionFormulaDAL.GuardarConfiguracionFormula(configuracion, configuracionImportar);

                        }
                        transaction.Complete();
                        importarArchivo = true;
                    }
                }
                else
                {
                    // No trae registros el EXCEL
                    throw new ExcepcionDesconocida(
                        ResourceServices.ConfigurarFormula_ArchivoNoContieneRegistros);
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
            return importarArchivo;
        }

        /// <summary>
        /// Transformar archivo excel a una lista de ConfiguracionFormulaInfo
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        private IList<ConfiguracionFormulaInfo> TransformarArchivo(string nombreArchivo)
        {
            IList<ConfiguracionFormulaInfo> lista = new List<ConfiguracionFormulaInfo>();
            var configuracionFormula = new ConfiguracionFormulaInfo();
            var archivo = new FileInfo(nombreArchivo);

            const int renglonInicio = 2;

            try
            {
                var prueba = new ExcelPackage(archivo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_ArchivoEnUso);
            }

            // Open and read the XlSX file.
            using (var package = new ExcelPackage(archivo))
            {
                // Get the work book in the file
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null && workBook.Worksheets.Count > 0)
                {
                    // Get the first worksheet
                    ExcelWorksheet currentWorksheet = workBook.Worksheets.First();

                    if (ValidarTitulosDeExcel(currentWorksheet, renglonInicio))
                    {
                        for (int renglon = renglonInicio + 1; renglon <= currentWorksheet.Dimension.End.Row; renglon++)
                            // read each row from the start of the data (start row + 1 header row) to the end of the spreadsheet.
                        {
                            configuracionFormula = ValidarContenidoDeRenglon(currentWorksheet, renglon);

                            if (ValidarFormulasObtenidas(configuracionFormula, renglon, currentWorksheet))
                            {
                                lista.Add(configuracionFormula);
                            }
                        }
                    }
                }
            }
            return lista;
        }

        /// <summary>
        /// Metodo para validar las formalas obtenidas en un regitro del documento excel
        /// </summary>
        /// <param name="configuracionFormula"></param>
        /// <param name="renglon"></param>
        /// <param name="currentWorksheet"></param>
        /// <returns></returns>
        private bool ValidarFormulasObtenidas(ConfiguracionFormulaInfo configuracionFormula, int renglon, ExcelWorksheet currentWorksheet)
        {
            bool regitroValido = false;
            if (configuracionFormula != null)
            {
                if (configuracionFormula.Formula != null )
                {
                    if (configuracionFormula.FormulaSiguiente != null )
                    {

                        if (configuracionFormula.PesoInicioMaximo >= configuracionFormula.PesoInicioMinimo)
                        {
                            if (configuracionFormula.DiasEstanciaMaximo >= configuracionFormula.DiasEstanciaMinimo)
                            {
                                if (configuracionFormula.DiasTransicionMaximo >= configuracionFormula.DiasTransicionMinimo)
                                {
                                    if (!String.IsNullOrEmpty(configuracionFormula.TipoGanado))
                                    {
                                        if (ValidarTiposDeGanado(configuracionFormula.TipoGanado))
                                        {
                                            regitroValido = true;
                                        }
                                        else
                                        {
                                            //Dias transicion maximo debe ser mayor a los dias transicion minimo
                                            throw new ExcepcionDesconocida(String.Format("{0}{1}{2}",
                                               ResourceServices.ConfigurarFormula_EnLaFormula,
                                               configuracionFormula.Formula.Descripcion,
                                               ResourceServices.ConfigurarFormula_TipoGanadoInvalido)
                                            );
                                        }
                                    }
                                    else
                                    {
                                        //Dias transicion maximo debe ser mayor a los dias transicion minimo
                                        throw new ExcepcionDesconocida(String.Format("{0}{1}{2}",
                                           ResourceServices.ConfigurarFormula_EnLaFormula,
                                           configuracionFormula.Formula.Descripcion,
                                           ResourceServices.ConfigurarFormula_TipoGanadoFaltante)
                                        );
                                    }
                                }
                                else
                                {
                                    //Dias transicion maximo debe ser mayor a los dias transicion minimo
                                    throw new ExcepcionDesconocida(String.Format("{0}{1}{2}",
                                       ResourceServices.ConfigurarFormula_EnLaFormula,
                                       configuracionFormula.Formula.Descripcion,
                                       ResourceServices.ConfigurarFormula_DiasTransicion)
                                    );
                                }
                            }
                            else
                            {
                                //Dias estancia maximo debe ser mayor a los dias estancia minimo
                                throw new ExcepcionDesconocida(String.Format("{0}{1}{2}",
                                      ResourceServices.ConfigurarFormula_EnLaFormula,
                                      configuracionFormula.Formula.Descripcion,
                                      ResourceServices.ConfigurarFormula_DiasEstancia)
                                   );
                            }
                        }
                        else
                        {
                            //Peso inicio maximo debe ser mayor al peso inicio minimo
                            throw new ExcepcionDesconocida(String.Format("{0}{1}{2}",
                                     ResourceServices.ConfigurarFormula_EnLaFormula,
                                     configuracionFormula.Formula.Descripcion,
                                     ResourceServices.ConfigurarFormula_PesoInicio)
                                  );
                        }
                    }
                    else
                    {
                        object formulaSiguiente = currentWorksheet.Cells[renglon, 6].Value;
                        //La formula: , no existe en el catalogo de formulas, Favor de validar.
                        throw new ExcepcionDesconocida(String.Format("{0}{1}{2}",
                            ResourceServices.ConfigurarFormula_FormulaNoExiste,
                            formulaSiguiente,
                            ResourceServices.ConfigurarFormula_FormulaNoExiste2)
                            );
                    }
                }
                else
                {
                    object formula = currentWorksheet.Cells[renglon, 1].Value;
                    //La formula: , no existe en el catalogo de formulas, Favor de validar.
                    throw new ExcepcionDesconocida(String.Format("{0}{1}{2}",
                        ResourceServices.ConfigurarFormula_FormulaNoExiste,
                        formula,
                        ResourceServices.ConfigurarFormula_FormulaNoExiste2)
                        );
                }
            }
            else
            {
                //No tiene el formato correcto
                throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_ArchivoNoTieneLaEstructura);
            }
            return regitroValido;
        }

        /// <summary>
        /// Validar los tipos de ganados contenidos en el archivo
        /// </summary>
        /// <param name="tipoGanado"></param>
        /// <returns></returns>
        private bool ValidarTiposDeGanado(string tipoGanado)
        {
            bool existenTipos = true;

            string[] split = tipoGanado.Split(new[] { '|' });

            TipoGanadoBL tipoGanadoBL = new TipoGanadoBL();

            int tipoID;
            TipoGanadoInfo tipoGanadoInfo ;
            foreach (string tipoGanadoID in split.Where(s => s.Trim() != ""))
            {

                if (int.TryParse(tipoGanadoID, out tipoID))
                {
                    tipoGanadoInfo = tipoGanadoBL.ObtenerPorID(tipoID);
                    if (tipoGanadoInfo == null)
                    {
                        //Tipo No existe
                        existenTipos = false;

                    }
                }
                else
                {
                    //Biene como string
                    existenTipos = false;

                }
            }

            return existenTipos;
        }

        /// <summary>
        /// Validar contenido del renglon leido y transformarlo a un objeto
        /// </summary>
        /// <param name="currentWorksheet"></param>
        /// <param name="renglon"></param>
        /// <returns></returns>
        private ConfiguracionFormulaInfo ValidarContenidoDeRenglon(ExcelWorksheet currentWorksheet, int renglon)
        {
            ConfiguracionFormulaInfo configuracionFormulaInfo = null;

            object formula = currentWorksheet.Cells[renglon, 1].Value;
            object pesoInicioMinimo = currentWorksheet.Cells[renglon, 2].Value;
            object pesoInicioMaximo = currentWorksheet.Cells[renglon, 3].Value;
            object tipoGanado = currentWorksheet.Cells[renglon, 4].Value;
            object pesoSalida = currentWorksheet.Cells[renglon, 5].Value;
            object formulaSiguiente = currentWorksheet.Cells[renglon, 6].Value;
            object diasEstanciaMinimo = currentWorksheet.Cells[renglon, 7].Value;
            object diasEstanciaMaximo = currentWorksheet.Cells[renglon, 8].Value;
            object diasTransicionMinimo = currentWorksheet.Cells[renglon, 9].Value;
            object diasTransicionMaximo = currentWorksheet.Cells[renglon, 10].Value;
            object disponibilidad = currentWorksheet.Cells[renglon, 11].Value;

            if (
                (formula != null) && (pesoInicioMinimo != null) && (pesoInicioMaximo != null) &&
                (tipoGanado != null) && (pesoSalida != null) && (formulaSiguiente != null) &&
                (diasEstanciaMinimo != null) && (diasEstanciaMaximo != null) &&
                (diasTransicionMinimo != null) &&
                (diasTransicionMaximo != null) && (disponibilidad != null)
                )
            {
                var formulaBL = new FormulaBL();

                try
                {
                    configuracionFormulaInfo = new ConfiguracionFormulaInfo
                    {
                        Formula = formulaBL.ObtenerPorDescripcion(formula.ToString()),
                        PesoInicioMinimo = Convert.ToInt32(pesoInicioMinimo.ToString()),
                        PesoInicioMaximo = Convert.ToInt32(pesoInicioMaximo.ToString()),
                        TipoGanado = tipoGanado.ToString(),
                        PesoSalida = Convert.ToInt32(pesoSalida.ToString()),
                        FormulaSiguiente = formulaBL.ObtenerPorDescripcion(formulaSiguiente.ToString()),
                        DiasEstanciaMinimo = Convert.ToInt32(diasEstanciaMinimo.ToString()),
                        DiasEstanciaMaximo = Convert.ToInt32(diasEstanciaMaximo.ToString()),
                        DiasTransicionMinimo = Convert.ToInt32(diasTransicionMinimo.ToString()),
                        DiasTransicionMaximo = Convert.ToInt32(diasTransicionMaximo.ToString()),
                        Disponibilidad = disponibilidad.ToString() == Disponibilidad.Si.ToString()
                            ? Disponibilidad.Si
                            : Disponibilidad.No
                    };
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_ValoresFormatoCorrecto);
                }

            }
            else
            {
                throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_DatosEnBlanco);
            }
            return configuracionFormulaInfo;
        }

        /// <summary>
        /// Metodo para validar los titulos del excel a importar
        /// </summary>
        /// <param name="currentWorksheet"></param>
        /// <param name="renglonInicio"></param>
        /// <returns></returns>
        private bool ValidarTitulosDeExcel(ExcelWorksheet currentWorksheet, int renglonInicio)
        {
            bool resp = false;
            try
            {
            
                object formulaTitulo = currentWorksheet.Cells[renglonInicio, 1].Value;
                object pesoInicioMinimoTitulo = currentWorksheet.Cells[renglonInicio, 2].Value;
                object pesoInicioMaximoTitulo = currentWorksheet.Cells[renglonInicio, 3].Value;
                object tipoGanadoTitulo = currentWorksheet.Cells[renglonInicio, 4].Value;
                object pesoSalidaTitulo = currentWorksheet.Cells[renglonInicio, 5].Value;
                object formulaSiguienteTitulo = currentWorksheet.Cells[renglonInicio, 6].Value;
                object diasEstanciaMinimoTitulo = currentWorksheet.Cells[renglonInicio, 7].Value;
                object diasEstanciaMaximoTitulo = currentWorksheet.Cells[renglonInicio, 8].Value;
                object diasTransicionMinimoTitulo = currentWorksheet.Cells[renglonInicio, 9].Value;
                object diasTransicionMaximoTitulo = currentWorksheet.Cells[renglonInicio, 10].Value;
                object disponibilidadTitulo = currentWorksheet.Cells[renglonInicio, 11].Value;

                if (
                    ((formulaTitulo != null) && (formulaTitulo.ToString() == ResourceServices.ConfigurarFormula_Formula)) &&
                    ((pesoInicioMinimoTitulo != null) && (pesoInicioMinimoTitulo.ToString() == ResourceServices.ConfigurarFormula_PesoInicioMinimo)) &&
                    ((pesoInicioMaximoTitulo != null) && (pesoInicioMaximoTitulo.ToString() == ResourceServices.ConfigurarFormula_PesoInicioMaximo)) &&
                    ((tipoGanadoTitulo != null) && (tipoGanadoTitulo.ToString() == ResourceServices.ConfigurarFormula_TipoGanado)) &&
                    ((pesoSalidaTitulo != null) && (pesoSalidaTitulo.ToString() == ResourceServices.ConfigurarFormula_PesoSalida)) &&
                    ((formulaSiguienteTitulo != null) && (formulaSiguienteTitulo.ToString() == ResourceServices.ConfigurarFormula_FormulaSiguiente)) &&
                    ((diasEstanciaMinimoTitulo != null) && (diasEstanciaMinimoTitulo.ToString() == ResourceServices.ConfigurarFormula_DiasEstanciaMinimo)) &&
                    ((diasEstanciaMaximoTitulo != null) && (diasEstanciaMaximoTitulo.ToString() == ResourceServices.ConfigurarFormula_DiasEstanciaMaximo)) &&
                    ((diasTransicionMinimoTitulo != null) && (diasTransicionMinimoTitulo.ToString() == ResourceServices.ConfigurarFormula_DiasTransicionMinimo)) &&
                    ((diasTransicionMaximoTitulo != null) && (diasTransicionMaximoTitulo.ToString() == ResourceServices.ConfigurarFormula_DiasTransicionMaximo)) &&
                    ((disponibilidadTitulo != null) && (disponibilidadTitulo.ToString() == ResourceServices.ConfigurarFormula_Disponibilidad))
                    )
                {
                    resp = true;
                }
                else
                {
                    //No tiene el formato correcto
                    throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_ArchivoNoTieneLaEstructura);
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

         return resp;

        }

        /// <summary>
        /// Metodo para exportar la configuracion de formulas de una organizacion
        /// </summary>
        /// <param name="configuracionFormulaExportar"></param>
        /// <returns></returns>
        public bool ExportarArchivo(ConfiguracionFormulaInfo configuracionFormulaExportar)
        {
            bool importarArchivo = false;
            try
            {
                Logger.Info();
                var configuracionFormulaDAL = new ConfiguracionFormulaDAL();
                var formulaBL = new FormulaBL();

                IList<ConfiguracionFormulaInfo> configuracionFormulaInfoLista =
                        configuracionFormulaDAL.ObtenerConfiguracionFormula(configuracionFormulaExportar.OrganizacionID);

                if (configuracionFormulaInfoLista != null)
                {
                    // Se inicia la transaccion
                    using (var transaction = new TransactionScope())
                    {
                        //Se obtiene info separada
                        foreach (var configuracion in configuracionFormulaInfoLista)
                        {
                            //Se obtienen las descripciones de las formulas
                            configuracion.Formula = formulaBL.ObtenerPorID(configuracion.Formula.FormulaId);
                            //Se obtienen las descripciones de las formulas
                            configuracion.FormulaSiguiente = formulaBL.ObtenerPorID(configuracion.FormulaSiguiente.FormulaId);
                        }
                        transaction.Complete();
                    }
                }
                
                //Se genera el archivo excel pero con solo los titulos
                importarArchivo = GenerarArchivo(configuracionFormulaInfoLista, configuracionFormulaExportar);

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
            return importarArchivo;
        }

        /// <summary>
        /// Metodo para generar el archivo excel
        /// </summary>
        /// <param name="configuracionFormulaInfoLista"></param>
        /// <param name="configuracionFormulaExportar"></param>
        /// <returns></returns>
        private bool GenerarArchivo(IList<ConfiguracionFormulaInfo> configuracionFormulaInfoLista,
                                   ConfiguracionFormulaInfo configuracionFormulaExportar)
        {
            bool resp = false;
            try
            {
                Logger.Info();
                string file = configuracionFormulaExportar.NombreArchivo;
                if (File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_ArchivoEnUso);
                    }
                    
                }
                var newFile = new FileInfo(file);

                using (var pck = new ExcelPackage(newFile))
                {
                    // get the handle to the existing worksheet
                    var wsData = pck.Workbook.Worksheets.Add(ResourceServices.ConfigurarFormula_TituloExcel);

                    wsData.Cells["A1"].Value = ResourceServices.ConfigurarFormula_TituloExcel;

                    using (ExcelRange r = wsData.Cells["A1:K1"])
                    {
                        r.Merge = true;
                        r.Style.Font.SetFromFont(new Font("Arial Bold", 16, FontStyle.Regular));
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 178, 34, 34));
                    }

                    ExcelRangeBase dataRange;
                    if (configuracionFormulaInfoLista == null ||
                        configuracionFormulaInfoLista.Count <= 0)
                    {

                        var configuracionFormulaInfo = new ConfiguracionFormulaInfo
                        {
                            Formula = new FormulaInfo{Descripcion = ""},
                            PesoInicioMinimo = 0,
                            PesoInicioMaximo = 0,
                            TipoGanado = "",
                            PesoSalida = 0,
                            FormulaSiguiente = new FormulaInfo{Descripcion = ""},
                            DiasEstanciaMinimo = 0,
                            DiasEstanciaMaximo = 0,
                            DiasTransicionMinimo = 0,
                            DiasTransicionMaximo = 0,
                            Disponibilidad = Disponibilidad.No
                        };
                        configuracionFormulaInfoLista = new List<ConfiguracionFormulaInfo> {configuracionFormulaInfo};

                        dataRange = wsData.Cells["A2"].LoadFromCollection(
                            from s in configuracionFormulaInfoLista
                            orderby s.ConfiguracionFormulaID
                            select new
                            {
                                Fórmula = s.Formula.Descripcion,
                                Peso_Inicio_Mínimo = s.PesoInicioMinimo,
                                Peso_Inicio_Máximo = s.PesoInicioMaximo,
                                Tipo_Ganado = s.TipoGanado,
                                Peso_Salida = s.PesoSalida,
                                Fórmula_Siguiente = s.FormulaSiguiente.Descripcion,
                                Días_Estancia_Mínimo = s.DiasEstanciaMinimo,
                                Días_Estancia_Máximo = s.DiasEstanciaMaximo,
                                Días_Transición_Mínimo = s.DiasTransicionMinimo,
                                Días_Transición_Máximo = s.DiasTransicionMaximo,
                                Disponibilidad = s.Disponibilidad
                            },
                            true, TableStyles.None);
                    }else
                    {
                        dataRange = wsData.Cells["A2"].LoadFromCollection(
                            from s in configuracionFormulaInfoLista
                            orderby s.ConfiguracionFormulaID
                            select new
                            {
                                Fórmula = s.Formula.Descripcion,
                                Peso_Inicio_Mínimo = s.PesoInicioMinimo,
                                Peso_Inicio_Máximo = s.PesoInicioMaximo,
                                Tipo_Ganado = s.TipoGanado,
                                Peso_Salida = s.PesoSalida,
                                Fórmula_Siguiente = s.FormulaSiguiente.Descripcion,
                                Días_Estancia_Mínimo = s.DiasEstanciaMinimo,
                                Días_Estancia_Máximo = s.DiasEstanciaMaximo,
                                Días_Transición_Mínimo = s.DiasTransicionMinimo,
                                Días_Transición_Máximo = s.DiasTransicionMaximo,
                                Disponibilidad = s.Disponibilidad,
                            },
                            true, TableStyles.None);
                    }
                    
                    using (ExcelRange r = wsData.Cells["A2:K2"])
                    {
                        r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        r.Style.Font.SetFromFont(new Font("Arial Bold", 11, FontStyle.Regular));
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 178, 34, 34));
                    }

                    IList<FormulaInfo> formulasList = new List<FormulaInfo>();
                    var formulasBL = new FormulaBL();
                    formulasList = formulasBL.ObtenerTodos();

                    IList<Disponibilidad> disponibilidadEnums =
                        Enum.GetValues(typeof(Disponibilidad)).Cast<Disponibilidad>().OrderByDescending(x=>x.ToString()).ToList();

                    if (formulasList != null && formulasList.Count > 0)
                    {

                        var celdasValidacionFormulas = String.Format("{0}{1}", "A3:A", configuracionFormulaInfoLista.Count + 2);
                        AsignarValidacionFormulas(celdasValidacionFormulas, 
                                                  wsData,
                                                  formulasList);

                        celdasValidacionFormulas = String.Format("{0}{1}", "F3:F", configuracionFormulaInfoLista.Count + 2);
                        AsignarValidacionFormulas(celdasValidacionFormulas, 
                                                  wsData,
                                                  formulasList);

                        celdasValidacionFormulas = String.Format("{0}{1}", "K3:K", configuracionFormulaInfoLista.Count + 2);
                        AsignarValidacionFormulas(celdasValidacionFormulas,
                                                  wsData,
                                                  disponibilidadEnums);

                        AgregarComentarios(wsData,configuracionFormulaInfoLista);
                        
                    }

                    AgregarPestañaTipoGanado(pck);

                    dataRange.AutoFitColumns();
                   
                    pck.Save();
                }
                resp = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }

        /// <summary>
        /// Agregar la pestaña de tipo de aganado como catalogo y ayuda en el excel
        /// </summary>
        /// <param name="pck"></param>
        private void AgregarPestañaTipoGanado(ExcelPackage pck)
        {
            try
            {
                // get the handle to the existing worksheet
                var wsData = pck.Workbook.Worksheets.Add(ResourceServices.ConfigurarFormula_PestañaTipoGanado);

                wsData.Cells["A1"].Value = ResourceServices.ConfigurarFormula_PestañaTipoGanado;

                using (ExcelRange r = wsData.Cells["A1:B1"])
                {
                    r.Merge = true;
                    r.Style.Font.SetFromFont(new Font("Arial Bold", 16, FontStyle.Regular));
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 178, 34, 34));
                    //FromArgb(100, 0, 150)
                }

                IList<TipoGanadoInfo> tipoGanadoList = new List<TipoGanadoInfo>();
                TipoGanadoBL tipoGanadoBL = new TipoGanadoBL();
                tipoGanadoList = tipoGanadoBL.ObtenerTodos();

                //var i = 0;
                var dataRange = wsData.Cells["A2"].LoadFromCollection(
                    
                    from s in tipoGanadoList
                    orderby s.TipoGanadoID
                    select new
                    {
                        //Identificador = i++,
                        Identificador = s.TipoGanadoID,
                        Descripcion = s.Descripcion
                    },
                    true, TableStyles.None);

                using (ExcelRange r = wsData.Cells["A2:B2"])
                {
                    r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    r.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    r.Style.Font.SetFromFont(new Font("Arial Bold", 11, FontStyle.Regular));
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 178, 34, 34));
                }

                dataRange.AutoFitColumns();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Funcion para agregar comentarios a las celdas
        /// </summary>
        /// <param name="wsData"></param>
        /// <param name="configuracionFormulaInfoLista"></param>
        private void AgregarComentarios(ExcelWorksheet wsData, IList<ConfiguracionFormulaInfo> configuracionFormulaInfoLista)
        {
            int i = 3;
            foreach (var comentario in configuracionFormulaInfoLista.Select(configuracionFormulaInfo => wsData.Cells[i, 4]))
            {
                comentario.AddComment(ResourceServices.ConfigurarFormula_ComentarioEjemploLlenarTipoGanado,
                    "CF");
                i++;
            }
        }

        /// <summary>
        /// Agregar validaciones a las formulas
        /// </summary>
        /// <param name="celdasValidacionFormulas"></param>
        /// <param name="wsData"></param>
        /// <param name="formulasList"></param>
        private void AsignarValidacionFormulas(string celdasValidacionFormulas, 
                                               ExcelWorksheet wsData, 
                                               IList<FormulaInfo> formulasList)
        {
            try
            {
                var validation = wsData.DataValidations.AddListValidation(celdasValidacionFormulas);
                validation.ShowErrorMessage = true;
                validation.ErrorStyle = ExcelDataValidationWarningStyle.warning;
                validation.ErrorTitle = ResourceServices.ConfigurarFormula_ValorInvalido;
                validation.Error = ResourceServices.ConfigurarFormula_SeleccioneValorDeLista;

                foreach (var formulaInfo in formulasList)
                {
                    validation.Formula.Values.Add(formulaInfo.Descripcion);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Agregar Validaciones a la disponibilidad
        /// </summary>
        /// <param name="celdasValidacionFormulas"></param>
        /// <param name="wsData"></param>
        /// <param name="disponibilidadList"></param>
        private void AsignarValidacionFormulas(string celdasValidacionFormulas,
                                               ExcelWorksheet wsData,
                                               IList<Disponibilidad> disponibilidadList)
        {
            try
            {
                var validation = wsData.DataValidations.AddListValidation(celdasValidacionFormulas);
                validation.ShowErrorMessage = true;
                validation.ErrorStyle = ExcelDataValidationWarningStyle.warning;
                validation.ErrorTitle = ResourceServices.ConfigurarFormula_ValorInvalido;
                validation.Error = ResourceServices.ConfigurarFormula_SeleccioneValorDeLista;

                foreach (var disponibilidadInfo in disponibilidadList)
                {
                    validation.Formula.Values.Add(disponibilidadInfo.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal List<ConfiguracionFormulaInfo> ObtenerFormulaPorTipoGanado(TipoGanadoInfo tipoGanadoIn, int organizacionID)
        {
            List<ConfiguracionFormulaInfo> result;
            try
            {
                Logger.Info();
                var configuracionFormulaDAL = new ConfiguracionFormulaDAL();
                result = configuracionFormulaDAL.ObtenerFormulaPorTipoGanado(tipoGanadoIn, organizacionID);
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
