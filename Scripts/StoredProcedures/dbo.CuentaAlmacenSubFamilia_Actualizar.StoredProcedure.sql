USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaAlmacenSubFamilia_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CuentaAlmacenSubFamilia_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CuentaAlmacenSubFamilia_Actualizar]
@CuentaAlmacenSubFamiliaID int,
@AlmacenID int,
@SubFamiliaID int,
@CuentaSAPID int,
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CuentaAlmacenSubFamilia SET
		AlmacenID = @AlmacenID,
		SubFamiliaID = @SubFamiliaID,
		CuentaSAPID = @CuentaSAPID,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CuentaAlmacenSubFamiliaID = @CuentaAlmacenSubFamiliaID
	SET NOCOUNT OFF;
END

GO
