USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tarifario_ObtenerEmbarqueGastoTarifa]    Script Date: 22/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Tarifario_ObtenerEmbarqueGastoTarifa]
GO
/****** Object:  StoredProcedure [dbo].[Tarifario_ObtenerEmbarqueGastoTarifa]    Script Date: 22/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 22-05-2017
-- Description: sp para regresar los gastos fijos de embarque tarifa
-- SpName     : Tarifario_ObtenerEmbarqueGastoTarifa 1
--======================================================  
CREATE PROCEDURE Tarifario_ObtenerEmbarqueGastoTarifa
@embarqueTarifaID INT
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #EmbarqueGastoTarifa(
	[gastoFijoId] int NOT NULL,
	[descripcion] VARCHAR(255) NOT NULL,
	[importe] DECIMAL(18,2) NOT NULL
	);
	INSERT INTO #EmbarqueGastoTarifa 
	SELECT 
		GF.GastoFijoID as gastoFijoId,
		GF.Descripcion as descripcion,
		GF.Importe as importe
	FROM
		GastosFijos AS GF
		INNER JOIN EmbarqueGastoTarifa AS EGT ON GF.GastoFijoID = EGT.GastoFijoID
	WHERE
		EGT.EmbarqueTarifaID = @embarqueTarifaID
	
	SELECT 
	[gastoFijoId],
	[descripcion],
	[importe]
	FROM #EmbarqueGastoTarifa
	DROP TABLE #EmbarqueGastoTarifa
	SET NOCOUNT OFF;
	
END