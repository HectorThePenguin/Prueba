USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ActualizarTransporteEmbarque]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ActualizarTransporteEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ActualizarTransporteEmbarque]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Luis Alfonso Sandoval Huerta 
-- Create date: 19/06/2017 11:00:00 a.m.
-- Description: Procedimiento almacenado que actualiza la
-- informacion ingresada en la seccion de transporte.
-- SpName: 
/* 
ProgramacionEmbarque_ActualizarTransporteEmbarque
	4
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
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ActualizarTransporteEmbarque]
--@ConfiguracionEmbarqueDetalleID	INT,
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
@UsuarioModificacionID  		INT
AS
BEGIN
	SET NOCOUNT ON;

	-- Guardar Información De Embarque Para La Pantalla De ProgramacionEmbarque_ActualizarTransporteEmbarque
	DECLARE @Activo INT = 1; /* Activo */
	DECLARE @Desactictivado INT = 0; /* Desactictivado */
	DECLARE @FleteCostoID INT = 4; /* Id correspondiente a tipo costo flete */
	DECLARE @GastoVariableCostoID INT = 8; /* Id correspondiente a tipo costo gasto variable */
	DECLARE @DemoraCostoID INT = 32; /* Id correspondiente a tipo costo flete */


	-- Actualizar embarque con información de transporte
	UPDATE Embarque SET
	  CitaDescarga = @CitaDescarga,
	  DobleTransportista = @DobleTransportista,	  
	  Kilometros = @Kilometros,
	  ProveedorID = @Transportista,
	  RutaID = @RutaID,
	  FechaModificacion = GETDATE(),
	  UsuarioModificacionID = @UsuarioModificacionID
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
    		@Activo,
    		@EmbarqueID,
    		@Observacion,
    		GETDATE(),
    		@UsuarioModificacionID
    	);
    END

	-- Actualizar En EmbarqueGastoFijo

	CREATE TABLE #tEmbarqueGastoFijo(GastoFijoID INT, Importe DECIMAL(10,2), Activo BIT)
	
	INSERT INTO #tEmbarqueGastoFijo(GastoFijoID, Importe, Activo)
	SELECT egt.GastoFijoID, gafi.Importe, gafi.Activo 
	FROM EmbarqueTarifa embTar (NOLOCK)
	INNER JOIN EmbarqueGastoTarifa egt (NOLOCK) ON (egt.EmbarqueTarifaID = embTar.EmbarqueTarifaID)
	INNER JOIN GastosFijos gafi (NOLOCK) ON (egt.GastoFijoID = gafi.GastoFijoID)
	WHERE embTar.ConfiguracionEmbarqueDetalleID = @RutaID

	-- Eliminado logico de la informacion de transporte en EmbarqueGastoFijo

	UPDATE EmbarqueGastoFijo SET
	  Activo = @Desactictivado,
	  FechaModificacion = GETDATE(),
	  UsuarioModificacionID = @UsuarioModificacionID
	WHERE EmbarqueID = @EmbarqueID;

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
		@UsuarioModificacionID
	FROM #tEmbarqueGastoFijo AS T;

	-- Actualizar En EmbarqueCosto

	-- Actualizar importe de flete
	UPDATE EmbarqueCosto SET
	  Importe = @ImporteFlete,
	  FechaModificacion = GETDATE(),
	  UsuarioModificacionID = @UsuarioModificacionID
	WHERE CostoID = @FleteCostoID
	AND EmbarqueID = @EmbarqueID;

	-- Actualizar importe de gasto variable
	UPDATE EmbarqueCosto SET
	  Importe = @ImporteGastoVariable,
	  FechaModificacion = GETDATE(),
	  UsuarioModificacionID = @UsuarioModificacionID
	WHERE CostoID = @GastoVariableCostoID
	AND EmbarqueID = @EmbarqueID;

	-- Actualizar importe de demora
	UPDATE EmbarqueCosto SET
	  Importe = @ImporteDemora,
	  FechaModificacion = GETDATE(),
	  UsuarioModificacionID = @UsuarioModificacionID
	WHERE CostoID = @DemoraCostoID
	AND EmbarqueID = @EmbarqueID;

	DROP TABLE #tEmbarqueGastoFijo;

	SET NOCOUNT OFF;
END

GO