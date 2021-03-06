USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadGanado_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 16/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CalidadGanado_ObtenerPorDescripcion '1 EUROPEO', 'H'
--======================================================
CREATE PROCEDURE [dbo].[CalidadGanado_ObtenerPorDescripcion]
@Descripcion varchar(50),
@Sexo char
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CalidadGanadoID,
		Descripcion,
		Sexo,
		Activo
	FROM CalidadGanado
	WHERE Descripcion = @Descripcion
		AND Sexo = @Sexo
	SET NOCOUNT OFF;
END

GO
