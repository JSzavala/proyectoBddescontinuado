# Ejemplos de Uso - Sistema de Productos

## Usando el Servicio de Productos

Para simplificar el código en tus formularios, puedes usar `clsServicioProductos`:

### Ejemplo 1: Cargar productos al iniciar el formulario

```csharp
private SERVICIOS.clsServicioProductos servicio;

private void FrmLista_Load(object sender, EventArgs e)
{
    servicio = new SERVICIOS.clsServicioProductos();
 servicio.RecargarDataGridView(dgvProductos);
}
```

### Ejemplo 2: Buscar producto por código de barras

```csharp
private void txtBarras_KeyUp(object sender, KeyEventArgs e)
{
    if (e.KeyCode == Keys.Enter)
 {
   string codigo = txtBarras.Text.Trim();
  
        if (!string.IsNullOrEmpty(codigo))
        {
    if (servicio.BuscarYSeleccionarProducto(dgvProductos, codigo))
    {
     MessageBox.Show("Producto encontrado y seleccionado");
          }
            else
   {
          MessageBox.Show("Producto NO encontrado");
            }
        }
     
        txtBarras.Clear();
    }
}
```

### Ejemplo 3: Descontinuar producto

```csharp
private void btnDescontinuar_Click(object sender, EventArgs e)
{
    int idProducto = servicio.ObtenerIdProductoSeleccionado(dgvProductos);
    
    if (idProducto > 0)
    {
  if (MessageBox.Show("¿Descontinuar este producto?", "Confirmación", 
            MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
   if (servicio.DescontinuarYRecargar(dgvProductos, idProducto))
            {
        MessageBox.Show("Producto descontinuado exitosamente");
     }
    else
    {
         MessageBox.Show("Error al descontinuar");
  }
        }
    }
    else
    {
        MessageBox.Show("Seleccione un producto");
    }
}
```

### Ejemplo 4: Obtener información del producto seleccionado

```csharp
private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
{
    var producto = servicio.ObtenerProductoSeleccionado(dgvProductos);
    
    if (producto != null)
    {
      string info = $"ID: {producto.idProducto}\n" +
       $"Nombre: {producto.nombreProducto}\n" +
             $"Categoría: {producto.categoria}\n" +
          $"Cantidad: {producto.cantidad}\n" +
               $"Precio: ${producto.precio}\n" +
       $"Oferta: {(producto.oferta ? "Sí" : "No")}\n" +
      $"Descontinuado: {(producto.descontinuado ? "Sí" : "No")}";
        
        MessageBox.Show(info, "Información del Producto");
    }
}
```

## Usando clsDaoProductos directamente

Si necesitas operaciones más específicas:

### Obtener todos los productos (incluyendo descontinuados)

```csharp
private void CargarTodosProductos()
{
    var dao = new DATOS.clsDaoProductos();
    List<POJOS.clsProductos> todos = dao.ObtenerTodosProductos();
    DATOS.clsLlenarDataGridView.LlenarProductos(dgvProductos, todos);
}
```

### Obtener solo productos activos

```csharp
private void CargarProductosActivos()
{
    var dao = new DATOS.clsDaoProductos();
    List<POJOS.clsProductos> activos = dao.ObtenerProductosActivos();
    DATOS.clsLlenarDataGridView.LlenarProductos(dgvProductos, activos);
}
```

### Buscar un producto específico

```csharp
private void BuscarProducto(string codigo)
{
    var dao = new DATOS.clsDaoProductos();
    var producto = dao.ObtenerProductoPorCodigo(codigo);
    
    if (producto != null)
    {
        MessageBox.Show($"Producto: {producto.nombreProducto}\n" +
      $"Precio: ${producto.precio}");
    }
    else
    {
        MessageBox.Show("Producto no encontrado");
    }
}
```

## Personalizar el DataGridView

### Agregar más columnas personalizadas

```csharp
private void ConfigurarDataGridView()
{
    dgvProductos.Columns.Add("idProducto", "ID");
    dgvProductos.Columns.Add("codigoBarras", "Código");
  dgvProductos.Columns.Add("nombreProducto", "Nombre Producto");
    
    // Agregar columna de botón para acciones
    DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
    btnEliminar.Name = "btnEliminar";
 btnEliminar.HeaderText = "Acciones";
    btnEliminar.Text = "Eliminar";
    btnEliminar.UseColumnTextForButtonValue = true;
    dgvProductos.Columns.Add(btnEliminar);
}
```

### Manejar clic en celda

```csharp
private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.ColumnIndex == dgvProductos.Columns["btnEliminar"].Index && e.RowIndex >= 0)
    {
      int idProducto = int.Parse(dgvProductos.Rows[e.RowIndex].Cells["idProducto"].Value.ToString());
        // Realizar acción
    }
}
```

## Filtrar productos en el DataGridView

```csharp
private void FiltrarPorCategoria(string categoria)
{
    var dao = new DATOS.clsDaoProductos();
    var todos = dao.ObtenerProductosActivos();
    
 var filtrados = todos.Where(p => p.categoria == categoria).ToList();
    
    DATOS.clsLlenarDataGridView.LlenarProductos(dgvProductos, filtrados);
}
```

## Manejo de Errores

### Validar conexión antes de operaciones

```csharp
private bool ValidarConexion()
{
    var conexion = new DATOS.clsConexion();
    if (conexion.AbrirConexion())
    {
conexion.CerrarConexion();
        return true;
    }
    
    MessageBox.Show("No se pudo establecer conexión con la base de datos");
    return false;
}
```

## Script SQL para datos de prueba

```sql
-- Insertar productos de prueba
INSERT INTO Productos (nombreProducto, cantidad, precio, categoria, oferta, descontinuado, codigoBarras) 
VALUES 
('Laptop Dell XPS 13', 5, 1299.99, 'Electrónica', 0, 0, 'LAP-DEL-XPS-001'),
('Mouse Logitech MX', 20, 99.99, 'Accesorios', 1, 0, 'MOU-LOG-MX-001'),
('Teclado Mecánico', 15, 149.99, 'Accesorios', 0, 0, 'TEC-MEC-001'),
('Monitor 4K 27"', 8, 399.99, 'Electrónica', 1, 0, 'MON-4K-27-001'),
('Hub USB-C', 30, 49.99, 'Accesorios', 0, 0, 'HUB-USB-001');

-- Ver todos los productos
SELECT * FROM Productos;

-- Ver solo productos activos
SELECT * FROM Productos WHERE descontinuado = 0;

-- Ver productos en oferta
SELECT * FROM Productos WHERE oferta = 1 AND descontinuado = 0;

-- Actualizar precio
UPDATE Productos SET precio = 1199.99 WHERE codigoBarras = 'LAP-DEL-XPS-001';

-- Descontinuar un producto
UPDATE Productos SET descontinuado = 1 WHERE idProducto = 1;
```
