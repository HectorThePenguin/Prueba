USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReemplazarAreteMismoCorral]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReemplazarAreteMismoCorral]
GO
/****** Object:  StoredProcedure [dbo].[ReemplazarAreteMismoCorral]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/10/01
-- Description: SP para Remplazar un arete nuevo sobre un animal de un corral en especifico
-- Origen     : APInterfaces
-- EXEC ReemplazarAreteMismoCorral 1
-- =============================================
CREATE PROCEDURE [dbo].[ReemplazarAreteMismoCorral]
	@Arete VARCHAR(15),
	@AreteTestigo VARCHAR(15),
	@CorralID INT,
	@LoteID INT,
	@UsuarioModificacionID INT
AS
BEGIN
	
	DECLARE @AnimalIDReemplazo BIGINT;

	/* Tomar el primer animal del corral a remplazar */
	SELECT TOP 1
		   @AnimalIDReemplazo = A.AnimalID
	  FROM AnimalMovimiento AM(NOLOCK)
	 INNER JOIN Animal A(NOLOCK) ON A.AnimalID = AM.AnimalID
	 WHERE CorralID = @CorralID
	   AND LoteID = @LoteID
	   AND AM.Activo = 1
	   AND A.Activo = 1
	 ORDER BY A.FechaModificacion ASC, AM.FechaMovimiento ASC, AM.AnimalMovimientoID ASC;
	 
	/* Reemplazar en el animal destino */
	UPDATE Animal
	   SET Arete = @Arete,
		   AreteMetalico = @AreteTestigo,
		   FechaModificacion = GETDATE(),
		   UsuarioModificacionID = @UsuarioModificacionID
	 WHERE AnimalID = @AnimalIDReemplazo;
	
END									 
GO
