USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarActivoEnLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarActivoEnLote]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarActivoEnLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: cesar.valdez
-- Fecha: 2013-12-29
-- Descripci�n:	Actualizar el numero de cabezas en lote
-- EXEC Lote_ActualizarActivoEnLote 1,1
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ActualizarActivoEnLote]
	@Activo BIT,
	@LoteID INT
AS
BEGIN
	SET NOCOUNT ON
	UPDATE Lote
	SET Activo = @Activo,
		FechaModificacion = GETDATE()
	WHERE LoteID = @LoteID
	SET NOCOUNT OFF
END

GO
