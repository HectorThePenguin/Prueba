USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalida_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalida_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalida_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2013/11/25
-- Description: Obtiene una Salida por ID y Organizacion
-- InterfaceSalida_ObtenerPorID 101,87
--=============================================
CREATE PROCEDURE [dbo].[InterfaceSalida_ObtenerPorID]
@SalidaID INT
, @OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON
		CREATE TABLE #Cabecero
		(
			OrganizacionID INT
			, OrganizacionDescripcion VARCHAR(250)
			, SalidaID INT
			, OrganizacionDestinoID INT
			, OrganizacionDestinoDescripcion VARCHAR(250)
			, TipoOrganizacionID INT
			, TipoOrganizacionDescripcion VARCHAR(250)
			, TipoOrganizacionDestinoID INT
			, TipoOrganizacionDestinoDescripcion VARCHAR(250)
			, FechaSalida SMALLDATETIME
			, EsRuteo BIT
			, Cabezas INT
			, Activo BIT
			, FechaRegistro SMALLDATETIME
			, UsuarioRegistro VARCHAR(250)
		)
		CREATE TABLE #Detalle
		(
			OrganizacionID INT
			 , SalidaID INT
			 , CabezasDetalle INT
			 , Importe MONEY
			 , PrecioKG MONEY
			 , TipoGanadoID INT
			 , TipoGanadoDescripcion VARCHAR(250)
		)
		CREATE TABLE #DetalleAnimal
		(
			Arete VARCHAR(15)
			 , PesoCompra MONEY
			 , PesoOrigen MONEY
		)
		INSERT INTO #Cabecero
		SELECT InSal.OrganizacionID
			 , O.Descripcion AS OrganizacionDescripcion
			 , InSal.SalidaID
			 , InSal.OrganizacionDestinoID
			 , OS.Descripcion AS OrganizacionDestinoDescripcion
			 , TON.TipoOrganizacionID
			 , TON.Descripcion AS TipoOrganizacionDescripcion
			 , TOD.TipoOrganizacionID AS TipoOrganizacionDestinoID
			 , TOD.Descripcion AS TipoOrganizacionDestinoDescripcion
			 , InSal.FechaSalida
			 , InSal.EsRuteo
			 , InSal.Cabezas
			 , InSal.Activo
			 , InSal.FechaRegistro
			 , InSal.UsuarioRegistro
		FROM InterfaceSalida InSal
		INNER JOIN Organizacion O
			ON (InSal.OrganizacionID = O.OrganizacionID)
		INNER JOIN Organizacion OS
			ON (InSal.OrganizacionDestinoID = OS.OrganizacionID)
		INNER JOIN TipoOrganizacion TON
			ON (O.TipoOrganizacionID = TON.TipoOrganizacionID)
		INNER JOIN TipoOrganizacion TOD
			ON (OS.TipoOrganizacionID = TOD.TipoOrganizacionID)
		WHERE InSal.SalidaID = @SalidaID
			AND InSal.OrganizacionID = @OrganizacionID
		INSERT INTO #Detalle
		SELECT InSalDet.OrganizacionID
		     , InSalDet.SalidaID
			 , InSalDet.Cabezas AS CabezasDetalle
			 , InSalDet.Importe
			 , InSalDet.PrecioKG
			 , TG.TipoGanadoID
			 , TG.Descripcion AS TipoGanadoDescripcion
		FROM #Cabecero InSal
		INNER JOIN InterfaceSalidaDetalle InSalDet
			ON (InSal.SalidaID = InSalDet.SalidaID
				AND InSal.OrganizacionID = InSalDet.OrganizacionID)
		INNER JOIN TipoGanado TG
			ON (InSalDet.TipoGanadoID = TG.TipoGanadoID)
		INSERT INTO #DetalleAnimal
		SELECT ISA.Arete
			 , ISA.PesoCompra
			 , ISA.PesoOrigen
		FROM #Detalle InSalDet
		INNER JOIN InterfaceSalidaAnimal ISA
			ON (InSalDet.OrganizacionID = ISA.OrganizacionID
				AND InSalDet.SalidaID = ISA.SalidaID
				AND InSalDet.TipoGanadoID = ISA.TipoGanadoID)
		SELECT OrganizacionID
				, OrganizacionDescripcion
				, SalidaID
				, OrganizacionDestinoID
				, OrganizacionDestinoDescripcion
				, TipoOrganizacionID
				, TipoOrganizacionDescripcion
				, TipoOrganizacionDestinoID
				, TipoOrganizacionDestinoDescripcion
				, FechaSalida
				, EsRuteo
				, Cabezas
				, Activo
				, FechaRegistro
				, UsuarioRegistro
		FROM #Cabecero
		SELECT OrganizacionID
			 , SalidaID
			 , CabezasDetalle
			 , Importe
			 , PrecioKG
			 , TipoGanadoID
			 , TipoGanadoDescripcion
		FROM #Detalle
		SELECT Arete
			 , PesoCompra
			 , PesoOrigen
		FROM #DetalleAnimal
		DROP TABLE #Cabecero
		DROP TABLE #Detalle
		DROP TABLE #DetalleAnimal
	SET NOCOUNT OFF
END

GO
