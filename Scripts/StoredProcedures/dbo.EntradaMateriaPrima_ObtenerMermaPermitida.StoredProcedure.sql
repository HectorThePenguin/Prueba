USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_ObtenerMermaPermitida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaMateriaPrima_ObtenerMermaPermitida]
GO
/****** Object:  StoredProcedure [dbo].[EntradaMateriaPrima_ObtenerMermaPermitida]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 26/11/2014
-- Description: Obtiene la merma permitida para el flete
-- SpName     : EXEC EntradaMateriaPrima_ObtenerMermaPermitida 1,1,1
--======================================================
CREATE PROCEDURE [dbo].[EntradaMateriaPrima_ObtenerMermaPermitida]
@FolioEntradaProducto INT,
@OrganizacionID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	F.ContratoID,
			F.Observaciones,
			F.MermaPermitida,
			F.ProveedorID,
			F.FleteID AS FleteId
	FROM EntradaProducto EP
	INNER JOIN RegistroVigilancia RV ON EP.RegistroVigilanciaID = RV.RegistroVigilanciaID
	INNER JOIN ProveedorChofer PCH ON RV.ProveedorChoferID = PCH.ProveedorChoferID
	INNER JOIN Flete F ON PCH.ProveedorID = F.ProveedorID
	WHERE EP.Folio = @FolioEntradaProducto
	AND EP.OrganizacionID = @OrganizacionID
	AND EP.Activo = @Activo
	AND RV.OrganizacionID = @OrganizacionID
	AND RV.Activo = @Activo
	AND F.OrganizacionID = @OrganizacionID
	AND F.Activo = @Activo
	AND PCH.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
