USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarGanadoVendido]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaIndividualVenta_GuardarGanadoVendido]
GO
/****** Object:  StoredProcedure [dbo].[SalidaIndividualVenta_GuardarGanadoVendido]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/02/28
-- Description: 
--  exec SalidaIndividualVenta_GuardarGanadoVendido 16, 500, 300, 1, 1, 3, 1
-- 001, Jorge Luis Velazquez Araujo 11/05/2015, se agrega validacion, para que la Venta deje correctas las Cabezas del Lote
-- 002, Jorge Luis Velazquez Araujo 15/07/2015, se quitan los Updates en la tabla Lote, ya que se realizan en otro proceso
--=============================================
CREATE PROCEDURE [dbo].[SalidaIndividualVenta_GuardarGanadoVendido]
	@FolioTicket INT,
	@PesoBruto decimal(10, 2),
	@Peso decimal(10, 2),
	@NumeroDeCabezas INT,
	@OrganizacionID INT,
	@UsuarioCreacionID INT,
	@TipoVenta INT = 1
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @VentaGanadoID INT
	DECLARE @LoteId INT
	DECLARE @AnimalMovimientoId INT
	DECLARE @FolioFactura VARCHAR(15)
	DECLARE @Serie VARCHAR(5)
	DECLARE @Folio VARCHAR(10)

	IF @TipoVenta = 1 BEGIN
		SELECT @VentaGanadoID = VG.VentaGanadoID, @LoteId = VG.LoteID 
		FROM VentaGanado VG(NOLOCK) 
		INNER JOIN Lote L(NOLOCK)
			ON (VG.LoteID = L.LoteID)
		WHERE VG.FolioTicket = @FolioTicket
			AND VG.OrganizacionID = @OrganizacionID
	END
	ELSE BEGIN
		SELECT @VentaGanadoID = SG.SalidaGanadoIntensivoID, @LoteId = SG.LoteID 
		FROM SalidaGanadoIntensivo SG(NOLOCK) 
		INNER JOIN Lote L(NOLOCK)
			ON (SG.LoteID = L.LoteID)
		WHERE SG.FolioTicket = @FolioTicket
			AND SG.OrganizacionID = @OrganizacionID
	END

	CREATE TABLE #tAnimales
	(
		AnimalID BIGINT
	)

	INSERT INTO #tAnimales
	SELECT AnimalID
	FROM VentaGanadoDetalle(NOLOCK)
	WHERE VentaGanadoID = @VentaGanadoID

	-- Obtiene el numero que sigue de la factura segun el parametro configurado para cada organizacion.
	EXEC FolioFactura_Obtener @OrganizacionID, @FolioFactura OUTPUT, @Serie OUTPUT, @Folio OUTPUT

	-- Se desactiva el ticket se actualiza el peso.
	IF @TipoVenta = 1 BEGIN
		UPDATE VentaGanado SET PesoBruto = @PesoBruto, FolioFactura = @FolioFactura, Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioCreacionID 
		WHERE FolioTicket = @FolioTicket AND OrganizacionID = @OrganizacionID
		-- Se desactivan los aretes vendidos
		UPDATE VentaGanadoDetalle SET Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacion = @UsuarioCreacionID 
		WHERE VentaGanadoID = @VentaGanadoID
	END
	ELSE BEGIN
		UPDATE SalidaGanadoIntensivo SET FolioFactura = @FolioFactura, Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioCreacionID 
		WHERE FolioTicket = @FolioTicket AND OrganizacionID = @OrganizacionID
		-- Se desactivan los aretes vendidos
		UPDATE SalidaGanadoIntensivoPesaje SET PesoBruto = @PesoBruto, Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioCreacionID 
		WHERE SalidaGanadoIntensivoID = @VentaGanadoID
	END
	--002
	--Al guardar la salida por venta debe decrementrarse el campo <Cabezas> de la tabla “Lote” 
	--y si el campo cabezas es igual a cero debe cambiar el campo Activo a 0 para cerrar el lote.
	--UPDATE Lote SET Cabezas = Cabezas - @NumeroDeCabezas, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioCreacionID WHERE LoteId = @LoteId;
	--IF (SELECT cabezas FROM Lote WHERE LoteId = @LoteId) <= 0
	--BEGIN
	--	UPDATE Lote SET Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioCreacionID WHERE LoteId = @LoteId;
	--END

	UPDATE Animal SET Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioCreacionID  WHERE AnimalId IN (
			SELECT A.AnimalID FROM #tAnimales (NOLOCK) AS VGD INNER JOIN Animal (NOLOCK) AS A ON (VGD.AnimalID = A.AnimalID))
	
	-- Insertamos el movimiento que esta activo con los nuevos valores que se capturaron en pantalla.
	INSERT INTO AnimalMovimiento(AnimalID, OrganizacionID, CorralID, LoteID, LoteIDOrigen, FechaMovimiento, Peso, TipoMovimientoID, Temperatura,
								TrampaID, OperadorID, Observaciones, Activo, FechaCreacion, UsuarioCreacionID)
	SELECT A.AnimalID, @OrganizacionID, CASE WHEN dbo.ObtenerCorralAnteriorAEnfermeria(A.AnimalID) = 0 THEN AM.CorralID ELSE dbo.ObtenerCorralAnteriorAEnfermeria(A.AnimalID) END, 
								CASE WHEN dbo.ObtenerLoteAnteriorAEnfermeria(A.AnimalID) = 0 THEN AM.LoteID ELSE dbo.ObtenerLoteAnteriorAEnfermeria(A.AnimalID) END
								, CASE WHEN dbo.ObtenerLoteAnteriorAEnfermeria(A.AnimalID) = 0 THEN AM.LoteID ELSE dbo.ObtenerLoteAnteriorAEnfermeria(A.AnimalID) END
								, GETDATE(), @Peso, 11, AM.Temperatura, AM.TrampaID, AM.OperadorID, AM.Observaciones, 1, GETDATE(), @UsuarioCreacionID
	FROM #tAnimales (NOLOCK) AS VGD 
	INNER JOIN Animal (NOLOCK) AS A ON (VGD.AnimalID = A.AnimalID) 
	INNER JOIN AnimalMovimiento (NOLOCK) AS AM ON (AM.AnimalID = A.AnimalID AND AM.Activo = 1)

	-- Inactivar los movimientos anteriores
	UPDATE AnimalMovimiento SET Activo = 0, FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioCreacionID 
	WHERE Activo = 1 AND AnimalId IN (SELECT A.AnimalID FROM #tAnimales (NOLOCK) AS VGD INNER JOIN Animal (NOLOCK) AS A ON (VGD.AnimalID = A.AnimalID))

	UPDATE AnimalMovimiento SET Activo = 1
	WHERE AnimalId IN (SELECT A.AnimalID FROM #tAnimales (NOLOCK) AS VGD INNER JOIN Animal (NOLOCK) AS A ON (VGD.AnimalID = A.AnimalID))
		AND TipoMovimientoID = 11

	CREATE TABLE #tAnimalesCosto
	(
		AnimalID BIGINT
	)
	INSERT INTO #tAnimalesCosto
	SELECT AC.AnimalID
	FROM #tAnimales tA
	INNER JOIN AnimalCosto AC(NOLOCK)
		ON (tA.AnimalID = AC.AnimalID
			AND AC.Importe > 0 AND CostoID = 1)

	--Movemos a las tablas historicas AnimalHistorico, AnimalMovimientoHistorico, AnimalCostoHistorico
	--insertamos la AnimalMovimiento historico
	INSERT INTO AnimalMovimientoHistorico (AnimalMovimientoId, AnimalId, OrganizacionId, CorralId, LoteId, FechaMovimiento, Peso,
		Temperatura, TipoMovimientoId, TrampaId, OperadorId, Observaciones, Activo, FechaCreacion, UsuarioCreacionId,
		FechaModificacion, UsuarioModificacionId, AnimalMovimientoIDAnterior)
		SELECT AnimalMovimientoId, AM.AnimalId, OrganizacionId, CorralId, LoteId, FechaMovimiento, Peso,
				Temperatura, TipoMovimientoId, TrampaId, OperadorId, Observaciones, Activo, FechaCreacion, UsuarioCreacionId,
				FechaModificacion, UsuarioModificacionId, AnimalMovimientoIDAnterior
		FROM AnimalMovimiento AM(NOLOCK)
		INNER JOIN #tAnimales tAC
			ON (AM.AnimalID = tAC.AnimalID)

	DELETE AM FROM AnimalMovimiento AM(NOLOCK)
	INNER JOIN #tAnimalesCosto A
		ON (AM.AnimalID = A.AnimalID)

		--002
		--DECLARE @CabezasLote INT--001
		--DECLARE @TipoCorralID INT
		--set @TipoCorralID = (select TipoCorralID from Lote where LoteID = @LoteId)
		--if @TipoCorralID <> 1
		--begin 
		--set @CabezasLote =		 (select count(am.AnimalID) from AnimalMovimiento (nolock) am where am.LoteID = @LoteId and am.Activo = 1)
		--update Lote set Cabezas = @CabezasLote
		--where LoteID = @LoteId
		--end


	DROP TABLE #tAnimales

	SET NOCOUNT OFF;
END

GO
