USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerImpresionCalidadGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerImpresionCalidadGanado]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerImpresionCalidadGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 17/12/2014
-- Description:	Obtiene los datos para la impresion de la Calificacion de Ganado
-- EntradaGanado_ObtenerImpresionCalidadGanado 1,'20141201'
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerImpresionCalidadGanado] @OrganizacionID INT
	,@FechaEntrada DATE
AS
BEGIN
	CREATE TABLE #DATOSENTRADA (
		EntradaGanadoID INT
		,FolioEntrada INT
		,Corral VARCHAR(10)
		,Proveedor VARCHAR(50)
		,FechaEntrada DATETIME
		)
	INSERT INTO #DATOSENTRADA
	SELECT eg.EntradaGanadoID
		,CAST(eg.FolioEntrada AS VARCHAR(20)) AS FolioEntrada
		,co.Codigo AS Corral
		,o.Descripcion AS Proveedor
		,eg.FechaEntrada AS FechaCalificacion
	FROM EntradaGanado eg
	INNER JOIN Usuario us ON eg.UsuarioCreacionID = us.UsuarioID
	INNER JOIN Organizacion o ON eg.OrganizacionOrigenID = o.OrganizacionID
	INNER JOIN Operador op ON eg.OperadorID = op.OperadorID
	INNER JOIN Jaula ja ON eg.JaulaID = ja.JaulaID
	INNER JOIN Corral co ON eg.CorralID = co.CorralID
	INNER JOIN Lote lo ON eg.LoteID = lo.LoteID
	WHERE eg.OrganizacionID = @OrganizacionID
		AND lo.Activo = 1
		AND CAST(eg.FechaEntrada AS DATE) = @FechaEntrada
	SELECT EntradaGanadoID
		,CAST(FolioEntrada AS VARCHAR(10)) AS FolioEntrada
		,Corral
		,Proveedor
		,FechaEntrada
	FROM #DATOSENTRADA
	SELECT ec.EntradaGanadoCalidadID
		,ec.EntradaGanadoID
		,cg.CalidadGanadoID
		,cg.Descripcion [CalidadGanado]
		,cg.Calidad
		,cg.Sexo
		,ec.Valor
		,ec.Activo
	FROM CalidadGanado cg
	INNER JOIN EntradaGanadoCalidad ec ON ec.CalidadGanadoID = cg.CalidadGanadoID
	INNER JOIN #DATOSENTRADA de ON ec.EntradaGanadoID = de.EntradaGanadoID
	WHERE ec.Activo = 1
END

GO
