USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalida_ObtenerPorSalidaOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalida_ObtenerPorSalidaOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalida_ObtenerPorSalidaOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/12/04
-- Description: 
-- InterfaceSalida_ObtenerPorSalidaOrganizacion 5, 121231
--=============================================
CREATE PROCEDURE [dbo].[InterfaceSalida_ObtenerPorSalidaOrganizacion] @OrganizacionID INT
	,@SalidaID INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #INTERFACESALIDA (
		OrganizacionID INT		
		,SalidaID INT
		,OrganizacionDestinoID INT
		,FechaSalida SMALLDATETIME
		,EsRuteo BIT
		,Cabezas INT
		,Activo BIT
		,FechaRegistro DATETIME
		,UsuarioRegistro VARCHAR(50)
		)
	CREATE TABLE #INTERFACESALIDADETALLE (
		OrganizacionID INT
		,SalidaID INT
		,TipoGanadoID INT
		,TipoGanado VARCHAR(50)
		,Cabezas INT
		,PrecioKg DECIMAL(19, 2)
		,Importe DECIMAL(19, 2)
		,FechaRegistro SMALLDATETIME
		,UsuarioRegistro VARCHAR(50)
		)
	CREATE TABLE #INTERFACESALIDANIMAL (
		OrganizacionID INT
		,SalidaID INT
		,Arete VARCHAR(15)
		,FechaCompra SMALLDATETIME
		,PesoCompra DECIMAL
		,TipoGanadoID INT
		,TipoGanado VARCHAR(50)
		,PesoOrigen DECIMAL
		,FechaRegistro SMALLDATETIME
		,UsuarioRegistro VARCHAR(50)
		)
	CREATE TABLE #INTERFACESALIDCOSTO (
		OrganizacionID INT
		,SalidaID INT
		,Arete VARCHAR(15)
		,FechaCompra SMALLDATETIME
		,CostoID INT
		,Costo VARCHAR(50)
		,ClaveContableCosto CHAR(3)
		,RetencionID INT
		,Retencion VARCHAR(50)
		,Importe MONEY
		,FechaRegistro SMALLDATETIME
		,UsuarioRegistro VARCHAR(50)
		)
	INSERT INTO #INTERFACESALIDA
	SELECT 		
		isa.OrganizacionID
		,SalidaID
		,OrganizacionDestinoID
		,FechaSalida
		,EsRuteo
		,Cabezas
		,Activo
		,FechaRegistro
		,UsuarioRegistro
	FROM InterfaceSalida isa	
	WHERE isa.OrganizacionID = @OrganizacionID
		AND isa.SalidaID = @SalidaID
	INSERT INTO #INTERFACESALIDADETALLE
	SELECT isd.OrganizacionID
		,isd.SalidaID
		,isd.TipoGanadoID
		,tg.Descripcion [TipoGanado]
		,isd.Cabezas
		,isd.PrecioKg
		,isd.Importe
		,isd.FechaRegistro
		,isd.UsuarioRegistro
	FROM InterfaceSalidaDetalle isd
	INNER JOIN #INTERFACESALIDA isal ON isd.OrganizacionID = isal.OrganizacionID
		AND isd.SalidaID = isal.SalidaID
	INNER JOIN TipoGanado tg ON tg.TipoGanadoID = isd.TipoGanadoID
	INSERT INTO #INTERFACESALIDANIMAL
	SELECT isa.OrganizacionID
		,isa.SalidaID
		,isa.Arete
		,isa.FechaCompra
		,isa.PesoCompra
		,isa.TipoGanadoID
		,isd.TipoGanado
		,isa.PesoOrigen
		,isa.FechaRegistro
		,isa.UsuarioRegistro
	FROM InterfaceSalidaAnimal isa
	INNER JOIN #INTERFACESALIDADETALLE isd ON isa.OrganizacionID = isd.OrganizacionID
		AND isa.SalidaID = isd.SalidaID
		AND isa.TipoGanadoID = isd.TipoGanadoID
	INSERT INTO #INTERFACESALIDCOSTO
	SELECT isc.OrganizacionID
		,isc.SalidaID
		,isc.Arete
		,isc.FechaCompra
		,co.CostoID
		,co.Descripcion [Costo]
		,co.ClaveContable
		,re.RetencionID
		,re.Descripcion [Retencion]
		,isc.Importe
		,isc.FechaRegistro
		,isc.UsuarioRegistro
	FROM InterfaceSalidaCosto isc	
	INNER JOIN Costo co ON isc.CostoID = co.CostoID
	LEFT JOIN Retencion re ON co.RetencionID = re.RetencionID
	INNER JOIN #INTERFACESALIDANIMAL isa ON isa.OrganizacionID = isc.OrganizacionID
		AND isa.SalidaID = isc.SalidaID
		AND isa.Arete = isc.Arete
		AND isa.FechaCompra = isc.FechaCompra
	SELECT OrganizacionID
		,SalidaID
		,OrganizacionDestinoID
		,FechaSalida
		,EsRuteo
		,Cabezas
		,Activo
		,FechaRegistro
		,UsuarioRegistro
	FROM #INTERFACESALIDA
	SELECT OrganizacionID
		,SalidaID
		,TipoGanadoID
		,TipoGanado
		,Cabezas
		,PrecioKg
		,Importe
		,FechaRegistro
		,UsuarioRegistro
	FROM #INTERFACESALIDADETALLE
	SELECT OrganizacionID
		,SalidaID
		,Arete
		,FechaCompra
		,PesoCompra
		,TipoGanadoID
		,TipoGanado
		,PesoOrigen
		,FechaRegistro
		,UsuarioRegistro
	FROM #INTERFACESALIDANIMAL
	SELECT OrganizacionID
		,SalidaID
		,Arete
		,FechaCompra
		,CostoID
		,Costo
		,ClaveContableCosto
		,RetencionID
		,Retencion
		,Importe
		,FechaRegistro
		,UsuarioRegistro
	FROM #INTERFACESALIDCOSTO
	SET NOCOUNT OFF;
END

GO
