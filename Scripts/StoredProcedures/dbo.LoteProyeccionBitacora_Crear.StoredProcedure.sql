USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccionBitacora_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteProyeccionBitacora_Crear]
GO
/****** Object:  StoredProcedure [dbo].[LoteProyeccionBitacora_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author      : Jorge Luis Velazquez Araujo
-- Create date : 05/06/2015
-- Sirve para guardar la Bitacora de Lote Proyeccion
-- SpName      : LoteProyeccionBitacora_Crear
--======================================================
CREATE PROCEDURE [dbo].[LoteProyeccionBitacora_Crear]
@LoteProyeccionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT LoteProyeccionBitacora (
		LoteProyeccionID
	,LoteID
	,OrganizacionID
	,Frame
	,GananciaDiaria
	,ConsumoBaseHumeda
	,Conversion
	,PesoMaduro
	,PesoSacrificio
	,DiasEngorda
	,FechaEntradaZilmax
	,FechaCreacion
	,UsuarioCreacionID
	,FechaModificacion
	,UsuarioModificacionID
	,Revision
	)
	SELECT 
	LoteProyeccionID
	,LoteID
	,OrganizacionID
	,Frame
	,GananciaDiaria
	,ConsumoBaseHumeda
	,Conversion
	,PesoMaduro
	,PesoSacrificio
	,DiasEngorda
	,FechaEntradaZilmax
	,FechaCreacion
	,UsuarioCreacionID
	,FechaModificacion
	,UsuarioModificacionID
	,Revision
	FROM LoteProyeccion
	where LoteProyeccionID = @LoteProyeccionID
	
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
