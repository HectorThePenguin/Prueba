USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnalisisGrasa_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnalisisGrasa_Crear]
GO
/****** Object:  StoredProcedure [dbo].[AnalisisGrasa_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Alejandro Quiroz M.	
-- Create date: 24/07/2014
-- Description: Crea un nuevo analisis de Grasa
-- AnalisisGrasa_Crear 1,1,'Grasa',1,2,3,4,'obs',1,1 
-- SELECT * FROM AnalisisGrasa
-- DROP PROCEDURE AnalisisGrasa_Crear 
--=============================================
CREATE PROCEDURE [dbo].[AnalisisGrasa_Crear]
	@OrganizacionID int,
	@EntradaProductoID int,
	@TipoMuestra varchar(10),
	@PesoMuestra decimal(15,2),
	@PesoTuboSeco decimal(15,2),
	@PesoTuboMuestra decimal(15,2),
	@Impurezas decimal(15,2),
	@Observaciones varchar(255),
	@Activo bit,
	@UsuarioCreacionID int
AS
BEGIN
INSERT INTO [dbo].[AnalisisGrasa]
           ([OrganizacionID]
           ,[EntradaProductoID]
           ,[TipoMuestra]
           ,[PesoMuestra]
           ,[PesoTuboSeco]
           ,[PesoTuboMuestra]
           ,[Impurezas]
           ,[Observaciones]
           ,[Activo]
           ,[FechaCreacion]
           ,[UsuarioCreacionID])
     VALUES
          (@OrganizacionID
		  ,@EntradaProductoID
		  ,@TipoMuestra
		  ,@PesoMuestra
		  ,@PesoTuboSeco
		  ,@PesoTuboMuestra
		  ,@Impurezas
		  ,@Observaciones
		  ,@Activo
		  ,GETDATE()
		  ,@UsuarioCreacionID)
END

GO
