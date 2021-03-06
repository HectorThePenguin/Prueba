USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarCabezas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarCabezas]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarCabezas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Lote_ActualizarCabezas]
@LoteID INT
, @Cabezas INT
, @UsuarioModificacionID INT
AS
/************************************
Autor	  : Gilberto Carranza
Fecha	  : 27/11/2013
Proposito : Actualizar un Lote
			, así como el numero de
			  cabezas
--001 Jorge Velazquez. Se modifica el SP para que no se este actualizando siempre las cabezas inicio
--002 Gilberto Carranza. Se valida si las cabezsas del lote son cero para 
************************************/
BEGIN

	SET NOCOUNT ON

	UPDATE Lote
	SET Cabezas = Cabezas + @Cabezas
		--, CabezasInicio = CabezasInicio + @Cabezas 001
		, UsuarioModificacionID = @UsuarioModificacionID
		, FechaModificacion = GETDATE()
	WHERE LoteID = @LoteID

	DECLARE @CabezasLote INT
	SET @CabezasLote = (SELECT Cabezas FROM Lote WHERE LoteID = @LoteID)

	--002
	IF (@CabezasLote > 0)
	BEGIN
		UPDATE Lote SET CabezasInicio = Cabezas --001
		WHERE LoteID = @LoteID 
			AND Cabezas > CabezasInicio
	END

	SET NOCOUNT OFF

END

GO
