USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 18/02/2014
-- Description:  Obtener el almacen por ID
--	
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_ObtenerPorID]
	@AlmacenID INT
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
      SET NOCOUNT OFF;
  END

GO
