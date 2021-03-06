USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ActualizarCosteo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_ActualizarCosteo]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_ActualizarCosteo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 16-10-2013
-- Description:	Actualiza una entrada de ganado
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_ActualizarCosteo]		
	@EntradaGanadoID INT, 
	@Usuario INT,
	@Costeado BIT
AS
BEGIN	
	SET NOCOUNT ON;
    Update EntradaGanado 
	SET 
		FechaModificacion = Getdate(), 
		UsuarioModificacionID = @Usuario,
		Costeado = @Costeado		
	WHERE EntradaGanadoID = @EntradaGanadoID
END

GO
