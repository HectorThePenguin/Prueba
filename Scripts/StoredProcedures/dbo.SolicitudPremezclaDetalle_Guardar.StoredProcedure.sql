USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudPremezclaDetalle_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SolicitudPremezclaDetalle_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[SolicitudPremezclaDetalle_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 16/07/2014 12:00:00 a.m.
-- Description: 
/*SpName     : SolicitudPremezclaDetalle_Guardar '<ROOT>
		<PremezclaDetalle>
			<SolicitudPremezclaID>1</SolicitudPremezclaID>
			<FechaLlegada>2014-07-14 09:33:00</FechaLlegada>
			<PremezclaID>1</PremezclaID>
			<CantidadSolicitada>12</CantidadSolicitada>
			<UsuarioCreacionID>1</UsuarioCreacionID>
			<Activo>1</Activo>
		</PremezclaDetalle>
		<PremezclaDetalle>
			<SolicitudPremezclaID>1</SolicitudPremezclaID>
			<FechaLlegada>2014-07-14 09:33:00</FechaLlegada>
			<PremezclaID>2</PremezclaID>
			<CantidadSolicitada>12</CantidadSolicitada>
			<UsuarioCreacionID>1</UsuarioCreacionID>
			<Activo>1</Activo>
		</PremezclaDetalle>
	</ROOT>
'*/
--======================================================
CREATE PROCEDURE [dbo].[SolicitudPremezclaDetalle_Guardar]
@XMLPremezclaDetalle XML
AS 
BEGIN
	INSERT INTO SolicitudPremezclaDetalle
	(SolicitudPremezclaID,FechaLlegada,PremezclaID,CantidadSolicitada,Activo,FechaCreacion,UsuarioCreacionID)
	SELECT 
		SolicitudPremezclaID = T.item.value('./SolicitudPremezclaID[1]', 'INT'),
		FechaLlegada = T.item.value('./FechaLlegada[1]', 'SMALLDATETIME'),
		PremezclaID = T.item.value('./PremezclaID[1]', 'INT'),
		CantidadSolicitada = T.item.value('./CantidadSolicitada[1]', 'INT'),
		Activo = T.item.value('./Activo[1]', 'BIT'),
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @XMLPremezclaDetalle.nodes('ROOT/PremezclaDetalle') AS T(item)
END

GO
