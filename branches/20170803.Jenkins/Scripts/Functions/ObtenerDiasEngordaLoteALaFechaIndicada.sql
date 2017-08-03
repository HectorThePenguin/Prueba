IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[ObtenerDiasEngordaLoteALaFechaIndicada]'))
  DROP FUNCTION [dbo].[ObtenerDiasEngordaLoteALaFechaIndicada]
GO
--=============================================
-- Author:		Ramses Santos
-- Create date: 2014-09-26
-- Origen: APInterfaces
-- Description:	Obtiene Los dias promedio de engorda de un Lote segun sus animales
-- select dbo.ObtenerDiasEngordaLoteALaFechaIndicada(23, '2014-07-02')
--=============================================
CREATE FUNCTION ObtenerDiasEngordaLoteALaFechaIndicada (
	@LoteID INT,
	@Fecha DATE
)
RETURNS INT
AS
BEGIN
	 DECLARE @DiasEngorda INT

	 SELECT @DiasEngorda = COALESCE((SUM(DATEDIFF(DAY, EG.FechaEntrada, @Fecha))/COUNT(1)),0)
	 FROM AnimalMovimiento(nolock) AM
	 INNER JOIN Animal(nolock) A ON A.AnimalID = AM.AnimalID
	 INNER JOIN EntradaGanado(nolock) EG ON A.FolioEntrada = EG.FolioEntrada
	 WHERE AM.Activo = 1 and AM.LoteID = @LoteID

	 RETURN @DiasEngorda;
END
