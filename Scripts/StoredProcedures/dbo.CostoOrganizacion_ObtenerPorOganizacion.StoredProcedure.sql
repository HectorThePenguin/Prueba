USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_ObtenerPorOganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoOrganizacion_ObtenerPorOganizacion]
GO
/****** Object:  StoredProcedure [dbo].[CostoOrganizacion_ObtenerPorOganizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/12/10
-- Description: 
-- CostoOrganizacion_ObtenerPorOganizacion 4,4
--=============================================
CREATE PROCEDURE [dbo].[CostoOrganizacion_ObtenerPorOganizacion]
@TipoOrganizacionOrigenID INT
,@OrganizacionDestinoID INT
AS
SELECT
co.CostoOrganizacionID
,c.CostoID
,c.ClaveContable
,re.RetencionID
,re.Descripcion [Retencion]
,c.Descripcion [Costo]
,c.AbonoA
,c.TipoProrrateoID
,ISNULL(cp.Importe,0) [Importe]
FROM CostoOrganizacion co
INNER JOIN Costo c ON co.CostoID = c.CostoID
LEFT JOIN CostoPromedio cp on co.CostoID = cp.CostoID
AND cp.OrganizacionID = @OrganizacionDestinoID
LEFT JOIN Retencion re on c.RetencionID = re.RetencionID
WHERE co.TipoOrganizacionID = @TipoOrganizacionOrigenID
AND co.Activo = 1
AND co.Automatico = 1

GO
