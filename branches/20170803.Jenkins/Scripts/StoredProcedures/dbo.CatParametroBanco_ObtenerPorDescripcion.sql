USE [SIAP]

--========================01_CatConfiguracionParametroBanco_ObtenerPorBanco========================
GO
IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'Banco_ObtenerPorID' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE Banco_ObtenerPorID
END
GO
--=============================================  
-- Author     : Audomar Piña Aguilar
-- Create date: 2015/04/08  
-- Description: Banco_ObtenerPorID 5  
-- Modificado : Roque Solis
-- Fecha	  :	24/09/2016
-- Modificacion : Se agrega los campos del pais
--=============================================  
CREATE PROCEDURE [dbo].[Banco_ObtenerPorID]  
@BancoID INT  
AS  
BEGIN  
	SET NOCOUNT ON;  

	SELECT B.BancoID,  
		   B.Descripcion,  
		   B.Telefono,
		   b.PaisID,
		   p.Descripcion AS Pais,
		   b.Telefono,  
		   b.Activo 
	  FROM Banco B  
	  INNER JOIN Pais p 
	    ON b.PaisID = p.PaisID
	 WHERE B.BancoID = @BancoID  

	SET NOCOUNT OFF;  
END  
GO


--========================02_CatConfiguracionParametroBanco_ObtenerPorBanco========================

IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatConfiguracionParametroBanco_ObtenerPorBanco' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatConfiguracionParametroBanco_ObtenerPorBanco
END
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 23/09/2016
-- Description: SP para la configuracion del parametro banco
-- Origen: APInterfaces
-- SpName     : dbo.CatConfiguracionParametroBanco_ObtenerPorBanco 1,1
-- --======================================================
CREATE PROCEDURE [dbo].[CatConfiguracionParametroBanco_ObtenerPorBanco]
	@BancoID INT,
	@Activo BIT,
	@Usuario INT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS(SELECT CatParametroConfigBancoID FROM  CatParametroConfigBanco WHERE BancoID = @BancoID)
		BEGIN
			INSERT INTO CatParametroConfigBanco
			SELECT
				@BancoID,
				PC.CatParametroBancoID,
				0,
				0,
				0,
				PC.Activo,
				getdate(),
				@Usuario,
				NULL,
				NULL
			FROM CatParametroBanco PC (Nolock)
			ORDER BY PC.CatParametroBancoID
		END
			SELECT
				PC.CatParametroConfigBancoID,
				PC.BancoID,
				PC.CatParametroBancoID,
				PB.Descripcion,
				PB.Valor,
				PC.X,
				PC.Y,
				PC.Width,
				PC.Activo
			FROM CatParametroConfigBanco PC (Nolock)
				INNER JOIN CatParametroBanco PB ON PC.CatParametroBancoID = PB.CatParametroBancoID
			WHERE PC.BancoID = @BancoID
				AND PB.Activo = @Activo
			ORDER BY PC.CatParametroConfigBancoID

		SET NOCOUNT OFF;

END
GO
--========================03_CatParametroBanco_EditarXML========================

IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatParametroBanco_EditarXML' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatParametroBanco_EditarXML
END
GO
/*
--=============================================  
--      Author: Jesús Erubiel Apodaca Soto
-- Create date: 2016/09/23  
-- Description: Edita los parámetros para los cheques.
--	   Origen : AP Interfaces
	   Ejemplo:

DECLARE @XMLData XML = '<ROOT>
		  <ParametroBanco>
			<CatParametroBancoID>4</CatParametroBancoID>
			<Descripcion>NOMBRE DEL PROVEEDOR</Descripcion>
			<Clave>PRM01</Clave>
			<TipoParametro>1</TipoParametro>
			<Valor>ESTE VALOR CAMBIO</Valor>
			<Estatus>1</Estatus>
			<FechaCreacion>2016-09-28 15:20:02.000</FechaCreacion>
			<UsuarioCreacionID>10</UsuarioCreacionID>
			<FechaModificacion>2016-09-28 15:20:02.000</FechaModificacion>
			<UsuarioModificacionID>10</UsuarioModificacionID>
		  </ParametroBanco>
		</ROOT>'

Exec CatParametroBanco_EditarXML @XMLData
*/
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



--========================04_CatParametroBanco_ObtenerPorDescripcion========================

IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatParametroBanco_ObtenerPorDescripcion' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatParametroBanco_ObtenerPorDescripcion
END
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 23/09/2016
-- Description: SP para obtener los parametros de banco
-- Origen: APInterfaces
-- SpName     : dbo.CatParametroBanco_ObtenerPorDescripcion 'Parametro 1',1
-- --======================================================
CREATE PROCEDURE [dbo].[CatParametroBanco_ObtenerPorDescripcion]
@Descripcion varchar(255),
@Activo BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY Descripcion ASC) AS [RowNum],
		CatParametroBancoID,
		Descripcion,
		Clave,
		TipoParametro,
		Valor,
		Activo
	FROM CatParametroBanco 
	WHERE Descripcion =  @Descripcion 
	AND Activo = @Activo
	
	SET NOCOUNT OFF;
END
GO

--========================05_CatParametroBanco_ObtenerPorPagina========================

IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatParametroBanco_ObtenerPorPagina' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatParametroBanco_ObtenerPorPagina
END
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 22/09/2016
-- Description: SP para obtener los datos de la CatParametroBanco
-- Origen: APInterfaces
-- SpName     : dbo.CatParametroBanco_ObtenerPorPagina '',1,1,0,10
-- --======================================================
CREATE PROCEDURE [dbo].[CatParametroBanco_ObtenerPorPagina]
@Descripcion varchar(255),
@TipoParametro INT,
@Activo BIT,
@Inicio INT,
@Limite INT 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ROW_NUMBER() OVER (ORDER BY CatParametroBancoID ASC) AS [RowNum],
		CatParametroBancoID,
		Descripcion,
		Clave,
		TipoParametro,
		Valor,
		Activo
	INTO #CatParametroBanco
	FROM CatParametroBanco 
	WHERE (Descripcion like '%' + @Descripcion + '%' OR @Descripcion = '')
	AND @TipoParametro IN (TipoParametro, 0)
	AND Activo = @Activo
	ORDER BY CatParametroBancoID ASC
	
	SELECT
		CatParametroBancoID,
		Descripcion,
		Clave,
		TipoParametro,
		Valor,
		Activo
	FROM #CatParametroBanco
	WHERE RowNum BETWEEN @Inicio AND @Limite
	ORDER BY CatParametroBancoID ASC
	
	SELECT
	COUNT(CatParametroBancoID) AS [TotalReg]
	FROM #CatParametroBanco
	
	DROP TABLE #CatParametroBanco
	SET NOCOUNT OFF;
END
GO


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


