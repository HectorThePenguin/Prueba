USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradaPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerEntradaPorLote]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradaPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ramses Santos
-- Create date: 31-03-2014
-- Description:	Obtiene una entrada de ganado por loteID corralID y organizacionID 
-- exec EntradaGanado_ObtenerEntradaPorLote 32953, 933, 1
--001 Jorge Luis Velazquez Araujo **Se agrega que regrese la columna EmbarqueID
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerEntradaPorLote]
	@LoteID INT,
	@CorralID INT,
	@OrganizacionID INT
AS
BEGIN	
		SELECT eng.FolioEntrada, 
			   eng.OrganizacionOrigenID, 
			   O.TipoOrganizacionID, 
			   eng.PesoBruto, 
			   eng.PesoTara, 
			   eng.CabezasRecibidas,
			   eng.FechaEntrada,
			   eng.EmbarqueID,--001 
			   eng.EsRuteo --001
		FROM EntradaGanado (NOLOCK) AS eng 
		INNER JOIN Organizacion (NOLOCK) AS O ON (O.OrganizacionID = eng.OrganizacionOrigenID AND O.Activo = 1)
		WHERE eng.LoteID = @LoteID AND eng.CorralID = @CorralID 
		AND eng.OrganizacionID = @OrganizacionID AND eng.Activo = 1
END

GO
