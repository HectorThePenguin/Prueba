USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Moneda_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Moneda_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Moneda_Crear
--======================================================
CREATE PROCEDURE [dbo].[Moneda_Crear]
@Descripcion varchar(50),
@Abreviatura varchar(10),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Moneda (
		Descripcion,
		Abreviatura,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@Descripcion,
		@Abreviatura,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
