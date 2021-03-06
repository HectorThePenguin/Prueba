USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoParcial_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoParcial_ObtenerPorContratoID]
GO
/****** Object:  StoredProcedure [dbo].[ContratoParcial_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 15/05/2014
-- Description: Obtiene una lista por ContratoID
-- SpName     : ContratoParcial_ObtenerPorContratoID 1
--======================================================
CREATE PROCEDURE [dbo].[ContratoParcial_ObtenerPorContratoID]
@ContratoID INT
AS
BEGIN
	SELECT 
		ContratoParcialID,
		ContratoID,
		Cantidad,
		Importe,
		Cp.TipoCambioID,
		Tc.Descripcion,
		Tc.Cambio,
		Cp.Activo,
		Cp.FechaCreacion,
		Cp.UsuarioCreacionID,
		Cp.FechaModificacion,
		Cp.UsuarioModificacionID
	FROM ContratoParcial Cp
	INNER JOIN TipoCambio Tc 
	ON Cp.TipoCambioID = Tc.TipoCambioID
	WHERE ContratoID = @ContratoID AND Cp.Activo = 1
END

GO
