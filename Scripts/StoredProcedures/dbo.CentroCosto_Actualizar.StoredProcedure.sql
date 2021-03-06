USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CentroCosto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CentroCosto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 28/08/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CentroCosto_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CentroCosto_Actualizar]
@CentroCostoID int,
@CentroCostoSAP char(6),
@Descripcion varchar(100),
@AreaDepartamento char(6),
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CentroCosto SET
		CentroCostoSAP = @CentroCostoSAP,
		Descripcion = @Descripcion,
		AreaDepartamento = @AreaDepartamento,
		Activo = @Activo,
		UsuarioCreacionID = @UsuarioCreacionID,
		FechaModificacion = GETDATE()
	WHERE CentroCostoID = @CentroCostoID
	SET NOCOUNT OFF;
END

GO
