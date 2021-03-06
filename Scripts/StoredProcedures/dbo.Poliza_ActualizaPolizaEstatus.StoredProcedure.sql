USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaPolizaEstatus]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_ActualizaPolizaEstatus]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_ActualizaPolizaEstatus]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 18/10/2014
-- Description:  Guarda el XML de la Poliza
-- =============================================
CREATE PROCEDURE [dbo].[Poliza_ActualizaPolizaEstatus]
@XmlPoliza XML
, @UsuarioModificacionID INT
AS
BEGIN

	SET NOCOUNT ON

		CREATE TABLE #tPolizas
		(
			PolizaID INT
		)
		INSERT INTO #tPolizas
		SELECT T.N.value('./PolizaID[1]','INT') AS PolizaID
		FROM @XmlPoliza.nodes('/MT_POLIZA_PERIFERICO/Datos') as T(N)

		UPDATE P
		SET P.Estatus = 0
			, P.FechaModificacion = GETDATE()
			, P.UsuarioModificacionID = @UsuarioModificacionID
		FROM Poliza P
		INNER JOIN #tPolizas A 
			ON (P.PolizaID = A.PolizaID)

	SET NOCOUNT OFF

END

GO
