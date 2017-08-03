USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerJaulasProgramadas]    Script Date: 06/07/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerJaulasProgramadas]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerJaulasProgramadas]    Script Date: 06/07/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 06-07-2017
-- Description: sp para obtener las jaulas programadas
-- SpName     : ProgramacionEmbarque_ObtenerJaulasProgramadas 1,2017-06-04
--======================================================  
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerJaulasProgramadas]
@OrganizacionID INT, 
@FechaInicio DATE
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CAST(CitaDescarga AS DATE) AS Fecha,
		count(1) AS JaulasProgramadas
	FROM Embarque
	WHERE
		OrganizacionID = @OrganizacionID AND 
		CitaDescarga BETWEEN @FechaInicio AND DATEADD(DAY,7,@FechaInicio) AND
		Activo = 1 AND
		Estatus = 1
	GROUP BY CAST(CitaDescarga AS DATE) ORDER BY JaulasProgramadas
	SET NOCOUNT OFF;
END
