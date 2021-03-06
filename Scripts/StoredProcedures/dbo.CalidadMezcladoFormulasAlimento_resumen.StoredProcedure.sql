USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_resumen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_resumen]
GO
/****** Object:  StoredProcedure [dbo].[CalidadMezcladoFormulasAlimento_resumen]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Eduardo Cota
-- Create date: 10/Noviembre/2014
-- Description:  Consulta de 
-- CalidadMezcladoFormulasAlimento_resumen 1, 10
-- =============================================
CREATE PROCEDURE [dbo].[CalidadMezcladoFormulasAlimento_resumen]
	@organizacionID INT,
	@formulaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		TipoMuestraID, NumeroMuestra, Peso, Particulas
	INTO
		#CalidadMezcladoDetalle
	FROM
		CalidadMezcladoDetalle (NOLOCK) 
	WHERE
		CalidadMezcladoID IN 
							(
							SELECT	
								CalidadMezcladoID
							FROM
								CalidadMezclado
							WHERE
								--(OrganizacionID = @organizacionID) AND (TipoTecnicaID = @formulaID) 
								(OrganizacionID = @organizacionID) AND (FormulaID = @formulaID) AND (CAST(GETDATE() as date)= CAST(Fecha as date))
							)
	SELECT  
		TM.Descripcion as TipoAnalisis,
		CMD.NumeroMuestra,
		CMD.TipoMuestraID,
		CMD.Peso,
		CMD.Particulas,
		CMF.PesoBaseHumeda,
		CMF.PesoBaseSeca,
		CMF.Factor
	INTO
		#Final
	FROM 
		#CalidadMezcladoDetalle CMD 
		inner join TipoMuestra TM on CMD.TipoMuestraID=TM.TipoMuestraID 
		inner join CalidadMezcladoFactor CMF on CMD.TipoMuestraID = CMF.TipoMuestraID
	SELECT
		TipoAnalisis,
		avg (Peso) as Peso,
		PesoBaseHumeda as PesoBH,
		PesoBaseSeca as PesoBS,
		Factor,
		avg (Particulas) as PromParticulasEsperadas,
		TipoMuestraID
	FROM
		#Final
	group by
		TipoAnalisis,
		TipoMuestraID,
		PesoBaseHumeda,
		PesoBaseSeca,
		Factor
	order by
		TipoMuestraID
	DROP TABLE #CalidadMezcladoDetalle
	DROP TABLE #Final
	SET NOCOUNT OFF;
END

GO
