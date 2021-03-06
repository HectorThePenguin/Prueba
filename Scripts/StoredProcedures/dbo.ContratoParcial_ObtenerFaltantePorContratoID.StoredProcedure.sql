USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoParcial_ObtenerFaltantePorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoParcial_ObtenerFaltantePorContratoID]
GO
/****** Object:  StoredProcedure [dbo].[ContratoParcial_ObtenerFaltantePorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 15/05/2014
-- Description: Obtiene una lista por ContratoID
-- SpName     : ContratoParcial_ObtenerFaltantePorContratoID 1092
--======================================================
CREATE PROCEDURE [dbo].[ContratoParcial_ObtenerFaltantePorContratoID]
@ContratoID INT
AS
BEGIN
	CREATE TABLE #ContratoParcial(
		ContratoParcialID INT,
		ContratoID INT,
		Cantidad INT,
		Importe DECIMAL(18,2),
		ImporteConvertido DECIMAL(18,2),
		TipoCambioID INT,
		Descripcion VARCHAR(50),
		Cambio DECIMAL(10,2),
		Activo BIT,
		FechaCreacion SMALLDATETIME,
		UsuarioCreacionID INT,
		CantidadEntrante INT
	)
	INSERT INTO #ContratoParcial (ContratoParcialID, ContratoID, Cantidad, Importe,ImporteConvertido,TipoCambioID,Descripcion,Cambio,
								  Activo, FechaCreacion, UsuarioCreacionID, CantidadEntrante)
	SELECT 
		CP.ContratoParcialID,
		CP.ContratoID,
		CP.Cantidad,
		CP.Importe,
		CP.Importe / TC.Cambio AS ImporteConvertido,
		TC.TipoCambioID,
		TC.Descripcion,
		TC.Cambio,
		CP.Activo,
		CP.FechaCreacion,
		CP.UsuarioCreacionID,
		0 AS CantidadEntrante
	FROM ContratoParcial CP
	INNER JOIN TipoCambio TC ON(CP.TipoCambioID = TC.TipoCambioID)
	WHERE ContratoID = @ContratoID AND CP.Activo = 1
	UPDATE TMP
	SET CantidadEntrante = (SELECT ISNULL(SUM(EPP.CantidadEntrante),0)
							FROM EntradaProductoParcial EPP 
							WHERE EPP.ContratoParcialID = TMP.ContratoParcialID AND Activo = 1)
	FROM #ContratoParcial TMP
	SELECT 
		ContratoParcialID,
		ContratoID,
		Cantidad,
		Importe,
		ImporteConvertido,
		TipoCambioID,
		Descripcion,
		Cambio,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		Cantidad - CantidadEntrante AS CantidadRestante
	FROM #ContratoParcial
	WHERE Cantidad > CantidadEntrante
END

GO
