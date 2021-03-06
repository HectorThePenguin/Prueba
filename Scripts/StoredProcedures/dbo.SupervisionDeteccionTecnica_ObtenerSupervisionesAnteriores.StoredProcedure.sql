USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDeteccionTecnica_ObtenerSupervisionesAnteriores]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervisionDeteccionTecnica_ObtenerSupervisionesAnteriores]
GO
/****** Object:  StoredProcedure [dbo].[SupervisionDeteccionTecnica_ObtenerSupervisionesAnteriores]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Create date: 19/02/2013
-- Description: Registra en el encabezado de la supervision de tecnica de deteccion
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[SupervisionDeteccionTecnica_ObtenerSupervisionesAnteriores]
@OrganizacionId INT,
@OperadorId	INT
AS
BEGIN
SELECT SupervisionDetectoresID, OrganizacionID, OperadorID, FechaSupervision, CriterioSupervisionID, Observaciones, Activo, FechaCreacion, UsuarioCreacionID
FROM SupervisionDetectores
where OrganizacionID = @OrganizacionId AND OperadorID = @OperadorId and Activo = 1
END

GO
