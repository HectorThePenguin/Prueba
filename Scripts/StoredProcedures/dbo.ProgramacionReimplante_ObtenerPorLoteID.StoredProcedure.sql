USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_ObtenerPorLoteID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionReimplante_ObtenerPorLoteID]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_ObtenerPorLoteID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Pedro.Delgado
-- Create date: 2014-04-01
-- Origen: APInterfaces
-- Description:	Obtiene la lista de programaci�n reimplante por el lote
-- EXEC ProgramacionReimplante_ObtenerPorLoteID 1,4
--=============================================
CREATE PROCEDURE [dbo].[ProgramacionReimplante_ObtenerPorLoteID]
@LoteID INT,
@OrganizacionID INT
AS
BEGIN
	SELECT 
		FolioProgramacionID,
		OrganizacionID,
		Fecha,
		LoteID,
		/*CorralDestinoID,*/
		ProductoID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM ProgramacionReimplante (NOLOCK)
	WHERE Activo = 1 AND LoteID = @LoteID AND OrganizacionID = @OrganizacionID
END

GO
