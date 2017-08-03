USE [SIAP]
GO

DROP PROCEDURE [dbo].[Organizacion_ObtenerPorPaginaAdminstranIngredientes]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  Daniel Benitez
-- Create date: 01-03-2017
-- Description: Otiene un listado de organizaciones paginado omitiendo las organizaciones de tipo compra directa
-- [Organizacion_ObtenerPorPaginaAdminstranIngredientes] 0, '', 1, 1,10     
-- =============================================    
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorPaginaAdminstranIngredientes]    
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
    AND O.TipoOrganizacionID != 3      
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
