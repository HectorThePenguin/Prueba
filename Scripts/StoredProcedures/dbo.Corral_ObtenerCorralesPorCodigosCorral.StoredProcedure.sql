USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorCodigosCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerCorralesPorCodigosCorral]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerCorralesPorCodigosCorral]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jorge Luis Velazquez Araujo
-- Create date: 2014-07-14
-- Description:	Obtiene un lista de corrales por sus codigos de corral
-- Corral_ObtenerCorralesPorCodigosCorral 
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerCorralesPorCodigosCorral]
	@OrganizacionID INT,
	@XmlCodigosCorral XML
AS
BEGIN
	SET NOCOUNT ON;
	CREATE TABLE #CODIGOSCORRAL
	(
		Codigo char(10)
	)	
	INSERT #CODIGOSCORRAL (Codigo)
	SELECT Codigo = t.item.value('./CodigoCorral[1]', 'char(10)')
	FROM @XmlCodigosCorral.nodes('ROOT/Codigos') AS t(item)
	SELECT 
		C.CorralID,
		C.OrganizacionID,
		C.Codigo,
		C.TipoCorralID,
		C.Capacidad,
		C.MetrosLargo,
		C.MetrosAncho,
		C.Seccion,
		C.Orden,
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID
	FROM Corral C (NOLOCK) 
	INNER JOIN #CODIGOSCORRAL CC ON C.Codigo = CC.Codigo
	WHERE C.OrganizacionID = @OrganizacionID    
	SET NOCOUNT OFF;
END

GO
