
--========================08_CatParametroBanco_GuardarXML========================

IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatParametroBanco_GuardarXML' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatParametroBanco_GuardarXML
END
GO
/*
--=============================================  
--      Author: Jesús Erubiel Apodaca Soto
-- Create date: 2016/09/22  
-- Description: Agrega parametros de configuración a los bancos y setea
				parametros por default.
--	   Origen : AP Interfaces
-- 	   Ejemplo:

DECLARE @XMLData XML = '<ROOT>
		  <ParametroBanco>
			<Descripcion>NOMBRE DEL PROVEEDOR</Descripcion>
			<Clave>PRM01</Clave>
			<TipoParametro>1</TipoParametro>
			<Valor>Nombre del proveedor: __________________ </Valor>
			<Estatus>1</Estatus>
			<FechaCreacion>2016-09-28 15:20:02.000</FechaCreacion>
			<UsuarioCreacionID>10</UsuarioCreacionID>
			<FechaModificacion>2016-09-28 15:20:02.000</FechaModificacion>
			<UsuarioModificacionID>10</UsuarioModificacionID>
		  </ParametroBanco>
		</ROOT>'

Exec CatParametroBanco_GuardarXML @XMLData, 10,10,100,1
--=============================================
*/
CREATE PROCEDURE CatParametroBanco_GuardarXML
	@XmlParametroBanco XML,
	@X INT,
	@Y INT,
	@With INT,
	@ActivoConfiguracion BIT
AS
BEGIN
	DECLARE @identityID INT = 0;
	DECLARE @Regs INT;
	DECLARE @Counter INT;
	DECLARE @Descripcion VARCHAR(255);
	DECLARE @Clave VARCHAR(20);
	DECLARE @TipoParametro INT;
	DECLARE @Valor VARCHAR(1000);
	DECLARE @Activo BIT;
	DECLARE @FechaCreacion DATETIME;
	DECLARE @UsuarioCreacionID INT;
	DECLARE @FechaModificacion DATETIME;
	DECLARE @UsuarioModificacionID INT;
	
	SET @Counter = 1;
	
	DECLARE 
		@tblParametroBanco TABLE
		(		
			 RowNum INT
			,Descripcion VARCHAR(255)
			,Clave VARCHAR(20)
			,TipoParametro INT
			,Valor VARCHAR(1000)
			,Activo BIT
			,FechaCreacion DATETIME
			,UsuarioCreacionID INT
			,FechaModificacion DATETIME
			,UsuarioModificacionID INT
		)
	BEGIN
		INSERT INTO 
			@tblParametroBanco
			(
				RowNum
				,Descripcion
				,Clave
				,TipoParametro
				,Valor
				,Activo
				,FechaCreacion
				,UsuarioCreacionID
			)
		SELECT
		    ROW_NUMBER() OVER ( ORDER BY t.item.value('./Descripcion[1]', 'VARCHAR(255)') ASC) AS RowNum,
			Descripcion			= t.item.value('./Descripcion[1]',			'VARCHAR(255)')
			,Clave				= t.item.value('./Clave[1]',				'VARCHAR(20)')
			,TipoParametro		= t.item.value('./TipoParametro[1]',		'INT')
			,Valor				= t.item.value('./Valor[1]',				'VARCHAR(1000)')
			,Activo				= t.item.value('./Estatus[1]',				'BIT')
			,FechaCreacion		= GETDATE()
			,UsuarioCreacionID	= t.item.value('./UsuarioCreacionID[1]',	'INT')
		FROM
			@XmlParametroBanco.nodes('ROOT/ParametroBanco') AS T(item)				

		/*INSERTA A LA TABLA CORRESPONDIENTE*/
		
		SET @Regs = (SELECT ISNULL(COUNT(*),0) FROM @tblParametroBanco);
		WHILE @Counter <= @Regs
		BEGIN
			SELECT
					 @Descripcion = tmp.Descripcion
					,@Clave = tmp.Clave
					,@TipoParametro = tmp.TipoParametro
					,@Valor = tmp.Valor
					,@Activo = tmp.Activo
					,@FechaCreacion = tmp.FechaCreacion
					,@UsuarioCreacionID = tmp.UsuarioCreacionID
								
				FROM
					@tblParametroBanco tmp	
				WHERE tmp.RowNum = @Counter;
			
			
			INSERT INTO CatParametroBanco
				(
					Descripcion
					,Clave
					,TipoParametro
					,Valor
					,Activo
					,FechaCreacion
					,UsuarioCreacionID
				)
				VALUES
				(
					@Descripcion,
					@Clave,
					@TipoParametro,
					@Valor,
					@Activo,
					@FechaCreacion,
					@UsuarioCreacionID
				)
				
			SET @identityID = @@IDENTITY
			
			Exec CatParametroConfigBanco_Guardar @identityID,@X,@Y,@With,@ActivoConfiguracion,@usuarioCreacionID;
					
			SET @Counter = @Counter + 1
		END
	END	
	/*REGRESA EL RESULTADO DEL INSERT*/
	SELECT @@ROWCOUNT AS Filas_Afectadas
END
GO


