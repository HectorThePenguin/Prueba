USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerClientePorCodigoSAPActivos]    Script Date: 26-04-2016 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cliente_ObtenerClientePorCodigoSAPActivos]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerClientePorCodigoSAPActivos]    Script Date: 26-04-2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:			Torres Lugo Manuel
-- Create date: 26-04-2016
-- Description:	Obtiene un Clientes  por codigo SAP
-- exec Cliente_ObtenerClientePorCodigoSAPActivos
-- =============================================
CREATE PROCEDURE [dbo].[Cliente_ObtenerClientePorCodigoSAPActivos]
@CodigoSAP VARCHAR(10)
AS
BEGIN	
	SET NOCOUNT ON
		SELECT 
			ClienteID, 
			CodigoSAP, 
			Descripcion, 
			Activo 
	FROM Cliente 
	WHERE CodigoSAP = @CodigoSAP 
	AND Activo = 1
	SET NOCOUNT OFF;
END