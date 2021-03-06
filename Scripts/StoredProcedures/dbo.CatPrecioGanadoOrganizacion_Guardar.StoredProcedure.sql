USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CatPrecioGanadoOrganizacion_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CatPrecioGanadoOrganizacion_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[CatPrecioGanadoOrganizacion_Guardar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Rub�n Guzman
-- Create date: 19/08/2015
-- Description:  Actualizar las listas de precios
-- CatPrecioGanadoOrganizacion_Guardar ''
CREATE PROCEDURE [dbo].[CatPrecioGanadoOrganizacion_Guardar]
	@Precios Xml,
	@UsuarioId INT
AS
BEGIN 
SET NOCOUNT ON;
	SELECT
		OrganizacionId = t.item.value('./OrganizacionId[1]', 'INT'),
		TipoGanadoId =  t.item.value('./TipoGanadoId[1]', 'INT'),
		Precio = t.item.value('./Precio[1]', 'FLOAT'),
		Peso = t.item.value('./Peso[1]', 'FLOAT')
	INTO #Precios
	FROM @Precios.nodes('ROOT/Datos') AS T(item)
	-- EXISTEN, HAY QUE ACTUALIZAR
	SELECT 
		P.OrganizacionId,
		P.TipoGanadoId,
		P.Precio,
		P.Peso
	INTO #Existe 
	FROM #Precios P
	INNER JOIN SuKarne.dbo.CatPrecioGanadoOrganizacion PG (NOLOCK)
		ON PG.OrganizacionId = P.OrganizacionId AND PG.TipoGanadoId = P.TipoGanadoId
	UPDATE PG
	SET PG.PrecioPromedio = E.Precio, PG.PesoPromedio = E.Peso, UsuarioModificacionId = @UsuarioId, FechaModificacion = GETDATE()
	FROM SuKarne.dbo.CatPrecioGanadoOrganizacion PG
	INNER JOIN #Existe E
		ON E.OrganizacionId = PG.OrganizacionId AND E.TipoGanadoId = PG.TipoGanadoId
	-- NO EXISTEN, HAY QUE INSERTAR
	SELECT
		P.OrganizacionId,
		P.TipoGanadoId,
		P.Precio,
		P.Peso
	INTO #NoExiste 
	FROM #Precios P
	LEFT OUTER JOIN SuKarne.dbo.CatPrecioGanadoOrganizacion PG (NOLOCK)
		ON PG.OrganizacionId = P.OrganizacionId AND PG.TipoGanadoId = P.TipoGanadoId
	WHERE PG.TipoGanadoId IS NULL
	INSERT INTO SuKarne.dbo.CatPrecioGanadoOrganizacion(TipoGanadoId,OrganizacionId,PrecioPromedio,PesoPromedio,Activo,FechaCreacion,UsuarioCreacionId)
	SELECT TipoGanadoId, OrganizacionId, Precio, Peso, 1, GETDATE(), @UsuarioId FROM #NoExiste
	SELECT 1 AS Resultado
SET NOCOUNT OFF;
END

GO
