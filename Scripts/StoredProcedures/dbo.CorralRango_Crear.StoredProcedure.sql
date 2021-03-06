USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralRango_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CorralRango_Crear]
	@OrganizacionID INT,
	@CorralID INT,
	@Sexo CHAR(1),
	@RangoInicial INT,
	@RangoFinal INT,
	@Activo INT,
	/*@FechaCreacion DATETIME,*/
	@UsuarioCreacionID INT
AS
BEGIN	
SET NOCOUNT ON;	
DECLARE @EXISTE INT;
	SET @EXISTE = (SELECT 1 FROM CorralRango WHERE CorralID = @CorralID AND OrganizacionID = @OrganizacionID AND Activo = 1 )
	IF (@EXISTE = 1) 
	BEGIN
		UPDATE CorralRango 
		 SET CorralID = @CorralID, 
			 Sexo = @Sexo, 
			 RangoInicial = @RangoInicial, 
			 RangoFinal = @RangoFinal, 
			 FechaModificacion = GETDATE(), 
			 UsuarioModificacionID = @UsuarioCreacionID
		WHERE CorralID = @CorralID
		AND OrganizacionID = @OrganizacionID
	END
	ELSE
		BEGIN
			INSERT INTO CorralRango (OrganizacionID, 
									 CorralID, 
									 Sexo, 
									 RangoInicial, 
									 RangoFinal, 
									 Activo, 
									 FechaCreacion, 
									 UsuarioCreacionID) 
			VALUES (@OrganizacionID, @CorralID, @Sexo, 
					@RangoInicial, @RangoFinal, @Activo, 
					GETDATE(), @UsuarioCreacionID)
		END
SET NOCOUNT OFF;
END

GO
