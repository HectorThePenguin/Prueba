USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPorLoteOrganizacion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerPorLoteOrganizacion]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPorLoteOrganizacion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Gilberto Carranza
-- Create date: 03/12/2014
-- Description:	Obtener Reparto
-- Reparto_ObtenerPorLoteOrganizacion 185, 1
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerPorLoteOrganizacion]
@LoteID INT,
@OrganizacionID INT
AS
BEGIN
	SET NOCOUNT ON;

		SELECT R.RepartoID
			,  R.OrganizacionID
			,  R.LoteID
			,  R.Fecha
			,  R.PesoInicio
			,  R.PesoProyectado
			,  R.DiasEngorda
			,  R.PesoRepeso
			,  R.Activo
			,  RD.RepartoDetalleID
			,  RD.TipoServicioID
			,  RD.FormulaIDProgramada
			,  RD.FormulaIDServida
			,  RD.CantidadProgramada
			,  RD.CantidadServida
			,  RD.HoraReparto
			,  RD.CostoPromedio
			,  RD.Importe
			,  RD.Cabezas
			,  RD.EstadoComederoID
			,  RD.CamionRepartoID
			,  RD.Ajuste
			,  RD.Prorrateo
			,  RD.AlmacenMovimientoID
			,  F.FormulaID
			,  F.Descripcion			AS Formula
			,  F.TipoFormulaID
			,  F.ProductoID
		FROM Reparto R
		INNER JOIN RepartoDetalle RD
			ON (R.RepartoID = RD.RepartoID
				AND RD.Servido = 1
				AND RD.Activo = 1)
		INNER JOIN Formula F
			ON (RD.FormulaIDServida = F.FormulaID)
		WHERE R.LoteID = @LoteID
			AND R.OrganizacionID = @OrganizacionID
			AND R.Activo = 1

	SET NOCOUNT OFF
END
GO
