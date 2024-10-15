using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

public class ClientServer
{
    public static char[] gamePosition = { '_', '_', '_', '_', '_', '_', '_', '_', '_' };

    [STAThread]
    public static async Task ConnectToServerAsync()
    {
        try
        {
            TcpClient client = new TcpClient();
            string localHost = "127.0.0.1";
            int ServerPort = 54321;

            await client.ConnectAsync(localHost, ServerPort);
            if (client.Connected)
            {
                await Console.Out.WriteLineAsync("Client : Igralec se je povezal kot Client");
                NetworkStream stream = client.GetStream();
                bool GameOver = false;
                while (!GameOver)
                {
                    string message = new string(gamePosition);
                    await Delo.SendFromClientAsync(message, stream, client, "M");
                    // 1. recv v programu
                    string[] recv1 = await Delo.Recv(stream, client);
                    await Console.Out.WriteLineAsync("Client : server vrača : " + recv1[1]);
                }

                // 1. send v programu

                await Task.Delay(1000);
                stream.Close();
                client.Close();
            }
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync("Exception: " + e.ToString());
        }
    }
public static async Task startServerAsync()
    {
        TcpListener server = null;
        TaskCompletionSource<bool> serverStart = new TaskCompletionSource<bool>();
            int ServerPort = 54321;
            IPAddress localHost = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localHost, ServerPort);
        try
        {
            Task StartServer = Task.Run(() =>
            {
            server.Start();
            serverStart.SetResult(true);
            });

            await serverStart.Task;
            await StartServer;
            await Console.Out.WriteLineAsync("Strežnik");
            await Console.Out.WriteLineAsync("Server : Poslušam na naslovu " + localHost + ":" + ServerPort);

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                bool GameOver = false;
                int counter = 0;
                await Console.Out.WriteLineAsync("Server : igralec se je povezak kot Server (" + Delo.ClientIp(client) + ":" + Delo.ClientPort(client) + ")");

                while (!GameOver || counter < 8) { 
                        string message = new(gamePosition);
                string[] clientMessage = await Delo.RecvAsync(stream);
                await Console.Out.WriteLineAsync("Server prejel : " + clientMessage[1]);

                await Delo.SendFromServerAsync(clientMessage[1], stream, clientMessage[0]);

                    counter++;
                }

                stream.Close();
                client.Close();
                await Console.Out.WriteLineAsync("Server : Client disconnected");
            }
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync("Exception: " + e.ToString());
        }
        finally
        {
            server.Stop();
        }
    }
    public class Delo
    {
        public static async Task SendFromServerAsync(string message, NetworkStream stream, string messageType)
        {
            try
            {
                string formattedMessage = toProtocol(message, messageType);
                byte[] data = Encoding.UTF8.GetBytes(formattedMessage);
                await stream.WriteAsync(data, 0, data.Length);
                await Console.Out.WriteLineAsync("Sent: " + formattedMessage);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Server: Sending was not successful! Exception: " + ex.ToString());
            }
        }

        public static async Task<string[]> RecvAsync(NetworkStream stream)
        {
            try
            {
                byte[] data = new byte[1024];
                int bytesRead = await stream.ReadAsync(data, 0, data.Length);

                byte[] resized = new byte[bytesRead];
                Array.Copy(data, resized, bytesRead);

                string message = Encoding.UTF8.GetString(resized);
                await Console.Out.WriteLineAsync($"Bytes to string: {message}");

                string[] parsed = await ParserAsync(message);

                return parsed;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Exception in RecvAsync: {e}");
                throw;
            }
        }
        public static async Task<string[]> ParserAsync(string message)
        {
            return await Task.Run(() => Parser(message));
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
            if (glava.ToLower() == "a" || glava.ToLower() == "b" || glava.ToLower() == "c")
            {
                return true;
            }
            return false;
        }
        public static async Task SendFromClientAsync(string message, NetworkStream stream, TcpClient client, string messageType)
        {
            try
            {
                byte[] data = new byte[1024];
                string formattedMessage = toProtocol(message, messageType);
                data = Encoding.UTF8.GetBytes(formattedMessage);
                await stream.WriteAsync(data, 0, data.Length);
                await Console.Out.WriteLineAsync("Sent: " + formattedMessage);
            }
            catch (Exception)
            {
                await Console.Out.WriteLineAsync("Client: Sending was not successful!");
            }
        }
        public static async Task<string[]> Recv(NetworkStream stream, TcpClient client)
        {
            byte[] data = new byte[1024];
            int bytesRead1 = await stream.ReadAsync(data, 0, data.Length);
            string message = Encoding.UTF8.GetString(data, 0, bytesRead1);
            string[] parsed = new string[2];
            parsed[0] = Parser(message)[0];
            parsed[1] = Parser(message)[1];
            return parsed;
        }
        public static string stringToServer(string message)
        {
            return message;
        }

    }
}