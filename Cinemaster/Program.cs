using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinemaster
{
    
    public enum EstadoAsiento
    {
        Libre,
        Ocupado

    }

    public class Persona
    {

        public string Nombre;
        public string Apellido;

        public Persona(string nombre, string apellido)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;

        }

    }

    public class Pelicula
    {
        public string Titulo;
        public string TituloOriginal;
        public Persona Director;
        public Dictionary<Persona, string> Reparto;
        public TimeSpan Duracion;
        public string Sinopsis;

        public Pelicula(string titulo, string titulooriginal, Persona director, Dictionary<Persona,string>reparto, TimeSpan duracion, string sinopsis)
        {

            this.Titulo = titulo;
            this.TituloOriginal = titulooriginal;
            this.Director = director;
            this.Reparto = reparto;
            this.Duracion = duracion;
            this.Sinopsis = sinopsis;

        }

    }

    public class Asiento
    {
        public int Fila;
        public int Columna;
        public bool EsVip;

        public Asiento (int fila, int columna)
        {
            this.Fila = fila;
            this.Columna = columna;

        }

    }

    public class Sala
    {
        public int Numero;
        public Asiento[,] Asientos;

        public Sala(int numero, Asiento [,] asientos)
        {
            this.Numero = numero;
            this.Asientos = asientos;                       
            
            for (int filas = 0; filas < this.Asientos.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < this.Asientos.GetLength(1); columnas++)
                {

                    
                    if (filas == this.Asientos.GetLength(0) - 1 || filas == this.Asientos.GetLength(0) - 2 || filas == this.Asientos.GetLength(0) - 3)
                    {
                                                                       

                            this.Asientos[filas, columnas] = new Asiento(filas, columnas);

                            this.Asientos[filas, columnas].EsVip = true;


                        

                    }
                    else

                    this.Asientos[filas, columnas] = new Asiento(filas, columnas);


                }

            }

        }

    }

    public class Funcion
    {

        public Pelicula Pelicula;
        public Sala Sala;
        public DateTime FechaHora;
        public Dictionary<Asiento, EstadoAsiento> EstadoAsientos = new Dictionary<Asiento, EstadoAsiento>();

        public Funcion(Pelicula pelicula, Sala sala, DateTime fechahora)
        {
            this.Pelicula = pelicula;
            this.Sala = sala;
            this.FechaHora = fechahora;

            for (int filas = 0; filas < this.Sala.Asientos.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < this.Sala.Asientos.GetLength(1); columnas++)
                {

                   this.EstadoAsientos.Add(this.Sala.Asientos[filas,columnas], EstadoAsiento.Libre);

                }

            }

        }

        public bool IntentarOcuparAsiento(Asiento asientoaocupar)
        {

            if (asientoaocupar == null)
            {
                throw new Exception("El asiento es null");                
            }


            if (this.EstadoAsientos[asientoaocupar] == EstadoAsiento.Ocupado)
                
                return false;


            this.EstadoAsientos[asientoaocupar] = EstadoAsiento.Ocupado;

            return true;


        }             

    }

    public class Entrada
    {
        public Funcion Funcion;
        public Asiento Asiento;
        public int Precio;
        public DateTime FechaEmision;

        public Entrada(Funcion funcion, Asiento asiento)
        {

            this.Funcion = funcion;
            this.Asiento = asiento;
            
            this.FechaEmision = DateTime.Now;

        }

    }

    public class Cine
    {
        public string Nombre;
        public List<Pelicula> Peliculas = new List<Pelicula>();       
        public List<Sala> Salas = new List<Sala>();
        public List<Funcion> Funciones = new List<Funcion>();
        public List<Entrada> Entradas = new List<Entrada>();
        public int PrecioEntrada;
        public int PrecioEntradaVIP;

        public Cine(string nombre, int precioentrada, int precioentradavip)
        {
            this.Nombre = nombre;
            this.PrecioEntrada = precioentrada;
            this.PrecioEntradaVIP = precioentradavip;

        }

        public List<Funcion> BuscarFuncion(Cine cine, Pelicula peliopcion)
        {

            if (peliopcion == null)
            {

                throw new Exception();

            }

            List<Funcion> funcionopcion = new List<Funcion>();

            int i = -1;

            foreach (var f in cine.Funciones)
            {

                if (f.Pelicula.Titulo == peliopcion.Titulo)
                {
                    i++;

                    Console.WriteLine($"{i} - {f.FechaHora.ToShortDateString()} {f.FechaHora.ToShortTimeString()}");


                    funcionopcion.Add(f);

                }

            }

            Console.WriteLine();

            return funcionopcion;

        }

    }


    class Program
    {
        public static string CentrarString(string str, int largo, char c)
        {
            int cantidad = largo - str.Length;

            if (cantidad <= 1)
            {
                if (cantidad == 1)
                {
                    return str.PadRight(largo);
                }
                return str;
            }

            int padleft = cantidad / 2 + str.Length;

            return str.PadLeft(padleft, c).PadRight(largo, c);
        }

        public static int PedirValorInt(string mensaje)
        {
            int rv = 0;

            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out rv))
            {
                Console.WriteLine("Valor invalido.");
                Console.Write(mensaje);
            }
            return rv;
        }

        public static int PedirValorIntClampeado(string mensaje, int cotaInferior, int cotaSuperior)
        {
            int rv = PedirValorInt(mensaje);

            while (rv < cotaInferior || rv > cotaSuperior)
            {
                Console.WriteLine("Valor invalido.");
                rv = PedirValorInt(mensaje);
            }

            return rv;
        }

        public static void PantallaPrincipal(Cine cine)
        {
            bool ingreso = false; int eleccion; int contador = -1;


            List<Pelicula> peliculas = new List<Pelicula>();

            foreach (var p in cine.Peliculas)
            {

                peliculas.Add(p);

            }                      
                       
                        
            while (!ingreso)
            {
                
                Console.Clear();

                Console.WriteLine("Bienvenido a CineMaster\n¿Que pelicula quiere ver?\n");

                foreach (var p in peliculas)
                {
                    contador++;

                    Console.WriteLine($"{contador} - {p.Titulo} ({p.TituloOriginal})");

                }

                Console.WriteLine();

                eleccion = PedirValorIntClampeado("Su eleccion: ", 0, peliculas.Count - 1);
                               

                Pelicula peliopcion = peliculas.ElementAt(eleccion);

                contador = -1;

                PantallaSeleccionFuncion(peliopcion, cine);

            }

            


        }

        public static void PantallaSeleccionFuncion(Pelicula peliopcion, Cine cine)
        {

            int eleccion;            
                          
            Console.Clear();


            Console.WriteLine($"Titulo: {peliopcion.Titulo}\n\nTitulo Original: {peliopcion.TituloOriginal}\nDuracion: {peliopcion.Duracion.TotalMinutes} minutos\nDirector: {peliopcion.Director.Nombre} {peliopcion.Director.Apellido}\n");

            foreach (var keyValuePair in peliopcion.Reparto)
            {
                Console.WriteLine($"*{keyValuePair.Key.Nombre} {keyValuePair.Key.Apellido} como: {keyValuePair.Value}");
            }

            Console.WriteLine($"\n{peliopcion.Sinopsis}\n");

            //////////////////////////////////////////////////////////////////////////////////////////////////

            Console.WriteLine("Funciones:\n");           

            List<Funcion> funcionopcion = cine.BuscarFuncion(cine, peliopcion);

            Console.WriteLine("Ingrese -1 para regresar al menu anterior.\n");

            eleccion = PedirValorIntClampeado("Su eleccion: ", -1, funcionopcion.Count - 1) ;

            if (eleccion == -1)
            {

                return;

            }
           
            Funcion funcionelegida = funcionopcion.ElementAt(eleccion);

            
            PantallaSeleccionAsiento(cine, funcionelegida);                   
            

        }

        static void PantallaSeleccionAsiento(Cine cine, Funcion funcion)
        {
            int ingresofila; int ingresocolumna; bool ocupar = false;

            string fecha = funcion.FechaHora.ToShortDateString();
            string hora = funcion.FechaHora.ToShortTimeString();

            Console.Clear();


            ////////////////////////////////////////////////////////////////////////////////////////////////// 

            Console.WriteLine($"Pelicula: {funcion.Pelicula.Titulo}\n");

            Console.WriteLine($"Funcion: {fecha} {hora}\n");

            string str = "PANTALLA";

            char c = '=';

            int largo = (3 * funcion.Sala.Asientos.GetLength(1));

            Console.WriteLine($" {CentrarString(str, largo, c)}");

            for (int col = 0; col < funcion.Sala.Asientos.GetLength(1); col++)
            {
                Console.Write($"  {col}");

            }

            Console.WriteLine();

            for (int filas = 0; filas < funcion.Sala.Asientos.GetLength(0); filas++)
            {
                Console.Write($"{filas}");

                for (int columnas = 0; columnas < funcion.Sala.Asientos.GetLength(1); columnas++)
                {
                                        
                    if (funcion.EstadoAsientos[funcion.Sala.Asientos[filas, columnas]] == EstadoAsiento.Ocupado)
                    {

                        Console.Write("[");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("O");
                        Console.ResetColor();
                        Console.Write("]");

                    }
                    else if (funcion.Sala.Asientos[filas, columnas].EsVip)
                    {
                        Console.Write("[V]");

                    }

                    else
                        Console.Write("[ ]");

                }

                Console.WriteLine();
            }


            Console.WriteLine();

            if (funcion.FechaHora.DayOfWeek == DayOfWeek.Wednesday)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Precio dia miercoles!\n");
                Console.ResetColor();

                Console.WriteLine($"Referencias:\n[ ] = Libre ${cine.PrecioEntrada / 2} Promo\n[V] = Vip ${cine.PrecioEntradaVIP/2} Promo\n[O] = Ocupado\nIngrese -1 para cancelar.\n");


            }
            else
            Console.WriteLine($"Referencias:\n[ ] = Libre ${cine.PrecioEntrada}\n[V] = Vip ${cine.PrecioEntradaVIP}\n[O] = Ocupado\nIngrese -1 para cancelar.\n");


            ////////////////////////////////////////////////////////////////////////////////////////////////// 


            while (!ocupar)
            {
                ingresofila = PedirValorIntClampeado("Ingrese fila:\n", -1, funcion.Sala.Asientos.GetLength(0)-1);

                if (ingresofila == -1 )
                {

                    return;

                }

                ingresocolumna = PedirValorIntClampeado("Ingrese columna:\n", -1, funcion.Sala.Asientos.GetLength(1)-1);

                
                if (ingresocolumna == -1 )
                {

                    return;

                }


                ocupar = funcion.IntentarOcuparAsiento(funcion.Sala.Asientos[ingresofila, ingresocolumna]);

                if (!ocupar)
                {

                    Console.WriteLine("Asiento ocupado. Seleccione otro asiento.");
                                        
                }


                if (ocupar)
                {                    
                    
                    cine.Entradas.Add(new Entrada(funcion, funcion.Sala.Asientos[ingresofila, ingresocolumna]));

                }                           
                            


            }


            Console.Clear();


            ////////////////////////////////////////////////////////////////////////////////////////////////// 

                                                
            Console.WriteLine("Confirmar asiento:\n");

            Console.WriteLine($"Pelicula: {funcion.Pelicula.Titulo}\nDia: {fecha} Hora: {hora}\nSala: {funcion.Sala.Numero}\n\nPresione enter para continuar.\n");
                       
            Console.ReadLine();                       

        }
        
        static void Main(string[] args)
        {

            Persona director1 = new Persona("Sean","Cunningan");
           
            Dictionary<Persona, string> reparto1 = new Dictionary<Persona, string>();
            reparto1.Add(new Persona("Ari", "Lehman"), "Jason");
            reparto1.Add(new Persona("Adriane","King"),"Alice");
            reparto1.Add(new Persona("Harry", "Crosby"),"Bill");

            string sinopsis1 = "Un grupo de adolescentes que son asesinados uno por uno por un asesino desconocido al intentar reabrir un campamento abandonado.";

            Pelicula pelicula1 = new Pelicula("Viernes 13", "Friday the 13th", director1, reparto1, new TimeSpan(1,28,0),sinopsis1);

            ///////////////////////////////////////////////////////////////////////////

            Persona director2 = new Persona("Stanley", "Cubrik");

            Dictionary<Persona, string> reparto2 = new Dictionary<Persona, string>();
            reparto2.Add(new Persona("Jack","Nicholson"),"Jack Torrance");
            reparto2.Add(new Persona("Shelley","Duvall"),"Wendy Torrance");
            reparto2.Add(new Persona("Danny", "LLoyd"), "Dany Torrance");

            string sinopsis2 = "Jack Torrance acepta una oferta de trabajo en un hotel de montaña que se encuentra a 65 kilómetros del pueblo más cercano. ... Danny, el hijo de Jack tiene la capacidad de ver visiones sobre el pasado del hotel y de resistirse a su poder hipnótico";

            Pelicula pelicula2 = new Pelicula("El Resplandor", "The Shinning", director2, reparto2, new TimeSpan(2,26,00),sinopsis2);

            ///////////////////////////////////////////////////////////////////////////

            Persona director3 = new Persona("John", "Carpenter");

            Dictionary<Persona, string> reparto3 = new Dictionary<Persona, string>();
            reparto3.Add(new Persona("Kurt","Russell"),"R.J McReady");
            reparto3.Add(new Persona("Wildford","Brimley"), "Dr.Blair");
            reparto3.Add(new Persona("Keith","David"), "Childs");

            string sinopsis3 = "Un grupo de investigadores estadounidenses en Antártida que se encuentran con la «Cosa», una forma de vida extraterrestre parasitaria que se asimila y luego imita a otros organismos.";


            Pelicula pelicula3 = new Pelicula("La Cosa", "The Thing", director3, reparto3, new TimeSpan(1,45,0),sinopsis3);

            ///////////////////////////////////////////////////////////////////////////

            Persona director4 = new Persona("Ridley", "Scott");

            Dictionary<Persona, string> reparto4 = new Dictionary<Persona, string>();
            reparto4.Add(new Persona("Sigourney", "Weaver"), "Ellen Ripley");
            reparto4.Add(new Persona("Veronica", "Cartwright"), "Lambert");
            reparto4.Add(new Persona("Yaphet", "Kotto"), "Parker");

            string sinopsis4 = "La tripulación de la nave espacial Nostromo atiende una señal de auxilio y, sin saberlo, sube a bordo una letal forma de vida extraterrestre.";

            Pelicula pelicula4 = new Pelicula("Alien, el octavo pasajero", "Alien", director4, reparto4, new TimeSpan(1,56,0), sinopsis4); ;

            ///////////////////////////////////////////////////////////////////////////
            

            Asiento[,] asientossala1 = new Asiento[8,8];                       

            Sala sala1 = new Sala(1, asientossala1);                                   

            Cine cine = new Cine("Cinemaster", 100, 200);

            cine.Peliculas.Add(pelicula1);
            cine.Peliculas.Add(pelicula2);
            cine.Peliculas.Add(pelicula3);
            cine.Peliculas.Add(pelicula4);

            cine.Salas.Add(sala1);                      

            Funcion funcion1 = new Funcion(pelicula1, sala1, new DateTime(2020, 11, 28, 18, 15, 0));                     
            Funcion funcion2 = new Funcion(pelicula1, sala1, new DateTime(2020, 11, 29, 20, 0,  0));
            Funcion funcion3 = new Funcion(pelicula2, sala1, new DateTime(2020, 11, 29, 20, 0,  0));
            Funcion funcion4 = new Funcion(pelicula2, sala1, new DateTime(2020, 11, 30, 19, 30, 0));
            Funcion funcion5 = new Funcion(pelicula3, sala1, new DateTime(2020, 11, 30, 22, 0,  0));
            Funcion funcion6 = new Funcion(pelicula3, sala1, new DateTime(2020, 12,  1, 20, 0,  0));
            Funcion funcion7 = new Funcion(pelicula4, sala1, new DateTime(2020, 12,  2, 18, 0,  0));
            Funcion funcion8 = new Funcion(pelicula4, sala1, new DateTime(2020, 12,  2, 20, 0,  0));


            cine.Funciones.Add(funcion1);
            cine.Funciones.Add(funcion2);
            cine.Funciones.Add(funcion3);
            cine.Funciones.Add(funcion4);
            cine.Funciones.Add(funcion5);
            cine.Funciones.Add(funcion6);
            cine.Funciones.Add(funcion7);
            cine.Funciones.Add(funcion8);

            PantallaPrincipal(cine);
            
            

            Console.ReadLine();


        }
    }
}
