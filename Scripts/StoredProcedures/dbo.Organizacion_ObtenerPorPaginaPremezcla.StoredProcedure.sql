USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorPaginaPremezcla]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorPaginaPremezcla]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorPaginaPremezcla]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pedro Delgado
-- Create date: 10-07-2014
-- Description:	Otiene un listado de organizaciones paginado
-- Organizacion_ObtenerPorPaginaPremezcla 0, '', 1, 1,10 
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorPaginaPremezcla]
 @OrganizacionID INT,    
 @Descripcion NVARCHAR(50),     
 @Activo BIT,    
 @Inicio INT,     
 @Limite INT    
AS    
BEGIN    
 SET NOCOUNT ON;    
 SELECT DISTINCT
  O.OrganizacionID,    
  O.TipoOrganizacionID,     
  O.Descripcion,    
  O.Direccion,    
  O.Rfc,  
  O.IvaID,    
  O.Activo      
  INTO #OrganizacionTemporal
  FROM Organizacion O 
  INNER JOIN Premezcla P ON (O.OrganizacionID = P.OrganizacionID)
  WHERE (O.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')    
    AND @OrganizacionID IN (O.OrganizacionID, 0)    
    AND O.Activo = @Activo    
SELECT     
     ROW_NUMBER() OVER ( ORDER BY O.descripcion ASC) AS RowNum,    
  O.OrganizacionID,    
  O.TipoOrganizacionID,     
  O.Descripcion,    
  O.Direccion,    
  O.Rfc,  
  O.IvaID,    
  O.Activo      
  INTO #Organizacion 
  FROM #OrganizacionTemporal O 
 SELECT     
  o.OrganizacionID,     
  o.TipoOrganizacionID,    
  ot.Descripcion as [TipoOrganizacion],      
  o.Descripcion, 
  o.Direccion,      
  o.Rfc,  
  o.IvaID,    
  i.Descripcion as [Iva],    
  o.Activo    
 FROM    #Organizacion o      
 INNER JOIN TipoOrganizacion ot on ot.TipoOrganizacionID = o.TipoOrganizacionID    
 INNER JOIN Iva i on i.IvaID = o.IvaID    
 WHERE   RowNum BETWEEN @Inicio AND @Limite    
 SELECT     
  COUNT(OrganizacionID)AS TotalReg     
 FROM #Organizacion     
END

GO
