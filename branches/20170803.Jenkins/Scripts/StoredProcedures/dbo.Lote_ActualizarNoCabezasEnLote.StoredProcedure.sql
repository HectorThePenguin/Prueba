USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarNoCabezasEnLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarNoCabezasEnLote]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarNoCabezasEnLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: cesar.valdez
-- Fecha: 2013-12-29
-- Descripción:	Actualizar el numero de cabezas en lote
-- EXEC Lote_ActualizarNoCabezasEnLote 20,20,1,1,2,0
-- 001 Jorge Velazquez. Se modifica el SP para que no se este actualizando siempre las cabezas inicio
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ActualizarNoCabezasEnLote]
	@Cabezas INT,
	@CabezasInicio INT,
	@UsuarioModificacionID INT,
	@LoteIDDestino INT,
	@LoteIDOrigen INT,
	@CabezasOrigen INT
						
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @TipoCorralOrigenID INT
	DECLARE @TipoCorralDestinoID INT

	set @TipoCorralOrigenID = (select TipoCorralID from Lote where LoteID = @LoteIDOrigen)
	set @TipoCorralDestinoID = (select TipoCorralID from Lote where LoteID = @LoteIDDestino)

	if @TipoCorralOrigenID <> 1
	begin 
		set @CabezasOrigen = (select count(am.AnimalID) from AnimalMovimiento (nolock) am where am.LoteID = @LoteIDOrigen and am.Activo = 1 AND TipoMovimientoID NOT IN (8, 11, 16))
	end

	if @TipoCorralDestinoID <> 1
	begin 
		set @Cabezas = (select count(am.AnimalID) from AnimalMovimiento (nolock) am where am.LoteID = @LoteIDDestino and am.Activo = 1 AND TipoMovimientoID NOT IN (8, 11, 16))
	end		

	UPDATE Lote
	SET Cabezas = @Cabezas,
		--CabezasInicio = @CabezasInicio, 001
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE LoteID = @LoteIDDestino

	UPDATE Lote --001
	SET CabezasInicio = Cabezas		
	WHERE LoteID = @LoteIDDestino
	and Cabezas > CabezasInicio


	UPDATE Lote
	SET Cabezas = @CabezasOrigen,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaSalida = GETDATE(),
		FechaModificacion = GETDATE()
	WHERE LoteID = @LoteIDOrigen
	
	IF ( @CabezasOrigen <= 0 )
	BEGIN
		UPDATE EntradaGanado
			SET Manejado = 1, 
			UsuarioModificacionID = @UsuarioModificacionID,
			FechaModificacion = GETDATE()
		WHERE LoteID = @LoteIDOrigen
		
		/* Si se sacan todas las cabezas del lote se inactiva el lote */
		UPDATE Lote
		SET Activo = 0,
			UsuarioModificacionID = @UsuarioModificacionID,
			FechaModificacion = GETDATE()
		WHERE LoteID = @LoteIDOrigen
	
	END

	SET NOCOUNT OFF

END

GO
