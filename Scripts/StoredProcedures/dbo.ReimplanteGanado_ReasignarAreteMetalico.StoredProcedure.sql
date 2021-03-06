USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ReasignarAreteMetalico]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ReasignarAreteMetalico]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ReasignarAreteMetalico]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2013/12/26
-- Description: SP para Reasignar Arete o Metalico
-- Origen     : APInterfaces
-- EXEC ReimplanteGanado_ReasignarAreteMetalico '123456','123456',0,1,1
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ReasignarAreteMetalico]
	@Arete VARCHAR(15),
	@AreteMetalico VARCHAR(15),
	@Guardar INT,
	@OrganizacionIDEntrada INT,
	@UsuarioModificacionID INT
AS
BEGIN
	DECLARE @IdentityID BIGINT;

	SET @IdentityID = (SELECT TOP 1 AnimalID
					     FROM Animal(NOLOCK)
					    WHERE Arete = @Arete
						  AND OrganizacionIDEntrada = @OrganizacionIDEntrada
						  AND Activo = 1
					   )
					
	IF (  @IdentityID > 0 )
		BEGIN
			IF ( @Guardar = 1 )
			BEGIN
				UPDATE Animal
					SET AreteMetalico = @AreteMetalico,
					FechaModificacion = GETDATE(),
					UsuarioModificacionID = @UsuarioModificacionID
				FROM Animal (NOLOCK)
				WHERE AnimalID = @IdentityID
			END
		END
	
	ELSE
		BEGIN		
			/* Se crea registro en la tabla de Animal*/
			SET @IdentityID = (SELECT TOP 1 AnimalID
					     FROM Animal(NOLOCK)
					    WHERE AreteMetalico = @AreteMetalico
						  AND OrganizacionIDEntrada = @OrganizacionIDEntrada
						  AND Activo = 1
					   )
					   
					   
			IF (  @IdentityID > 0 )
			BEGIN
				IF ( @Guardar = 1 )
				BEGIN
					UPDATE Animal
					SET Arete = @Arete,
					FechaModificacion = GETDATE(),
					UsuarioModificacionID = @UsuarioModificacionID
					FROM Animal (NOLOCK)
					WHERE AnimalID = @IdentityID
				END
			END
		END
		
	SELECT 
		anm.AnimalID,
		anm.Arete,
		anm.AreteMetalico,
		anm.FechaCompra,
		anm.TipoGanadoID,
		anm.CalidadGanadoID,
		anm.ClasificacionGanadoID,
		anm.PesoCompra,
		anm.OrganizacionIDEntrada,
		anm.FolioEntrada,
		anm.PesoLlegada,
		anm.Paletas,
		anm.CausaRechadoID,
		anm.Venta,
		anm.Cronico,
		anm.Activo,
		anm.FechaCreacion,
		anm.UsuarioCreacionID,
		anm.FechaModificacion,
		anm.UsuarioModificacionID,
		anmov.Peso,
		crl.Codigo [CodigoCorral]
	FROM Animal anm(NOLOCK)
	INNER JOIN AnimalMovimiento anmov (NOLOCK) ON anm.AnimalID = anmov.AnimalID
	INNER JOIN Corral crl (NOLOCK) ON crl.CorralID = anmov.CorralID
	WHERE anm.AnimalID = @IdentityID
	AND anmov.Activo = 1 
END
GO
