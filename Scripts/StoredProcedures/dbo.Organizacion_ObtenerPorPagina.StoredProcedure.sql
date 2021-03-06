USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  Raul Esquer    
-- Create date: 21-10-2013    
-- Description: Otiene un listado de organizaciones paginado    
-- Organizacion_ObtenerPorPagina 1, '', 1, 1,10     
--001 Jorge Luis Velazquez Ararujo 31/07/2015 **Se agrega para que regrese las columnas de Division y Sociedad    
-- =============================================    
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorPagina]    
 @OrganizacionID INT,        
 @Descripcion NVARCHAR(50),         
 @Activo BIT,        
 @Inicio INT,         
 @Limite INT        
AS        
BEGIN        
 SET NOCOUNT ON;        
 SELECT         
     ROW_NUMBER() OVER ( ORDER BY O.descripcion ASC) AS RowNum,        
  O.OrganizacionID,        
  O.TipoOrganizacionID,         
  O.Descripcion,      
  O.Division,--001    
  O.Sociedad,--001      
  O.Direccion,        
  O.Rfc,      
  O.IvaID,        
  O.Activo,  
  O.ZonaID          
  INTO #Organizacion        
  FROM Organizacion O        
  WHERE (O.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')        
    AND @OrganizacionID IN (O.OrganizacionID, 0)        
    AND O.Activo = @Activo        
 SELECT         
  o.OrganizacionID,         
  o.TipoOrganizacionID,        
  ot.Descripcion as [TipoOrganizacion],          
  o.Descripcion,     
  o.Division,--001    
  o.Sociedad,--001    
  o.Direccion,          
  o.Rfc,      
  o.IvaID,        
  i.Descripcion as [Iva],        
  o.Activo,       
  o.ZonaID,   
  z.Descripcion as [Zona]  
 FROM    #Organizacion o          
 INNER JOIN TipoOrganizacion ot on ot.TipoOrganizacionID = o.TipoOrganizacionID        
 INNER JOIN Iva i on i.IvaID = o.IvaID    
 LEFT JOIN Zona Z ON o.ZonaID = z.ZonaID  
 WHERE   RowNum BETWEEN @Inicio AND @Limite        
 SELECT         
  COUNT(OrganizacionID)AS TotalReg         
 FROM #Organizacion         
END    

GO
