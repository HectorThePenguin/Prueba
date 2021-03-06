USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ClienteCreditoExcel_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ClienteCreditoExcel_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[ClienteCreditoExcel_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--============================================
--Author      : Ruben Guzman Meza
--Create date : 2015/05/25
--Descripcion : ClienteCreditoExcel_ObtenerPorID
--============================================
CREATE PROCEDURE [dbo].[ClienteCreditoExcel_ObtenerPorID] 
(
@CreditoID INT
)
AS
BEGIN 
	SET  NOCOUNT ON;
	
	SELECT C.CreditoID, C.Nombre
	FROM CreditoSOFOM AS C(NOLOCK)
	WHERE  C.CreditoID = @CreditoID AND Saldo > 0
	
	SET NOCOUNT OFF;
END 
GO