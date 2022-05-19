using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP5WebTransactionnel.Models;

namespace TP5WebTransactionnel.DataAccessLayer.Factories
{
    public class MenuChoiceFactory
    {
        public MenuChoice CreateEmpty()
        {
            return new MenuChoice();
        }
        private MenuChoice CreateFromReader(MySqlDataReader sqlRow)
        {
            int id = (int)sqlRow["Id"];
            string description = sqlRow["Description"].ToString();
            string pathFile = sqlRow["ImagePath"].ToString();

            return new MenuChoice(id, description, pathFile);
        }
        public List<MenuChoice> GetAllMenuChoice()
        {
            List<MenuChoice> ListeMenuChoice = new List<MenuChoice>();
            MySqlConnection conn = null;
            MySqlDataReader dataReader = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "SELECT * FROM h22_travail4_2031887.tp5_menuchoices order by Description;";

                dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ListeMenuChoice.Add(CreateFromReader(dataReader));
                }

            }
            finally
            {
                dataReader?.Close();
                conn?.Close();
            }

            return ListeMenuChoice;
        }

        public MenuChoice GetById(int Id)
        {
            MenuChoice MenuChoisi = null;
            MySqlConnection conn = null;
            MySqlDataReader dataReader = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "SELECT * FROM h22_travail4_2031887.tp5_menuchoices WHERE Id = @Id";
                sqlCmd.Parameters.AddWithValue("@Id", Id);

                dataReader = sqlCmd.ExecuteReader();
                if (dataReader.Read())
                {
                    MenuChoisi = CreateFromReader(dataReader);
                }
            }
            finally
            {
                dataReader?.Close();
                conn?.Close();
            }
            return MenuChoisi;
        }
        public MenuChoice GetByName(string desc)
        {
            MenuChoice MenuChoisi = null;
            MySqlConnection conn = null;
            MySqlDataReader dataReader = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "SELECT * FROM h22_travail4_2031887.tp5_menuchoices WHERE Description = @desc";
                sqlCmd.Parameters.AddWithValue("@desc", desc);

                dataReader = sqlCmd.ExecuteReader();
                if (dataReader.Read())
                {
                    MenuChoisi = CreateFromReader(dataReader);
                }
            }
            finally
            {
                dataReader?.Close();
                conn?.Close();
            }
            return MenuChoisi;
        }


        public void AjouterMenuChoice(MenuChoice menuChoisi)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open(); 

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "INSERT INTO `h22_travail4_2031887`.`tp5_menuchoices` (`Description`, `ImagePath`) VALUES (@Descrip, @imgPath);";
                sqlCmd.Parameters.AddWithValue("@Descrip", menuChoisi.Description);
                sqlCmd.Parameters.AddWithValue("@imgPath", menuChoisi.ImagePath);
                sqlCmd.ExecuteNonQuery();   

            }
            finally
            {
                conn?.Close();
            }
        }
        public void DeleteMenuChoice(int idMenuChoice)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "DELETE FROM `h22_travail4_2031887`.`tp5_menuchoices` WHERE Id = @lId;";
                sqlCmd.Parameters.AddWithValue("@lId", idMenuChoice);
                sqlCmd.ExecuteNonQuery();

            }
            finally
            {
                conn?.Close();
            }
        }
        public void UpdateMenuChoice(MenuChoice menuChoisi)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "UPDATE `h22_travail4_2031887`.`tp5_menuchoices` SET Description = @LaDescription WHERE (Id = @lId);";
                sqlCmd.Parameters.AddWithValue("@lId", menuChoisi.Id);
                sqlCmd.Parameters.AddWithValue("@LaDescription", menuChoisi.Description);
                sqlCmd.ExecuteNonQuery();

            }
            finally
            {
                conn?.Close();
            }
        }

    }
}
