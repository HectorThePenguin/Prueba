USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Jaula_crear]
GO
/****** Object:  StoredProcedure [dbo].[Jaula_crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Modified: Luis Alfonso Sandoval Huerta
-- Modification date: 19/05/2017
-- Description:  Crear una Jaula.
-- Se modifica para agregar los campos NumEconomico,
-- MarcaID, Modelo, Boletinado y Observaciones
-- [Jaula_Crear] 4805, 'Jaula001', 1, 1, 1, 123, 1, 2003, '', 1, 5
-- =============================================
CREATE PROCEDURE [dbo].[Jaula_crear]
 @ProveedorID        INT,
 @PlacaJaula         VARCHAR(10),
 @Capacidad          INT,
 @Secciones          INT,
 @NumEconomico		 VARCHAR(10),
 @MarcaID			 INT,
 @Modelo		     INT,
 @Boletinado	     BIT,
 @Observaciones	     VARCHAR(250),
 @Activo             BIT,
 @UsuarioCreacionID  INT
AS
  BEGIN
      SET NOCOUNT ON;
      INSERT INTO Jaula(
         ProveedorID,
		 PlacaJaula,
		 Capacidad,
		 Secciones,
		 NumEconomico,
		 MarcaID,
		 Modelo,
		 Boletinado,
		 Observaciones,
		 Activo,
		 FechaCreacion,
		 UsuarioCreacionID    
		)
		VALUES(
		 @ProveedorID,
		 @PlacaJaula,
		 @Capacidad,
		 @Secciones,
		 @NumEconomico,
		 @MarcaID,
		 @Modelo,
		 @Boletinado,
		 @Observaciones,
		 @Activo,
		 GETDATE(),        
		 @UsuarioCreacionID    
		)
		SELECT SCOPE_IDENTITY()
		SET NOCOUNT OFF;
END  

GO
