USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorCorralDisponible]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerPorCorralDisponible]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerPorCorralDisponible]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 06/01/2014 12:00:00 a.m.
-- Description:
-- SpName     : EntradaGanado_ObtenerPorCorralDisponible 4,36,1
--======================================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerPorCorralDisponible]
@OrganizacionID int,
@CorralID int,
@EmbarqueID int
AS
BEGIN
	Select COUNT(eg.EntradaGanadoID) as [TotalRegistros] 
	From EntradaGanado eg
	Inner join Lote l on l.LoteID = eg.LoteID
	Where eg.OrganizacionID = @OrganizacionID
	And eg.CorralID = @CorralID
	And eg.Activo = 1 
    And l.Activo = 1 
	And eg.EmbarqueID != @EmbarqueID
END

GO
