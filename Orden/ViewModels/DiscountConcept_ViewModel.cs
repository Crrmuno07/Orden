using Orden.Model;
using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Orden.ViewModels
{
    public class DiscountConcept_ViewModel : GenericRepository<DiscountConcept>
    {
        private OleDbCommand command;
        private readonly string FileParameter = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), "Archivos", ConfigurationManager.AppSettings["Parametros"]);
           
        public DiscountConcept DiscountConcept(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.DiscountConcepts.Where(P => P.IdTicket == Case).FirstOrDefault();
            }
        }
        public string AgreementPoints(string Agreement)
        {
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                command = new OleDbCommand("SELECT [Puntos] FROM [Convenio$] WHERE [TIPOS] = '" + Agreement + "'", connection);
                connection.Open();
                using (DbDataReader dr = command.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                    }
                }
            }
            return "";
        }
        public string EmployeePercentage(string Coin, string Type)
        {
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                command = new OleDbCommand("SELECT [Porcentaje] FROM [TasasEmpleados$] WHERE [MONEDA] = '" + Coin + "' AND [TIPO] = '" + Type + "'", connection);
                connection.Open();
                using (DbDataReader dr = command.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                    }
                }
            }
            return "";
        }
        public string AttributionsPercentage(string Attribution)
        {
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                command = new OleDbCommand("SELECT [Porcentaje] FROM [Atribuciones$] WHERE [TIPOS] = '" + Attribution + "'", connection);
                connection.Open();
                using (DbDataReader dr = command.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                    }
                }
            }
            return "";
        }
        public string PropertyPercentage(string PropertyConservation)
        {
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                command = new OleDbCommand("SELECT [Porcentaje] FROM [Inmueble$] WHERE [TIPOS] = '" + PropertyConservation + "'", connection);
                connection.Open();
                using (DbDataReader dr = command.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                    }
                }
            }
            return "";
        }
        public string PackagePercentage(string Package)
        {
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                command = new OleDbCommand("SELECT [Porcentaje] FROM [Paquete$] WHERE [TIPOS] = '" + Package + "'", connection);
                connection.Open();
                using (DbDataReader dr = command.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                    }
                }
            }
            return "";
        }
        public void Delete(string Ticket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                var ToDelete = model.DiscountConcepts.Where(X => X.IdTicket == Ticket).FirstOrDefault();
                if (ToDelete != null)
                {
                    model.Entry(ToDelete).State = EntityState.Deleted;
                    model.SaveChanges();
                }
            }
        }
    }
}
