USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerEsCargaInicialAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalMovimiento_ObtenerEsCargaInicialAnimal]
GO
/****** Object:  StoredProcedure [dbo].[AnimalMovimiento_ObtenerEsCargaInicialAnimal]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/07/22
-- Description: SP para Obtener Si el animal es de Carga Inicial
-- Origen     : APInterfaces
-- EXEC AnimalMovimiento_ObtenerEsCargaInicialAnimal 1
-- =============================================
CREATE PROCEDURE [dbo].[AnimalMovimiento_ObtenerEsCargaInicialAnimal]
	@AnimalID BIGINT
AS
BEGIN
	
	SELECT COUNT(1) CargaInicial
	  FROM Animal A(NOLOCK)
	 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
	 WHERE AM.AnimalID = @AnimalID 
	   AND AM.Observaciones = 'Carga Inicial'

END
GO
