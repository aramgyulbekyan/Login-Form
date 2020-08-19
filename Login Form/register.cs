using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Form
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();

            // placeholder
            usernameField.Text = "Enter Your name";
            usernameField.ForeColor = Color.Gray;
            surnameField.Text = "Enter Surname";
            surnameField.ForeColor = Color.Gray;
            loginField.Text = "Enter Your login";
            loginField.ForeColor = Color.Gray;
            //passField.Text = "Enter Your password";
            //passField.ForeColor = Color.Gray;
            //passconfirmField.Text = "Confirm Your password";
            //passconfirmField.ForeColor = Color.Gray;
            emailField.Text = "Enter e-mail";
            emailField.ForeColor = Color.Gray;
        }
       

        

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Black;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.White;
        }
       

        private void closeButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void usernameField_Enter(object sender, EventArgs e)
        {
           //text clean

           if (usernameField.Text == "Enter Your name")
            {
                usernameField.Text = "";
                usernameField.ForeColor = Color.Black;
            }
            

        }

        private void usernameField_Leave(object sender, EventArgs e)
        {
            if (usernameField.Text == "")
            {
                usernameField.Text = "Enter Your name";

                usernameField.ForeColor = Color.Gray;
            }
                

;        }

        private void surnameField_Enter(object sender, EventArgs e)
        {
            if (surnameField.Text == "Enter Surname")
            {
                surnameField.Text = "";
                surnameField.ForeColor = Color.Black;
            }


        }

        private void surnameField_Leave(object sender, EventArgs e)
        {
            if (surnameField.Text == "")
            {
                surnameField.Text = "Enter Surname";
                surnameField.ForeColor = Color.Gray;
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Enter Your login")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "Enter Your login";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void emailField_Enter(object sender, EventArgs e)
        {
            if (emailField.Text == "Enter e-mail")
            {
                emailField.Text = "";
                emailField.ForeColor = Color.Black;
                   
            }
        }

        private void emailField_Leave(object sender, EventArgs e)
        {

         
            if (emailField.Text == "")
            {
                emailField.Text = "Enter e-mail";
                emailField.ForeColor = Color.Black;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (usernameField.Text == "Enter Your name")
            {
                MessageBox.Show("Please Enter Your name");
                return;
            } 

            //suname ....

            

            int currentAge = DateTime.Today.Year - dob.Value.Year;
            string age = currentAge.ToString();


            if (isUserExists())
                return;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `user` (`name`, `surname`, `login`, `pass`, `email`, `age`) VALUES (@name, @surname, @login, @password, @email, @age)", db.getConnection () );

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = usernameField.Text ;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surnameField.Text;
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailField.Text;
            command.Parameters.Add("@age", MySqlDbType.VarChar).Value = age;

            db.openConnection();


            if (command.ExecuteNonQuery() == 1)

                MessageBox.Show("You are succsessfuly registered");
            else
                MessageBox.Show("Something went wrong");

                        db.closeConnection();
        }

        public Boolean isUserExists()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            // Заглушки

            MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `login` = @uL ", db.getConnection());

            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;
           

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("This login is taken");
                return true;
            }


            else

                return false;


        }

    }
}
