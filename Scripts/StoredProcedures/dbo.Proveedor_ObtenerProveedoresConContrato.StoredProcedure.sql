USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedoresConContrato]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerProveedoresConContrato]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedoresConContrato]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 17/12/2014
-- Description:  Obtiene un proveedor con contrato
-- Proveedor_ObtenerProveedoresConContrato '', 1, 1, 15
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerProveedoresConContrato]
@Descripcion VARCHAR(50)
, @OrganizacionID INT
, @Inicio INT
, @Limite INT
AS
BEGIN
	SET NOCOUNT ON;
		DECLARE @EstatusCancelado INT
		DECLARE @MaizAmarillo INT, @MaizBlanco INT
		SET @EstatusCancelado = 47
		SET @MaizAmarillo = 101
		SET @MaizBlanco = 100
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
		INNER JOIN Contrato C
			ON (P.ProveedorID = C.ProveedorID
				AND C.ProductoID IN (@MaizAmarillo, @MaizBlanco))
		WHERE (@Descripcion = '' OR P.Descripcion LIKE '%' + @Descripcion + '%')
			AND C.OrganizacionID = @OrganizacionID
			AND C.EstatusID <> @EstatusCancelado
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
