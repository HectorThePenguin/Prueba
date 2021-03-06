USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_Crear]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Poliza_Crear]
GO
/****** Object:  StoredProcedure [dbo].[Poliza_Crear]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 07/12/2013
-- Description:  Guarda el XML de la Poliza
-- =============================================
CREATE PROCEDURE [dbo].[Poliza_Crear]
@TipoPolizaID INT
, @XmlPoliza XML
, @OrganizacionID INT
, @Estatus BIT
, @UsuarioCreacionID INT
, @Conciliada BIT
, @Procesada  BIT = 0
, @ArchivoEnviadoServidor BIT = 0
AS
BEGIN

	INSERT INTO Poliza
	(
		TipoPolizaID
		, XmlPoliza
		, OrganizacionID
		, Estatus
		, UsuarioCreacionID
		, FechaCreacion 
		, Conciliada
		, ArchivoEnviadoServidor
		, Procesada
	)
	VALUES
	(
		@TipoPolizaID
		, @XmlPoliza
		, @OrganizacionID
		, @Estatus
		, @UsuarioCreacionID
		, GETDATE()
		, @Conciliada
		, @ArchivoEnviadoServidor
		, @Procesada
	)

	SELECT SCOPE_IDENTITY()
END

GO
