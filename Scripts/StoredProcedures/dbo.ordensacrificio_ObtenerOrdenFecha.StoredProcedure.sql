USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ordensacrificio_ObtenerOrdenFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ordensacrificio_ObtenerOrdenFecha]
GO
/****** Object:  StoredProcedure [dbo].[ordensacrificio_ObtenerOrdenFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[ordensacrificio_ObtenerOrdenFecha]     
(    
 @fechaOrden VARCHAR(30)    
 ,@OrganizacionID INT    
)    
AS     
BEGIN    
 Declare @Fecha As datetime    
 SET DATEFORMAT  dmy  
    Set @Fecha =  convert(datetime, substring (@fechaOrden,1,10))      
        
    SELECT     
OrdenSacrificioID    
,FolioOrdenSacrificio    
,OrganizacionID    
,Observaciones    
,FechaOrden    
,EstatusID    
,Activo    
,FechaCreacion    
,UsuarioCreacionID    
,CASE WHEN FechaModificacion is null THEN  FechaModificacion    
   ELSE NULL    
 END AS 'FechaModificacion'    
,UsuarioModificacionID    
    FROM     
    OrdenSacrificio(NOLOCK)    
    WHERE     
    convert(char(10),FechaOrden,120) = convert(char(10),@Fecha,120)     
    AND  OrganizacionID = @OrganizacionID    
END    
    
GO
