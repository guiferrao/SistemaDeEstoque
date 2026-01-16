using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeEstoque.Data;
using SistemaDeEstoque.Models;
using SistemaDeEstoque.ViewModels;
using SistemaDeEstoque.ViewModels.Produtos;

namespace SistemaDeEstoque.Controllers
{
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("v1/produtos")]
        public async Task<IActionResult> ObterProdutos()
        {
            try
            {
                var produtos = await _context.Produtos
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .ToListAsync();
                return Ok(new ResultViewModel<List<Produto>>(produtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Produto>>($"Falha interna no servidor"));
            }
        }

        [HttpGet("v1/produtos/{id:int}")]
        public async Task<IActionResult> ObterProdutoPorId(
            [FromRoute] int id)
        {
            try
            {
                var produto = await _context.Produtos
                    .AsNoTracking()
                    .Include(x => x.Categoria)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (produto == null)
                {
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));
                }
                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Produto>($"Falha interna no servidor"));
            }
        }

        [HttpPost("v1/produtos")]
        public async Task<IActionResult> CriarProduto(
            [FromBody] EditorProdutoViewModel model)
        {
            try
            {
                var produto = new Produto
                {
                    Nome = model.Nome,
                    Valor = model.Valor,
                    Quantidade = model.Quantidade,
                    CategoriaId = model.CategoriaId
                };

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                return Created($"v1/produtos/{produto.Id}", new ResultViewModel<Produto>(produto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Produto>($"Falha interna no servidor"));

            }
        }

        [HttpPut("v1/produtos/{id:int}")]
        public async Task<IActionResult> AtualizarProduto(
            [FromRoute] int id,
            [FromBody] EditorProdutoViewModel model)
        {
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
                if (produto == null)
                {
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));
                }
                produto.Nome = model.Nome;
                produto.Valor = model.Valor;
                produto.Quantidade = model.Quantidade;
                produto.CategoriaId = model.CategoriaId;

                _context.Produtos.Update(produto);
                _context.SaveChanges();
                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Produto>($"Falha interna no servidor"));
            }
        }
        [HttpDelete("v1/produtos/{id:int}")]
        public async Task<IActionResult> DeletarProduto(
            [FromRoute] int id)
        {
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
                if (produto == null)
                {
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));
                }
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Produto>($"Falha interna no servidor"));
            }
        }
    }
}
