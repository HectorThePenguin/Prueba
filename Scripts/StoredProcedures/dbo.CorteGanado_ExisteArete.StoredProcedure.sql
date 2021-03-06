USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ExisteArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ExisteArete]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ExisteArete]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Daniel.Benitez
-- Create date: 2015/07/23
-- Description: Consulta la tabla de animal para verificar si el arete existe
-- Origen     : APInterfaces
-- select dbo.CorteGanado_ExisteArete ('484090100112980', 'false')
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ExisteArete]
	@Arete VARCHAR(15),
	@AreteMetalico VARCHAR(15),
	@Organizacion INT
AS
BEGIN
	DECLARE @Encontrado INT
	SET @Encontrado = 0
	IF ( LEN(@Arete) > 0 AND LEN(@AreteMetalico) > 0)
		BEGIN
			IF EXISTS (SELECT TOP 1 AnimalID FROM Animal WHERE Arete = @Arete AND AreteMetalico = @AreteMetalico AND OrganizacionIDEntrada = @Organizacion AND Activo = 1 )
			BEGIN 
				SET @Encontrado = 1
			END
		END
	ELSE
		BEGIN
			IF ( LEN(@Arete) > 0 AND LEN(@AreteMetalico) = 0)
				BEGIN
					IF EXISTS (SELECT TOP 1 AnimalID FROM Animal WHERE Arete = @Arete AND OrganizacionIDEntrada = @Organizacion AND Activo = 1 )
						BEGIN 
							SET @Encontrado = 1
						END
				END
			ELSE
				BEGIN
					IF ( LEN(@Arete) = 0 AND LEN(@AreteMetalico) > 0)
						BEGIN
							IF EXISTS (SELECT TOP 1 AnimalID FROM Animal WHERE AreteMetalico = @AreteMetalico AND OrganizacionIDEntrada = @Organizacion AND Activo = 1 )
								BEGIN 
									SET @Encontrado = 1
								END
						END
				END
		END
	SELECT ISNULL (@Encontrado,0)
END

GO
