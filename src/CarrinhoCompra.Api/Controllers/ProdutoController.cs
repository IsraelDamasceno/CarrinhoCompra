using CarrinhoCompra.Domain.Domain;
using CarrinhoCompra.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarrinhoCompra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produto;
        public ProdutoController(IProduto produto )
        {
            _produto = produto;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            return _produto.List();
        }

        [HttpGet("{id}")]
        public Produto Get(int id)
        {
            Produto produto = _produto.Find(id);
            return produto;
        }

        // POST api/values
        [HttpPost]
        public ObjectResult Post([FromBody]Produto value)
        {
            value = _produto.Insert(value);
            return Ok(value);
        }

        [HttpPut("{id}")]
        public ObjectResult Put(int id, [FromBody]Produto value)
        {
            if (_produto.Edit(value))
            {
                return Ok(value);
            }
            return new NotFoundObjectResult(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ObjectResult Delete(int id)
        {
            if (_produto.Delete(id))
            {
                return Ok(new { Status = "Success", Id = id });
            }
            return new NotFoundObjectResult(new { Status = "NotFound", Id = id });
        }
    }
}