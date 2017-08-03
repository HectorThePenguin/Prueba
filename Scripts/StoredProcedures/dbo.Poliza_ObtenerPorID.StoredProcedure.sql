USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Poliza_ObtenerPorID]
@PolizaID	INT
AS
BEGIN

	SET NOCOUNT ON

		SELECT *
		FROM Poliza
		WHERE PolizaID = @PolizaID

	SET NOCOUNT OFF

END
GO
