using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Transactions;
using BLToolkit.Data.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Properties;
using Color = iTextSharp.text.Color;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace SIE.Services.Servicios.BL
{
    internal class EvaluacionCorralBL
    {
        /// <summary>
        ///     Metodo que crear una evaluacion de riesgo de partida
        /// </summary>
        /// <param name="evaluacionCorral"></param>
        /// <param name="tipoFolio"></param>
        internal int GuardarEvaluacionCorral(EvaluacionCorralInfo evaluacionCorral, int tipoFolio)
        {
            try
            {
                Logger.Info();
                var evaluacionCorralDAL = new EvaluacionCorralDAL();

                ValidaCamposEntradaGanado(evaluacionCorral);

                using (var transaccion = new TransactionScope())
                {
                    int evaluacionID = 0;
                    evaluacionID = evaluacionCorralDAL.GuardarEvaluacionCorral(evaluacionCorral, tipoFolio);
                    if (evaluacionID > 0)
                    {
                        evaluacionCorralDAL.GuardarEvaluacionCorralDetalle(evaluacionCorral.PreguntasEvaluacionCorral, evaluacionID);
                    }
                    else
                    {
                        throw new ExcepcionDesconocida("NO SE PUEDE GUARDAR LA EVALUACION DEL CORRAL");
                    }

                    transaccion.Complete();
                    return evaluacionID;
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
        }

        /// <summary>
        /// Valida que los campos de Tipo Cadena Contengan valor no Nulo
        /// </summary>
        /// <param name="evaluacionCorralInfo"></param>
        internal void ValidaCamposEntradaGanado(EvaluacionCorralInfo evaluacionCorralInfo)
        {
            if (String.IsNullOrWhiteSpace(evaluacionCorralInfo.Justificacion))
            {
                evaluacionCorralInfo.Justificacion = String.Empty;
            }
        }

        internal ResultadoInfo<EvaluacionCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, EvaluacionCorralInfo filtro)
        {
            ResultadoInfo<EvaluacionCorralInfo> result;
            try
            {
                Logger.Info();
                var evaluacionDAL = new EvaluacionCorralDAL();
                result = evaluacionDAL.ObtenerPorPagina(pagina, filtro);
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

        internal List<EvaluacionCorralInfo> ObtenerEvaluaciones(EvaluacionCorralInfo filtro)
        {
            List<EvaluacionCorralInfo> result;
            try
            {
                Logger.Info();
                var evaluacionDAL = new EvaluacionCorralDAL();
                result = evaluacionDAL.ObtenerEvaluaciones(filtro);
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
        internal void ImprimirEvaluacionPartida(EvaluacionCorralInfo evaluacionCorralInfoSelecionado,bool webForm)
        {
            var reporte = new Document(PageSize.A4, 10, 10, 35, 75);
            var preguntaPl = new PreguntaBL();
            var evaluacionBL = new EvaluacionCorralBL();
            try
            {
                string nombreArchivo = "EvaluacionPartida.pdf";

                nombreArchivo = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,nombreArchivo);

                if (File.Exists(nombreArchivo))
                {
                    try
                    {
                        File.Delete(nombreArchivo);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_ArchivoEnUso);
                    }

                }

                PdfWriter.GetInstance(reporte, new FileStream(nombreArchivo, FileMode.OpenOrCreate));
                reporte.Open();
                var dirLogo = "";

                if (webForm)
                {
                    dirLogo = AppDomain.CurrentDomain.BaseDirectory + "Images\\skLogo.png";
                }
                else
                {
                    dirLogo = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\skLogo.png";
                }
                
                var fuenteDatos = new Font { Size = 8, Color = Color.BLACK };

                var imgSuperior = Image.GetInstance(dirLogo);
                imgSuperior.ScaleAbsolute(90f, 25f);

                //float[] medidaCeldas = { 0.75f, 1.20f, 0.75f, 0.75f, 0.75f, 1.20f, 1.0f, 2.25f, 0.85f, 0.85f, 0.85f };
                var table = new PdfPTable(3);
                //table.SetWidths(medidaCeldas);
                //

                //////TITULO
                var cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_CodigoISO, fuenteDatos))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = 0
                };
                table.AddCell(cell);
                 cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_RevisionISO, fuenteDatos))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = 0
                };
                 table.AddCell(cell);
                 cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_FechaISO, fuenteDatos))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = 0
                };
                table.AddCell(cell);
                //Estapacio en blanco
                cell = new PdfPCell(new Phrase(" ", fuenteDatos))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = 0,
                    Colspan = 3
                };
                table.AddCell(cell);
                
                cell = new PdfPCell(imgSuperior) { Padding = 2, Border = 0 };
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                cell.BorderWidth = 1.2f;
                cell.BackgroundColor = Color.RED;// new Color(0xC0, 0xC0, 0xC0);
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_TituloImpresion, FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))) ;
                // the first cell spans 10 columns
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 10;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                cell.BorderWidth = 1.2f;
                cell.BackgroundColor = Color.RED;// new Color(0xC0, 0xC0, 0xC0);
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {  Border = 0 };
                table.AddCell(cell);
                reporte.Add(table);
                
                
                //////////////Datos generales
                table = new PdfPTable(8);
                //BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                //Font fuenteDatosCursiva = new Font(bfTimes, 9, Font.ITALIC, Color.RED);

                var fuenteDatosCursiva = new Font { Size = 9, Color = Color.BLACK,};

                cell = new PdfPCell(new Phrase(" ", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER ,
                    Colspan = 8
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_Corral, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.LEFT_BORDER
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.Corral.Codigo.Trim(), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = 0
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_SubTituloImpresion, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.RIGHT_BORDER,
                    Colspan = 6
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_Cbz, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.LEFT_BORDER,
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.Cabezas.ToString(), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = 0
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.RIGHT_BORDER,
                    Colspan = 6
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_NumPartida, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.LEFT_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.PartidasAgrupadas, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.BOTTOM_BORDER,
                    //BorderWidth = 1.0f,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_FechaLlegada, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = 0,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.FechaRecepcion.ToShortDateString() , fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER,
                    //BorderWidth = 1.2f,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_ProcedenciaAnimales, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    Border = Rectangle.LEFT_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.OrganizacionOrigenAgrupadas, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.BOTTOM_BORDER,
                    //BorderWidth = 1.2f,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_FechaEvaluacion, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    Border = 0,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.FechaEvaluacion.ToShortDateString(), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER,
                    //BorderWidth = 1.2f,
                    Colspan = 2
                };
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_InventarioInicial, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    Border = Rectangle.LEFT_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.Cabezas.ToString(), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    Border = 0,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_Evaluador, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_BOTTOM,
                    Border = Rectangle.NO_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                var operadorBL = new OperadorBL();
                evaluacionCorralInfoSelecionado.Operador = operadorBL.ObtenerPorID(evaluacionCorralInfoSelecionado.Operador.OperadorID);
                cell = new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.Operador.NombreCompleto, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER,
                    //BorderWidth = 1.2f,
                    Colspan = 2
                };
                table.AddCell(cell);

                ResultadoInfo<PreguntaInfo> resultadoPreguntasEnfermeria = 
                    preguntaPl.ObtenerPorFormularioID((int)TipoPreguntas.DatosEnfermeria);
                var animalesGrado3 = 0;
                foreach (string respuesta in 
                                    from pregunta in resultadoPreguntasEnfermeria.Lista 
                                    where pregunta.Descripcion.Contains("Grado 3") 
                                    select evaluacionBL.ObtenerRespuestaAPreguntaEvaluacion(
                                        pregunta.PreguntaID,
                                        evaluacionCorralInfoSelecionado.EvaluacionID
                                    )
                         )
                {
                    animalesGrado3 = int.Parse(respuesta);
                }

                //Se calcula el inventario final (Inventario Inicial - Grado 3)
                var inventarioFinal = evaluacionCorralInfoSelecionado.Cabezas;

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_InventarioFinal, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.LEFT_BORDER,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase((inventarioFinal - animalesGrado3).ToString(), fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = 0,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_HoraEvaluacion, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = 0,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.HoraEvaluacion, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER,
                    //BorderWidth = 1.2f,
                    Colspan = 2
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.LEFT_BORDER |  Rectangle.RIGHT_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);

                /////////////////Preguntas seccion 1
                
                ResultadoInfo<PreguntaInfo> resultadoPreguntas = preguntaPl.ObtenerPorFormularioID((int)TipoPreguntas.EvaluacionRiesgo);
                var total = 0;
                if (resultadoPreguntas != null && resultadoPreguntas.Lista != null && resultadoPreguntas.Lista.Count > 0)
                {
                    
                    foreach (var pregunta in resultadoPreguntas.Lista)
                    {
                        string respuesta = evaluacionBL.ObtenerRespuestaAPreguntaEvaluacion(
                                                            pregunta.PreguntaID,
                                                            evaluacionCorralInfoSelecionado.EvaluacionID
                                                        );
                        string[] preguntasValor = pregunta.Descripcion.Split('.');
                        cell = new PdfPCell(new Phrase(preguntasValor[0], fuenteDatosCursiva))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            Border = Rectangle.LEFT_BORDER,
                            Colspan = 6
                        };
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(respuesta, fuenteDatosCursiva))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            Border = Rectangle.RECTANGLE | Rectangle.TOP_BORDER,
                            //BorderWidth = 1.2f,
                            Colspan = 2
                        };
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(preguntasValor[1], fuenteDatosCursiva))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                            Colspan = 8
                        };
                        table.AddCell(cell);

                        if (respuesta == "1")
                        {
                            preguntasValor[1] = Regex.Replace(preguntasValor[1], "[^0-9]+", "");
                            total += int.Parse(preguntasValor[1]);
                        }
                    }
                }
                /////////////Total

                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_Total, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = Rectangle.LEFT_BORDER,
                    Colspan = 6
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(total.ToString(), fuenteDatosCursiva))
                {
                    BackgroundColor = new Color(169, 245, 242),
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = Rectangle.RECTANGLE | Rectangle.TOP_BORDER,
                    //BorderWidth = 1.2f,
                    Colspan = 2
                };
                table.AddCell(cell);


                /////////////////Preguntas Nota
                cell = new PdfPCell(new Phrase(" ", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_Nota, fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);



                /////////////////Preguntas seccion Enfermeria
                cell = new PdfPCell(new Phrase(" ", fuenteDatosCursiva))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);

                var fuenteDatosEnfermeria = new Font { Size = 8, Color = Color.BLACK };
                string[] textosPartidasGarrapatas = new[] { "PARTIDA CON GARRAPATA", "LEVE ≤ 5 cbz", "MODERADO > 5 cbz", "GRAVE ≥ 15 cbz" };

                if (resultadoPreguntasEnfermeria != null && resultadoPreguntasEnfermeria.Lista != null && resultadoPreguntasEnfermeria.Lista.Count > 0)
                {
                    var indice = 0;
                    foreach (var pregunta in resultadoPreguntasEnfermeria.Lista)
                    {
                        string respuesta = evaluacionBL.ObtenerRespuestaAPreguntaEvaluacion(
                                                            pregunta.PreguntaID,
                                                            evaluacionCorralInfoSelecionado.EvaluacionID
                                                        );
                        cell = new PdfPCell(new Phrase(pregunta.Descripcion, fuenteDatosEnfermeria))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            Border = Rectangle.LEFT_BORDER,
                            Colspan = 3
                        };
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(respuesta, fuenteDatosEnfermeria))
                        {
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            VerticalAlignment = Element.ALIGN_BOTTOM,
                            Border = Rectangle.BOTTOM_BORDER,
                            //BorderWidth = 1.2f,
                            Colspan = 1
                        };
                        table.AddCell(cell);

                        if (indice < 4)
                        {
                            if (indice == 0)
                            {
                                var texto = String.Format("{0}", textosPartidasGarrapatas[indice]);
                                cell = new PdfPCell(new Phrase(texto, fuenteDatosEnfermeria))
                                {
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    Border = Rectangle.NO_BORDER,
                                    Colspan = 3
                                };
                                table.AddCell(cell);
                            }
                            else
                            {
                                var texto = "";
                                texto = String.Format("{0}    {1}", textosPartidasGarrapatas[indice], 
                                    textosPartidasGarrapatas[indice].Contains(
                                    evaluacionCorralInfoSelecionado.NivelGarrapata.ToString().ToUpper()) ? "(X)" : "(   )");

                                cell = new PdfPCell(new Phrase(texto, fuenteDatosEnfermeria))
                                {
                                    HorizontalAlignment = Element.ALIGN_RIGHT,
                                    Border = Rectangle.NO_BORDER,
                                    Colspan = 2
                                };
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase("", fuenteDatosEnfermeria))
                                {
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    Border = Rectangle.NO_BORDER
                                };
                                table.AddCell(cell);

                            }

                            cell = new PdfPCell(new Phrase("", fuenteDatosEnfermeria))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                Border = Rectangle.RIGHT_BORDER
                            };
                            table.AddCell(cell);
                            
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(" ", fuenteDatosEnfermeria))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                Border = Rectangle.RIGHT_BORDER,
                                Colspan = 4
                            };
                            table.AddCell(cell);
                        }

                        indice++;
                    }
                }

                ///////////Partida con garrapata
                //////////Aseguir con metafilaxia

                cell = new PdfPCell(new Phrase(" ", fuenteDatos))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fuenteDatos))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);

                if (evaluacionCorralInfoSelecionado.EsMetafilaxia || 
                    evaluacionCorralInfoSelecionado.MetafilaxiaAutorizada)
                {
                    cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_AccionASeguirMetafilaxia, fuenteDatosEnfermeria))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                        Colspan = 8
                    };
                    table.AddCell(cell);

                    /* Metafilaxia Autorizada */
                    if (evaluacionCorralInfoSelecionado.MetafilaxiaAutorizada)
                    {
                        cell =
                            new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_AccionASeguirMetafilaxiaJustificacion,
                                fuenteDatosEnfermeria))
                            {
                                HorizontalAlignment = Element.ALIGN_LEFT,
                                Border = Rectangle.LEFT_BORDER,
                                Colspan = 1
                            };
                        table.AddCell(cell);

                        cell =
                           new PdfPCell(new Phrase(evaluacionCorralInfoSelecionado.Justificacion,
                               fuenteDatosEnfermeria))
                           {
                               HorizontalAlignment = Element.ALIGN_LEFT,
                               Border = Rectangle.RIGHT_BORDER,
                               Colspan = 7
                           };
                        table.AddCell(cell);
                    }

                }
                else
                {
                    cell = new PdfPCell(new Phrase(ResourceServices.EvaluacionPartida_AccionASeguirNormal, fuenteDatosEnfermeria))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                        Colspan = 8
                    };
                    table.AddCell(cell);
                    
                }

                cell = new PdfPCell(new Phrase(" ", fuenteDatos))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                    Colspan = 8
                };
                table.AddCell(cell);

                reporte.Add(table);
                
                reporte.Close();

                if (!webForm)
                {
                    SendToPrinter(nombreArchivo);
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (DocumentException ex)
            {
                Logger.Error(ex);
                reporte.Close();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (IOException ex)
            {
                Logger.Error(ex);
                reporte.Close();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                reporte.Close();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Metodo para obtener la respuesta a la pregunta de evaluacion de partidas
        /// </summary>
        /// <param name="preguntaId"></param>
        /// <param name="evaluacionId"></param>
        /// <returns></returns>
        private string ObtenerRespuestaAPreguntaEvaluacion(int preguntaId, int evaluacionId)
        {
            string respuesta = "";
            try
            {
                Logger.Info();
                var evaluacionCorralDAL = new EvaluacionCorralDAL();
                respuesta = evaluacionCorralDAL.ObtenerRespuestaAPreguntaEvaluacion(preguntaId, evaluacionId);

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
            return respuesta;
        }

        /// <summary>
        /// Inviar la pantalla de impresion
        /// </summary>
        /// <param name="archivo"></param>
        private void SendToPrinter(string archivo)
        {
            try
            {
                Process.Start(archivo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
    }
}
