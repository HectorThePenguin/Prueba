USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Producto_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Producto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Producto_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : sp_helptext Producto_Crear
--======================================================
CREATE PROCEDURE [dbo].[Producto_Crear]
@ProductoID INT,
@Descripcion varchar(50),
@SubFamiliaID int,
@UnidadID int,
@ManejaLote bit,
@MaterialSAP varchar(18),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT Producto (
		ProductoID,
		Descripcion,
		SubFamiliaID,
		UnidadID,
		ManejaLote,
		MaterialSAP,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@ProductoID,
		@Descripcion,
		@SubFamiliaID,
		@UnidadID,
		@ManejaLote,
		@MaterialSAP,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SET NOCOUNT OFF;
END

GO
