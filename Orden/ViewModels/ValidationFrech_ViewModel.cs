using Orden.Helpers;
using Orden.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Orden.ViewModels
{
    public class ValidationFrech_ViewModel : GenericRepository<ValidationFrech>
    {
        private readonly string FileParameter = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), "Archivos", ConfigurationManager.AppSettings["Parametros"]);
        private readonly string FileMCY = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), "Archivos", ConfigurationManager.AppSettings["PathFileTxt"]);

        public CommonFunctions common = new CommonFunctions();
        private OleDbCommand command;

        public ValidationFrech ValidationFrech(string Case)
        {
            using (ModelOrder model = new ModelOrder())
            {
                return model.ValidationFreches.Where(P => P.IdTicket == Case).FirstOrDefault();
            }
        }
        public int Salary()
        {
            int Salary = 0;
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand("Select [SMMLV] From [Modalidad$]", connection))
                {
                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(dr);
                            Salary = int.Parse(dt.Rows[0]["SMMLV"].ToString());
                        }
                    }
                }
            }
            return Salary;
        }
        public void Delete(string Ticket)
        {
            using (ModelOrder model = new ModelOrder())
            {
                ValidationFrech ToDelete = model.ValidationFreches.Where(X => X.IdTicket == Ticket).FirstOrDefault();
                if (ToDelete != null)
                {
                    model.Entry(ToDelete).State = EntityState.Deleted;
                    model.SaveChanges();
                }
            }
        }
        public int HistoricalSalary(int year)
        {
            int Salary = 0;
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand("Select [valor] From [HistoricoSalario$] where [año] = " + year + "", connection))
                {
                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(dr);
                            if (dt.Rows.Count == 1)
                            {
                                Salary = int.Parse(dt.Rows[0]["valor"].ToString());
                            }
                        }
                    }
                }
            }
            return Salary;
        }
        public string GetYear(string IdParticipant)
        {
            try
            {
                if (File.Exists(FileMCY))
                {
                    string year = "";
                    if (FileMCY != "")
                    {
                        string[] Values = { "ANULADO", "HABILITADO", "RECHAZADO", "RENUNCIA", "VENCIDO" };
                        string[] campos;
                        List<string> filas = File.ReadAllLines(FileMCY)
                                .Where(x => x.Contains(IdParticipant.ToString()))
                                .ToList();
                        if (filas.Count() == 0)
                        {
                            return "Cliente no se encuentra en la base de MCY";
                        }
                        foreach (var item in filas)
                        {
                            campos = item.Split("|".ToCharArray());
                            if (campos[21] == IdParticipant.ToString())
                            {
                                bool var = Values.Contains(campos[15]);
                                if (var == false)
                                {
                                    if (campos[8] != "")
                                    {
                                        year = campos[8].Substring(6);
                                        return year;
                                    }
                                    else
                                    {
                                        return "Cliente se encuentra en MCY pero no tiene año de asignación";
                                    }
                                }
                            }
                        }
                        if (year == "") return "Cliente Sin Asignacion de MCY";
                    }
                    return "";
                }
                else
                {
                    MessageBox.Show("El archivo mi casa ya no se encuentra en la ruta especificado: " + FileMCY, "Mensaje Informativo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public int PointsSalary(decimal Appraisal, int Year)
        {
            int salary = HistoricalSalary(Year);
            int Val1 = 0;
            int Val2 = 0;
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand("SELECT [RangoInicial],[RangoFinal] FROM [SalarioPuntos$]", connection))
                {
                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Val1 = int.Parse(dr[0].ToString());
                                Val2 = int.Parse(dr[1].ToString());
                            }
                        }
                    }
                }
            }
            if (Appraisal > 0 && Appraisal <= (salary * Val1))
            {
                return 5;
            }
            else if (Appraisal > (salary * Val1) && Appraisal <= (salary * Val2))
            {
                return 4;
            }
            else
            {
                return 0;
            }
        }
        public string MCY(string City, string Dept, decimal? Val, int Year)
        {
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                connection.Open();
                int salary = HistoricalSalary(Year);
                int Val1 = 0;
                int Val2 = 0;
                if (Year > 2019)
                {
                    if (City != "" && Dept != "" && Val != 0)
                    {
                        string Query = "SELECT [Valor] FROM [CiudadesModalidad$] WHERE [Departamento] = '" + Dept + "' AND [Municipio] = '" + City + "'";
                        string ValCity = common.ExecuteQuery(Query);
                        if (ValCity == "") ValCity = "0";
                        if (Val.ToString() != "")
                        {
                            command = new OleDbCommand("SELECT [Rango1],[Rango2] FROM[RangosMCY$] WHERE[Ciudad] = " + ValCity + " AND [Anno] >= " + Year + "", connection);
                            using (DbDataReader dr = command.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        Val1 = int.Parse(dr[0].ToString()) * salary;
                                        Val2 = int.Parse(dr[1].ToString()) * salary;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    command = new OleDbCommand("SELECT [Rango1],[Rango2] FROM[RangosMCY$] WHERE [Anno] <= 2019", connection);
                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Val1 = int.Parse(dr[0].ToString()) * salary;
                                Val2 = int.Parse(dr[1].ToString()) * salary;
                            }
                        }
                    }
                }
                if (!(Val >= Val1 && Val <= Val2))
                {
                    return "El valor avalúo supera el rango del año de asignación de mi casa ya";
                }
            }
            return "";
        }
        public string PointsModalityMCY(string City, string Dept, decimal? Val, int Year)
        {
            string excelConnectionString = string.Format(ConfigurationManager.ConnectionStrings["Excel"].ConnectionString, FileParameter);
            using (OleDbConnection connection = new OleDbConnection(excelConnectionString))
            {
                connection.Open();
                int salary = HistoricalSalary(Year);
                decimal rango = (decimal)(Val / salary);
                string Query = "SELECT [Valor] FROM [CiudadesModalidad$] WHERE [Departamento] = '" + Dept + "' AND [Municipio] = '" + City + "'";
                string ValCity = common.ExecuteQuery(Query);
                if (ValCity == "") ValCity = "0";
                using (OleDbCommand command = new OleDbCommand("SELECT [Moda] FROM [ModalidadMCY$] WHERE[Ciudad] = " + ValCity + "  AND " + rango + " BETWEEN[RangoInicial] AND [RangoFinal]", connection))
                {
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
            }
            return "";
        }
    }
}
