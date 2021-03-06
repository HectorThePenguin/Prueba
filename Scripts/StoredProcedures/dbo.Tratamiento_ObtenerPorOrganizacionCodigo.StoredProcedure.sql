USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerPorOrganizacionCodigo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Tratamiento_ObtenerPorOrganizacionCodigo]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerPorOrganizacionCodigo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 20/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Tratamiento_ObtenerPorOrganizacionCodigo
--======================================================
CREATE PROCEDURE [dbo].[Tratamiento_ObtenerPorOrganizacionCodigo] @OrganizacionID INT
	,@CodigoTratamiento INT
	,@Sexo CHAR
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TratamientoID
		,OrganizacionID
		,CodigoTratamiento
		,TipoTratamientoID
		,Sexo
		,RangoInicial
		,RangoFinal
		,Activo
	FROM Tratamiento
	WHERE OrganizacionID = @OrganizacionID
		AND CodigoTratamiento = @CodigoTratamiento
		AND Sexo = @Sexo
	SET NOCOUNT OFF;
END

GO
