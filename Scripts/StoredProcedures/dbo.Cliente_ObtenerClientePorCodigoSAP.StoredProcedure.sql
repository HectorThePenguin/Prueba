USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerClientePorCodigoSAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cliente_ObtenerClientePorCodigoSAP]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerClientePorCodigoSAP]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ramses Santos
-- Create date: 04-03-2014
-- Description:	Obtiene un listado de Clientes 
-- exec Cliente_ObtenerClientePorCodigoSAP '1234'
-- =============================================
CREATE PROCEDURE [dbo].[Cliente_ObtenerClientePorCodigoSAP]
	@CodigoSAP VARCHAR(10)
AS
BEGIN	
	SET NOCOUNT ON
	SELECT ClienteID, CodigoSAP, Descripcion, Activo FROM Cliente WHERE CodigoSAP = @CodigoSAP  
END

GO
