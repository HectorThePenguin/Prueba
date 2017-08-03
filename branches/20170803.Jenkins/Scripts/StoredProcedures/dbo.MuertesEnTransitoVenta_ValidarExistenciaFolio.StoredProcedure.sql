IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MuertesEnTransitoVenta_ValidarExistenciaFolio]') AND type in (N'P', N'PC'))
BEGIN
    DROP PROCEDURE MuertesEnTransitoVenta_ValidarExistenciaFolio;
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================s
-- Author     : Daniel Ricardo Benitez Carrillo
-- Create date: 2016/12/20
-- Description: Valida si existen folios registrados con muertes en transito
-- MuertesEnTransitoVenta_ValidarExistenciaFolio 1
--=============================================
CREATE PROCEDURE [dbo].[MuertesEnTransitoVenta_ValidarExistenciaFolio]
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(ec.EntradaCondicionID) Total 
	FROM EntradaGanado eg
	INNER JOIN EntradaCondicion ec ON eg.EntradaGanadoID = ec.EntradaGanadoID
	WHERE eg.OrganizacionID = @OrganizacionID and ec.Cabezas > 0 and ec.CondicionId = 3
	SET NOCOUNT OFF;
END

