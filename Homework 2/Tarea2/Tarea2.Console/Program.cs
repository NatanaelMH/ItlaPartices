using System;

class Program
{
    static void Main()
    {
        // Natanael Marte 20241711
        // Programa que determina si un número es par o impar

        Console.Write("Ingrese un número: ");
        int numero = Convert.ToInt32(Console.ReadLine()); 

        
        if (numero % 2 == 0)
        {
            Console.WriteLine("El número " + numero + " es PAR.");
        }
        else
        {
            Console.WriteLine("El número " + numero + " es IMPAR.");
        }

        Console.ReadLine(); 
    }
}

