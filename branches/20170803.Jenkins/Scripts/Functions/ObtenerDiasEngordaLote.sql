IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[ObtenerDiasEngordaLote]'))
  DROP FUNCTION [dbo].[ObtenerDiasEngordaLote]
GO
--=============================================

-- Author:		Roberto Aguilar Pozos

-- Create date: 2014-09-26

-- Origen: APInterfaces

-- Description:	Obtiene Los dias promedio de engorda de un Lote segun sus animales

-- select ObtenerDiasEngordaLote(1)

--=============================================
create function ObtenerDiasEngordaLote(
@LoteID int
)
returns int
AS
begin
	declare @DiasEngorda int
	SELECT @DiasEngorda = COALESCE((SUM(DATEDIFF(DAY, EG.FechaEntrada, GETDATE()))/COUNT(1)),0) 
   
	 FROM AnimalMovimiento(nolock) AM

	 INNER JOIN Animal(nolock) A ON A.AnimalID = AM.AnimalID

	 INNER JOIN EntradaGanado(nolock) EG ON A.FolioEntrada = EG.FolioEntrada

	 WHERE AM.Activo = 1 and AM.LoteID = @LoteID

	 return @DiasEngorda;
end
