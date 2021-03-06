USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Rotomix_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Rotomix_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Rotomix_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 09/01/2015 12:00:00 a.m.
-- Description: 
-- SpName     : Rotomix_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Rotomix_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		r.RotomixID,
		o.OrganizacionID,
		o.Descripcion AS Organizacion,
		r.Descripcion,
		r.Activo
	FROM Rotomix r
	INNER JOIN Organizacion o on r.OrganizacionID = o.OrganizacionID
	WHERE r.Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
