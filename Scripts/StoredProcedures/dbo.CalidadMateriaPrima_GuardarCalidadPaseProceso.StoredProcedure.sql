USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMateriaPrima_GuardarCalidadPaseProceso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMateriaPrima_GuardarCalidadPaseProceso]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMateriaPrima_GuardarCalidadPaseProceso]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Gilberto Carranza
-- Fecha: 2014-06-19
-- Descripci�n:	Guardar calidad pase proceso
-- =============================================
CREATE PROCEDURE [dbo].[CalidadMateriaPrima_GuardarCalidadPaseProceso]
@xmlIndicadores XML
, @UsuarioCreacionID INT
AS
BEGIN
	CREATE TABLE #tIndicadores
	(
		PedidoDetalleID			INT
		, Movimiento			INT
		, IndicadorObjetivoID	INT
		, Resultado				DECIMAL(10,2)
		, ColorObjetivoID		INT
		, Observaciones			VARCHAR(255)
	)
	INSERT INTO #tIndicadores
	SELECT DISTINCT A.PedidoDetalleID
		,  A.Movimiento
		,  A.IndicadorObjetivoID
		,  A.Resultado
		,  A.ColorObjetivoID
		,  A.Observaciones
	FROM 
	(
		SELECT 
			  t.item.value('./PedidoDetalleID[1]', 'INT')			AS PedidoDetalleID
			, t.item.value('./Movimiento[1]', 'INT')				AS Movimiento
			, t.item.value('./IndicadorObjetivoID[1]', 'INT')		AS IndicadorObjetivoID
			, t.item.value('./Resultado[1]', 'DECIMAL(10,2)')		AS Resultado
			, t.item.value('./ColorObjetivoID[1]', 'INT')			AS ColorObjetivoID
			, t.item.value('./Observaciones[1]', 'VARCHAR(255)')	AS Observaciones
		FROM @xmlIndicadores.nodes('ROOT/semaforo') AS T(item)
	) A
	INSERT INTO CalidadMateriaPrima
	(
		Activo
		, ColorObjetivoID
		, FechaCreacion
		, IndicadorObjetivoID
		, Movimiento
		, Observaciones
		, PedidoDetalleID
		, Resultado
		, UsuarioCreacionID
	)
	SELECT 1
		,  I.ColorObjetivoID
		,  GETDATE()
		,  I.IndicadorObjetivoID
		,  I.Movimiento
		,  I.Observaciones
		,  NULLIF(I.PedidoDetalleID,0)
		,  I.Resultado
		,  @UsuarioCreacionID
	FROM #tIndicadores I
	DROP TABLE #tIndicadores
END

GO
