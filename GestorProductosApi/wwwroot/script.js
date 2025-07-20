// URL base de la API REST que expone el backend ASP.NET Core
const API_URL = 'https://localhost:7232/api/productos';

// Variable global para guardar la fila actualmente resaltada (cuando se edita un producto)
let filaResaltada = null;

// Evento que se ejecuta cuando todo el contenido del DOM ha cargado
document.addEventListener("DOMContentLoaded", () => {
    cargarProductos(); // Obtener y mostrar productos al cargar la página
    document.getElementById('formProducto').addEventListener('submit', guardarProducto);
    document.getElementById('btnLimpiar').addEventListener('click', limpiarFormulario);
});

// Función asincrónica que obtiene los productos de la API y los muestra en la tabla
async function cargarProductos() {
    try {
        const res = await fetch(API_URL); // Realiza solicitud GET a la API
        const productos = await res.json(); // Convierte la respuesta a JSON
        const tabla = document.getElementById('tablaProductos');
        tabla.innerHTML = ''; // Limpia la tabla antes de llenarla

        productos.forEach(p => {
            // Crea una nueva fila para cada producto
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
            tabla.appendChild(fila); // Añade la fila a la tabla
        });
    } catch (error) {
        console.error("Error cargando productos:", error);
    }
}

// Función que guarda un nuevo producto o actualiza uno existente
async function guardarProducto(e) {
    e.preventDefault(); // Previene el comportamiento por defecto del formulario

    const id = document.getElementById('producto_id').value;
    const nombre = document.getElementById('nombre').value;
    const precio = parseFloat(document.getElementById('precio').value);

    // Validación básica: no permitir precios negativos
    if (precio < 0) {
        mostrarMensaje('El precio no puede ser negativo.', true);
        return;
    }

    // Genera la fecha actual en formato ISO 
    const ahora = new Date();
    const fechaLocalIso = new Date(ahora.getTime() - ahora.getTimezoneOffset() * 60000).toISOString();

    const producto = {
        nombre,
        precio,
        fechaCreacion: fechaLocalIso
    };

    try {
        if (id) {
            // Si hay ID, se actualiza (PUT)
            await fetch(`${API_URL}/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(producto)
            });
            mostrarMensaje('Producto actualizado correctamente.');
        } else {
            // Si no hay ID, se crea un nuevo producto (POST)
            await fetch(API_URL, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(producto)
            });
            mostrarMensaje('Producto guardado correctamente.');
        }

        limpiarFormulario();
        cargarProductos(); // Recarga la tabla
    } catch (error) {
        console.error("Error guardando producto:", error);
        mostrarMensaje('Error al guardar el producto.', true);
    }
}

// Función que obtiene un producto específico y lo carga en el formulario
async function editarProducto(id, boton) {
    try {
        const res = await fetch(`${API_URL}/${id}`);
        const producto = await res.json();

        document.getElementById('producto_id').value = producto.productoId;
        document.getElementById('nombre').value = producto.nombre;
        document.getElementById('precio').value = producto.precio;

        // Resalta la fila seleccionada para indicar que está en modo edición
        if (filaResaltada) filaResaltada.classList.remove('resaltado');
        const fila = boton.closest('tr');
        fila.classList.add('resaltado');
        filaResaltada = fila;
    } catch (error) {
        console.error("Error cargando producto:", error);
        mostrarMensaje('Error al cargar el producto.', true);
    }
}

// Función que elimina un producto
async function eliminarProducto(id) {
    if (!confirm('¿Seguro que deseas eliminar este producto?')) return;
    try {
        await fetch(`${API_URL}/${id}`, { method: 'DELETE' });
        mostrarMensaje('Producto eliminado correctamente.');
        cargarProductos(); // Recarga la tabla
    } catch (error) {
        console.error("Error eliminando producto:", error);
        mostrarMensaje('Error al eliminar el producto.', true);
    }
}

// Limpia el formulario y remueve resaltado de fila si la hay
function limpiarFormulario() {
    document.getElementById('producto_id').value = '';
    document.getElementById('formProducto').reset();
    if (filaResaltada) filaResaltada.classList.remove('resaltado');
    filaResaltada = null;
}

// Muestra mensajes de estado (éxito o error) en pantalla de forma temporal
function mostrarMensaje(texto, esError = false) {
    const mensaje = document.getElementById('mensaje');
    mensaje.textContent = texto;
    mensaje.className = esError ? 'error' : 'exito';
    mensaje.style.display = 'block';

    // Oculta el mensaje después de 3 segundos
    setTimeout(() => {
        mensaje.style.display = 'none';
    }, 3000);
}


