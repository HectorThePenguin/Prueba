USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jos� Gilberto Quintero L�pez
-- Create date: 21-10-2013
-- Description:	Otiene un listado de Proveedores paginado
-- Proveedor_ObtenerPorPagina 0, '','', 1, 15 
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorPagina]
	@ProveedorID INT,
	@CodigoSAP VARCHAR(10),
	@Descripcion NVARCHAR(50),	
	@Activo BIT,
	@Inicio INT, 
	@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY p.Descripcion ASC) AS RowNum,
		p.ProveedorID,
		p.Descripcion,
		p.CodigoSAP,
		p.TipoProveedorID,
		p.ImporteComision,
		p.Activo		
		INTO #Proveedor
		FROM Proveedor p
		WHERE (p.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
		AND (p.CodigoSAP LIKE '%'+@CodigoSAP+'%' OR @CodigoSAP = '')
		  AND p.Activo = @Activo
		  AND @ProveedorID IN (p.ProveedorID, 0)
	SELECT 
		p.ProveedorID, 
		p.Descripcion,
		p.CodigoSAP,	
		p.TipoProveedorID,
		p.ImporteComision,
		tp.Descripcion as  [TipoProveedor],
		p.Activo
	FROM #Proveedor	p 
	INNER JOIN TipoProveedor tp on tp.TipoProveedorID = p.TipoProveedorID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(ProveedorID)AS TotalReg 
	FROM #Proveedor	
	SET NOCOUNT OFF;
END

GO
