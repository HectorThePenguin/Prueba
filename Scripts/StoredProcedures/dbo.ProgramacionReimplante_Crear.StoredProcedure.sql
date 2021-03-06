USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ProgramacionReimplante_Crear]
GO
/****** Object:  StoredProcedure [dbo].[ProgramacionReimplante_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      ANDRES VEJAR
-- Origen:       APInterfaces
-- Create date:  26/11/2013
-- Description:  Guarda las proyecciones seleccionadas para reimplante
-- Modificado:	 Eduardo Cota
-- Descripcion:	 Se agrega campo "Punta Chica"
--ProgramacionReimplante_Crear
--1,
--'2014-12-06',
--2902, 
--97, 
--7, 
--'<ROOT>
--  <CorralesDestino>
--    <CorralesDestinoID>1</CorralesDestinoID>
--    <PuntaChica>1</PuntaChica>
--  </CorralesDestino>
--</ROOT>'
-- =============================================
CREATE PROCEDURE [dbo].[ProgramacionReimplante_Crear]  
	@OrganizacionId INT,
	@Fecha Date,
	@LoteId INT,
	@ProductoId INT,
	@UsuarioId INT,
	@XMLCorralDestino XML
AS
BEGIN
	DECLARE @IdentityID BIGINT;
	CREATE TABLE #CorralesDestino
	(
		CorralDestinoID INT,
		PuntaChica BIT
	)
	INSERT INTO #CorralesDestino
	SELECT DISTINCT T.ITEM.value('./CorralesDestinoID[1]','INT') AS CorralDestinoID,
	T.ITEM.value('./PuntaChica[1]','INT') AS PuntaChica
	FROM @XMLCorralDestino.nodes('/ROOT/CorralesDestino') as T(ITEM)
	/* Se crea el cabecero */
	INSERT INTO ProgramacionReimplante (OrganizacionID, Fecha, LoteID, ProductoID, Activo, FechaCreacion, UsuarioCreacionID)
	VALUES (@OrganizacionId, @Fecha, @LoteId, @ProductoId, 1, GETDATE(), @UsuarioId)
	/* Se obtiene el id Insertado */
	SET @IdentityID = (SELECT @@IDENTITY) 
	/* Se crea el detalle */
	INSERT INTO ProgramacionReimplanteDetalle (FolioProgramacionID,CorralDestinoID,Activo,FechaCreacion,UsuarioCreacionID,PuntaChica)
	SELECT  @IdentityID,CorralDestinoID,1,GETDATE(),@UsuarioId,PuntaChica
	FROM #CorralesDestino
	SELECT 1
END

GO
