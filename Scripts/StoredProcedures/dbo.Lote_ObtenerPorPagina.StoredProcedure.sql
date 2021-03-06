USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================    
-- Author     : Cesar Fernando Vega Vazquez    
-- Create date: 28/04/2014  
-- Description:     
-- SpName     : Lote_ObtenerPorPagina  
-- Modo de Uso: Lote_ObtenerPorPagina 4, null, '1', 1, 1, 15  
--======================================================    
CREATE PROCEDURE [dbo].[Lote_ObtenerPorPagina]    
@Organizacion int,    
@LoteID int,    
@Descripcion varchar(20),    
@Activo BIT,    
@Inicio INT,    
@Limite INT     
AS    
BEGIN    
 SET NOCOUNT ON;    
 if(@Descripcion is null)  
 BEGIN  
  SET @Descripcion = ''  
 END  
 SET @Descripcion = '%' + @Descripcion + '%'  
 IF(@LoteID is null)  
 BEGIN  
  set @LoteID = 0  
 END  
 create table #Lote  
 (  
  Indice int   
  , LoteID int  
  , Lote varchar(20)  
 )  
 INSERT INTO #Lote (Indice, LoteID, Lote)  
  SELECT    
   ROW_NUMBER() OVER (ORDER BY Lote ASC),  
   LoteID,    
   Lote    
  FROM   
   Lote    
  WHERE   
   @LoteId in (Lote, 0)  
   AND Activo = @Activo    
   AND OrganizacionID = @Organizacion  
   And Lote like @Descripcion     
 SELECT    
  LoteID,    
  Lote,
  @Organizacion as OrganizacionID
 FROM   
  #Lote    
 WHERE   
  Indice BETWEEN @Inicio AND @Limite    
 SELECT    
  COUNT(LoteID) AS [TotalReg]    
 FROM   
  #Lote    
 DROP TABLE #Lote    
 SET NOCOUNT OFF;    
END 

GO
