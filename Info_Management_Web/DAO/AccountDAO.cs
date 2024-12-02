using Info_Management_Web.Models;
using System.Data.SqlClient;

namespace Info_Management_Web.DAO
{
    public class AccountDAO
    {
        private readonly string _connectionString;

        public AccountDAO()
        {
            _connectionString = "Server=.;Database=InfoMngDB;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public void AddAccount(Account Account)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Accounts (Username, Password, Fullname, Role) VALUES (@Username, @Password, @Fullname, @Role)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Username", Account.Username);
                command.Parameters.AddWithValue("@Password", Account.Password);
                command.Parameters.AddWithValue("@Fullname", Account.Fullname);
                command.Parameters.AddWithValue("@Role", Account.Role);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> Accounts = new List<Account>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Accounts";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Accounts.Add(new Account
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["Fullname"].ToString(),
                        Role = Convert.ToInt32(reader["Role"])
                    });
                }
            }

            return Accounts;
        }

        public Account GetAccountById(int id)
        {
            Account account = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Accounts WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    account = new Account
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["Fullname"].ToString(),
                        Role = Convert.ToInt32(reader["Role"])
                    };
                }
            }

            return account;
        }

        public Account GetAccountByUsername(string username)
        {
            Account account = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Accounts WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    account = new Account
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["Fullname"].ToString(),
                        Role = Convert.ToInt32(reader["Role"])
                    };
                }
            }

            return account;
        }

        public Account login(string username, string password)
        {
            Account account = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Accounts WHERE Username = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    account = new Account
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Fullname = reader["Fullname"].ToString(),
                        Role = Convert.ToInt32(reader["Role"])
                    };
                }
            }

            return account;
        }

        public void UpdateAccount(Account Account)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Accounts SET Username = @Username, Password = @Password, Fullname = @Fullname, Role = @Role WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", Account.Id);
                command.Parameters.AddWithValue("@Username", Account.Username);
                command.Parameters.AddWithValue("@Password", Account.Password);
                command.Parameters.AddWithValue("@Fullname", Account.Fullname);
                command.Parameters.AddWithValue("@Role", Account.Role);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAccount(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Accounts WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
