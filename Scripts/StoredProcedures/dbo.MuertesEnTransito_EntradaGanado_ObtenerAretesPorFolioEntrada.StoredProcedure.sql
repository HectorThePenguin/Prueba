USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_EntradaGanado_ObtenerAretesPorFolioEntrada]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[MuertesEnTransito_EntradaGanado_ObtenerAretesPorFolioEntrada]
GO
/****** Object:  StoredProcedure [dbo].[MuertesEnTransito_EntradaGanado_ObtenerAretesPorFolioEntrada]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Alejandro Quiroz
-- Create date: 01/12/2014
-- Description: 
-- SpName     : MuertesEnTransito_EntradaGanado_ObtenerAretesPorFolioEntrada 4513,1
--======================================================
CREATE PROCEDURE [dbo].[MuertesEnTransito_EntradaGanado_ObtenerAretesPorFolioEntrada]
@FolioEntrada AS Int,
@OrganizacionID AS Int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ISA.Arete, CONVERT(BIT, COALESCE(AN.Activo,0)) Activo
	FROM  InterfaceSalidaAnimal ISA 
	inner join EntradaGanado EG ON EG.FolioOrigen = ISA.SalidaID
											 AND EG.OrganizacionOrigenID = ISA.OrganizacionID
	LEFT JOIN Animal AN ON ISA.AnimalID = AN.AnimalID AND AN.Activo = 1
	where EG.FolioEntrada = @FolioEntrada
	AND EG.Activo = 1
	AND EG.OrganizacionID = @OrganizacionID
	AND COALESCE(ISA.AnimalID,0) = 0
	
	
	SET NOCOUNT OFF;
END

GO
