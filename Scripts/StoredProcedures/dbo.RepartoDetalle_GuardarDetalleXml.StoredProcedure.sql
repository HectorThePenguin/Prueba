USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoDetalle_GuardarDetalleXml]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoDetalle_GuardarDetalleXml]
GO
/****** Object:  StoredProcedure [dbo].[RepartoDetalle_GuardarDetalleXml]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 02/09/2014
-- Description:  Guardar la lista de Reparto Detalle
-- RepartoDetalle_GuardarDetalleXml
-- ===============================================================
CREATE PROCEDURE [dbo].[RepartoDetalle_GuardarDetalleXml] @XmlRepartoDetalle XML
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @RepartoDetalle AS TABLE (
		RepartoID int		
		,TipoServicioID INT
		,FormulaIDProgramada int
		,FormulaIDServida int
		,CantidadProgramada int
		,CantidadServida int	
		,HoraReparto char(5)
		,CostoPromedio 	decimal(12,4)
		,Importe decimal(18,2)
		,Servido bit
		,Cabezas int
		,EstadoComederoID int
		,CamionRepartoID int
		,Observaciones varchar(255)
		,Activo bit
		,UsuarioCreacionID INT				
		)
	INSERT @RepartoDetalle (
		RepartoID
		,TipoServicioID
		,FormulaIDProgramada
		,FormulaIDServida
		,CantidadProgramada
		,CantidadServida
		,HoraReparto
		,CostoPromedio
		,Importe
		,Servido
		,Cabezas
		,EstadoComederoID
		,CamionRepartoID
		,Observaciones
		,Activo
		,UsuarioCreacionID
		)
	SELECT RepartoID = t.item.value('./RepartoID[1]', 'INT')
		,TipoServicioID = t.item.value('./TipoServicioID[1]', 'INT')
		,FormulaIDProgramada = t.item.value('./FormulaIDProgramada[1]', 'INT')
		,FormulaIDServida = t.item.value('./FormulaIDServida[1]', 'INT')
		,CantidadProgramada = t.item.value('./CantidadProgramada[1]', 'INT')
		,CantidadServida = t.item.value('./CantidadServida[1]', 'INT')
		,HoraReparto = t.item.value('./HoraReparto[1]', 'CHAR(5)')
		,CostoPromedio = t.item.value('./CostoPromedio[1]', 'DECIMAL(12,4)')
		,Importe = t.item.value('./Importe[1]', 'DECIMAL(18,2)')
		,Servido = t.item.value('./Servido[1]', 'BIT')
		,Cabezas = t.item.value('./Cabezas[1]', 'INT')
		,EstadoComederoID = t.item.value('./EstadoComederoID[1]', 'INT')
		,CamionRepartoID = t.item.value('./CamionRepartoID[1]', 'INT')
		,Observaciones = t.item.value('./Observaciones[1]', 'VARCHAR(255)')
		,Activo = t.item.value('./Activo[1]', 'BIT')
		,UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT')
	FROM @XmlRepartoDetalle.nodes('ROOT/RepartoDetalle') AS T(item)	
	INSERT RepartoDetalle (
		RepartoID
		,TipoServicioID
		,FormulaIDProgramada
		,FormulaIDServida
		,CantidadProgramada
		,CantidadServida
		,HoraReparto
		,CostoPromedio
		,Importe
		,Servido
		,Cabezas
		,EstadoComederoID
		,CamionRepartoID
		,Observaciones
		,Activo
		,UsuarioCreacionID
		,FechaCreacion
		)
	SELECT RepartoID
		RepartoID
		,TipoServicioID
		,FormulaIDProgramada
		,FormulaIDServida
		,CantidadProgramada
		,CantidadServida
		,HoraReparto
		,CostoPromedio
		,Importe
		,Servido
		,Cabezas
		,EstadoComederoID
		,CamionRepartoID
		,Observaciones
		,Activo
		,UsuarioCreacionID
		,GETDATE()
	FROM @RepartoDetalle
	SET NOCOUNT OFF;
END

GO
