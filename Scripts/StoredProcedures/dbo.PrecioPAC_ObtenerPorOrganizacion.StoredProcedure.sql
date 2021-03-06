USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PrecioPAC_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PrecioPAC_ObtenerPorOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[PrecioPAC_ObtenerPorOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Gilberto Carranza
-- Create date: 31/03/2015
-- Description: Obtiene los datos del Precio PAC
-- [PrecioPAC_ObtenerPorOrganizacion] 1
-- --=============================================
CREATE PROCEDURE [dbo].[PrecioPAC_ObtenerPorOrganizacion]
@OrganizacionID	INT
AS
BEGIN

	SET NOCOUNT ON;

		SELECT PrecioPACID
			,  OrganizacionID
			,  TipoPACID
			,  Precio
			,  FechaInicio
			,  Activo
			,  PrecioViscera
		FROM PrecioPAC
		WHERE OrganizacionID = @OrganizacionID
			AND Activo = 1

	SET NOCOUNT OFF;
END

GO
