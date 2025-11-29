using System.Collections.Generic;

namespace Library
{
    public sealed class BaseDatosAdmin
    {
        private static readonly BaseDatosAdmin _instance = new BaseDatosAdmin();
        
        public List<Admin> ListaAdmin = new List<Admin>();
        
        private BaseDatosAdmin()
        {
        }
        
        public static BaseDatosAdmin Instance
        {
            get { return _instance; }
        }

        public bool ExisteNombre(string name)
        {
            bool result = false;
            foreach (Admin admin in ListaAdmin)
            {
                if (admin.Nombre == name)
                {
                    result = true;
                }
            }
            return result;
        }
        
        public void AgregarAdmin(Admin admin)
        {
            string nombre = admin.Nombre;
            if (!ExisteNombre(nombre))
            {
                ListaAdmin.Add(admin);
            }
        }
        
        public Admin AdminSegunNombre(string nombre)
        {
            Admin admin = ListaAdmin.Find(x => x.Nombre == nombre);
            return admin;
        }
    }
}