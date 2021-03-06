USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerDatosDescargaDataLink]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ObtenerDatosDescargaDataLink]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ObtenerDatosDescargaDataLink]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jorge Luis Velazquez Araujo	
-- Create date: 02-09-2014
-- Description:	Consulta informacion de alimento del Corral
-- [Lote_ObtenerDatosDescargaDataLink] 1, 3679
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ObtenerDatosDescargaDataLink]
@OrganizacionId INT 
,@LoteID INT
AS

BEGIN 
SET NOCOUNT ON;
	
	declare @PesoInicio int
	
	set @PesoInicio = (select AVG(Peso) from AnimalMovimiento(NOLOCK) where LoteID = @LoteID AND Activo = 1 GROUP by LoteID )
	
	SELECT 		
		FechaInicio,
		LoteID,
		@PesoInicio AS PesoInicio,
		Cabezas
	FROM Lote
	WHERE 
	OrganizacionID = @OrganizacionID
	AND LoteID = @LoteID
SET NOCOUNT OFF;	
END


GO
