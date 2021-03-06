USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_GuardarReimplantes]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LoteReimplante_GuardarReimplantes]
GO
/****** Object:  StoredProcedure [dbo].[LoteReimplante_GuardarReimplantes]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===============================================================
-- Author:    Jorge Luis Velázquez Araujo
-- Create date: 15/01/2014
-- Description:  Guardar la lista de Reimplantes para un Lote
-- 
-- ===============================================================
CREATE PROCEDURE [dbo].[LoteReimplante_GuardarReimplantes] @XmlReimplante XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @LoteReimplante AS TABLE (
		LoteReimplanteID INT
		,LoteProyeccionID INT
		,NumeroReimplante INT
		,FechaProyectada SMALLDATETIME
		,PesoProyectado INT
		,FechaReal SMALLDATETIME
		,PesoReal INT
		,FechaCreacion SMALLDATETIME
		,UsuarioCreacionID INT
		,FechaModificacion SMALLDATETIME
		,UsuarioModificacionID INT
		)

	INSERT @LoteReimplante (
		LoteReimplanteID
		,LoteProyeccionID
		,NumeroReimplante
		,FechaProyectada
		,PesoProyectado		
		,UsuarioCreacionID		
		)
	SELECT LoteReimplanteID = t.item.value('./LoteReimplanteID[1]', 'INT')
		,LoteProyeccionID = t.item.value('./LoteProyeccionID[1]', 'INT')
		,NumeroReimplante = t.item.value('./NumeroReimplante[1]', 'INT')
		,FechaProyectada = t.item.value('./FechaProyectada[1]', 'SMALLDATETIME')
		,PesoProyectado = t.item.value('./PesoProyectado[1]', 'INT')
		,[UsuarioCreacionID] = t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @XmlReimplante.nodes('ROOT/LoteReimplante') AS T(item)

	INSERT LoteReimplante (		
		LoteProyeccionID
		,NumeroReimplante
		,FechaProyectada
		,PesoProyectado
		,FechaCreacion
		,UsuarioCreacionID
		)
	SELECT 	
		LoteProyeccionID
		,NumeroReimplante
		,FechaProyectada
		,PesoProyectado 
		,GETDATE()
		,[UsuarioCreacionID]
	FROM @LoteReimplante
	where LoteReimplanteID = 0

	update lr set 
	lr.FechaProyectada = lr1.FechaProyectada,
	lr.PesoProyectado = lr1.PesoProyectado,
	lr.UsuarioModificacionID = lr1.UsuarioCreacionID,
	lr.FechaModificacion = GETDATE()
	from LoteReimplante lr
	inner join @LoteReimplante lr1 on lr.LoteReimplanteID = lr1.LoteReimplanteID
	

	SET NOCOUNT OFF;
END

GO
