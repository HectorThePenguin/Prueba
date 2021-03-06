USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedoresConContratoPorCodigoSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerProveedoresConContratoPorCodigoSAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerProveedoresConContratoPorCodigoSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 17/12/2014
-- Description:  Obtiene un proveedor por producto
-- Proveedor_ObtenerProveedoresConContratoPorCodigoSAP '0000600010', 1
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerProveedoresConContratoPorCodigoSAP]
@CodigoSAP VARCHAR(20)
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
		DECLARE @EstatusCancelado INT
		DECLARE @MaizAmarillo INT, @MaizBlanco INT
		SET @EstatusCancelado = 47
		SET @MaizAmarillo = 101
		SET @MaizBlanco = 100
		SELECT P.ProveedorID
			,P.Descripcion
			,P.CodigoSAP
			,TP.TipoProveedorID
			,TP.Descripcion AS TipoProveedor
		FROM Proveedor P
		INNER JOIN TipoProveedor TP 
			ON (P.TipoProveedorID = TP.TipoProveedorID)
		INNER JOIN Contrato C 
			ON (P.ProveedorID = C.ProveedorID	
				AND C.ProductoID IN (@MaizAmarillo, @MaizBlanco))
		WHERE P.CodigoSAP = @CodigoSAP
			AND C.OrganizacionID = @OrganizacionID
		GROUP BY P.ProveedorID
			,P.Descripcion
			,P.CodigoSAP
			,TP.TipoProveedorID
			,TP.Descripcion
	SET NOCOUNT OFF;
END

GO
