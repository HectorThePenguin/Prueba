USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_ObtenerRetencionesConCosto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Retencion_ObtenerRetencionesConCosto]
GO
/****** Object:  StoredProcedure [dbo].[Retencion_ObtenerRetencionesConCosto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 2013/12/11
-- Description: 
-- 
/* Retencion_ObtenerRetencionesConCosto '<ROOT>
  <Datos>
    <CostoID>1</CostoID>
  </Datos>
  <Datos>
    <CostoID>11</CostoID>
  </Datos>
  <Datos>
    <CostoID>1</CostoID>
  </Datos>
  <Datos>
    <CostoID>3</CostoID>
  </Datos>
  <Datos>
    <CostoID>4</CostoID>
  </Datos>
  <Datos>
    <CostoID>2</CostoID>
  </Datos>
</ROOT>'*/
--=============================================
CREATE PROCEDURE [dbo].[Retencion_ObtenerRetencionesConCosto]
@XmlCostos XML
AS
BEGIN
	SET NOCOUNT ON
	SELECT C.CostoID
		 , R.RetencionID
		 , R.Descripcion
		 , R.IndicadorImpuesto
		 , R.IndicadorRetencion
		 , R.Tasa
		 , R.TipoRetencion
	FROM (
		SELECT  
			CostoID = T.Item.value('./CostoID[1]', 'INT')
		FROM @XmlCostos.nodes('ROOT/Datos') AS T(Item)
	) X	
	INNER JOIN Costo C
	 ON (X.CostoID = C.CostoID)
	INNER JOIN Retencion R
		ON (C.RetencionID = R.RetencionID)
	SET NOCOUNT OFF
END

GO
