Console.WriteLine("Bienvenido a mi lista de Contactes");


//names, lastnames, addresses, telephones, emails, ages, bestfriend
bool runing = true;
List<int> ids = new List<int>();
Dictionary<int, string> names = new Dictionary<int, string>();
Dictionary<int, string> lastnames = new Dictionary<int, string>();
Dictionary<int, string> addresses = new Dictionary<int, string>();
Dictionary<int, string> telephones = new Dictionary<int, string>();
Dictionary<int, string> emails = new Dictionary<int, string>();
Dictionary<int, int> ages = new Dictionary<int, int>();
Dictionary<int, bool> bestFriends = new Dictionary<int, bool>();


while (runing)
{
    Console.WriteLine(@"1. Agregar Contacto     2. Ver Contactos    3. Buscar Contactos     4. Modificar Contacto   6. Eliminar Contacto    6. Salir");
    Console.WriteLine("Digite el número de la opción deseada");

    int typeOption = Convert.ToInt32(Console.ReadLine());

    switch (typeOption)
    {
        case 1:
            {
                //Console.WriteLine("Digite el nombre de la persona");
                //string name = Console.ReadLine();
                //Console.WriteLine("Digite el apellido de la persona");
                //string lastname = Console.ReadLine();
                //Console.WriteLine("Digite la dirección");
                //string address = Console.ReadLine();
                //Console.WriteLine("Digite el telefono de la persona");
                //string phone = Console.ReadLine();
                //Console.WriteLine("Digite el email de la persona");
                //string email = Console.ReadLine();
                //Console.WriteLine("Digite la edad de la persona en números");
                //int age = Convert.ToInt32(Console.ReadLine());
                //Console.WriteLine("Especifique si es mejor amigo: 1. Si, 2. No");
                ////var temp = Convert.ToInt32(Console.ReadLine());
                ////bool isBestFriend;
                ////if (temp == 1)
                ////{ isBestFriend = true; }
                ////else
                ////{ isBestFriend = false; }
                //bool isBestFriend = Convert.ToInt32(Console.ReadLine()) == 1;

                //var id = ids.Count + 1;
                //ids.Add(id);
                //names.Add(id, name);
                //lastnames.Add(id, lastname);
                //addresses.Add(id, address);
                //telephones.Add(id, phone);
                //emails.Add(id, email);
                //ages.Add(id, age);
                //bestFriends.Add(id, isBestFriend);

                AddContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);

            }
            break;
        case 2: //extract this to a method
            {
                Console.WriteLine($"Nombre          Apellido            Dirección           Telefono            Email           Edad            Es Mejor Amigo?");
                Console.WriteLine($"____________________________________________________________________________________________________________________________");
                foreach (var id in ids)
                {
                    var isBestFriend  = bestFriends[id];

                    //string isBestFriendStr;
                     
                    //if (isBestFriend == true)
                    //{
                    //    isBestFriendStr = "Si";
                    //}
                    //else {
                    //    isBestFriendStr = "No";
                    //}

                    string isBestFriendStr = (isBestFriend == true) ? "Si" : "No";
                    Console.WriteLine($"{names[id]}         {lastnames[id]}         {addresses[id]}         {telephones[id]}            {emails[id]}            {ages[id]}          {isBestFriendStr}");
                }

            }
            break;
        case 3: //search
            {
                {
                    Console.WriteLine("Ingrese el nombre del contacto a buscar:");
                    string searchName = Console.ReadLine();
                    bool found = false;
                    foreach (var id in ids)
                    {
                        if (names[id].Contains(searchName, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"{names[id]} {lastnames[id]} | Dirección: {addresses[id]} | Teléfono: {telephones[id]} | Email: {emails[id]} | Edad: {ages[id]} | Mejor Amigo: {(bestFriends[id] ? "Sí" : "No")}");
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine("No se encontró ningún contacto con ese nombre.");
                    }
                }
                break;
            }
            
        case 4: //modify
            {
                {
                    Console.WriteLine("Ingrese el ID del contacto a modificar:");
                    int idToModify = Convert.ToInt32(Console.ReadLine());
                    if (ids.Contains(idToModify))
                    {
                        Console.WriteLine("Modificando datos del contacto...");
                        Console.WriteLine("Ingrese el nuevo nombre:");
                        names[idToModify] = Console.ReadLine();
                        Console.WriteLine("Ingrese el nuevo apellido:");
                        lastnames[idToModify] = Console.ReadLine();
                        Console.WriteLine("Ingrese la nueva dirección:");
                        addresses[idToModify] = Console.ReadLine();
                        Console.WriteLine("Ingrese el nuevo teléfono:");
                        telephones[idToModify] = Console.ReadLine();
                        Console.WriteLine("Ingrese el nuevo email:");
                        emails[idToModify] = Console.ReadLine();
                        Console.WriteLine("Ingrese la nueva edad:");
                        ages[idToModify] = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("¿Es el mejor amigo? 1. Sí, 2. No");
                        bestFriends[idToModify] = Convert.ToInt32(Console.ReadLine()) == 1;
                        Console.WriteLine("Contacto modificado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("ID no válido.");
                    }
                }
            }
            break;
        case 5: //delete
            {
                {
                    Console.WriteLine("Ingrese el ID del contacto a eliminar:");
                    int idToDelete = Convert.ToInt32(Console.ReadLine());
                    if (ids.Contains(idToDelete))
                    {
                        ids.Remove(idToDelete);
                        names.Remove(idToDelete);
                        lastnames.Remove(idToDelete);
                        addresses.Remove(idToDelete);
                        telephones.Remove(idToDelete);
                        emails.Remove(idToDelete);
                        ages.Remove(idToDelete);
                        bestFriends.Remove(idToDelete);
                        Console.WriteLine("Contacto eliminado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("ID no válido.");
                    }
                }
                break;

                has context menu }
            break;
        case 6:
            runing = false;
            break;
        default:
            Console.WriteLine("Tu eres o te haces el idiota?");
            break;
    }
}


static void AddContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames, Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails, Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
{
    Console.WriteLine("Digite el nombre de la persona");
    string name = Console.ReadLine();
    Console.WriteLine("Digite el apellido de la persona");
    string lastname = Console.ReadLine();
    Console.WriteLine("Digite la dirección");
    string address = Console.ReadLine();
    Console.WriteLine("Digite el telefono de la persona");
    string phone = Console.ReadLine();
    Console.WriteLine("Digite el email de la persona");
    string email = Console.ReadLine();
    Console.WriteLine("Digite la edad de la persona en números");
    int age = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Especifique si es mejor amigo: 1. Si, 2. No");

    bool isBestFriend = Convert.ToInt32(Console.ReadLine()) == 1;

    var id = ids.Count + 1;
    ids.Add(id);
    names.Add(id, name);
    lastnames.Add(id, lastname);
    addresses.Add(id, address);
    telephones.Add(id, phone);
    emails.Add(id, email);
    ages.Add(id, age);
    bestFriends.Add(id, isBestFriend);
}