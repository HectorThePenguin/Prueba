USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_ObtenerCheckListPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CheckListCorral_ObtenerCheckListPorLote]
GO
/****** Object:  StoredProcedure [dbo].[CheckListCorral_ObtenerCheckListPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 08/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CheckListCorral_ObtenerCheckListPorLote
--======================================================
CREATE PROCEDURE [dbo].[CheckListCorral_ObtenerCheckListPorLote]
@OrganizacionID INT
,@LoteID INT
AS
SELECT
CheckListCorralID
,OrganizacionID
,LoteID
,PDF
,Activo
FROM CheckListCorral
WHERE 
OrganizacionID = @OrganizacionID
AND LoteID = @LoteID
AND Activo = 1

GO
