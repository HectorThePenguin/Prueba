USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoPromedio_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CostoPromedio_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CostoPromedio_Actualizar]
@CostoPromedioID int,
@OrganizacionID int,
@CostoID int,
@Importe decimal(19,2),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CostoPromedio SET
		OrganizacionID = @OrganizacionID,
		CostoID = @CostoID,
		Importe = @Importe,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CostoPromedioID = @CostoPromedioID
	SET NOCOUNT OFF;
END

GO
