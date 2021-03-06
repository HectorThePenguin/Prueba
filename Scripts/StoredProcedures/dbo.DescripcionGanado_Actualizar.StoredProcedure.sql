USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DescripcionGanado_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DescripcionGanado_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[DescripcionGanado_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : DescripcionGanado_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[DescripcionGanado_Actualizar]
@DescripcionGanadoID int,
@Descripcion varchar(255),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE DescripcionGanado SET
		Descripcion = @Descripcion,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE DescripcionGanadoID = @DescripcionGanadoID
	SET NOCOUNT OFF;
END

GO
