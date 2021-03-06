USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RevisionImplante_ObtenerRevisionActual]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RevisionImplante_ObtenerRevisionActual]
GO
/****** Object:  StoredProcedure [dbo].[RevisionImplante_ObtenerRevisionActual]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Roque Solis
-- Create date: 10/12/2014
-- Description:  Obtiene si el animal ya tuvo una revisión en el día actual
-- EXEC RevisionImplante_ObtenerRevisionActual 35848, 1
-- ===============================================================
CREATE PROCEDURE [dbo].[RevisionImplante_ObtenerRevisionActual] 
	@AnimalId INT,
	@Activo INT
AS
BEGIN
	SET NOCOUNT ON;
		SELECT RevisionImplanteID,
				LoteID,
				AnimalID,
				CAST(Fecha AS DATE) AS Fecha,
				AreaRevisionID,
				EstadoImplanteID,
				FechaCreacion,
				UsuarioCreacionID
		FROM RevisionImplante
		WHERE AnimalID = @AnimalId
		AND CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)
		AND Activo = @Activo
	SET NOCOUNT OFF;
END

GO
