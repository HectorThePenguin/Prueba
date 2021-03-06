USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorSexo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoGanado_ObtenerPorSexo]
GO
/****** Object:  StoredProcedure [dbo].[TipoGanado_ObtenerPorSexo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TipoGanado_ObtenerPorSexo]
	@Sexo CHAR(1)
AS
BEGIN
SET NOCOUNT ON;	
	SELECT TG.TipoGanadoID AS TipoGanadoID,TC.RangoInicial AS PesoMinimo 
	FROM TipoGanado TG
	INNER JOIN TipoGanadoCorrales TC ON TG.TipoGanadoID = TC.TipoGanadoID
	WHERE TG.sexo = @Sexo 
SET NOCOUNT OFF;	
END

GO
