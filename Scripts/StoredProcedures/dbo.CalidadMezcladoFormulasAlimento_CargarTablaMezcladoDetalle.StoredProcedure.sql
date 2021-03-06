USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_CargarTablaMezcladoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_CargarTablaMezcladoDetalle]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_CargarTablaMezcladoDetalle]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 14/Noviembre/2014
-- Description:  Consulta de 
-- CalidadMezcladoFormulasAlimento_CargarTablaMezcladoDetalle 1, 1, 1 
-- =============================================
CREATE PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_CargarTablaMezcladoDetalle]
@OrganizacionID INT,
@FormulaID INT,
@TipoTecnicaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TM.Descripcion as TipoAnalisis, CMD.NumeroMuestra, CMD.Peso, CMD.Particulas
	FROM
		CalidadMezcladoDetalle CMD inner join TipoMuestra TM on CMD.TipoMuestraID=TM.TipoMuestraID
    Where
		CMD.CalidadMezcladoID = 
		(
		select
			CalidadMezcladoID
		from
			CalidadMezclado
		where
			(OrganizacionID = @OrganizacionID) and (FormulaID=@FormulaID) and (TipoTecnicaID=@TipoTecnicaID) and (cast(Fecha as date) = cast(GETDATE() as date))
		)
	SET NOCOUNT OFF;
END

GO
