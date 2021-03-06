USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Jaula_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 07/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Jaula_ObtenerPorDescripcion '215-UM4'
--======================================================
CREATE PROCEDURE [dbo].[Jaula_ObtenerPorDescripcion]
@Descripcion varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		j.JaulaID,
		j.ProveedorID,
		p.Descripcion as [Proveedor],
		p.CodigoSAP,
		j.PlacaJaula,
		j.Capacidad,
		j.Secciones,
		j.NumEconomico,
		j.Activo
	FROM Jaula j
	INNER JOIN Proveedor p on p.ProveedorID = j.ProveedorID
	WHERE PlacaJaula = @Descripcion
	SET NOCOUNT OFF;
END

GO
