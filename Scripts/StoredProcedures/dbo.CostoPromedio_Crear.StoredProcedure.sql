USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoPromedio_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CostoPromedio_Crear
--======================================================
CREATE PROCEDURE [dbo].[CostoPromedio_Crear]
@OrganizacionID int,
@CostoID int,
@Importe decimal(19,2),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CostoPromedio (
		OrganizacionID,
		CostoID,
		Importe,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@CostoID,
		@Importe,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
