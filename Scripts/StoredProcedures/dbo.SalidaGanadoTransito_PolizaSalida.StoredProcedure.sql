USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_PolizaSalida]    Script Date: 23/04/2016 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanadoTransito_PolizaSalida]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanadoTransito_PolizaSalida]    Script Date: 23/04/2016 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Manuel Lugo Torres
-- Create date: 23/04/2016
-- Description: SP par obtener los datos faltantes para la generacion de la poliza.
-- SpName     : dbo.SalidaGanadoTransito_PolizaSalida
-- --======================================================
CREATE PROCEDURE [dbo].[SalidaGanadoTransito_PolizaSalida]
@Folio INT,
@Muerte BIT
AS
BEGIN
 DECLARE @DescripcionTipoPoliza VARCHAR(100)

	IF (@Muerte = 1)
		BEGIN
			SET @DescripcionTipoPoliza = 'Salida Por Muerte Transito'
		END
	ELSE
		BEGIN
			SET @DescripcionTipoPoliza = 'Salida Por Venta Transito'
		END
			
  SELECT  
		TP.PostFijoRef3,
		Org.Sociedad,
		TP.Descripcion,
		PO.Valor,
		Org.Division
	FROM SalidaGanadoTransito AS SGT
	INNER JOIN TipoPoliza AS TP ON TP.Descripcion = @DescripcionTipoPoliza
	INNER JOIN Organizacion AS Org 	ON Org.OrganizacionID = SGT.OrganizacionID
	INNER JOIN SalidaGanadoTransitoDetalle AS SGD 	ON SGD.SalidaGanadoTransitoID = SGT.SalidaGanadoTransitoID
	INNER JOIN Parametro AS P 	ON P.Clave = 'CTACENTROCOSTOENG'
	INNER JOIN ParametroOrganizacion AS PO ON PO.ParametroID = P.ParametroID AND PO.OrganizacionID = Org.OrganizacionID
	WHERE SGT.Muerte = @Muerte
	AND SGT.Folio = @Folio
	GROUP BY SGT.Folio, SGT.FechaCreacion, TP.PostFijoRef3, Org.Sociedad, TP.Descripcion, 
			 PO.Valor, SGT.NumCabezas, SGT.Kilos, Org.Division
END