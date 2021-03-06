USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Menu_ObtenerPorUsuario]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Menu_ObtenerPorUsuario]
GO
/****** Object:  StoredProcedure [dbo].[Menu_ObtenerPorUsuario]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 01/10/2013
-- Description:	Obtiene los formularios a los que tiene permiso el usuario.
-- Menu_ObtenerPorUsuario 'luis.velazquez', 1
-- 001 Jorge Luis Velazquez Araujo 07/12/2015 ** Se agrega para que solo regrese las funcionalides de Engorda
-- =============================================
CREATE PROCEDURE [dbo].[Menu_ObtenerPorUsuario]		
@UsuarioActiveDirectory VARCHAR(50)
, @AplicacionWeb BIT
AS
BEGIN
	SET NOCOUNT ON;	
	SELECT 
		M.ModuloID, 
		M.Descripcion Modulo, 
		F.FormularioID, 
		F.Descripcion Formulario,
		F.WinForm, 
		F.Imagen, 
		M.Control,
		pa.PadreID,
		pa.Descripcion Padre
		,pa.Imagen ImagenPadre
		,F.Orden AS OrdenFormulario
		,M.Orden AS OrdenModulo
	FROM Usuario U 
	INNER JOIN UsuarioGrupo UG 
		ON UG.UsuarioID = U.UsuarioID
	INNER JOIN Grupo G 
		ON G.GrupoID = UG.GrupoID
	INNER JOIN GrupoFormulario GF 
		ON GF.GrupoID = G.GrupoID 
	INNER JOIN Formulario F 
		ON F.FormularioID = GF.FormularioID 
			AND F.Web = @AplicacionWeb
	INNER JOIN Modulo M 
		ON M.ModuloID = F.ModuloID
		LEFT JOIN Padre pa on f.PadreID = pa.PadreID
	WHERE U.UsuarioActiveDirectory = @UsuarioActiveDirectory 
	and f.Activo = 1
	and f.Abasto = 0	 
	ORDER BY M.Orden, F.Orden 
END

GO
