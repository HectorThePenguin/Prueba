USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorPaginaTiposOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorPaginaTiposOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorPaginaTiposOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roque Enrique
-- Create date: 20-06-2014
-- Description:	Otiene un listado de organizaciones paginado filtrado por tipos de organaizcion
-- Organizacion_ObtenerPorPaginaTiposOrganizacion 0, '', 1, 1,10 ,'<ROOT>
--		<TiposOrganizaciones>
--			<TipoOrganizacionID>1</TipoOrganizacionID>
--		</TiposOrganizaciones>
--		<TiposOrganizaciones>
--			<TipoOrganizacionID>2</TipoOrganizacionID>
--		</TiposOrganizaciones>
--		<TiposOrganizaciones>
--			<TipoOrganizacionID>3</TipoOrganizacionID>
--		</TiposOrganizaciones>
--	</ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorPaginaTiposOrganizacion]
 @OrganizacionID INT,    
 @Descripcion NVARCHAR(50),     
 @Activo BIT,    
 @Inicio INT,     
 @Limite INT,
 @XmlTiposOrganizacion XML
AS    
BEGIN    
 SET NOCOUNT ON;  
DECLARE @TmpTiposOrganizacion TABLE(TipoOrganizacionID INT)
	INSERT INTO @TmpTiposOrganizacion
	SELECT TipoOrganizacionID  = T.item.value('./TipoOrganizacionID[1]', 'INT')
	  FROM @XmlTiposOrganizacion.nodes('ROOT/TiposOrganizaciones') AS T(item) 
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
  FROM Organizacion O    
  WHERE (O.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')    
    AND (@OrganizacionID = 0 OR O.OrganizacionID = @OrganizacionID)    
    AND O.Activo = @Activo    
	AND O.TipoOrganizacionID IN (SELECT TipoOrganizacionID FROM @TmpTiposOrganizacion)
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
