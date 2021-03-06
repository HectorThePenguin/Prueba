USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorProductoBoleta_Crear]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorProductoBoleta_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 24/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorProductoBoleta_Crear
--======================================================
CREATE PROCEDURE [dbo].[IndicadorProductoBoleta_Crear]
@IndicadorProductoID int,
@OrganizacionID int,
@RangoMinimo decimal(10,3),
@RangoMaximo decimal(10,3),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT IndicadorProductoBoleta (
		IndicadorProductoID,
		OrganizacionID,
		RangoMinimo,
		RangoMaximo,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@IndicadorProductoID,
		@OrganizacionID,
		@RangoMinimo,
		@RangoMaximo,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
