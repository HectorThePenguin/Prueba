USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteReimplante_ObtenerPorLoteXML]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerPorLoteXML]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Gilberto Carranza
-- Create date: 2014/05/22
-- Description: SP para obtener el lote reimplante
-- LoteReimplante_ObtenerPorLoteXML
-- =============================================
CREATE PROCEDURE [dbo].[LoteReimplante_ObtenerPorLoteXML]
@XmlLote XML,
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT
			LR.LoteReimplanteID,
			LR.LoteProyeccionID,
			LR.NumeroReimplante,
			LR.FechaProyectada,
			LR.PesoProyectado,
			LR.FechaReal,
			LR.PesoReal
		FROM LoteReimplante LR (NOLOCK)
		INNER JOIN LoteProyeccion LP (NOLOCK) 
			ON (LR.LoteProyeccionID = LP.LoteProyeccionID)
		INNER JOIN 
		(
			SELECT T.N.value('./LoteID[1]','INT') AS LoteID
			FROM @XmlLote.nodes('/ROOT/Lotes') as T(N)
		) A ON (LP.LoteID = A.LoteID
				AND LP.OrganizacionID = @OrganizacionID)
	SET NOCOUNT OFF;
END

GO
