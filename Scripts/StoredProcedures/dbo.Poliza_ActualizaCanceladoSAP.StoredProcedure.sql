USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaCanceladoSAP]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ActualizaCanceladoSAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaCanceladoSAP]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 05-12-2014
-- Description:	Actualiza las cabezas en lote
-- Poliza_ActualizaCanceladoSAP
-- =============================================
CREATE PROCEDURE [dbo].[Poliza_ActualizaCanceladoSAP]
@XmlPoliza	XML
AS 
BEGIN	

	SET NOCOUNT ON

		CREATE TABLE #tPolizas
		(
			PolizaID	INT
		)

		INSERT INTO #tPolizas
		SELECT T.N.value('./PolizaID[1]','INT') AS PolizaID
		FROM @XmlPoliza.nodes('/ROOT/PolizasID') as T(N)

		UPDATE P
		SET P.Cancelada = 1
		FROM Poliza P
		INNER JOIN #tPolizas tP
			ON (P.PolizaID = tP.PolizaID)

	SET NOCOUNT OFF
END

GO
