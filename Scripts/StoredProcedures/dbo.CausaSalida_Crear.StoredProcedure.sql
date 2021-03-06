USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaSalida_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CausaSalida_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CausaSalida_Crear
--======================================================
CREATE PROCEDURE [dbo].[CausaSalida_Crear]
@Descripcion varchar(50),
@TipoMovimientoID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CausaSalida (
		Descripcion,
		TipoMovimientoID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@TipoMovimientoID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
