USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Contrato_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Contrato_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 19/06/2015
-- Description: Actualizar los datos del Contrato
-- Contrato_Actualizar 
-- =============================================
CREATE PROCEDURE [dbo].[Contrato_Actualizar]		
	@ContratoID INT,
	@EstatusID INT,
	@Activo INT,
	@Tolerancia decimal(10,2),
	@Cantidad int,
	@PesoNegociar varchar(10),
	@FolioAserca varchar(15),
	@FolioCobertura int,
	@CalidadOrigen bit,
	@AplicaDescuento bit,
	@TipoFleteID int,
	@UsuarioModificacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE Contrato
			SET EstatusID = @EstatusID,
			Activo = @Activo,
			Tolerancia = @Tolerancia,
			Cantidad = @Cantidad,
			PesoNegociar = @PesoNegociar,
			FolioAserca = @FolioAserca,
			FolioCobertura = @FolioCobertura,
			CalidadOrigen = @CalidadOrigen,
			AplicaDescuento = @AplicaDescuento,
			TipoFleteID = @TipoFleteID,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = @UsuarioModificacionID
			WHERE ContratoID = @ContratoID
	SET NOCOUNT OFF;

END

GO
