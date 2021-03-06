USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_GuardarAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_GuardarAnimal]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_GuardarAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2013/12/18
-- Description: SP para Crear un registro en la tabla de Animal
-- Origen     : APInterfaces
-- EXEC CorteGanado_GuardarAnimal '123456','123456','2013-12-12',1,1,1,200,1,123412,150,1,1,1,1,1,1
-- 001 Jorge Luis Velazquez Araujo 18/02/2016 **Se agrega la parte de la Bitacora
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_GuardarAnimal]
	@Arete VARCHAR(15),
	@AreteMetalico VARCHAR(15),
	@FechaCompra DATETIME,
	@TipoGanadoID INT,
	@CalidadGanadoID INT,
	@ClasificacionGanadoID INT,
	@PesoCompra INT,
	@OrganizacionIDEntrada INT,
	@FolioEntrada BIGINT,
	@PesoLlegada INT,
	@Paletas INT,
	@CausaRechadoID INT,
	@Venta INT,
	@Cronico INT,
	@Activo INT,
	@UsuarioCreacionID INT,
	@CambioSexo BIT = 0,
	@AnimalID BIGINT, --001
	@AplicaBitacora BIT --001
AS
BEGIN
	
	--INICIA 001
	CREATE TABLE #AnimalBitacora
	(
		AnimalID bigint,
		Fecha smalldatetime,
		AreteOriginal varchar(15),
		AreteMetalicoOriginal varchar(15),
		TipoGanadoIDOriginal int,
		AreteReemplazo varchar(15),
		AreteMetalicoNuevoReemplazo varchar(15),
		TipoGanadoIDReemplazo int,
		UsuarioID int
	)

	INSERT INTO #AnimalBitacora
	SELECT 
	@AnimalID
	,GETDATE()
	,a.Arete
	,a.AreteMetalico
	,a.TipoGanadoID
	,@Arete
	,@AreteMetalico
	,@TipoGanadoID
	,@UsuarioCreacionID
	FROM Animal a
	where AnimalID = @AnimalID
	and @AnimalID > 0

	if @AplicaBitacora = 1
	BEGIN
		INSERT INTO BitacoraAnimal
		select 
		AnimalID,
		Fecha,
		AreteOriginal,
		AreteMetalicoOriginal,
		TipoGanadoIDOriginal,
		AreteReemplazo,
		AreteMetalicoNuevoReemplazo,
		TipoGanadoIDReemplazo,
		UsuarioID
		from #AnimalBitacora
	end
	--FINALIZA 001

	DECLARE @IdentityID BIGINT;
	IF (@CausaRechadoID = 0) 
	BEGIN
		SET @CausaRechadoID = NULL;
	END

	IF (ISNULL(@AnimalID,0) = 0)
	BEGIN
		SET @IdentityID = (SELECT TOP 1 AnimalID
						     FROM Animal
						    WHERE Arete = @Arete
							  AND OrganizacionIDEntrada = @OrganizacionIDEntrada
							  AND Activo = 1
						   )
	END

	ELSE
	BEGIN
		SET @IdentityID = @AnimalID --001
	END

	IF (  ISNULL(@IdentityID,0) = 0 ) 
	BEGIN
		IF ( LEN(@AreteMetalico) > 0 )
		BEGIN
			SET @IdentityID = (SELECT TOP 1 AnimalID
					     FROM Animal
					    WHERE AreteMetalico = @AreteMetalico
						  AND OrganizacionIDEntrada = @OrganizacionIDEntrada
						  AND Activo = 1
					   )
		END
	END

	

	IF (  @IdentityID > 0 )
		BEGIN
			UPDATE Animal 
			    SET ClasificacionGanadoID = CASE WHEN @ClasificacionGanadoID IS NULL THEN ClasificacionGanadoID
											ELSE @ClasificacionGanadoID
											END,
					Paletas = @Paletas, 
			        CausaRechadoID = @CausaRechadoID, 
					Venta= @Venta, 
					UsuarioModificacionID = @UsuarioCreacionID, 
					TipoGanadoID = @TipoGanadoID,
					CalidadGanadoID = @CalidadGanadoID,
					Arete = @Arete,
					AreteMetalico = @AreteMetalico,
					FechaModificacion = GETDATE()
			WHERE AnimalID = @IdentityID
			SELECT 
				AnimalID,
				Arete,
				AreteMetalico,
				FechaCompra,
				TipoGanadoID,
				CalidadGanadoID,
				ClasificacionGanadoID,
				PesoCompra,
				OrganizacionIDEntrada,
				FolioEntrada,
				PesoLlegada,
				Paletas,
				CausaRechadoID,
				Venta,
				Cronico,
				Activo,
				FechaCreacion,
				UsuarioCreacionID,
				FechaModificacion,
				UsuarioModificacionID
			FROM Animal 
			WHERE AnimalID = @IdentityID
		END
	ELSE
		BEGIN		
			/* Se crea registro en la tabla de Animal*/
			INSERT INTO Animal(
				Arete,
				AreteMetalico,
				FechaCompra,
				TipoGanadoID,
				CalidadGanadoID,
				ClasificacionGanadoID,
				PesoCompra,
				OrganizacionIDEntrada,
				FolioEntrada,
				PesoLlegada,
				Paletas,
				CausaRechadoID,
				Venta,
				Cronico,
				Activo,
				FechaCreacion,
				UsuarioCreacionID,
				CambioSexo)
			VALUES(
				@Arete,
				@AreteMetalico,
				@FechaCompra,
				@TipoGanadoID,
				@CalidadGanadoID,
				@ClasificacionGanadoID,
				@PesoCompra,
				@OrganizacionIDEntrada,
				@FolioEntrada,
				@PesoLlegada,
				@Paletas,
				@CausaRechadoID,
				@Venta,
				@Cronico,
				@Activo,
				GETDATE(),
				@UsuarioCreacionID,
				@CambioSexo)
			/* Se obtiene el id Insertado */
			SET @IdentityID = (SELECT @@IDENTITY)
			SELECT 
				AnimalID,
				Arete,
				AreteMetalico,
				FechaCompra,
				TipoGanadoID,
				CalidadGanadoID,
				ClasificacionGanadoID,
				PesoCompra,
				OrganizacionIDEntrada,
				FolioEntrada,
				PesoLlegada,
				Paletas,
				CausaRechadoID,
				Venta,
				Cronico,
				Activo,
				FechaCreacion,
				UsuarioCreacionID,
				FechaModificacion,
				UsuarioModificacionID
			FROM Animal 
			WHERE AnimalID = @IdentityID
		END
END

GO
