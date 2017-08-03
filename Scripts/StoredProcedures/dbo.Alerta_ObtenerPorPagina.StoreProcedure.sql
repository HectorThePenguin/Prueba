--======================================================
-- Author     : Franco Jesus Inzunza Martinez
-- Create date: 15/03/2016 11:30:00 a.m.
-- Description: 
-- SpName     : Alerta_ObtenerPorPagina 0,'',0,0,1,1,15
--======================================================
ALTER PROCEDURE [dbo].[Alerta_ObtenerPorPagina]
@AlertaID int,
@Descripcion varchar(255),
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Alerta.Descripcion ASC) AS [RowNum],
		AlertaID,
    Alerta.Descripcion AS 'Descripcion',
		HorasRespuesta,
		TerminadoAutomatico,
		Alerta.Activo AS 'Activo',
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID,
		Alerta.ModuloID AS 'ModuloID',
		Modulo.Descripcion AS 'Modulo'
	INTO #Alerta 
	FROM Alerta INNER JOIN Modulo ON Alerta.ModuloID=Modulo.ModuloID
	WHERE (Alerta.Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND @AlertaID in (AlertaID, 0) AND Alerta.Activo = @Activo

	SELECT
	AlertaID,
    Descripcion,
		HorasRespuesta,
		TerminadoAutomatico,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID,
		ModuloID,
    Modulo
	FROM #Alerta
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND RowNum BETWEEN @Inicio AND @Limite
	SELECT
	COUNT(AlertaID) AS [TotalReg]
	FROM #Alerta
	DROP TABLE #Alerta
	SET NOCOUNT OFF;
END