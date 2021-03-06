USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TransferenciaGanadoIndividual_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TransferenciaGanadoIndividual_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[TransferenciaGanadoIndividual_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/11/26
-- Description: SP para Transferir un animal de corral
-- Origen     : APInterfaces
-- EXEC [dbo].[TransferenciaGanadoIndividual_Guardar] 1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[TransferenciaGanadoIndividual_Guardar]
	@AnimalID INT,
	@CorralDestinoID INT,
	@UsuarioCreacionID INT,
	@DecrementaCabezas BIT
AS
BEGIN

	DECLARE @CorralID INT;
	DECLARE @LoteID INT;
	DECLARE @LoteIDOrigen INT;
	DECLARE @Peso INT;
	DECLARE @Temperatura INT;
	DECLARE @TrampaID INT;
	DECLARE @OperadorID INT;
	DECLARE @MovTraspasoDeGanado INT = 17;
	DECLARE @OrganizacionID INT;
		
	/* Se obtiene la informacion del animal a transferir*/
	SELECT /*@AnimalID = A.AnimalID,*/
	       @Peso = AM.Peso, 
		   @Temperatura = AM.Temperatura, 
		   @TrampaID = AM.TrampaID, 
		   @OperadorID = AM.OperadorID, 
		   @LoteIDOrigen = AM.LoteID,
		   @OrganizacionID = AM.OrganizacionID
	  FROM Animal A
	 INNER JOIN AnimalMovimiento AM ON A.AnimalID = AM.AnimalID 
	 WHERE 1 = 1
	   AND A.AnimalID = @AnimalID
	   AND AM.Activo = 1;	
	
	/* Se obtienen los Datos del corral Destino*/
	SELECT TOP 1 @CorralID = C.CorralID, @LoteID = L.LoteID
	  FROM Lote L 
	 INNER JOIN Corral C ON C.CorralID = L.CorralID
	 WHERE 1 = 1 
	   AND L.Activo = 1
	   AND C.CorralID = @CorralDestinoID;
	
	/* Se guarda el movimiento de transpaso */
	EXECUTE AnimalMovimiento_Guardar @AnimalID, @OrganizacionID, @CorralID, @LoteID, @Peso, @Temperatura, @MovTraspasoDeGanado, @TrampaID, @OperadorID, '', 1, @UsuarioCreacionID;

	IF @DecrementaCabezas = 1
		BEGIN

			DECLARE @CabezasOrigen INT
			SET @CabezasOrigen = (SELECT COUNT(am.AnimalID) 
								  FROM AnimalMovimiento (NOLOCK) am 
								  inner join Animal a on am.AnimalID = a.AnimalID --001
								  WHERE am.LoteID = @LoteIDOrigen 
									AND am.Activo = 1 
									AND a.Activo = 1 --001
									AND TipoMovimientoID NOT IN (8, 11, 16))

			DECLARE @CabezasDestino INT
			SET @CabezasDestino = (SELECT COUNT(am.AnimalID) 
								   FROM AnimalMovimiento (NOLOCK) am 
								   inner join Animal a on am.AnimalID = a.AnimalID --001
								   WHERE am.LoteID = @LoteID
									 AND am.Activo = 1 
									 AND a.Activo = 1 --001
									 AND TipoMovimientoID NOT IN (8, 11, 16))

			/* Decrementar lote Origen */
			UPDATE Lote
			   SET Cabezas = @CabezasOrigen,
				   Activo = CASE WHEN (@CabezasOrigen) <= 0 THEN 0 ELSE 1 END,
				   FechaSalida = GETDATE(),
				   UsuarioModificacionID = @UsuarioCreacionID,
				   FechaModificacion = GETDATE()
			 WHERE LoteID = @LoteIDOrigen;
			
			/* Incrementar lote Destino */
			UPDATE Lote
			   SET Cabezas = @CabezasDestino,
				   CabezasInicio = CASE WHEN @CabezasDestino > CabezasInicio THEN @CabezasDestino ELSE CabezasInicio END,
				   UsuarioModificacionID = @UsuarioCreacionID,
				   FechaModificacion = GETDATE()
			 WHERE LoteID = @LoteID;
	END
END

GO
