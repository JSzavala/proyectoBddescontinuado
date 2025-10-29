-- ============================================
-- SCRIPT SQL - CREACIÓN DE BASE DE DATOS
-- Sistema de Productos Descontinuados
-- ============================================

-- Crear la base de datos (si no existe)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TuBaseDatos')
BEGIN
    CREATE DATABASE TuBaseDatos;
END
GO

-- Usar la base de datos
USE TuBaseDatos;
GO

-- ============================================
-- CREAR TABLA PRODUCTOS
-- ============================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Productos')
BEGIN
    CREATE TABLE Productos (
  idProducto INT PRIMARY KEY IDENTITY(1,1),
        nombreProducto NVARCHAR(100) NOT NULL,
        cantidad INT NOT NULL DEFAULT 0,
        precio DECIMAL(10, 2) NOT NULL,
        categoria NVARCHAR(50) NOT NULL,
   oferta BIT NOT NULL DEFAULT 0,
        descontinuado BIT NOT NULL DEFAULT 0,
        codigoBarras NVARCHAR(50) NOT NULL UNIQUE,
        fechaCreacion DATETIME DEFAULT GETDATE(),
        fechaActualizacion DATETIME DEFAULT GETDATE()
    );
    
    PRINT 'Tabla Productos creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Productos ya existe.';
END
GO

-- ============================================
-- CREAR ÍNDICES
-- ============================================

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CodigoBarras')
    CREATE INDEX IX_CodigoBarras ON Productos(codigoBarras);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Descontinuado')
  CREATE INDEX IX_Descontinuado ON Productos(descontinuado);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Categoria')
    CREATE INDEX IX_Categoria ON Productos(categoria);

PRINT 'Índices creados.';
GO

-- ============================================
-- INSERTAR DATOS DE PRUEBA
-- ============================================

-- Limpiar datos previos (opcional)
-- DELETE FROM Productos;

-- Insertar productos de ejemplo
IF (SELECT COUNT(*) FROM Productos) = 0
BEGIN
    INSERT INTO Productos (nombreProducto, cantidad, precio, categoria, oferta, descontinuado, codigoBarras)
    VALUES
    ('Laptop Dell XPS 13', 5, 1299.99, 'Electrónica', 0, 0, 'LAP-DEL-XPS-001'),
    ('Mouse Logitech MX Master 3', 20, 99.99, 'Accesorios', 1, 0, 'MOU-LOG-MX-001'),
    ('Teclado Mecánico Corsair', 15, 149.99, 'Accesorios', 0, 0, 'TEC-MEC-001'),
    ('Monitor LG 4K 27"', 8, 399.99, 'Electrónica', 1, 0, 'MON-4K-27-001'),
    ('Hub USB-C Belkin', 30, 49.99, 'Accesorios', 0, 0, 'HUB-USB-C-001'),
  ('Webcam Logitech 1080p', 12, 79.99, 'Accesorios', 0, 0, 'WEB-LOG-001'),
    ('Auriculares Sony WH-1000XM4', 10, 349.99, 'Audio', 0, 0, 'AUR-SONY-001'),
    ('Tablet Samsung Tab S7', 7, 649.99, 'Electrónica', 0, 0, 'TAB-SAM-S7-001'),
    ('SSD Kingston 1TB', 25, 89.99, 'Almacenamiento', 1, 0, 'SSD-KINGS-001'),
    ('Cable HDMI 2.1', 50, 15.99, 'Cables', 0, 0, 'CAB-HDMI-21-001');
    
    PRINT 'Datos de prueba insertados exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla ya contiene datos.';
END
GO

-- ============================================
-- VISTAS ÚTILES
-- ============================================

-- Vista: Productos Activos
IF OBJECT_ID('vwProductosActivos', 'V') IS NOT NULL
    DROP VIEW vwProductosActivos;
GO

CREATE VIEW vwProductosActivos AS
SELECT 
    idProducto,
  codigoBarras,
    nombreProducto,
    cantidad,
    precio,
    categoria,
    oferta,
    fechaCreacion
FROM Productos
WHERE descontinuado = 0
GO

-- Vista: Productos en Oferta
IF OBJECT_ID('vwProductosOferta', 'V') IS NOT NULL
    DROP VIEW vwProductosOferta;
GO

CREATE VIEW vwProductosOferta AS
SELECT 
    idProducto,
    codigoBarras,
  nombreProducto,
    precio,
    categoria,
    cantidad
