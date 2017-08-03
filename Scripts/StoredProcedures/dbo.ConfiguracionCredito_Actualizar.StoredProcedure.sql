IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'ConfiguracionCredito_Actualizar' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[ConfiguracionCredito_Actualizar]
END 
GO
--======================================================      
-- Author     : Sergio Alberto Gamez Gomez    
-- Create date: 23/06/2016    
-- Description:       
-- SpName     : ConfiguracionCredito_Actualizar      
--======================================================      
CREATE PROCEDURE [dbo].[ConfiguracionCredito_Actualizar]     
@XML XML, 
@TipoCreditoID INT,
@PlazoCreditoID INT,
@Activo BIT,
@UsuarioCreacionID INT,
@ConfiguracionCreditoID INT
AS  
BEGIN      
SET NOCOUNT ON
	
	SELECT   
		Mes = T.item.value('./Mes[1]', 'INT'),  
		Porcentaje   = T.item.value('./Porcentaje[1]','INT')  
	INTO #ConfiguracionCredito
	FROM  @XML.nodes('ROOT/ConfiguracionCredito') AS T(item)

	UPDATE ConfiguracionCredito 
	SET TipoCreditoID = @TipoCreditoID,
		PlazoCreditoID = @PlazoCreditoID,
		Activo = @Activo,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID =	@UsuarioCreacionID
	WHERE ConfiguracionCreditoID = @ConfiguracionCreditoID

	DELETE FROM ConfiguracionCreditoRetenciones WHERE ConfiguracionCreditoID = @ConfiguracionCreditoID
	
	INSERT INTO ConfiguracionCreditoRetenciones(ConfiguracionCreditoID,NumeroMes,PorcentajeRetencion,FechaCreacion,UsuarioCreacionID)
	SELECT @ConfiguracionCreditoID, Mes, Porcentaje, GETDATE(), @UsuarioCreacionID
	FROM #ConfiguracionCredito
	
	DROP TABLE #ConfiguracionCredito	
	
	SELECT @ConfiguracionCreditoID AS ID	

SET NOCOUNT OFF    
END