IF EXISTS (SELECT 1 FROM sysobjects WHERE name =  'CreditoSOFOM_ObtenerPorID' AND type = 'P')
BEGIN
	DROP PROCEDURE [dbo].[CreditoSOFOM_ObtenerPorID]
END 
GO
CREATE PROCEDURE [dbo].[CreditoSOFOM_ObtenerPorID] 
@CreditoID INT  
AS  
BEGIN  
SET NOCOUNT ON
	 
	SELECT   
		CS.CreditoID,
		Saldo = CAST(CS.Saldo AS NUMERIC(18,2)),
		CS.TipoCreditoID,
		TipoCredito = ISNULL(TC.Descripcion,'')
	FROM CreditoSOFOM CS
	INNER JOIN TipoCredito TC
		ON TC.TipoCreditoID = CS.TipoCreditoID AND TC.Activo = 1
	WHERE CS.CreditoID = @CreditoID

SET NOCOUNT OFF
END