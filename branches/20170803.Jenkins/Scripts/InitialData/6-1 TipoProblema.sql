---
-- Llenado de la tabla de tipo de Problema
---
insert into TipoProblema (Descripcion, Activo, FechaCreacion, UsuarioCreacionID) values ('Principal', 1, getdate(), 1) --  1
insert into TipoProblema (Descripcion, Activo, FechaCreacion, UsuarioCreacionID) values ('Secundario', 1, getdate(), 1) -- 2
insert into TipoProblema (Descripcion, Activo, FechaCreacion, UsuarioCreacionID) values ('No Visible', 1, getdate(), 1) -- 3
