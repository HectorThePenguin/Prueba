IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[obtenerTipoGanadoLote]'))
  DROP FUNCTION [dbo].[obtenerTipoGanadoLote]
GO
--=============================================
-- Author:		Roberto Aguilar Pozos
-- Create date: 2014-09-26
-- Origen: APInterfaces
-- Description:	Obtiene El tipo de Ganado de un lote segun el sexo y promedio de pesos
-- select obtenerTipoGanadoLote(3)
--=============================================
create Function obtenerTipoGanadoLote(
@LoteID int
)
returns varchar(50)
as
begin
declare @sexo char(1)
declare @SumaPeso int
declare @tipoGanado varchar(50)
--Verificamos que el corral tenga lote activo
if Exists(select LoteId from Lote(nolock) where LoteID = @LoteID and Activo = 1)
	begin
	declare @AnimalMuestra int
	--Se toma un animal del lote activo del corral
	select top 1 @AnimalMuestra = animalID 
	from AnimalMovimiento(nolock) 
	where LoteID = @LoteID
	--Se obtiene el sexo del corral (sexo del animal muestra)
	select @sexo = sexo 
	from TipoGanado(nolock) TG
	inner join animal(nolock) A on A.TipoGanadoID = TG.TipoGanadoID
	where A.AnimalID = @AnimalMuestra
	--Se obtiene el peso total de los animales en el lote activo del corral
	select @SumaPeso = sum(Peso)
	from AnimalMovimiento(nolock) Am 
	where Am.LoteID = @LoteID and Am.Activo = 1
	--Se Obtiene el tipo de ganado
	select @tipoGanado  = TG.Descripcion
		from TipoGanado(nolock) TG 
	inner join Lote(nolock) L on @SumaPeso/L.Cabezas between TG.PesoMinimo and TG.PesoMaximo and TG.Sexo = @sexo
	where l.LoteID = @LoteID
	end
else
	begin --En caso de que el corral no tenga lote activo, no se puede obtener un tipo de ganado y se devuelve nulo
	set @tipoGanado = ''
	end
	return @tipoGanado
end