USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarLoteCargaInicial]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Migracion_GuardarLoteCargaInicial]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarLoteCargaInicial]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 30/01/2015
-- Description: Guarda informacion en la tabla LoteCargaInicial
-- Origen: APInterfaces
-- Migracion_GuardarLoteCargaInicial 1
-- =============================================
CREATE PROCEDURE [dbo].[Migracion_GuardarLoteCargaInicial]
	@OrganizacionID INT
AS
  BEGIN
    SET NOCOUNT ON

	/* Se crean las entradas de ganado carga inicial */
	SELECT 
		OrganizacionID = @OrganizacionID, 
		c.CorralID,
		Lote = ROW_NUMBER() OVER (ORDER BY CorralID), 
		c.TipoCorralID,
		TipoProcesoID = 1,
		FechaInicio = r.[FECHA INICIO], 
		CabezasInicio = COUNT(cm.Arete), 
		FechaCierre = DATEADD(DAY,7,r.[FECHA INICIO]),
		Cabezas = COUNT(arete), 
		FechaDisponibilidad = NULL,
		DisponibilidadManual = 0,
		Activo =  1, 
		FechaSalida = NULL, 
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = 1,
		FechaModificacion = NULL,
		UsuarioModificacionID = NULL
	INTO LoteCargaInicial
	FROM RESUMEN r
	INNER JOIN CtrManiTMP cm
		ON r.CORRAL = cm.NumCorr /* RIGHT(RTRIM(LTRIM('000'+r.CORRAL)),3) = cm.NumCorr */
	INNER JOIN Corral c
		ON cm.NumCorr = c.Codigo /* cm.NumCorr = RIGHT('000' + RTRIM(c.Codigo),3) */
	   AND c.OrganizacionID = @OrganizacionID AND Activo = 1
	WHERE r.ORGANIZACION = 3 /* @OrganizacionID */
	GROUP BY c.CorralID, c.TipoCorralID, r.[FECHA INICIO];

	SET NOCOUNT OFF
  END


GO
