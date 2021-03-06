USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GastoInventario_GuardarGasto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GastoInventario_GuardarGasto]
GO
/****** Object:  StoredProcedure [dbo].[GastoInventario_GuardarGasto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Andres Vejar
-- Create date: 18/07/2014
-- Description: Guardar el gasto al inventario especificado generando el costo por animal
-- GastoInventario_GuardarGasto 1, 'Parcial/Total', '2014-01-01 13:00', 10, '0012023', 12, 10232.30, 'obser', 'FE12', 1, 1,13,10
--=============================================
CREATE PROCEDURE [dbo].[GastoInventario_GuardarGasto] 
@OrganizacionId INT,
@TipoGasto VARCHAR(10),
@FechaGasto smalldatetime,
@CostoId INT,
@TieneCuenta INT,
@CuentaSAPId INT,
@ProveedorId int,
@Importe decimal(18,2),
@Observaciones varchar(255),
@Factura varchar(50),
@IVA bit,
@Retencion bit,
@TipoFolio int,
@CorralId int,
@Activo bit,
@UsuarioId int
, @TotalCorrales INT
, @CentroCosto	 VARCHAR(50)
, @CuentaGasto   VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
		DECLARE @FolioGasto INT
		DECLARE @TipoReferencia INT
		DECLARE @TotalAnimales INT
		DECLARE @ImporteProrateado decimal(18,2)
		DECLARE @ImporteDiferencia decimal(18,2)
		DECLARE @GrupoProduccion INT
		DECLARE @GrupoEnfermeria INT
		DECLARE @Script varchar(1200)
		DECLARE @Indice BIGINT
		SET @GrupoProduccion = 2
		SET @GrupoEnfermeria = 3
		SET @TipoReferencia = 4
		SET NOCOUNT OFF
		CREATE TABLE #AnimalGastoInventario
		(
			AnimalID BIGINT
		)
		EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @FolioGasto output
		IF (@ProveedorId = 0)
		BEGIN
			SET @ProveedorId = NULL;
		END
		IF(@CuentaSAPId = 0)
		BEGIN
			SET @CuentaSAPId = NULL;
		END
		IF(@Factura = '')
		BEGIN
			SET @Factura = NULL;
		END
		INSERT INTO GastoInventario (OrganizacionID, TipoGasto, FolioGasto, FechaGasto, CostoID, TieneCuenta, CuentaSAPID,
		ProveedorID,Factura, Importe, IVA, Observaciones,Retencion,Activo,FechaCreacion,UsuarioCreacionID
		, CorralID, TotalCorrales, CentroCosto, CuentaGasto)
		VALUES
		( @OrganizacionId,@TipoGasto,@FolioGasto,@FechaGasto,@CostoId, @TieneCuenta, @CuentaSAPId,
		  @ProveedorId, @Factura, @Importe,@IVA,@Observaciones,@Retencion,@Activo,GETDATE(),@UsuarioId
		, @CorralId, @TotalCorrales, @CentroCosto, @CuentaGasto)
		declare @GastoInventarioID int
		set @GastoInventarioID = SCOPE_IDENTITY()
		SET @Script = 
			'INSERT INTO #AnimalGastoInventario 
			SELECT A.AnimalId 
			FROM Animal (NOLOCK) AS A
			INNER JOIN AnimalMovimiento (NOLOCK) AS AM ON A.AnimalId = AM.AnimalId
			INNER JOIN Lote (NOLOCK) AS L ON (L.LoteId = AM.LoteId)
			INNER JOIN Corral (NOLOCK) as C ON C.CorralId = L.CorralId
			INNER JOIN TipoCorral (NOLOCK) AS TC ON (C.TipoCorralID = TC.TipoCorralID)
			INNER JOIN GrupoCorral (NOLOCK) AS GC ON (TC.GrupoCorralID = GC.GrupoCorralID)
			WHERE
			C.OrganizacionID = '+ CAST(@OrganizacionId AS varchar(10))+'
			AND  GC.GrupoCorralID IN (' +  CAST(@GrupoProduccion as VARCHAR(2)) + ',' + CAST(@GrupoEnfermeria AS VARCHAR(2)) +') 
			AND TC.Activo = 1 AND GC.Activo = 1 AND L.Activo = 1 AND C.Activo = 1 AND AM.Activo = 1'
		--Prorateo de animal costo
		if(@TipoGasto = 'Parcial')
			SET @Script = @Script + ' AND C.CorralId =' + CAST(@CorralId AS VARCHAR(10))
		EXEC (@Script)
		SET @TotalAnimales = (SELECT count(*) from #AnimalGastoInventario)
		SET @ImporteProrateado = @Importe / @TotalAnimales
		SET @ImporteDiferencia = @Importe -(@ImporteProrateado*@TotalAnimales)
		INSERT INTO AnimalCosto(
			AnimalID,
			FechaCosto,
			CostoID,
			FolioReferencia,
			Importe,
			FechaCreacion,
			UsuarioCreacionID,
			TipoReferencia)
		SELECT AnimalId, GETDATE(),@CostoId,@FolioGasto,@ImporteProrateado, GETDATE(), @UsuarioId, @TipoReferencia FROM #AnimalGastoInventario
		SET @Indice = (SELECT SCOPE_IDENTITY());
		--SELECT @ImporteDiferencia
		UPDATE AnimalCosto SET Importe = Importe + @ImporteDiferencia where AnimalCostoId = @Indice
		select @GastoInventarioID
		DROP TABLE #AnimalGastoInventario
	SET NOCOUNT OFF;
END

GO
