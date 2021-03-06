USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ReemplazarAretes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ReemplazarAretes]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ReemplazarAretes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/07/22
-- Description: SP para Obtener Si el animal es de Carga Inicial
-- Origen     : APInterfaces
-- EXEC Animal_ReemplazarAretes 1
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ReemplazarAretes]
	@AnimalID BIGINT,
	@Arete VARCHAR(15),
	@AreteTestigo VARCHAR(15),
	@CorralIDDestino INT,
	@LoteIDDestino INT,
	@UsuarioCreacionID INT
AS
BEGIN
	
	DECLARE @AnimalIDReemplazo BIGINT;
	DECLARE @AreteReemplazo VARCHAR(15);
	DECLARE @AreteTestigoReemplazo VARCHAR(15);
	DECLARE @CorralIDOrigen INT;
	DECLARE @LoteIDOrigen INT;
	DECLARE @SexoOrigen CHAR(1);
	
	/* Tomar el corral del animal a reemplazar*/
	SELECT TOP 1
		   @CorralIDOrigen = AM.CorralID,
		   @LoteIDOrigen = AM.LoteID,
		   @SexoOrigen = TG.Sexo
	  FROM AnimalMovimiento AM(NOLOCK)
	 INNER JOIN Animal A(NOLOCK) ON A.AnimalID = AM.AnimalID
     INNER JOIN TipoGanado TG ON A.TipoGanadoID = TG.TipoGanadoID
	 WHERE AM.AnimalID = @AnimalID
	   AND AM.Activo = 1;
	   
	/* Tomar el primer animal del corral destino a remplazar */
	SELECT TOP 1 @AnimalIDReemplazo = Animales.AnimalID,
			   @AreteReemplazo = Animales.Arete,
			   @AreteTestigoReemplazo = Animales.AreteMetalico
	  FROM (
		SELECT TOP 1 A.AnimalID,
			   A.Arete,
			   A.AreteMetalico,
			   A.FechaModificacion,
			   1 AS Origen
		  FROM AnimalMovimiento AM(NOLOCK)
		 INNER JOIN Animal A(NOLOCK) ON A.AnimalID = AM.AnimalID
		 INNER JOIN TipoGanado TG ON A.TipoGanadoID = TG.TipoGanadoID
		 WHERE CorralID = @CorralIDDestino
		   AND LoteID = @LoteIDDestino
		   AND AM.Activo = 1
		   AND A.Activo = 1
		   AND TG.Sexo = @SexoOrigen
				-- ORDER BY A.FechaModificacion ASC
	UNION ALL 
		SELECT TOP 1 A.AnimalID,
			   A.Arete,
			   A.AreteMetalico,
			   A.FechaModificacion,
			   2 AS Origen
		  FROM AnimalMovimiento AM(NOLOCK)
		 INNER JOIN Animal A(NOLOCK) ON A.AnimalID = AM.AnimalID
	     INNER JOIN TipoGanado TG ON A.TipoGanadoID = TG.TipoGanadoID
		 WHERE CorralID = @CorralIDDestino
		   AND LoteID = @LoteIDDestino
		   AND AM.Activo = 1
		   AND A.Activo = 1
				-- ORDER BY A.FechaModificacion ASC
	) AS Animales
	ORDER BY Animales.Origen ASC, FechaModificacion ASC;
	   
	/* Reemplazar en el destino */
	UPDATE Animal
	   SET Arete = @Arete,
		   AreteMetalico = @AreteTestigo,
		   FechaModificacion = GETDATE(),
		   UsuarioModificacionID = @UsuarioCreacionID
	 WHERE AnimalID = @AnimalIDReemplazo;
	
	/* Reemplazar en el Origen */
	UPDATE Animal
	   SET Arete = @AreteReemplazo,
		   AreteMetalico = @AreteTestigoReemplazo,
		   FechaModificacion = GETDATE(),
		   UsuarioModificacionID = @UsuarioCreacionID
	 WHERE AnimalID = @AnimalID;
	
	
END									 
GO
