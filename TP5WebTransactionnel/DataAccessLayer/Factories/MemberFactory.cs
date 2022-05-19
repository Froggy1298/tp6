using MySql.Data.MySqlClient;
using System.Collections.Generic;
using TP5WebTransactionnel.Models;
namespace TP5WebTransactionnel.DataAccessLayer.Factories
{
    public class MemberFactory
    {
        //TODO ASS
        public Member CreateEmpty()
        {
            return new Member();
        }
        private Member CreateFromReader(MySqlDataReader sqlRow)
        {
            int id = (int)sqlRow["Id"];
            string Nom = sqlRow["Nom"].ToString();
            string Courriel = sqlRow["Courriel"].ToString();
            string userName = sqlRow["NomUtilisateur"].ToString();
            string password = sqlRow["MotPasse"].ToString();
            string role = sqlRow["Role"].ToString();

            return new Member(id, Nom, Courriel, userName, password, role);
        }

        public List<Member> GetAllMember()
        {
            List<Member> members = new List<Member>();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM h22_travail4_2031887.tp5_members";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    members.Add(CreateFromReader(mySqlDataReader));
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return members;
        }
        public Member GetById(int id)
        {
            Member member = null;
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM h22_travail4_2031887.tp5_members WHERE Id = @id";
                mySqlCmd.Parameters.AddWithValue("@id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    member = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return member;
        }
        public Member GetByUsername(string username)
        {
            Member member = null;
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM h22_travail4_2031887.tp5_members WHERE NomUtilisateur = @name";
                mySqlCmd.Parameters.AddWithValue("@name", username);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    member = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return member;

        }

        public void AjouterMember(Member member)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "INSERT INTO `h22_travail4_2031887`.`tp5_members` " +
                    "(`Nom`, `Courriel`, `NomUtilisateur`, `MotPasse`, `Role`) " +
                    "VALUES (@LeNom, @LeCourriel, @LeUserName, @MDP, @Role);";
                sqlCmd.Parameters.AddWithValue("@LeNom", member.Name);
                sqlCmd.Parameters.AddWithValue("@LeCourriel", member.Email.ToString());
                sqlCmd.Parameters.AddWithValue("@LeUserName", member.Username);
                sqlCmd.Parameters.AddWithValue("@MDP", member.Password);
                sqlCmd.Parameters.AddWithValue("@Role", member.Role);
                sqlCmd.ExecuteNonQuery();
            }
            finally
            {
                conn?.Close();
            }
        }
        public void DeleteFromId(int Id)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "DELETE FROM `h22_travail4_2031887`.`tp5_members` WHERE (`Id` = @Id);";
                sqlCmd.Parameters.AddWithValue("@Id", Id);

                sqlCmd.ExecuteNonQuery();

            }
            finally
            {
                conn?.Close();
            }
        }
        public void UpdateMember(Member member)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "UPDATE `h22_travail4_2031887`.`tp5_members` " +
                    "SET `Nom` = @Nom," +
                    " `Courriel` = @Mail," +
                    " `NomUtilisateur` = @UserName," +
                    " `MotPasse` = @MDP," +
                    "`Role` = @RL" + 
                    " WHERE (`Id` = @Id);";
                sqlCmd.Parameters.AddWithValue("@Id", member.Id);
                sqlCmd.Parameters.AddWithValue("@Nom", member.Name);
                sqlCmd.Parameters.AddWithValue("@Mail", member.Email);
                sqlCmd.Parameters.AddWithValue("@MDP", member.Password);
                sqlCmd.Parameters.AddWithValue("@RL", member.Role);
                sqlCmd.Parameters.AddWithValue("@UserName", member.Username);
                sqlCmd.ExecuteNonQuery();

            }
            finally
            {
                conn?.Close();
            }

        }

       

    }
}
