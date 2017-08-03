USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_GuardarDatosEmbarque]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_GuardarDatosEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_GuardarDatosEmbarque]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Luis Alfonso Sandoval Huerta 
-- Create date: 19/06/2017 11:00:00 a.m.
-- Description: Procedimiento almacenado que guarda la
-- informacion ingresada en la seccion de datos.
-- SpName: 
/* 
ProgramacionEmbarque_GuardarDatosEmbarque 14974, 4384, 1, '215-UM4', '176-EC3', 'Mis Observaciones', 1
*/
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_GuardarDatosEmbarque]
@EmbarqueID			INT,
@ChoferID1          INT,
@ChoferID2          INT,
@PlacaJaula			VARCHAR (10),
@PlacaCamion  		VARCHAR (10),
@Observacion		VARCHAR (255),
@UsuarioCreacionID  INT
AS
BEGIN
	SET NOCOUNT ON;
    DECLARE @Activo BIT = 1;  /* Activo */
    DECLARE @TipoFolio INT = 1; /* Tipo de folio Programacion Embarque */
    DECLARE @Folio INT = 0; /* Folio nuevo para Programacion Embarque */
    DECLARE @OrganizacionID INT = 0; /* OrganizacionID */
    DECLARE @JaulaID INT = 0; /* JaulaID */
    DECLARE @CamionID INT = 0; /* CamionID */
    DECLARE @Operador1 INT = 0; /* ProveedorChoferID de operador 1 */
    DECLARE @Operador2 INT = 0; /* ProveedorChoferID de operador 2 */
    DECLARE @NumOperador1 INT = 1; /* Numero de operador 1 */
    DECLARE @NumOperador2 INT = 2; /* Numero de operador 2 */

    /* Se obtiene la organizacion ID del embarque */  
    SELECT @OrganizacionID = OrganizacionID
    FROM Embarque
    WHERE EmbarqueID = @EmbarqueID;

    /* Se obtiene la Operador1 segun el ChoferID*/
    SELECT @Operador1 = ProveedorChoferID
    FROM ProveedorChofer
    WHERE ChoferID = @ChoferID1;

    /* Se obtiene el jaula ID segun su placa */
    SELECT @JaulaID = JaulaID
    FROM Jaula
    WHERE PlacaJaula = @PlacaJaula;

    /* Se obtiene el camion ID segun su placa */
    SELECT @CamionID = CamionID
    FROM Camion
    WHERE PlacaCamion = @PlacaCamion;

    /* Se obtiene el folioEmbarque */  
    EXEC Folio_Obtener @OrganizacionID,@TipoFolio,@Folio OUTPUT;
    
	/* Guardar Información De Embarque Para La Pantalla De Datos */
	UPDATE Embarque 
       SET FolioEmbarque = @Folio,
    	   JaulaID = @JaulaID,
    	   CamionID = @CamionID,
    	   FechaModificacion = GETDATE(),
    	   UsuarioModificacionID = @UsuarioCreacionID
	 WHERE EmbarqueID = @EmbarqueID;

    /* Insertar primer operador */
    INSERT INTO EmbarqueOperador
	(
		Activo,
		EmbarqueID,
		ProveedorChoferID,
		NumOperador,
		FechaModificacion,
		UsuarioCreacionID
	)
	VALUES 
	(
		@Activo,
		@EmbarqueID,
		@Operador1,
		@NumOperador1,
		GETDATE(),
		@UsuarioCreacionID
	);

    /* Insertar segundo operador */
	IF (NULLIF(@ChoferID2, 0) IS NOT NULL)
    BEGIN 

        /* Se obtiene la Operador2 segun el ChoferID*/
        SELECT @Operador2 = ProveedorChoferID
        FROM ProveedorChofer
        WHERE ChoferID = @ChoferID2;

    	INSERT INTO EmbarqueOperador
    	(
    		Activo,
    		EmbarqueID,
    		ProveedorChoferID,
    		NumOperador,
    		FechaModificacion,
    		UsuarioCreacionID
    	)
    	VALUES 
    	(
    		@Activo,
    		@EmbarqueID,
    		@Operador2,
    		@NumOperador2,
    		GETDATE(),
    		@UsuarioCreacionID
    	);
    END

    /* Insertar observaciones */
    IF (NULLIF(@Observacion, '') IS NOT NULL)
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
    		@UsuarioCreacionID
    	);
    END

    /* Se regresa folio de embarque registrado */
    SELECT 
        @Folio AS FolioEmbarque,
        es.Descripcion AS Descripcion
    FROM Embarque em 
    INNER JOIN Estatus es ON em.Estatus = es.EstatusID
    WHERE EmbarqueID = @EmbarqueID

	SET NOCOUNT OFF;
END

GO