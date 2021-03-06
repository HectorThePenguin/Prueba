USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralDetector_Crear]
GO
/****** Object:  StoredProcedure [dbo].[CorralDetector_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 04/03/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CorralDetector_Crear
--======================================================
CREATE PROCEDURE [dbo].[CorralDetector_Crear]
	@OperadorID INT,
	@UsuarioCreacionID INT,
	@CorralesXML XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Corrales AS TABLE ([CorralDetectorID] INT, [CorralID] INT, [Activo] BIT)
	INSERT @Corrales ([CorralDetectorID], [CorralID], [Activo])
	SELECT [CorralDetectorID] = t.item.value('./CorralDetectorID[1]', 'INT'),
		   [CorralID] = t.item.value('./CorralID[1]', 'INT'),
		   [Activo] = t.item.value('./Activo[1]', 'BIT')
	FROM @CorralesXML.nodes('ROOT/Corrales') AS t(item)
	UPDATE cd
	SET OperadorID = @OperadorID
		,CorralID = cs.CorralID
		,Activo = cs.Activo
		,[FechaModificacion] = GETDATE()
		,UsuarioModificacionID = @UsuarioCreacionID
	FROM CorralDetector cd
	INNER JOIN @Corrales cs ON cs.CorralDetectorID = cd.CorralDetectorID
	INSERT CorralDetector (
		OperadorID,
		CorralID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	SELECT
		@OperadorID,
		C.CorralID,
		C.Activo,
		@UsuarioCreacionID,
		GETDATE()
	FROM @Corrales C
	WHERE C.CorralDetectorID = 0
	/*AND NOT EXISTS (
		SELECT CorralID
		  FROM CorralDetector CD
		 WHERE CD.Activo = 1 
		   AND CD.CorralID = C.CorralID
		   -- AND CD.OperadorID = @OperadorID
		)*/
	SET NOCOUNT OFF;
END

GO
