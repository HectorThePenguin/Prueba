USE [SIAP]
GO

DROP PROCEDURE [dbo].[Proveedor_ObtenerFleteroPorPagina]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Daniel Benitez
-- Create date: 22-12-2016
-- Description:	Otiene un listado de Proveedores registrados como cliente paginados
-- Proveedor_ObtenerFleteroPorPagina 0, '','', 1, 1, 15 
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerFleteroPorPagina]
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
		INNER JOIN Cliente c ON LTRIM(RTRIM(p.Descripcion)) = LTRIM(RTRIM(c.Descripcion))
		WHERE (p.descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
		AND (p.CodigoSAP LIKE '%'+@CodigoSAP+'%' OR @CodigoSAP = '')
		  AND p.Activo = @Activo AND c.Activo = @Activo
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
