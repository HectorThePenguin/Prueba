USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CancelarMovimiento_ObtenerMuertesCancelarMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CancelarMovimiento_ObtenerMuertesCancelarMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[CancelarMovimiento_ObtenerMuertesCancelarMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: C�sar Valdez
-- Create date: 12/02/2013
-- Description: Obtiene la lista de cabezas muertas detectadas cancelas
-- Empresa: Apinterfaces
-- EXEC CancelarMovimiento_ObtenerMuertesCancelarMovimiento 4
-- =============================================
CREATE PROCEDURE [dbo].[CancelarMovimiento_ObtenerMuertesCancelarMovimiento]
 @OrganizacionId INT
AS
BEGIN
	SELECT MU.MuerteID, 
		   MU.Arete, 
		   MU.AreteMetalico, 
		   CR.Codigo,
		   MU.OperadorDeteccion, 
		  (COALESCE(OP.Nombre,'') + ' ' +
		   COALESCE(OP.ApellidoPaterno,'') + ' ' +
		   COALESCE(OP.ApellidoMaterno,'') 
		   ) AS OperadorDeteccionNombre,
		   COALESCE(MU.OperadorRecoleccion,0) AS OperadorRecoleccion, 
		  COALESCE(
			  (SELECT (COALESCE(Nombre,'') + ' ' +
					   COALESCE(ApellidoPaterno,'') + ' ' +
					   COALESCE(ApellidoMaterno,'') 
					   )AS Nombre 
				 FROM Operador 
				WHERE OperadorID = MU.OperadorRecoleccion
			   ), '') AS OperadorRecoleccionNombre,
		   MU.FechaDeteccion
	FROM muertes MU
	INNER JOIN Lote LT ON MU.LoteId = LT.LoteID
	INNER JOIN Corral CR ON LT.CorralID = CR.CorralID
	INNER JOIN Operador OP ON OP.OperadorID = MU.OperadorDeteccion
	WHERE LT.OrganizacionID = @OrganizacionId
	AND MU.FechaNecropsia IS NULL
	AND MU.Activo = 1
END

GO
