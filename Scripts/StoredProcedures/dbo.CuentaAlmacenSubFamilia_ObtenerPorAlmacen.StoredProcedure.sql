USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_ObtenerPorAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaAlmacenSubFamilia_ObtenerPorAlmacen]
GO
/****** Object:  StoredProcedure [dbo].[CuentaAlmacenSubFamilia_ObtenerPorAlmacen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Gilberto Carranza
-- Create date: 11/08/2014
-- Description:  Obtiene los productos por almacen
-- CuentaAlmacenSubFamilia_ObtenerPorAlmacen 1
-- =============================================
CREATE PROCEDURE [dbo].[CuentaAlmacenSubFamilia_ObtenerPorAlmacen]
@AlmacenID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT CuentaAlmacenSubFamiliaID
			,  AlmacenID
			,  SubFamiliaID
			,  CuentaSAPID
		FROM CuentaAlmacenSubFamilia
		WHERE AlmacenID = @AlmacenID
	SET NOCOUNT OFF
END

GO
