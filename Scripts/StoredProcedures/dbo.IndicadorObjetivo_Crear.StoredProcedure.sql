USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorObjetivo_Crear]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorObjetivo_Crear
--======================================================
CREATE PROCEDURE [dbo].[IndicadorObjetivo_Crear]
@IndicadorProductoCalidadID int,
@TipoObjetivoCalidadID int,
@OrganizacionID int,
@ObjetivoMinimo decimal(10,2),
@ObjetivoMaximo decimal(10,2),
@Tolerancia decimal(10,2),
@Medicion varchar(10),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT IndicadorObjetivo (
		IndicadorProductoCalidadID,
		TipoObjetivoCalidadID,
		OrganizacionID,
		ObjetivoMinimo,
		ObjetivoMaximo,
		Tolerancia,
		Medicion,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@IndicadorProductoCalidadID,
		@TipoObjetivoCalidadID,
		@OrganizacionID,
		@ObjetivoMinimo,
		@ObjetivoMaximo,
		@Tolerancia,
		@Medicion,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
