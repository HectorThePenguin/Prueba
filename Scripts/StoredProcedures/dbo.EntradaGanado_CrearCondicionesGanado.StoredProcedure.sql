USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_CrearCondicionesGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaGanado_CrearCondicionesGanado]
GO
/****** Object:  StoredProcedure [dbo].[EntradaGanado_CrearCondicionesGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raul Esquer
-- Create date: 16-10-2013
-- Description:	Guarda una lista de condiciones de ganado
-- =============================================
CREATE PROCEDURE [dbo].[EntradaGanado_CrearCondicionesGanado]
	@XmlCondiciones XML
AS
BEGIN	
	SET NOCOUNT ON;
	SELECT  
		EntradaCondicionID = T.Item.value('./EntradaCondicionID[1]', 'INT'),
		EntradaGanadoID = T.Item.value('./EntradaGanadoID[1]', 'INT'), 
	    CondicionID = T.Item.value('./CondicionID[1]', 'INT'), 
		Cabezas = T.Item.value('./Cabezas[1]', 'INT'), 
		Activo = T.Item.value('./Activo[1]', 'BIT'), 					
		UsuarioID = T.Item.value('./Usuario[1]', 'INT'),
		RegistroModificado = T.Item.value('./RegistroModificado[1]', 'BIT')
	INTO #EntradaCondicion
	FROM @XmlCondiciones.nodes('ROOT/EntradaGanado') AS T(Item)
	INSERT INTO EntradaCondicion (			
			EntradaGanadoID,
			CondicionID,
			Cabezas,
			Activo,
			FechaCreacion,
			UsuarioCreacionID ) 	
	SELECT 
		EntradaGanadoID, 
		CondicionID,
		Cabezas,	
		Activo, 
		Getdate(),
		UsuarioID
	FROM #EntradaCondicion C
	WHERE C.EntradaCondicionID = 0
	Update EntradaCondicion
	Set CondicionID = E.CondicionID,
		Cabezas = E.Cabezas, 
		Activo = E.Activo, 
		FechaModificacion = Getdate(), 
		UsuarioModificacionID = NULLIF(E.UsuarioID,0)
	FROM EntradaCondicion EC
		INNER JOIN #EntradaCondicion E
			ON  EC.EntradaCondicionID = E.EntradaCondicionID
	Where E.RegistroModificado = 1
	DROP TABLE #EntradaCondicion		
END

GO
