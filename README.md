# Respuestas

## ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?

- **Composición**: La relación entre `Cliente` y `Pedido` es una composición porque si se elimina el Cliente, el Pedido también debe eliminarse.
- **Agregación**: La relación entre `Cadete` y `Pedido` es una agregación porque un Pedido puede ser reasignado a otro Cadete, y los Cadetes pueden existir independientemente de un Pedido específico.

## ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?

### Cadetería

- `AsignarPedido(Cadete cadete, Pedidos pedido)`: Asigna un pedido a un cadete.
- `ReasignarPedido(Cadete anterior, Cadete nuevo, Pedidos pedido)`: Reasigna un pedido de un cadete a otro.
- `AgregarCadete(Cadete cadete)`: Añade un nuevo cadete a la cadetería.
- `EliminarCadete(Cadete cadete)`: Elimina un cadete de la cadetería.
- `EliminarPedido(Pedidos pedido)`: Elimina un pedido y el cliente asociado.
- `GenerarInforme()`: Genera un informe sobre la actividad de la cadetería.

### Cadete

- `EntregarPedido(Pedidos pedido)`: Marca un pedido como entregado.
- `ListarPedidos()`: Muestra los pedidos asignados al cadete.
- `AgregarPedido(Pedidos pedido)`: Añade un pedido a la lista de pedidos del cadete.
- `EliminarPedido(Pedidos pedido)`: Elimina un pedido de la lista de pedidos del cadete.
- `CalcularJornal()`: Calcula el jornal a cobrar por el cadete.

## Teniendo en cuenta los principios de abstracción y ocultamiento, ¿qué atributos, propiedades y métodos deberían ser públicos y cuáles privados?

### Cadetería

- **Atributos**:
  - `nombre` (privado): Nombre de la cadetería.
  - `telefono` (privado): Teléfono de la cadetería.
  - `listadoCadetes` (privado): Lista de cadetes que trabajan en la cadetería.

- **Métodos**:
  - `AsignarPedido(Cadete cadete, Pedidos pedido)` (público): Asigna un pedido a un cadete.
  - `ReasignarPedido(Cadete cadeteOrigen, Cadete cadeteDestino, Pedidos pedido)` (público): Reasigna un pedido de un cadete a otro.
  - `AgregarCadete(Cadete cadete)` (público): Añade un nuevo cadete a la cadetería.
  - `EliminarCadete(Cadete cadete)` (público): Elimina un cadete de la cadetería.
  - `EliminarPedido(Pedidos pedido)` (público): Elimina un pedido y el cliente asociado.
  - `GenerarInforme()` (público): Genera un informe sobre la actividad de la cadetería.

### Pedidos

- **Atributos**:
  - `nro` (privado): Número del pedido.
  - `obs` (privado): Observaciones sobre el pedido.
  - `cliente` (privado): Cliente asociado al pedido.
  - `estado` (privado): Estado del pedido.

- **Métodos**:
  - `CambiarEstado(Estado nuevoEstado)` (público): Cambia el estado del pedido.
  - `mostrarPedido()` (público): Muestra la información del pedido.

### Cadete

- **Atributos**:
  - `id` (privado): Identificador único del cadete.
  - `nombre` (privado): Nombre del cadete.
  - `direccion` (privado): Dirección del cadete.
  - `telefono` (privado): Teléfono del cadete.
  - `listaPedidos` (privado): Lista de pedidos asignados al cadete.

- **Métodos**:
  - `EntregarPedido(Pedidos pedido)` (público): Marca un pedido como entregado.
  - `ListarPedidos()` (público): Muestra los pedidos asignados al cadete.
  - `AgregarPedido(Pedidos pedido)` (público): Añade un pedido a la lista de pedidos del cadete.
  - `EliminarPedido(Pedidos pedido)` (público): Elimina un pedido de la lista de pedidos del cadete.
  - `CalcularJornal()` (público): Calcula el jornal a cobrar por el cadete.

### Cliente

- **Atributos**:
  - `nombre` (privado): Nombre del cliente.
  - `direccion` (privado): Dirección del cliente.
  - `telefono` (privado): Teléfono del cliente.
  - `datosReferenciaDireccion` (privado): Datos de referencia de la dirección del cliente.

- **Métodos**:
  - `VerDireccionCliente()` (público): Muestra la dirección del cliente.
  - `VerDatosCliente()` (público): Muestra los datos del cliente.

## ¿Cómo diseñaría los constructores de cada una de las clases?

Cadeteria:
public Cadeteria(string nombre, string telefono)
{
this.Nombre = nombre;
this.Telefono = telefono;
this.ListadoCadetes = new List<Cadete>();
}
Cliente:
public Cliente(string nombre, string direccion, string telefono, string datosReferenciaDireccion)
{
this.Nombre = nombre;
this.Direccion = direccion;
this.Telefono = telefono;
this.DatosReferenciaDireccion = datosReferenciaDireccion;
}
public Cadete(int id, string nombre, string direccion, string telefono)
{
this.Id = id;
this.Nombre = nombre;
this.Direccion = direccion;
this.Telefono=telefono;
this.ListaPedidos = new List<Pedidos>();
}
Pedido:
public Pedidos(int nro, string obs, Cliente cliente, Estado estado)
{
this.nro = nro;
this.obs = obs;
this.Cliente = cliente;
this.Estado = estado;
}

## ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?
Se podría considerar la implementación de interfaces para definir los contratos de las clases Cadete, Cliente, y Pedidos, promoviendo así una mayor flexibilidad y adherencia a los principios de diseño orientado a objetos.