USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerPorSalidaOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerPorSalidaOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaAnimal_ObtenerPorSalidaOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/04/02
-- Description: 
-- InterfaceSalidaAnimal_ObtenerPorSalidaOrganizacion 7, 11
--=============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaAnimal_ObtenerPorSalidaOrganizacion] 
@OrganizacionID INT
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
	DROP TABLE #INTERFACESALIDA
	DROP TABLE #INTERFACESALIDADETALLE
	DROP TABLE #INTERFACESALIDANIMAL
	SET NOCOUNT OFF;
END

GO
