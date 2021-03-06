USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteMermaPermitida_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 09/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : FleteMermaPermitida_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[FleteMermaPermitida_ObtenerPorID]
@FleteMermaPermitidaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		FleteMermaPermitidaID,
		OrganizacionID,
		SubFamiliaID,
		MermaPermitida,
		Activo
	FROM FleteMermaPermitida
	WHERE FleteMermaPermitidaID = @FleteMermaPermitidaID
	SET NOCOUNT OFF;
END

GO
