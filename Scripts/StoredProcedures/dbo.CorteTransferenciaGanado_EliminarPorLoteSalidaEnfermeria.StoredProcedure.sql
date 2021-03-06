USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_EliminarPorLoteSalidaEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteTransferenciaGanado_EliminarPorLoteSalidaEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_EliminarPorLoteSalidaEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: edgar.villarreal
-- Fecha: 2013-12-19
-- Origen: APInterfaces
-- Descripci�n:	Elimina registros de aniamalSalida por el loteID 
-- EXEC CorteTransferenciaGanado_EliminarPorLoteSalidaEnfermeria 15,64,6
-- =============================================
CREATE PROCEDURE [dbo].[CorteTransferenciaGanado_EliminarPorLoteSalidaEnfermeria]
@LoteID INT
AS
BEGIN
	DELETE FROM AnimalSalida WHERE LoteID = @LoteID ;
END

GO
