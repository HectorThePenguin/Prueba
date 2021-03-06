USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerCabezasSacrificadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerCabezasSacrificadas]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_ObtenerCabezasSacrificadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 09/03/2015
-- Description: Obtiene los datos del traspaso de ganado
-- --=============================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspaso_ObtenerCabezasSacrificadas]
@XmlInterface	XML
AS
BEGIN
	SET NOCOUNT ON;
			SELECT LSL.LoteSacrificioID
				,  LSL.Serie
				,  LSL.Folio
				,  LSL.InterfaceSalidaTraspasoDetalleID
				,  LSL.Fecha
				,  LSL.Corral
				,  COUNT(LSLD.AnimalID)		AS Canales
			FROM LoteSacrificioLucero LSL
			INNER JOIN
			(
				SELECT
					t.item.value('./Id[1]', 'BIGINT') AS Id
				FROM @XmlInterface.nodes('ROOT/InterfaceDetalleID') AS T (item)
			) X ON (LSL.InterfaceSalidaTraspasoDetalleID = x.Id)
			INNER JOIN LoteSacrificioLuceroDetalle LSLD
				ON (LSL.LoteSacrificioID = LSLD.LoteSacrificioID)
			WHERE LEN(LSL.Serie) > 0	
				AND LEN(LSL.Folio) > 0
			GROUP BY LSL.LoteSacrificioID
				,	 LSL.Serie
				,	 LSL.Folio
				,	 LSL.InterfaceSalidaTraspasoDetalleID
				,	 LSL.Fecha
				,    LSL.Corral
	SET NOCOUNT OFF;
END

GO
