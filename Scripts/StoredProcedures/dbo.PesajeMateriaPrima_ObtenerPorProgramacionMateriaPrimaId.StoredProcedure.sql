USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaId]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaId]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaId]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 26/06/2014
-- Description: 
-- SpName     : PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaId 1
--======================================================
CREATE PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaId]
@ProgramacionMateriaPrimaId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    PesajeMateriaPrimaID,
		ProgramacionMateriaPrimaID,
		ProveedorChoferID,
		Ticket,
		CamionID,
		PesoBruto,
		PesoTara,
		Piezas,
		TipoPesajeID,
		UsuarioIDSurtido,
		FechaSurtido,
		UsuarioIDRecibe,
		FechaRecibe,
		EstatusID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	FROM PesajeMateriaPrima (NOLOCK) WHERE ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaId
	AND Activo = 1
	SET NOCOUNT OFF;
END

GO
