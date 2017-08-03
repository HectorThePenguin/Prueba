IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente_ObtenerTotalClientesActivos]') AND type in (N'P', N'PC'))
BEGIN
    DROP PROCEDURE Cliente_ObtenerTotalClientesActivos;
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================s
-- Author     : Daniel Ricardo Benitez Carrillo
-- Create date: 2016/12/20
-- Description: Obtiene el total de clientes activos
-- Cliente_ObtenerTotalClientesActivos
--=============================================
CREATE PROCEDURE [dbo].[Cliente_ObtenerTotalClientesActivos]
AS
BEGIN
	SET NOCOUNT ON;
	 SELECT count(cl.ClienteID) Total
	 FROM Cliente cl WHERE Activo = 1
	SET NOCOUNT OFF;
END

