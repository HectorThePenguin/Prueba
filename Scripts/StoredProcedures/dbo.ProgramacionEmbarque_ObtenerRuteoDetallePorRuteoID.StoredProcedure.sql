USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRuteoDetallePorRuteoID]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerRuteoDetallePorRuteoID]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRuteoDetallePorRuteoID]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 02-05-2017
-- Description: Procedimiento almacenado que obtiene las rutas configuradas para el origen y destino.
-- SpName     : ProgramacionEmbarque_ObtenerRuteoDetallePorRuteoID 28,'2017-06-05', 8
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerRuteoDetallePorRuteoID]
@RuteoID BIGINT,
@CitaCarga DATETIME,
@HoraCarga FLOAT
AS
BEGIN
	SELECT od.OrganizacionID, rd.RuteoDetalleID, od.Descripcion, DATEADD(hour,  @HoraCarga + SUM( Horas ) OVER( ORDER BY RuteoDetalleID ), @CitaCarga) AS Fecha, 
	CASE 
		WHEN (@HoraCarga + SUM( Horas ) OVER( ORDER BY RuteoDetalleID )) > 24
			THEN (@HoraCarga + SUM( Horas ) OVER( ORDER BY RuteoDetalleID )) - 24
		ELSE (@HoraCarga + SUM( Horas ) OVER( ORDER BY RuteoDetalleID ))
	END Horas
	FROM RuteoDetalle rd (NOLOCK)
	INNER JOIN Organizacion od (NOLOCK) ON (rd.OrganizacionDestinoID = od.OrganizacionID)
	WHERE rd.RuteoID = @RuteoID
	AND rd.Activo = 1
END