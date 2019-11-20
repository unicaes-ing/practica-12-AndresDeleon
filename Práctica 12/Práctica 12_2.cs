using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Práctica_12
{
    class Práctica_12_2
    {
        //Andrés Lemus 13/11/2019
        static void Main(string[] args)
        {
            Program.Mascotas mascotas;
            FileStream stream;
            BinaryFormatter format = new BinaryFormatter();
            if (File.Exists("mascotas.bin"))
            {
                try
                {
                    stream = new FileStream("mascotas.bin", FileMode.Open, FileAccess.Read);
                    mascotas = (Program.Mascotas)format.Deserialize(stream);
                    stream.Close();
                    Console.WriteLine("DATOS DE LA MASCOTA");
                    Console.WriteLine();
                    Console.WriteLine("Nombre: " + mascotas.nombre);
                    Console.WriteLine("Especie: " + mascotas.especie);
                    Console.WriteLine("Raza: " + mascotas.raza);
                    Console.WriteLine("Sexo: " + mascotas.sexo);
                    Console.WriteLine("Edad: " + mascotas.getEdad());
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
}
