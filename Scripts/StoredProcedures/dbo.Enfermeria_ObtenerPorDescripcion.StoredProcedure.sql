USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Enfermeria_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Enfermeria_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 07/05/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Enfermeria_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Enfermeria_ObtenerPorDescripcion]
@OrganizacionID INT,
@Descripcion VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
		SELECT E.EnfermeriaID
			,  E.OrganizacionID
			,  E.Activo
			,  E.Descripcion
			,  O.Descripcion AS Organizacion
		FROM Enfermeria E
		INNER JOIN Organizacion O
			ON (E.OrganizacionID = O.OrganizacionID)
		WHERE E.OrganizacionID = @OrganizacionID
			AND E.Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
