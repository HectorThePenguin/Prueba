USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClaseCostoProducto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ClaseCostoProducto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 02/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ClaseCostoProducto_Crear
--======================================================
CREATE PROCEDURE [dbo].[ClaseCostoProducto_Crear]
@AlmacenID int,
@ProductoID int,
@CuentaSAPID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT ClaseCostoProducto (		
		AlmacenID,
		ProductoID,
		CuentaSAPID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@AlmacenID,
		@ProductoID,
		@CuentaSAPID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
