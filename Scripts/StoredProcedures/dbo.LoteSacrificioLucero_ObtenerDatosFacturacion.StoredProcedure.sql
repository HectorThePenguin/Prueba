USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ObtenerDatosFacturacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[LoteSacrificioLucero_ObtenerDatosFacturacion]
GO
/****** Object:  StoredProcedure [dbo].[LoteSacrificioLucero_ObtenerDatosFacturacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 12/09/2014
-- Description: Obtiene los datos para la poliza de lote sacrificio
-- SpName     : LoteSacrificioLucero_ObtenerDatosFacturacion '20150421', 5
--======================================================
CREATE PROCEDURE [dbo].[LoteSacrificioLucero_ObtenerDatosFacturacion]
@Fecha DATE
, @OrganizacionID INT
AS
BEGIN

	SET NOCOUNT ON

  		SELECT 
		       LS.Fecha  
		    ,  SUM(LS.ImporteCanal)  AS ImporteCanal  
		    ,  SUM(LS.ImportePiel)  AS ImportePiel  
		    ,  SUM(LS.ImporteVisera)  AS ImporteViscera  
			,  LS.Serie
			,  LS.Folio
		FROM LoteSacrificioLucero LS
		WHERE CAST(LS.Fecha AS DATE) = @Fecha
			AND LS.ImporteCanal > 0
			AND LS.Serie IS NULL
			AND LS.Folio IS NULL
  		GROUP BY 
     		LS.Fecha  
			,  LS.Serie
			,  LS.Folio

	SET NOCOUNT OFF

END

GO
