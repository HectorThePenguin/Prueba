USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaGrado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[DeteccionGanado_ConsultaGrado]
GO
/****** Object:  StoredProcedure [dbo].[DeteccionGanado_ConsultaGrado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 20/02/2014
-- Description:	Obtiene los grados a mostrar
-- [Deteccion_GanadoConsultaGrado] 1
--======================================================
CREATE PROCEDURE [dbo].[DeteccionGanado_ConsultaGrado]
AS
BEGIN
	SELECT GradoID,Descripcion,NivelGravedad
	FROM Grado WHERE Activo = 1
	SET NOCOUNT OFF;
END

GO
