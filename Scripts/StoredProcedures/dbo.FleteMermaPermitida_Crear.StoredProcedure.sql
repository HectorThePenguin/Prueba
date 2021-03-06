USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteMermaPermitida_Crear]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 09/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : FleteMermaPermitida_Crear
--======================================================
CREATE PROCEDURE [dbo].[FleteMermaPermitida_Crear]
@OrganizacionID int,
@SubFamiliaID int,
@MermaPermitida decimal(10,3),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT FleteMermaPermitida (
		OrganizacionID,
		SubFamiliaID,
		MermaPermitida,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@SubFamiliaID,
		@MermaPermitida,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
