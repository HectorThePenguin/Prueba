using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.WinForm.Auxiliar
{
    public class ExportarPoliza
    {
        public void ImprimirPoliza(MemoryStream stream, string nombreArchivo)
        {
            var file = new SaveFileDialog
                           {FileName = nombreArchivo, Filter = @"Archivos PDF|*.pdf", Title = @"Guardar Archivo PDF"};
            var result = file.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (file.FileName != "")
                {
                    if (File.Exists(file.FileName))
                    {
                        try
                        {
                            var archivoPDF = new FileStream(file.FileName, FileMode.Open);
                            archivoPDF.Close();
                        }
                        catch (IOException)
                        {
                            throw new ExcepcionServicio(Properties.Resources.ExportarExcel_ArchivoAbierto);
                        }
                    }
                    try
                    {
                        File.WriteAllBytes(file.FileName, stream.ToArray());
                        //Process.Start(file.FileName);
                        var proceso = new Process
                                          {
                                              StartInfo =
                                                  {FileName = file.FileName, Verb = "print", CreateNoWindow = false}
                                          };
                        proceso.Start();
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        stream.Flush();
                        stream.Close();
                        stream.Dispose();
                    }
                }
            }
        }
    }
}
