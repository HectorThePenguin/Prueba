USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanadoCentros_ObtenerTipoGanadoIDPorEntradaGanado]    Script Date: 05/12/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanadoCentros_ObtenerTipoGanadoIDPorEntradaGanado]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanadoCentros_ObtenerTipoGanadoIDPorEntradaGanado]    Script Date: 05/12/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Hugo Castillo
-- Create date: 05/12/2015
-- Description:  Obtiene la descripcion y el tipoganado id 
-- TipoGanadoCentros_ObtenerTipoGanadoIDPorEntradaGanado 21428 
-- =============================================
CREATE PROCEDURE [dbo].[TipoGanadoCentros_ObtenerTipoGanadoIDPorEntradaGanado]
@EntradaGanadoID INT
AS
BEGIN
	SET NOCOUNT ON;	
	SELECT det.tipoganadoid AS TipoGanadoID, tg.Descripcion AS Descripcion
		FROM entradaganado eg
		INNER JOIN entradaganadocosteo egc ON eg.EntradaGanadoId = egc.EntradaGanadoId
		INNER JOIN entradadetalle det ON det.EntradaGanadoCosteoID = egc.EntradaGanadoCosteoID 
		INNER JOIN tipoganado tg ON tg.tipoganadoid = det.tipoganadoid
		WHERE eg.entradaganadoid = @EntradaGanadoID	
END
GO

