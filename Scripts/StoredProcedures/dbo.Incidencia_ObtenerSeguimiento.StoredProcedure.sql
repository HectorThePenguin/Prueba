USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_ObtenerSeguimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Incidencia_ObtenerSeguimiento]
GO
/****** Object:  StoredProcedure [dbo].[Incidencia_ObtenerSeguimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eric García
-- Create date: 15/03/2016 3:22:00 p.m.
-- Description: Obtiene todas las incidencias activas por la organización del usuario
-- SpName     : EXEC Incidencia_ObtenerSeguimiento 504273
--=============================================  
CREATE PROCEDURE [dbo].[Incidencia_ObtenerSeguimiento]  
@IncidenciaID INT

AS  
BEGIN  
	SET NOCOUNT ON; 

	SELECT IncidenciaSeguimientoID,
				 IncidenciaID,
				 Fecha,
				 FechaVencimiento,
				 FechaVencimientoAnterior, 
				 ISNULL(InSe.AccionID,0) AS AccionID,
				 ISNULL(A.Descripcion,'') AS AccionDescripcion,
				 ISNULL(AccionAnteriorID, 0) AS AccionAnteriorID,
				 ISNULL(Comentarios,'') Comentarios,
				 EstatusID,
				 EstatusAnteriorID,
				 ISNULL(UsuarioResponsableID,0) AS UsuarioResponsableID,
				 ISNULL(UsuarioResponsableAnteriorID, 0) AS UsuarioResponsableAnteriorID,
				 NivelAlertaID,
				 NivelAlertaAnteriorID
	FROM IncidenciaSeguimiento InSe
	LEFT JOIN Accion A ON InSe.AccionID = A.AccionID
	WHERE IncidenciaID = @IncidenciaID
	ORDER BY IncidenciaSeguimientoID DESC;

	SET NOCOUNT OFF;  
END
