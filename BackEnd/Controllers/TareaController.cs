using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using ProyectoAngular.Models;

namespace ProyectoAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly DbangularContext _baseDatos;

        public TareaController(DbangularContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("lista")] 
        public async Task<IActionResult> Lista()
        {
            var listaTareas = await _baseDatos.Tareas.ToListAsync();
            return Ok(listaTareas);
        }

        [HttpPost]
        [Route("agregar")]
        public async Task<IActionResult> Agregar([FromBody] Tarea request)
        {
            await _baseDatos.Tareas.AddAsync(request);
            await _baseDatos.SaveChangesAsync();
            return Ok(request);
        }

        [HttpDelete]
        [Route("eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var tareaEliminar = await _baseDatos.Tareas.FindAsync(id);

            if(tareaEliminar == null)
            {
                return BadRequest("No existe la tarea.");
            }

            _baseDatos.Tareas.Remove(tareaEliminar);
            await _baseDatos.SaveChangesAsync();
            return Ok(tareaEliminar);
        }
    }
}
