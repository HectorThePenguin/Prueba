USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoTransito_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoTransito_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 25/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EntradaGanadoTransito_Crear
--======================================================
CREATE PROCEDURE [dbo].[EntradaGanadoTransito_Crear] @LoteID INT
	,@Cabezas INT
	,@Peso INT
	,@Activo BIT
	,@UsuarioCreacionID INT
	,@XmlDetalle		XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Fecha DATETIME
	SET @Fecha = GETDATE()

	INSERT EntradaGanadoTransito (
		LoteID
		,Cabezas
		,Peso
		,Activo
		,UsuarioCreacionID
		,FechaCreacion
		)
	VALUES (
		@LoteID
		,@Cabezas
		,@Peso
		,@Activo
		,@UsuarioCreacionID
		,@Fecha
		)

	DECLARE @EntradaGanadoTransitoID INT
	SET @EntradaGanadoTransitoID = SCOPE_IDENTITY()

	INSERT INTO EntradaGanadoTransitoDetalle(EntradaGanadoTransitoID, CostoID, Importe, Activo, FechaCreacion, UsuarioCreacionID)
	SELECT @EntradaGanadoTransitoID
		,  t.item.value('./CostoID[1]', 'INT') AS CostoID
		,  t.item.value('./Importe[1]', 'DECIMAL(18, 2)') AS Importe
		,  @Activo
		,  @Fecha
		,  @UsuarioCreacionID
	FROM @XmlDetalle.nodes('ROOT/Detalle') AS T (item)

	SELECT @EntradaGanadoTransitoID

	SET NOCOUNT OFF;
END

GO
