using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Examen1.Controllers
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection _connection;
        public DataBase() { }

        public DataBase(string dbpath)
        {
            _connection = new SQLiteAsyncConnection(dbpath);
            // Creacion de tablas de base de datos
            _connection.CreateTableAsync<Models.Contactos>().Wait();
        }


        // Creacion de metodos crud de base de datos Create - Read - Update - Delete

        public Task<int> AddContacto(Models.Contactos est)
        {
            if (est.id == 0)
            {
                return _connection.InsertAsync(est);
            }
            else
            {
                return _connection.UpdateAsync(est);
            }
        }

        public Task<List<Models.Contactos>> ObtenerListaContactos()
        {
            return _connection.Table<Models.Contactos>().ToListAsync();
        }

        public Task<Models.Contactos> ObtenerContacto(int pid)
        {
            return _connection.Table<Models.Contactos>().Where(i => i.id == pid).FirstOrDefaultAsync();
        }

        public Task<int> DeleteContacto(Models.Contactos est)
        {
            return _connection.DeleteAsync(est);
        }
    }
}
