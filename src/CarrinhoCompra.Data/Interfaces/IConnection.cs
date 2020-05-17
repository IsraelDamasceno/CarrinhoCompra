using System;
using System.Data.SqlClient;

namespace CarrinhoCompra.Data.Interfaces
{
    public interface IConnection: IDisposable
    {
        SqlConnection Connect { get; }
        SqlConnection Open();
        void Close();
        SqlCommand CreateCommand();

    }
}
