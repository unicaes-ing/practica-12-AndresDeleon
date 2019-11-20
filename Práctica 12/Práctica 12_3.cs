using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Práctica_12
{
    class Práctica_12_3
    {
        //Andrés Lemus 13/11/2019

        [Serializable]

        public struct Alumno
        {
            public string carnet;
            public string nombre;
            public string carrera;
            private decimal cum;

            public void setCum(decimal cum)
            {
                this.cum = cum;
            }

            public decimal getCum()
            {
                return cum;
            }
        }

        private static Dictionary<string, Alumno> dicAlumnos = new Dictionary<string, Alumno>();
        private static BinaryFormatter format = new BinaryFormatter();
        private const string NOMBRE_ARCHIVO = "alumnos.bin";

        public static bool guardarDiccionario(Dictionary<string, Alumno> diccionario)
        {
            try
            {
                FileStream stream = new FileStream(NOMBRE_ARCHIVO, FileMode.Create, FileAccess.Write);
                format.Serialize(stream, diccionario);
                stream.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static bool leerDiccionario()
        {
            try
            {
                FileStream stream = new FileStream(NOMBRE_ARCHIVO, FileMode.Open, FileAccess.Read);
                dicAlumnos = (Dictionary<string, Alumno>)format.Deserialize(stream);
                stream.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        static void Main(string[] args)
        {
            if (File.Exists(NOMBRE_ARCHIVO))
                leerDiccionario();
            else
                guardarDiccionario(dicAlumnos);
            int op = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("MENU");
                Console.WriteLine("[1] Agregar Alumno");
                Console.WriteLine("[2] Mostrar Alumnos");
                Console.WriteLine("[3] Buscar Alumno");
                Console.WriteLine("[4] Editar Alumno");
                Console.WriteLine("[5] Eliminar Alumno");
                Console.WriteLine("[6] Salir");
                Console.Write("\n [OPCIÓN]: ");
                if (int.TryParse(Console.ReadLine(), out op) && op >= 1 && op <= 6)
                {
                    Console.Clear();
                    switch (op)
                    {
                        case 1:
                            agregarAlumno();
                            break;
                        case 2:
                            mostrarAlumnos();
                            break;
                        case 3:
                            buscarAlumno();
                            break;
                        case 4:
                            editarAlumno();
                            break;
                        case 5:
                            eliminarAlumno();
                            break;
                        case 6:
                            salir();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Tiene que ser un entero entre 1 y 6 :c");
                }
                Console.ReadKey();
            } while (op != 6);
        }

        public static void agregarAlumno()
        {
            try
            {
                Console.WriteLine("AGREGAR ALUMNO \n");
                Alumno alumno = new Alumno();
                do
                {
                    Console.Write("Carnet: ");
                    alumno.carnet = Console.ReadLine();
                    if (dicAlumnos.ContainsKey(alumno.carnet))
                        Console.WriteLine("Ya está registrado ese carnet :c");
                } while (dicAlumnos.ContainsKey(alumno.carnet));
                Console.Write("Nombre: ");
                alumno.nombre = Console.ReadLine();
                Console.Write("Carrera: ");
                alumno.carrera = Console.ReadLine();
                Console.Write("CUM: ");
                alumno.setCum(Convert.ToDecimal(Console.ReadLine()));
                dicAlumnos.Add(alumno.carnet, alumno);
                guardarDiccionario(dicAlumnos);
                Console.WriteLine("\nEl alumno se almacenó con éxito c:");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static void mostrarAlumnos()
        {
            try
            {
                Console.WriteLine("LISTADO DE ALUMNOS");
                Console.WriteLine();
                Console.WriteLine("{0,-9}    {1,-20}   {2,-40}   {3,-4}", "Carnet", "Nombre", "Carrera", "CUM");
                Console.WriteLine("===========================================================================================");
                leerDiccionario();
                foreach (KeyValuePair<string, Alumno> alumGuardados in dicAlumnos)
                {
                    Alumno alum = alumGuardados.Value;
                    Console.WriteLine("{0,-10}   {1,-20}   {2,-40}   {3,-4}", alum.carnet, alum.nombre, alum.carrera, alum.getCum());
                }
                Console.WriteLine("===========================================================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static void buscarAlumno()
        {
            Console.Write("Carnet del Alumno a buscar: ");
            string car = Console.ReadLine();
            leerDiccionario();
            foreach (KeyValuePair<string, Alumno> alumno in dicAlumnos)
            {
                Console.Clear();
                Alumno alum = alumno.Value;
                if (alumno.Key == car)
                {
                    Console.WriteLine("¡Alumno Encontrado!");
                    Console.WriteLine();
                    Console.WriteLine("Carnet: " + alum.carnet);
                    Console.WriteLine("Nombre: " + alum.nombre);
                    Console.WriteLine("Carrera: " + alum.carrera);
                    Console.WriteLine("CUM: " + alum.getCum());
                    goto Afuera;
                }
                else
                {
                    Console.WriteLine("EL CARNET QUE ESTÁ BUSCANDO NO SE ENCUENTRA REGISTRADO O NO ES VÁLIDO");
                }
            }
        Afuera:;
        }

        public static void editarAlumno()
        {
            Console.Write("Carnet del Alumno que desea editar: ");
            string car = Console.ReadLine();
            leerDiccionario();
            foreach (KeyValuePair<string, Alumno> alumno in dicAlumnos)
            {
                Console.Clear();
                Alumno alum = alumno.Value;
                if (alumno.Key == car)
                {
                    Console.WriteLine("DATOS DEL ALUMNO");
                    Console.WriteLine();
                    Console.Write("Carnet: ");
                    alum.carnet = Console.ReadLine();
                    Console.Write("Nombre: ");
                    alum.nombre = Console.ReadLine();
                    Console.Write("Carrera: ");
                    alum.carrera = Console.ReadLine();
                    Console.Write("CUM: ");
                    alum.setCum(Convert.ToDecimal(Console.ReadLine()));
                    dicAlumnos.Remove(car);
                    dicAlumnos.Add(alum.carnet, alum);
                    guardarDiccionario(dicAlumnos);
                    Console.WriteLine("\nEl alumno se editó con éxito :)");
                    goto Afuera;
                }
                else
                {
                    Console.WriteLine("EL CARNET QUE ESTÁ BUSCANDO NO SE ENCUENTRA REGISTRADO O NO ES VÁLIDO");
                }
            }
        Afuera:;
        }

        public static void eliminarAlumno()
        {
            Console.Write("Carnet del Alumno que desea eliminar: ");
            string car = Console.ReadLine();
            leerDiccionario();
            foreach (KeyValuePair<string, Alumno> alumno in dicAlumnos)
            {
                Console.Clear();
                Alumno alum = alumno.Value;
                if (alumno.Key == car)
                {
                    dicAlumnos.Remove(car);
                    guardarDiccionario(dicAlumnos);
                    Console.WriteLine("¡El Alumno del carnet {0} fue eliminado con éxito!", alumno.Key);
                    goto Afuera;
                }
                else
                {
                    Console.WriteLine("EL CARNET QUE ESTÁ BUSCANDO NO SE ENCUENTRA REGISTRADO O NO ES VÁLIDO");
                }
            }
        Afuera:;
        }

        public static void salir()
        {
            Console.WriteLine("¡GRACIAS POR PREFERIRNOS! \nTENGA UN BUEN DÍA ;D");
        }
    }
}
