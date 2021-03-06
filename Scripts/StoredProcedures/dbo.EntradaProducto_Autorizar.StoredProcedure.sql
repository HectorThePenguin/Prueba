USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_Autorizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_Autorizar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_Autorizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Pedro Delgado
-- Create date: 23/05/2014
-- Description:	Actualiza un registro de entrada producto
/*EntradaProducto_Autorizar '
	<ROOT>
		<EntradaProducto>
			<Folio>1</Folio>
			<OrganizacionID>4</OrganizacionID>
			<Justificacion>ABC</Justificacion>
			<UsuarioModificacionID>5</UsuarioModificacionID>
			<OperadorIDAutoriza>1</OperadorIDAutoriza>
			<EstatusID>24</EstatusID>
		</EntradaProducto>
	</ROOT>
'*/
--======================================================
CREATE PROCEDURE [dbo].[EntradaProducto_Autorizar]
@XMLEntrada XML 
AS 
BEGIN
	DECLARE @TMP TABLE (Folio INT, OrganizacionID INT, Justificacion VARCHAR(255),OperadorIDAutoriza INT, UsuarioModificacionID INT, EstatusID INT)

	INSERT INTO @TMP
	(Folio , OrganizacionID , Justificacion , UsuarioModificacionID, OperadorIDAutoriza , EstatusID )
	SELECT 
			Folio = T.item.value('./Folio[1]', 'INT'),
			OrganizacionID    = T.item.value('./OrganizacionID[1]', 'INT'),
			Justificacion = T.item.value('./Justificacion[1]', 'VARCHAR(255)'),
			UsuarioModificacionID   = T.item.value('./UsuarioModificacionID[1]','INT'),
			OperadorIDAutoriza = T.item.value('./OperadorIDAutoriza[1]','INT'),
			EstatusID = T.item.value('./EstatusID[1]','INT')
	FROM  @XMLEntrada.nodes('ROOT/EntradaProducto') AS T(item)

	UPDATE EntradaProducto 
		SET Justificacion = TMP.Justificacion, 
			EstatusID = TMP.EstatusID,
			OperadorIDAutoriza = TMP.OperadorIDAutoriza,
			FechaModificacion = GETDATE(),
			UsuarioModificacionID = TMP.UsuarioModificacionID
	FROM EntradaProducto EP (NOLOCK)
	INNER JOIN @TMP TMP ON (TMP.Folio = EP.Folio AND TMP.OrganizacionID = EP.OrganizacionID)
END
GO
