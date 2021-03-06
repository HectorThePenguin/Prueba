USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerInformacionCorraletasDisponiblesSacrificio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerInformacionCorraletasDisponiblesSacrificio]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerInformacionCorraletasDisponiblesSacrificio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : C�sar Valdez
-- Create date: 22/08/2014
-- Description: Procedimiento para obtener la informacion de los corrales enviados por parametros
-- SpName     : Corral_ObtenerInformacionCorraletasDisponiblesSacrificio '<ROOT><Corrales><CorralID>1</CorralID></Corrales></ROOT>'
--======================================================
CREATE PROCEDURE [dbo].[Corral_ObtenerInformacionCorraletasDisponiblesSacrificio]
	@OrganizacionID INT,
	@CodigosCorraletaXML XML
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @CodigosCorraleta AS TABLE ([Codigo] VARCHAR(10))
	INSERT @CodigosCorraleta ([Codigo])
	SELECT [Codigo] = t.item.value('./Corraleta[1]', 'VARCHAR(10)')
	FROM @CodigosCorraletaXML.nodes('ROOT/Corral') AS T(item)
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
	  FROM Corral C
	 INNER JOIN @CodigosCorraleta CC ON CC.Codigo = C.Codigo
--	 INNER JOIN Lote L ON L.CorralID = C.CorralID
	 WHERE 1 = 1
	   AND C.OrganizacionID = @OrganizacionID
	 ORDER BY C.Codigo
--	   AND L.Activo = 1;
	SET NOCOUNT OFF
END

GO
