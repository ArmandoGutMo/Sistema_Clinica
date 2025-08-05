using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class EmpleadoDAO
    {

        #region "PATRON SINGLETON"
        private static EmpleadoDAO daoEmpleado = null;
        private EmpleadoDAO() { }
        public static EmpleadoDAO getInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new EmpleadoDAO();
            }
            return daoEmpleado;
        }
        #endregion


        public Empleado AccesoSistema(String user, String password)
        {
            SQLiteConnection conexion = null;
            SQLiteCommand cmd = null;
            Empleado objEmpleado = null;
            SQLiteDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionBD();
                string query = "SELECT * FROM Empleado WHERE usuario = @prmUser AND clave = @prmPassword";
                cmd = new SQLiteCommand(query, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@prmUser", user);
                cmd.Parameters.AddWithValue("@prmPassword", password);
                conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    objEmpleado = new Empleado();
                    objEmpleado.ID = Convert.ToInt32(dr["idEmpleado"].ToString());
                    objEmpleado.Usuario = dr["usuario"].ToString();
                    objEmpleado.Clave = dr["clave"].ToString();
                }
            }
            catch (Exception ex)
            {
                objEmpleado = null;
                throw ex;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return objEmpleado;
        }
    }
}
