USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_ValidarLoteProgramado]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_ValidarLoteProgramado]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_ValidarLoteProgramado]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 06/05/2015
-- Description: SP para consultar validar si el Lote esta programado para Sacrificio
-- EXEC OrdenSacrificio_ValidarLoteProgramado 10
-- =============================================
CREATE PROCEDURE [dbo].[OrdenSacrificio_ValidarLoteProgramado]    
	@LoteID INT	
AS
BEGIN

SELECT
	LoteID
FROM OrdenSacrificio os
INNER JOIN OrdenSacrificioDetalle osd
	ON os.OrdenSacrificioID = osd.OrdenSacrificioID
WHERE osd.LoteID = @LoteID
AND CAST(os.FechaOrden AS DATE) >= CAST(GETDATE() AS DATE)
AND os.Activo = 1
AND osd.Activo = 1

END
GO
