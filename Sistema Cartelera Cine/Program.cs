using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Sistema_Cartelera_Cine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FuncionRepositorio repo = new FuncionRepositorio();
            int opcion = 0;

            do
            {
                Console.WriteLine("\n===== SISTEMA DE CARTELERA DE CINE =====");
                Console.WriteLine("1. Registrar función");
                Console.WriteLine("2. Listar funciones");
                Console.WriteLine("3. Buscar función por Id");
                Console.WriteLine("4. Actualizar precio");
                Console.WriteLine("5. Eliminar función");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                int.TryParse(Console.ReadLine(), out opcion);

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            Registrar(repo);
                            break;

                        case 2:
                            Listar(repo);
                            break;

                        case 3:
                            Buscar(repo);
                            break;

                        case 4:
                            Actualizar(repo);
                            break;

                        case 5:
                            Eliminar(repo);
                            break;

                        case 6:
                            Console.WriteLine("Saliendo del sistema...");
                            break;

                        default:
                            Console.WriteLine("Opción inválida.");
                            break;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error de base de datos: " + ex.Message);
                }

            } while (opcion != 6);
        }

        public static void Registrar(FuncionRepositorio repo)
        {
            Console.Write("Nombre de la película: ");
            string pelicula = Console.ReadLine() ?? "";

            Console.Write("Nombre de la sala: ");
            string sala = Console.ReadLine() ?? "";

            Console.Write("Precio: ");

            if (string.IsNullOrWhiteSpace(pelicula) ||
                string.IsNullOrWhiteSpace(sala) ||
                !decimal.TryParse(Console.ReadLine(), out decimal precio) ||
                precio <= 0)
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }

            Funcion x = new Funcion();
            x.Pelicula = pelicula;
            x.Sala = sala;
            x.Precio = precio;

            repo.Insertar(x);

            Console.WriteLine("Función registrada.");
        }

        public static void Listar(FuncionRepositorio repo)
        {
            List<Funcion> lista = repo.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("No hay funciones registradas.");
                return;
            }

            decimal total = 0;

            foreach (Funcion x in lista)
            {
                Console.WriteLine(x.ToString());
                total += x.Precio;
            }

            Console.WriteLine("TOTAL DE PRECIOS: Q " + total.ToString("F2"));
        }

        public static void Buscar(FuncionRepositorio repo)
        {
            Console.Write("Ingrese el Id: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            Funcion? x = repo.BuscarPorId(id);

            if (x == null)
            {
                Console.WriteLine("No existe ese Id.");
            }
            else
            {
                Console.WriteLine(x.ToString());
            }
        }

        public static void Actualizar(FuncionRepositorio repo)
        {
            Console.Write("Ingrese el Id: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            Funcion? x = repo.BuscarPorId(id);

            if (x == null)
            {
                Console.WriteLine("No existe ese Id.");
                return;
            }

            Console.Write("Ingrese el nuevo precio: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal precio) ||
                precio <= 0)
            {
                Console.WriteLine("Precio inválido.");
                return;
            }

            x.Precio = precio;

            if (repo.Actualizar(x))
            {
                Console.WriteLine("Función actualizada.");
            }
        }

        public static void Eliminar(FuncionRepositorio repo)
        {
            Console.Write("Ingrese el Id: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            if (repo.Eliminar(id))
            {
                Console.WriteLine("Función eliminada.");
            }
            else
            {
                Console.WriteLine("No existe ese Id.");
            }
        }
    }
}