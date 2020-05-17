using System;
using System.ComponentModel.DataAnnotations;

namespace CarrinhoCompra.Domain.Domain
{
    public class Produto
    {
        public Produto()
        {
        }
        public Produto(int id, string nomeProduto, double preco, string categoria, string descricao, DateTime modificacao)
        {
            Id = id;
            NomeProduto = nomeProduto;
            Preco = preco;
            Categoria = categoria;
            Descricao = descricao;
            Modificacao = modificacao;
        }

        [Key]
        public int  Id{ get;  set; }
        public string NomeProduto { get;  set; }
        public double Preco { get;  set; }
        public string Categoria { get;  set; }
        public string Descricao { get;  set; }
        public DateTime Modificacao { get;  set; }

    }
}
