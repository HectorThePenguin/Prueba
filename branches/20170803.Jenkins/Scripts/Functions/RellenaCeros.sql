IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[RellenaCeros]'))
		DROP FUNCTION [dbo].[RellenaCeros]
GO
--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2013/12/21
-- Description: Función para rellenar de ceros a la izquierda
-- Modificacion: Se modifica para contemplar los 10 digitos del codigo sap de los proveedores
--				Pedro Delgado				
-- select dbo.RellenaCeros(4,3)
--=============================================
CREATE FUNCTION RellenaCeros(@valor BIGINT, @caracteres int)
RETURNS VARCHAR(20)
BEGIN
	RETURN  right(replicate('0',@caracteres) + rtrim(@valor), @caracteres)
END
