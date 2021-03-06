USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_ObtenerPorEntradaGanadoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoCosteo_ObtenerPorEntradaGanadoID]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_ObtenerPorEntradaGanadoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2013/11/25
-- Description: 
-- EntradaGanadoCosteo_ObtenerPorEntradaGanadoID 2011
--=============================================
CREATE PROCEDURE [dbo].[EntradaGanadoCosteo_ObtenerPorEntradaGanadoID] 
@EntradaGanadoID INT
,@Estatus BIT = 1
AS 
BEGIN
	SET NOCOUNT ON;

	DECLARE @EntradaGanadoCosteo AS TABLE (
		EntradaGanadoCosteoID INT
		,OrganizacionID INT
		,EntradaGanadoID INT
		,Activo BIT
		,Observacion VARCHAR(255)
		)

	INSERT @EntradaGanadoCosteo (
		EntradaGanadoCosteoID
		,OrganizacionID
		,EntradaGanadoID
		,Activo
		,Observacion
		)
	SELECT eg.EntradaGanadoCosteoID
		,eg.OrganizacionID
		,eg.EntradaGanadoID
		,eg.Activo
		,eg.Observacion
	FROM EntradaGanadoCosteo eg
	WHERE EntradaGanadoID = @EntradaGanadoID
		AND Activo = @Estatus

	SELECT eg.EntradaGanadoCosteoID
		,eg.OrganizacionID
		,o.Descripcion AS [Organizacion]
		,eg.EntradaGanadoID
		,eg.Activo
		,eg.Observacion
	FROM @EntradaGanadoCosteo eg
	INNER JOIN Organizacion o ON o.OrganizacionID = eg.OrganizacionID

	DECLARE @EntradaGanadoCosteoID INT

	SET @EntradaGanadoCosteoID = (
			SELECT TOP 1 EntradaGanadoCosteoID
			FROM @EntradaGanadoCosteo
			)

	SELECT ec.EntradaGanadoCalidadID
		,ec.EntradaGanadoID
		,cg.CalidadGanadoID
		,cg.Descripcion [CalidadGanado]
		,cg.Calidad
		,cg.Sexo
		,ec.Valor
		,ec.Activo
	FROM CalidadGanado cg
	LEFT JOIN EntradaGanadoCalidad ec ON ec.CalidadGanadoID = cg.CalidadGanadoID
										AND EC.EntradaGanadoID = @EntradaGanadoID
		--AND ec.EntradaGanadoCosteoID = @EntradaGanadoCosteoID
	--LEFT JOIN EntradaGanado EG ON (EC.EntradaGanadoID = EG.EntradaGanadoID)

	SELECT ed.EntradaDetalleID
		,ed.EntradaGanadoCosteoID
		,ed.TipoGanadoID
		,tg.Descripcion AS [TipoGanado] 
		,ed.Cabezas
		,ed.PesoOrigen
		,ed.PesoLlegada 
		,ed.PrecioKilo
		,ed.Importe
		,ed.ImporteOrigen AS ImporteOrigen --Columna para que no marque error el costeo, Servicio
		,ed.Activo
	FROM EntradaDetalle ed
	INNER JOIN TipoGanado tg ON tg.TipoGanadoID = ed.TipoGanadoID
	INNER JOIN @EntradaGanadoCosteo eg ON eg.EntradaGanadoCosteoID = ed.EntradaGanadoCosteoID
	WHERE ed.EntradaDetalleID > 0

	SELECT ec.EntradaGanadoCostoID
		,ec.EntradaGanadoCosteoID
		,ec.CostoID
		,co.Descripcion [Costo]
		,ec.TieneCuenta
		,ec.CuentaProvision
		,cs.Descripcion [CuentraProvisionDescripcion]
		,co.ClaveContable
		,re.RetencionID
		,re.Descripcion [RetencionDescripcion]
		,ec.ProveedorID
		,p.Descripcion AS [Proveedor]
		,p.CodigoSAP
		,ec.NumeroDocumento
		,ec.Importe
		,ec.Iva
		,ec.Retencion		
		,ec.Activo
	FROM EntradaGanadoCosto ec
	LEFT JOIN Proveedor p ON p.ProveedorID = ec.ProveedorID
	LEFT JOIN CuentaSAP cs ON ec.CuentaProvision = cs.CuentaSAP
	INNER JOIN Costo co ON ec.CostoID = co.CostoID
	LEFT JOIN Retencion re on co.RetencionID = re.RetencionID
	INNER JOIN @EntradaGanadoCosteo eg ON eg.EntradaGanadoCosteoID = ec.EntradaGanadoCosteoID
	WHERE ec.EntradaGanadoCostoID > 0

	SET NOCOUNT OFF;
END

GO
