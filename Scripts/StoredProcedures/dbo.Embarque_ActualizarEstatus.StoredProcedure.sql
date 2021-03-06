USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ActualizarEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_ActualizarEstatus]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_ActualizarEstatus]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 07-11-2013
-- Description:	Actualiza el estatus de los embarques a recibido
-- Embarque_ActualizarEstatus 2, 2
-- =============================================
CREATE PROCEDURE [dbo].[Embarque_ActualizarEstatus]
	@EmbarqueID INT, 		
	@Estatus INT
AS
BEGIN	
	SET NOCOUNT ON;
    UPDATE Embarque
	SET Estatus = @Estatus	
	WHERE EmbarqueID = @EmbarqueID	  
END

GO
