USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaAlmacenSubFamilia_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 03/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CuentaAlmacenSubFamilia_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[CuentaAlmacenSubFamilia_ObtenerPorID]
@CuentaAlmacenSubFamiliaID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		cas.CuentaAlmacenSubFamiliaID,
		a.AlmacenID,
		a.Descripcion AS Almacen,
		sf.SubFamiliaID,
		sf.Descripcion AS SubFamilia,		
		cs.CuentaSAPID,
		cs.CuentaSAP,
		cs.Descripcion CuentaSAPDescripcion,
		cas.Activo
	FROM CuentaAlmacenSubFamilia cas
	INNER JOIN SubFamilia sf on cas.SubFamiliaID = sf.SubFamiliaID
	INNER JOIN Almacen a on cas.AlmacenID = a.AlmacenID
	INNER JOIN CuentaSAP cs on cas.CuentaSAPID = cs.CuentaSAPID
	WHERE CuentaAlmacenSubFamiliaID = @CuentaAlmacenSubFamiliaID
	SET NOCOUNT OFF;
END

GO
