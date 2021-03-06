USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_GuardarOrden]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[OrdenSacrificio_GuardarOrden]
GO
/****** Object:  StoredProcedure [dbo].[OrdenSacrificio_GuardarOrden]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/02/27
-- Description: SP para Crear un registro en la tabla de OrdenSacrificio
-- Origen     : APInterfaces
-- EXEC OrdenSacrificio_GuardarOrden 5,4,'Observaciones',1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[OrdenSacrificio_GuardarOrden]
    @OrdenSacrificioID INT,
	@TipoFolioID INT,
	@OrganizacionID INT,
	@Observaciones VARCHAR(300),
	@EstatusID INT,
	@Activo INT,
	@FechaOrden DATETIME,
	@UsuarioCreacionID INT
AS
BEGIN
	DECLARE @IdentityID BIGINT;
	DECLARE @Lote INT 
	DECLARE @FolioOrdenSacrificio INT
	IF @OrdenSacrificioID= 0 
	BEGIN
		EXEC Folio_Obtener @OrganizacionID, @TipoFolioID, @Folio = @FolioOrdenSacrificio output
		/* Se crea registro en la tabla de Orden sacrificio*/
		INSERT INTO OrdenSacrificio(
			FolioOrdenSacrificio,
			OrganizacionID,
			Observaciones,
			EstatusID,
			Activo,
			FechaOrden,
			FechaCreacion,
			UsuarioCreacionID
			)
		VALUES(
			@FolioOrdenSacrificio,
			@OrganizacionID,
			@Observaciones,
			@EstatusID,
			@Activo,
			@FechaOrden,
			GETDATE(),
			@UsuarioCreacionID
			);
			/* Se obtiene el id Insertado */
			SET @IdentityID = (SELECT @@IDENTITY)
	END
	ELSE
		BEGIN
			SET @IdentityID = @OrdenSacrificioID
			UPDATE OrdenSacrificio
			   SET Observaciones = @Observaciones,
			       FechaModificacion = GETDATE(),
				   UsuarioModificacionID = @UsuarioCreacionID
		     WHERE OrdenSacrificioID = @IdentityID
		END
		SELECT 
				OrdenSacrificioID,
				FolioOrdenSacrificio,
                OrganizacionID,
                Observaciones,
                EstatusID,
                Activo
			FROM OrdenSacrificio 
			WHERE OrdenSacrificioID = @IdentityID
			 AND EstatusID = @EstatusID
			 AND Activo = 1
END

GO
