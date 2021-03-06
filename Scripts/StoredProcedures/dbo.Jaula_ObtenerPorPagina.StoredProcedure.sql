USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Jaula_ObtenerPorPagina]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_ObtenerPorPagina]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 06/01/2014
-- Modified: Luis Alfonso Sandoval Huerta
-- Modification date: 22/05/2017
-- Description:  Obtener listado de Jaulas
-- Se modifica para agregar nuevos campos:
-- NumEconomico, MarcaID, Modelo, Boletinado,
-- Observaciones
-- [Jaula_ObtenerPorPagina] '', 4805, 1, 1, 15
-- =============================================
CREATE PROCEDURE [dbo].[Jaula_ObtenerPorPagina]
  @PlacaJaula   VARCHAR(10)
, @ProveedorID  INT
, @Activo 		BIT = null
, @Inicio 		INT
, @Limite 		INT
AS
  BEGIN
    SET NOCOUNT ON;
		SELECT
			ROW_NUMBER() OVER (ORDER BY PlacaJaula ASC) AS RowNum,
			JaulaID,
			Placajaula,
			ProveedorID,
			Capacidad,
			Secciones,
			NumEconomico,
			MarcaID,
			Modelo,
			Boletinado,
			Observaciones,
			Activo
		INTO #Datos
		FROM Jaula
		WHERE (Activo = @Activo OR @Activo IS null)
				AND PlacaJaula LIKE '%' + @PlacaJaula + '%'
				AND @ProveedorID IN (ProveedorID, 0)
				AND Activo = @Activo
		ORDER BY Placajaula
		SELECT
			j.JaulaID,
			j.ProveedorID,
			p.Descripcion AS [Proveedor],
			p.CodigoSAP,
			j.PlacaJaula,
			j.Capacidad,
			j.Secciones,
			j.NumEconomico,
			j.MarcaID,
			m.Descripcion AS [Marca],
			j.Modelo,
			j.Boletinado,
			j.Observaciones,
			j.Activo
		FROM #Datos j
		INNER JOIN Proveedor p ON p.ProveedorID = j.ProveedorID
		LEFT JOIN Marca m ON m.MarcaID = j.MarcaID AND m.Activo = 1 AND m.Tracto = 0
		WHERE RowNum BETWEEN @Inicio AND @Limite
		SELECT 
			COUNT(JaulaID) AS TotalReg
		From #Datos
		DROP TABLE #Datos
    SET NOCOUNT OFF;
END

GO
