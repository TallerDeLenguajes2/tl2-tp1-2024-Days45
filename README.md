# Respuestas

## ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
- **Composición**: La relación entre `Cliente` y `Pedido` es una composición porque si se elimina el Cliente, el Pedido también debe eliminarse.
- **Agregación**: La relación entre `Cadete` y `Pedido` es una agregación porque un Pedido puede ser reasignado a otro Cadete, y los Cadetes pueden existir independientemente de un Pedido específico.

## ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
### Cadetería
- `AsignarPedido(Cadete cadete, Pedido pedido)`: Asigna un pedido a un cadete.
- `ReasignarPedido(Cadete cadeteOrigen, Cadete cadeteDestino, Pedido pedido)`: Reasigna un pedido de un cadete a otro.
- `AgregarCadete(Cadete cadete)`: Añade un nuevo cadete a la cadetería.
- `EliminarCadete(Cadete cadete)`: Elimina un cadete de la cadetería.
- `EliminarPedido(Pedido pedido)`: Elimina un pedido y el cliente asociado.
- `GenerarInforme()`: Genera un informe sobre la actividad de la cadetería.

### Cadete
- `EntregarPedido(Pedido pedido)`: Marca un pedido como entregado.
- `ListarPedidos()`: Muestra los pedidos asignados al cadete.
- `AgregarPedido(Pedido pedido)`: Añade un pedido a la lista de pedidos del cadete.
- `EliminarPedido(Pedido pedido)`: Elimina un pedido de la lista de pedidos del cadete.
- `CalcularJornal()`: Calcula el jornal a cobrar por el cadete.

## Teniendo en cuenta los principios de abstracción y ocultamiento, ¿qué atributos, propiedades y métodos deberían ser públicos y cuáles privados?
### Cadetería
- **Atributos**:
  - `pedidos` (privado): Lista de pedidos gestionados por la cadetería.
  - `cadetes` (privado): Lista de cadetes que trabajan en la cadetería.
- **Métodos**:
  - `AsignarPedido(Cadete cadete, Pedido pedido)` (público): Asigna un pedido a un cadete.
  - `ReasignarPedido(Cadete cadeteOrigen, Cadete cadeteDestino, Pedido pedido)` (público): Reasigna un pedido de un cadete a otro.
  - `AgregarCadete(Cadete cadete)` (público): Añade un nuevo cadete a la cadetería.
  - `EliminarCadete(Cadete cadete)` (público): Elimina un cadete de la cadetería.
  - `EliminarPedido(Pedido pedido)` (público): Elimina un pedido y el cliente asociado.
  - `GenerarInforme()` (público): Genera un informe sobre la actividad de la cadetería.

### Cadete
- **Atributos**:
  - `id` (privado): Identificador único del cadete.
  - `nombre` (privado): Nombre del cadete.
  - `direccion` (privado): Dirección del cadete.
  - `telefono` (privado): Teléfono del cadete.
  - `pedidos` (privado): Lista de pedidos asignados al cadete.
- **Métodos**:
  - `EntregarPedido(Pedido pedido)` (público): Marca un pedido como entregado.
  - `ListarPedidos()` (público): Muestra los pedidos asignados al cadete.
  - `AgregarPedido(Pedido pedido)` (público): Añade un pedido a la lista de pedidos del cadete.
  - `EliminarPedido(Pedido pedido)` (público): Elimina un pedido de la lista de pedidos del cadete.
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
public Cadeteria(string nombre, string telefono) {
    this.nombre = nombre;
    this.telefono = telefono;
    this.listadoCadetes = new List<Cadete>();
}
Cliente:
public Cliente(string nombre, string direccion, string telefono, string datosReferenciaDireccion) {
    this.nombre = nombre;
    this.direccion = direccion;
    this.telefono = telefono;
    this.datosReferenciaDireccion = datosReferenciaDireccion;
}
Cadete:
public Cadete(int id, string nombre, string direccion, string telefono) {
    this.id = id;
    this.nombre = nombre;
    this.direccion = direccion;
    this.telefono = telefono;
    this.pedidos = new List<Pedido>();
}
Pedido:
public Pedido(int nro, string obs, Cliente cliente, string estado) {
    this.nro = nro;
    this.obs = obs;
    this.cliente = cliente;
    this.estado = estado;
}

## ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases? no se me ocurre