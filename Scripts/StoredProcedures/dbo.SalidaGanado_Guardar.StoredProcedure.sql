USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanado_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaGanado_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[SalidaGanado_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 07/04/2014
-- Description:  Guardar el Salida Ganado
-- Origen: APInterfaces
-- SalidaGanado_Guardar 8,4,12,7,1,1
-- =============================================
CREATE PROCEDURE [dbo].[SalidaGanado_Guardar]
	@TipoMovimientoID INT,
	@OrganizacionID INT,
	@VentaGanadoID INT,
	@TipoFolio INT,
	@Activo BIT,
	@UsuarioCreacionID INT
AS
  BEGIN
      SET NOCOUNT ON
	DECLARE @FolioSalida INT ;
	DECLARE @IdentityID BIGINT;
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @FolioSalida OUTPUT
	INSERT INTO SalidaGanado (OrganizacionID, 
							  TipoMovimientoID, 
							  Fecha, 
							  VentaGanadoID,
							  Folio, 
							  Activo, 
							  UsuarioCreacionID, 
							  FechaCreacion) 
	VALUES (@OrganizacionID, 
	        @TipoMovimientoID, 
			GETDATE(), 
			@VentaGanadoID,
			@FolioSalida, 
			@Activo , 
			@UsuarioCreacionID, 
			GETDATE())
	/* Se obtiene el id Insertado */
	SET @IdentityID = (SELECT @@IDENTITY)
	SELECT SalidaGanadoID, 
	       OrganizacionID,
		   TipoMovimientoID,
		   Fecha,
		   Folio,
		   VentaGanadoID,
		   Activo,
		   UsuarioCreacionID,
		   FechaCreacion
	FROM SalidaGanado 
	WHERE SalidaGanadoID = @IdentityID
	SET NOCOUNT OFF
  END

GO
