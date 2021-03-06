USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_Guardar]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ServicioAlimento_Guardar]
GO
/****** Object:  StoredProcedure [dbo].[ServicioAlimento_Guardar]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Marco Zamora
-- Create date: 21/20/2013
-- Description: Guardar ServicioAlimento 
-- Empresa:Apinterfaces
-- =============================================
CREATE PROCEDURE [dbo].[ServicioAlimento_Guardar]
 @XmlServicioAlimento XML
AS
BEGIN
		DECLARE @ServicioAlimentoTemporal AS TABLE
		(
		[ServicioID] INT,
		[OrganizacionID] INT,
		[CorralID] INT,
		[Fecha] SMALLDATETIME,
		[FormulaID] INT,
		[KilosProgramados] INT,
		[Comentarios] VARCHAR(200),
		[Activo] BIT,
		[FechaCreacion] SMALLDATETIME,
		[UsuarioCreacionID] INT,
		[FechaModificacion] SMALLDATETIME,
		[UsuarioModificacionID] INT)
 INSERT @ServicioAlimentoTemporal(
		[ServicioID],
		[OrganizacionID],
		[CorralID],
		[FormulaID],
		[KilosProgramados],
		[Comentarios],
		[Activo],
		[UsuarioCreacionID],
		[UsuarioModificacionID])
  SELECT 
    [ServicioID]  = t.item.value('./ServicioID[1]', 'INT'),
    [OrganizacionID]  = t.item.value('./OrganizacionID[1]', 'INT'),
    [CorralID]    = t.item.value('./CorralID[1]', 'INT'),
    [FormulaID]    = t.item.value('./FormulaID[1]', 'INT'),
    [KilosProgramados]  = t.item.value('./KilosProgramados[1]', 'INT'),
    [Comentarios]   = t.item.value('./Comentarios[1]', 'VARCHAR(200)'),
    [Activo]    = t.item.value('./Activo[1]', 'BIT'),
    [UsuarioCreacionID]  = t.item.value('./UsuarioCreacionID[1]', 'INT'),
    [UsuarioModificacionID] = t.item.value('./UsuarioModificacionID[1]', 'INT')
   FROM   @XmlServicioAlimento.nodes('ROOT/ServicioAlimento') AS T(item)
 UPDATE s SET s.CorralID=temp.CorralID,
			  s.fecha=GETDATE(),
			  s.FormulaID=temp.FormulaID,
			  s.KilosProgramados=temp.KilosProgramados, 
			  s.Comentarios=temp.Comentarios,
			  s.FechaModificacion=GETDATE(),
			  s.UsuarioModificacionID=temp.UsuarioModificacionID
 FROM @ServicioAlimentoTemporal temp
 INNER JOIN ServicioAlimento s(NOLOCK) ON s.ServicioID = temp.ServicioID
 INSERT INTO ServicioAlimento 
 ( 
  OrganizacionID,
  CorralID,
  Fecha,
  FormulaID,
  KilosProgramados,
  Comentarios,
  Activo,
  FechaCreacion,
  FechaModificacion,
  UsuarioCreacionID
 )
 SELECT
      [OrganizacionID],
      [CorralID]
      ,GETDATE()
      ,[FormulaID]
      ,[KilosProgramados]
	  ,[Comentarios]
      ,[Activo]
      ,GETDATE()
	  ,GETDATE()
      ,[UsuarioCreacionID]
 FROM  @ServicioAlimentoTemporal t
 WHERE t.ServicioID = 0; /*NOT EXISTS (SELECT '' FROM ServicioAlimento s WHERE s.ServicioID = t.ServicioID)*/
END

GO
