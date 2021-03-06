USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoPromedio_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CostoPromedio_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/12/09
-- Description: 
-- CostoPromedio_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[CostoPromedio_ObtenerPorID]
@CostoPromedioID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		cp.CostoPromedioID,
		cp.OrganizacionID,
		o.Descripcion as [Organizacion],
		cp.CostoID,
		c.Descripcion as [Costo],
		c.ClaveContable as [ClaveContable],
		cp.Importe,
		cp.Activo
	FROM CostoPromedio cp
	inner join Organizacion o on o.OrganizacionID = cp.OrganizacionID
	inner join Costo c on c.CostoID = cp.CostoID
	WHERE CostoPromedioID = @CostoPromedioID
	SET NOCOUNT OFF;
END

GO
