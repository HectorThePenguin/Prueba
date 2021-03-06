USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ChequeraEtapas_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ChequeraEtapas_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[ChequeraEtapas_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Sergio Alberto Gamez Gomez
-- Create date: 17/06/2015
-- Description:  Obtener listado de todas las etapas activas de una chequera
-- ChequeraEtapas_ObtenerTodos
-- =============================================
CREATE PROCEDURE [dbo].[ChequeraEtapas_ObtenerTodos]
AS  
BEGIN  
	SET NOCOUNT ON;
	SELECT 
		EtapaID,
		Descripcion
	FROM ChequeraEtapas (NOLOCK)
	WHERE Estatus = 1
	SET NOCOUNT OFF;  
END

GO
