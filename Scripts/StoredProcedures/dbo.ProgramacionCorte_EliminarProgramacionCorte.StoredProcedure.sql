USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionCorte_EliminarProgramacionCorte]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionCorte_EliminarProgramacionCorte]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionCorte_EliminarProgramacionCorte]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Fecha: 20-12-2013
-- Descripci�n:	Eliminar Partida
-- EXEC ProgramacionCorte_EliminarProgramacionCorte 24, 2
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionCorte_EliminarProgramacionCorte]
	@NoPartida XML, 
	@organizacionID INT
AS
BEGIN
	DECLARE @PartidasCorte AS TABLE ([NoPartida] INT)
	INSERT @PartidasCorte ([NoPartida])
	SELECT [NoPartida] = t.item.value('./NoPartida[1]', 'INT')
	FROM @NoPartida.nodes('ROOT/PartidasCorte') AS T(item)
	UPDATE ProgramacionCorte
	   SET Activo = 0,
		   FechaModificacion = GETDATE(),
		   FechaFinCorte = GETDATE()
	 WHERE OrganizacionID = @organizacionID
	   AND FolioEntradaID IN(SELECT NoPartida FROM @PartidasCorte)
	   AND Activo = 1
END

GO
