USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cliente_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Cliente_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 03/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Cliente_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Cliente_ObtenerPorDescripcion]
@Descripcion varchar(150)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ClienteID,
		CodigoSAP,
		Descripcion,
		Activo
	FROM Cliente
	WHERE Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
