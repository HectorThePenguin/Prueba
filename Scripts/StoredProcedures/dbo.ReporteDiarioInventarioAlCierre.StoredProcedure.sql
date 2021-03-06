USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDiarioInventarioAlCierre]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReporteDiarioInventarioAlCierre]
GO
/****** Object:  StoredProcedure [dbo].[ReporteDiarioInventarioAlCierre]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ReporteDiarioInventarioAlCierre]  @OrganizacionId INT,@Fecha DATE
AS
BEGIN 
		SET NOCOUNT ON
		DECLARE @FamiliaMateriaPrima INT=1
		DECLARE @AlmacenPlantaAlimentos INT=8
		DECLARE @AlmacenMateriasPrimas INT=6
		CREATE TABLE #Tmp_InventarioDiario
		(
			ProductoID					INT NOT NULL DEFAULT 0,
			Ingrediente					VARCHAR(200) NOT NULL DEFAULT '',
			TMExisAlmacenPA				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			TMExisAlmacenMP				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			TMInvTotalPAyMA				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			TMConsumoDia				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			DiasCobertura				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			CapacidadAlmacenajeDias		DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			DiasCoberturaFaltante		DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			MinimoDiasReorden			DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			TMCapacidadAlmacenaje		DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			EstatusReorden				VARCHAR(200) NOT NULL DEFAULT '',
			NumeroInventarios			INT NOT NULL DEFAULT 0
		)
		CREATE TABLE #Tmp_InventarioDiarioAux
		(
			ProductoID					INT NOT NULL DEFAULT 0,
			Ingrediente					VARCHAR(200) NOT NULL DEFAULT '',
			TMExisAlmacenPA				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			TMExisAlmacenMP				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			TMInvTotalPAyMA				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			TMConsumoDia				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			DiasCobertura				DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			CapacidadAlmacenajeDias		DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			DiasCoberturaFaltante		DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			MinimoDiasReorden			DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			TMCapacidadAlmacenaje		DECIMAL (12,2) NOT NULL DEFAULT 0.00,
			EstatusReorden				VARCHAR(200) NOT NULL DEFAULT '',
		)
		INSERT INTO #Tmp_InventarioDiario(ProductoID,Ingrediente)
		SELECT distinct AI.ProductoID,P.Descripcion
		FROM AlmacenInventario AI 
		INNER JOIN Producto P ON P.ProductoID = AI.ProductoID 
		INNER JOIN SubFamilia SF ON SF.SubFamiliaID = P.SubFamiliaID 
		INNER JOIN Almacen A ON A.AlmacenID = AI.AlmacenID 
		INNER JOIN TipoAlmacen TA ON TA.TipoAlmacenID = A.TipoAlmacenID 
		WHERE SF.FamiliaID = @FamiliaMateriaPrima
		AND A.OrganizacionID = @OrganizacionId 
		AND TA.TipoAlmacenID IN ( @AlmacenPlantaAlimentos, @AlmacenMateriasPrimas ) AND AI.Cantidad > 0 
		select TMPD.ProductoID, TMPD.Ingrediente, SUM(AI.Cantidad) as CantidadPA
		INTO #CantidadPA
		FROM #Tmp_InventarioDiario TMPD
		INNER JOIN AlmacenInventario AI ON TMPD.ProductoID = AI.ProductoID
		INNER JOIN Producto P ON P.ProductoID = AI.ProductoID 
		INNER JOIN SubFamilia SF ON SF.SubFamiliaID = P.SubFamiliaID 
		INNER JOIN Almacen A ON A.AlmacenID = AI.AlmacenID 
		INNER JOIN TipoAlmacen TA ON TA.TipoAlmacenID = A.TipoAlmacenID 
		WHERE SF.FamiliaID = @FamiliaMateriaPrima
		AND A.OrganizacionID = @OrganizacionId 
		AND TA.TipoAlmacenID IN ( @AlmacenPlantaAlimentos ) AND AI.Cantidad > 0 
		GROUP BY TMPD.ProductoID, TMPD.Ingrediente
		select TMPD.ProductoID, TMPD.Ingrediente, SUM(AI.Cantidad) as CantidadMP
		INTO #CantidadMP
		FROM #Tmp_InventarioDiario TMPD
		INNER JOIN AlmacenInventario AI ON TMPD.ProductoID = AI.ProductoID
		INNER JOIN Producto P ON P.ProductoID = AI.ProductoID 
		INNER JOIN SubFamilia SF ON SF.SubFamiliaID = P.SubFamiliaID 
		INNER JOIN Almacen A ON A.AlmacenID = AI.AlmacenID 
		INNER JOIN TipoAlmacen TA ON TA.TipoAlmacenID = A.TipoAlmacenID 
		WHERE SF.FamiliaID = @FamiliaMateriaPrima
		AND A.OrganizacionID = @OrganizacionId 
		AND TA.TipoAlmacenID IN ( @AlmacenMateriasPrimas ) AND AI.Cantidad > 0 
		GROUP BY TMPD.ProductoID, TMPD.Ingrediente
		update #Tmp_InventarioDiario SET TMExisAlmacenPA = #CantidadPA.CantidadPA
		FROM #CantidadPA WHERE #Tmp_InventarioDiario.ProductoID = #CantidadPA.ProductoID
		update #Tmp_InventarioDiario SET TMExisAlmacenMP = #CantidadMP.CantidadMP
		FROM #CantidadMP WHERE #Tmp_InventarioDiario.ProductoID = #CantidadMP.ProductoID
		--OBTENER TOTAL PA Y MA		
		UPDATE #Tmp_InventarioDiario SET TMInvTotalPAyMA = (TMExisAlmacenPA + TMExisAlmacenMP)
		--ObtenerConsumo del Dia
		SELECT TMPD.ProductoID, COALESCE(SUM(PFD.CantidadProducto),0) AS TMConsumoDia
		INTO #Consumos
		FROM #Tmp_InventarioDiario TMPD
		INNER JOIN AlmacenMovimientoDetalle AMD ON AMD.ProductoID = TMPD.ProductoID 
		LEFT JOIN ProduccionFormulaDetalle PFD ON PFD.ProductoID = TMPD.ProductoID 
		LEFT JOIN ProduccionFormula PF ON PF.ProduccionFormulaID = PFD.ProduccionFormulaID
		AND CAST(AMD.FechaCreacion as DATE) = @Fecha
		AND CAST(PF.FechaProduccion as DATE) = @Fecha
		GROUP BY TMPD.ProductoID
		update #Tmp_InventarioDiario SET TMConsumoDia = #Consumos.TMConsumoDia
		FROM #Consumos WHERE #Tmp_InventarioDiario.ProductoID = #Consumos.ProductoID
		--OBTENER DIAS COBERTURA
		UPDATE #Tmp_InventarioDiario SET DiasCobertura = COALESCE(TMInvTotalPAyMA / NULLIF(TMConsumoDia,0), 0)
		--OBTENER MINIMOS DIAS REORDEN Y CAPACIDAD ALMACENAJE
		Insert into #Tmp_InventarioDiarioAux(ProductoID, Ingrediente, TMExisAlmacenPA, TMExisAlmacenMP, TMInvTotalPAyMA, TMConsumoDia, DiasCobertura, TMCapacidadAlmacenaje, MinimoDiasReorden)
		SELECT TMP1.ProductoID, TMP1.Ingrediente, TMP1.TMExisAlmacenPA, TMP1.TMExisAlmacenMP, TMP1.TMInvTotalPAyMA, TMP1.TMConsumoDia, TMP1.DiasCobertura,
		SUM(AL.CapacidadAlmacenaje) as Capacidad,
		SUM(AL.DiasReorden) / COUNT(AL.AlmacenInventarioID) As Dias
		FROM #Tmp_InventarioDiario TMP1
		INNER JOIN AlmacenInventario AL ON TMP1.ProductoID = AL.ProductoID
		INNER JOIN Almacen ALM on AL.AlmacenID = ALM.AlmacenId
		INNER JOIN TipoAlmacen TP on ALM.TipoAlmacenID = TP.TipoAlmacenID
		WHERE TP.TipoAlmacenID IN (@AlmacenMateriasPrimas, @AlmacenPlantaAlimentos)
		Group by TMP1.ProductoID, TMP1.Ingrediente, TMP1.TMExisAlmacenPA, TMP1.TMExisAlmacenMP, TMP1.TMInvTotalPAyMA, TMP1.TMConsumoDia, TMP1.DiasCobertura
		--OBTENER ESTATUS REPORDEN E INGREDIENTES
		UPDATE #Tmp_InventarioDiarioAux SET EstatusReorden= CASE WHEN DiasCobertura = MinimoDiasReorden THEN 'OK'
														      WHEN DiasCobertura > MinimoDiasReorden THEN 'Sobre Inventario'
															  WHEN DiasCobertura < MinimoDiasReorden THEN 'Realizar Pedido'
														 END
		--OBTENER CAPACIDAD ALMACENAJE EN DIAS
		UPDATE #Tmp_InventarioDiarioAux SET CapacidadAlmacenajeDias = COALESCE(TMCapacidadAlmacenaje / NULLIF(TMConsumoDia,0), 0) 
		--OBTENER DIAS COBERTURA FALTANTES
		UPDATE #Tmp_InventarioDiarioAux SET DiasCoberturaFaltante= COALESCE(CapacidadAlmacenajeDias / NULLIF(DiasCobertura,0), 0) 
		select ProductoID, Ingrediente, TMExisAlmacenPA /1000 as TMExisAlmacenPA, TMExisAlmacenMP /1000 as TMExisAlmacenMP, TMInvTotalPAyMA /1000 as TMInvTotalPAyMA, 
		TMConsumoDia, DiasCobertura, TMCapacidadAlmacenaje, MinimoDiasReorden, EstatusReorden,
		CapacidadAlmacenajeDias, DiasCoberturaFaltante
		from #Tmp_InventarioDiarioAux order by Ingrediente
		DROP TABLE #Tmp_InventarioDiario
		DROP TABLE #Tmp_InventarioDiarioAux
		DROP TABLE #CantidadPA
		DROP TABLE #CantidadMP
		DROP TABLE #Consumos
END

GO
