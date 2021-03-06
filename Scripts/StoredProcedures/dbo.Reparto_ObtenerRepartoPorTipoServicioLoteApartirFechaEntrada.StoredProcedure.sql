USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoPorTipoServicioLoteApartirFechaEntrada]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerRepartoPorTipoServicioLoteApartirFechaEntrada]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoPorTipoServicioLoteApartirFechaEntrada]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 11/04/2014
-- Description:  Obtener Los repartos De un Lote por tipo de servicio apartir de la fehca entrada
-- Origen: APInterfaces
-- Reparto_ObtenerRepartoPorTipoServicioLoteApartirFechaEntrada 4,5,3,1,'2014-04-08',1
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerRepartoPorTipoServicioLoteApartirFechaEntrada]
	@OrganizacionID INT,
	@TipoFormulaID INT,
	@TipoServicioID INT,
	@LoteID INT,
	@FechaEntrada DATETIME,
	@Activo BIT
AS
  BEGIN
    SET NOCOUNT ON
	SELECT R.RepartoID, R.Fecha, RD.FormulaIDServida, RD.CantidadServida
	  FROM Reparto R
	 INNER JOIN RepartoDetalle RD ON R.RepartoID = RD.RepartoID
	 INNER JOIN Formula F ON RD.FormulaIDServida = F.FormulaID 
	 INNER JOIN Producto P ON P.ProductoID = F.ProductoID
	 WHERE RD.TipoServicioID = @TipoServicioID
	   AND F.TipoFormulaID = @TipoFormulaID
	   AND R.OrganizacionID = @OrganizacionID
	   AND R.LoteID = @LoteID
	   AND P.Activo = @Activo
	   AND CAST(R.Fecha AS DATE) BETWEEN CAST(@FechaEntrada AS DATE) AND CAST(GETDATE() AS DATE)
	   AND P.Activo = F.Activo
	SET NOCOUNT OFF
  END

GO
