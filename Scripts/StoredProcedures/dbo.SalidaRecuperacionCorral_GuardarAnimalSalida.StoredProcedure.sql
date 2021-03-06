USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacionCorral_GuardarAnimalSalida]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaRecuperacionCorral_GuardarAnimalSalida]
GO
/****** Object:  StoredProcedure [dbo].[SalidaRecuperacionCorral_GuardarAnimalSalida]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Edgar.Villarreal
-- Fecha: 2014-02-28
-- Descripci�n:	Guardar en AnimalSalida
-- EXEC SalidaRecuperacionCorral_GuardarAnimalSalida 29,1,3,9,1,5
-- EXEC SalidaRecuperacionCorral_GuardarAnimalSalida 30,1,3,9,1,5
-- =============================================
CREATE PROCEDURE [dbo].[SalidaRecuperacionCorral_GuardarAnimalSalida]
		@AnimalID INT,
		@LoteID INT,
		@Corraleta INT,
		@TipoMovimiento INT,
		@Activo INT,
		@UsuarioID INT
AS
BEGIN
	DECLARE @IdentityID BIGINT;
	SET @IdentityID = (SELECT TOP 1 COUNT(*)
					     FROM AnimalSalida
					    WHERE AnimalID = @AnimalID
							AND Activo = 1
					   )
	IF (  @IdentityID = 0 )
		BEGIN
			INSERT INTO AnimalSalida(
				AnimalID,
				LoteID,
				CorraletaID,
				TipoMovimientoID,
				FechaSalida,
				Activo,
				FechaCreacion,
				UsuarioCreacionID)
			VALUES(
				@AnimalID,
				@LoteID,
				@Corraleta,
				@TipoMovimiento,
				GETDATE(),
				@Activo,
				GETDATE(),
				@UsuarioID);
				SELECT AnimalID FROM AnimalSalida WHERE AnimalID = @AnimalID;
		END
		ELSE 
		BEGIN
			SELECT AnimalID FROM AnimalSalida WHERE AnimalID = @AnimalID;
		END
END

GO
