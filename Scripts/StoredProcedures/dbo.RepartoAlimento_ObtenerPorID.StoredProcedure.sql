USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimento_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimento_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 14/07/2014 12:00:00 a.m.
-- Description: 
-- SpName     : RepartoAlimento_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[RepartoAlimento_ObtenerPorID]
@RepartoAlimentoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		RepartoAlimentoID,
		TipoServicioID,
		CamionRepartoID,
		UsuarioIDReparto,
		HorometroInicial,
		HorometroFinal,
		OdometroInicial,
		OdometroFinal,
		LitrosDiesel,
		FechaReparto,
		Activo
	FROM RepartoAlimento
	WHERE RepartoAlimentoID = @RepartoAlimentoID
	SET NOCOUNT OFF;
END

GO
