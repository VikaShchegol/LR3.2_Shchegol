using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection...");
            MySqlConnection conn = DBUtils.GetDBConnection();

            try
            {
                Console.WriteLine("Opening Connection...");
                conn.Open();
                Console.WriteLine("Connection successful!");
                QueryEmployee(conn);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            Console.Read();
        }

        private static void QueryEmployee(MySqlConnection conn)
        {
            string[] columns = { "material_name", "materials_code", "manufacturer", "price_per_unit", "min_order_quantity", "shelf_life" };
            string[] columnNames = { "Назва", "Код", "Виробник", "Ціна за одиницю", "Мінімальна кількість замовлення", "Термін придатності" };
            ConsoleColor[] colors = { ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Blue, ConsoleColor.Red };

            // Виводимо список доступних колонок
            for (int i = 0; i < columnNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {columnNames[i]}");
            }

            Console.WriteLine("Оберіть номери полів, які потрібно вивести (через кому):");
            string input = Console.ReadLine();
            string[] selectedColumns = input.Split(',');

            Console.WriteLine("-----------------------");
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

            string sql = "SELECT ";
            bool firstColumn = true;
            foreach (string selectedColumn in selectedColumns)
            {
                if (int.TryParse(selectedColumn, out int columnIndex) && columnIndex >= 1 && columnIndex <= columns.Length)
                {
                    if (!firstColumn)
                        sql += ", ";
                    sql += columns[columnIndex - 1];
                    firstColumn = false;
                }
            }
            sql += " FROM materials";

            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("-----------------------");
                        foreach (string selectedColumn in selectedColumns)
                        {
                            if (int.TryParse(selectedColumn, out int columnIndex) && columnIndex >= 1 && columnIndex <= columns.Length)
                            {
                                Console.ForegroundColor = colors[columnIndex - 1];
                                Console.Write($"{columnNames[columnIndex - 1]}: ");
                                Console.ResetColor();
                                Console.WriteLine(reader[columns[columnIndex - 1]]);
                            }
                        }
                        Console.WriteLine("-----------------------");
                    }
                }
            }
        }
    }
}