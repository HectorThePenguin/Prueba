USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_Resumen]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_Resumen]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_Resumen]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Eduardo Cota
-- Create date: 1/12/2014
-- Description  Obtiene de la formula ingresada y para la organizacion del usuario, cuanto hay en inventario y 
--              cuanto esta programada para repartir
-- SpName     : ProduccionFormula_Resumen 1, 9, 8
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_Resumen] 
@organizacionID int,
@formulaId int,
@tipoAlmacenId int
AS
BEGIN
	DECLARE @DescripcionFormula char(30)
	DECLARE @CantidadProducida decimal(18,2)
	DECLARE @CantidadReparto int
	SET NOCOUNT ON;
	--Obtiene la descripci�n de la formula
	SET @DescripcionFormula = 
							(
							SELECT 
								Descripcion
							FROM 
								Formula
							WHERE
								FormulaID = @FormulaID
							)
	--Obtiene la cantidad producida disponible en almacen
	SET @CantidadProducida = 
							(
							SELECT
								Cantidad
							FROM
								AlmacenInventario 
							WHERE
								ProductoID = 
											(
											select 
												ProductoID
											from
												Formula
											where
												FormulaID = @formulaId
											)
							AND
								AlmacenID = 
											(
											select	
												AlmacenID
											from
												Almacen
											where
												OrganizacionID = @organizacionID 
												and
												TipoAlmacenID = @TipoAlmacenID
											)
							)
	--Obtine la cantidad de formula a repartir en el dia 
	SET @CantidadReparto = 
							(
							SELECT 
								sum (RD.CantidadServida) as CantidadProducida
							FROM
								RepartoDetalle RD 
							inner join Reparto R on RD.RepartoID = R.RepartoID
							WHERE
								RD.FormulaIDServida = @formulaId 
								and
								OrganizacionID = @organizacionID
								and
								cast (R.Fecha as date)  = cast (getdate () as date)
							)
	SET NOCOUNT OFF;
	--Inserta los datos obtenido dentro de la tabla temporal Final
	CREATE TABLE #Final (DescripcionFormula char (30), CantidadProducida decimal (11,2), CantidadReparto int);
	INSERT INTO #Final VALUES(@DescripcionFormula, @CantidadProducida, @CantidadReparto); 
	--Obtiene la tabla final
	SELECT
		DescripcionFormula,
		COALESCE(CantidadProducida,0) AS CantidadProducida,
		COALESCE(CantidadReparto,0) AS CantidadReparto
	FROM
		#final
	WHERE
		1=1
	DROP TABLE #Final
END

GO
