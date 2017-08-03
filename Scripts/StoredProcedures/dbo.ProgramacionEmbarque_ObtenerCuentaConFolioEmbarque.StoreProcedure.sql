USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerCuentaConFolioEmbarque]    Script Date: 13/06/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerCuentaConFolioEmbarque]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerCuentaConFolioEmbarque]    Script Date: 13/06/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Lorenzo Antonio Villaseñor Martínez
-- Create date: 13/06/2017
-- Description: sp para obtener si cuenta con folio de embarque
-- SpName     : ProgramacionEmbarque_ObtenerCuentaConFolioEmbarque 1
--======================================================  
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerCuentaConFolioEmbarque]
@EmbarqueID INT
AS
BEGIN
	SELECT 
	emb.FolioEmbarque, 
	est.Descripcion
FROM Embarque emb (NOLOCK)
INNER JOIN Estatus est (NOLOCK) ON (emb.Estatus = est.EstatusID)
WHERE emb.EmbarqueID = @EmbarqueID
END