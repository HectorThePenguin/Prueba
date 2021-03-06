USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Banco_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Banco_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Sergio Alberto Gamez Gomez
-- Fecha: 17-06-2015
-- Descripci�n:	Obtener un listado de los bancos activos
-- Banco_ObtenerTodos
-- =============================================
CREATE PROCEDURE [dbo].[Banco_ObtenerTodos] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT     
		BancoID = B.BancoID,
		Descripcion = UPPER(B.Descripcion),
		Telefono = B.Telefono,
		PaisID = B.PaisID,
		Pais = UPPER(P.Descripcion),
		Activo = B.Activo
	FROM Banco B (NOLOCK)
	INNER JOIN PAIS P (NOLOCK)	
		ON B.PaisID = P.PaisID
	WHERE B.Activo = 1
	SET NOCOUNT OFF;  
END

GO
