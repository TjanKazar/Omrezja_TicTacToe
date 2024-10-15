using System.Net;
using System.Net.Sockets;
using System.Text;

[STAThread]
static void Main()
{
}

// ustvarjanje socketa za strežnik

public class Delo
{
    public static void Send(string sporocilo, NetworkStream stream, string tipSporočila)
    {
        try
        {
            byte[] data = new byte[1024];
            string msg = toProtocol(sporocilo, tipSporočila);
            data = Encoding.UTF8.GetBytes(msg);
            stream.Write(data, 0, data.Length);
            Console.WriteLine("sent : " + msg);
        }
        catch (Exception)
        {
            Console.WriteLine("Strežnik : Pošiljanje ni bilo uspešno!");
        }
    }
    public static string[] Recv(NetworkStream stream)
    {
        try
        {
            byte[] data = new byte[1024];
            int stBytov = stream.Read(data, 0, data.Length);
            Console.WriteLine(stBytov);
            byte[] resized = new byte[stBytov];
            foreach (byte b in data)
            {
                if (b == 0)
                {

                }
                else
                    Console.Write(((char)b));
            }
            string message = Encoding.UTF8.GetString(data);
            Console.WriteLine($"bytes to string : {message}");
            string[] parsed = new string[2];
            parsed[0] = Parser(message)[0];
            parsed[1] = Parser(message)[1];
            return parsed;
        }
        catch (Exception e)
        {
            throw;
        }

    }
    public static string[] Parser(string raw)
    {
        string[] headerPayload = raw.Split('|', 2);
        string[] parsed = new string[2];
        try
        {
            string? glava = null;
            string? vsebina = null;
            if (headerPayload.Length != 2)
            {
            }
            else
            {
                glava = headerPayload[0];
                vsebina = headerPayload[1];
            }
            if (glava == "#U")
            {
                parsed[0] = "U";
                parsed[1] = vsebina;
                return parsed;
            }
            if (glava == "#R")
            {
                parsed[0] = "R";
                parsed[1] = vsebina;
                return parsed;
            }
            if (glava == "#M")
            {
                parsed[0] = "M";
                parsed[1] = vsebina;
                return parsed;
            }
            if (glava == "#A")
            {
                parsed[0] = "A";
                parsed[1] = vsebina;
                return parsed;
            }
            if (glava == "#B")
            {
                parsed[0] = "B";
                parsed[1] = vsebina;
                return parsed;
            }
            if (glava == "#C")
            {
                parsed[0] = "C";
                parsed[1] = vsebina;
                return parsed;
            }
            if (glava == "#D")
            {
                parsed[0] = "D";
                parsed[1] = vsebina;
                return parsed;
            }
            if (glava == "#E")
            {
                parsed[0] = "E";
                parsed[1] = vsebina;
                return parsed;
            }
            if (glava == "#F")
            {
                parsed[0] = "F";
                parsed[1] = vsebina;
                return parsed;
            }
            else
            {
                parsed[0] = "X";
                parsed[1] = null;
            }

        }
        catch (Exception)
        {
            Console.WriteLine("Napaka pri razpoznavanju sporočila");
        }
        return parsed;
    }
    public static string toProtocol(string message, string type)
    {
        type = type.ToUpper();
        string toProtocol = "#" + type + "|" + message;
        return toProtocol;
    }
    public static string ClientIp(TcpClient client)
    {
        IPEndPoint ip = (IPEndPoint)client.Client.RemoteEndPoint;
        return ip.Address.ToString();
    }
    public static int ClientPort(TcpClient client)
    {
        IPEndPoint ip = (IPEndPoint)client.Client.RemoteEndPoint;
        int port = ip.Port;
        return port;
    }
    public static bool validator(string glava)
    {
        if (glava.ToLower() == "a" || glava.ToLower() == "b" || glava.ToLower() == "c" || glava.ToLower() == "d" || glava.ToLower() == "e" || glava.ToLower() == "f")
        {
            return true;
        }
        return false;
    }
    public static string fen(string Vsebina)
    {
        string rezultat = "";
        string[] parts = Vsebina.Split(" ", 2);
        string piecePosition = parts[0];
        string actions = parts[1];
        string[] rows = piecePosition.Split("/", 8);
        if (rows.Count() != 8)
        {
            rezultat += "\n";
        }
        foreach (string row in rows)
        {
            rezultat += "\n";
            foreach (char square in row)
            {
                if (square >= '1' && square <= '8')
                {
                    int numberOfSpaces = int.Parse(square.ToString());
                    rezultat += new string(' ', numberOfSpaces);
                }
                if (square == 'r' || square == 'R' || square == 'n' || square == 'N' || square == 'b' || square == 'B' || square == 'k' || square == 'K' || square == 'q' || square == 'Q' || square == 'p' || square == 'P')
                {
                    rezultat += square;
                }
            }
        }
        rezultat += "\n";

        string[] actionParts = actions.Split(" ");
        string turnToMove = actionParts[0];
        string castlingRights = actionParts[1];
        string enPassantSquare = actionParts[2];
        string halfmoveClock = actionParts[3];
        string fullmoveNumber = actionParts[4];

        string naVrsti;
        if (turnToMove.ToLower() == "w")
            naVrsti = "Beli";
        else if (turnToMove.ToLower() == "b")
            naVrsti = "Črni";
        else
            naVrsti = "Se ne da zazbrati iz podanega zapisa";


        rezultat += $"Na vrsti za potezo: {naVrsti}\n";

        string moznostRokade = "\n";
        if (castlingRights.Contains('K'))
            moznostRokade += "beli, kraljeva stran\n";
        if (castlingRights.Contains('Q'))
            moznostRokade += "beli, damina stran\n";
        if (castlingRights.Contains('k'))
            moznostRokade += "črni, kraljeva stran\n";
        if (castlingRights.Contains('q'))
            moznostRokade += "črni, damina stran\n";

        rezultat += $"možnosti rokade: \n {moznostRokade}\n";
        rezultat += $"možnosti za en passant: {enPassantSquare} \n";
        rezultat += $"število polpotez: {halfmoveClock} \n";
        rezultat += $"število poteze: {fullmoveNumber} \n";

        return rezultat;

    }
}
}