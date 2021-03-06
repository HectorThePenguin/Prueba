USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorEstado]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorEstado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 15/05/2014
-- Description: 
-- SpName     : Contrato_ObtenerPorEstado 1
--======================================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorEstado]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ContratoID,
		OrganizacionID,
		Folio,
		ProductoID,
		TipoContratoID,
		TipoFleteID,
		ProveedorID,
		Precio,
		TipoCambioID,
		Cantidad,
		Merma,
		PesoNegociar,
		Fecha,
		FechaVigencia,
		Activo
	FROM Contrato
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
