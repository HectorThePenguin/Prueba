USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaIdSinActivo]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaIdSinActivo]
GO
/****** Object:  StoredProcedure [dbo].[PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaIdSinActivo]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 26/06/2014
-- Description: 
-- SpName     : PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaIdSinActivo 1
--======================================================
CREATE PROCEDURE [dbo].[PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaIdSinActivo]
@ProgramacionMateriaPrimaId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
	    PMP.PesajeMateriaPrimaID,
		PMP.ProgramacionMateriaPrimaID,
		PMP.ProveedorChoferID,
		PMP.Ticket,
		PMP.CamionID,
		PMP.PesoBruto,
		PMP.PesoTara,
		PMP.Piezas,
		PMP.TipoPesajeID,
		PMP.UsuarioIDSurtido,
		PMP.FechaSurtido,
		PMP.UsuarioIDRecibe,
		PMP.FechaRecibe,
		PMP.EstatusID,
		PMP.Activo,
		PMP.FechaCreacion,
		PMP.UsuarioCreacionID
	FROM PesajeMateriaPrima (NOLOCK) PMP
			INNER JOIN Estatus (NOLOCK) E ON (E.EstatusID = PMP.EstatusID)
	WHERE ProgramacionMateriaPrimaID = @ProgramacionMateriaPrimaId
	AND E.DescripcionCorta != 'PMPCancela'
	SET NOCOUNT OFF;
END

GO
