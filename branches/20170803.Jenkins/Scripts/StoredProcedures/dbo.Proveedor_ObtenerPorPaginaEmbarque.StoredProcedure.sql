USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPaginaEmbarque]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorPaginaEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorPaginaEmbarque]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 31/03/2016
-- Description:  Obtiene una lista de proveedores por el Embarque
-- Proveedor_ObtenerPorPaginaEmbarque 'GASTELUM', 0,1,1,15
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorPaginaEmbarque]
@Descripcion VARCHAR(50)
,@EmbarqueID INT
,@Activo INT
,@Inicio INT
, @Limite INT
AS
BEGIN
	SET NOCOUNT ON;
	
	-- select @Descripcion
		SELECT ROW_NUMBER() OVER (ORDER BY P.Descripcion ASC) AS RowNum
			,  P.ProveedorID
			,  P.Descripcion
			,  P.CodigoSAP
			,  TP.TipoProveedorID
			,  TP.Descripcion AS TipoProveedor
		INTO #tProveedor
		FROM Proveedor P
		INNER JOIN TipoProveedor TP
			ON (P.TipoProveedorID = TP.TipoProveedorID)
		INNER JOIN EmbarqueDetalle ed on ed.ProveedorID = p.ProveedorID
		WHERE (@Descripcion = '' OR P.Descripcion LIKE '%' + @Descripcion + '%')
			AND P.Activo = @Activo
			AND @EmbarqueID in (0,ed.EmbarqueID)
		GROUP BY P.ProveedorID
			  ,  P.Descripcion
			  ,  P.CodigoSAP
			  ,  TP.TipoProveedorID
			  ,  TP.Descripcion
		SELECT ProveedorID
			,  Descripcion
			,  CodigoSAP
			,  TipoProveedorID
			,  TipoProveedor
		FROM #tProveedor
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT COUNT(ProveedorID) AS TotalReg
		FROM #tProveedor
		DROP TABLE #tProveedor
	SET NOCOUNT OFF;
END

GO
