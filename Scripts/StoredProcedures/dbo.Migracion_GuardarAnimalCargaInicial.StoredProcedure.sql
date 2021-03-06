USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarAnimalCargaInicial]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Migracion_GuardarAnimalCargaInicial]
GO
/****** Object:  StoredProcedure [dbo].[Migracion_GuardarAnimalCargaInicial]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:    César Valdez Figueroa
-- Create date: 30/01/2015
-- Description: Guarda informacion en la tabla LoteCargaInicial
-- Origen: APInterfaces
-- Migracion_GuardarAnimalCargaInicial 1
-- =============================================
CREATE PROCEDURE [dbo].[Migracion_GuardarAnimalCargaInicial]
	@OrganizacionID INT
AS
  BEGIN
    SET NOCOUNT ON

	/* Se crean las entradas de ganado carga inicial */
	SELECT 
		Arete = CAST(cm.Arete AS BIGINT),  
		AreteMetalico = '',
		FechaCompra = cm.FechaComp,
		Tipo_Gan = CASE WHEN r.[TIPO DE GANADO] IS NULL THEN cm.TipoGan ELSE LEFT(r.[TIPO DE GANADO],1) END,
		cm.CalEng,
		ClasificacionGanadoID = 4,
		PesoCompra = CASE WHEN r.[PESO ORIGEN] IS NULL THEN 0 ELSE r.[PESO ORIGEN] END,
		OrganizacionIDEntrada = @OrganizacionID,
		FolioEntradaID = 0,
		PesoLlegada = CASE WHEN r.[PESO ORIGEN]  IS NULL THEN 0 ELSE r.[PESO ORIGEN] * .9 END,
		cm.Paletas,
		CausaRechazoID = NULL,
		Venta = 0,
		Cronico = 0,
		Activo = 1,
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = 1,
		FechaModificacion = NULL,
		UsuarioModificacionID = NULL
	INTO AnimalCargaInicial
	FROM CtrManiTMP cm
	LEFT JOIN RESUMEN r ON r.CORRAL = cm.NumCorr /* RIGHT(LTRIM(RTRIM('000' + r.CORRAL)),3) = cm.NumCorr*/
    WHERE r.ORGANIZACION = 3;

	SET NOCOUNT OFF
  END


GO
