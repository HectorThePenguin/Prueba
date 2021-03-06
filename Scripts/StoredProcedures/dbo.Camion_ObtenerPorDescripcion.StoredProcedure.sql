USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Camion_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Camion_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Camion_ObtenerPorDescripcion '176-EC3'
--======================================================
CREATE PROCEDURE [dbo].[Camion_ObtenerPorDescripcion]
@Descripcion VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		c.CamionID,
		c.ProveedorID,
		p.Descripcion as [Proveedor],
		p.CodigoSAP,
		c.PlacaCamion,
		c.Activo,
		c.NumEconomico
	FROM Camion c
	INNER JOIN Proveedor p on p.ProveedorID = c.ProveedorID
	WHERE PlacaCamion = @Descripcion
	SET NOCOUNT OFF;
END

GO
