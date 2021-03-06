USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_ObtenerCorralesConfigurados]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralRango_ObtenerCorralesConfigurados]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_ObtenerCorralesConfigurados]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Andres Vejar
-- Create date: 05/12/2013
-- Origen: APInterfaces
-- Description:	Obtiene los corrales que ya han sido configurados para una organizacion
-- execute CorralRango_ObtenerCorralesConfigurados 1;
-- =============================================
CREATE PROCEDURE [dbo].[CorralRango_ObtenerCorralesConfigurados]
	@OrganizacionID INT
AS
BEGIN	
SET NOCOUNT ON;	
	SELECT CR.OrganizacionID, CR.CorralID, CR.Sexo, CR.RangoInicial, CR.RangoFinal, CR.Activo, C.Codigo, TP.Descripcion
	FROM CorralRango CR
    INNER JOIN Corral C ON CR.CorralID = C.CorralID
	INNER JOIN TipoGanado TP ON TP.SEXO = CR.Sexo 
	INNER JOIN TipoGanadoCorrales TC ON TP.TipoGanadoID = TC.TipoGanadoID AND TC.RangoInicial = CR.RangoInicial
	WHERE CR.OrganizacionID = @OrganizacionID AND CR.Activo = 1
	ORDER BY CR.Sexo, CR.RangoInicial
SET NOCOUNT OFF;	
END

GO
