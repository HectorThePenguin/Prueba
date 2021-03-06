USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarFechaSalidaEnLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarFechaSalidaEnLote]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarFechaSalidaEnLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: cesar.valdez
-- Fecha: 2013-12-29
-- Descripci�n:	Actualizar la fecha salida en lote
-- EXEC Lote_ActualizarFechaSalidaEnLote 1
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ActualizarFechaSalidaEnLote]
	@LoteID INT
AS
BEGIN
	SET NOCOUNT ON
	UPDATE Lote
	SET FechaSalida = GETDATE(),
		FechaModificacion = GETDATE()
	WHERE LoteID = @LoteID
	SET NOCOUNT OFF
END

GO
