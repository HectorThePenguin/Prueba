USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTipoGanadoSexoPeso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ObtenerTipoGanadoSexoPeso]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTipoGanadoSexoPeso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Cesar Valdez
-- Create date: 2013/12/11
-- Description: Sp para obtener el tipo de ganado en base al sexo y peso
-- Origen     : APInterfaces
-- EXEC  [dbo].[CorteGanado_ObtenerTipoGanadoSexoPeso] 'M',550
--=============================================
CREATE PROCEDURE [dbo].[CorteGanado_ObtenerTipoGanadoSexoPeso]
	@Sexo CHAR(1),
	@Peso INT
AS
BEGIN	
SET NOCOUNT ON;
	SELECT
		TipoGanadoID,
		Descripcion,
		Sexo,
		PesoMinimo,
		PesoMaximo,
		Activo
	FROM TipoGanado
	WHERE sexo = @Sexo
	AND @Peso BETWEEN PesoMinimo AND PesoMaximo
SET NOCOUNT OFF;	
END

GO
