USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorralRango_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[CorralRango_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CorralRango_Actualizar]
	@XmlActualizar XML
AS
BEGIN	
SET NOCOUNT ON;	
DECLARE @CorralRangoTemporal AS TABLE
		([CorralAnteriorID] INT,
		 [OrganizacionID] INT,
		 [CorralID] INT,
		 [Sexo] CHAR(1),
		 [RangoInicial] INT,
		 [RangoFinal] INT,
		 [UsuarioModificacionID] INT
		)
 INSERT @CorralRangoTemporal(
		[CorralAnteriorID],
		[OrganizacionID],
		[CorralID],
		[Sexo],
		[RangoInicial],
		[RangoFinal],
		[UsuarioModificacionID])
  SELECT 
    [CorralAnteriorID]  = t.item.value('./CorralAnteriorID[1]', 'INT'),
    [OrganizacionID]  = t.item.value('./OrganizacionID[1]', 'INT'),
    [CorralID]    = t.item.value('./CorralID[1]', 'INT'),
    [Sexo]    = t.item.value('./Sexo[1]', 'CHAR(1)'),
    [RangoInicial]  = t.item.value('./RangoInicial[1]', 'INT'),
    [RangoFinal]   = t.item.value('./RangoFinal[1]', 'INT'),
    [UsuarioModificacionID] = t.item.value('./UsuarioModificacionID[1]', 'INT')
   FROM   @XmlActualizar.nodes('ROOT/CorralRango') AS T(item)
	UPDATE CR 
	SET CR.CorralID = temp.CorralID, 
		CR.Sexo = temp.Sexo, 
		CR.RangoInicial = temp.RangoInicial, 
		CR.RangoFinal = temp.RangoFinal, 
		CR.FechaModificacion = GETDATE(), 
		CR.UsuarioModificacionID = temp.UsuarioModificacionID
	FROM @CorralRangoTemporal temp
	INNER JOIN CorralRango CR(NOLOCK) ON CR.CorralID = temp.CorralAnteriorID
		AND CR.OrganizacionID = temp.OrganizacionID
SET NOCOUNT OFF;	
END

GO
