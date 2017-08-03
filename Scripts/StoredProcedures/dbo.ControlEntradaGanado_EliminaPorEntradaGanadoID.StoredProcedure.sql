IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[ControlEntradaGanado_EliminaPorEntradaGanadoID]'))
  DROP PROCEDURE [dbo].[ControlEntradaGanado_EliminaPorEntradaGanadoID]

go
-- =============================================
-- Autor: Edgar Villarreal
-- Fecha: 2015-09-06
-- Origen: APInterfaces
-- Descripción:	Elimina registros de Control entrada ganado
-- EXEC ControlEntradaGanado_EliminaPorEntradaGanadoID 2
-- =============================================
CREATE PROCEDURE [dbo].[ControlEntradaGanado_EliminaPorEntradaGanadoID]
@EntradaGanadoID INT
AS
BEGIN
	
	DELETE AEGD FROM ControlEntradaGanadoDetalle AEGD 
	INNER JOIN ControlEntradaGanado CEG  ON CEG.ControlEntradaGanadoID=AEGD.ControlEntradaGanadoID
		WHERE CEG.EntradaGanadoID = @EntradaGanadoID;

	DELETE FROM ControlEntradaGanado  WHERE EntradaGanadoID = @EntradaGanadoID;
	
	
END



