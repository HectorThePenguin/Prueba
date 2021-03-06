USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoCosto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[FleteInternoCosto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[FleteInternoCosto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 21/07/2014
-- Description: Crea flete interno costo a partir de una lista
-- FleteInternoCosto_Actualizar
--=============================================
CREATE PROCEDURE [dbo].[FleteInternoCosto_Actualizar]
@XmlFleteInternoCosto XML
AS
BEGIN
	DECLARE @tmpFleteInternoCosto AS TABLE
	(
	    FleteInternoCostoID INT,
		Tarifa DECIMAL(18,4),
		Activo INT,
		UsuarioModificacionID INT
	)
	INSERT @tmpFleteInternoCosto(
	    FleteInternoCostoID,
		Tarifa,
		Activo,
		UsuarioModificacionID
		)
	SELECT FleteInternoCostoID = T.item.value('./FleteInternoCostoID[1]', 'INT'),
		Tarifa = T.item.value('./Tarifa[1]', 'DECIMAL(18,4)'),
		Activo = T.item.value('./Activo[1]', 'INT'),
		UsuarioModificacionID = T.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM  @XmlFleteInternoCosto.nodes('ROOT/XmlFleteInternoCosto') AS T(item)
	UPDATE FleteInternoCosto SET
		Tarifa = Tmp.Tarifa,
		Activo = Tmp.Activo,
		FechaModificacion = GETDATE(),
		UsuarioModificacionID = Tmp.UsuarioModificacionID
	FROM FleteInternoCosto FL
	INNER JOIN @tmpFleteInternoCosto Tmp ON Tmp.FleteInternoCostoID = FL.FleteInternoCostoID
	WHERE FL.FleteInternoCostoID = Tmp.FleteInternoCostoID
END

GO
