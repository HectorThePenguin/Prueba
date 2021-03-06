USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_ActualizarProrrateo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoCosteo_ActualizarProrrateo]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_ActualizarProrrateo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 05/04/2014
-- Description:  Actualizar Flag Prorrateo de consumos
-- Origen: APInterfaces
-- EntradaGanadoCosteo_ActualizarProrrateo 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanadoCosteo_ActualizarProrrateo]
	@EntradaGanadoCosteoID INT
AS
  BEGIN
    SET NOCOUNT ON
		UPDATE EntradaGanadoCosteo
		SET Prorrateado = 1,
			FechaModificacion = GETDATE()/*,
			UsuarioModificacion*/
		WHERE EntradaGanadoCosteoID = @EntradaGanadoCosteoID
	SET NOCOUNT OFF
  END

GO
