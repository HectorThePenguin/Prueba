USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[IndicadorObjetivo_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[IndicadorObjetivo_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 13/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : IndicadorObjetivo_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[IndicadorObjetivo_Actualizar]
@IndicadorObjetivoID int,
@IndicadorProductoCalidadID int,
@TipoObjetivoCalidadID int,
@OrganizacionID int,
@ObjetivoMinimo decimal(10,2),
@ObjetivoMaximo decimal(10,2),
@Tolerancia decimal(10,2),
@Medicion varchar(10),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE IndicadorObjetivo SET
		IndicadorProductoCalidadID = @IndicadorProductoCalidadID,
		TipoObjetivoCalidadID = @TipoObjetivoCalidadID,
		OrganizacionID = @OrganizacionID,
		ObjetivoMinimo = @ObjetivoMinimo,
		ObjetivoMaximo = @ObjetivoMaximo,
		Tolerancia = @Tolerancia,
		Medicion = @Medicion,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE IndicadorObjetivoID = @IndicadorObjetivoID
	SET NOCOUNT OFF;
END

GO
