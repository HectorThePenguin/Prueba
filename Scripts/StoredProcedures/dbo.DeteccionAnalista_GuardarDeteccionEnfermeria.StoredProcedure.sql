USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnalista_GuardarDeteccionEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionAnalista_GuardarDeteccionEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionAnalista_GuardarDeteccionEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Roque.Solis
-- Create date: 20/02/2014
-- Description:  guarda la deteccion de
-- Origen: APInterfaces
-- EXEC DeteccionAnalista_GuardarDeteccionEnfermeria 1,1,5,5,2,2,'',0,'<ROOT><Problemas><ProblemaID>3</ProblemaID></Problemas></ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[DeteccionAnalista_GuardarDeteccionEnfermeria]
 @Deteccion INT,
 @Arete VARCHAR(15),
 @Estatus INT,
 @AnimalMovimientoID BIGINT,
 @Usuario INT,
 @GradoId INT,
 @Justificacion varchar(50),
 @Diagnostico INT,
 @ProblemasAnalista XML
AS
BEGIN
	DECLARE @DeteccionAnimalID INT
	DECLARE @DiagnosticoAnalistaID INT
	CREATE TABLE #tmpProblemasAnalista
	(
		ProblemaID INT
	)
	INSERT #tmpProblemasAnalista ([ProblemaID])
	SELECT [ProblemaID] = t.item.value('./ProblemaID[1]', 'INT')
	FROM @ProblemasAnalista.nodes('ROOT/Problemas') AS t(item)
	UPDATE Deteccion
		SET Arete = A.Arete,
				AreteMetalico = A.AreteMetalico,
			FotoDeteccion = ''
  FROM AnimalMovimiento AM 
	INNER JOIN Animal A ON (A.AnimalID = AM.AnimalID AND AM.AnimalMovimientoID = @AnimalMovimientoID)
	WHERE DeteccionID =  @Deteccion
	INSERT INTO DeteccionAnimal
	   (AnimalMovimientoID,  Arete,        AreteMetalico,     	 
		FotoDeteccion,       LoteID,	   OperadorID,        	 
		TipoDeteccionID,     GradoID,      Observaciones,     	 
		DescripcionGanadoID, NoFierro,     FechaDeteccion,    	 
		DeteccionAnalista,   Activo,       FechaCreacion,     	 
		UsuarioCreacionID)
	SELECT 
		AM.AnimalMovimientoID, A.Arete,      A.AreteMetalico,     
		D.FotoDeteccion,     D.LoteID,     D.OperadorID,        
		D.TipoDeteccionID,   D.GradoID,    D.Observaciones,     
		D.DescripcionGanadoID, D.NoFierro,   D.FechaDeteccion,    
		@Diagnostico,        D.Activo,     D.FechaCreacion,     
		D.UsuarioCreacionID
	FROM Deteccion D,
	AnimalMovimiento AM 
	INNER JOIN Animal A ON (A.AnimalID = AM.AnimalID)
	WHERE D.DeteccionID =  @Deteccion AND AM.AnimalMovimientoID = @AnimalMovimientoID;
	SET @DeteccionAnimalID = (SELECT @@IDENTITY)
		INSERT INTO DeteccionSintomaAnimal
			   (DeteccionAnimalID,     SintomaID,          Activo,                   
				FechaCreacion,         UsuarioCreacionID,  FechaModificacion,     
				UsuarioModificacionID)	
		SELECT @DeteccionAnimalID ,    SintomaID,          Activo,                   
			   FechaCreacion,          UsuarioCreacionID,  FechaModificacion,        
			   UsuarioModificacionID
		  FROM DeteccionSintoma
		  WHERE DeteccionID = @Deteccion
  	IF @Diagnostico = 1
	BEGIN
	    INSERT INTO DiagnosticoAnalista
				(DeteccionAnimalID, GradoID,         Justificacion,         
				  Activo,            FechaCreacion,   UsuarioCreacionID)
		   VALUES
				(@DeteccionAnimalID, @GradoId,        @Justificacion,
				 @Estatus,           GETDATE(),       @Usuario)
		SET @DiagnosticoAnalistaID = (SELECT @@IDENTITY)
	    INSERT INTO DiagnosticoAnalistaDetalle 
				 (DiagnosticoAnalistaID, 	ProblemaID,       Activo,                       
				  FechaCreacion,            UsuarioCreacionID)
		  SELECT @DiagnosticoAnalistaID,    ProblemaID,       @Estatus,
				  GETDATE(),				@Usuario                  
		  FROM #tmpProblemasAnalista
	END		
	DROP TABLE #tmpProblemasAnalista
END

GO
