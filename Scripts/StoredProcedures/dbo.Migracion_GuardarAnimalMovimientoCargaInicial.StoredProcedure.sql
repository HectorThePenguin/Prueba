USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarAnimalMovimientoCargaInicial]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Migracion_GuardarAnimalMovimientoCargaInicial]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarAnimalMovimientoCargaInicial]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 30/01/2015
-- Description: Guarda informacion en la tabla LoteCargaInicial
-- Origen: APInterfaces
-- Migracion_GuardarAnimalMovimientoCargaInicial 1
-- =============================================
CREATE PROCEDURE [dbo].[Migracion_GuardarAnimalMovimientoCargaInicial]
	@OrganizacionID INT
AS
  BEGIN
    SET NOCOUNT ON

	/* Se crean los movimiento de corte en AnimalMovimientoCargaInicial */
	SELECT 
		Arete = CAST(cm.Arete AS BIGINT), 
		AnimalID = 0,
		OrganizacionID = @OrganizacionID,
		CorralID = c.CorralID,
		LoteID = 0,
		FechaMovimiento = cm.FechaCorte,
		Peso = cm.PesoCorte,
		Temperatura = CAST(cm.Temperatura AS Decimal(5,1)),
		TipoMovimientoID = 5,
		TrampaID = 4,
		OperadorID = 6,
		Observaciones = 'Carga Inicial',
		Activo = 1,
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = 1,
		FechaModificacion = NULL,
		UsuarioModificacionID = NULL
	 INTO AnimalMovimientoCargaInicial
	 FROM CtrManiTMP cm
	INNER JOIN AnimalCargaInicial A ON cm.Arete = A.Arete
	INNER JOIN Corral c
		  ON cm.NumCorr = c.Codigo /* cm.NumCorr = RIGHT('000' + RTRIM(c.Codigo),3) */
	     AND c.OrganizacionID = @OrganizacionID 
	     AND c.Activo = 1;

	/* Se crean los movimiento de reimplante en AnimalMovimientoCargaInicial */
/*	INSERT AnimalMovimientoCargaInicial
	SELECT 
		Arete = CAST(cr.Arete AS BIGINT), 
		AnimalID = 0,
		OrganizacionID = @OrganizacionID,
		CorralID = c.CorralID,
		LoteID = 0,
		FechaMovimiento = cr.FechaReim,
		Peso = cr.PesoReimp,
		Temperatura = 0,
		TipoMovimientoID = 6,
		TrampaID = 4,
		OperadorID = 6,
		Observaciones = 'Carga Inicial',
		Activo = 1,
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = 1,
		FechaModificacion = NULL,
		UsuarioModificacionID = NULL
	FROM CtrReimTMP cr
	INNER JOIN CtrManiTMP cm ON cr.Arete = cm.Arete AND cr.FechaComp = cm.FechaComp
    INNER JOIN AnimalCargaInicial A ON cm.Arete = A.Arete
	INNER JOIN Corral c
		ON cm.NumCorr = RIGHT('000' + RTRIM(Codigo),3) 
		AND c.OrganizacionID = @OrganizacionID 
		AND c.Activo = 1;
*/
	SET NOCOUNT OFF
  END


GO
