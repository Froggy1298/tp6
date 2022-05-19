using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TP5WebTransactionnel.Models;
using TP5WebTransactionnel.DataAccessLayer;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace TP5WebTransactionnel.DataAccessLayer.Factories
{
    public class ReservationFactory
    {
        DAL dal = new DAL();
        public Reservation CreateEmpty()
        {
            return new Reservation(0,string.Empty,default,0,DateTime.Now,default);
        }
        private Reservation CreateFromReader(MySqlDataReader sqlRow)
        {
            int _Id = sqlRow.GetInt32("Id");
            string _Nom = sqlRow.GetString("Nom");
            string _Courriel = sqlRow.GetString("Courriel");
            int _NbPersonne = sqlRow.GetInt32("NbPersonne");
            DateTime _DateReservation = sqlRow.GetDateTime("DateReservation");
            MenuChoice _MenuChoice = dal.MenuChoiceFactory.GetById(sqlRow.GetInt32("MenuChoiceId"));

            return new Reservation(_Id, _Nom, _Courriel, _NbPersonne, _DateReservation, _MenuChoice);
        }
        
        public List<Reservation> GetAllReservation()
        { 
            List<Reservation> reservations = new List<Reservation>();
            MySqlConnection conn = null;
            MySqlDataReader dataReader = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM h22_travail4_2031887.tp5_reservations order by Nom;";

                dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    reservations.Add(CreateFromReader(dataReader));
                }

            }
            finally
            {
                dataReader.Close();
                conn.Close();
            }
            return reservations;
        }
        public Reservation GetById(int Id)
        {
            Reservation reservation = null;
            MySqlConnection conn = null;
            MySqlDataReader dataReader = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "SELECT * FROM h22_travail4_2031887.tp5_reservations WHERE Id = @Id";
                sqlCmd.Parameters.AddWithValue("@Id", Id);

                dataReader = sqlCmd.ExecuteReader();
                if (dataReader.Read())
                {
                    reservation = CreateFromReader(dataReader);
                }
            }
            finally
            {
                dataReader?.Close();
                conn?.Close();
            }
            return reservation;
        }
        public void AjouterReservation(Reservation reservation)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "INSERT INTO `h22_travail4_2031887`.`tp5_reservations` " +
                    "(`Nom`, `Courriel`, `NbPersonne`, `DateReservation`, `MenuChoiceId`) " +
                    "VALUES (@LeNom, @LeCourriel, @LeNbPersonne, @LaDateReservation, @LeIdMenuChoisi);";
                sqlCmd.Parameters.AddWithValue("@LeNom", reservation.Nom);
                sqlCmd.Parameters.AddWithValue("@LeCourriel", reservation.Courriel.ToString());
                sqlCmd.Parameters.AddWithValue("@LeNbPersonne", reservation.NbPersonne);
                sqlCmd.Parameters.AddWithValue("@LaDateReservation", reservation.DateReservation);
                sqlCmd.Parameters.AddWithValue("@LeIdMenuChoisi", reservation.ChoixMenu.Id);
                sqlCmd.ExecuteNonQuery();
                reservation.Id = GetLastIdFromBd();
            }
            finally
            {
                conn?.Close();
            }
        }
        private int GetLastIdFromBd()
        {
            MySqlConnection conn = null;
            MySqlDataReader dataReader = null;

            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id FROM h22_travail4_2031887.tp5_reservations order by Id desc limit 1;";

                dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                    return dataReader.GetInt32("Id");
            }
            finally
            {
                conn?.Close();
            }
            return -1;
        }

        public void DeleteFromId(int Id)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(DAL.ConnectionString);
                conn.Open();

                MySqlCommand sqlCmd = conn.CreateCommand();
                sqlCmd.CommandText = "DELETE FROM `h22_travail4_2031887`.`tp5_reservations` WHERE (`Id` = @Id);";
                sqlCmd.Parameters.AddWithValue("@Id", Id);

                sqlCmd.ExecuteNonQuery();

            }
            finally
            {
                conn?.Close();
            }
        }



    }
}
