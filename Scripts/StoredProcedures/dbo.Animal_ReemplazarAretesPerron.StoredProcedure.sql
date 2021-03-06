USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ReemplazarAretesPerron]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ReemplazarAretesPerron]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ReemplazarAretesPerron]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/07/22
-- Description: SP para Obtener Si el animal es de Carga Inicial
-- Origen     : APInterfaces
-- EXEC Animal_ReemplazarAretesPerron 1
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ReemplazarAretesPerron]
	@AnimalID BIGINT,
	@AnimalIDDestino BIGINT,
  /*@CorralIDDestino INT,
	@LoteIDDestino INT,  */
	@UsuarioCreacionID INT
AS
BEGIN
	DECLARE @AnimalIDReemplazo BIGINT;
	DECLARE @AreteReemplazo VARCHAR(15);
	DECLARE @AreteTestigoReemplazo VARCHAR(15);
	DECLARE @CorralIDOrigen INT;
	DECLARE @LoteIDOrigen INT;
	DECLARE @AreteOrigen VARCHAR(15);
	DECLARE @AreteTestigoOrigen VARCHAR(15);
	/* Tomar el corral del animal a reemplazar*/
	SELECT TOP 1
		   @CorralIDOrigen = AM.CorralID,
		   @LoteIDOrigen = AM.LoteID,
		   @AreteTestigoOrigen = A.AreteMetalico,
		   @AreteOrigen = A.Arete
	  FROM AnimalMovimiento AM(NOLOCK)
	  INNER JOIN Animal A(NOLOCK) ON A.AnimalID = AM.AnimalID
	 WHERE A.AnimalID = @AnimalID
	   AND AM.Activo = 1;
	/* Tomar el primer animal del corral destino a remplazar */
	SELECT TOP 1
		   @AnimalIDReemplazo = A.AnimalID,
		   @AreteReemplazo = A.Arete,
		   @AreteTestigoReemplazo = A.AreteMetalico
	  FROM AnimalMovimiento AM(NOLOCK)
	 INNER JOIN Animal A(NOLOCK) ON A.AnimalID = AM.AnimalID
	 WHERE A.AnimalID = @AnimalIDDestino
	     /*  CorralID = @CorralIDDestino
	   AND LoteID = @LoteIDDestino*/
	   AND AM.Activo = 1
	   AND A.Activo = 1;
	 -- ORDER BY A.FechaModificacion ASC;
	/* Reemplazar en el destino */
	UPDATE Animal
	   SET Arete = @AreteOrigen,
		   AreteMetalico = @AreteTestigoOrigen,
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
