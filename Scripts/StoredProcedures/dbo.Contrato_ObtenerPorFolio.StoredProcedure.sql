USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ObtenerPorFolio]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ObtenerPorFolio]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 17/05/2014
-- Description: Obtiene un contrato por Folio
-- SpName     : Contrato_ObtenerPorFolio 10
--======================================================
CREATE PROCEDURE [dbo].[Contrato_ObtenerPorFolio]
@Folio INT,
@Organizacion INT 
AS
BEGIN
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
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		C.CalidadOrigen
	FROM Contrato (NOLOCK) C
	WHERE C.Folio = @Folio
	AND OrganizacionID = @Organizacion
	AND Activo = 1
END
GO
