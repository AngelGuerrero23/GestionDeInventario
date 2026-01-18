# Revisión de Práctica: Gestión de Inventario

Revisor: Jesus Manuel Bonilla Morillo(2023-0452)  

## Comentarios sobre el Funcionamiento

El sistema presenta una interfaz adecuada y comprensible.  
El programa permite **crear, editar y eliminar productos del inventario**, afectando correctamente la existencia según las operaciones realizadas.

## Áreas de Mejora

- Al eliminar un **producto asociado a una entrada**, aunque el producto se elimina correctamente de la entrada, **el total de la misma no se actualiza** y mantiene el valor anterior en lugar de volver a **0**.
- Este comportamiento puede generar inconsistencias visuales y lógicas al momento de editar entradas existentes.

## Confirmación de Lógica de Inventario

| Funcionalidad | Estado | Observaciones |
| Crear Producto | ✅ Funciona | El producto se añade correctamente al inventario. |
| Editar Producto | ✅ Funciona | Los cambios de precio y existencia se reflejan correctamente. |
| Eliminar Producto | ✅ Funciona | El producto se elimina sin afectar otros registros. |
| Eliminar Producto con entrada asociada | ⚠️ Advertencia | El producto se elimina de la entrada, pero el total no se actualiza y conserva el valor anterior. |

## Conclusión
La lógica de negocio funciona correctamente y cumple con los requisitos mínimos establecidos para la gestión de inventario.  
No obstante, se recomienda ajustar la actualización del total de las entradas cuando se eliminen productos asociados, para mantener la coherencia de los datos.
