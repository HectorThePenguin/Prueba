	USE [SIAP]
	GO
	/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_GuardarTransporteEmbarque]    Script Date: 31/05/2017 09:31:44 a.m. ******/
	DROP PROCEDURE [dbo].[ProgramacionEmbarque_GuardarTransporteEmbarque]
	GO
	/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_GuardarTransporteEmbarque]    Script Date: 31/05/2017 09:31:44 a.m. ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	--======================================================  
	-- Author     : Luis Alfonso Sandoval Huerta 
	-- Create date: 19/06/2017 11:00:00 a.m.
	-- Description: Procedimiento almacenado que guarda la
	-- informacion ingresada en la seccion de transporte.
	-- SpName: 
	/* 
	ProgramacionEmbarque_GuardarTransporteEmbarque 
		'1900-01-01'
		,1
		,14974
		,600
		,600
		,500
		,5
		,'Mis Observaciones'
		,13
		,1
		,1
	*/
	--======================================================
	CREATE PROCEDURE [dbo].[ProgramacionEmbarque_GuardarTransporteEmbarque]
	@CitaDescarga					SMALLDATETIME,
	@DobleTransportista				BIT,
	@EmbarqueID 					INT,
	@ImporteDemora					DECIMAL(10,2),
	@ImporteFlete					DECIMAL(10,2),
	@ImporteGastoVariable			DECIMAL(10,2),
	@Kilometros 					DECIMAL(10,2),
	@Observacion					VARCHAR (255),
	@RutaID 						INT,
	@Transportista				  	INT,
	@UsuarioCreacionID  			INT
	AS
	BEGIN
		SET NOCOUNT ON;
		DECLARE @FleteCostoID INT = 4; /* Id correspondiente a tipo costo flete */
		DECLARE @GastoVariableCostoID INT = 8; /* Id correspondiente a tipo costo gasto variable */
		DECLARE @DemoraCostoID INT = 32; /* Id correspondiente a tipo costo flete */
		-- Guardar Información De Embarque Para La Pantalla De ProgramacionEmbarque_GuardarTransporteEmbarque

		-- Actualizar embarque con información de transporte
		UPDATE Embarque SET
		  CitaDescarga = @CitaDescarga,
		  DobleTransportista = @DobleTransportista,	  
		  Kilometros = @Kilometros,
		  ProveedorID = @Transportista,
		  RutaID = @RutaID,
		  FechaModificacion = GETDATE(),
		  UsuarioModificacionID = @UsuarioCreacionID
		WHERE EmbarqueID = @EmbarqueID;

		-- Actualizar Observaciones

		IF (@Observacion IS NOT NULL)
		BEGIN 
			INSERT INTO EmbarqueObservaciones
			(
				Activo,
				EmbarqueID,
				Observacion,
				FechaModificacion,
				UsuarioCreacionID
			)
			VALUES 
			(
				1,
				@EmbarqueID,
				@Observacion,
				GETDATE(),
				@UsuarioCreacionID
			);
		END

		-- Insertar En EmbarqueGastoFijo

		CREATE TABLE #tEmbarqueGastoFijo(GastoFijoID INT, Importe DECIMAL(10,2), Activo BIT)
		
		INSERT INTO #tEmbarqueGastoFijo(GastoFijoID, Importe, Activo)
		SELECT egt.GastoFijoID, gafi.Importe, gafi.Activo 
		FROM EmbarqueTarifa embTar (NOLOCK)
		INNER JOIN EmbarqueGastoTarifa egt (NOLOCK) ON (egt.EmbarqueTarifaID = embTar.EmbarqueTarifaID)
		INNER JOIN GastosFijos gafi (NOLOCK) ON (egt.GastoFijoID = gafi.GastoFijoID)
		WHERE embTar.ConfiguracionEmbarqueDetalleID = @RutaID
		

		INSERT INTO EmbarqueGastoFijo (
			Activo,
			EmbarqueID,
			GastoFijoID, 
			Importe,
			UsuarioCreacionID
		) 
		SELECT
			T.Activo,
			@EmbarqueID, 
			T.GastoFijoID,
			T.Importe,
			@UsuarioCreacionID
		FROM #tEmbarqueGastoFijo AS T;

		-- Insertar En EmbarqueCosto

		-- Insertar importe de flete
		INSERT INTO EmbarqueCosto (
			Activo,
			EmbarqueID,
			CostoID,
			Importe,
			UsuarioCreacionID		
		)
		VALUES (
			1,
			@EmbarqueID,
			@FleteCostoID,
			@ImporteFlete,
			@UsuarioCreacionID
		);

		-- Insertar importe de gasto variable
		INSERT INTO EmbarqueCosto (
			Activo,
			EmbarqueID,
			CostoID,
			Importe,
			UsuarioCreacionID		
		)
		VALUES (
			1,
			@EmbarqueID,
			@GastoVariableCostoID,
			@ImporteGastoVariable,
			@UsuarioCreacionID
		);

		-- Insertar importe de demora
		INSERT INTO EmbarqueCosto (
			Activo,
			EmbarqueID,
			CostoID,
			Importe,
			UsuarioCreacionID		
		)
		VALUES (
			1,
			@EmbarqueID,
			@DemoraCostoID,
			@ImporteDemora,
			@UsuarioCreacionID
		);

		DROP TABLE #tEmbarqueGastoFijo;

		SET NOCOUNT OFF;
	END

	GO