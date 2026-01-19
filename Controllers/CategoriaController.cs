using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeEstoque.Data;
using SistemaDeEstoque.Models;
using SistemaDeEstoque.ViewModels;
using SistemaDeEstoque.ViewModels.Categorias;

namespace SistemaDeEstoque.Controllers
{
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("v1/categorias")]
        public async Task<IActionResult> ObterCategorias()
        {
            try
            {
                var categoria = await _context.Categorias
                    .AsNoTracking()
                    .ToListAsync();
                return Ok(new ResultViewModel<List<Categoria>>(categoria));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Categoria>>($"Falha interna no servidor"));
            }
        }

        [HttpGet("v1/categorias/{id:int}")]
        public async Task<IActionResult> ObterCategoriaPorId(
            [FromRoute] int id)
        {
            try
            {
                var categoria = await _context.Categorias
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (categoria == null)
                {
                    return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));
                }
                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Categoria>($"Falha interna no servidor"));
            }
        }

        [HttpPost("v1/categorias")]
        public async Task<IActionResult> CriarCategoria(
            [FromBody] EditorCategoriaViewModel model)
        {
            try
            {
                var categoria = new Categoria
                {
                    Nome = model.Nome
                };

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                return Created($"v1/categorias/{categoria.Id}", new ResultViewModel<Categoria>(categoria));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Categoria>($"Falha interna no servidor"));

            }
        }

        [HttpPut("v1/categorias/{id:int}")]
        public async Task<IActionResult> AtualizarCategoria(
            [FromRoute] int id,
            [FromBody] EditorCategoriaViewModel model)
        {
            try
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
                if (categoria == null)
                {
                    return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));
                }
                categoria.Nome = model.Nome;

                _context.Categorias.Update(categoria);
                await _context.SaveChangesAsync();
                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Categoria>($"Falha interna no servidor"));
            }
        }
        [HttpDelete("v1/categorias/{id:int}")]
        public async Task<IActionResult> DeletarCategoria(
            [FromRoute] int id)
        {
            try
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
                if (categoria == null)
                {
                    return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));
                }
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                return Ok(new ResultViewModel<Categoria>(categoria));
            }
            catch(DbUpdateException dbEx)
            {
                return StatusCode(500, new ResultViewModel<Categoria>("Não é possível excluir uma categoria que possui produtos associados."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Categoria>($"Falha interna no servidor"));
            }
        }
    }
}
