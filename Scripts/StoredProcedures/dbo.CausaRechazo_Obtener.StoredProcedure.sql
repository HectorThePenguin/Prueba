USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CausaRechazo_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CausaRechazo_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[CausaRechazo_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Francisco Alejo Pacheco
-- Create date: 11/12/2013
-- Origen: APInterfaces
-- Descripcion: Registros Encontrados en Causa Rechazo
-- CausaRechazo_Obtener
-- =============================================
CREATE PROCEDURE [dbo].[CausaRechazo_Obtener]
	AS
	BEGIN
		SET NOCOUNT ON;
		SELECT 
			Descripcion,
			CausaRechazoID 
		FROM CausaRechazo
		WHERE Activo = 1
		ORDER BY Descripcion ASC
		SET NOCOUNT OFF;
	END 

GO
