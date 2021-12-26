using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRUD.Controllers
{
    
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public DataContext _context { get; }
        public WeatherForecastController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task <IActionResult> Get()
        {   
            
            return Ok(await _context.Produtos.ToListAsync());
        }

        [HttpGet("id/{id}")]
        public async Task <ActionResult> Get(int id)
        {
            var reg = await _context.Produtos.FirstOrDefaultAsync(x => x.ProdutosID == id);
            Console.WriteLine(reg);
            return Ok(reg);
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            var singleRec = _context.Produtos.FirstOrDefault( x => x.ProdutosID == id);// object your want to delete
                _context.Produtos.Remove(singleRec);
                await _context.SaveChangesAsync();
            return Ok(await _context.Produtos.ToListAsync());
        }

        [HttpPost]
        public async Task <ActionResult> cadastrar([FromBody] Produtos produt )
        {
            _context.Add(produt);
            var resul = await _context.SaveChangesAsync();
            return Ok(resul);
        }

     

        [HttpPut]
        public async Task <ActionResult> editar([FromBody] Produtos produt )
        {
            Console.WriteLine("Hi!");
            var reg = await _context.Produtos.FirstOrDefaultAsync(x => x.ProdutosID == produt.ProdutosID);
            Console.WriteLine(produt.ProdutosID);
            Console.WriteLine(produt.Estoque);
            Console.WriteLine(produt.Descricao);
            reg.Nome = produt.Nome;
            reg.Valor = produt.Valor;
            reg.Estoque = produt.Estoque;
            reg.Descricao = produt.Descricao;
            var resul = await _context.SaveChangesAsync();
            return Ok(resul);
        }
    }
}
