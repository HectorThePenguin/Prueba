IF EXISTS(SELECT *
      FROM   sys.objects
      WHERE  [object_id] = Object_id(N'[dbo].[ObtenerConstanteValor]'))
		DROP FUNCTION [dbo].[ObtenerConstanteValor]
GO
CREATE FUNCTION dbo.ObtenerConstanteValor (@Desc VARCHAR(50), @OrganizacionID INT, @Codigo INT, @Sexo CHAR(1))
RETURNS VARCHAR(20)
AS
BEGIN

DECLARE @Valor VARCHAR(11);

SELECT @Valor = CAST(Valor AS VARCHAR)
FROM Calculo C (NOLOCK)
INNER JOIN Constantes Co (NOLOCK)
	ON C.CalculoID = Co.CalculoID
WHERE C.Descripcion = @Desc AND co.OrganizacionID = @OrganizacionID AND Co.Codigo = @Codigo AND Co.Sexo = @Sexo;

RETURN (ISNULL(@Valor,'0'));

END