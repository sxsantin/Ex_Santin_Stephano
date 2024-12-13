using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface IHorarioService
    {
        // Crear un producto
        Horarios CreateProduct(Horarios horarios);

        // Eliminar un producto por ID
        bool Delete(int id);

        // Obtener todos los productos
        List<Horarios> GetAll();

        // Obtener un producto por ID
        Horarios GetById(int id);

        // Actualizar un producto
        bool UpdateProduct(Horarios horarios);
    }
}
