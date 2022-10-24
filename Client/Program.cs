using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.IO;
using System;
using System.Drawing.Imaging;

namespace Client
{

    public class ClientProgram
    {
        const int MAXSIZE = 32767;
        //Sends packet to destination
        private static void SendMessage(UdpClient udpClient, Byte[] sendBytes, IPEndPoint SendEP)
        {
            udpClient.Send(sendBytes, sendBytes.Length, SendEP);
        }

        private static void RecieveMessage(UdpClient udpClient, IPEndPoint RecieveEP)
        {
            byte[] RecievedData = udpClient.Receive(ref RecieveEP);
            Console.WriteLine($"Received broadcast from {RecieveEP} :");
            Console.WriteLine(Encoding.ASCII.GetString(RecievedData));
        }
        public static byte[] Color()
        {
            byte[] RGB = new byte[3];

            string[] ColorArray = new string[] { "red", "blue", "green", "black", "white" };
            byte[,] ColorValues = new byte[,] { { 255, 0, 0 },
                                                { 0, 0, 255 },
                                                { 0, 255, 0 },
                                                {0,  0,   0 },
                                                { 255, 255, 255 } };

            string ccode = "";
            while (true)
            {
                Console.WriteLine("Input color, for available colors input colors: ");
                string input = Console.ReadLine();
                if (input == "colors")
                {
                    for (int i = 0; i < 5; i++)
                    {
                        ccode = ccode + ColorArray[i] + " ";
                    }
                    Console.WriteLine("Available colors are: " + ccode);
                    continue;
                }



                for (int i = 0; i < ColorArray.Length; i++)
                {
                    if (ColorArray[i] == input)
                    {
                        RGB[0] = ColorValues[i, 0];
                        RGB[1] = ColorValues[i, 1];
                        RGB[2] = ColorValues[i, 2];
                        return RGB;

                    }
                }
                Console.WriteLine("Error, such color does not exist!");

            }
        }
        //Command Methods
        public static byte[] ClearDisplay(byte command, byte[] RGB)
        {
            byte[] output = null;List<byte> sendBytesList;
            sendBytesList = new List<byte>();
            sendBytesList.Add(command);
            sendBytesList.AddRange(RGB);
            output = sendBytesList.ToArray();

            return output;
        }

        public static byte[] ThreeVarsConverter(byte command, Int16 x1, Int16 y1, byte[] RGB)
        {
            byte[] output = null;List<byte> sendBytesList;
            sendBytesList = new List<byte>();
            sendBytesList.Add(command);
            sendBytesList.AddRange(BitConverter.GetBytes(x1));
            sendBytesList.AddRange(BitConverter.GetBytes(y1));
            sendBytesList.AddRange(RGB);
            output = sendBytesList.ToArray();

            return output;
        }
        public static byte[] FiveVarsConverter(byte command, Int16 var1, Int16 var2, Int16 var3, Int16 var4, byte[] RGB)
        {
            byte[] output = null;List<byte> sendBytesList;
            sendBytesList = new List<byte>();
            sendBytesList.Add(command);
            sendBytesList.AddRange(BitConverter.GetBytes(var1));
            sendBytesList.AddRange(BitConverter.GetBytes(var2));
            sendBytesList.AddRange(BitConverter.GetBytes(var3));
            sendBytesList.AddRange(BitConverter.GetBytes(var4));
            sendBytesList.AddRange(RGB);
            output = sendBytesList.ToArray();

            return output;
        }

        public static byte[] CircleConverter(byte command, Int16 val1, Int16 val2, Int16 val3, byte[] RGB)
        {
            byte[] output = null; List<byte> sendBytesList;
            sendBytesList = new List<byte>();
            sendBytesList.Add(command);
            sendBytesList.AddRange(BitConverter.GetBytes(val1));
            sendBytesList.AddRange(BitConverter.GetBytes(val2));
            sendBytesList.AddRange(BitConverter.GetBytes(val3));
            sendBytesList.AddRange(RGB);
            output = sendBytesList.ToArray();


            return output;
        }


