USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorPaginaTipoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorPaginaTipoAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorPaginaTipoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesus Alvarez
-- Create date: 17/07/2014
-- Description:	Obtiene un listado de almacen por tipos almacen
-- Almacen_ObtenerPorPaginaTipoAlmacen 0, 4, '', 1, 1, 15,'<ROOT>
--		<TiposAlmacen>
--			<TipoAlmacenID>1</TipoAlmacenID>
--		</TiposAlmacen>
--		<TiposAlmacen>
--			<TipoAlmacenID>5</TipoAlmacenID>
--		</TiposAlmacen>
--	</ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorPaginaTipoAlmacen]
 @AlmacenID INT,
 @OrganizacionID INT,
 @Descripcion NVARCHAR(50),     
 @Activo BIT,    
 @Inicio INT,     
 @Limite INT,
 @XmlTiposAlmacen XML
AS    
BEGIN    
 SET NOCOUNT ON;  
	DECLARE @TmpTiposAlmacen TABLE(TipoAlmacenID INT)
	INSERT INTO @TmpTiposAlmacen
	SELECT TipoAlmacenID  = T.item.value('./TipoAlmacenID[1]', 'INT')
	  FROM @XmlTiposAlmacen.nodes('ROOT/TiposAlmacen') AS T(item) 
 SELECT     
     ROW_NUMBER() OVER ( ORDER BY A.descripcion ASC) AS RowNum,
	A.AlmacenID,
	A.OrganizacionID,    
	A.CodigoAlmacen,
	A.Descripcion,
	A.TipoAlmacenID,
	A.CuentaInventario,
	A.CuentaInventarioTransito,
	A.CuentaDiferencias,
	A.Activo      
  INTO #Almacen   
  FROM Almacen A
  WHERE (A.Descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')    
    AND (@AlmacenID = 0 OR A.AlmacenID = @AlmacenID)    
    AND A.Activo = @Activo    
	AND A.TipoAlmacenID IN (SELECT TipoAlmacenID FROM @TmpTiposAlmacen)
	AND A.OrganizacionID = @OrganizacionID
 SELECT     
	A.AlmacenID,
	A.OrganizacionID,
	A.CodigoAlmacen,
	A.Descripcion,
	A.TipoAlmacenID,
	A.CuentaInventario,
	A.CuentaInventarioTransito,
	A.CuentaDiferencias,
	A.Activo  
 FROM  #Almacen a    
 INNER JOIN TipoAlmacen TA on TA.TipoAlmacenID = a.TipoAlmacenID
 AND TA.Activo = @Activo
 WHERE RowNum BETWEEN @Inicio AND @Limite    
 SELECT     
  COUNT(AlmacenID)AS TotalReg     
 FROM #Almacen     
END

GO
