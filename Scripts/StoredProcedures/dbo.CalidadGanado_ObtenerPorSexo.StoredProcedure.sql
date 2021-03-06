USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerPorSexo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadGanado_ObtenerPorSexo]
GO
/****** Object:  StoredProcedure [dbo].[CalidadGanado_ObtenerPorSexo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  Francisco Alejo Pacheco
-- Create date: 11/12/2013
-- Origen: APInterfaces
-- Descripcion: Obtener CalidadGanado Por Sexo
-- CalidadGanado_ObtenerPorSexo
-- =============================================
    CREATE PROCEDURE [dbo].[CalidadGanado_ObtenerPorSexo]
		@Sexo varchar(1)
	AS
	BEGIN
		SET NOCOUNT ON;
			SELECT 
				Descripcion,
				CalidadGanadoID 
			FROM CalidadGanado
			WHERE Sexo=@Sexo 
		 ORDER BY Descripcion ASC
		SET NOCOUNT OFF;
	END 

GO
