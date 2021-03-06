USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoCosto_ObtenerPorFleteInternoDetalleID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInternoCosto_ObtenerPorFleteInternoDetalleID]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoCosto_ObtenerPorFleteInternoDetalleID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 22/07/2014
-- Description: Obtiene flete interno detalle por flete interno
-- SpName     : FleteInternoCosto_ObtenerPorFleteInternoDetalleID 3, 1
--======================================================
CREATE PROCEDURE [dbo].[FleteInternoCosto_ObtenerPorFleteInternoDetalleID]
@FleteInternoDetalleID INT,
@Activo BIT
AS
BEGIN
	SELECT 
		FIC.FleteInternoCostoID,
		FIC.FleteInternoDetalleID,
		FIC.CostoID,
		C.ClaveContable,
		C.Descripcion,
		C.TipoCostoID,
		FIC.Tarifa,
		FIC.Activo
	FROM FleteInternoCosto FIC (NOLOCK)
	INNER JOIN Costo C ON C.CostoID = FIC.CostoID
	WHERE FIC.FleteInternoDetalleID = @FleteInternoDetalleID
	AND FIC.Activo = @Activo
END

GO
