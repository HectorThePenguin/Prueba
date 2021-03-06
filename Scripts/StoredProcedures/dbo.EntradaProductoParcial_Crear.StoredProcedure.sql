USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoParcial_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProductoParcial_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoParcial_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pedro Delgado
-- Create date: 2014/04/02
-- Description: Sp para crear las parcialidades de un contrato
-- Origen     : APInterfaces
/* EXEC  [dbo].[EntradaProductoParcial_Crear] '
<ROOT>
	<EntradaProductoParcial>
		<EntradaProductoID>1193</EntradaProductoID>
		<ContratoParcialID>15</ContratoParcialID>
		<CantidadEntrante>10</CantidadEntrante>
		<UsuarioCreacionID>1</UsuarioCreacionID>
	</EntradaProductoParcial>
</ROOT>'*/
--=============================================
CREATE PROCEDURE [dbo].[EntradaProductoParcial_Crear]
@XMLEntradaProductoParcial XML
AS
BEGIN
	INSERT INTO EntradaProductoParcial (EntradaProductoID,ContratoParcialID,CantidadEntrante,Activo,FechaCreacion,UsuarioCreacionID)
	SELECT
		EntradaProductoID = T.item.value('./EntradaProductoID[1]', 'INT'),
		ContratoParcialID = T.item.value('./ContratoParcialID[1]', 'INT'),
		CantidadEntrante = T.item.value('./CantidadEntrante[1]', 'INT'),
		Activo = 1,
		FechaCreacion = GETDATE(),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @XMLEntradaProductoParcial.nodes('ROOT/EntradaProductoParcial') AS T(item)
END

GO
