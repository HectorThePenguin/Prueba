IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[obtenerDiasUltimaFormulaCorral]'))
  DROP FUNCTION [dbo].[obtenerDiasUltimaFormulaCorral]
GO
--=============================================
-- Author:		Roberto Aguilar Pozos
-- Create date: 2014-09-30
-- Origen: APInterfaces
-- Description:	Obtiene Los dias promedio de engorda de un Lote segun sus animales
-- select obtenerDiasUltimaFormulaCorral(3)
--=============================================
create Function obtenerDiasUltimaFormulaCorral(
@CorralID int
)
returns int
AS
BEGIN
	declare @UltimaFormula int
	declare @UltimoID int
	declare @UltimaFecha date
	declare @UltimoLote int
	select @UltimaFormula = RD.FormulaIDServida, @UltimaFecha = R.Fecha,@UltimoLote = R.LoteID,
	@UltimoID = RD.RepartoID
	FROM RepartoDetalle(nolock)RD
	INNER JOIN Reparto(nolock) R on R.RepartoID = RD.RepartoID
	WHERE REpartoDetalleID = (select max(RD.RepartoDetalleID) 
							from RepartoDetalle(nolock) RD 
							inner join Reparto(nolock) Re on Re.RepartoID = RD.RepartoID 
							where CorralID = @CorralID and FormulaIDServida is not null)
	declare @IdREpartoFormulaAnterior int
	select @IdREpartoFormulaAnterior = isnull(max(Re.RepartoID),0) 
							from RepartoDetalle(nolock) RD 
							INNER JOIN Reparto(nolock) Re on Re.RepartoID = RD.RepartoID 
							WHERE Re.CorralID = @CorralID 
							AND (RD.FormulaIDServida != @UltimaFormula 
							OR Re.LoteID != @UltimoLote)
							AND FormulaIDServida is not null
							AND Re.RepartoID < @UltimoID
	declare @DiasUltimoFormula int

	select @DiasUltimoFormula = DATEDIFF(D,MIN(Re.Fecha),@UltimaFecha) from Reparto(nolock) Re 
	inner join RepartoDetalle(nolock) RD on RD.RepartoID = Re.RepartoID 
	where Re.RepartoID > @IdREpartoFormulaAnterior and Re.CorralID = @CorralID and RD.FormulaIDServida is not null
	
	Return @DiasUltimoFormula
END