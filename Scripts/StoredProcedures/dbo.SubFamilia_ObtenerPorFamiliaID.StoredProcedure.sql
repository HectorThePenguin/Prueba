USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorFamiliaID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SubFamilia_ObtenerPorFamiliaID]
GO
/****** Object:  StoredProcedure [dbo].[SubFamilia_ObtenerPorFamiliaID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : SubFamilia_ObtenerPorFamiliaID
--======================================================
CREATE PROCEDURE [dbo].[SubFamilia_ObtenerPorFamiliaID] @FamiliaID INT
AS
SET NOCOUNT ON
SELECT fa.FamiliaID
	,fa.Descripcion [Familia]
	,sf.SubFamiliaID
	,sf.Descripcion
	,sf.Activo
FROM SubFamilia sf
INNER JOIN Familia fa ON sf.FamiliaID = fa.FamiliaID
WHERE fa.FamiliaID = @FamiliaID
SET NOCOUNT OFF

GO
