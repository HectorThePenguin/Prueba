USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradaPorLoteCorralOrganiziacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ObtenerEntradaPorLoteCorralOrganiziacion]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ObtenerEntradaPorLoteCorralOrganiziacion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Carlos Cazarez
-- Create date: 02-12-2015
-- Description:	Obtiene una entrada de ganado por loteID corralID y organizacionID 
-- exec EntradaGanado_ObtenerEntradaPorLoteCorralOrganiziacion 52582, 7438, 4
--001 Jorge Luis Velazquez Araujo **Se agrega que regrese la columna EmbarqueID
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ObtenerEntradaPorLoteCorralOrganiziacion]
	@LoteID INT,
	@CorralID INT,
	@OrganizacionID INT
AS
BEGIN	
		SELECT eng.EntradaGanadoID,
				 eng.FolioEntrada, 
			   eng.OrganizacionOrigenID,
				 eng.OrganizacionID,
				 eng.FolioOrigen,
				 eng.CorralID,
				 eng.LoteID,
			   O.TipoOrganizacionID, 
			   eng.PesoBruto, 
			   eng.PesoTara, 
			   eng.CabezasRecibidas,
				 eng.CabezasOrigen,
			   eng.FechaEntrada,
				 eng.FechaSalida,
				 eng.ChoferID,
				 eng.JaulaID,
				 eng.CamionID,
				 eng.OperadorID,
			   eng.EmbarqueID,--001 
			   eng.EsRuteo, --001
				 eng.Fleje,
				 eng.Checklist
		FROM EntradaGanado (NOLOCK) AS eng 
		INNER JOIN Organizacion (NOLOCK) AS O ON (O.OrganizacionID = eng.OrganizacionOrigenID AND O.Activo = 1)
		WHERE eng.LoteID = @LoteID AND eng.CorralID = @CorralID 
		AND eng.OrganizacionID = @OrganizacionID AND eng.Activo = 1
END
GO