IF EXISTS (SELECT * 
			FROM dbo.sysobjects 
			WHERE id = object_id('[Developer]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
				DROP TABLE [Developer]
GO
Create table Developer
(
	[DeveloperID] int  IDENTITY,
	[Login] VARCHAR(50),
	[Nombre] VARCHAR(100),
	[Estatus] BIT,
	CONSTRAINT PK_Developer PRIMARY KEY (DeveloperID)
)

INSERT Developer([Login], [Nombre],[Estatus]) VALUES ('joseg.quinterol'	 , 'José Gilberto Quintero López'	, 1)
INSERT Developer([Login], [Nombre],[Estatus]) VALUES ('luis.velazquez'	 , 'Jorge Luis Velazquez Araujo'	, 1)
INSERT Developer([Login], [Nombre],[Estatus]) VALUES ('gilberto.carranza', 'Gilberto Julián Carranza Castro', 1)
INSERT Developer([Login], [Nombre],[Estatus]) VALUES ('raul.esquer'		 , 'Raúl Antonio Esquer Verduzco'	, 1)
