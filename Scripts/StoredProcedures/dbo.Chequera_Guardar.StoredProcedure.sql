USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chequera_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Sergio Alberto Gamez Gomez
-- Fecha: 17-06-2015
-- Descripcion:	Guardar una chequera
-- Chequera_Guardar ''
-- =============================================
CREATE PROCEDURE [dbo].[Chequera_Guardar]   
	@XmlChequera XML   
AS  
BEGIN  
	BEGIN TRY  
		BEGIN TRAN 
		DECLARE @ChequeraId AS INT    
		DECLARE @EstatusId AS INT  
		DECLARE @CentroAcopioId AS INT  
		DECLARE @UsuarioId AS INT
		SELECT  
			ChequeraId = t.item.value('./ChequeraId[1]', 'INT'),  
			NumeroChequera = t.item.value('./NumeroChequera[1]', 'VARCHAR(50)'),  
			ChequeIDInicial = t.item.value('./ChequeIDInicial[1]', 'INT'),  
			ChequeIDFinal = t.item.value('./ChequeIDFinal[1]', 'INT'),  
			OrganizacionId = t.item.value('./OrganizacionId[1]', 'INT'),  
			BancoId =  t.item.value('./BancoId[1]', 'INT'),  
			Activo = t.item.value('./Activo[1]', 'INT'),  
			UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')   
		INTO #Chequera  
		FROM @XmlChequera.nodes('ROOT/Chequera') AS T(item)  
		SELECT @ChequeraId = ChequeraId, @EstatusId = Activo, @CentroAcopioId = OrganizacionId, @UsuarioId = UsuarioCreacionID FROM #Chequera
		IF @ChequeraId = 0 
		BEGIN
			SELECT @ChequeraId = ISNULL(MAX(ChequeraId),0) + 1 FROM [Sukarne].[dbo].[Chequera] (NOLOCK) WHERE OrganizacionId = @CentroAcopioId
			INSERT INTO [Sukarne].[dbo].[Chequera] (ChequeraId,NumeroChequera,ChequeraIDInicial,ChequeraIDFinal,OrganizacionId,BancoId,Activo,FechaCreacion,UsuarioCreacionID,FechaModificacion,UsuarioModificacionID)  
			SELECT @ChequeraId,@ChequeraId,ChequeIDInicial,ChequeIDFinal,OrganizacionId,BancoId,Activo,GETDATE(),UsuarioCreacionID,GETDATE(),UsuarioCreacionID FROM #Chequera 
		END
		ELSE
		BEGIN
			UPDATE [Sukarne].[dbo].[Chequera] SET Activo = @EstatusId, UsuarioModificacionID = @UsuarioId, FechaModificacion = GETDATE() WHERE ChequeraId = @ChequeraId AND OrganizacionId = @CentroAcopioId
		END
		SELECT @ChequeraId AS Resultado  
		COMMIT TRAN  
	END TRY    
	BEGIN CATCH    
		ROLLBACK TRAN  
		SELECT 0 AS Resultado  
	END CATCH;  
END
GO