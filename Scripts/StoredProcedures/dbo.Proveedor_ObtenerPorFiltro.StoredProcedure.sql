USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorFiltro]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorFiltro]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorFiltro]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque Solis
-- Create date: 2014/12/10
-- Description: 
-- EXEC Proveedor_ObtenerPorFiltro 0,'', 'MIRANDA','<ROOT><Datos><tipoProveedor>1</tipoProveedor></Datos><Datos><tipoProveedor>5</tipoProveedor></Datos></ROOT>',1,1,15
--=============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorFiltro]
@ProveedorID INT,
@CodigoSAP VARCHAR(10),
@Descripcion VARCHAR(50),
@xmlTipoCuenta XML,
@Activo BIT,
@Inicio INT, 
@Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #tTipoProveedor
	(
		TipoProveedorID INT
	)
	INSERT INTO #tTipoProveedor
	SELECT DISTINCT T.N.value('./tipoProveedor[1]','INT') AS TipoProveedor
	FROM @xmlTipoCuenta.nodes('/ROOT/Datos') as T(N)
	SELECT 
	    ROW_NUMBER() OVER ( ORDER BY P.Descripcion ASC) AS RowNum,
		P.ProveedorID,
		P.CodigoSAP,
		P.Descripcion,
		P.TipoProveedorID,
		P.Activo 
	INTO #tmpProveedor
	FROM Proveedor P
	INNER JOIN #tTipoProveedor TTP ON (P.TipoProveedorID = TTP.TipoProveedorID)
	WHERE (P.Descripcion LIKE '%'+@Descripcion+'%' OR @Descripcion = '')
	AND (@ProveedorID = P.ProveedorID OR @ProveedorID = 0)
	AND (P.CodigoSAP = @CodigoSAP OR @CodigoSAP = '')
	AND P.Activo = @Activo OR @Activo IS NULL
	SELECT 
		p.ProveedorID, 
		p.Descripcion,
		p.CodigoSAP,	
		p.TipoProveedorID,
		tp.Descripcion as  [TipoProveedor],
		p.Activo
	FROM #tmpProveedor	p 
	INNER JOIN TipoProveedor tp on tp.TipoProveedorID = p.TipoProveedorID
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT 
		COUNT(ProveedorID)AS TotalReg 
	FROM #tmpProveedor	
	SET NOCOUNT OFF;
END

GO
