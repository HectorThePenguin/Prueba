USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionMateriaPrima_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionMateriaPrima_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Octavio Quintero>
-- Create date: <11/06/2014>
-- Description:	Guarda la programacion de las cantidades del detalle pedido
/*[ProgramacionMateriaPrima_Crear] '
	<ROOT>
		<ProgramacionMateriaPrima>
				<PedidoDetalleID></PedidoDetalleID>
				<OrganizacionID>1</OrganizacionID>
				<AlmacenID></AlmacenID>
				<AlmacenInventarioLoteID></AlmacenInventarioLoteID>
				<CantidadProgramada></CantidadProgramada>
				<CantidadEntregada></CantidadEntregada>
				<Observaciones></Observaciones>
				<UsuarioCreacionID></UsuarioCreacionID>
		</ProgramacionMateriaPrima>
	</ROOT>
'*/
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionMateriaPrima_Crear] 
	@XML_Programacion XML
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    INSERT INTO ProgramacionMateriaPrima
			(PedidoDetalleID,OrganizacionID,AlmacenID,InventarioLoteIDOrigen,
			 CantidadProgramada,CantidadEntregada,Observaciones,FechaProgramacion,
			 Activo,FechaCreacion,UsuarioCreacionID)
	(SELECT M.IdPedidoDetalle,
			M.IdOrganizacion,
			M.IdAlmacen,
			M.IdLoteInventario,
			M.CantidadProgramada,
			M.CantidadEntregada,
			M.Observaciones,
			GETDATE(),
			1,
			GETDATE(),
			M.UsuarioCreacionID			
	 FROM 
		(SELECT 
			IdPedidoDetalle = T.item.value('./PedidoDetalleID[1]', 'INT'),
			IdOrganizacion   = T.item.value('./OrganizacionID[1]','INT'),
			IdAlmacen = T.item.value('./AlmacenID[1]','INT'),
			IdLoteInventario = T.item.value('./AlmacenInventarioLoteID[1]','INT'),
			CantidadProgramada = T.item.value('./CantidadProgramada[1]','Decimal'),
			CantidadEntregada = T.item.value('./CantidadEntregada[1]','Decimal'),
			Observaciones = T.item.value('./Observaciones[1]','Varchar(255)'),
			UsuarioCreacionID    = T.item.value('./UsuarioCreacionID[1]', 'INT')
		FROM  @XML_Programacion.nodes('ROOT/ProgramacionMateriaPrima') AS T(item)) M
		)
	 SET NOCOUNT OFF;
END

GO
