USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProduccionFormula_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ProduccionFormula_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Antonio Delgado Viedas
-- Create date: 13/06/2014 12:00:00 a.m.
-- Description: Crea un registro en ProduccionFormula
-- SpName     : ProduccionFormula_Crear
--======================================================
CREATE PROCEDURE [dbo].[ProduccionFormula_Crear] @OrganizacionID INT
	,@FormulaID INT
	,@CantidadProducida DECIMAL(14, 2)
	,@TipoFolio INT
	,@AlmacenMovimientoEntradaID bigint
	,@AlmacenMovimientoSalidaID bigint
	,@UsuarioCreacionID INT
	,@FechaProduccion DATETIME
AS
BEGIN
	DECLARE @ProduccionFormulaID INT
	DECLARE @FolioFormula INT
	EXEC Folio_Obtener @OrganizacionID
		,@TipoFolio
		,@Folio = @FolioFormula OUTPUT
	INSERT INTO ProduccionFormula (
		OrganizacionID
		,FolioFormula
		,FormulaID
		,CantidadProducida
		,FechaProduccion
		,Activo
		,AlmacenMovimientoEntradaID
		,AlmacenMovimientoSalidaID
		,FechaCreacion
		,UsuarioCreacionID
		)
	VALUES (
		@OrganizacionID
		,@FolioFormula
		,@FormulaID
		,@CantidadProducida
		,@FechaProduccion
		,1
		,@AlmacenMovimientoEntradaID
		,@AlmacenMovimientoSalidaID
		,GETDATE()
		,@UsuarioCreacionID
		)
	SELECT @ProduccionFormulaID = @@IDENTITY
	SELECT ProduccionFormulaID
		,FolioFormula
		,OrganizacionID
		,FormulaID
		,CantidadProducida
		,FechaProduccion
		,Activo
		,AlmacenMovimientoEntradaID
		,AlmacenMovimientoSalidaID
		,FechaCreacion
		,UsuarioCreacionID
	FROM ProduccionFormula(NOLOCK) PF
	WHERE ProduccionFormulaID = @ProduccionFormulaID
END

GO
