USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ObtenerClienteByID]    Script Date: 13/04/2016 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoTransito_ObtenerClienteByID]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_ObtenerClienteByID]    Script Date: 13/04/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Torres Lugo Manuel
-- Create date: 13/04/2016
-- Description: 
-- SpName     : SalidaGanadoTransito_ObtenerClienteByID
--======================================================
CREATE PROCEDURE [dbo].[SalidaGanadoTransito_ObtenerClienteByID]
@ClienteID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ClienteID,
		CodigoSAP,
		Descripcion
	FROM Cliente
	WHERE ClienteID = @ClienteID
	AND Activo = 1
	SET NOCOUNT OFF;
END