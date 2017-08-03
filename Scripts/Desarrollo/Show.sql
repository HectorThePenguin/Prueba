IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[Show]'))
  DROP PROCEDURE [dbo].[Show]

go

-- =============================================
-- Author     : José Gilberto Quintero López
-- Create date: 31/10/2013
-- Description:  Ayuda para buscar tablas
-- Show '', 'U'
-- =============================================
CREATE PROCEDURE [dbo].[Show] @name VARCHAR(50) = '',
                              @type VARCHAR(3) = 'U'
AS
  BEGIN
      DECLARE @Select VARCHAR(500)

      SET  @name = '%' + @name + '%'
      SET @Select = CASE
                      WHEN @type = 'U' THEN 'SELECT * FROM [dbo].'
                      ELSE 'SP_HELPTEXT '
                    END

      SELECT [SqlText] = @Select + '[' + Rtrim(name) + ']'
      FROM   sysobjects
      WHERE  name LIKE @name
             AND @type IN ( type )
  END
go