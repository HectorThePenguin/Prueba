--Modificaciones de estructura de BD

ALTER TABLE SIAP.dbo.EnvioProducto
	ALTER COLUMN Folio BIGINT

ALTER TABLE SuKarne.dbo.CacRecepcionProductoEnc
	ADD  TipoMovimientoID INT NULL

ALTER TABLE SuKarne.dbo.CacRecepcionProductoDet
	ADD  TipoMovimientoID INT NULL

