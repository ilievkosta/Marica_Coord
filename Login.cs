using System;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
namespace Marica_Coord
{
  
   
public partial class Login : Form
    {
        public static string user = "";
        public Login()
        {
            InitializeComponent();
            this.lblPasswordMessage.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string pathToFile = AppDomain.CurrentDomain.BaseDirectory;
            string newPath = Path.GetFullPath(Path.Combine(pathToFile, @"..\..\"));

            string cs = @"Data Source=" + newPath + "MaricaDB.db" + ";Pooling=true;FailIfMissing=false;Version=3";

            SQLiteConnection connection = new SQLiteConnection(cs);
            connection.Open();
            string stm = "SELECT [username], [password], [salt] FROM [loginData] where [username]=@username";
            SQLiteCommand command = new SQLiteCommand(stm, connection);
            command.Parameters.Add(new SQLiteParameter("@username", textBoxUsername.Text));
            SQLiteDataReader reader = command.ExecuteReader();
 
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    byte[] saltedPassword = logReg.GetSaltedPasswordHash(textBoxUsername.Text, reader.GetString(2));
                    string hashPassword = Convert.ToBase64String(saltedPassword);
                    if (reader.GetString(1) == hashPassword)
                    {
                        this.Hide();
                        

                        user = reader.GetString(0);
                        MaricaForm form1 = new MaricaForm(user);
                        form1.ShowDialog();
                        
                       
                    }
                   
                    else
                    
                    {
                        this.lblPasswordMessage.Visible=true;
                    }
                   
                
                }
            }
            reader.Close();

        }


        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register reg = new Register();
           
            reg.ShowDialog();
            
        }
    }
}
