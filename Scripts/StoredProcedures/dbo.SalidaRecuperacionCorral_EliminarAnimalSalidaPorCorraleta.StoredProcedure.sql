USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacionCorral_EliminarAnimalSalidaPorCorraleta]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaRecuperacionCorral_EliminarAnimalSalidaPorCorraleta]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacionCorral_EliminarAnimalSalidaPorCorraleta]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: edgar.villarreal
-- Fecha: 2013-12-19
-- Origen: APInterfaces
-- Descripci�n:	Elimina registros de aniamalSalida por el loteID 
-- EXEC SalidaRecuperacionCorral_EliminarAnimalSalidaPorCorraleta 1
-- =============================================
CREATE PROCEDURE [dbo].[SalidaRecuperacionCorral_EliminarAnimalSalidaPorCorraleta]
@Corraleta INT,
@LoteID INT
AS
BEGIN
	DELETE FROM AnimalSalida 
	WHERE CorraletaID = @Corraleta 
	AND LoteID = @LoteID;
END

GO
