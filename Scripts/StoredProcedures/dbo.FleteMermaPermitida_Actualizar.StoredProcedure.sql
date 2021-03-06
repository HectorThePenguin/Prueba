USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteMermaPermitida_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[FleteMermaPermitida_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 09/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : FleteMermaPermitida_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[FleteMermaPermitida_Actualizar]
@FleteMermaPermitidaID int,
@OrganizacionID int,
@SubFamiliaID int,
@MermaPermitida decimal(10,3),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE FleteMermaPermitida SET
		OrganizacionID = @OrganizacionID,
		SubFamiliaID = @SubFamiliaID,
		MermaPermitida = @MermaPermitida,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE FleteMermaPermitidaID = @FleteMermaPermitidaID
	SET NOCOUNT OFF;
END

GO
