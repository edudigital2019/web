using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tipo_Datos.Data;
using Tipo_Datos.Models.Entidades;

namespace Tipo_Datos.Controllers
{
    public class ClientesController : Controller
    {
        private readonly DatosDbContext _dbContext;
        public ClientesController(DatosDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Clientes.ToListAsync());
        }

        public IActionResult Nuevo() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> 
            Nuevo([Bind("Nombres,Email,Telefono,Direccion,Cedula_RUC," +
            "Create_At,Update_At,isDelete")] ClientesModel cliente)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(cliente);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return  View(cliente);
        }

        
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();
            return View(cliente); 
        }

        
        [HttpPost]
        public async Task<IActionResult> Editar(
            int id,
            [Bind("Id,Nombres,Email,Telefono,Direccion,Cedula_RUC," +
            "Create_At,Update_At,isDelete")]
            ClientesModel input)
        {
            if (id != input.Id) return NotFound();
            if (!ModelState.IsValid) return View(input);

            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            cliente.Nombres = input.Nombres;
            cliente.Email = input.Email;
            cliente.Telefono = input.Telefono;
            cliente.Direccion = input.Direccion;
            cliente.Cedula_RUC = input.Cedula_RUC;
            cliente.Update_At = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return NotFound();
            return View(cliente); 
        }

        
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            _dbContext.Clientes.Remove(cliente);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}


