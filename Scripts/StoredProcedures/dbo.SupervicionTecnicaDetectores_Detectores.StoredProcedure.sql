USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SupervicionTecnicaDetectores_Detectores]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SupervicionTecnicaDetectores_Detectores]
GO
/****** Object:  StoredProcedure [dbo].[SupervicionTecnicaDetectores_Detectores]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Ricardo Lopez
-- Create date: 14/02/2014
-- Description:  Obtiene Nombre del Detector Evaluado.
-- EXEC SupervicionTecnicaDetectores_Detectores 1, 19
-- =============================================
CREATE PROCEDURE [dbo].[SupervicionTecnicaDetectores_Detectores]	
@OrganizacionID INT,
@IdSupervisor INT	
AS
BEGIN
	SET NOCOUNT ON;
  SELECT	DISTINCT
			OP.OperadorID,
			OP.Nombre,
			OP.ApellidoPaterno,
			OP.ApellidoMaterno, 
			r.RolID,
			r.Descripcion Rol,
			OP.UsuarioID,
			o.OrganizacionID,
			o.Descripcion AS Organizacion,
			OP.Activo
	FROM
SupervisorEnfermeria SE
INNER JOIN EnfermeriaCorral EC ON SE.EnfermeriaId = EC.EnfermeriaId
INNER JOIN CorralDetector CD ON CD.CorralId = EC.CorralId
INNER JOIN Operador OP ON OP.OperadorId = CD.OperadorId
INNER JOIN Rol r on op.RolID = r.RolID
INNER JOIN Organizacion o on op.OrganizacionID = o.OrganizacionID
WHERE SE.OperadorId = @IdSupervisor and OP.OrganizacionId = @OrganizacionID
			AND EC.Activo = 1
			AND SE.Activo = 1
			AND CD.Activo = 1
			AND OP.Activo = 1
   ORDER BY OP.Nombre;
SET NOCOUNT OFF;
END

GO
