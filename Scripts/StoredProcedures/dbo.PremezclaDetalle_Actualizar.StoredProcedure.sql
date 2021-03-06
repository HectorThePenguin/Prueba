USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDetalle_Actualizar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[PremezclaDetalle_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[PremezclaDetalle_Actualizar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jesus Alvarez
-- Create date: 15/07/2014
-- Description: Crea premezcla detalle a partir de una lista
-- PremezclaDetalle_Actualizar
--=============================================
CREATE PROCEDURE [dbo].[PremezclaDetalle_Actualizar]
@XmlPremezclaDetalle XML
AS
BEGIN
	DECLARE @tmpPremezclaDetalle AS TABLE
	(
	    PremezclaDetalleID INT,
		Porcentaje DECIMAL(10,3),
		Activo INT,
		UsuarioModificacionID INT
	)
	INSERT @tmpPremezclaDetalle(
	    PremezclaDetalleID,
		Porcentaje,
		Activo,
		UsuarioModificacionID
		)
	SELECT PremezclaDetalleID = T.item.value('./PremezclaDetalleID[1]', 'INT'),
		Porcentaje = T.item.value('./Porcentaje[1]', 'DECIMAL(10,3)'),
		Activo = T.item.value('./Activo[1]', 'INT'),
		UsuarioModificacionID = T.item.value('./UsuarioModificacionID[1]', 'INT')
	FROM  @XmlPremezclaDetalle.nodes('ROOT/XmlPremezclaDetalle') AS T(item)
	UPDATE PremezclaDetalle SET
		Porcentaje = Tmp.Porcentaje,
		Activo = Tmp.Activo,
		UsuarioModificacionID = Tmp.UsuarioModificacionID,
		FechaModificacion = GETDATE()
	FROM PremezclaDetalle PD
	INNER JOIN @tmpPremezclaDetalle Tmp ON Tmp.PremezclaDetalleID = PD.PremezclaDetalleID
	WHERE PD.PremezclaDetalleID = Tmp.PremezclaDetalleID
END

GO
