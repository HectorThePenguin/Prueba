USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteReimplante_ObtenerPorLote]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_ObtenerPorLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/25
-- Description: SP para obtener el lote reimplante
-- Origen     : APInterfaces
-- EXEC LoteReimplante_ObtenerPorLote 1437, 4
-- =============================================
CREATE PROCEDURE [dbo].[LoteReimplante_ObtenerPorLote]
@LoteID INT,
@OrganizacionID INT
AS
BEGIN
	SELECT
		LR.LoteReimplanteID,
		LR.LoteProyeccionID,
		LR.NumeroReimplante,
		LR.FechaProyectada,
		LR.PesoProyectado,
		LR.FechaReal,
		LR.PesoReal
	FROM LoteReimplante LR (NOLOCK)
   INNER JOIN LoteProyeccion LP (NOLOCK) ON LR.LoteProyeccionID=LP.LoteProyeccionID
   WHERE LP.LoteID = @LoteID
	AND LP.OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
