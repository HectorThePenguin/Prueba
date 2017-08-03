USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoCalidad_Crear]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoCalidad_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoCalidad_Crear
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoCalidad_Crear]
@IndicadorID int,
@ProductoID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT IndicadorProductoCalidad (
		IndicadorID,
		ProductoID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@IndicadorID,
		@ProductoID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
