INSERT INTO Calculo VALUES ('FRAME', GETDATE(), 1, NULL, NULL)
INSERT INTO Calculo VALUES ('GDP', GETDATE(), 1, NULL, NULL)
INSERT INTO Calculo VALUES ('CBH', GETDATE(), 1, NULL, NULL)
INSERT INTO Calculo VALUES ('PESOMADURO', GETDATE(), 1, NULL, NULL)
INSERT INTO Calculo VALUES ('PESOSACR', GETDATE(), 1, NULL, NULL)
INSERT INTO Calculo VALUES ('ZILMAX', GETDATE(), 1, NULL, NULL)

INSERT INTO Constantes SELECT CalculoID, 4, 1, 'M', '3.55014', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'FRAME'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'H', '2.178', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'FRAME'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'M', '0.0000208', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'FRAME'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'H', '0.000005302', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'FRAME'
INSERT INTO Constantes SELECT CalculoID, 4, 3, 'M', '0.0393', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'FRAME'
INSERT INTO Constantes SELECT CalculoID, 4, 3, 'H', '0.011', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'FRAME'
INSERT INTO Constantes SELECT CalculoID, 4, 4, 'M', '0.00867', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'FRAME'
INSERT INTO Constantes SELECT CalculoID, 4, 4, 'H', '0', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'FRAME'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'M', '1.49512', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'GDP'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'H', '1.22185', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'GDP'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'M', '0.48932', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'GDP'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'H', '0.44292', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'GDP'
INSERT INTO Constantes SELECT CalculoID, 4, 3, 'M', '0.004996', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'GDP'
INSERT INTO Constantes SELECT CalculoID, 4, 3, 'H', '0.005588', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'GDP'
INSERT INTO Constantes SELECT CalculoID, 4, 4, 'M', '0.000004919', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'GDP'
INSERT INTO Constantes SELECT CalculoID, 4, 4, 'H', '0.000006578', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'GDP'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'M', '16.56', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'H', '16.56', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'M', '0.02988', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'H', '0.02988', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 3, 'M', '2.9205', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 3, 'H', '2.9205', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 4, 'M', '0.3135', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 4, 'H', '0.3135', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 5, 'M', '2.19', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 5, 'H', '2.19', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'CBH'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'M', '509.6', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'H', '551.5', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'M', '0.4697', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'H', '0.2482', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 3, 'M', '0.96', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 3, 'H', '0.96', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 4, 'M', '46.54', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 4, 'H', '39.84', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 5, 'M', '0', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 5, 'H', '0.00119', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOMADURO'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'M', '0.94', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOSACR'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'H', '0.92', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'PESOSACR'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'M', '34', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'ZILMAX'
INSERT INTO Constantes SELECT CalculoID, 4, 1, 'H', '34', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'ZILMAX'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'M', '30', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'ZILMAX'
INSERT INTO Constantes SELECT CalculoID, 4, 2, 'H', '30', GETDATE(), 1, NULL, NULL FROM Calculo WHERE Descripcion = 'ZILMAX'

INSERT INTO ConfiguracionImplante VALUES (230, 999, 60, 120, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (200, 229, 50, 110, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (190, 199, 65, 130, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (180, 189, 60, 120, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (170, 179, 60, 100, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (150, 169, 50, 100, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (125, 149, 65, 0, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (120, 124, 60, 0, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (115, 119, 55, 0, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (110, 114, 45, 0, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (90, 99, 40, 0, GETDATE(), 1, NULL, NULL)
INSERT INTO ConfiguracionImplante VALUES (0, 89, 0, 0, GETDATE(), 1, NULL, NULL)