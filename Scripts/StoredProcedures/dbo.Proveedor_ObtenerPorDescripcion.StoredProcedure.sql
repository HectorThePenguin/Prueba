USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Proveedor_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Proveedor_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Raúl Antonio Esquer Verduzco
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Proveedor_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Proveedor_ObtenerPorDescripcion]
@Descripcion varchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		P.ProveedorID,
		P.CodigoSAP,
		P.Descripcion,
		P.TipoProveedorID,
		TP.Descripcion TipoProveedor,
		P.Activo
	FROM Proveedor P 
	INNER JOIN TipoProveedor TP 
		ON P.TipoProveedorID = TP.TipoProveedorID
	WHERE P.Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
