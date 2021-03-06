USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Genera_SapMovto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Genera_SapMovto]
GO
/****** Object:  StoredProcedure [dbo].[Genera_SapMovto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		José Rico - EvoluSoftware
-- Create date: 29 Nov 2014
-- Description:	Crea el Registro faltante en la
--				Tabla SAPMovto para hacer posi-
--				ble la devolución de productos.
-- =============================================
create Procedure [dbo].[Genera_SapMovto]
	@id_Movto	Char(33)
As
Begin
	Set NoCount On;
	
	Declare @cFecha			Char(8),
			@nCliente		Int,
			@nReferencia	Int,
			@cRef3			Char(20)
			
	Set @cFecha			= Substring(@id_Movto, 16,8)
	Set @nCliente		= Convert(Int,Ltrim(Substring(@id_Movto, 24,10)))
	Set @nReferencia	= Convert(Int,Ltrim(Substring(@id_Movto, 4,10)))
	
	Select	@cRef3 = cRef3
	  From	Con_Polizas_SAP (NoLock) 
	 Where	dFecha_Documento	= @cFecha
	   And	cCliente			= @nCliente
	   And	nReferencia			= @nReferencia
	
    Insert Into SapMovto (idMovto, NoPoliza, Consecutivo, bEnviadoCorporativo)
    Values (@id_Movto, @cRef3, 1, 0)
    
    Select @cRef3
End


GO
