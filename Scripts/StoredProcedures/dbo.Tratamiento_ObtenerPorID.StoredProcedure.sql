USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Tratamiento_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Tratamiento_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[Tratamiento_ObtenerPorID]
@TratamientoID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		T.TratamientoID,
		T.OrganizacionID,
		T.CodigoTratamiento,
		T.TipoTratamientoID,
		T.Sexo,
		T.RangoInicial,
		T.RangoFinal,
		T.Activo
		, O.Descripcion AS Organizacion
		, TT.Descripcion AS TipoTratamiento
	FROM Tratamiento T
	INNER JOIN Organizacion O
		ON (T.OrganizacionID = O.OrganizacionID)
	INNER JOIN TipoTratamiento TT
		ON (T.TipoTratamientoID = TT.TipoTratamientoID)
	WHERE T.TratamientoID = @TratamientoID
	SET NOCOUNT OFF;
END

GO
