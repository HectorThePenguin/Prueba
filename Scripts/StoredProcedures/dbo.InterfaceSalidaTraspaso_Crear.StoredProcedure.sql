USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[InterfaceSalidaTraspaso_Crear]
GO
/****** Object:  StoredProcedure [dbo].[InterfaceSalidaTraspaso_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 27/08/2014
-- Description: Crea un registro en la tabla interface salida traspaso
-- SpName     : InterfaceSalidaTraspaso_Crear 1, 2, 1, '123', '132', 1, 10, 10, 2, 1, 5, 10, 1, 1
--======================================================
CREATE PROCEDURE [dbo].[InterfaceSalidaTraspaso_Crear]
	@OrganizacionID INT,
	@OrganizacionIDDestino INT,
	@FolioTraspaso INT,
	@CabezasEnvio INT,
	@TraspasoGanado BIT,
	@SacrifioGanado BIT,
	@PesoTara INT,
	@PesoBruto INT,
	@Activo BIT,
	@UsuarioCreacionID INT
AS
BEGIN	
		if @PesoTara = 0
		set @PesoTara = null

		if @PesoBruto = 0
		set @PesoBruto = null

		INSERT INTO InterfaceSalidaTraspaso 
		(OrganizacionID, 
		OrganizacionIDDestino, 
		FolioTraspaso, 
		CabezasEnvio, 
		FechaEnvio, 
		PesoTara,
		PesoBruto,
		Activo, 
		FechaCreacion, 
		UsuarioCreacionID
		,TraspasoGanado
		,SacrificioGanado)
		 VALUES (
		 @OrganizacionID, 
		 @OrganizacionIDDestino, 
		 @FolioTraspaso, 
		 @CabezasEnvio, 
		 GETDATE(), 
		 @PesoTara,
		 @PesoBruto,
		 @Activo, 
		 GETDATE(), 
		 @UsuarioCreacionID
		 ,@TraspasoGanado
		 ,@SacrifioGanado)

	SELECT InterfaceSalidaTraspasoID,
		OrganizacionID,
		OrganizacionIDDestino,
		FolioTraspaso,
		FechaEnvio,
		CabezasEnvio,
		SacrificioGanado,
		TraspasoGanado,
		PesoTara,
		PesoBruto,
		Activo
		, TraspasoGanado
		, SacrificioGanado
	FROM InterfaceSalidaTraspaso WHERE InterfaceSalidaTraspasoID = @@IDENTITY

	
END
GO
