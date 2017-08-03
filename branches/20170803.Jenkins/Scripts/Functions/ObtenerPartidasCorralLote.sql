IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerPartidasCorralLote]')
		)
	DROP FUNCTION [dbo].[ObtenerPartidasCorralLote]
GO
-- =============================================
-- Author:    César Valdez
-- Create date: 20/05/2014
-- Description:  Obtener listado de partidas separadas por comas
-- select dbo.ObtenerPartidasCorralLote(3078,124,',') 
-- =============================================
CREATE FUNCTION [dbo].[ObtenerPartidasCorralLote] (
	@CorralID INT, @LoteID INT, @Separador CHAR(1))
RETURNS VARCHAR(255)
AS
BEGIN
	DECLARE @resultado VARCHAR(255);
	DECLARE @FolioEntradaActual AS INT;
		
	-- Declaración del cursor
	DECLARE curFolioEntrada CURSOR FOR 
			SELECT EG.FolioEntrada 
			  FROM EntradaGanado EG
			 WHERE EG.Activo = 1
			   AND EG.CorralID = @CorralID
		   AND EG.LoteID = @LoteID;
	
	-- Apertura del cursor
	OPEN curFolioEntrada
	-- Lectura de la primera fila del cursor
		FETCH curFolioEntrada INTO @FolioEntradaActual
			WHILE (@@FETCH_STATUS = 0 )
			BEGIN
			
				SET @resultado = CONCAT ( @resultado , @FolioEntradaActual , @Separador);
			
				FETCH curFolioEntrada INTO @FolioEntradaActual
			END
		-- Cierre del cursor
		CLOSE curFolioEntrada
	-- Liberar los recursos
	DEALLOCATE curFolioEntrada

	SET @resultado = SUBSTRING ( @resultado ,0 , LEN (@resultado ) )
		
	RETURN isnull(@resultado, '0')
END
