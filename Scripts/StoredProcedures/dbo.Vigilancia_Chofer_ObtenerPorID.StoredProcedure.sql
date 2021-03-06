USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Chofer_ObtenerPorID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Vigilancia_Chofer_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Vigilancia_Chofer_ObtenerPorID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 21/Mayo/2014
-- Description:  Consulta por Chofer ID y filtra por Tipo Transporte de Fletes
-- Vigilancia_Chofer_ObtenerPorID 19, 375
-- =============================================
CREATE PROCEDURE [dbo].[Vigilancia_Chofer_ObtenerPorID]	
@ChoferID INT,
@ProveedorID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1
	      C.ChoferID,
		  C.Nombre,
		  C.ApellidoPaterno,
		  C.ApellidoMaterno,
		  C.Activo
	 FROM ProveedorChofer P (NOLOCK) 
	INNER JOIN Chofer C ON P.ChoferID = C.ChoferID
	WHERE C.ChoferID = @ChoferID 
	  AND P.ProveedorID = @ProveedorID
	  AND P.Activo = 1
	SET NOCOUNT OFF;
END

GO
