USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_DesactivarLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_DesactivarLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[Lote_DesactivarLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gilberto Carranza
-- Create date: 05-12-2014
-- Description:	Actualiza las cabezas en lote
-- Lote_DesactivarLoteXML
-- =============================================
CREATE PROCEDURE [dbo].[Lote_DesactivarLoteXML]
@LotesXML	XML
AS
BEGIN	

	SET NOCOUNT ON

		UPDATE L
		SET L.Activo = 0
			, L.UsuarioModificacionID = x.UsuarioModificacionID
			, L.FechaModificacion = GETDATE()
		FROM Lote L
		INNER JOIN
		(
			SELECT T.N.value('./LoteID[1]','INT') AS LoteID
				,  T.N.value('./UsuarioModificacionID[1]','INT') AS UsuarioModificacionID
			FROM @LotesXML.nodes('/ROOT/Lotes') as T(N)
		) x ON (L.LoteID = x.LoteID)

	SET NOCOUNT OFF
END

GO
