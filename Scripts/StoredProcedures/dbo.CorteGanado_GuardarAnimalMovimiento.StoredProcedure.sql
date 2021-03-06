USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_GuardarAnimalMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_GuardarAnimalMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_GuardarAnimalMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2013/12/18
-- Description: SP para Crear un registro en la tabla de AnimalMovimiento
-- Origen     : APInterfaces
-- EXEC [dbo].[CorteGanado_GuardarAnimalMovimiento] 3,1,14,1,'2013-12-12',250,35,1,4,1,'Comentarios',1,1
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_GuardarAnimalMovimiento]
	@AnimalID BIGINT,
	@OrganizacionID INT,
	@CorralID INT,
	@LoteID INT,
	@Peso INT,
	@Temperatura DECIMAL(5,1),
	@TipoMovimientoID INT,
	@TrampaID INT,
	@OperadorID INT,
	@Observaciones VARCHAR(255),
	@Activo INT,
	@UsuarioCreacionID INT
AS
BEGIN
	DECLARE @IdentityID BIGINT;
	DECLARE @IDMovimientoAnterior BIGINT;
	DECLARE @LoteOrigen INT;
	/* Se obtiene el ultimo movimiento activo para desactivarlo*/
	SELECT TOP 1 @IDMovimientoAnterior = AnimalMovimientoID, @LoteOrigen = LoteID
								   FROM AnimalMovimiento 
								  WHERE AnimalID = @AnimalID
								    AND OrganizacionID = @OrganizacionID
									AND Activo = 1
	/* Se actualiza el movimiento anterios a inactivo para q el nuevo este sea el ultimo movimiento activo */  
	UPDATE AnimalMovimiento SET Activo = 0 WHERE AnimalMovimientoID = @IDMovimientoAnterior
	/* Se crea registro en la tabla de Animal Movimiento*/
	INSERT INTO AnimalMovimiento(
		AnimalID,
		OrganizacionID,
		CorralID,
		LoteID,
		FechaMovimiento,
		Peso,
		Temperatura,
		TipoMovimientoID,
		TrampaID,
		OperadorID,
		Observaciones,
		LoteIDOrigen,
		AnimalMovimientoIDAnterior,
		Activo,
		FechaCreacion,
		UsuarioCreacionID)
	VALUES(
		@AnimalID,
		@OrganizacionID,
		@CorralID,
		@LoteID,
		GETDATE(),
		@Peso,
		@Temperatura,
		@TipoMovimientoID,
		@TrampaID,
		@OperadorID,
		@Observaciones,
		@LoteOrigen,
		@IDMovimientoAnterior,
		@Activo,
		GETDATE(),
		@UsuarioCreacionID)
	/* Se obtiene el id Insertado */
	SET @IdentityID = (SELECT @@IDENTITY) 
	SELECT 
		AnimalID,
		AnimalMovimientoID,
		OrganizacionID,
		CorralID,
		LoteID,
		FechaMovimiento,
		Peso,
		Temperatura,
		TipoMovimientoID,
		TrampaID,
		OperadorID,
		Observaciones,
		LoteIDOrigen,
		AnimalMovimientoIDAnterior,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM AnimalMovimiento 
	WHERE AnimalMovimientoID = @IdentityID;
END

GO
