USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Grados_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Grados_Obtener]
GO
/****** Object:  StoredProcedure [dbo].[Grados_Obtener]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Roque.Solis
-- Fecha: 17-01-2014
-- Origen: 		APInterfaces
-- Descripci�n:	Obtiene el tratamiento del ganado de acuerdo al peso y sexo.
-- Exec Grados_Obtener 
-- =============================================
CREATE PROCEDURE [dbo].[Grados_Obtener]
AS
BEGIN	
	SELECT GradoID, Descripcion, NivelGravedad 
	FROM Grado (NOLOCK)
	WHERE Activo = 1;
END

GO
