USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ActualizarCantidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_ActualizarCantidad]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_ActualizarCantidad]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 10/12/2014
-- Description: Actualiza la cantidad y precio del contrato
-- Contrato_ActualizarCantidad 
-- =============================================
CREATE PROCEDURE [dbo].[Contrato_ActualizarCantidad]		
	@ContratoID INT,
	@Cantidad INT,
	@Precio decimal(18,4),	
	@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Contrato
			SET Cantidad = Cantidad + @Cantidad,
			Precio = @Precio,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID
			WHERE ContratoID = @ContratoID
	SET NOCOUNT OFF;
END

GO
