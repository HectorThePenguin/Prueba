USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Operador_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Operador_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 04/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Operador_ObtenerPorDescripcion 'ALFONSO MARTINEZ MUNGUIA'
--======================================================
CREATE PROCEDURE [dbo].[Operador_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		OperadorID,
		Nombre,
		ApellidoPaterno,
		ApellidoMaterno,
		CodigoSAP,
		RolID,
		UsuarioID,
		OrganizacionID,
		Activo
	FROM Operador
	WHERE (Nombre + ' ' + ApellidoPaterno +' ' + ApellidoMaterno = @Descripcion)
	SET NOCOUNT OFF;
END

GO
