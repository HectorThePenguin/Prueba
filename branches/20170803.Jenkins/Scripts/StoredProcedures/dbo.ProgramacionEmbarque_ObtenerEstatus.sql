USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerEstatus]    Script Date: 25/06/2017 12:00:00 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerEstatus]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerEstatus]    Script Date: 25/06/2017 12:00:00 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martinez
-- Create date: 25/06/2017 12:00:00 a.m.
-- Description: Procedimiento almacenado para consultar el estatus del embarque.
-- SpName: ProgramacionEmbarque_ObtenerEstatus 15439
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerEstatus]
@EmbarqueID			    INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		es.Descripcion AS Descripcion
	FROM Embarque em 
	INNER JOIN Estatus es ON em.Estatus = es.EstatusID
	WHERE EmbarqueID = @EmbarqueID;
	SET NOCOUNT OFF;
END
