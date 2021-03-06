USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2013/11/04
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[Embarque_Actualizar]
	@EmbarqueID int,
	@OrganizacionID int,
	@FolioEmbarque int,
	@TipoEmbarqueID tinyint,
	@Estatus tinyint,
	@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE Embarque SET 
			OrganizacionID = @OrganizacionID,
			FolioEmbarque = @FolioEmbarque,
			TipoEmbarqueID = @TipoEmbarqueID,
			Estatus = @Estatus,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID			
		WHERE EmbarqueID = @EmbarqueID
END

GO
