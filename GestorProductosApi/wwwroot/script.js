const API_URL = 'https://localhost:7232/api/productos'; 

let filaResaltada = null;

document.addEventListener("DOMContentLoaded", () => {
    cargarProductos();

    document.getElementById('formProducto').addEventListener('submit', guardarProducto);
    document.getElementById('btnLimpiar').addEventListener('click', limpiarFormulario);
});

// Cargar y mostrar productos
async function cargarProductos() {
    try {
        const res = await fetch(API_URL);
        const productos = await res.json();
        const tabla = document.getElementById('tablaProductos');
        tabla.innerHTML = '';
        productos.forEach(p => {
            const fila = document.createElement('tr');
            fila.innerHTML = `
                <td>${p.productoId}</td>
                <td>${p.nombre}</td>
                <td>${p.precio}</td>
                <td>${new Date(p.fechaCreacion).toLocaleString()}</td>
                <td>
                    <button onclick="editarProducto(${p.productoId}, this)">Editar</button>
                    <button onclick="eliminarProducto(${p.productoId})">Eliminar</button>
                </td>
            `;
            tabla.appendChild(fila);
        });
    } catch (error) {
        console.error("Error cargando productos:", error);
    }
}

// Guardar o editar producto
async function guardarProducto(e) {
    e.preventDefault();

    const id = document.getElementById('producto_id').value;
    const nombre = document.getElementById('nombre').value;
    const precio = parseFloat(document.getElementById('precio').value);

    if (precio < 0) {
        mostrarMensaje('El precio no puede ser negativo.', true);
        return;
    }

    const ahora = new Date();
    const fechaLocalIso = new Date(ahora.getTime() - ahora.getTimezoneOffset() * 60000).toISOString();

    const producto = {
        nombre,
        precio,
        fechaCreacion: fechaLocalIso
    };

    try {
        if (id) {
            await fetch(`${API_URL}/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(producto)
            });
            mostrarMensaje('Producto actualizado correctamente.');
        } else {
            await fetch(API_URL, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(producto)
            });
            mostrarMensaje('Producto guardado correctamente.');
        }

        limpiarFormulario();
        cargarProductos();
    } catch (error) {
        console.error("Error guardando producto:", error);
        mostrarMensaje('Error al guardar el producto.', true);
    }
}

// Editar producto
async function editarProducto(id, boton) {
    try {
        const res = await fetch(`${API_URL}/${id}`);
        const producto = await res.json();
        document.getElementById('producto_id').value = producto.productoId;
        document.getElementById('nombre').value = producto.nombre;
        document.getElementById('precio').value = producto.precio;

        // Resaltar fila
        if (filaResaltada) filaResaltada.classList.remove('resaltado');
        const fila = boton.closest('tr');
        fila.classList.add('resaltado');
        filaResaltada = fila;
    } catch (error) {
        console.error("Error cargando producto:", error);
        mostrarMensaje('Error al cargar el producto.', true);
    }
}

// Eliminar producto
async function eliminarProducto(id) {
    if (!confirm('¿Seguro que deseas eliminar este producto?')) return;
    try {
        await fetch(`${API_URL}/${id}`, { method: 'DELETE' });
        mostrarMensaje('Producto eliminado correctamente.');
        cargarProductos();
    } catch (error) {
        console.error("Error eliminando producto:", error);
        mostrarMensaje('Error al eliminar el producto.', true);
    }
}

// Limpiar formulario
function limpiarFormulario() {
    document.getElementById('producto_id').value = '';
    document.getElementById('formProducto').reset();
    if (filaResaltada) filaResaltada.classList.remove('resaltado');
    filaResaltada = null;
}

// Mostrar mensajes
function mostrarMensaje(texto, esError = false) {
    const mensaje = document.getElementById('mensaje');
    mensaje.textContent = texto;
    mensaje.className = esError ? 'error' : 'exito';
    mensaje.classList.add('exito');
    mensaje.style.display = 'block';

    setTimeout(() => {
        mensaje.style.display = 'none';
    }, 3000);
}
