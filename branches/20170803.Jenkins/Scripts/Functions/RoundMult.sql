IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[RoundMult]'))
		DROP FUNCTION [dbo].[RoundMult]
GO
-- =============================================
-- Author:    Pedro Delgado
-- Create date: 10/05/2014
-- Description:  Genera el multipo mas cercano
-- Origen: APInterfaces
-- RoundMult 5,2
-- =============================================
CREATE FUNCTION [dbo].[RoundMult] (@Valor numeric(18,2),@Multiplo Numeric(18,2))
RETURNS nvarchar(50)
AS
BEGIN

declare @Residuo numeric(18,0)
DECLARE @ValASumar numeric(18,0)

set @Residuo=@Valor%@Multiplo

IF @Residuo=0
 BEGIN
  return @Valor
 END
 ELSE
  set @ValASumar= @Multiplo-@Residuo
  return @Valor+@ValASumar
 END