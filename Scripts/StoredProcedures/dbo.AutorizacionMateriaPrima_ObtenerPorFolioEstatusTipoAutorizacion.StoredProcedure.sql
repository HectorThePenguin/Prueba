USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerPorFolioEstatusTipoAutorizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerPorFolioEstatusTipoAutorizacion]
GO
/****** Object:  StoredProcedure [dbo].[AutorizacionMateriaPrima_ObtenerPorFolioEstatusTipoAutorizacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Edgar Villarreal
-- Create date: 01/12/2014 12:00:00 a.m.
-- Description: Obtiene solicitud de AutorizacionMateriaPrima
-- SpName     : AutorizacionMateriaPrima_ObtenerPorFolioEstatusTipoAutorizacion 1,2,524,52,1
--======================================================
CREATE PROCEDURE [dbo].[AutorizacionMateriaPrima_ObtenerPorFolioEstatusTipoAutorizacion]
@OrganizacionID INT,
@TipoAutorizacionID INT,
@Folio INT,
@EstatusID INT,
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1	AMP.AutorizacionMateriaPrimaID,
					AMP.OrganizacionID,
					AMP.TipoAutorizacionID,
					AMP.Folio,
					AMP.Justificacion,
					AMP.Lote,
					AMP.Precio,
					AMP.Cantidad,
					AMP.ProductoID,
					AMP.EstatusID,
					AMP.AlmacenID,
					PMP.CantidadProgramada
	FROM AutorizacionMateriaPrima AMP
	INNER JOIN ProgramacionMateriaPrima PMP ON PMP.AutorizacionMateriaPrimaID = AMP.AutorizacionMateriaPrimaID
	WHERE AMP.OrganizacionID = @OrganizacionID
		AND AMP.TipoAutorizacionID = @TipoAutorizacionID
		AND AMP.EstatusID = @EstatusID
		AND AMP.Folio = @Folio
		AND AMP.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
