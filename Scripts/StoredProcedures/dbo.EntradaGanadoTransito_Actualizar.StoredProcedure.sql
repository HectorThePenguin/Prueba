USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoTransito_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 25/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EntradaGanadoTransito_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[EntradaGanadoTransito_Actualizar] @EntradaGanadoTransitoID INT
	,@LoteID INT
	,@Cabezas INT
	,@Peso INT
	,@Activo BIT
	,@UsuarioModificacionID INT
	,@XmlDetalle XML
AS
BEGIN
	SET NOCOUNT ON;

	CREATE TABLE #tDetalle (
		EntradaGanadoTransitoID INT
		,CostoID INT
		,Importe DECIMAL(18, 2)
		)

	DECLARE @Fecha DATETIME

	SET @Fecha = GETDATE()

	INSERT INTO #tDetalle
	SELECT t.item.value('./EntradaGanadoTransitoID[1]', 'INT') AS EntradaGanadoTransitoID
		,t.item.value('./CostoID[1]', 'INT') AS CostoID
		,t.item.value('./Importe[1]', 'DECIMAL(18, 2)') AS Importe
	FROM @XmlDetalle.nodes('ROOT/Detalle') AS T(item)

	UPDATE EntradaGanadoTransito
	SET LoteID = @LoteID
		,Cabezas = Cabezas + @Cabezas
		,Peso = Peso + @Peso
		,Activo = @Activo
		,UsuarioModificacionID = @UsuarioModificacionID
		,FechaModificacion = @Fecha
	WHERE EntradaGanadoTransitoID = @EntradaGanadoTransitoID

	UPDATE EGDT
	SET EGDT.Importe = EGDT.Importe + x.Importe
		,EGDT.Activo = @Activo
		,EGDT.UsuarioModificacionID = @UsuarioModificacionID
		,EGDT.FechaModificacion = @Fecha
	FROM EntradaGanadoTransitoDetalle EGDT
	INNER JOIN #tDetalle x ON (
			EGDT.EntradaGanadoTransitoID = x.EntradaGanadoTransitoID
			AND EGDT.CostoID = X.CostoID
			AND EGDT.EntradaGanadoTransitoID = @EntradaGanadoTransitoID
			)

	INSERT INTO EntradaGanadoTransitoDetalle (
		EntradaGanadoTransitoID
		,CostoID
		,Importe
		,Activo
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT @EntradaGanadoTransitoID
		,x.CostoID
		,x.Importe
		,@Activo
		,@Fecha
		,@UsuarioModificacionID
	FROM EntradaGanadoTransitoDetalle EGDT
	RIGHT OUTER JOIN #tDetalle x ON (EGDT.EntradaGanadoTransitoID = x.EntradaGanadoTransitoID
									AND EGDT.CostoID = X.CostoID)
	WHERE EGDT.EntradaGanadoTransitoID IS NULL
		AND EGDT.CostoID IS NULL

	SET NOCOUNT OFF;
END

GO
