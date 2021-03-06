USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorIDFiltroTipoOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Organizacion_ObtenerPorIDFiltroTipoOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Organizacion_ObtenerPorIDFiltroTipoOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jesus Alvarez
-- Create date: 18/07/2014
-- Description:  Obtener organizacion por id y filtro tipo organizacion
-- Organizacion_ObtenerPorIDFiltroTipoOrganizacion 19, 1, '<ROOT>
--  <TiposOrganizaciones>
--    <TipoOrganizacionID>2</TipoOrganizacionID>
--  </TiposOrganizaciones>
--  <TiposOrganizaciones>
--    <TipoOrganizacionID>4</TipoOrganizacionID>
--  </TiposOrganizaciones>
--</ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[Organizacion_ObtenerPorIDFiltroTipoOrganizacion]
@OrganizacionID int,
@Activo BIT,
@XmlTiposOrganizacion XML
AS
  BEGIN
      SET NOCOUNT ON;
	DECLARE @TmpTiposOrganizacion TABLE(TipoOrganizacionID INT)
	INSERT INTO @TmpTiposOrganizacion
	SELECT TipoOrganizacionID  = T.item.value('./TipoOrganizacionID[1]', 'INT')
	  FROM @XmlTiposOrganizacion.nodes('ROOT/TiposOrganizaciones') AS T(item) 
      SELECT O.OrganizacionID,
			O.TipoOrganizacionID,
			O.Descripcion,
			O.Activo,
			O.Direccion,
			TOO.Descripcion AS TipoOrganizacion
      FROM Organizacion O
	  INNER JOIN TipoOrganizacion TOO
		ON (O.TipoOrganizacionID = TOO.TipoOrganizacionID)
      WHERE O.OrganizacionID = @OrganizacionID
	  AND O.TipoOrganizacionID IN (SELECT TipoOrganizacionID FROM @TmpTiposOrganizacion)
	  AND O.Activo = @Activo
      SET NOCOUNT OFF;
  END

GO
