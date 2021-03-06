USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionCorte_ActualizarFechaInicio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionCorte_ActualizarFechaInicio]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionCorte_ActualizarFechaInicio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Fecha: 17-06-2014
-- Descripci�n:	Actualiza fecha inicio de la partida con la primer cabeza cortada
-- EXEC ProgramacionCorte_ActualizarFechaInicio 24, 2
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionCorte_ActualizarFechaInicio]
	@NoPartida XML, 
	@OrganizacionID INT,
	@UsuarioModificacionID INT
AS
BEGIN
	DECLARE @PartidasCorte AS TABLE ([NoPartida] INT)
	INSERT @PartidasCorte ([NoPartida])
	SELECT [NoPartida] = t.item.value('./NoPartida[1]', 'INT')
	FROM @NoPartida.nodes('ROOT/PartidasCorte') AS T(item)
	UPDATE ProgramacionCorte
	   SET FechaInicioCorte = GETDATE(),
		   FechaModificacion = GETDATE(),
		   UsuarioModificacionID = @UsuarioModificacionID
	 WHERE OrganizacionID = @organizacionID
	   AND FolioEntradaID IN (SELECT NoPartida FROM @PartidasCorte)
END

GO
