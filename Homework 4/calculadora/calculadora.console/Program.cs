using System;
//Natanael Marte Hidalgo  20241711
class Fraccion
{
    private int numerador;
    private int denominador;

    public Fraccion()
    {
        numerador = 0;
        denominador = 1;
    }

    public Fraccion(int numerador, int denominador)
    {
        if (denominador != 0)
        {
            this.numerador = numerador;
            this.denominador = denominador;
        }
        else
        {
            Console.WriteLine("El denominador no puede ser cero.");
            this.numerador = 0;
            this.denominador = 1;
        }
    }

    public Fraccion Sumar(Fraccion otraFraccion)
    {
        int nuevoNumerador = this.numerador * otraFraccion.denominador + otraFraccion.numerador * this.denominador;
        int nuevoDenominador = this.denominador * otraFraccion.denominador;
        return new Fraccion(nuevoNumerador, nuevoDenominador);
    }

    public Fraccion Restar(Fraccion otraFraccion)
    {
        int nuevoNumerador = this.numerador * otraFraccion.denominador - otraFraccion.numerador * this.denominador;
        int nuevoDenominador = this.denominador * otraFraccion.denominador;
        return new Fraccion(nuevoNumerador, nuevoDenominador);
    }

    public Fraccion Multiplicar(Fraccion otraFraccion)
    {
        int nuevoNumerador = this.numerador * otraFraccion.numerador;
        int nuevoDenominador = this.denominador * otraFraccion.denominador;
        return new Fraccion(nuevoNumerador, nuevoDenominador);
    }

    public Fraccion Dividir(Fraccion otraFraccion)
    {
        int nuevoNumerador = this.numerador * otraFraccion.denominador;
        int nuevoDenominador = this.denominador * otraFraccion.numerador;
        return new Fraccion(nuevoNumerador, nuevoDenominador);
    }

    public void MostrarFraccion()
    {
        Console.WriteLine($"{numerador}/{denominador}");
    }
}

class Program
{
    static void Main()
    {
        Fraccion fraccion1 = new Fraccion(1, 2);
        Fraccion fraccion2 = new Fraccion(3, 4);

        Fraccion resultadoSuma = fraccion1.Sumar(fraccion2);
        Console.Write("Resultado de la suma: ");
        resultadoSuma.MostrarFraccion();

        Fraccion resultadoResta = fraccion1.Restar(fraccion2);
        Console.Write("Resultado de la resta: ");
        resultadoResta.MostrarFraccion();

        Fraccion resultadoMultiplicacion = fraccion1.Multiplicar(fraccion2);
        Console.Write("Resultado de la multiplicación: ");
        resultadoMultiplicacion.MostrarFraccion();

        Fraccion resultadoDivision = fraccion1.Dividir(fraccion2);
        Console.Write("Resultado de la división: ");
        resultadoDivision.MostrarFraccion();
    }
}

