USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerTipoGanadoDeLote]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerTipoGanadoDeLote]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerTipoGanadoDeLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/10/11
-- Description: SP para Obtener el tipo de ganado por lote
-- Origen     : APInterfaces
-- EXEC ReimplanteGanado_ObtenerTipoGanadoDeLote 1
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerTipoGanadoDeLote]
	@LoteID INT
AS
BEGIN
	
	DECLARE @sexo CHAR(1);
	DECLARE @pesoPromedio INT;
	
	/* Obtener los corrales reimplantados al dia */
	SELECT TOP 1 @pesoPromedio = (TGanados.PesoTotal/TGanados.TotalAnimales), @sexo = TGanados.Sexo
	  FROM (
			SELECT SUM(A.PesoCompra) PesoTotal, TG.TipoGanadoID, TG.Sexo, COUNT(1) TotalAnimales 
				FROM Animal A(NOLOCK)
			 INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
			 INNER JOIN TipoGanado TG on TG.TipoGanadoID = A.TipoGanadoID
			 WHERE AM.Activo = 1
				 AND AM.LoteID = @LoteID
			 GROUP BY TG.Sexo, TG.TipoGanadoID
	  ) AS TGanados 
	 ORDER BY TGanados.TotalAnimales DESC, TGanados.PesoTotal DESC;

	/* Se busca el tipo de ganado en base al sexo y peso promedio obtenido anteriormente */
	SELECT TOP 1 TGa.TipoGanadoID,
				TGa.Descripcion,
				TGa.Sexo,
				TGa.PesoMinimo,
				TGa.PesoMaximo,
				TGa.PesoSalida,
				TGa.Activo,
				TGa.FechaCreacion,
				TGa.UsuarioCreacionID,
				TGa.FechaModificacion,
				TGa.UsuarioModificacionID
	  FROM TipoGanado TGa 
	 WHERE TGa.Sexo = @sexo
	   AND @pesoPromedio BETWEEN  TGa.PesoMinimo	AND TGa.PesoMaximo 
	
END

GO
