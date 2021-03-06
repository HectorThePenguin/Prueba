USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaAlmacenSubFamilia_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CuentaAlmacenSubFamilia_Crear
--======================================================
CREATE PROCEDURE [dbo].[CuentaAlmacenSubFamilia_Crear]
@AlmacenID int,
@SubFamiliaID int,
@CuentaSAPID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CuentaAlmacenSubFamilia (
		AlmacenID,
		SubFamiliaID,
		CuentaSAPID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@AlmacenID,
		@SubFamiliaID,
		@CuentaSAPID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
