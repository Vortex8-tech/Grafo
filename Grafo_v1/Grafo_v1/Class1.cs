using ComandosConsola;

namespace Grafo_v1
{
    public class Grafo
    {
        public Consola consola = new Consola("", ConsoleColor.White);

        public List<Grafo> son;
        public string key;
        public List<string> key2;
        public int repiter, rMax;
        public bool active;

        public Grafo() 
        {
            repiter = 0;
            rMax = 0;
            active = false;
            key = "";
            key2 = new List<string>();
            son = new List<Grafo>();
        }//nada
        public Grafo(string _key, int _rMax = 1)
        {
            repiter = 0;
            rMax = _rMax;
            active = true;
            key = _key;
            son = new List<Grafo>();

        }
        public Grafo(string _key, bool _active)
        {
            repiter = 0;
            rMax = 0;
            active = _active;
            key = _key;
            son = new List<Grafo>();

        }
        public Grafo(List<string> _key2, int _rMax = 1)
        {
            repiter = 0;
            rMax = _rMax;
            active = true;
            key2 = _key2;
            son = new List<Grafo>();

        }
        public Grafo(List<string> _key2, bool _active)
        {
            repiter = 0;
            rMax = 0;
            active = _active;
            key2 = _key2;
            son = new List<Grafo>();

        }

