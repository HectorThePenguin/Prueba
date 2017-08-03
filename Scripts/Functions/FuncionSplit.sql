IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[FuncionSplit]'))
		DROP FUNCTION [dbo].[FuncionSplit]
GO
CREATE FUNCTION [dbo].[FuncionSplit] 
( 
    @Cadena NVARCHAR(MAX), 
    @Delimitador CHAR(1) 
) 
RETURNS @output TABLE(Registros NVARCHAR(MAX) 
) 
BEGIN 
    DECLARE @Inicio INT, @Fin INT 
    SELECT @Inicio = 1, @Fin = CHARINDEX(@Delimitador, @Cadena) 
    WHILE @Inicio < LEN(@Cadena) + 1 BEGIN 
        IF @Fin = 0  
            SET @Fin = LEN(@Cadena) + 1
       
        INSERT INTO @output (Registros)  
        VALUES(SUBSTRING(@Cadena, @Inicio, @Fin - @Inicio)) 
        SET @Inicio = @Fin + 1 
        SET @Fin = CHARINDEX(@Delimitador, @Cadena, @Inicio)
        
    END 
    RETURN
END