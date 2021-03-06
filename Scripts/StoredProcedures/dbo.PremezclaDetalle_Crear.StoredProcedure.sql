USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDetalle_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PremezclaDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDetalle_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 15/07/2014
-- Description: Crea premezcla detalle a partir de una lista
-- PremezclaDetalle_Crear
--=============================================
CREATE PROCEDURE [dbo].[PremezclaDetalle_Crear]
@XmlPremezclaDetalle XML
AS
BEGIN
	DECLARE @tmpPremezclaDetalle AS TABLE
	(
	    PremezclaID INT,
		ProductoID INT,
		Porcentaje DECIMAL(10,3),
		Activo INT,
		UsuarioCreacionID INT
	)
	INSERT @tmpPremezclaDetalle(
	    PremezclaID,
		ProductoID,
		Porcentaje,
		Activo,
		UsuarioCreacionID
		)
	SELECT 
		PremezclaID = T.item.value('./PremezclaID[1]', 'INT'),
		ProductoID = T.item.value('./ProductoID[1]', 'INT'),
		Porcentaje = T.item.value('./Porcentaje[1]', 'DECIMAL(10,3)'),
		Activo = T.item.value('./Activo[1]', 'INT'),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM  @XmlPremezclaDetalle.nodes('ROOT/XmlPremezclaDetalle') AS T(item)
			/* Se crea registro en la tabla de Orden sacrificio*/
			INSERT INTO PremezclaDetalle(
				PremezclaID,
				ProductoID,
				Porcentaje,
				Activo,
				FechaCreacion,
			    UsuarioCreacionID
				)
			SELECT PremezclaID,
				   ProductoID,
				   Porcentaje,
				   Activo,
				   GETDATE(),
				   UsuarioCreacionID
			FROM @tmpPremezclaDetalle
END

GO
