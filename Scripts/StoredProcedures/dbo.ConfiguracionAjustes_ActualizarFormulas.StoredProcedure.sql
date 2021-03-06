USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAjustes_ActualizarFormulas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionAjustes_ActualizarFormulas]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionAjustes_ActualizarFormulas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author:		Ramses Santos
-- Create date: 16/09/2014
-- Description:	Actualiza las formulas del reparto sin afectar los kilogramos programados.
/*
	ConfiguracionAjustes_ActualizarFormulas
	'<ROOT>
		<RepartoGrabar>
			<RepartoID>132986</RepartoID>
			<RepartoDetalleIdManiana>163225</RepartoDetalleIdManiana>
			<RepartoDetalleIdTarde>0</RepartoDetalleIdTarde>
			<OrganizacionID>1</OrganizacionID>
			<CorralCodigo>C13</CorralCodigo>
			<Lote>801</Lote>
			<TipoServicioID>1</TipoServicioID>
			<FormulaIDProgramada>2</FormulaIDProgramada>
			<CantidadProgramada>100</CantidadProgramada>
			<EstadoComederoID>0</EstadoComederoID>
			<Observaciones></Observaciones>
			<FechaReparto>2014-09-13T00:00:00</FechaReparto>
			<UsuarioCreacionID>5</UsuarioCreacionID>
			<Servido>0</Servido>
			<ValidaPorcentaje>0</ValidaPorcentaje>
		</RepartoGrabar>
		<RepartoGrabar>
			<RepartoID>132986</RepartoID>
			<RepartoDetalleIdManiana>0</RepartoDetalleIdManiana>
			<RepartoDetalleIdTarde>163884</RepartoDetalleIdTarde>
			<OrganizacionID>1</OrganizacionID>
			<CorralCodigo>C13</CorralCodigo>
			<Lote>801</Lote>
			<TipoServicioID>2</TipoServicioID>
			<FormulaIDProgramada>1</FormulaIDProgramada>
			<CantidadProgramada>100</CantidadProgramada>
			<EstadoComederoID>0</EstadoComederoID>
			<Observaciones></Observaciones>
			<FechaReparto>2014-09-13T00:00:00</FechaReparto>
			<UsuarioCreacionID>5</UsuarioCreacionID>
			<Servido>0</Servido>
			<ValidaPorcentaje>0</ValidaPorcentaje>
		</RepartoGrabar>
	</ROOT>'
*/
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionAjustes_ActualizarFormulas]
@XmlReparto XML
AS
BEGIN
	IF OBJECT_ID('tempdb..#Reparto') IS NOT NULL 
			DROP TABLE #Reparto
	CREATE TABLE #Reparto (
		OrganizacionID INT,
		RepartoID BIGINT,
		RepartoDetalleIdManiana BIGINT,
		RepartoDetalleIdTarde BIGINT,
		CorralCodigo VARCHAR(20),
		Lote VARCHAR(20),
		TipoServicioID INT,
		FormulaIDProgramada INT,
		CantidadProgramada INT,
		EstadoComederoID INT,
		Observaciones VARCHAR(250),
		FechaReparto DATE,
		UsuarioCreacionID INT,
		Servido INT,
		ValidaPorcentaje INT
	)
	INSERT INTO #Reparto 
	            (OrganizacionID, RepartoID, RepartoDetalleIdManiana, RepartoDetalleIdTarde, CorralCodigo, Lote, TipoServicioID, FormulaIDProgramada, CantidadProgramada,
	                                  EstadoComederoID, Observaciones, FechaReparto, UsuarioCreacionID, Servido, ValidaPorcentaje)
	SELECT 
		OrganizacionID  = T.item.value('./OrganizacionID[1]', 'INT'),
		RepartoID  = T.item.value('./RepartoID[1]', 'BIGINT'),
		RepartoDetalleIdManiana  = T.item.value('./RepartoDetalleIdManiana[1]', 'BIGINT'),
		RepartoDetalleIdTarde  = T.item.value('./RepartoDetalleIdTarde[1]', 'BIGINT'),
		CorralCodigo  = T.item.value('./CorralCodigo[1]', 'VARCHAR(20)'),
		Lote  = CASE WHEN T.item.value('./Lote[1]', 'VARCHAR(20)') = '0' THEN NULL ELSE T.item.value('./Lote[1]', 'VARCHAR(20)') END,
		TipoServicioID    = T.item.value('./TipoServicioID[1]', 'INT'),
		FormulaIDProgramada    = T.item.value('./FormulaIDProgramada[1]', 'INT'),
		CantidadProgramada  = T.item.value('./CantidadProgramada[1]', 'INT'),
		EstadoComederoID   = T.item.value('./EstadoComederoID[1]', 'INT'),
		Observaciones = T.item.value('./Observaciones[1]','VARCHAR(250)'),
		FechaReparto = CONVERT(CHAR(8),T.item.value('./FechaReparto[1]','DATETIME'),112),
		UsuarioCreacionID = T.item.value('./UsuarioCreacionID[1]', 'INT'),
		Servido = T.item.value('./Servido[1]', 'INT'),
		ValidaPorcentaje = T.item.value('./ValidaPorcentaje[1]', 'INT')
	FROM  @XmlReparto.nodes('ROOT/RepartoGrabar') AS T(item)
	UPDATE RD SET RD.FormulaIDProgramada = Tmp.FormulaIDProgramada, RD.UsuarioModificacionID = Tmp.UsuarioCreacionID, RD.FechaModificacion = GETDATE(), RD.Ajuste = 1
	FROM RepartoDetalle RD
	INNER JOIN #Reparto Tmp ON (Tmp.RepartoID = RD.RepartoID AND
	(RD.RepartoDetalleID = Tmp.RepartoDetalleIdManiana OR RD.RepartoDetalleID = Tmp.RepartoDetalleIdTarde))
	WHERE Tmp.RepartoID > 0
	AND Tmp.Servido = 0
END

GO
