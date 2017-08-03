--========================07_CatParametroConfigBanco_Guardar========================

IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatParametroConfigBanco_Guardar' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatParametroConfigBanco_Guardar
END
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 22/09/2016
-- Description: SP para guardar la configuracion del parametro cheque para cada uno de los bancos
-- Origen: APInterfaces
-- SpName     : dbo.CatParametroConfigBanco_Guardar 1,10,10,10,1,1
-- --======================================================
CREATE PROCEDURE CatParametroConfigBanco_Guardar
	@CatParametroBancoID INT,
	@X INT,
	@Y INT,
	@Width INT,
	@Activo BIT,
	@UsuarioCreacionID INT
AS
BEGIN

	INSERT INTO 
		CatParametroConfigBanco 
		(
			BancoID
			,CatParametroBancoID
			,X
			,Y
			,Width
			,Activo
			,FechaCreacion
			,UsuarioCreacionID		
		)
	SELECT
		 B.BancoID
		,@CatParametroBancoID
		,@X
		,@Y
		,@Width
		,@Activo
		,GETDATE()
		,@UsuarioCreacionID	
	FROM
		Banco B

	SELECT @@ROWCOUNT AS Filas_Afectadas
END
GO
