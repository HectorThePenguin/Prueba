--========================03_CatParametroBanco_EditarXML========================

IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatParametroBanco_EditarXML' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatParametroBanco_EditarXML
END
GO
--=============================================  
--      Author: Jesús Erubiel Apodaca Soto
-- Create date: 2016/09/23  
-- Description: Edita los parámetros para los cheques.
--	   Origen : AP Interfaces
	   Ejemplo:
--=============================================
CREATE PROCEDURE [dbo].[CatParametroBanco_EditarXML]
	@XmlParametroBanco XML
AS
BEGIN
	DECLARE @CatParametroBancoID INT = 0;
	DECLARE 
		@tblParametroBanco TABLE
		(	
			CatParametroBancoID INT		
			,Descripcion VARCHAR(255)
			,Clave VARCHAR(20)
			,TipoParametro INT
			,Valor VARCHAR(1000)
			,Activo BIT			
			,FechaModificacion DATETIME
			,UsuarioModificacionID INT
		)
	
		INSERT INTO 
			@tblParametroBanco
			(
				CatParametroBancoID 
				,Descripcion
				,Clave
				,TipoParametro
				,Valor
				,Activo				
				,FechaModificacion
				,UsuarioModificacionID
			)
		SELECT
			CatParametroBancoID			= t.item.value('./CatParametroBancoID[1]',				'INT')
			,Descripcion		= t.item.value('./Descripcion[1]',			'VARCHAR(255)')
			,Clave				= t.item.value('./Clave[1]',				'VARCHAR(20)')
			,TipoParametro	= t.item.value('./TipoParametro[1]',		'INT')
			,Valor				= t.item.value('./Valor[1]',				'VARCHAR(1000)')
			,Activo				= t.item.value('./Estatus[1]',				'BIT')
			,FechaModificacion	= GETDATE()
			,UsuarioModificacion = t.item.value('./UsuarioModificacionID[1]',	'INT')
		FROM
			@XmlParametroBanco.nodes('ROOT/ParametroBanco') AS T(item)
				
		UPDATE
			CatParametroBanco
		SET
			Descripcion = tmp.Descripcion 
			,Clave = tmp.Clave
			,TipoParametro = tmp.TipoParametro
			,Valor = tmp.Valor
			,Activo = tmp.Activo						
			,FechaModificacion = GETDATE()
			,UsuarioModificacionID = tmp.UsuarioModificacionID
		FROM
			CatParametroBanco cpb
		INNER JOIN @tblParametroBanco tmp ON
			cpb.CatParametroBancoID = tmp.CatParametroBancoID
		WHERE
			cpb.CatParametroBancoID = tmp.CatParametroBancoID

		SELECT @@ROWCOUNT AS Filas_Afectadas				
	
END
GO