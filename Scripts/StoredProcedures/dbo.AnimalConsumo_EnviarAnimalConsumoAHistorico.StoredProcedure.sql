USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AnimalConsumo_EnviarAnimalConsumoAHistorico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AnimalConsumo_EnviarAnimalConsumoAHistorico]
GO
/****** Object:  StoredProcedure [dbo].[AnimalConsumo_EnviarAnimalConsumoAHistorico]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 30/12/2014
-- Description:  Enviar Animal Costo a AnimalConsumoHistorico
-- AnimalConsumo_EnviarAnimalConsumoAHistorico
-- =============================================
CREATE PROCEDURE [dbo].[AnimalConsumo_EnviarAnimalConsumoAHistorico]
@AnimalID BIGINT
AS
  BEGIN
    SET NOCOUNT ON
		INSERT INTO AnimalConsumoHistorico
		(
			AnimalConsumoID
			, AnimalID
			, RepartoID
			, FormulaIDServida
			, Cantidad
			, TipoServicioID
			, Fecha
			, Activo
			, FechaCreacion
			, UsuarioCreacionID
			, FechaModificacion
			, UsuarioModificacionID
		)
		SELECT AnimalConsumoID
			, AnimalID
			, RepartoID
			, FormulaIDServida
			, Cantidad
			, TipoServicioID
			, Fecha
			, Activo
			, FechaCreacion
			, UsuarioCreacionID
			, FechaModificacion
			, UsuarioModificacionID
		FROM AnimalConsumo
		WHERE AnimalID = @AnimalID
	SET NOCOUNT OFF
END

GO
