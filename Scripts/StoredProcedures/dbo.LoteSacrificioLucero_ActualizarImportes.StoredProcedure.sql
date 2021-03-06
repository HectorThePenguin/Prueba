USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ActualizarImportes]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_ActualizarImportes]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ActualizarImportes]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Ramses Santos
-- Create date: 2014/08/18
-- Description: Sp para obtener los datos para generar la factura.
-- LoteSacrificioLucero_ActualizarImportes 140
--=============================================
CREATE PROCEDURE [dbo].[LoteSacrificioLucero_ActualizarImportes] 
@XmlInterface	XML
AS
BEGIN
	SET NOCOUNT ON;
		  UPDATE LS
		  SET ImporteCanal	= x.ImporteCanal
			, ImportePiel	= x.ImportePiel
			, ImporteVisera	= x.ImporteViscera
		  FROM LoteSacrificioLucero (NOLOCK) AS LS
		  INNER JOIN 
		  (
				SELECT
					t.item.value('./Id[1]', 'BIGINT') AS Id
					, t.item.value('./ImporteCanal[1]', 'DECIMAL(18,2)') AS ImporteCanal
					, t.item.value('./ImportePiel[1]', 'DECIMAL(18,2)') AS ImportePiel
					, t.item.value('./ImporteViscera[1]', 'DECIMAL(18,2)') AS ImporteViscera
					,t.item.value('./Corral[1]', 'varchar(10)') AS Corral
				FROM @XmlInterface.nodes('ROOT/InterfaceDetalleID') AS T (item)
		  ) x ON (LS.InterfaceSalidaTraspasoDetalleID = x.Id AND ls.Corral = x.Corral)	  
	SET NOCOUNT OFF;
END

GO