        public void Detector(string txt, int seak = 0)
        {
            if (txt.Length > 0)
            {
                bool coincide = false;
                foreach (Grafo s in son)//Obtengo c/sub nodo actual
                {
                    foreach (char k in s.key)
                    {
                        if (k == txt[seak]) { coincide = true; break; }
                    }

                    if (coincide)
                    {
                        if (s.active)//verifica si se requiere contador de repeticiones
                        {
                            s.repiter++;//en caso verdadero aumenta en 1 el contador del nodo

                            if (s.repiter > s.rMax)//verifica que no sea mayor del maximo
                            {//si esto ocurre es un error
                                consola.escribe(txt[seak].ToString(), ConsoleColor.Red);
                                Error("Numero de repeticiones mayor al indicado"); break;
                            }//si es falso el programa sigue
                        }//si no se requiere no es necesario aumentar en 1

                        bool isLast = false;
                        if (seak + 1 > txt.Length - 1)//verificmos que no haya mas caracteres
                        {//en ese caso verifico si el nodo hijo no tenga un hijo "Last"
                            foreach (Grafo s2 in s.son)
                            {
                                if (s2.key != "Last") { isLast = false; } else { isLast = true; break; }
                            }
                            //en caso de no tenerlo es error, pero si lo tiene entonces fin del grafo
                            if (!isLast)
                            {
                                consola.escribe(txt[seak].ToString(), ConsoleColor.Red);
                                Error("Cadena de texto insuficiente, se esperaba un \"Last\"");
                                break;
                            }
                            else
                            {
                                consola.escribe(txt[seak].ToString());
                                consola.escribe("\nACEPTED\n\n", ConsoleColor.Green);
                                consola.escribe("", ConsoleColor.White);
                                s.repiter = 0;
                                break;
                            }
                        }

                        consola.escribe(txt[seak].ToString());//si todo coincide se escribe el numero
                        s.Detector(txt, seak + 1);//vuelvo a llamar a la funcion, desde el nodo actual
                        s.repiter = 0;//una vez completado, se resetea el repetidor del nodo actual
                        break;
                    }

                }

                if (!coincide)//en caso de que no haya ninguna coincidencia con el carcater actual
                {
                    consola.escribe(txt[seak].ToString(), ConsoleColor.Red);
                    Error($"Caracter en la posicion [{seak}] del texto no es compatible con los sub nodos");
                }
            }
            else Error("Cadena de texto vacia");
        }
        public void Detector2(string txt, int seak = 0)
        {
            if (txt.Length > 0)
            {
                bool coincide = false; int seakAux = 0;
                foreach (Grafo s in son)//Obtengo c/sub nodo actual
                {
                    foreach (string k in s.key2)
                    {
                        for (int i = seak; i < txt.Length; i++)
                        {
                            if (k[i - seak] == txt[i]) { coincide = true; seakAux++; }
                            else { coincide = false; break; }
                            if (seakAux == k.Length) { break; }
                        }
                        if (coincide) { break; }
                    }

                    if (coincide)
                    {
                        if (s.active)//verifica si se requiere contador de repeticiones
                        {
                            s.repiter++;//en caso verdadero aumenta en 1 el contador del nodo

                            if (s.repiter > s.rMax)//verifica que no sea mayor del maximo
                            {//si esto ocurre es un error
                                for (int i = seak; i < seak + seakAux; i++) consola.escribe(txt[i].ToString(), ConsoleColor.Red);
                                for (int i = seak + seakAux; i < txt.Length; i++) consola.escribe(txt[i].ToString(), ConsoleColor.White);
                                Error("Numero de repeticiones mayor al indicado"); break;
                            }//si es falso el programa sigue
                        }//si no se requiere no es necesario aumentar en 1

                        bool isLast = false;
                        if (seak + seakAux > txt.Length - 1)//verificmos que no haya mas caracteres
                        {//en ese caso verifico si el nodo hijo no tenga un hijo "Last"
                            foreach (Grafo s2 in s.son)
                            {
                                foreach (string k in s2.key2)
                                {
                                    if (k != "Last") { isLast = false; } else { isLast = true; break; }
                                }

                            }
                            //en caso de no tenerlo es error, pero si lo tiene entonces fin del grafo
                            if (!isLast)
                            {
                                for (int i = seak; i < seak + seakAux; i++) consola.escribe(txt[i].ToString(), ConsoleColor.Red);
                                for (int i = seak + seakAux; i < txt.Length; i++) consola.escribe(txt[i].ToString(), ConsoleColor.White);
                                Error("Cadena de texto insuficiente, se esperaba un \"Last\"");
                                break;
                            }
                            else
                            {
                                for (int i = seak; i < seak + seakAux; i++) consola.escribe(txt[i].ToString());
                                consola.escribe("\nACEPTED\n\n", ConsoleColor.Green);
                                consola.escribe("", ConsoleColor.White);
                                s.repiter = 0;
                                break;
                            }
                        }

                        for (int i = seak; i < seak + seakAux; i++) consola.escribe(txt[i].ToString());//si todo coincide se escribe el numero
                        s.Detector2(txt, seak + seakAux);//vuelvo a llamar a la funcion, desde el nodo actual
                        s.repiter = 0;//una vez completado, se resetea el repetidor del nodo actual
                        break;
                    }

                }

                if (!coincide)//en caso de que no haya ninguna coincidencia con el carcater actual
                {
                    for (int i = seak; i < seak + seakAux; i++) consola.escribe(txt[i].ToString());
                    for (int i = seak + seakAux; i < txt.Length; i++) consola.escribe(txt[i].ToString(), ConsoleColor.Red);
                    Error($"Desde la posicion [{seak + seakAux}] del texto no es compatible con los sub nodos");
                }
            }
            else Error("Cadena de texto vacia");
        }
        public void Error(string razon)
        {
            consola.escribe($"\nERROR: {razon}\n", ConsoleColor.Red);
            consola.escribe("", ConsoleColor.White);
        }

        public class Conection
        {
            public Conection() { }

            public void Direction(ref List<Grafo> Grafo, string type, int n1, int n2)
            {
                Grafo n = new Grafo();

                if (type == "U")
                {
                    Grafo[n1].son.Add(Grafo[n2]);
                }
                else if (type == "B")
                {
                    Grafo[n1].son.Add(Grafo[n2]);
                    Grafo[n2].son.Add(Grafo[n1]);
                }
                else { n.Error("Direccion incorrecta, prueba con \"U\" o \"B\""); }
            }
        }
    }
}