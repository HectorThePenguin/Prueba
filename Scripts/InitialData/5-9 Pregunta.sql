/* Preguntas Datos Enfermeria */
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1,'Número de cbz CC1','',1,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'Número de cbz CC2','',2,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'Número de cbz CC3','',3,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'CC3 Mayor a 3','',4,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'% CC3 o menos','',5,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'Número de Enfermos Grado 1','',6,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'Número de Enfermos Grado 2','',7,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'Número de Enfermos Grado 3','',8,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'Número de Enfermos Grado 4','',9,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'Número de Ganado con Traumatismo','',10,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'Número de Ganado Postrados','',11,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'Número de Ganado con Problemas Digestivos','',12,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (1 ,'% de Tasa de Morbilidad','',13,1,GETDATE(),1)

/* Preguntas Evaluacion Riesgo */
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (2,'Mas del 25% de los animales presentan signos de CRB a la llegada. Valor: 2 punto','25',1,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (2,'Más del 25% de los animales con condición corporal 3 o menos. Valor: 1 punto','25',2,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (2,'Más del 10% de Merma. Valor: 1 punto','10',3,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (2,'Menos de objetivo 235 kg de peso promedio de peso de origen. Valor: 1 punto','235',4,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (2,'Ganado con más de 17 hrs de transito. Valor: 1 punto','17',5,1,GETDATE(),1)


						
/* Preguntas Supervicion Tecnica Detencion en Corral*/

INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El detector evaluado esta Certificado (CRB). Valor: 2 Puntos','2',1,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El detector se detiene fuera del corral y observa todo el ganado, en busca de signos de enfermedad. Valor: 1 Puntos','1',2,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El Detector identifica los animales enfermos de CRB antes de entrar al corral. Valor 1 Punto ','1',3,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El Detector registra correctamente todos los signos clinicos del ganado y la informacion completa del animal en cuestion en el formato de Detección de Ganado (FENVIZ-07-116). Valor 1 Punto ','1',4,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El Detector entra de manera calmada al corral, por una orilla, con el objetivo se seguir identificando animales enfermos. Valor 1 Punto ','1',5,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El Detector registra todos los animales enfermos detectados en el corral antes de empezar a sacarlos. Valor 1 Punto ','1',6,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El Detector saca todos los animales enfermos del corral con apoyo del compañero de trabajo. Valor: 2 Puntos','2',7,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El Detector saca el ganado enfermo del corral utilizando las tecnicas de Manejo de ganado sin estrés. Valor: 2 Puntos','2',8,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El Detector no dejo animales enfermos sin tratamiento en lo corral. Valor: 2 Puntos','2',9,1,GETDATE(),1)
INSERT INTO Pregunta (TipoPreguntaID, Descripcion, Valor, Orden, Activo, FechaCreacion, UsuarioCreacionID) VALUES (3 ,'El Detector revisa correctamente sus corrales asignados. Valor: 1 Puntos','1',10,1,GETDATE(),1)






