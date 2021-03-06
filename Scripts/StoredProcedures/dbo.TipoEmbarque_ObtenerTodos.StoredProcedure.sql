USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoEmbarque_ObtenerTodos]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoEmbarque_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[TipoEmbarque_ObtenerTodos]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jos� Gilberto Quintero L�pez
-- Create date: 15/10/2013
-- Description:	Obtener listado de Tipo de embarque.
-- TipoEmbarque_ObtenerTodos
-- =============================================
CREATE PROCEDURE [dbo].[TipoEmbarque_ObtenerTodos]	
@Activo BIT = NULL	
AS
BEGIN
	SET NOCOUNT ON;
	Select TipoEmbarqueID,
			Descripcion,
			Activo
	From TipoEmbarque
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
