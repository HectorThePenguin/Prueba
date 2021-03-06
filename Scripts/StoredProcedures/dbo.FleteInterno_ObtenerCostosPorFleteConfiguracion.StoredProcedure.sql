USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ObtenerCostosPorFleteConfiguracion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInterno_ObtenerCostosPorFleteConfiguracion]
GO
/****** Object:  StoredProcedure [dbo].[FleteInterno_ObtenerCostosPorFleteConfiguracion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 31/08/2014
-- Description: 
-- SpName     : FleteInterno_ObtenerCostosPorFleteConfiguracion 5640, 1, 5, 100, 1
--001 Jorge Luis Velazquez *Se agrega columna para regresar el Tipo de Tarifa
--======================================================
CREATE PROCEDURE [dbo].[FleteInterno_ObtenerCostosPorFleteConfiguracion]
@ProveedorID INT,
@OrganizacionID INT,
@AlmacenIDOrigen INT,
@ProductoID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		FIC.FleteInternoCostoID,
		FIC.FleteInternoDetalleID,
		FIC.CostoID,
		FIC.Tarifa,
		FIC.Activo,
		FIC.FechaCreacion,
		FIC.UsuarioCreacionID,
		FID.TipoTarifaID --001
	FROM FleteInterno FT
	INNER JOIN FleteInternoDetalle FID ON  FID.FleteInternoID = FT.FleteInternoID
	INNER JOIN FleteInternoCosto FIC ON FIC.FleteInternoDetalleID = FID.FleteInternoDetalleID
	WHERE FT.AlmacenIDOrigen = @AlmacenIDOrigen
		AND FT.OrganizacionID = @OrganizacionID
		AND FT.ProductoID = @ProductoID
		AND FID.ProveedorID = @ProveedorID
		AND FT.Activo = @Activo
		AND FID.Activo = @Activo
		AND FIC.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
