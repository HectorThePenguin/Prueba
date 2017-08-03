IF EXISTS(SELECT *
          FROM   sys.objects
          WHERE  [object_id] = Object_id(N'[dbo].[Chequera_ValidarChequesGirados]'))
  DROP PROCEDURE [dbo].[Chequera_ValidarChequesGirados]
GO
-- =============================================================================================
-- Autor: Sergio Alberto Gamez Gomez
-- Fecha: 23-10-2015
-- Descripcion:	Regresar el cheque final si la chequera no ha llegado a su tope.
-- Chequera_ValidarChequesGirados 8,1
-- =============================================================================================
CREATE PROCEDURE [dbo].[Chequera_ValidarChequesGirados]
	@OrganizacionId INT,
	@ChequeraId INT 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT         
		ChequeraIDFinal
	FROM [Sukarne].[dbo].[Chequera] C (NOLOCK)    
	LEFT OUTER JOIN [Sukarne].[dbo].[CacCheque] CC (NOLOCK)
		ON CAST(CC.NumeroChequera AS INT) = C.ChequeraId AND CC.OrganizacionId = C.OrganizacionId  
	WHERE C.ChequeraId = @ChequeraId AND C.OrganizacionId = @OrganizacionId
	GROUP BY C.ChequeraIDFinal
	HAVING C.ChequeraIDFinal > ISNULL(MAX(ChequeID),C.ChequeraIDFinal)	

	SET NOCOUNT OFF;  
END
GO