USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarFechaZilmax]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarFechaZilmax]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarFechaZilmax]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 11/02/2015
-- Description:  Obtener listado de Corrales Cerrados para programacion de reimplante de ganado
-- Lote_ActualizarFechaZilmax
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ActualizarFechaZilmax] 
@LotesZilmaxXML XML
AS
BEGIN
	SET NOCOUNT ON;
		UPDATE L
		SET L.FechaEntradaZilmax = CASE WHEN x.FechaEntradaZilmax = CAST('19000101' AS DATE) THEN NULL ELSE x.FechaEntradaZilmax END
			, L.FechaSalidaZilmax = CASE WHEN x.FechaSalidaZilmax = CAST('19000101' AS DATE) THEN NULL ELSE x.FechaSalidaZilmax END
			, L.UsuarioModificacionID = x.UsuarioModificacionID
			, L.FechaModificacion = GETDATE()
		FROM Lote L
		INNER JOIN
		(
			SELECT T.N.value('./LoteID[1]', 'INT') AS LoteID
				,T.N.value('./UsuarioModificacionID[1]', 'INT') AS UsuarioModificacionID
				,T.N.value('./FechaEntradaZilmax[1]', 'DATE') AS FechaEntradaZilmax
				,T.N.value('./FechaSalidaZilmax[1]', 'DATE') AS FechaSalidaZilmax
			FROM @LotesZilmaxXML.nodes('/ROOT/Lotes') AS T(N)
		) x ON (L.LoteID = x.LoteID)
	SET NOCOUNT OFF;
END

GO
