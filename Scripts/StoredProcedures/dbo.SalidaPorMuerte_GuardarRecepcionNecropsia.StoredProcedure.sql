USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_GuardarRecepcionNecropsia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaPorMuerte_GuardarRecepcionNecropsia]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_GuardarRecepcionNecropsia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Create date: 18/02/2013
-- Description: Actualiza la tabla de muertes con la recepcion de necropsia
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[SalidaPorMuerte_GuardarRecepcionNecropsia]
@MuerteId INT,
@OperadorId INT
AS
BEGIN
	UPDATE Muertes SET EstatusId = 8
	where MuerteId = @MuerteId
END

GO
