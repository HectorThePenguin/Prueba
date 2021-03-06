USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProveedorChofer_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[ProveedorChofer_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Cesar Fernando Vega Vazquez
-- Create date: 18/10/2013
-- Description:	Guardar lista Chofer - Proveedor.
/*
ProveedorChofer_Guardar '<Root>
  <ProveedorChofer ProveedorId="4685" ChoferId="4" UsuarioId="6" />
  <ProveedorChofer ProveedorId="4685" ChoferId="34" UsuarioId="6" />
  <ProveedorChofer ProveedorId="4685" ChoferId="27" UsuarioId="6" />
  <ProveedorChofer ProveedorId="4685" ChoferId="14" UsuarioId="6" />
</Root>'ProveedorChofer where proveedo
*/
-- =============================================
CREATE PROCEDURE [dbo].[ProveedorChofer_Guardar]    
	@XML XML
AS  
BEGIN  
	SET NOCOUNT ON;  
	SELECT
		T.item.value('./@ProveedorId', 'INT') as ProveedorID
		, T.item.value('./@ChoferId', 'INT') as ChoferID
		, T.item.value('./@UsuarioId', 'INT') as UsuarioId
	INTO
		#ProveedorChofer_Guardar_Proveedor
	FROM
		@XML.nodes('Root/ProveedorChofer') as T(item)
	--Nuevos
	INSERT INTO
		ProveedorChofer (ProveedorID, ChoferID, FechaCreacion, UsuarioCreacionID, Activo)
	SELECT
		pgp.ProveedorID,  pgp.ChoferID, GETDATE(), UsuarioId, 1
	FROM
		#ProveedorChofer_Guardar_Proveedor pgp
		left join ProveedorChofer pc on pgp.ProveedorID = pc.ProveedorID and pgp.ChoferID = pc.ChoferID
	where
		pc.ProveedorID is null
	--Activar
	UPDATE
		ProveedorChofer
	SET
		Activo = 1
	FROM
		#ProveedorChofer_Guardar_Proveedor pgp
		inner join ProveedorChofer pc on pgp.ProveedorID = pc.ProveedorID and pgp.ChoferID = pc.ChoferID
	--Desactivar
	UPDATE
		pc
	SET
		Activo = 0
	FROM
		#ProveedorChofer_Guardar_Proveedor pgp
		right join ProveedorChofer pc on pgp.ProveedorID = pc.ProveedorID and pgp.ChoferID = pc.ChoferID
	where
		pgp.ProveedorID is null
		and pc.ProveedorID in (select t.u.value('./@ProveedorId', 'INT') from @XML.nodes('Root/ProveedorChofer') t(u))
	DROP TABLE #ProveedorChofer_Guardar_Proveedor
	SELECT * FROM ProveedorChofer WHERE ProveedorID in (select t.u.value('./@ProveedorId', 'INT') from @XML.nodes('Root/ProveedorChofer') t(u))
	SET NOCOUNT OFF;      
END  

GO
