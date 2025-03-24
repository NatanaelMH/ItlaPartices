using System;
using System.Collections.Generic;

//Natanael Marte Hidalgo 20241711
class Contacto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }

    public Contacto(int id, string nombre, string telefono, string email, string direccion)
    {
        Id = id;
        Nombre = nombre;
        Telefono = telefono;
        Email = email;
        Direccion = direccion;
    }

    public override string ToString()
    {
        return $"{Id}    {Nombre}      {Telefono}      {Email}     {Direccion}";
    }
}

class Agenda
{
    private Dictionary<int, Contacto> contactos = new Dictionary<int, Contacto>();

    public void AgregarContacto(string nombre, string telefono, string email, string direccion)
    {
        int id = contactos.Count + 1;
        Contacto nuevoContacto = new Contacto(id, nombre, telefono, email, direccion);
        contactos.Add(id, nuevoContacto);
    }

    public void VerContactos()
    {
        Console.WriteLine("Id           Nombre          Telefono            Email           Dirección");
        Console.WriteLine("___________________________________________________________________________");

        foreach (var contacto in contactos.Values)
        {
            Console.WriteLine(contacto);
        }
    }

    public void EditarContacto(int id, string nombre, string telefono, string email, string direccion)
    {
        if (contactos.ContainsKey(id))
        {
            contactos[id].Nombre = nombre;
            contactos[id].Telefono = telefono;
            contactos[id].Email = email;
            contactos[id].Direccion = direccion;
        }
        else
        {
            Console.WriteLine("Contacto no encontrado.");
        }
    }

    public void EliminarContacto(int id)
    {
        if (contactos.ContainsKey(id))
        {
            contactos.Remove(id);
        }
        else
        {
            Console.WriteLine("Contacto no encontrado.");
        }
    }

    public Contacto BuscarContacto(int id)
    {
        if (contactos.ContainsKey(id))
        {
            return contactos[id];
        }
        else
        {
            Console.WriteLine("Contacto no encontrado.");
            return null;
        }
    }
}

class Program
{
    static void Main()
    {
        Agenda agenda = new Agenda();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Mi Agenda Perrón");
            Console.WriteLine("Bienvenido a tu lista de contactes");
            Console.Write("1. Agregar Contacto      ");
            Console.Write("2. Ver Contactos     ");
            Console.Write("3. Buscar Contactos      ");
            Console.Write("4. Modificar Contacto        ");
            Console.Write("5. Eliminar Contacto     ");
            Console.WriteLine("6. Salir");
            Console.Write("Elige una opción: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddContact(agenda);
                    break;
                case 2:
                    agenda.VerContactos();
                    break;
                case 4:
                    EditContact(agenda);
                    break;
                case 5:
                    DeleteContact(agenda);
                    break;
                case 3:
                    SearchContact(agenda);
                    break;
                case 6:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida");
                    break;
            }
        }
    }

    static void AddContact(Agenda agenda)
    {
        Console.WriteLine("Vamos a agregar ese contacte que te trae loco.");

        Console.Write("Digite el Nombre: ");
        var name = Console.ReadLine();
        Console.Write("Digite el Teléfono: ");
        var telefono = Console.ReadLine();
        Console.Write("Digite el Email: ");
        var email = Console.ReadLine();
        Console.Write("Digite la dirección: ");
        var address = Console.ReadLine();

        agenda.AgregarContacto(name, telefono, email, address);
    }

    static void EditContact(Agenda agenda)
    {
        agenda.VerContactos();
        Console.WriteLine("Digite un  Id de Contacto Para Editar");
        int idSeleccionado = Convert.ToInt32(Console.ReadLine());

        var contacto = agenda.BuscarContacto(idSeleccionado);
        if (contacto != null)
        {
            Console.Write($"El nombre es: {contacto.Nombre}, Digite el Nuevo Nombre: ");
            var name = Console.ReadLine();
            Console.Write($"El Teléfono es: {contacto.Telefono}, Digite el Nuevo Teléfono: ");
            var telefono = Console.ReadLine();
            Console.Write($"El Email es: {contacto.Email}, Digite el Nuevo Email: ");
            var email = Console.ReadLine();
            Console.Write($"La dirección es: {contacto.Direccion}, Digite la nueva dirección: ");
            var address = Console.ReadLine();

            agenda.EditarContacto(idSeleccionado, name, telefono, email, address);
        }
    }

    static void DeleteContact(Agenda agenda)
    {
        agenda.VerContactos();
        Console.WriteLine("Digite un Id de Contacto Para Eliminar");
        int idSeleccionado = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Seguro que desea eliminar? 1. Si, 2. No");
        int opcion = Convert.ToInt32(Console.ReadLine());

        if (opcion == 1)
        {
            agenda.EliminarContacto(idSeleccionado);
        }
    }

    static void SearchContact(Agenda agenda)
    {
        Console.WriteLine("Digite un Id de Contacto Para Mostrar");
        int idSeleccionado = Convert.ToInt32(Console.ReadLine());

        var contacto = agenda.BuscarContacto(idSeleccionado);
        if (contacto != null)
        {
            Console.WriteLine(contacto);
        }
    }
}
