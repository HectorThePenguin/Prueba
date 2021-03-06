USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GenerarProyeccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GenerarProyeccion]
GO
/****** Object:  StoredProcedure [dbo].[GenerarProyeccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--==========================================================
-- Author     : Jesus Alberto Garcia Reyes
-- Create date: 2014/01/14  
-- Description: SP Que genera los datos para la proyeccion
-- GenerarProyeccion
-- Ejemplo    :	EXEC dbo.GenerarProyeccion 38315, 4, '2015-08-02 08:56:00', 'H', 287, 80
--==========================================================
CREATE PROCEDURE [dbo].[GenerarProyeccion]
@LoteID INT, @OrganizacionID INT, @FechaInicio SMALLDATETIME, @Sexo CHAR(1), @PesoOrigen INT, @Merma DECIMAL(5,2)
AS
BEGIN
SET NOCOUNT ON
DECLARE @MermaMaxima decimal(18,2)
set @MermaMaxima = (select top 1 CAST(ISNULL(Valor,0) AS DECIMAL(5,2)) from ParametroGeneral pg 
						inner join Parametro pa on pg.ParametroID = pa.ParametroID where pa.Clave = 'MermaMaxima' )
if @MermaMaxima is not NULL
BEGIN
	if @Merma > @MermaMaxima
	begin
		set @Merma = @MermaMaxima
	end
END
--DECLARE @LoteID INT, @OrganizacionID INT, @FechaInicio SMALLDATETIME, @Sexo CHAR(1), @PesoOrigen INT, @Merma DECIMAL(5,2)
--SET @LoteID = 1
--SET @OrganizacionID = 4
--SET @FechaInicio = '20140101'
--SET @Sexo = 'M'
--SET @PesoOrigen = 210
--SET @Merma = 9.52
--Declaramos variables para las ecuaciones
DECLARE @Var1 DECIMAL(15, 10), @Var2 DECIMAL(15, 10), @Var3 DECIMAL(15, 10), @Var4 DECIMAL(15, 10), @Var5 DECIMAL(15, 10)
--Declaramos las variables de los datos a regresa
DECLARE @Frame DECIMAL(4, 2), @GDP DECIMAL(4, 2), @CBH DECIMAL(5, 2), @CONV DECIMAL(4, 2), @PesoMaduro DECIMAL(5, 2), @PesoSacrificio DECIMAL(5, 2),
@DiasEngorda DECIMAL(5, 2), @DiasImplante1 INT, @DiasImplante2 INT, @ZILMAX SMALLDATETIME
--Calculamos Frame
SELECT
	@Var1 = dbo.ObtenerConstanteValor('FRAME', @OrganizacionID, 1, @Sexo)
SELECT
	@Var2 = dbo.ObtenerConstanteValor('FRAME', @OrganizacionID, 2, @Sexo)
SELECT
	@Var3 = dbo.ObtenerConstanteValor('FRAME', @OrganizacionID, 3, @Sexo)
SELECT
	@Var4 = dbo.ObtenerConstanteValor('FRAME', @OrganizacionID, 4, @Sexo)
SELECT
	@Frame = @Var1 + (@Var4 * @PesoOrigen) - (@Var2 * CASE @Sexo WHEN 'M' THEN 1 ELSE 2 END) - (@Var3 * @Merma)
--Calculamos Ganancia Diaria Promedio
SELECT
	@Var1 = dbo.ObtenerConstanteValor('GDP', @OrganizacionID, 1, @Sexo)
SELECT
	@Var2 = dbo.ObtenerConstanteValor('GDP', @OrganizacionID, 2, @Sexo)
SELECT
	@Var3 = dbo.ObtenerConstanteValor('GDP', @OrganizacionID, 3, @Sexo)
SELECT
	@Var4 = dbo.ObtenerConstanteValor('GDP', @OrganizacionID, 4, @Sexo)
SELECT
	@GDP = @Var1 - (@Var2 * @Frame) + (@Var3 * @PesoOrigen) - (@Var4 * POWER(@PesoOrigen, 2))
--Calculamos Consumo Base Humeda
SELECT
	@Var1 = dbo.ObtenerConstanteValor('CBH', @OrganizacionID, 1, @Sexo)
SELECT
	@Var2 = dbo.ObtenerConstanteValor('CBH', @OrganizacionID, 2, @Sexo)
SELECT
	@Var3 = dbo.ObtenerConstanteValor('CBH', @OrganizacionID, 3, @Sexo)
SELECT
	@Var4 = dbo.ObtenerConstanteValor('CBH', @OrganizacionID, 4, @Sexo)
SELECT
	@Var5 = dbo.ObtenerConstanteValor('CBH', @OrganizacionID, 5, @Sexo)
SELECT
	@CBH = (@Var1 + (@Var2 * @PesoOrigen) - (@Var3 * @Frame) - (@Var4 * CASE @Sexo
		WHEN 'M' THEN 1
		ELSE 2
	END)) / @Var5
--Calculamos Conversion
SELECT
	@CONV = @CBH / @GDP
--Calculamos Peso Maduro
SELECT
	@Var1 = dbo.ObtenerConstanteValor('PESOMADURO', @OrganizacionID, 1, @Sexo)
SELECT
	@Var2 = dbo.ObtenerConstanteValor('PESOMADURO', @OrganizacionID, 2, @Sexo)
SELECT
	@Var3 = dbo.ObtenerConstanteValor('PESOMADURO', @OrganizacionID, 3, @Sexo)
SELECT
	@Var4 = dbo.ObtenerConstanteValor('PESOMADURO', @OrganizacionID, 4, @Sexo)
SELECT
	@Var5 = dbo.ObtenerConstanteValor('PESOMADURO', @OrganizacionID, 5, @Sexo)
SELECT
	@PesoMaduro = (@Var1 + (CASE @Sexo
		WHEN 'M' THEN 1
		ELSE -1
	END * (@Var2 * (@PesoOrigen * @Var3))) + (@var5 * POWER((@PesoOrigen * @Var3), 2)) - (@Var4 * @Frame)) / @Var3
--Calculamos Peso Sacrificio
SELECT
	@Var1 = dbo.ObtenerConstanteValor('PESOSACR', @OrganizacionID, 1, @Sexo)
SELECT
	@PesoSacrificio = @Var1 * @PesoMaduro
--Calculamos Dias Engorda
SELECT
	@DiasEngorda = ROUND((@PesoSacrificio - @PesoOrigen) / @GDP, 0)
--Calculamos Dias Implante
SELECT
	@DiasImplante1 = DiasImplante1,
	@DiasImplante2 = DiasImplante2
FROM ConfiguracionImplante
WHERE @DiasEngorda BETWEEN DiasMin AND DiasMax
--Calculamos Entrada Zilmax
SELECT
	@ZILMAX = DATEADD(DAY, -1 * CAST((SELECT
		dbo.ObtenerConstanteValor('ZILMAX', @OrganizacionID, 1, @Sexo))
	AS INT), DATEADD(DAY, CAST(@DiasEngorda AS INT), @FechaInicio))
--Regresamos datos proyeccion
SELECT
	@LoteID AS LoteID,
	@OrganizacionID AS OrganizacionID,
	@Frame AS Frame,
	@GDP AS GananciaDiaria,
	@CBH AS ConsumoBase,
	@CONV AS Conversion,
	CAST(ROUND(@PesoMaduro, 0) AS INT) AS PesoMaduro,
	CAST(ROUND(@PesoSacrificio, 0) AS INT) AS PesoSacrificio,
	CAST(@DiasEngorda AS INT) AS DiasEngorda,
	DATEADD(DAY, @DiasEngorda, @FechaInicio) AS FechaSacrificio,
	@ZILMAX AS EntradaZilmax
--Regresamos datos reimplantes
SELECT
	1 AS PrimerImplante,
	CASE @DiasImplante1
		WHEN 0 THEN NULL
		ELSE DATEADD(DAY, @DiasImplante1, @FechaInicio)
	END AS FechaProyectada1,
	CASE @DiasImplante1
		WHEN 0 THEN NULL
		ELSE CAST(ROUND(@PesoOrigen + (@DiasImplante1 * @GDP), 0) AS INT)
	END AS PesoProyectado1,
	2 AS SegundoImplante,
	CASE @DiasImplante2
		WHEN 0 THEN NULL
		ELSE DATEADD(DAY, @DiasImplante2, @FechaInicio)
	END AS FechaProyectada2,
	CASE @DiasImplante2
		WHEN 0 THEN NULL
		ELSE CAST(ROUND(@PesoOrigen + (@DiasImplante2 * @GDP), 0) AS INT)
	END AS PesoProyectado2
SET NOCOUNT OFF
END

GO
