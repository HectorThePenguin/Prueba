USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Moneda_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Moneda_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[Moneda_Actualizar]
@MonedaID int,
@Descripcion varchar(50),
@Abreviatura varchar(10),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Moneda SET
		Descripcion = @Descripcion,
		Abreviatura = @Abreviatura,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE MonedaID = @MonedaID
	SET NOCOUNT OFF;
END

GO
