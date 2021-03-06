USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorMaterialSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_ObtenerPorMaterialSAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_ObtenerPorMaterialSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 21/07/2015
-- Description: regresa un producto por el MaterialSAP
-- SpName     : Producto_ObtenerPorMaterialSAP
--======================================================
CREATE PROCEDURE [dbo].[Producto_ObtenerPorMaterialSAP]
@MaterialSAP varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ProductoID,
		Descripcion,
		SubFamiliaID,
		UnidadID,
		MaterialSAP,
		Activo
	FROM Producto
	WHERE MaterialSAP = @MaterialSAP
	SET NOCOUNT OFF;
END

GO
