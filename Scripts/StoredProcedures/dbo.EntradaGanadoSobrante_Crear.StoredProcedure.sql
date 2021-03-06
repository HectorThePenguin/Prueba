USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoSobrante_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoSobrante_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoSobrante_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cesar Valdez
-- Create date: 01-12-2014
-- Description:	Crea una entrada de ganado sobrante
-- EXEC  [dbo].[EntradaGanadoSobrante_Crear] 1, 2, 1, 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanadoSobrante_Crear]	
	@EntradaGanadoID INT,
	@AnimalID INT, 
	@Importe DECIMAL(18,2),
	@Costeado BIT,
	@UsuarioCreacionID INT
AS
BEGIN	
	SET NOCOUNT ON;
	INSERT INTO EntradaGanadoSobrante (
				EntradaGanadoID, 
				AnimalID, 
				Importe, 
				Costeado, 
				Activo, 
				FechaCreacion, 
				UsuarioCreacionID,
				FechaModificacion, 
				UsuarioModificacionID) 
	VALUES (@EntradaGanadoID, 
	        @AnimalID, 
			@Importe, 
			@Costeado, 
			1, 
			GETDATE(), 
			@UsuarioCreacionID,
			NULL,
			NULL)
	SELECT SCOPE_IDENTITY() AS [EntradaGanadoSobranteID];
END

GO
