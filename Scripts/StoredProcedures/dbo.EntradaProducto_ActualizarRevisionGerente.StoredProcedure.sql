USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarRevisionGerente]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_ActualizarRevisionGerente]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_ActualizarRevisionGerente]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Gilberto Carranza
-- Create date: 08/12/2014
-- Description:	Actualiza un registro de entrada producto
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_ActualizarRevisionGerente]
@EntradaProductoID INT 
AS  
BEGIN
	
	UPDATE EntradaProducto
	SET REvisado = 1
	WHERE EntradaProductoID = @EntradaProductoID

END

GO
