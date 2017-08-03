IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE [object_id] = Object_id(N'[dbo].[ObtenerOrganizacionOrigenCorralLote]')
		)
	DROP FUNCTION [dbo].[ObtenerOrganizacionOrigenCorralLote]
GO
-- =============================================
-- Author:    César Valdez
-- Create date: 20/05/2014
-- Description:  Obtener listado de organizaciones origen separadas por comas
-- select dbo.ObtenerOrganizacionOrigenCorralLote(3078,124) 
-- =============================================
CREATE FUNCTION [dbo].[ObtenerOrganizacionOrigenCorralLote] (
	@CorralID INT, @LoteID INT)
RETURNS VARCHAR(255)
AS
BEGIN
	DECLARE @resultado VARCHAR(255);
	DECLARE @OrganizacionOrigenActual VARCHAR(50);
	DECLARE @Coma CHAR(1);
		
	-- Declaración del cursor
	DECLARE curOrganizacionOrigen CURSOR FOR 
		SELECT O.Descripcion
		  FROM EntradaGanado EG
		 INNER JOIN Organizacion O ON (O.OrganizacionID = EG.OrganizacionOrigenID)
		 WHERE EG.Activo = 1
		   AND EG.CorralID = @CorralID
		   AND EG.LoteID = @LoteID;
	
	SET @Coma = ',';
	
	-- Apertura del cursor
	OPEN curOrganizacionOrigen
	-- Lectura de la primera fila del cursor
		FETCH curOrganizacionOrigen INTO @OrganizacionOrigenActual
			WHILE (@@FETCH_STATUS = 0 )
			BEGIN
			
				SET @resultado = CONCAT ( @resultado , RTRIM(LTRIM(@OrganizacionOrigenActual)) , @Coma);
			
				FETCH curOrganizacionOrigen INTO @OrganizacionOrigenActual
			END
		-- Cierre del cursor
		CLOSE curOrganizacionOrigen
	-- Liberar los recursos
	DEALLOCATE curOrganizacionOrigen

	SET @resultado = SUBSTRING ( @resultado ,0 , LEN (@resultado )  )
		
	RETURN isnull(@resultado, '0')
END
