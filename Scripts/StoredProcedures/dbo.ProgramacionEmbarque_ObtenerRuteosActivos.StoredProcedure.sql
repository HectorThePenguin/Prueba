USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRuteosActivos]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerRuteosActivos]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRuteosActivos]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 31-05-2017
-- Description: Procedimiento almacenado que obtiene los datos para la pestaña "Programacion" en la pantalla programación embarque
-- SpName     : ProgramacionEmbarque_ObtenerRuteosActivos
--======================================================  
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerRuteosActivos]
AS
BEGIN
	SELECT 	r.RuteoID, r.NombreRuteo 
	FROM Ruteo r (NOLOCK)
	INNER JOIN RuteoDetalle rd (NOLOCK) ON (r.RuteoID = rd.RuteoID)
	WHERE r.Activo = 1 AND rd.Activo = 1
END