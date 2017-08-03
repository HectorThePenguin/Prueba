USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_GuardarProgramacionEmbarque]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_GuardarProgramacionEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_GuardarProgramacionEmbarque]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Luis Alfonso Sandoval Huerta 
-- Create date: 05/06/2017 11:00:00 a.m.
-- Description: Procedimiento almacenado que guarda la
-- informacion ingresada en la seccion de programacion
-- para un programacion de embarque tipo ruteo.
-- SpName     : 
/* 
ProgramacionEmbarque_GuardarProgramacionEmbarque 
	1
	,'1900-01-01'
	,'1900-01-01'
	,1
	,1
	,'Mis Observaciones'
	,1
	,1
	,1
	,'Nombre de responsable'
	,1
	,1
*/
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_GuardarProgramacionEmbarque]
@Activo					BIT,
@CitaCarga 				SMALLDATETIME,
@CitaDescarga 			SMALLDATETIME,
@Estatus				INT,
@HorasTransito			INT,
@Observacion			VARCHAR (255),
@OrganizacionDestinoID 	INT,
@OrganizacionID 		INT,
@OrganizacionOrigenID 	INT,
@ResponsableEmbarque 	VARCHAR(20),
@TipoEmbarqueID 		INT,
@UsuarioCreacionID  	INT
AS
BEGIN
	SET NOCOUNT ON;

	-- Guardar Información De Embarque Para La Pantalla De Programación

	DECLARE @EmbarqueID INT;

	INSERT INTO Embarque (
		Activo,
		CitaCarga,			
		CitaDescarga,
		Estatus,		
		ResponsableEmbarque,						
		FolioEmbarque,
		HorasTransito,		
		OrganizacionDestinoID,
		OrganizacionID,
		OrganizacionOrigenID,
		TipoEmbarqueID,	
		UsuarioCreacionID
	) 
	VALUES (
		@Activo,
		@CitaCarga,			
		@CitaDescarga,
		@Estatus,		
		@ResponsableEmbarque,						
		0,
		@HorasTransito,		
		@OrganizacionDestinoID,
		@OrganizacionID,
		@OrganizacionOrigenID,
		@TipoEmbarqueID,
		@UsuarioCreacionID
	);

	SET @EmbarqueID  = 
	(SELECT 
		EmbarqueID
	FROM Embarque 
	WHERE EmbarqueID = @@Identity)

	-- Insertar Observaciónes

	IF (NULLIF(@Observacion, '') IS NOT NULL)
    BEGIN
    	INSERT EmbarqueObservaciones (
			Activo,
			EmbarqueID,		
			Observacion,	
			UsuarioCreacionID
		)
		VALUES (
			@Activo,
			@EmbarqueID,			
			@Observacion,	
			@UsuarioCreacionID
		);  
    END

	SET NOCOUNT OFF;
END

GO