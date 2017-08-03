USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ActualizarCabezasOrigen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ActualizarCabezasOrigen]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ActualizarCabezasOrigen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Juli�n Carranza Castro
-- Create date: 25/11/2014 12:00:00 a.m.
-- Description: 
-- SpName     : EntradaGanado_ActualizarCabezasOrigen
--======================================================
CREATE PROCEDURE [dbo].[EntradaGanado_ActualizarCabezasOrigen]
@EntradaGanadoID INT,
@CabezasOrigen INT 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE EntradaGanado SET
		CabezasOrigen = @CabezasOrigen
	WHERE EntradaGanadoID = @EntradaGanadoID
	SET NOCOUNT OFF;
END

GO
