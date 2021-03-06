USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListGanadoMuerto_GuardarRecoleccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListGanadoMuerto_GuardarRecoleccion]
GO
/****** Object:  StoredProcedure [dbo].[CheckListGanadoMuerto_GuardarRecoleccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Andres Vejar
-- Create date: 19/02/2013
-- Description: Actualiza la tabla de muertes con el ganado recolectado
-- Empresa: Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[CheckListGanadoMuerto_GuardarRecoleccion]
 @MuerteId int,
 @OperadorId int,
 @FechaRecoleccion smalldatetime
AS
BEGIN
		UPDATE Muertes 
		SET FechaRecoleccion = GETDATE()/*@FechaRecoleccion*/, 
		    OperadorRecoleccion = @OperadorId,
		    EstatusId = 5
		where MuerteId = @MuerteId
END

GO
