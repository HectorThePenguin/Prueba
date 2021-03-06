USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Obtener_CalidadMezcladoFactor]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Obtener_CalidadMezcladoFactor]
GO
/****** Object:  StoredProcedure [dbo].[Obtener_CalidadMezcladoFactor]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Villarreal Medina Edgar
-- Create date: 18/10/2013
-- Description:	Obtener listado de calidad de mezclado factor.
-- Obtener_CalidadMezcladoFactor
-- =============================================
CREATE PROCEDURE [dbo].[Obtener_CalidadMezcladoFactor]		
AS
BEGIN
	SET NOCOUNT ON;
SELECT TM.TipoMuestraID, TM.Descripcion, CMF.PesoBaseHumeda, CMF.PesoBaseSeca ,CMF.Factor
FROM TipoMuestra  (NOLOCK) TM
INNER JOIN CalidadMezcladoFactor  (NOLOCK) CMF ON CMF.TipoMuestraID=TM.TipoMuestraID
	SET NOCOUNT ON;	   
END

GO
