/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRuteosPorOrigenyDestino]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerRuteosPorOrigenyDestino]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerRuteosPorOrigenyDestino]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 02-05-2017
-- Description: Procedimiento almacenado que obtiene las rutas configuradas para el origen y destino.
-- SpName     : ProgramacionEmbarque_ObtenerRuteosPorOrigenyDestino 1,5
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerRuteosPorOrigenyDestino]
@OrganizacionOrigenID BIGINT,
@OrganizacionDestinoID BIGINT
AS
BEGIN
	SELECT DISTINCT r.RuteoID, r.NombreRuteo
	FROM Ruteo r (NOLOCK)
	INNER JOIN RuteoDetalle rd ON rd.RuteoID = r.RuteoID
	WHERE r.OrganizacionOrigenID = @OrganizacionOrigenID 
	AND r.OrganizacionDestinoID = @OrganizacionDestinoID 
	AND r.Activo = 1
	AND rd.Activo = 1
	
END