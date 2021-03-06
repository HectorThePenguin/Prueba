USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteMermaPermitida_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 09/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : FleteMermaPermitida_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[FleteMermaPermitida_ObtenerTodos]
@Activo BIT = NULL
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
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
