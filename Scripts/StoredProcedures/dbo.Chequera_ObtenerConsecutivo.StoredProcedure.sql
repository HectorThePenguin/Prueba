USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ObtenerConsecutivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Chequera_ObtenerConsecutivo]
GO
/****** Object:  StoredProcedure [dbo].[Chequera_ObtenerConsecutivo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Sergio Alberto Gamez Gomez
-- Fecha: 17-06-2015
-- Descripci�n:	Obtener el consecutivo de una chequera
-- Chequera_ObtenerConsecutivo 1
-- =============================================
CREATE PROCEDURE [dbo].[Chequera_ObtenerConsecutivo]
	@OrganizacionId INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT       
		Chequera = ISNULL(MAX(ChequeraId),0) + 1  
	FROM [Sukarne].[dbo].[Chequera] (NOLOCK)
	WHERE OrganizacionId = @OrganizacionId 
	SET NOCOUNT OFF;  
END

GO
