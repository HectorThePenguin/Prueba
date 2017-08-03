IF EXISTS (
                SELECT *
                FROM sys.objects
                WHERE [object_id] = Object_id(N'[dbo].[Capa]')
                )
        DROP TABLE [dbo].[Capa]
GO

CREATE TABLE Capa (
        CapaId INT 
        ,Descripcion VARCHAR(50)
        ,Proyecto varchar(50)
        ,Directorio VARCHAR(50)
        ,FormatoArchivo VARCHAR(50)
        ,Orden int
        ,Activo BIT
        ,constraint pk_Capa_CapaId primary key(CapaId)
        )

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (1, 'Capa SIAP', 'SIE', 'SIAP', '{0}Info.cs', 11, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (2, 'Capa PL', 'SIE', 'PL', '{0}PL.cs', 12, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (3, 'Capa BL', 'SIE', 'BL', '{0}BL.cs', 13, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (4, 'Capa DAL', 'SIE', 'DAL', '{0}DAL.cs', 14, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (5, 'Capa Auxiliar', 'SIE', 'Auxiliar', 'Aux{0}DAL.cs', 15, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (6, 'Capa Maper', 'SIE', 'Maper', 'Map{0}DAL.cs', 16, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (7, 'Procedimiento Crear', 'SIE', 'Procedimientos', '{0}_Crear.sql', 17, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (8, 'Procedimiento Actualizar', 'SIE', 'Procedimientos', '{0}_Actualizar.sql', 18, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (9, 'Procedimiento ObtenerTodos', 'SIE', 'Procedimientos', '{0}_ObtenerTodos.sql', 19, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (10, 'Procedimiento ObtenerPorId', 'SIE', 'Procedimientos', '{0}_ObtenerPorID.sql', 20, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (11, 'Procedimiento ObtenerPorDescripcion', 'SIE', 'Procedimientos', '{0}_ObtenerPorDescripcion.sql', 21, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (12, 'Procedimiento ObtenerPorPagina', 'SIE', 'Procedimientos', '{0}_ObtenerPorPagina.sql', 6, 0)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (13, 'Formulario Xaml', 'SIE', 'SIE.WinForm', '{0}.xaml', 7, 1)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (14, 'Formulario Xaml.cs', 'SIE', 'SIE.WinForm', '{0}.xaml.cs', 8, 1)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (15, 'Formulario Recursos', 'SIE', 'SIE.WinForm.Recursos', '{0}.txt', 5, 1)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (16, 'Formulario Xaml Edición', 'SIE', 'SIE.WinForm', '{0}Edicion.xaml', 9, 1)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (17, 'Formulario Xaml.cs Edición', 'SIE', 'SIE.WinForm', '{0}Edicion.xaml.cs', 10, 1)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (18, 'Capa BLToolkit SIAP', 'SIE', 'Info', '{0}Info.cs', 1, 1)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (19, 'Capa BLToolkit BL', 'SIE', 'BL', '{0}BL.cs', 2, 1)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (20, 'Capa BLToolkit DAL', 'SIE', 'ORM.DAL', '{0}DAL.cs', 4, 1)
GO

INSERT INTO dbo.Capa (CapaId, Descripcion, Proyecto, Directorio, FormatoArchivo, Orden, Activo)
VALUES (21, 'Capa BLToolkit Accesor', 'SIE', 'ORM.Accessor', '{0}Accessor.cs', 3, 1)
GO


        
Select * from Capa      
Where Activo = 1
Order by Orden
