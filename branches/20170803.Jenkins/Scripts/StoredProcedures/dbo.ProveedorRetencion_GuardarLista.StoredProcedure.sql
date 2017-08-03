USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorRetencion_GuardarLista]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorRetencion_GuardarLista]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorRetencion_GuardarLista]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProveedorRetencion_GuardarLista]
@XmlRetenciones XML
AS
BEGIN
	SELECT 
		ProveedorRetencionID = T.item.value('./ProveedorRetencionID[1]', 'INT'),
		ProveedorID = T.item.value('./ProveedorID[1]', 'INT'),
		RetencionID = T.item.value('./RetencionID[1]', 'INT'),
		IvaID = T.item.value('./IvaID[1]', 'INT'),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT'),
		UsuarioModificacionID = T.item.value('./UsuarioModificacionID[1]', 'INT'),
		Activo = T.item.value('./Activo[1]', 'BIT')
	INTO #RETENCIONES
	FROM @XmlRetenciones.nodes('ROOT/ProveedorRetencion') AS T(item)

	update #RETENCIONES set RetencionID = NULL
	where RetencionID = 0

	update #RETENCIONES set IvaID = NULL
	where IvaID = 0
	
	INSERT INTO ProveedorRetencion
	(
	ProveedorID
	,RetencionID
	,IvaID
	,Activo
	,FechaCreacion
	,UsuarioCreacionID
	)
	SELECT 
	ProveedorID
	,RetencionID
	,IvaID
	,Activo
	,GETDATE()
	,UsuarioCreacionID
	FROM #RETENCIONES
	WHERE ProveedorRetencionID = 0

	UPDATE pr
	set 
	pr.RetencionID = r.RetencionID
	,pr.IvaID = r.IvaID
	,pr.Activo = r.Activo
	,pr.FechaModificacion = GETDATE()
	,pr.UsuarioModificacionID = r.UsuarioModificacionID
	FROM ProveedorRetencion pr
	Inner join #RETENCIONES r on pr.ProveedorRetencionID = r.ProveedorRetencionID
	where r.ProveedorRetencionID > 0

END

GO
