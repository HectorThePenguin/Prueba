USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoCosto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInternoCosto_Crear]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoCosto_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 21/07/2014
-- Description: Crea flete interno costo a partir de listado
-- FleteInternoCosto_Crear
--=============================================
CREATE PROCEDURE [dbo].[FleteInternoCosto_Crear]
@XmlFleteInternoCosto XML
AS
BEGIN
	DECLARE @tmpFleteInternoCosto AS TABLE
	(
	    FleteInternoDetalleID INT,
		CostoID INT,
		Tarifa DECIMAL(18,4),
		Activo INT,
		UsuarioCreacionID INT
	)
	INSERT @tmpFleteInternoCosto(
	    FleteInternoDetalleID,
		CostoID,
		Tarifa,
		Activo,
		UsuarioCreacionID
		)
	SELECT 
		FleteInternoDetalleID = T.item.value('./FleteInternoDetalleID[1]', 'INT'),
		CostoID = T.item.value('./CostoID[1]', 'INT'),
		Tarifa = T.item.value('./Tarifa[1]', 'DECIMAL(18,4)'),
		Activo = T.item.value('./Activo[1]', 'INT'),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @XmlFleteInternoCosto.nodes('ROOT/XmlFleteInternoCosto') AS T(item)
			/* Se crea registro en la tabla de Orden sacrificio*/
			INSERT INTO FleteInternoCosto(
				FleteInternoDetalleID,
				CostoID,
				Tarifa,
				Activo,
				FechaCreacion,
			    UsuarioCreacionID
				)
			SELECT FleteInternoDetalleID,
				   CostoID,
				   Tarifa,
				   Activo,
				   GETDATE(),
				   UsuarioCreacionID
			FROM @tmpFleteInternoCosto
END

GO