        public static byte[] TextConverter(byte command, Int16 val1, Int16 val2, Int16 val4, string text, byte[] RGB)
        {
            List<byte> sendBytesList; byte[] output;
            sendBytesList = new List<byte>();
            sendBytesList.Add(command);
            sendBytesList.AddRange(BitConverter.GetBytes(val1));
            sendBytesList.AddRange(BitConverter.GetBytes(val2));
            sendBytesList.AddRange(RGB);
            sendBytesList.AddRange(BitConverter.GetBytes(val4));
            sendBytesList.AddRange(BitConverter.GetBytes(Convert.ToInt16(Encoding.ASCII.GetBytes(text).Length)));
            sendBytesList.AddRange(Encoding.ASCII.GetBytes(text));
            output = sendBytesList.ToArray();
            return output;
        }

        public static byte[] RoundedRectangleConverter(byte command, Int16 val1, Int16 val2, Int16 val3, Int16 val4, Int16 val5, byte[] RGB)
        {
            byte[] output = null;  List<byte> sendBytesList;
            sendBytesList = new List<byte>();
            sendBytesList.Add(command);
            sendBytesList.AddRange(BitConverter.GetBytes(val1));
            sendBytesList.AddRange(BitConverter.GetBytes(val2));
            sendBytesList.AddRange(BitConverter.GetBytes(val3));
            sendBytesList.AddRange(BitConverter.GetBytes(val4));
            sendBytesList.AddRange(BitConverter.GetBytes(val5));
            sendBytesList.AddRange(RGB);
            output = sendBytesList.ToArray();

            return output;
        }

        public static byte[] ImageConverter(byte command, Int16 val1, Int16 val2, string path)
        {
            byte[] output = null; List<byte> sendBytesList; Bitmap map; Color color;
            map = new Bitmap(path);
            sendBytesList = new List<byte>();
            sendBytesList.Add(command);
            sendBytesList.AddRange(BitConverter.GetBytes(val1));
            sendBytesList.AddRange(BitConverter.GetBytes(val2));
            sendBytesList.AddRange(BitConverter.GetBytes(Convert.ToInt16(map.Width)));
            sendBytesList.AddRange(BitConverter.GetBytes(Convert.ToInt16(map.Height)));
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    color = map.GetPixel(i, j);
                    sendBytesList.Add(color.R);
                    sendBytesList.Add(color.G);
                    sendBytesList.Add(color.B);
                }
            }
            output = sendBytesList.ToArray();

