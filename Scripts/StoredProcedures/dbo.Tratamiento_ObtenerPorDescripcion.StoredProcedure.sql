USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Tratamiento_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Tratamiento_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Tratamiento_ObtenerPorDescripcion]
@OrganizacionID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TratamientoID,
		OrganizacionID,
		CodigoTratamiento,
		TipoTratamientoID,
		Sexo,
		RangoInicial,
		RangoFinal,
		Activo
	FROM Tratamiento
	WHERE OrganizacionID = @OrganizacionID
	SET NOCOUNT OFF;
END

GO
