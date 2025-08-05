using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace CapaDatos
{
    public class Conexion
    {
            #region "PATRON SINGLETON"
            private static Conexion conexion = null;
            private Conexion() { }
            public static Conexion getInstance()
            {
                if (conexion == null)
                {
                    conexion = new Conexion();
                }
                return conexion;
            }
            #endregion

            public SQLiteConnection ConexionBD()
            {
                SQLiteConnection conexion = new SQLiteConnection();
                conexion.ConnectionString = @"Data Source=C:\Users\AdminSena\source\repos\CapaPresentacion\App_Data\clinicadb.db;"
                ;
                return conexion;
            }
        }
    }

