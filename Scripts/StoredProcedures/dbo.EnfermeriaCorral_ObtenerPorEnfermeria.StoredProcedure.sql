USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EnfermeriaCorral_ObtenerPorEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EnfermeriaCorral_ObtenerPorEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[EnfermeriaCorral_ObtenerPorEnfermeria]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Vel�zquez Araujo
-- Create date: 06/06/2014
-- Description:  Obtiene los Corrales de una Enfermeria
-- Origen: APInterfaces
-- EnfermeriaCorral_ObtenerPorEnfermeria 3
CREATE PROCEDURE [dbo].[EnfermeriaCorral_ObtenerPorEnfermeria] @EnfermeriaID INT
AS
SELECT 
ec.EnfermeriaCorralID
,ec.CorralID
	,co.Codigo
	,ec.Activo
FROM EnfermeriaCorral ec
INNER JOIN Corral co ON ec.CorralID = co.CorralID
where ec.EnfermeriaID = @EnfermeriaID

GO
