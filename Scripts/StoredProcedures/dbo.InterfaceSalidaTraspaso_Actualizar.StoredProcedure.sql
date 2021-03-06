USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspaso_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 26/02/2015
-- Description: Actualiza un registro en la tabla interface salida traspaso
-- SpName     : InterfaceSalidaTraspaso_Actualizar 
--======================================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspaso_Actualizar]
	@InterfaceSalidaTraspasoID INT
	,@CabezasEnvio INT
	,@PesoTara INT
	,@PesoBruto INT	
	,@UsuarioModificacionID INT
AS
BEGIN	
		if @PesoTara = 0
		set @PesoTara = null
		if @PesoBruto = 0
		set @PesoBruto = null
		update InterfaceSalidaTraspaso set CabezasEnvio = @CabezasEnvio, PesoTara = @PesoTara, PesoBruto = @PesoBruto,
											FechaModificacion = GETDATE(), UsuarioModificacionID = @UsuarioModificacionID
		WHERE InterfaceSalidaTraspasoID = @InterfaceSalidaTraspasoID
END

GO
