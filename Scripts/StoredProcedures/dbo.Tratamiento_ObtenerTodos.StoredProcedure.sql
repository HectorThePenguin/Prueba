USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Tratamiento_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Tratamiento_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Tratamiento_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Tratamiento_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TratamientoID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		CodigoTratamiento,
		tt.TipoTratamientoID,
		tt.Descripcion AS TipoTratamiento,
		Sexo,
		RangoInicial,
		RangoFinal,
		t.Activo
	FROM Tratamiento t
	INNER JOIN Organizacion o on t.OrganizacionID = o.OrganizacionID
	INNER JOIN TipoTratamiento tt ON t.TipoTratamientoID = tt.TipoTratamientoID
	WHERE t.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
