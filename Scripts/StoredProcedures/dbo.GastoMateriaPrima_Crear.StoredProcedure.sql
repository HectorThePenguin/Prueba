USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[GastoMateriaPrima_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[GastoMateriaPrima_Crear]
GO
/****** Object:  StoredProcedure [dbo].[GastoMateriaPrima_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Alejandro Quiroz	
-- Create date: 17/07/2014
-- Description: Crea un nuevo gasto materia prima
-- GastoMateriaPrima_Crear 1,1,1,1,1,1,null,1,100,1,1,'Prueba sdjfkj',1,1
-- DROP PROCEDURE GastoMateriaPrima_Crear
--=============================================
CREATE PROCEDURE [dbo].[GastoMateriaPrima_Crear]
	@AlmacenMovimientoID bigint,
	@OrganizacionID int,
	@TipoMovimientoID int,
	@ProductoID int,
	@TieneCuenta bit,
	@CuentaSAPID int,
	@ProveedorID int,
	@AlmacenInventarioLoteID int,
	@Importe decimal(18,2),
	@IVA bit,	
	@Observaciones varchar(255),
	@Activo bit,
	@TipoFolio int,
	@UsuarioCreacionID int
	, @Cantidad DECIMAL(18,2)
AS
BEGIN
	DECLARE @FolioGasto INT
	EXEC Folio_Obtener @OrganizacionID, @TipoFolio, @Folio = @FolioGasto output
    IF (@AlmacenInventarioLoteID = 0) SET @AlmacenInventarioLoteID = NULL
	IF (@ProveedorID = 0) SET @ProveedorID = NULL 
	IF (@CuentaSAPID = 0) SET @CuentaSAPID = NULL  
	INSERT INTO [dbo].[GastoMateriaPrima]
			   ([AlmacenMovimientoID]
			   ,[OrganizacionID]
			   ,[FolioGasto]
			   ,[TipoMovimientoID]
			   ,[Fecha]
			   ,[ProductoID]
			   ,[TieneCuenta]
			   ,[CuentaSAPID]
			   ,[ProveedorID]
			   ,[AlmacenInventarioLoteID]
			   ,[Importe]
			   ,[IVA]			   
			   ,[Observaciones]
			   ,[Activo]
			   ,[FechaCreacion]
			   ,[UsuarioCreacionID]
			   ,[Cantidad]
			   )
		 VALUES
			   (@AlmacenMovimientoID
			   ,@OrganizacionID
			   ,@FolioGasto
			   ,@TipoMovimientoID
			   ,GETDATE()
			   ,@ProductoID
			   ,@TieneCuenta
			   ,@CuentaSAPID
			   ,@ProveedorID
			   ,@AlmacenInventarioLoteID
			   ,@Importe
			   ,@IVA			   
			   ,@Observaciones
			   ,@Activo
			   ,GETDATE()
			   ,@UsuarioCreacionID
			   ,@Cantidad
			   )
	select SCOPE_IDENTITY()
END

GO
