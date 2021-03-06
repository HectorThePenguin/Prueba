USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorIDFiltroTipoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorIDFiltroTipoAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorIDFiltroTipoAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Jesus Alvarez
-- Create date: 17/07/2014
-- Description:  Obtener el almacen por ID, Tipo Almacen
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorIDFiltroTipoAlmacen]
@AlmacenID INT,
@OrganizacionID INT,
@Activo BIT,
@XmlTiposAlmacen XML
AS
BEGIN
      SET NOCOUNT ON;
	DECLARE @TmpTiposAlmacen TABLE(TipoAlmacenID INT)
	INSERT INTO @TmpTiposAlmacen
	SELECT TipoAlmacenID  = T.item.value('./TipoAlmacenID[1]', 'INT')
	  FROM @XmlTiposAlmacen.nodes('ROOT/TiposAlmacen') AS T(item) 
		SELECT a.AlmacenID,
			a.OrganizacionID,
			o.Descripcion AS Organizacion,
			a.CodigoAlmacen,
			a.Descripcion,
			a.TipoAlmacenID,
			ta.Descripcion as [TipoAlmacen],
			a.CuentaInventario,
			a.CuentaInventarioTransito,
			a.CuentaDiferencias,
			a.Activo,
			a.FechaCreacion,
			a.UsuarioCreacionID
		FROM Almacen a 
		inner join TipoAlmacen ta on ta.TipoAlmacenID = a.TipoAlmacenID
		inner join Organizacion o on a.OrganizacionID = o.OrganizacionID
		WHERE AlmacenID = @AlmacenID
		AND a.OrganizacionID = @OrganizacionID
		AND a.TipoAlmacenID IN (SELECT TipoAlmacenID FROM @TmpTiposAlmacen)
      SET NOCOUNT OFF;
END

GO
