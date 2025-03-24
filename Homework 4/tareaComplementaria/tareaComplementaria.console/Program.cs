using System;
using System.Collections.Generic;

// Natanael MArte Hidalgo 20241711
class Paciente
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Diagnostico { get; set; }

    public Paciente(int id, string nombre, int edad, string diagnostico)
    {
        ID = id;
        Nombre = nombre;
        Edad = edad;
        Diagnostico = diagnostico;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"ID: {ID} | Nombre: {Nombre} | Edad: {Edad} | Diagnóstico: {Diagnostico}");
    }
}


class AdministradorPacientes
{
    private List<Paciente> listaPacientes = new List<Paciente>();
    private int contadorID = 1;

    public void AgregarPaciente(string nombre, int edad, string diagnostico)
    {
        Paciente nuevo = new Paciente(contadorID, nombre, edad, diagnostico);
        listaPacientes.Add(nuevo);
        contadorID++;
        Console.WriteLine("Paciente agregado correctamente.\n");
    }

    public void MostrarPacientes()
    {
        if (listaPacientes.Count == 0)
        {
            Console.WriteLine("No hay pacientes registrados.\n");
            return;
        }

        Console.WriteLine("Lista de Pacientes:");
        foreach (var paciente in listaPacientes)
        {
            paciente.MostrarInformacion();
        }
        Console.WriteLine();
    }

    public void BuscarPaciente(int id)
    {
        var paciente = listaPacientes.Find(p => p.ID == id);
        if (paciente != null)
        {
            Console.WriteLine("Paciente encontrado:");
            paciente.MostrarInformacion();
        }
        else
        {
            Console.WriteLine("Paciente no encontrado.\n");
        }
    }

    public void EliminarPaciente(int id)
    {
        var paciente = listaPacientes.Find(p => p.ID == id);
        if (paciente != null)
        {
            listaPacientes.Remove(paciente);
            Console.WriteLine("Paciente eliminado correctamente.\n");
        }
        else
        {
            Console.WriteLine("Paciente no encontrado.\n");
        }
    }
}

class Program
{
    static void Main()
    {
        AdministradorPacientes admin = new AdministradorPacientes();
        int opcion;
        do
        {
            Console.WriteLine("\nSistema de Registro de Pacientes");
            Console.WriteLine("1. Agregar Paciente");
            Console.WriteLine("2. Mostrar Pacientes");
            Console.WriteLine("3. Buscar Paciente");
            Console.WriteLine("4. Eliminar Paciente");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.Write("Nombre: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Edad: ");
                    int edad = int.Parse(Console.ReadLine());
                    Console.Write("Diagnóstico: ");
                    string diagnostico = Console.ReadLine();
                    admin.AgregarPaciente(nombre, edad, diagnostico);
                    break;
                case 2:
                    admin.MostrarPacientes();
                    break;
                case 3:
                    Console.Write("Ingrese el ID del paciente: ");
                    int idBuscar = int.Parse(Console.ReadLine());
                    admin.BuscarPaciente(idBuscar);
                    break;
                case 4:
                    Console.Write("Ingrese el ID del paciente a eliminar: ");
                    int idEliminar = int.Parse(Console.ReadLine());
                    admin.EliminarPaciente(idEliminar);
                    break;
                case 5:
                    Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción no válida, intente nuevamente.");
                    break;
            }
        } while (opcion != 5);
    }
}

