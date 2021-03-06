USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralDetector_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CorralDetector_ObtenerPorPagina 1, '', 1, 1, 100
--======================================================
CREATE PROCEDURE [dbo].[CorralDetector_ObtenerPorPagina]
@OrganizacionID INT,
@NombreOperador VARCHAR(50),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @RolBasculista int
	set @RolBasculista = 2 --Rol del Basculista
	SELECT 
		ROW_NUMBER() OVER (ORDER BY O.OperadorID ASC) AS [RowNum],
		O.OperadorID,
		O.Nombre AS Nombre,
		O.ApellidoPaterno AS ApellidoPaterno,
		O.ApellidoMaterno AS ApellidoMaterno,
		O.CodigoSAP,
		O.Activo,
		U.UsuarioID,
		U.Nombre AS Usuario,
		Org.OrganizacionID,
		Org.Descripcion AS Descripcion,
		R.RolID,
		R.Descripcion AS Rol
	INTO #CorralDetector
	FROM Operador O
	INNER JOIN Usuario U	ON (U.UsuarioID = O.UsuarioID)
	INNER JOIN Organizacion Org	ON (O.OrganizacionID = Org.OrganizacionID)
	INNER JOIN Rol R ON (O.RolID = R.RolID)
	WHERE O.Activo = @Activo AND Org.OrganizacionID = @OrganizacionID
	  AND O.Nombre + ' ' + O.ApellidoPaterno + ' ' + O.ApellidoMaterno LIKE '%' + @NombreOperador + '%'
	  and o.RolID = @RolBasculista
	  --AND EXISTS (
			--SELECT TOP 1 CorralDetectorID 
			--  FROM CorralDetector CD
			-- INNER JOIN Corral C ON (CD.CorralID = C.CorralID 
			--			 AND C.OrganizacionID = Org.OrganizacionID)
			-- WHERE CD.Activo = @Activo AND C.Activo = @Activo
			--	 AND OperadorID = O.OperadorID
		 --)
	SELECT
		OperadorID,
		CodigoSAP,
		Nombre,
		ApellidoMaterno,
		ApellidoPaterno,
		Activo,
		UsuarioID,
		Usuario,
		Descripcion,
		OrganizacionID,
		RolID,
		Rol
	FROM #CorralDetector
	WHERE RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(OperadorID) AS [TotalReg]
	FROM #CorralDetector
	DROP TABLE #CorralDetector
	SET NOCOUNT OFF;
END

GO
