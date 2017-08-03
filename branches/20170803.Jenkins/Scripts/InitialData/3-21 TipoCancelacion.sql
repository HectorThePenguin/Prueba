/*    Tipos de Cancelacion    */
INSERT INTO TipoCancelacion (Descripcion, DiasPermitidos, Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('ENTRADA POR COMPRA', 3, 1, GETDATE(), 1)
INSERT INTO TipoCancelacion (Descripcion, DiasPermitidos, Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('ENTRADA POR TRASPASO', 3, 1, GETDATE(), 1)
INSERT INTO TipoCancelacion (Descripcion, DiasPermitidos, Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('VENTAS Y TRASPASOS', 3, 1, GETDATE(), 1)
INSERT INTO TipoCancelacion (Descripcion, DiasPermitidos, Activo, FechaCreacion, UsuarioCreacionID) 
VALUES ('PEDIDO', 3, 1, GETDATE(), 1)