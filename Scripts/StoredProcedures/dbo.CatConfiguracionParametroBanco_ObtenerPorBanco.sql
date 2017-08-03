USE [SIAP]
--========================02_CatConfiguracionParametroBanco_ObtenerPorBanco========================
IF EXISTS(SELECT object_id FROM sys.objects WHERE name = 'CatConfiguracionParametroBanco_ObtenerPorBanco' AND TYPE = 'P')
BEGIN
	DROP PROCEDURE CatConfiguracionParametroBanco_ObtenerPorBanco
END
GO
--======================================================
-- Author     : Roque Solis
-- Create date: 23/09/2016
-- Description: SP para la configuracion del parametro banco
-- Origen: APInterfaces
-- SpName     : dbo.CatConfiguracionParametroBanco_ObtenerPorBanco 1,1
-- --======================================================
CREATE PROCEDURE [dbo].[CatConfiguracionParametroBanco_ObtenerPorBanco]
	@BancoID INT,
	@Activo BIT,
	@Usuario INT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS(SELECT CatParametroConfigBancoID FROM  CatParametroConfigBanco WHERE BancoID = @BancoID)
		BEGIN
			INSERT INTO CatParametroConfigBanco
			SELECT
				@BancoID,
				PC.CatParametroBancoID,
				0,
				0,
				0,
				PC.Activo,
				getdate(),
				@Usuario,
				NULL,
				NULL
			FROM CatParametroBanco PC (Nolock)
			ORDER BY PC.CatParametroBancoID
		END
			SELECT
				PC.CatParametroConfigBancoID,
				PC.BancoID,
				PC.CatParametroBancoID,
				PB.Descripcion,
				PB.Valor,
				PC.X,
				PC.Y,
				PC.Width,
				PC.Activo
			FROM CatParametroConfigBanco PC (Nolock)
				INNER JOIN CatParametroBanco PB ON PC.CatParametroBancoID = PB.CatParametroBancoID
			WHERE PC.BancoID = @BancoID
				AND PB.Activo = @Activo
			ORDER BY PC.CatParametroConfigBancoID

		SET NOCOUNT OFF;

END
GO
