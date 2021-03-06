USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_ObtenerLectorComedero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EstadoComedero_ObtenerLectorComedero]
GO
/****** Object:  StoredProcedure [dbo].[EstadoComedero_ObtenerLectorComedero]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:    Jorge Luis Velazquez Araujo  
-- Create date: 02-04-2014  
-- Description:  Obtiene el estado de los Comederos, para el sistema Lector de Comedero  
-- EstadoComedero_ObtenerLectorComedero   
-- =============================================  
CREATE PROCEDURE [dbo].[EstadoComedero_ObtenerLectorComedero]
AS
BEGIN
	SELECT EstadoComederoID AS [IdEstadoComedero]
		,DescripcionCorta
		,Descripcion
		,NoServir
		,AjusteBase
		,Tendencia
		,Activo AS [Status]
	FROM EstadoComedero
	WHERE Activo = 1
	ORDER BY EstadoComederoID
END

GO
