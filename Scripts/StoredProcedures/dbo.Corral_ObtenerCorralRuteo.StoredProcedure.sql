USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralRuteo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerCorralRuteo]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralRuteo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 14/11/2013
-- Description:  Obtiene si el Corral ha sido usado
--				 anteriormente en una entrada con ruteo.
-- Corral_ObtenerCorralRuteo 2,1
-- =============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerCorralRuteo]
@EmbarqueID INT
,@CorralID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT COUNT(EG.CorralID) AS CorralRuteado
		FROM EntradaGanado EG
		INNER JOIN Corral C
			ON (EG.CorralID = C.CorralID)
		WHERE EG.EmbarqueID = @EmbarqueID
			AND EG.CorralID = @CorralID
			AND EsRuteo = 1
	SET NOCOUNT OFF
END

GO
