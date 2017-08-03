USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorCodigoSAPEmbarque]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorCodigoSAPEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorCodigoSAPEmbarque]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jorge Luis Velazquez Araujo
-- Create date: 31/03/2016
-- Description:  Obtiene un proveedor por Embarque
-- Proveedor_ObtenerPorCodigoSAPEmbarque '0000300065', 0, 0, 1
-- =============================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorCodigoSAPEmbarque]
@CodigoSAP VARCHAR(10),
@EmbarqueID INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT P.ProveedorID
			,  P.Descripcion
			,  P.CodigoSAP
			,  TP.TipoProveedorID
			,  TP.Descripcion AS TipoProveedor
		FROM Proveedor P
		inner join TipoProveedor tp on p.TipoProveedorID = tp.TipoProveedorID
		INNER JOIN EmbarqueDetalle ed on p.ProveedorID = ed.ProveedorID
		WHERE P.CodigoSAP = @CodigoSAP
			AND ed.EmbarqueID = @EmbarqueID
			AND ed.Activo = 1
		GROUP BY P.ProveedorID
			  ,  P.Descripcion
			  ,  P.CodigoSAP
			  ,  TP.TipoProveedorID
			  ,  TP.Descripcion
	SET NOCOUNT OFF;
END

GO
