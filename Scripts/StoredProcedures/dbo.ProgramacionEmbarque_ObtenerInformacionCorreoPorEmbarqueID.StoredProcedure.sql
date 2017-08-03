USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerInformacionCorreoPorEmbarqueID]    Script Date: 31/05/2017 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerInformacionCorreoPorEmbarqueID]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionEmbarque_ObtenerInformacionCorreoPorEmbarqueID]    Script Date: 31/05/2017 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sandoval Toledo Jesús Alejandro 
-- Create date: 29-06-2017 
-- Description: Procedimiento almacenado que guarda la
-- informacion ingresada en la seccion de datos.
-- SpName: 
/* 
ProgramacionEmbarque_ObtenerInformacionCorreoPorEmbarqueID 15249
*/
--======================================================
CREATE PROCEDURE [dbo].[ProgramacionEmbarque_ObtenerInformacionCorreoPorEmbarqueID]
@EmbarqueID			INT
AS
BEGIN
	SELECT 	emb.EmbarqueID, emb.OrganizacionOrigenID, oo.Descripcion AS OrganizacionOrigen, emb.OrganizacionDestinoID, 
					od.Descripcion AS OrganizacionDestino, emb.TipoEmbarqueID, te.Descripcion AS TipoEmbarque, emb.CitaCarga,
					emb.ResponsableEmbarque
	FROM Embarque emb (NOLOCK)
	INNER JOIN Organizacion oo (NOLOCK) ON (emb.OrganizacionOrigenID = oo.OrganizacionID)
	INNER JOIN Organizacion od (NOLOCK) ON (emb.OrganizacionDestinoID = od.OrganizacionID)
	INNER JOIN TipoEmbarque te (NOLOCK) ON (emb.TipoEmbarqueID = te.TipoEmbarqueID)
	WHERE EmbarqueID = @EmbarqueID
	AND emb.Activo = 1
END