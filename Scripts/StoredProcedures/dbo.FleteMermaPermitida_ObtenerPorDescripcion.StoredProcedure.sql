USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteMermaPermitida_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 09/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : FleteMermaPermitida_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[FleteMermaPermitida_ObtenerPorDescripcion]
@OrganizacionID INT,
@SubFamiliaID	INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		FMP.FleteMermaPermitidaID,
		FMP.OrganizacionID,
		FMP.SubFamiliaID,
		FMP.MermaPermitida,
		FMP.Activo
		, O.Descripcion			AS Organizacion
		, SF.Descripcion		AS SubFamilia
	FROM FleteMermaPermitida FMP
	INNER JOIN Organizacion O
		ON (FMP.OrganizacionID = O.OrganizacionID)
	INNER JOIN SubFamilia SF
		ON (FMP.SubFamiliaID = SF.SubFamiliaID)
	WHERE FMP.OrganizacionID = @OrganizacionID
		AND FMP.SubFamiliaID = @SubFamiliaID
	SET NOCOUNT OFF;
END

GO