            return output;
        }
        public static byte[] OrientationConverter(byte command, Int16 rotation)
        {
            byte[] output = null; List<byte> sendBytesList;
            sendBytesList = new List<byte>();
            sendBytesList.Add(command);
            sendBytesList.AddRange(BitConverter.GetBytes(rotation));
            output = sendBytesList.ToArray();
            return output;
        }

        public static byte[] SetWindowSize(Int16 width, Int16 height)
        {
            byte[] output = null; List<byte> sendBytesList;
            sendBytesList = new List<byte>();
            sendBytesList.Add(254);
            sendBytesList.AddRange(BitConverter.GetBytes(width));
            sendBytesList.AddRange(BitConverter.GetBytes(height));
            output = sendBytesList.ToArray();
            return output;
        }

        public static Int16 NumberInputCheckAny(int size)
        {
            int input;
            Int16 output;
            while (true)
            {
                Console.WriteLine($"Min Number: -{size}, Max Number: {size}");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Error, not a number!");
                    continue;
                }

                if (input > size)
                {
                    Console.WriteLine("Error, number is too big");
                    continue;
                }
                else if (input < -size)
                {
                    Console.WriteLine("Error, number is too small");
                    continue;
                }
                else
                {
                    output = Convert.ToInt16(input);
                    return output;
                }

            }
        }

        public static Int16 NumberInputCheckOnlyPositive()
        {
            int input;
            Int16 output;
            while (true)
            {
                Console.WriteLine("Only positive numbers, Max Number: 32767");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Error, not a number!");
                    continue;
                }

                if (input > 32767)
                {
                    Console.WriteLine("Error, number is too big");
                    continue;
                }
                else if (input < 1)
                {
                    Console.WriteLine("Error, number is not positive");
                    continue;
                }
                else
                {
                    output = Convert.ToInt16(input);
                    return output;
                }

            }
        }
 
        static void Main(string[] args)
        {

            //Arguments 
            byte command; Int16 val1, val2, val3, val4, val5, port; string imgname, text; byte[] sendBytes, RGB; string IpAddr; Int16 width = 0, height = 0;
            Console.WriteLine("Client Begin");

            UdpClient udpClient = new UdpClient(0);
            Console.WriteLine("Enter Ip Address of the server (Format is x.x.x.x): ");
            IpAddr = Console.ReadLine();
            Console.WriteLine("Enter the port of the server: ");
            port = Convert.ToInt16(Console.ReadLine());
            IPAddress serverAddr = IPAddress.Parse(IpAddr);
            IPEndPoint SendEP = new IPEndPoint(serverAddr, port);
            Console.WriteLine($"Input the size of window, max number: {MAXSIZE}, min number -{MAXSIZE}:");
            while (width == 0)
            {

                Console.WriteLine("Width:");
                try
                {
                    width = Convert.ToInt16(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Error, not a number!");
                    continue;
                }
                if(width > MAXSIZE || width < -MAXSIZE)
                {
                    Console.WriteLine("Error, number too big/small!");
                        width = 0;
                }
                
                

            }
            while(height == 0)
            {
                Console.WriteLine("Height:");
                try
                {
                    height = Convert.ToInt16(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Error, not a number!");
                    continue;
                }
                if (height > MAXSIZE || height < -MAXSIZE)
                {
                    Console.WriteLine("Error, number too big/small!");
                    height = 0;
                }
            }
            sendBytes = SetWindowSize(width, height);
            SendMessage(udpClient, sendBytes, SendEP);
            RecieveMessage(udpClient, SendEP);
            while (true)
            {
                Console.WriteLine("Input the command you wish to send to the server, to view the commands enter help, to exit enter exit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "help":
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("clear display; draw pixel; draw line; draw rectangle; fill rectangle; draw ellipse; fill ellipse; draw circle; fill circle;" +
                            "draw rounded rectangle; fill rounded rectangle; draw text; draw image; set orientation; get width; get height ");
                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    case "clear display":
                        command = 1;
                        Console.WriteLine("Clearing display");
                        RGB = Color();
                        sendBytes = ClearDisplay(command, RGB);
                        
                        SendMessage(udpClient, sendBytes, SendEP);//send command;
                        RecieveMessage(udpClient, SendEP);
                        break;


                    case "draw pixel":
                        command = 2;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        RGB = Color();
                        sendBytes = ThreeVarsConverter(command, val1, val2, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;


                    case "draw line":
                        command = 3;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input x2: ");
                        val3 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y2: ");
                        val4 = NumberInputCheckAny(MAXSIZE);
                        RGB = Color();
                        sendBytes = FiveVarsConverter(command, val1, val2, val3, val4, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;

                    case "draw rectangle":
                        command = 4;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input width: ");
                        val3 = NumberInputCheckOnlyPositive();
                        Console.WriteLine("Input height: ");
                        val4 = NumberInputCheckOnlyPositive();
                        RGB = Color();
                        sendBytes = FiveVarsConverter(command, val1, val2, val3, val4, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;

                    case "fill rectangle":
                        command = 5;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input width: ");
                        val3 = NumberInputCheckOnlyPositive();
                        Console.WriteLine("Input height: ");
                        val4 = NumberInputCheckOnlyPositive();
                        RGB = Color();
                        sendBytes = FiveVarsConverter(command, val1, val2, val3, val4, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;

                    case "draw ellipse":
                        command = 6;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input radius x: ");
                        val3 = NumberInputCheckOnlyPositive();
                        Console.WriteLine("Input radius y: ");
                        val4 = NumberInputCheckOnlyPositive();
                        RGB = Color();
                        sendBytes = FiveVarsConverter(command, val1, val2, val3, val4, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;
                    case "fill ellipse":
                        command = 7;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input radius x: ");
                        val3 = NumberInputCheckOnlyPositive();
                        Console.WriteLine("Input radius y: ");
                        val4 = NumberInputCheckOnlyPositive();
                        RGB = Color();
                        sendBytes = FiveVarsConverter(command, val1, val2, val3, val4, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;

                    case "draw circle":
                        command = 8;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input radius: ");
                        val3 = NumberInputCheckOnlyPositive();
                        RGB = Color();
                        sendBytes = CircleConverter(command, val1, val2, val3, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;

                    case "fill circle":
                        command = 9;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input radius: ");
                        val3 = NumberInputCheckOnlyPositive();
                        RGB = Color();
                        sendBytes = CircleConverter(command, val1, val2, val3, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;

                    case "draw rounded rectangle":
                        command = 10;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input width: ");
                        val3 = NumberInputCheckOnlyPositive();
                        Console.WriteLine("Input height: ");
                        val4 = NumberInputCheckOnlyPositive();
                        Console.WriteLine("Input radius: ");
                        val5 = NumberInputCheckOnlyPositive();
                        RGB = Color();
                        sendBytes = RoundedRectangleConverter(command, val1, val2, val3, val4, val5, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;



                    case "fill rounded rectangle":
                        command = 11;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input width: ");
                        val3 = NumberInputCheckOnlyPositive();
                        Console.WriteLine("Input height: ");
                        val4 = NumberInputCheckOnlyPositive();
                        Console.WriteLine("Input radius: ");
                        val5 = NumberInputCheckOnlyPositive();
                        RGB = Color();
                        sendBytes = RoundedRectangleConverter(command, val1, val2, val3, val4, val5, RGB);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;

                    case "draw text":
                        command = 12;
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);

                        Console.WriteLine("Input font size: ");
                        val3 = NumberInputCheckOnlyPositive(); ;
                        Console.WriteLine("Input text:\b ");
                        text = Console.ReadLine();
                        RGB = Color();
                        sendBytes = TextConverter(command, val1, val2, val3, text, RGB);

                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;

                    case "draw image":
                        string imgpath;
                        command = 13;
                        Console.WriteLine("Input image path with file extension(img must be in desktop)");
                        imgname = Console.ReadLine();
                        Console.WriteLine("Input x1: ");
                        val1 = NumberInputCheckAny(MAXSIZE);
                        Console.WriteLine("Input y1: ");
                        val2 = NumberInputCheckAny(MAXSIZE);
                        imgpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), imgname);
                        sendBytes = ImageConverter(command, val1, val2, imgpath);
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;
                    case "set orientation":
                        command = 14;
                        while(true)
                        {
                            try
                            {
                                Console.WriteLine("Choose orientation(In degrees):");
                                Int16 choice = Convert.ToInt16(Console.ReadLine());
                                sendBytes = OrientationConverter(command, choice);
                                SendMessage(udpClient, sendBytes, SendEP);
                                RecieveMessage(udpClient, SendEP);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Incorrect input, try again");
                                continue;
                            }

                        }
                        break;
                    case "get width":
                            command = 15;
                        sendBytes = new byte[] { 15 };
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);


                        break;

                    case "get height":
                        command = 16    ;
                        sendBytes = new byte[] { 16 };
                        SendMessage(udpClient, sendBytes, SendEP);
                        RecieveMessage(udpClient, SendEP);
                        break;


                    default:
                        Console.WriteLine("Error, command unrecognized, try again");
                        break;


                }

            }

        }
    }
}
