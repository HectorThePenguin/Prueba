USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_ObtenerPorFiltros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaValor_ObtenerPorFiltros]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_ObtenerPorFiltros]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CuentaValor_ObtenerPorFiltros
--======================================================
CREATE PROCEDURE [dbo].[CuentaValor_ObtenerPorFiltros]
@OrganizacionID INT,
@CuentaID INT,
@Activo BIT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		cv.CuentaValorID,
		cu.CuentaID,
		cu.Descripcion AS Cuenta,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		cv.Valor,
		cv.Activo		
	FROM CuentaValor cv
	INNER JOIN Organizacion o on cv.OrganizacionID = o.OrganizacionID
	inner join Cuenta cu on cv.CuentaID = cu.CuentaID
	WHERE 
	cv.Activo = @Activo
	and @OrganizacionID in (o.OrganizacionID,0)
	and @CuentaID in (cu.CuentaID,0)
	SET NOCOUNT OFF;
END

GO
