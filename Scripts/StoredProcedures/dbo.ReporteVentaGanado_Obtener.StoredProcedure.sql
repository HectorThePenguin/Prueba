USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteVentaGanado_Obtener]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteVentaGanado_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[ReporteVentaGanado_Obtener]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 25/04/2014
-- Description: Obtiene los datos para reporte de Venta de ganado
-- ReporteVentaGanado_Obtener 1, '20150222' ,'20150301'
-- =============================================
CREATE PROCEDURE [dbo].[ReporteVentaGanado_Obtener]
@OrganizacionID INT
, @FechaInicial DATE
, @FechaFinal DATE
AS
BEGIN
	SET NOCOUNT ON
		DECLARE @SalidaPorVenta INT
		,   @TipoCorralProduccion INT
		,   @GrupoCorralProduccion INT
		,   @EntradaEnfermeria INT
		,   @EntradaSalidaEnfermeria INT
		,   @TipoTratamientoCorte TINYINT, @TipoTratamientoEnfermeria TINYINT
		SELECT @SalidaPorVenta = 11
			 , @TipoCorralProduccion = 2
			 , @EntradaEnfermeria = 7
			 , @EntradaSalidaEnfermeria = 9
			 , @GrupoCorralProduccion = 2
			 , @TipoTratamientoCorte = 1
			 , @TipoTratamientoEnfermeria = 4
		CREATE TABLE #tMovimientosSalida
		(
			AnimalMovimientoID	BIGINT
			, AnimalID			BIGINT
			, FechaMovimiento	SMALLDATETIME
			, CorralID			INT
			, Corral			VARCHAR(100)
		)
		CREATE TABLE #tAnimales
		(
			AnimalID				BIGINT
			, Arete					VARCHAR(50)
			, FolioEntrada			BIGINT
			, TipoGanadoID			INT
			, OrganizacionIDEntrada	INT
			, TipoGanado			VARCHAR(50)
			, Sexo					CHAR(1)
		)
		CREATE TABLE #tFolios
		(
			EntradaGanadoID			INT
			, FolioEntrada			INT
			, FechaEntrada			SMALLDATETIME
			, OrganizacionOrigenID	INT
			, TipoOrganizacionID	INT
			, Organizacion			VARCHAR(100)
			, AnimalID				INT
		)
		INSERT INTO #tMovimientosSalida
		SELECT MAX(AM.AnimalMovimientoID) AS AnimalMovimientoID
			,  AM.AnimalID
			,  MAX(AM.FechaMovimiento) AS FechaMovimiento
			,  C.CorralID
			,  C.Codigo
		FROM AnimalMovimiento AM
		INNER JOIN Corral C
			ON (AM.CorralID = C.CorralID)
		WHERE CAST(FechaMovimiento AS DATE) BETWEEN @FechaInicial AND @FechaFinal
			AND TipoMovimientoID = @SalidaPorVenta
			AND AM.OrganizacionID = @OrganizacionID
		GROUP BY AM.AnimalID
			,    C.CorralID
			,    C.Codigo
		UNION
		SELECT MAX(AM.AnimalMovimientoID) AS AnimalMovimientoID
			,  AM.AnimalID
			,  MAX(AM.FechaMovimiento) AS FechaMovimiento
			,  C.CorralID
			,  C.Codigo
		FROM AnimalMovimientoHistorico AM
		INNER JOIN Corral C
			ON (AM.CorralID = C.CorralID)
		WHERE CAST(FechaMovimiento AS DATE) BETWEEN @FechaInicial AND @FechaFinal
			AND TipoMovimientoID = @SalidaPorVenta
			AND AM.OrganizacionID = @OrganizacionID
		GROUP BY AM.AnimalID
			,    C.CorralID
			,    C.Codigo
		SELECT MS.AnimalMovimientoID
			,  MS.AnimalID
			,  MS.FechaMovimiento
			,  MS.Corral
			,  E.Descripcion AS Enfermeria
		FROM #tMovimientosSalida MS
		INNER JOIN EnfermeriaCorral EC
			ON (MS.CorralID = EC.CorralID)
		INNER JOIN Enfermeria E
			ON (EC.EnfermeriaID = E.EnfermeriaID
				AND E.OrganizacionID = @OrganizacionID)
		SELECT MAX(AM.AnimalMovimientoID) AS AnimalMovimientoID
			,  C.Codigo AS CorralProduccion
			,  AM.AnimalID
			,  MAX(AM.FechaMovimiento) AS FechaMovimiento
		FROM AnimalMovimiento AM
		INNER JOIN Corral C
			ON (AM.CorralID = C.CorralID
				AND AM.OrganizacionID = @OrganizacionID)
		INNER JOIN TipoCorral TC 
			ON (C.TipoCorralID = TC.TipoCorralID
				AND TC.GrupoCorralID = @GrupoCorralProduccion)
		INNER JOIN #tMovimientosSalida MCE
			ON (AM.AnimalID = MCE.AnimalID
				AND AM.FechaMovimiento <= MCE.FechaMovimiento)
		GROUP BY AM.CorralID
			,	 C.Codigo
			,	 AM.AnimalID
			,    AM.FechaMovimiento
		ORDER BY AM.FechaMovimiento DESC
		INSERT INTO #tAnimales
		SELECT A.AnimalID
			,  A.Arete
			,  A.FolioEntrada
			,  A.TipoGanadoID
			,  A.OrganizacionIDEntrada
			,  TG.Descripcion
			,  TG.Sexo
		FROM #tMovimientosSalida MS
		INNER JOIN Animal A
			ON (MS.AnimalID = A.AnimalID)
		INNER JOIN TipoGanado TG
			ON (A.TipoGanadoID = TG.TipoGanadoID)
		UNION
		SELECT A.AnimalID
			,  A.Arete
			,  A.FolioEntrada
			,  A.TipoGanadoID
			,  A.OrganizacionIDEntrada
			,  TG.Descripcion
			,  TG.Sexo
		FROM #tMovimientosSalida MS
		INNER JOIN AnimalHistorico A
			ON (MS.AnimalID = A.AnimalID)
		INNER JOIN TipoGanado TG
			ON (A.TipoGanadoID = TG.TipoGanadoID)
		SELECT T.CodigoTratamiento
			,  MC.AnimalID
			,  MC.AnimalMovimientoID
			,  MC.FechaMovimiento
			,  P.Descripcion AS Producto
		FROM #tAnimales A
		INNER JOIN 
		(
			SELECT AnimalID,AnimalMovimientoID,FechaMovimiento,TipoMovimientoID
			FROM AnimalMovimiento
			UNION ALL
			SELECT AnimalID,AnimalMovimientoID,FechaMovimiento,TipoMovimientoID
			FROM AnimalMovimientoHistorico
		) MC
			ON (A.AnimalID = MC.AnimalID)
		INNER JOIN AlmacenMovimiento ALM
			ON (MC.AnimalMovimientoID = ALM.AnimalMovimientoID
				AND MC.TipoMovimientoID IN (@EntradaEnfermeria, @EntradaSalidaEnfermeria))
		INNER JOIN AlmacenMovimientoDetalle AMD
			ON (ALM.AlmacenMovimientoID = AMD.AlmacenMovimientoID)
		INNER JOIN Tratamiento T
			ON (AMD.TratamientoID = T.TratamientoID
				AND T.TipoTratamientoID IN (@TipoTratamientoCorte, @TipoTratamientoEnfermeria))
		LEFT OUTER JOIN Producto P
			ON (AMD.ProductoID = P.ProductoID)
		INSERT INTO #tFolios
		SELECT EG.EntradaGanadoID
			,  EG.FolioEntrada
			,  EG.FechaEntrada
			,  EG.OrganizacionOrigenID
			,  O.TipoOrganizacionID
			,  O.Descripcion AS Organizacion
			,  A.AnimalID
		FROM #tAnimales A
		INNER JOIN EntradaGanado EG
			ON (A.FolioEntrada = EG.FolioEntrada
				AND EG.OrganizacionID = @OrganizacionID)
		INNER JOIN Organizacion O
			ON (EG.OrganizacionOrigenID = O.OrganizacionID)
		SELECT F.EntradaGanadoID
			,  F.FechaEntrada
			,  F.FolioEntrada
			,  F.OrganizacionOrigenID
			,  F.TipoOrganizacionID
			,  F.Organizacion
			,  P.Descripcion AS Proveedor
			,  F.AnimalID
		FROM #tFolios F
		LEFT JOIN EntradaGanadoCosteo EGC
			ON (F.EntradaGanadoID = EGC.EntradaGanadoID
				AND EGC.OrganizacionID = @OrganizacionID)
		LEFT OUTER JOIN EntradaGanadoCosto EGCosto
			ON (EGC.EntradaGanadoCosteoID = EGCosto.EntradaGanadoCosteoID
				AND EGCosto.CostoID = 1)
		LEFT OUTER JOIN Proveedor P
			ON (EGCosto.ProveedorID = P.ProveedorID)
		GROUP BY F.EntradaGanadoID
			,  F.FechaEntrada
			,  F.FolioEntrada
			,  F.OrganizacionOrigenID
			,  F.TipoOrganizacionID
			,  F.Organizacion
			,  P.Descripcion
			,  F.AnimalID
		ORDER BY FechaEntrada
		SELECT A.AnimalID
			,  A.Arete
			,  A.FolioEntrada
			,  A.OrganizacionIDEntrada
			,  A.TipoGanado
			,  A.TipoGanadoID
			,  A.Sexo
			,  CS.Descripcion AS CausaSalida
			,  CAST(VG.PesoBruto - VG.PesoTara AS INT) AS Peso
			,  VG.FolioTicket
		FROM #tAnimales A
		INNER JOIN VentaGanadoDetalle VGD
			ON (A.AnimalID = vgd.AnimalID)
		INNER JOIN CausaPrecio CP
			ON (VGD.CausaPrecioID = CP.CausaPrecioID)
		INNER JOIN CausaSalida CS
			ON (CP.CausaSalidaID = CS.CausaSalidaID)
		INNER JOIN VentaGanado VG
			ON (VGD.VentaGanadoID = VG.VentaGanadoID)
		DROP TABLE #tMovimientosSalida
		DROP TABLE #tAnimales
		DROP TABLE #tFolios
	SET NOCOUNT OFF
END

GO
