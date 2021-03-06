USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Jaula_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Modified: Luis Alfonso Sandoval Huerta
-- Modification date: 19/05/2017
-- Description:  Actualizar una Jaula.
-- Se modifica para agregar los campos NumEconomico,
-- MarcaID, Modelo, Boletinado y Observaciones
-- [Jaula_Actualizar] 2368, 4805, 'Jaula001', 1, 1, 123, 1, 2003, 0, '', 1, 5
-- =============================================
CREATE PROCEDURE [dbo].[Jaula_Actualizar]		
 @JaulaID				  INT,
 @ProveedorID             INT,
 @PlacaJaula              VARCHAR(10),
 @Capacidad             INT,
 @Secciones               INT,
 @NumEconomico			  VARCHAR(10),
 @MarcaID				  INT,
 @Modelo				  INT,
 @Boletinado			  BIT,
 @Observaciones			  VARCHAR(250),	
 @Activo                  BIT,
 @UsuarioModificacionID   INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Jaula 
		SET ProveedorID = @ProveedorID,
			PlacaJaula = @PlacaJaula, 
			Capacidad = @Capacidad,
			Secciones = @Secciones,
			Activo = @Activo, 
			NumEconomico = @NumEconomico,
			MarcaID = @MarcaID,
			Modelo = @Modelo,
			Boletinado = @Boletinado,
			Observaciones = @Observaciones,
			Fechamodificacion = GETDATE(),
			UsuariomodificacionID = @UsuariomodificacionID
	WHERE JaulaID = @JaulaID
	SET NOCOUNT ON;
END

GO
