USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FormaPago_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FormaPago_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[FormaPago_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================  
-- Author     : Sergio Alberto Gamez Gomez
-- Create date: 23/07/2016
-- Description: Obtener todas las formas de pagos activas
-- SpName     : FormaPago_ObtenerTodos
--======================================================  
CREATE PROCEDURE [dbo].[FormaPago_ObtenerTodos]
AS  
BEGIN  
	SET NOCOUNT ON; 
	SELECT
		FormaPagoID,
		Descripcion = UPPER(Descripcion)
	FROM FormaPago (NOLOCK) 
	WHERE Activo = 1
	SET NOCOUNT OFF;  
END

GO
