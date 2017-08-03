IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'ConfiguracionCredito_Crear' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[ConfiguracionCredito_Crear]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 23/06/2016    
-- Description:       
-- SpName     : ConfiguracionCredito_Crear '', 1, 1, 1, 1
--======================================================      
CREATE PROCEDURE [dbo].[ConfiguracionCredito_Crear]     
@XML XML, 
@TipoCreditoID INT,
@PlazoCreditoID INT,
@Activo BIT,
@UsuarioCreacionID INT
AS  
BEGIN      
SET NOCOUNT ON

	DECLARE @ConfiguracionCreditoID INT
	
	SELECT   
		Mes = T.item.value('./Mes[1]', 'INT'),  
		Porcentaje   = T.item.value('./Porcentaje[1]','INT')  
	INTO #ConfiguracionCredito
	FROM  @XML.nodes('ROOT/ConfiguracionCredito') AS T(item)

	INSERT INTO ConfiguracionCredito(TipoCreditoID,PlazoCreditoID,Activo,FechaCreacion,UsuarioCreacionID)
	VALUES(@TipoCreditoID,@PlazoCreditoID,@Activo,GETDATE(),@UsuarioCreacionID) 
	
	SET @ConfiguracionCreditoID = @@IDENTITY  

	INSERT INTO ConfiguracionCreditoRetenciones(ConfiguracionCreditoID,NumeroMes,PorcentajeRetencion,FechaCreacion,UsuarioCreacionID)
	SELECT @ConfiguracionCreditoID, Mes, Porcentaje, GETDATE(), @UsuarioCreacionID
	FROM #ConfiguracionCredito
	
	DROP TABLE  #ConfiguracionCredito
	
	SELECT @ConfiguracionCreditoID AS ID	

SET NOCOUNT OFF    
END