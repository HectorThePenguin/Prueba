USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorIDOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorIDOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorIDOrganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 12/11/2014
-- Description:  Obtener el almacen por ID y organizacion ID
--	
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorIDOrganizacion]
	@AlmacenID INT,
	@OrganizacionID INT
AS
  BEGIN
      SET NOCOUNT ON;
		SELECT a.AlmacenID,
			a.OrganizacionID,
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
		WHERE AlmacenID = @AlmacenID
		AND @OrganizacionID in (a.OrganizacionID,0)
      SET NOCOUNT OFF;
  END

GO
