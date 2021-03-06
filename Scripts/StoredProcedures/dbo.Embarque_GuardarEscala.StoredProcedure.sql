USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_GuardarEscala]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Embarque_GuardarEscala]
GO
/****** Object:  StoredProcedure [dbo].[Embarque_GuardarEscala]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jos� Gilberto Quintero L�pez
-- Create date: 31/10/2013
-- Description:  Guardar el detalle de las escalas del embarque
-- 
-- =============================================
CREATE PROCEDURE [dbo].[Embarque_GuardarEscala]		
	@XmlEscala XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Detalle AS TABLE
	  (
		[EmbarquedetalleID]			INT,
		[EmbarqueID]				INT,
		[ProveedorID]               INT,
		[ChoferID]                  INT,
		[JaulaID]                   INT,
		[CamionID]                  INT,
		[OrganizacionOrigenID]      INT,
		[OrganizacionDestinoID]     INT,
		[FechaSalida]               SMALLDATETIME,
		[FechaLlegada]              SMALLDATETIME,
		[Orden]                     INT,
		[Horas]						DECIMAL,
		[Recibido]                  BIT,
		[Activo]                    BIT,
		[FechaCreacion]             SMALLDATETIME,
		[UsuarioCreacionID]         INT,
		[FechaModificacion]         SMALLDATETIME,
		[UsuarioModificacionID]     INT,
		[Comentarios]				VARCHAR(255),
		[Kilometros]				DECIMAL(10,2)
	  )
	INSERT @Detalle
		   ([EmbarqueDetalleID],
			[EmbarqueID],
			[ProveedorID],
			[ChoferID],              
			[JaulaID],               
			[CamionID],              
			[OrganizacionOrigenID],
			[OrganizacionDestinoID], 
			[FechaSalida],           
			[FechaLlegada],          
			[Orden],   
			[Horas],                 
			[Recibido],              
			[Activo],                
			[UsuarioCreacionID],     
			[UsuarioModificacionID],
			[Comentarios]
			,[Kilometros]
			)
		SELECT 
				[EmbarquedetalleID]		 = t.item.value('./EmbarqueDetalleID[1]', 'INT'),
				[EmbarqueID]			 = t.item.value('./EmbarqueID[1]', 'INT'),
				[ProveedorID]            = t.item.value('./ProveedorID[1]', 'INT'),
				[ChoferID]               = t.item.value('./ChoferID[1]', 'INT'),
				[JaulaID]                = t.item.value('./JaulaID[1]', 'INT'),
				[CamionID]               = t.item.value('./CamionID[1]', 'INT'),
				[OrganizacionOrigenID]   = t.item.value('./OrganizacionOrigenID[1]', 'INT'),
				[OrganizacionDestinoID]  = t.item.value('./OrganizacionDestinoID[1]', 'INT'),
				[FechaSalida]            = t.item.value('./FechaSalida[1]', 'SMALLDATETIME'),
				[FechaLlegada]           = t.item.value('./FechaLlegada[1]', 'SMALLDATETIME'),
				[Orden]                  = t.item.value('./Orden[1]', 'INT'),
				[Horas]                  = t.item.value('./Horas[1]', 'DECIMAL'),
				[Recibido]               = t.item.value('./Recibido[1]', 'BIT'),
				[Activo]                 = t.item.value('./Activo[1]', 'BIT'),
				[UsuarioCreacionID]      = t.item.value('./UsuarioCreacionID[1]', 'INT'),
				[UsuarioModificacionID]  = t.item.value('./UsuarioModificacionID[1]', 'INT'),
				[Comentarios]			 = t.item.value('./Comentarios[1]', 'VARCHAR(255)')
				,[Kilometros]			 = t.item.value('./Kilometros[1]', 'DECIMAL(10,2)')
			FROM   @XmlEscala.nodes('ROOT/Embarque') AS T(item)
	UPDATE pr
	SET    [EmbarqueID]			   = dt.EmbarqueID,
		   [ProveedorID]           = dt.ProveedorID,          
		   [ChoferID]              = dt.ChoferID,             
		   [JaulaID]               = dt.JaulaID,              
		   [CamionID]              = dt.CamionID,             
		   [OrganizacionOrigenID]  = dt.OrganizacionOrigenID ,
		   [OrganizacionDestinoID] = dt.OrganizacionDestinoID,
		   [FechaSalida]           = dt.FechaSalida,          
		   [FechaLlegada]          = dt.FechaLlegada,         
		   [Orden]                 = dt.Orden,    
		   [Horas]				   = dt.Horas,            
		   [Recibido]              = dt.Recibido,             
		   [Activo]                = dt.Activo,               
		   [FechaModificacion]     = GETDATE(),    
		   [UsuarioModificacionID] = dt.UsuarioModificacionID,
		   [Comentarios]		   = dt.Comentarios
		   ,[Kilometros]		   = dt.Kilometros
	FROM   EmbarqueDetalle pr
		   INNER JOIN @Detalle dt
				   ON dt.EmbarquedetalleID =  pr.EmbarquedetalleID
	INSERT EmbarqueDetalle
		   ([EmbarqueID],
			[ProveedorID],
			[ChoferID],
			[JaulaID],
			[CamionID],
			[OrganizacionOrigenID],
			[OrganizacionDestinoID],
			[FechaSalida],
			[FechaLlegada],
			[Orden],
			[Horas],
			[Recibido],
			[Activo],
			[FechaCreacion],
			[UsuarioCreacionID],
			[Comentarios],
			[Kilometros]
			)
	SELECT [EmbarqueID],
		   [ProveedorID],
		   [ChoferID],
		   [JaulaID],
		   [CamionID],
		   [OrganizacionOrigenID],
		   [OrganizacionDestinoID],
		   [FechaSalida],
		   [FechaLlegada],
		   [Orden],
		   [Horas],
		   [Recibido],
		   [Activo],
		   GETDATE(),
		   [UsuarioCreacionID],
		   [Comentarios]
		   , [Kilometros]
	FROM   @Detalle
	WHERE  EmbarquedetalleID = 0
	SET NOCOUNT OFF;
END

GO
