USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerCuentaPorClaveCuenta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cuenta_ObtenerCuentaPorClaveCuenta]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerCuentaPorClaveCuenta]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Cuenta_ObtenerCuentaPorClaveCuenta]
@OrganizacionID INT
,@ClaveCuenta varchar(50)
AS
SET NOCOUNT ON
BEGIN
SELECT
cv.Valor
,c.Descripcion
FROM Cuenta c
inner JOIN CuentaValor cv on c.CuentaID = cv.CuentaID
WHERE c.ClaveCuenta = @ClaveCuenta
and CV.OrganizacionID = @OrganizacionID
AND c.Activo = 1 
AND cv.Activo = 1
END

GO
