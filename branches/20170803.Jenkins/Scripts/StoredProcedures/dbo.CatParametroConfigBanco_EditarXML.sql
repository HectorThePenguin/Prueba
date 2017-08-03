--========================06_CatParametroConfigBanco_EditarXML========================
IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatParametroConfigBanco_EditarXML' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatParametroConfigBanco_EditarXML
END
GO
/*
--=============================================  
--      Author: Jesús Erubiel Apodaca Soto
-- Create date: 2016/09/23  
-- Description: Edita la configuración de parametros de cheques por banco
--              a través del uso de un XML, solo modifica los afectados.
--	   Origen : AP Interfaces
--     Ejemplo:

DECLARE @XMLData XML = '<ROOT>
	<ConfiguracionParametroBancoEditar>
		<CatParametroConfigBancoID>25</CatParametroConfigBancoID>
		<X>1120</X>
		<Y>910</Y>
		<Width>700</Width>
		<Estatus>1</Estatus>
		<FechaModificacion>2016-09-22 16:41:30</FechaModificacion>
		<UsuarioModificacionID>10</UsuarioModificacionID>
	</ConfiguracionParametroBancoEditar>
</ROOT>'
Exec CatParametroConfigBanco_EditarXML @XMLData;

--=============================================  
*/
CREATE PROCEDURE CatParametroConfigBanco_EditarXML
	@XmlParametroConfiguracionBanco XML
AS
BEGIN

	-- TABLA TEMPORAL QUE ALMACENARÁ LA INFORMACIÓN DEL XML 
	DECLARE 
		@tmpParametroConfiguracionBanco TABLE
		(
			CatParametroConfigBancoID INT
			,CatParametroBancoID INT
			,X INT
			,Y INT
			,Width INT
			,Activo BIT			
			,UsuarioModificacionID INT
		)		

	INSERT INTO	
		@tmpParametroConfiguracionBanco
		(
			CatParametroConfigBancoID 
			,CatParametroBancoID 
			,X 
			,Y 
			,Width 
			,Activo 			
			,UsuarioModificacionID
		)
	SELECT		
		CatParametroConfigBancoID	= t.item.value('./CatParametroConfigBancoID[1]',	'INT')
		,CatParametroBancoID						= t.item.value('./CatParametroBancoID[1]',		'INT')
		,X									= t.item.value('./X[1]',				'INT')
		,Y									= t.item.value('./Y[1]',				'INT')
		,Width								= t.item.value('./Width[1]',			'INT')
		,Activo								= t.item.value('./Estatus[1]',			'BIT')		
		,UsuarioModificacion				= t.item.value('./UsuarioModificacionID[1]',	'INT')
	FROM	
		@XmlParametroConfiguracionBanco.nodes('ROOT/ConfiguracionParametroBancoEditar') as T(item)	

	/*SELECT QUE REGRESA LAS DIFERENCIAS */
	UPDATE
		CatParametroConfigBanco
	SET
		X						=	tpc.X 
		,Y						=	tpc.Y 
		,Width					=	tpc.Width			
		,Activo					=	tpc.Activo
		,FechaModificacion		=	GETDATE()
		,UsuarioModificacionID	=	tpc.UsuarioModificacionID			
	FROM
		CatParametroConfigBanco pcb
			INNER JOIN @tmpParametroConfiguracionBanco tpc on
				pcb.CatParametroConfigBancoID = tpc.CatParametroConfigBancoID
	WHERE
		pcb.X <> tpc.X
		OR pcb.Y <> tpc.Y
		OR pcb.Width <> tpc.Width
		OR pcb.Activo <> tpc.Activo

	SELECT @@ROWCOUNT AS Filas_Afectadas
END
GO
