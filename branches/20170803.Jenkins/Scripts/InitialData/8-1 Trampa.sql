--configuramos la trampa de necropsia
INSERT INTO Trampa (Descripcion, OrganizacionId, TipoTrampa, HostName, Activo, FechaCreacion, usuarioCreacionId)
VALUES ('TrampaNecropsia', 4, 'E', 'TrampaNecropsia', 1, GETDATE(), 1)
INSERT INTO trampa (Descripcion, OrganizacionId, TipoTrampa, HostName, Activo, FechaCreacion, usuarioCreacionId)
VALUES ('Trampa Prueba Manejo', 4, 'M', 'TrampaManejo', 1, GETDATE(), 1)
INSERT INTO trampa (Descripcion, OrganizacionId, TipoTrampa, HostName, Activo, FechaCreacion, usuarioCreacionId)
VALUES ('Trampa Prueba Enfermeria', 4, 'E', 'TrampaEnfermeria', 1, GETDATE(), 1)