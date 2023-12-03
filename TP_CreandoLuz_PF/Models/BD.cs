using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
namespace TP_CreandoLuz_PF.Models{
    public class BD{
        private static string _connectionString = @"Server=A-PHZ2-CIDI-025;DataBase=BDD_CreandoLuzForm;Trusted_Connection=True";

        public static void MandarForm(Form Frm)
        {
        string sql = "INSERT INTO Form (Nombre, Apellido, Telefono, Mail, Cuerpo) VALUES (@pNombre, @pApellido, @pTelefono, @pMail, @pCuerpo)";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            db.Execute(sql, new {pNombre = Frm.Nombre, pApellido = Frm.Apellido, pTelefono = Frm.Telefono, pMail = Frm.Mail, pCuerpo = Frm.Cuerpo});
        }
        }
        public static List<Form> ObtenerForms()
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                string sql = "SELECT * FROM Form";
                return db.Query<Form>(sql).ToList();
            }
        }

        public static Usuario ObtenerUsuarioPorEmail(string email)
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                string sql = "SELECT * FROM Usuarios WHERE Email = @Email";
                return db.QuerySingleOrDefault<Usuario>(sql, new { Email = email });
            }
        }
        public static bool RegistrarUsuario(Usuario usuario)
        {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {db.Open();
        string sql = "INSERT INTO Usuarios (Nombre, Apellido, Telefono, Email, Password) VALUES (@Nombre, @Apellido, @Telefono, @Email, @Password)";
        int rowsAffected = db.Execute(sql, usuario);
        return rowsAffected > 0;
        }
        }

    }
}