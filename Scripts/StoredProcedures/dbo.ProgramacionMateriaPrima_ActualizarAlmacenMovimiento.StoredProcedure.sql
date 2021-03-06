USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ActualizarAlmacenMovimiento]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionMateriaPrima_ActualizarAlmacenMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_ActualizarAlmacenMovimiento]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Carranza
-- Create date: 14/08/2014
-- Description: 
-- SpName     : ProgramacionMateriaPrima_ActualizarAlmacenMovimiento
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionMateriaPrima_ActualizarAlmacenMovimiento]
@ProgramacionMateriaPrimaID INT,
@AlmacenMovimientoID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ProgramacionMateriaPrima
	SET AlmacenMovimientoID = @AlmacenMovimientoID
	WHERE ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaID
	SET NOCOUNT OFF;
END

GO
