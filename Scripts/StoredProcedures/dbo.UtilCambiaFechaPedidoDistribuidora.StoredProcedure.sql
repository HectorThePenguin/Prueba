USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[UtilCambiaFechaPedidoDistribuidora]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[UtilCambiaFechaPedidoDistribuidora]
GO
/****** Object:  StoredProcedure [dbo].[UtilCambiaFechaPedidoDistribuidora]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create Procedure [dbo].[UtilCambiaFechaPedidoDistribuidora]                      
	@cFecha VarChar(8), 
	@Pedido Int                      
As                      
Begin  
                        
	Set NoCount On
                    
	Begin Transaction
               
		Insert Into BitacoraUtilINV_PedidoDistribuidora                 
		Select	nCanalDistribucion Canal,
				nPedido,
				nCanalDistribucion_Surtira CanalSurtira,
				nCanalDistribucion_Destino Canal_Destino,
				dFecha,
				bSurtido,
				GETDATE()dFechaModificacion               
		  From	INV_PedidoDistribuidora (nolock) Where nPedido=@Pedido              
                
		Update	INV_PedidoDistribuidora Set dFecha=@cFecha                
		 Where	nPedido = @Pedido  
		   And	bActivo=1 
		   And	bSurtido=0        
             
		Delete	From TLM_EmbarquePedidos        
		 Where	PedidoDistribuidora_nPedido = @Pedido 
		   And	dFechaSurtido<@cFecha        
                       
	If @@ERROR <> 0 
		RollBack Transaction
	Else
		Commit Transaction
			                      
	Set NoCount Off    
	                       
End

GO
