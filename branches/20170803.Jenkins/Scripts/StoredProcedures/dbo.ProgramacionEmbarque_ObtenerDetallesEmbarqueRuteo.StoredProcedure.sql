USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerDetallesEmbarqueRuteo]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerDetallesEmbarqueRuteo]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerDetallesEmbarqueRuteo]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 02-05-2017
-- Description: Procedimiento almacenado que obtiene las rutas configuradas para el origen y destino.
-- SpName     : ProgramacionEmbarque_ObtenerDetallesEmbarqueRuteo 14853
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerDetallesEmbarqueRuteo]
@EmbarqueID BIGINT
AS
BEGIN
		SELECT EmbarqueRuteoID, org.Descripcion, er.FechaProgramada, er.Horas, er.RuteoID
	FROM EmbarqueRuteo er (NOLOCK)
	INNER JOIN Organizacion org (NOLOCK) ON (er.OrganizacionID = org.OrganizacionID)
	WHERE er.EmbarqueID = @EmbarqueID
	
END