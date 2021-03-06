USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Pedido_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Pedido_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Pedro Delgado
-- Create date: 29/06/2014
-- Description:  Guardar el pedido
-- Origen: APInterfaces
-- Pedido_Crear 4,1,28,1,13,1
-- =============================================
CREATE PROCEDURE [dbo].[Pedido_Crear]
	@OrganizacionID INT,
	@AlmacenID INT,
	@EstatusID INT,
	@UsuarioCrecionID INT,
	@TipoFolioID INT,
	@Observaciones VARCHAR(255),
	@Activo BIT
AS
  BEGIN
    SET NOCOUNT ON
	DECLARE @FolioPedido INT ;
	DECLARE @IdentityID BIGINT;
	EXEC Folio_Obtener @OrganizacionID, @TipoFolioID, @Folio = @FolioPedido OUTPUT
	INSERT INTO Pedido (OrganizacionID, 
							  AlmacenID, 
							  FolioPedido, 
							  FechaPedido,
							  Observaciones, 
							  EstatusID, 
							  Activo, 
							  FechaCreacion,
								UsuarioCreacionID) 
	VALUES (@OrganizacionID, 
	        @AlmacenID, 
			@FolioPedido, 
			GETDATE(), 
			@Observaciones, 
			@EstatusID,
			@Activo,
			GETDATE(),
			@UsuarioCrecionID)
	/* Se obtiene el id Insertado */
	SET @IdentityID = (SELECT @@IDENTITY)
	SELECT 
		PedidoID,
		OrganizacionID,
		AlmacenID,
		FolioPedido,
		FechaPedido,
		Observaciones,
		EstatusID,
		Activo,
		FechaCreacion,
		UsuarioCreacionID
	FROM Pedido 
	WHERE PedidoID = @IdentityID
	SET NOCOUNT OFF
  END

GO
