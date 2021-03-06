USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoSobrante_ObtenerSobrantePorEntradaGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoSobrante_ObtenerSobrantePorEntradaGanado]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoSobrante_ObtenerSobrantePorEntradaGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		César Valdez
-- Create date: 2014-08-12
-- Description:	Obtiene el ganado sobrante de una entrada
-- EntradaGanadoSobrante_ObtenerSobrantePorEntradaGanado 1
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanadoSobrante_ObtenerSobrantePorEntradaGanado]
	@EntradaGanadoID INT
AS
BEGIN	
	SELECT
		EGS.EntradaGanadoSobranteID,
		EGS.EntradaGanadoID,
		EGS.AnimalID,
		COALESCE(A.PesoCompra,0) 'PesoCompra',
		EGS.Importe,
		EGS.Costeado,
		EGS.Activo,
		EGS.FechaCreacion,
		EGS.UsuarioCreacionID
	  FROM EntradaGanadoSobrante EGS
     INNER JOIN Animal A(NOLOCK) ON A.AnimalID = EGS.AnimalID
     WHERE EGS.EntradaGanadoID = @EntradaGanadoID
END

GO
