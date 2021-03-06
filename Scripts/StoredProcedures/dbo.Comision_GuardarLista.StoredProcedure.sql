USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Comision_GuardarLista]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Comision_GuardarLista]
GO
/****** Object:  StoredProcedure [dbo].[Comision_GuardarLista]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Comision_GuardarLista]
@XmlComisiones XML
AS
BEGIN
	SELECT 
		ProveedorComisionID = T.item.value('./ProveedorComisionID[1]', 'INT'),
		ProveedorID = T.item.value('./ProveedorID[1]', 'INT'),
		TipoComisionID = T.item.value('./TipoComisionID[1]', 'INT'),
		Tarifa = T.item.value('./Tarifa[1]', 'DECIMAL(12,2)'),
		UsuarioID = T.item.value('./UsuarioID[1]', 'INT'),
		Accion = T.item.value('./Accion[1]', 'INT')
	INTO #Comisiones
	FROM @XmlComisiones.nodes('ROOT/ProveedorComisiones/ComisionesObjeto') AS T(item)
	UPDATE pcom 
	SET pcom.Tarifa = com.Tarifa, pcom.UsuarioModificacionID = com.UsuarioID, pcom.FechaModificacion = GETDATE()
	FROM ProveedorComision pcom 
	INNER JOIN #Comisiones com ON (pcom.ProveedorComisionID = com.ProveedorComisionID)
	WHERE com.Accion = 2
	INSERT INTO ProveedorComision (ProveedorID, TipoComisionID, Tarifa, UsuarioCreacionID, Activo )
	SELECT ProveedorID, TipoComisionID, Tarifa, UsuarioID, 1 
	FROM #Comisiones WHERE Accion = 1
	UPDATE pcom 
	SET pcom.Activo = 0, pcom.UsuarioModificacionID = com.UsuarioID, pcom.FechaModificacion = GETDATE()
	FROM ProveedorComision pcom 
	INNER JOIN #Comisiones com ON (pcom.ProveedorComisionID = com.ProveedorComisionID)
	WHERE com.Accion = 3
	DROP TABLE #Comisiones
END

GO
