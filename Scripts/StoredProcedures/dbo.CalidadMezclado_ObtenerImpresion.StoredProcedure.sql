USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezclado_ObtenerImpresion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMezclado_ObtenerImpresion]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezclado_ObtenerImpresion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -----------------------------------------------------------    
-- =============================================  
-- Author:  Jorge Luis Velazquez Araujo  
-- Create date: 17/12/2014  
-- Description: Obtiene los datos para la impresion de la Distribucion de Alimento  
-- CalidadMezclado_ObtenerImpresion 1,'20141223'  
-- =============================================  
CREATE PROCEDURE [dbo].[CalidadMezclado_ObtenerImpresion] @OrganizacionID INT  
 ,@Fecha DATE  
AS  
BEGIN  
 CREATE TABLE #CALIDADMEZCLADO (  
  CalidadMezcladoID INT  
  ,Tecnica VARCHAR(50)  
  ,Organizacion VARCHAR(50)  
  ,Formula VARCHAR(50)  
  ,FechaPremezcla DATETIME  
  ,FechaBatch DATETIME  
  ,Corral VARCHAR(10)  
  ,TipoLugarMuestra VARCHAR(50)  
  ,Chofer VARCHAR(200)  
  ,EncargadoMezcladora VARCHAR(200)  
  ,Batch INT  
  ,TiempoMezclado INT  
  ,EncargadoMuestreo VARCHAR(200)  
  ,GramosMicrotoxina INT  
  )  
 INSERT INTO #CALIDADMEZCLADO  
 SELECT cm.CalidadMezcladoID  
  ,tt.Descripcion AS Tecnica  
  ,o.Descripcion AS Organizacion  
  ,fo.Descripcion AS Formula  
  ,cm.FechaPremezcla  
  ,cm.FechaBatch  
  ,co.Codigo AS Corral  
  ,tlm.Descripcion AS TipoLugarMuestra  
  ,ch.Nombre + ' ' + ch.ApellidoPaterno + ' ' + ch.ApellidoMaterno AS Chofer  
  ,op.Nombre + ' ' + op.ApellidoPaterno + ' ' + op.ApellidoMaterno AS EncargadoMezcladora  
  ,cm.Batch  
  ,cm.TiempoMezclado  
  ,op1.Nombre + ' ' + op1.ApellidoPaterno + ' ' + op1.ApellidoMaterno AS EncargadoMuestreo  
  ,cm.GramosMicrotoxina  
 FROM CalidadMezclado cm  
 INNER JOIN TipoTecnica tt ON cm.TipoTecnicaID = tt.TipoTecnicaID  
 INNER JOIN Organizacion o ON cm.OrganizacionID = o.OrganizacionID  
 INNER JOIN Formula fo ON cm.FormulaID = fo.FormulaID  
 LEFT JOIN Lote lo ON cm.LoteID = lo.LoteID  
 LEFT JOIN Corral co ON lo.CorralID = co.CorralID  
 INNER JOIN TipoLugarMuestra tlm ON cm.TipoLugarMuestraID = tlm.TipoLugarMuestraID  
 left JOIN Chofer ch ON cm.ChoferID = ch.ChoferID  
 left JOIN Operador op ON cm.OperadorIDMezclador = op.OperadorID  
 INNER JOIN Operador op1 ON cm.OperadorIDAnalista = op1.OperadorID  
 WHERE cm.OrganizacionID = @OrganizacionID    
  AND CAST(cm.Fecha AS DATE) = @Fecha  
 SELECT CalidadMezcladoID  
  ,Tecnica  
  ,Organizacion  
  ,Formula  
  ,FechaPremezcla  
  ,FechaBatch  
  ,Corral  
  ,TipoLugarMuestra  
  ,Chofer  
  ,EncargadoMezcladora  
  ,Batch  
  ,TiempoMezclado  
  ,EncargadoMuestreo  
  ,GramosMicrotoxina  
 FROM #CALIDADMEZCLADO  
 SELECT 
   cm.CalidadMezcladoID  AS CalidadMezcladoID
   ,tm.Descripcion AS TipoMuestra  
  ,cmf.Factor AS ParticulasEsperadas  
  ,cmf.PesoBaseHumeda  
  ,cmf.PesoBaseSeca  
  ,cmd.NumeroMuestra  
  ,cmd.Peso  
  ,cmd.Particulas  
 FROM CalidadMezcladoDetalle cmd  
 INNER JOIN #CALIDADMEZCLADO cm ON cmd.CalidadMezcladoID = cm.CalidadMezcladoID  
 INNER JOIN TipoMuestra tm ON cmd.TipoMuestraID = tm.TipoMuestraID  
 INNER JOIN CalidadMezcladoFactor cmf ON cmd.TipoMuestraID = cmf.TipoMuestraID  
END

GO
