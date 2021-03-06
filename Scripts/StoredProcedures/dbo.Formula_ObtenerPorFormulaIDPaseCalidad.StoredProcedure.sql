USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorFormulaIDPaseCalidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Formula_ObtenerPorFormulaIDPaseCalidad]
GO
/****** Object:  StoredProcedure [dbo].[Formula_ObtenerPorFormulaIDPaseCalidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/06/16
-- Description: Procedimiento almacenado para obtener folios de pase a proceso
-- Formula_ObtenerPorFormulaIDPaseCalidad 2
--=============================================
CREATE PROCEDURE [dbo].[Formula_ObtenerPorFormulaIDPaseCalidad]
@FormulaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT F.FormulaID
		,  F.Descripcion	AS Formula
		,  sb.Descripcion	AS SubFamilia
		,  Pro.ProductoID
	FROM Formula F
	INNER JOIN Producto Pro
		ON (F.ProductoID = Pro.ProductoID
			AND F.FormulaID = @FormulaID)
	INNER JOIN SubFamilia SB
		ON (Pro.SubFamiliaID = sb.SubFamiliaID)
	SET NOCOUNT OFF;
END

GO