FROM Productos
WHERE oferta = 1 AND descontinuado = 0
GO

-- Vista: Productos Descontinuados
IF OBJECT_ID('vwProductosDescontinuados', 'V') IS NOT NULL
    DROP VIEW vwProductosDescontinuados;
GO

CREATE VIEW vwProductosDescontinuados AS
SELECT 
    idProducto,
    codigoBarras,
    nombreProducto,
  categoria,
    fechaActualizacion
FROM Productos
WHERE descontinuado = 1
GO

PRINT 'Vistas creadas.';
GO

-- ============================================
-- PROCEDIMIENTOS ALMACENADOS
-- ============================================

-- Procedimiento: Obtener todos los productos activos
IF OBJECT_ID('sp_ObtenerProductosActivos', 'P') IS NOT NULL
    DROP PROCEDURE sp_ObtenerProductosActivos;
GO

CREATE PROCEDURE sp_ObtenerProductosActivos
AS
BEGIN
    SELECT 
        idProducto,
        nombreProducto,
        cantidad,
  precio,
        categoria,
        oferta,
      descontinuado,
        codigoBarras
    FROM Productos
    WHERE descontinuado = 0
    ORDER BY nombreProducto ASC;
END
GO

-- Procedimiento: Buscar producto por código de barras
IF OBJECT_ID('sp_BuscarProductoPorCodigo', 'P') IS NOT NULL
    DROP PROCEDURE sp_BuscarProductoPorCodigo;
GO

CREATE PROCEDURE sp_BuscarProductoPorCodigo
    @codigoBarras NVARCHAR(50)
AS
BEGIN
    SELECT 
        idProducto,
        nombreProducto,
        cantidad,
  precio,
   categoria,
        oferta,
      descontinuado,
        codigoBarras
    FROM Productos
    WHERE codigoBarras = @codigoBarras;
END
GO

-- Procedimiento: Descontinuar producto
IF OBJECT_ID('sp_DescontinuarProducto', 'P') IS NOT NULL
    DROP PROCEDURE sp_DescontinuarProducto;
GO

CREATE PROCEDURE sp_DescontinuarProducto
    @idProducto INT
AS
BEGIN
    UPDATE Productos
    SET descontinuado = 1,
        fechaActualizacion = GETDATE()
 WHERE idProducto = @idProducto;
    
    SELECT 'Producto descontinuado exitosamente' AS Mensaje;
END
GO

-- Procedimiento: Reactivar producto
IF OBJECT_ID('sp_ReactivarProducto', 'P') IS NOT NULL
    DROP PROCEDURE sp_ReactivarProducto;
GO

CREATE PROCEDURE sp_ReactivarProducto
    @idProducto INT
AS
BEGIN
    UPDATE Productos
    SET descontinuado = 0,
        fechaActualizacion = GETDATE()
    WHERE idProducto = @idProducto;
    
    SELECT 'Producto reactivado exitosamente' AS Mensaje;
END
GO

-- Procedimiento: Obtener productos por categoría
IF OBJECT_ID('sp_ObtenerProductosPorCategoria', 'P') IS NOT NULL
    DROP PROCEDURE sp_ObtenerProductosPorCategoria;
GO

CREATE PROCEDURE sp_ObtenerProductosPorCategoria
    @categoria NVARCHAR(50)
AS
BEGIN
  SELECT 
     idProducto,
        nombreProducto,
        cantidad,
        precio,
        categoria,
codigoBarras
    FROM Productos
    WHERE categoria = @categoria AND descontinuado = 0
    ORDER BY nombreProducto ASC;
END
GO

PRINT 'Procedimientos almacenados creados.';
GO

-- ============================================
-- CONSULTAS DE PRUEBA
-- ============================================

-- Ver todos los productos
-- SELECT * FROM Productos;

-- Ver solo productos activos
-- SELECT * FROM Productos WHERE descontinuado = 0;

-- Ver productos en oferta
-- SELECT * FROM Productos WHERE oferta = 1 AND descontinuado = 0;

-- Contar productos por categoría
-- SELECT categoria, COUNT(*) as Total FROM Productos WHERE descontinuado = 0 GROUP BY categoria;

-- Buscar un producto específico
-- SELECT * FROM Productos WHERE codigoBarras = 'LAP-DEL-XPS-001';

-- ============================================
-- FIN DEL SCRIPT
-- ============================================

PRINT '???????????????????????????????????????????';
PRINT 'Base de datos configurada exitosamente ?';
PRINT '???????????????????????????????????????????';
GO
