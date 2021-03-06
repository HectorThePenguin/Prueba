USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteBitacora_FechaLLegadaEntradaGanaderaPesaje]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteBitacora_FechaLLegadaEntradaGanaderaPesaje]
GO
/****** Object:  StoredProcedure [dbo].[ReporteBitacora_FechaLLegadaEntradaGanaderaPesaje]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Alejandro Quiroz
-- Create date: 29/06/2015
-- Description: Sp para obtener la bitacora de vigilancia por los campos de fecha: Llegada, EntradaGanadera y Pesaje.
--              El TipoFecha esta a la espera de 1.-Fecha Llegada, 2.-Fecha Entrada y 3.-Fecha Pesaje
-- exec ReporteBitacora_FechaLLegadaEntradaGanaderaPesaje 1, 1, '2015-05-31', '2015-05-31' 

--=============================================
CREATE PROCEDURE [dbo].[ReporteBitacora_FechaLLegadaEntradaGanaderaPesaje]
@OrganizacionID INT,
@TipoFecha INT,
@FechaInicial DATE,
@FechaFinal DATE
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @FechaDefault AS DATE
	SET @FechaDefault = CAST('1900-01-01' AS DATE)
	
	SELECT
		EP.Folio,
		RV.Transportista AS Empresa,
		RV.Chofer,
		TC.Descripcion AS AccesoA,
		RV.Marca,
		RV.Camion AS Placa,
		RV.Color,
		P.Descripcion AS Producto,
		dbo.ObtenerPorcentajeHumedadPorEntradaProductoID(EP.EntradaProductoID) AS PorcentajeHumedad,
		ISNULL(RV.FechaLLegada, @FechaDefault) AS Llegada,
		ISNULL(EP.Fecha, @FechaDefault) AS EntradaGanadera,
		ISNULL(EP.FechaPesaje, @FechaDefault) AS FechaPesaje,
		ISNULL(EP.FechaDestara, @FechaDefault) AS FechaDestara,
		ISNULL(EP.FechaInicioDescarga, @FechaDefault) AS FechaInicioDescarga,
		ISNULL(EP.FechaFinDescarga, @FechaDefault) AS FechaFinDescarga,
		ISNULL(PTE.Tiempo, CAST('0:0' AS TIME)) AS TiempoEstandar
	FROM RegistroVigilancia RV
	INNER JOIN Producto P ON RV.ProductoID = P.ProductoID
	INNER JOIN EntradaProducto EP ON EP.RegistroVigilanciaID = RV.RegistroVigilanciaID
	INNER JOIN TipoContrato TC ON TC.TipoContratoID = EP.TipoContratoID
  LEFT JOIN ProductoTiempoEstandar PTE ON P.ProductoID = PTE.ProductoID
	WHERE RV.OrganizacionID = @OrganizacionID
	AND @FechaInicial <= CAST (CASE
		WHEN @TipoFecha = 1 THEN RV.FechaLLegada
		WHEN @TipoFecha = 2 THEN EP.Fecha
		WHEN @TipoFecha = 3 THEN EP.FechaPesaje END AS DATE)
	AND @FechaFinal >= CAST(CASE
		WHEN @TipoFecha = 1 THEN RV.FechaLLegada
		WHEN @TipoFecha = 2 THEN EP.Fecha
		WHEN @TipoFecha = 3 THEN EP.FechaPesaje END AS DATE)

	SET NOCOUNT OFF;
END

GO
