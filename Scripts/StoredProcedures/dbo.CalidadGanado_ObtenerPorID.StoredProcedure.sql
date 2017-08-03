USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadGanado_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/28
-- Description: 
-- CalidadGanado_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[CalidadGanado_ObtenerPorID]
@CalidadGanadoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		CalidadGanadoID,
		Descripcion,
		Sexo,
		Activo
	FROM CalidadGanado
	WHERE CalidadGanadoID = @CalidadGanadoID
	SET NOCOUNT OFF;
END

GO
