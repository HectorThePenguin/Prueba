USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ActualizarEstatusDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_ActualizarEstatusDetalle]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ActualizarEstatusDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 07-11-2013
-- Description:	Actualiza el estatus de los embarques a recibido
-- Embarque_ActualizarEstatusDetalle 2, 2
-- =============================================
CREATE PROCEDURE [dbo].[Embarque_ActualizarEstatusDetalle]
	@EmbarqueID INT, 	
	@OrganizacionOrigenID INT
AS
BEGIN	
	SET NOCOUNT ON;
    UPDATE EmbarqueDetalle 
	SET Recibido = 1
	FROM Embarque PE
	INNER JOIN EmbarqueDetalle PED
		ON PE.EmbarqueID = PED.EmbarqueID
	WHERE PE.EmbarqueID = @EmbarqueID
	  AND PED.OrganizacionOrigenID  = @OrganizacionOrigenID	 
END

GO
