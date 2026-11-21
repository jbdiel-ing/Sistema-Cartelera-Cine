using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Sistema_Cartelera_Cine
{
    public class FuncionRepositorio
    {
        private const string conexion =
            @"Server=(localdb)\CineLocalDB;Database=CarteleraDB;Integrated Security=True;TrustServerCertificate=True;";

        public void Insertar(Funcion x)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "INSERT INTO Funciones (Pelicula, Sala, Precio) VALUES (@Pelicula, @Sala, @Precio)";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Pelicula", x.Pelicula);
                    cmd.Parameters.AddWithValue("@Sala", x.Sala);
                    cmd.Parameters.AddWithValue("@Precio", x.Precio);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Funcion> ListarTodos()
        {
            List<Funcion> lista = new List<Funcion>();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "SELECT Id, Pelicula, Sala, Precio FROM Funciones ORDER BY Id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Funcion x = new Funcion();
                        x.Id = dr.GetInt32(0);
                        x.Pelicula = dr.GetString(1);
                        x.Sala = dr.GetString(2);
                        x.Precio = dr.GetDecimal(3);
                        lista.Add(x);
                    }
                }
            }

            return lista;
        }

        public Funcion? BuscarPorId(int id)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "SELECT Id, Pelicula, Sala, Precio FROM Funciones WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Funcion x = new Funcion();
                            x.Id = dr.GetInt32(0);
                            x.Pelicula = dr.GetString(1);
                            x.Sala = dr.GetString(2);
                            x.Precio = dr.GetDecimal(3);
                            return x;
                        }
                    }
                }
            }

            return null;
        }

        public bool Actualizar(Funcion x)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "UPDATE Funciones SET Precio = @Precio WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Precio", x.Precio);
                    cmd.Parameters.AddWithValue("@Id", x.Id);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                string sql = "DELETE FROM Funciones WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
    }
}