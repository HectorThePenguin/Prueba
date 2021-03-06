USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_GuardarXml]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoAlimentoDetalle_GuardarXml]
GO
/****** Object:  StoredProcedure [dbo].[RepartoAlimentoDetalle_GuardarXml]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 14/07/2014
-- Description:  Guardar la lista de Reparto Alimento Detalle
-- RepartoAlimentoDetalle_GuardarXml
-- ===============================================================
CREATE PROCEDURE [dbo].[RepartoAlimentoDetalle_GuardarXml] @XmlRepartoAlimentoDetalle XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @RepartoAlimentoDetalle AS TABLE (
		RepartoAlimentoDetalleID INT
		,RepartoAlimentoID INT
		,FolioReparto INT
		,FormulaIDRacion INT
		,Tolva VARCHAR(10)
		,KilosEmbarcados INT
		,KilosRepartidos INT
		,Sobrante INT
		,PesoFinal int
		,CorralIDInicio INT
		,CorralIDFinal INT
		,HoraRepartoInicio VARCHAR(5)
		,HoraRepartoFinal VARCHAR(5)
		,Observaciones VARCHAR(255)
		,Activo BIT
		,UsuarioCreacionID INT
		,UsuarioModificacionID INT
		)
	INSERT @RepartoAlimentoDetalle (
		RepartoAlimentoDetalleID
		,RepartoAlimentoID
		,FolioReparto
		,FormulaIDRacion
		,Tolva
		,KilosEmbarcados
		,KilosRepartidos
		,Sobrante
		,PesoFinal
		,CorralIDInicio
		,CorralIDFinal
		,HoraRepartoInicio
		,HoraRepartoFinal
		,Observaciones
		,Activo
		,UsuarioCreacionID
		,UsuarioModificacionID
		)
	SELECT RepartoAlimentoDetalleID = t.item.value('./RepartoAlimentoDetalleID[1]', 'INT')
		,RepartoAlimentoID = t.item.value('./RepartoAlimentoID[1]', 'INT')
		,FolioReparto = t.item.value('./FolioReparto[1]', 'INT')
		,FormulaIDRacion = t.item.value('./FormulaIDRacion[1]', 'INT')
		,Tolva = t.item.value('./Tolva[1]', 'varchar(10)')
		,KilosEmbarcados = t.item.value('./KilosEmbarcados[1]', 'int')
		,KilosRepartidos = t.item.value('./KilosRepartidos[1]', 'int')
		,Sobrante = t.item.value('./Sobrante[1]', 'int')
		,PesoFinal = t.item.value('./PesoFinal[1]', 'int')
		,CorralIDInicio = t.item.value('./CorralIDInicio[1]', 'int')
		,CorralIDFinal = t.item.value('./CorralIDFinal[1]', 'int')
		,HoraRepartoInicio = t.item.value('./HoraRepartoInicio[1]', 'varchar(5)')
		,HoraRepartoFinal = t.item.value('./HoraRepartoFinal[1]', 'varchar(5)')
		,Observaciones = t.item.value('./Observaciones[1]', 'varchar(255)')
		,Activo = t.item.value('./Activo[1]', 'bit')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'int')
		,UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'int')		
	FROM @XmlRepartoAlimentoDetalle.nodes('ROOT/RepartoAlimentoDetalle') AS T(item)
	INSERT RepartoAlimentoDetalle (
		RepartoAlimentoID
		,FolioReparto
		,FormulaIDRacion
		,Tolva
		,KilosEmbarcados
		,KilosRepartidos
		,Sobrante
		,PesoFinal
		,CorralIDInicio
		,CorralIDFinal
		,HoraRepartoInicio
		,HoraRepartoFinal
		,Observaciones
		,Activo
		,UsuarioCreacionID
		,FechaCreacion
		)
	SELECT RepartoAlimentoID
		,FolioReparto
		,FormulaIDRacion
		,Tolva
		,KilosEmbarcados
		,KilosRepartidos
		,Sobrante
		,PesoFinal
		,CorralIDInicio
		,CorralIDFinal
		,HoraRepartoInicio
		,HoraRepartoFinal
		,Observaciones
		,Activo
		,UsuarioCreacionID
		,GETDATE()
	FROM @RepartoAlimentoDetalle
	UPDATE rad
	SET RepartoAlimentoID = dt.RepartoAlimentoID
		,FolioReparto = dt.FolioReparto
		,FormulaIDRacion = dt.FormulaIDRacion
		,Tolva = dt.Tolva
		,KilosEmbarcados = dt.KilosEmbarcados
		,KilosRepartidos = dt.KilosRepartidos
		,Sobrante = dt.Sobrante
		,PesoFinal = dt.PesoFinal
		,CorralIDInicio = dt.CorralIDInicio
		,CorralIDFinal = dt.CorralIDFinal
		,HoraRepartoInicio = dt.HoraRepartoInicio
		,HoraRepartoFinal = dt.HoraRepartoFinal
		,Observaciones = dt.Observaciones
		,Activo = dt.Activo
		,FechaModificacion = GETDATE()
		,UsuarioModificacionID = dt.UsuarioModificacionID
	FROM RepartoAlimentoDetalle rad
	INNER JOIN @RepartoAlimentoDetalle dt ON rad.RepartoAlimentoDetalleID = dt.RepartoAlimentoDetalleID
	SET NOCOUNT OFF;
END

GO
