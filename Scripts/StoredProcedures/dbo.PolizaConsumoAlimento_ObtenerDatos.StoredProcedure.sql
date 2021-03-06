USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PolizaConsumoAlimento_ObtenerDatos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PolizaConsumoAlimento_ObtenerDatos]
GO
/****** Object:  StoredProcedure [dbo].[PolizaConsumoAlimento_ObtenerDatos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2014/07/21
-- Description: PolizaConsumoAlimento_ObtenerDatos 1, '<ROOT><Movimientos><Movimiento>32295</Movimiento></Movimientos></ROOT>'
--=============================================
CREATE PROCEDURE [dbo].[PolizaConsumoAlimento_ObtenerDatos]
@OrganizacionID			INT
,@XmlMovimiento			XML
AS
BEGIN
	SET NOCOUNT ON
			DECLARE @Fecha CHAR(8)
			SET @Fecha = CONVERT(CHAR(8), GETDATE() - 1, 112)
			CREATE TABLE #tFormulas
			(
				Fecha				DATETIME
				, FormulaID			INT
				, ProductoID		INT
				, Division			VARCHAR(10)
				, OrganizacionID	INT
				, AlmacenID			INT
				, Importe			DECIMAL(18,2)
			)
			INSERT INTO #tFormulas
			SELECT @Fecha
				,  F.FormulaID
				,  F.ProductoID
				,  Org.Division
				,  Org.OrganizacionID
				,  AM.AlmacenID
				,  AMD.Importe
			FROM AlmacenMovimientoDetalle AMD(NOLOCK)
			INNER JOIN Formula F(NOLOCK)
				ON (AMD.ProductoID = F.ProductoID)
			INNER JOIN Organizacion Org(NOLOCK)
				ON (Org.OrganizacionID = @OrganizacionID)
			--INNER JOIN 
			--(
			--	SELECT T.N.value('./Movimiento[1]','INT') AS AlmacenMovimiento
			--	FROM @XmlMovimiento.nodes('/ROOT/Movimientos') as T(N)
			--) tM ON (AMD.AlmacenMovimientoID = tM.AlmacenMovimiento)
			INNER JOIN AlmacenMovimiento AM
				ON (AMD.AlmacenMovimientoID = AM.AlmacenMovimientoID)
			SELECT Fecha
					, FormulaID			
					, ProductoID		
					, Division			
					, OrganizacionID	
					, AlmacenID			
					, SUM(Importe) AS Importe
			FROM #tFormulas
			GROUP BY  Fecha
					, FormulaID
					, ProductoID
					, Division
					, OrganizacionID
					, AlmacenID
			SELECT @Fecha AS Fecha
				,  Org.Division
				,  SUM(RD.Importe) AS Importe
				,  RD.FormulaIDServida
				,  Org.OrganizacionID
			FROM Reparto R(NOLOCK)
			INNER JOIN RepartoDetalle RD(NOLOCK)
				ON (R.RepartoID = RD.RepartoID
					AND RD.Servido = 1
					AND RD.Importe > 0) --- 1116
			INNER JOIN Organizacion Org(NOLOCK)
				ON (R.OrganizacionID = Org.OrganizacionID
					AND Org.OrganizacionID = @OrganizacionID)
			INNER JOIN #tFormulas tF
				ON (RD.FormulaIDServida = tF.FormulaID)
			WHERE CONVERT(CHAR(8), R.Fecha, 112) = @Fecha
			GROUP BY Org.Division
				,  RD.FormulaIDServida
				,  Org.OrganizacionID
			SELECT @Fecha			AS Fecha
				,  FormulaID
				,  ProductoID
				,  Division
				,  OrganizacionID
				,  AlmacenID
			FROM #tFormulas
			GROUP BY FormulaID
				,  ProductoID
				,  Division
				,  OrganizacionID
				,  AlmacenID
			DROP TABLE #tFormulas
			/*
			SELECT @Fecha AS Fecha
				,  PF.FormulaID
				,  PFD.ProductoID
				,  Org.Division
				,  Org.OrganizacionID
				,  PF.AlmacenMovimientoEntradaID
				,  PF.AlmacenMovimientoSalidaID
			FROM ProduccionFormula PF(NOLOCK)
			INNER JOIN ProduccionFormulaDetalle PFD(NOLOCK)
				ON (PF.ProduccionFormulaID = PFD.ProduccionFormulaID) ---5001
			INNER JOIN Organizacion Org(NOLOCK)
				ON (PF.OrganizacionID = Org.OrganizacionID
					AND Org.OrganizacionID = @OrganizacionID)
			WHERE CONVERT(CHAR(8), FechaProduccion, 112) = @Fecha
			*/
	SET NOCOUNT OFF
END

GO
