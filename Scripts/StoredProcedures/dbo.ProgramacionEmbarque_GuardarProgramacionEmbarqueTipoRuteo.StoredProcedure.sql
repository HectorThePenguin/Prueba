USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_GuardarProgramacionEmbarqueTipoRuteo]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_GuardarProgramacionEmbarqueTipoRuteo]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_GuardarProgramacionEmbarqueTipoRuteo]    Script Date: 31/05/2017 09:31:44 a.m. ******/
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
ProgramacionEmbarque_GuardarProgramacionEmbarqueTipoRuteo
'<ListaRuteoDetalle>
	<RuteoDetalle>
		<OrganizacionID>3</OrganizacionID>
		<FechaProgramada>2017-06-14</FechaProgramada>
		<Horas>01:00:00</Horas>
		<Kilometros>6</Kilometros>
		<RuteoID>3</RuteoID>
	</RuteoDetalle>
	<RuteoDetalle>
		<OrganizacionID>3</OrganizacionID>
		<FechaProgramada>2017-06-14</FechaProgramada>
		<Horas>01:00:00</Horas>
		<Kilometros>6</Kilometros>
		<RuteoID>3</RuteoID>
	</RuteoDetalle></ListaRuteoDetalle>'
	,1
	,'1900-01-01'
	,'1900-01-01'
	,1
	,1
	,'Mis Observaciones'
	,1
	,1
	,1
	,1
	,'Nombre de responsable'
	,1
	,1
*/
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_GuardarProgramacionEmbarqueTipoRuteo]
@XmlRuteoDetalle		XML,
@Activo					BIT,
@CitaCarga 				SMALLDATETIME,
@CitaDescarga 			SMALLDATETIME,
@Estatus				INT,
@HorasTransito			INT,
@Observacion			VARCHAR (255),
@OrganizacionDestinoID 	INT,
@OrganizacionID 		INT,
@OrganizacionOrigenID 	INT,
@Recibido				INT,
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

	-- Insertar Observaciones

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

	-- Insertar Información De Ruteo

	CREATE TABLE ##tRuteoDetalle(Orden INT, OrganizacionID INT, Horas TIME(0), Kilometros INT, FechaProgramada SMALLDATETIME, RuteoID INT)
	
	INSERT INTO ##tRuteoDetalle(Orden,FechaProgramada,OrganizacionID,Horas,Kilometros, RuteoID)
		SELECT 
			ROW_NUMBER() OVER (ORDER BY T.item.value('./FechaProgramada[1]', 'SMALLDATETIME') ASC) AS [RowNum],
			T.item.value('./FechaProgramada[1]', 'SMALLDATETIME'),
			T.item.value('./OrganizacionID[1]', 'INT'),
			T.item.value('./Horas[1]', 'TIME(0)'),
			T.item.value('./Kilometros[1]', 'INT'),
			T.item.value('./RuteoID[1]', 'INT')
		FROM  @XmlRuteoDetalle.nodes('ListaRuteoDetalle/RuteoDetalle') AS T(item);

	INSERT INTO EmbarqueRuteo (
		Activo,
		Orden,
		EmbarqueID, 
		Horas,
		Kilometros,
		OrganizacionID,
		FechaProgramada,
		Recibido,
		UsuarioCreacionID,
		RuteoID
	) 
	SELECT
		@Activo,
		T.Orden,
		@EmbarqueID, 
		T.Horas,
		T.Kilometros,
		T.OrganizacionID,
		T.FechaProgramada,
		@Recibido,
		@UsuarioCreacionID,
		T.RuteoID
	FROM ##tRuteoDetalle AS T;

	DROP TABLE ##tRuteoDetalle;

	SET NOCOUNT OFF;
END

GO