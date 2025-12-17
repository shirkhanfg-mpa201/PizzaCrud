using Microsoft.Data.SqlClient;
using System.Data;

SqlConnection connection = new SqlConnection("Server=LAPTOP-87ACN0RL; Database=PizzaDB;Trusted_connection=true ; TrustServerCertificate=true");
connection.Open();

//SqlCommand command = new SqlCommand("INSERT INTO Pizzas (Name, Price) VALUES ('Hawaiian', 8.99)", connection);
// var reader =command.ExecuteReader();  Console.WriteLine(reader);
//SqlDataAdapter adapter = new SqlDataAdapter(command);
//DataSet dataSet = new DataSet();  
//adapter.Fill(dataSet);

//SqlCommand command = new SqlCommand("UPDATE Pizzas SET Price = 9.99 WHERE Name = 'Hawaiian'", connection);
//command.ExecuteNonQuery();

bool exit = false;
while (!exit)
{
    Console.WriteLine("PizzaMizza Menu");
    Console.WriteLine("1. Pizzalara bax");
    Console.WriteLine("2. Pizza elave et");
    Console.WriteLine("3. Pizza sil");
    Console.WriteLine("0. Çıxış");

    Console.Write("Seçim: ");
    string? choice = Console.ReadLine();
    Console.Clear();
    switch (choice) { 
        case "1":
            // GetPizzas();
            PrintAllPizzas(connection);
            break;
        case "2":
        //  AddPizza;
        nameInput:
            Console.Write("Pizza adı: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ad boş ola bilmez");
                goto nameInput;
            }
        priceInput:
            Console.Write("Qiymet: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price) || price<0)
            {
                Console.WriteLine("Düzgün reqem daxil edin");
                continue;
            }

            Console.Write("Ingredient sayı: ");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count) || count < 0)
            {
                Console.WriteLine("Düzgün reqem daxil edin");
                continue;
            }
            SqlCommand addCommand = new SqlCommand($"INSERT INTO Pizzas VALUES ('{name}', {price}, {count})", connection);
            var addPizza=addCommand.ExecuteNonQuery();
            if (addPizza>0)
            {
                Console.WriteLine("Pizza elavə olundu");
            }

            break;
        case "3":
            // DeletePizza();
            PrintAllPizzas(connection);
            Console.Write("Silinəcək pizzanın ID-sini daxil edin: ");
            int deleteId;
            while (!int.TryParse(Console.ReadLine(), out deleteId) || deleteId < 0)
            {
                Console.WriteLine("Düzgün reqem daxil edin");
                continue;
            }
            SqlCommand deleteCommand = new SqlCommand($"DELETE FROM Pizzas WHERE ID={deleteId}", connection);
            var deletePizza = deleteCommand.ExecuteNonQuery();
            if (deletePizza > 0)
            {
                Console.WriteLine("Pizza silindi");
            }

            break;
        case "0":
            exit = true;
            return;
        default:
            Console.WriteLine("Yanlış seçim!");
            break;
    }
}

static void PrintAllPizzas(SqlConnection connection)
{
    SqlCommand getAllCommand = new SqlCommand("SELECT * FROM Pizzas", connection);

    SqlDataAdapter getAllAdapter = new(getAllCommand);
    DataSet getAllPizzas = new DataSet();

    getAllAdapter.Fill(getAllPizzas);

    Console.WriteLine("\n Pizzalar");
    foreach (DataRow row in getAllPizzas.Tables[0].Rows)
    {
        Console.Write($"\n{row["ID"]}.  {row["Name"]}. {row["Price"]}  {row["IngredientCount"]} ");
    }
    Console.WriteLine("\n");
}