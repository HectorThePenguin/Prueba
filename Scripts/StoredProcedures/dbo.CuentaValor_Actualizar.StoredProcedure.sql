USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaValor_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CuentaValor_Actualizar
--======================================================
CREATE PROCEDURE [dbo].[CuentaValor_Actualizar]
@CuentaValorID int,
@CuentaID int,
@OrganizacionID int,
@Valor varchar(20),
@Activo int,
@UsuarioModificacionID int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CuentaValor SET
		CuentaID = @CuentaID,
		OrganizacionID = @OrganizacionID,
		Valor = @Valor,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CuentaValorID = @CuentaValorID
	SET NOCOUNT OFF;
END

GO
