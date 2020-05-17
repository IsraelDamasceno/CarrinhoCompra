using CarrinhoCompra.Data.Interfaces;
using CarrinhoCompra.Domain.Domain;
using CarrinhoCompra.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CarrinhoCompra.Data.Repositories
{
    public class ProdutoRepository : IProduto
    {
        private IConnection _connection;
        public ProdutoRepository(IConnection connection)
        {
            _connection = connection;
        }
        public bool Delete(Produto value)
        {
            return Delete(value.Id);
        }

        public bool Delete(object id)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "DELETE FROM PRODUTOS WHERE Id=@Id";
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = (int)id;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }

 
        public bool Edit(Produto value)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "UPDATE PRODUTOS SET PRODUTO=@PRODUTO, PRECO=@PRECO, CATEGORIA=@CATEGORIA, DESCRICAO=@DESCRICAO, CREATEDATA=@CREATEDATA WHERE Id=@Id";
                _command.Parameters.Add("@PRODUTO", SqlDbType.VarChar, 50).Value = value.NomeProduto;
                _command.Parameters.Add("@PRECO", SqlDbType.Float).Value = value.Preco;
                _command.Parameters.Add("@CATEGORIA", SqlDbType.VarChar).Value = value.Categoria;
                _command.Parameters.Add("@DESCRICAO", SqlDbType.VarChar).Value = value.Descricao;
                _command.Parameters.Add("@CREATEDATA", SqlDbType.DateTime).Value = value.Modificacao;
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = value.Id;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }

        public Produto Find(object id)
        {
            Produto produto = null;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Id, PRODUTO, PRECO, CATEGORIA, DESCRICAO, CREATEDATA  FROM PRODUTOS WHERE Id=@Id";
                _command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        produto = new Produto(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5));
                    }
                }
            }
            return produto;
        }

        public Produto Insert(Produto value)
        {
            using(SqlCommand _command = _connection.CreateCommand())
            {
                string sql = "INSERT INTO PRODUTOS(PRODUTO, PRECO, CATEGORIA, DESCRICAO, CREATEDATA) VALUES(@PRODUTO, @PRECO, @CATEGORIA, @DESCRICAO, @CREATEDATA);SELECT @@IDENTITY;";
                _command.CommandText = sql;
                _command.Parameters.Add("@PRODUTO", SqlDbType.VarChar).Value = value.NomeProduto;
                _command.Parameters.Add("@PRECO", SqlDbType.Float).Value = value.Preco;
                _command.Parameters.Add("@CATEGORIA", SqlDbType.VarChar).Value = value.Categoria;
                _command.Parameters.Add("@DESCRICAO", SqlDbType.VarChar).Value = value.Descricao;
                _command.Parameters.Add("@CREATEDATA", SqlDbType.DateTime).Value = value.Modificacao;
                int id = 0;
                if(int.TryParse(_command.ExecuteScalar().ToString(),out id))
                {
                    value.Id = id;
                }
            }
            return value;
        }

        public IEnumerable<Produto> List()
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT TOP 100 * FROM PRODUTOS ORDER BY CATEGORIA";
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Produto(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5));
                        }
                    }
                }
            }
        }

        public bool Pacth(Produto value)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
