USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chofer_crear]
GO
/****** Object:  StoredProcedure [dbo].[Chofer_crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Guardar un Chofer.
-- Chofer_Crear 'Chofer001', 1
-- =============================================
CREATE PROCEDURE [dbo].[Chofer_crear]
 @Nombre             VARCHAR(50),
 @ApellidoPaterno    VARCHAR(50),
 @ApellidoMaterno    VARCHAR(50),
 @Activo             BIT,
 @UsuarioCreacionID  INT,
 @Boletinado         BIT,
 @Observaciones		 VARCHAR(500)
AS
  BEGIN
      SET NOCOUNT ON;
      INSERT INTO Chofer(
				  Nombre,
                  ApellidoPaterno,
                  ApellidoMaterno,
                  Activo,
                  FechaCreacion,
                  UsuarioCreacionID,
				  Boletinado,
				  Observaciones)
      VALUES      
      ( 
		@Nombre,               
		@ApellidoPaterno,
		@ApellidoMaterno,
		@Activo,
		GETDATE(),       
		@UsuarioCreacionID,
		@Boletinado,
		@Observaciones)
      SELECT SCOPE_IDENTITY()
  END  

GO
