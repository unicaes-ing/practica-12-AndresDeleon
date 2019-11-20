using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Práctica_12
{
    class Program
    {
        //Andrés Lemus 13/11/2019
        [Serializable]

        public struct Mascotas
        {
            public string nombre;
            public string especie;
            public string raza;
            public string sexo;
            private int edad;

            public void setEdad(int edad)
            {
                if (edad >= 0)
                {
                    this.edad = edad;
                }
            }

            public int getEdad()
            {
                return edad;
            }
        }
        static void Main(string[] args)
        {
            Mascotas mascota = new Mascotas();
            FileStream stream;
            BinaryFormatter format = new BinaryFormatter();
            try
            {
                Console.WriteLine("ESTRUCTURA DE MASCOTAS");
                Console.WriteLine();
                Console.Write("Nombre: ");
                mascota.nombre = Console.ReadLine();
                Console.Write("Especie: ");
                mascota.especie = Console.ReadLine();
                Console.Write("Raza: ");
                mascota.raza = Console.ReadLine();
                Console.Write("Sexo: ");
                mascota.sexo = Console.ReadLine();
                Console.Write("Edad (años): ");
                mascota.setEdad(Convert.ToInt32(Console.ReadLine()));
                stream = new FileStream("mascotas.bin", FileMode.Create, FileAccess.Write);
                format.Serialize(stream, mascota);
                stream.Close();
                Console.WriteLine();
                Console.WriteLine("La mascota fue almacenada con éxito c:");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
