# Guía de Configuración - Sistema de Productos Descontinuados

## Estructura Creada

Se ha generado la siguiente estructura de archivos para manejar la base de datos:

```
proyectoBddescontinuado/
??? DATOS/
? ??? clsConexion.cs          - Manejo de conexiones a BD
?   ??? clsDaoProductos.cs      - Consultas y operaciones con productos
?   ??? clsLlenarDataGridView.cs - Helper para llenar controles
??? POJOS/
?   ??? clsProductos.cs - Clase modelo de Producto
??? FrmLista.cs      - Formulario principal (actualizado)
```

## Configuración de la Conexión

### 1. Actualizar la cadena de conexión en `clsConexion.cs`

Abre el archivo `DATOS\clsConexion.cs` y cambia esta línea:

```csharp
private string cadenaConexion = "Server=SERVIDOR;Database=BASEDATOS;User Id=USUARIO;Password=CONTRASEÑA;";
```

**Ejemplos según tu configuración:**

- **SQL Server local (Windows Authentication):**
  ```csharp
  private string cadenaConexion = "Server=.\\SQLEXPRESS;Database=TuBaseDatos;Integrated Security=true;";
  ```

- **SQL Server con credenciales:**
  ```csharp
  private string cadenaConexion = "Server=localhost;Database=TuBaseDatos;User Id=sa;Password=tuContrasena;";
  ```

- **SQL Server remoto:**
  ```csharp
  private string cadenaConexion = "Server=192.168.1.100;Database=TuBaseDatos;User Id=usuario;Password=contrasena;";
  ```

### 2. Estructura de Tabla esperada en la BD

La aplicación espera una tabla `Productos` con esta estructura:

```sql
CREATE TABLE Productos (
    idProducto INT PRIMARY KEY IDENTITY(1,1),
    nombreProducto NVARCHAR(100) NOT NULL,
    cantidad INT NOT NULL,
    precio DECIMAL(10, 2) NOT NULL,
    categoria NVARCHAR(50) NOT NULL,
    oferta BIT NOT NULL DEFAULT 0,
    descontinuado BIT NOT NULL DEFAULT 0,
    codigoBarras NVARCHAR(50) NOT NULL UNIQUE
);
```

## Funcionalidades Implementadas

### 1. **CargarProductos()** - En el Load del formulario
   - Carga todos los productos activos (no descontinuados)
   - Llena el DataGridView con columnas:
   - ID, Código, Nombre, Categoría, Cantidad, Precio, Oferta, Descontinuado

### 2. **Buscar por Código de Barras** - txt_Barras KeyUp
   - Presiona ENTER para buscar un producto
   - Si existe, selecciona la fila en el DataGridView
   - Si no existe, muestra un mensaje

### 3. **Descontinuar Producto** - btnDescontinuar Click
   - Selecciona un producto del DataGridView
   - Haz clic en "Descontinuar"
   - Pide confirmación
   - Actualiza el estado en la BD
   - Recarga la lista

## Métodos disponibles en clsDaoProductos

```csharp
// Obtiene todos los productos
List<clsProductos> ObtenerTodosProductos()

// Obtiene solo productos activos
List<clsProductos> ObtenerProductosActivos()

// Busca un producto por código de barras
clsProductos ObtenerProductoPorCodigo(string codigo)

// Marca un producto como descontinuado
bool DescontinuarProducto(int idProducto)
```

## Requisitos del Proyecto

- .NET Framework 4.7.2
- SQL Server 2012 o superior
- System.Data.SqlClient (incluido en .NET Framework)

## Prueba de la aplicación

1. **Configura la cadena de conexión** en `clsConexion.cs`
2. **Crea la tabla** en tu base de datos usando el script SQL anterior
3. **Inserta datos de prueba:**
   ```sql
   INSERT INTO Productos VALUES 
   ('Laptop', 5, 999.99, 'Electrónica', 0, 0, 'LAP001'),
   ('Mouse', 20, 25.50, 'Accesorios', 1, 0, 'MOU001'),
   ('Teclado', 15, 75.00, 'Accesorios', 0, 0, 'TEC001');
   ```
4. **Compila y ejecuta** la aplicación

## Notas Importantes

- Los campos ID, Código y Nombre son obligatorios
- El código de barras debe ser único en la BD
- Los productos descontinuados no aparecen en la lista principal
- Para ver todos los productos (incluyendo descontinuados), modifica `CargarProductos()` para usar `ObtenerTodosProductos()`
