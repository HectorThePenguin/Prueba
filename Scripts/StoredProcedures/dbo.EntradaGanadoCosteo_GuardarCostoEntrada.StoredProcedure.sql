USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_GuardarCostoEntrada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanadoCosteo_GuardarCostoEntrada]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanadoCosteo_GuardarCostoEntrada]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===========================================================================
-- Author:    José Gilberto Quintero López
-- Create date: 31/10/2013
-- Description:  Guardar el detalle de los costos (EntradaGanadoCostoEntrada)
-- genera_sp EntradaGanadoCostoEntrada
-- ==========================================================================
CREATE PROCEDURE [dbo].[EntradaGanadoCosteo_GuardarCostoEntrada]
	@XmlDetalle XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Detalle AS TABLE
	  (
		[EntradaGanadoCostoID] INT,
		[EntradaGanadoCosteoID] INT,
		[CostoID] BIGINT,
		[TieneCuenta] BIT,
		[ProveedorID] INT,
		[NumeroDocumento] VARCHAR(20),
		[Importe] DECIMAL(10,2),
		[ProveedorComisionID] INT,
		[Iva] BIT,
		[Retencion] BIT,		
		[Activo] BIT,
		[UsuarioCreacion] INT,
		[UsuarioModificacion] INT,
		[CuentaProvision] VARCHAR(10)
	  )

	INSERT @Detalle
		   (
			[EntradaGanadoCostoID],
			[EntradaGanadoCosteoID],
			[CostoID],
			[TieneCuenta],
			[ProveedorID],
			[NumeroDocumento],
			[ProveedorComisionID],
			[Importe],
			[Iva],
			[Retencion],			
			[Activo],
			[UsuarioCreacion],
			[UsuarioModificacion],
			[CuentaProvision]
			)

		SELECT
			[EntradaGanadoCostoID]= t.item.value('./EntradaGanadoCostoID[1]', 'INT'),
			[EntradaGanadoCosteoID]      = t.item.value('./EntradaGanadoCosteoID[1]', 'INT'),
			[CostoID]                    = t.item.value('./CostoID[1]', 'INT'),
			[TieneCuenta]                = t.item.value('./TieneCuenta[1]', 'BIT'),
			[ProveedorID]                = t.item.value('./ProveedorID[1]', 'INT'),
			[NumeroDocumento]            = t.item.value('./NumeroDocumento[1]', 'VARCHAR(20)'),
			[ProveedorComisionID]		 = case when t.item.value('./ProveedorComisionID[1]', 'INT') = 0 then null else t.item.value('./ProveedorComisionID[1]', 'INT') end,
			[Importe]                    = t.item.value('./Importe[1]', 'DECIMAL(10,2)'),
			[Iva]                        = t.item.value('./Iva[1]', 'BIT'),
			[Retencion]                  = t.item.value('./Retencion[1]', 'BIT'),			
			[Activo]                     = t.item.value('./Activo[1]', 'BIT'),
			[UsuarioCreacion]            = t.item.value('./UsuarioCreacion[1]', 'INT'),
			[UsuarioModificacion]        = t.item.value('./UsuarioModificacion[1]', 'INT'),
			[CuentaProvision]                   = t.item.value('./CuentaProvision[1]', 'VARCHAR(10)')

			FROM @XmlDetalle.nodes('ROOT/EntradaGanadoCosto') AS T(item)
			
			update @Detalle set ProveedorID = NULL
			where ProveedorID = 0

	UPDATE ec
	SET    
		[EntradaGanadoCosteoID]      = dt.[EntradaGanadoCosteoID],
		[CostoID]                    = dt.[CostoID],
		[TieneCuenta]                = dt.[TieneCuenta],
		[ProveedorID]                = dt.[ProveedorID],
		[NumeroDocumento]            = dt.[NumeroDocumento],
		[Importe]                    = dt.[Importe],
		[ProveedorComisionID]		 = case dt.[ProveedorComisionID] when 0 then NULL else dt.[ProveedorComisionID] end,
		[Iva]                        = dt.[Iva],
		[Retencion]                  = dt.[Retencion],    		
		[Activo]                     = dt.[Activo],
		[FechaModificacion]          = GETDATE(),
		[UsuarioModificacion]        = NULLIF(dt.[UsuarioModificacion], 0) ,
		[CuentaProvision]                   = dt.[CuentaProvision]
	FROM  EntradaGanadoCosto ec
	INNER JOIN @Detalle dt
		ON dt.EntradaGanadoCostoID =  ec.EntradaGanadoCostoID

	INSERT EntradaGanadoCosto
		   (
			[EntradaGanadoCosteoID],      
			[CostoID],                    
			[TieneCuenta],                
			[ProveedorID],                
			[NumeroDocumento],            
			[Importe],  
			[ProveedorComisionID],
			[Iva],                        
			[Retencion],                  			                 
			[Activo],                     
			[FechaCreacion],              
			[UsuarioCreacion],            
			[CuentaProvision] 
			)

	SELECT 
		[EntradaGanadoCosteoID],      
		[CostoID],                    
		[TieneCuenta],                
		[ProveedorID],                
		[NumeroDocumento],            
		[Importe],  
		case [ProveedorComisionID] when 0 then NULL else [ProveedorComisionID] end,
		[Iva],                        
		[Retencion],                  		              
		[Activo],                     
		GETDATE(),              
		[UsuarioCreacion],            
		[CuentaProvision]                   

	FROM @Detalle
	WHERE EntradaGanadoCostoID = 0

	SET NOCOUNT OFF;
END

GO
