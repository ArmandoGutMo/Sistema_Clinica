using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class PacienteDAO
    {
        #region "PATRON SINGLETON"
        private static PacienteDAO daoPaciente = null;
        private PacienteDAO() { }
        public static PacienteDAO getInstance()
        {
            if (daoPaciente == null)
            {
                daoPaciente = new PacienteDAO();
            }
            return daoPaciente;
        }
        #endregion

        public bool RegistrarPaciente(Paciente objPaciente)
        {
            bool response = false;

            try
            {
                using (SQLiteConnection con = Conexion.getInstance().ConexionBD())
                {

                    string query = "INSERT INTO Paciente (Nombres, ApPaterno, ApMaterno, Edad, Sexo, NroDocumento, Direccion, Telefono, Estado) " +
                                   "VALUES (@Nombres, @ApPaterno, @ApMaterno, @Edad, @Sexo, @NroDocumento, @Direccion, @Telefono, @Estado)";
                    con.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombres", objPaciente.Nombres);
                        cmd.Parameters.AddWithValue("@ApPaterno", objPaciente.ApPaterno);
                        cmd.Parameters.AddWithValue("@ApMaterno", objPaciente.ApMaterno);
                        cmd.Parameters.AddWithValue("@Edad", objPaciente.Edad);
                        cmd.Parameters.AddWithValue("@Sexo", objPaciente.Sexo);
                        cmd.Parameters.AddWithValue("@NroDocumento", objPaciente.NroDocumento);
                        cmd.Parameters.AddWithValue("@Direccion", objPaciente.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", objPaciente.Telefono);
                        cmd.Parameters.AddWithValue("@Estado", objPaciente.Estado);

                        int filas = cmd.ExecuteNonQuery();
                        response = filas > 0;
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar paciente: " + ex.Message);
                response = false;
            }

            return response;
        }

     
        public List<Paciente> ListarPacientes()
        {
            List<Paciente> Lista = new List<Paciente>();
            SQLiteConnection con = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader dr = null;

            try
            {
                con = Conexion.getInstance().ConexionBD();
                con.Open();
                cmd = new SQLiteCommand("SELECT * FROM Paciente", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Paciente objPaciente = new Paciente();
                    objPaciente.IdPaciente = Convert.ToInt32(dr["idPaciente"].ToString());
                    objPaciente.Nombres = dr["nombres"].ToString();
                    objPaciente.ApPaterno = dr["apPaterno"].ToString();
                    objPaciente.ApMaterno = dr["apMaterno"].ToString();
                    objPaciente.Edad = Convert.ToInt32(dr["edad"].ToString());
                    objPaciente.Sexo = dr["sexo"].ToString();
                    objPaciente.NroDocumento = dr["nroDocumento"].ToString();
                    objPaciente.Direccion = dr["direccion"].ToString();
                    objPaciente.Telefono = dr["telefono"].ToString();
                    objPaciente.Estado = Convert.ToBoolean(dr["estado"]);


                    Lista.Add(objPaciente);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return Lista;
        }
    }
}
