USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[howto_sacrificiolucero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[howto_sacrificiolucero]
GO
/****** Object:  StoredProcedure [dbo].[howto_sacrificiolucero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[howto_sacrificiolucero]
as
print '/******************************************************************************************************************************************'
print '* INSTRUCCIONES PARA GENERAR EL CIERRE DE SACRIFICIO DE LUCERO DE MANERA MANUAL															  '
print '* 																																		  '
print '* OrganizacionID:																														  '
print '* 	- 1 = Culiacan																														  '
print '* 	- 2 = Mexicali (no aplica)																											  '
print '* 	- 3 = Monterrey																														  '
print '* 	- 4 = Monarca																														  '
print '* 																																		  '
print '* Fecha:																																	  '
print '* 	- Formato de Fecha AAAA-MM-DD																										  '
print '******************************************************************************************************************************************/'
print '																																			  '
print '/******************************************************************************************************************************************'
print '* Validar la disponibilidad del sacrificio en base a los folios de salida separados por coma.											  '
print '* Copiar las columnas LoteID, InterfaceSalidaTraspasoDetalleID, Cabezas en el documento de Excel en las columnas I, J y K respectivamente. '
print '* Verificar que la columna M diga ''SI''																									  '
print '******************************************************************************************************************************************/'
print 'exec sp_validarDisponibilidadSacrificioLucero ''1709,1710,1711,1712,1713,1718''															  '
print ' 																																		  '
print '/******************************************************************************************************************************************'
print '* Obtener la informacion del Sacrificio directamente del SCP enviando Fecha y Organizacion.												  '
print '* Copiar la columna Lote en el documento de Excel en la columna F.																		  '
print '******************************************************************************************************************************************/'
print 'exec sp_ObtenerLoteSacrificioLucero ''2015-09-19'', 3																						  '
print ' 																																		  '
print '/******************************************************************************************************************************************'
print '* Copiar del documento de Excel la columna Q debajo de la instruccion insert, para llenar la tabla ProgramacionSacrificioLucero			  '
print '******************************************************************************************************************************************/'
print 'insert into ProgramacionSacrificioLucero(Folio, Corral, Lote, Cabezas, ISTDID, LoteID, Fecha, OrganizacionID)							  '
print '																																			  '
print '/******************************************************************************************************************************************'
print '* Ejecutar el store para generar toda la informacion del Sacrificio																		  '
print '******************************************************************************************************************************************/'
print 'exec sp_EjecutarProgramacionSacrificioLucero																								  '
print ' 																																		  '
print '/******************************************************************************************************************************************'
print '* Verificar si la ejecucion de la programacion es coherente.																				  '
print '* Copiar la columna Cabezas en el documento de Excel en la columna L.																	  '
print '* Verificar que la columna N diga ''SI''																									  '
print '******************************************************************************************************************************************/'
print 'exec sp_verificarSacrificioLucero ''1709,1710,1711,1712,1713,1718''																		  '
print '																																			  '
print '/******************************************************************************************************************************************'
print '* Pasos para verificar sacrificio de lucero de Monarca, no es necesario																	  '
print '******************************************************************************************************************************************/'
print '-- exec ordensacrificio_ObtenerOrdenFecha ''01-09-2015'', 4																				  '
print '-- exec ObtenerRelacionCorralSacrificioIntensivo 610																						  '
print '																																			  '
print '/******************************************************************************************************************************************'
print '* Store para culminar el cierre de sacrificio, recibe los parametros																		  '
print '* - fecha																																  '
print '* - organizacion																															  '
print '* - descargarInventario																													  '
print '* - descargarConsumo																														  '
print '* - eliminar																																  '
print '* - activar																																  '
print '*																																		  '
print '* primero ejecutar con los parametros fecha y organizacion para verificar el numero de cabezas, detalle y movimientos que deben ser los    '
print '*	sacrificados.																														  '
print '* segundo ejecutar con los parametros fecha, organizacion, descargarInventario = 1 y descargarConsumo = 1 para enviar al historico		  '
print '* tercero ejecutar con los parametros fecha, organizacion, descargarInventario = 0, descargarConsumo = 0, eliminar = 1 y activar = 1 para  '
print '*	dejar limpiar las transaccionales.																									  '
print '******************************************************************************************************************************************/'
print 'exec sp_cierreSacrificioLucero ''2015-09-19'', 3																				  '




GO
