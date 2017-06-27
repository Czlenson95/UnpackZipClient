using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnpackZipClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IPHostEntry ipHostInfo;
        IPAddress ipAddress;
        IPEndPoint remoteEP;
        Socket sendSocket;


        private void btnConnect_Click(object sender, EventArgs e)
        {
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            remoteEP = new IPEndPoint(ipAddress, 11000);
            sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sendSocket.Connect(remoteEP);
            logWindowUpdate("Socket connected to {0} " + sendSocket.RemoteEndPoint.ToString());

            /*  try
              {

                  ipHostInfo = Dns.Resolve(Dns.GetHostName());
                  ipAddress = ipHostInfo.AddressList[0];
                  remoteEP = new IPEndPoint(ipAddress, 11000);
                  sendSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);

                  // Connect the socket to the remote endpoint. Catch any errors.
                  try
                  {
                      sendSocket.Connect(remoteEP);

                      logWindowUpdate("Socket connected to {0} " + sendSocket.RemoteEndPoint.ToString());

                      // Encode the data string into a byte array.
                      // byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");
                      byte[] msg = Encoding.ASCII.GetBytes("xDDDDD/rn/rn/rn");

                      // Send the data through the socket.
                      int bytesSent = sendSocket.Send(msg);

                      // Receive the response from the remote device.
                      int bytesRec = sendSocket.Receive(bytes);
                      logWindowUpdate("Echoed test = {0} " + Encoding.ASCII.GetString(bytes, 0, bytesRec));

                      // Release the socket.
                      sendSocket.Shutdown(SocketShutdown.Both);
                      sendSocket.Close();

                  }
                  catch (ArgumentNullException ane)
                  {
                      Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                  }
                  catch (SocketException se)
                  {
                      Console.WriteLine("SocketException : {0}", se.ToString());
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine("Unexpected exception : {0}", ex.ToString());
                  }

              }
              catch (Exception ex)
              {
                  Console.WriteLine(ex.ToString());
              }*/
        }

        byte[] bytes = new byte[1024];
        string filePath;
        string text;
        Byte[] fileBytecode;
        int nextFileByte;

        public void logWindowUpdate(dynamic toLog)
        {
            lstClientInfo.Items.Add(toLog.ToString());
            lstClientInfo.Refresh();
        }


        private void btnSendFile_Click(object sender, EventArgs e)
        {
          int size = -1;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                filePath = file;
                try
                {
                    text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }
            }
       
            Console.WriteLine(size + " --- " + filePath);

            try
            {
                //byte[] msg = Encoding.ASCII.GetBytes(text + "/rn/rn/rn");
                byte[] endTab = Encoding.ASCII.GetBytes("/rn/rn/rn");
                byte[] msg = File.ReadAllBytes(filePath);

                int oldLen = msg.Length;
                Array.Resize(ref msg, msg.Length + endTab.Length);
                Array.Copy(endTab, 0, msg, oldLen, endTab.Length);


                File.WriteAllBytes("temporary.zip", msg);

                int bytesSent = sendSocket.Send(msg);

                // Receive the response from the remote device.
                int bytesRec = sendSocket.Receive(bytes);
                //logWindowUpdate("Echoed test length:  " + Encoding.ASCII.GetString(bytes, 0, bytesRec));
                string filesInZip = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                List<string> names = filesInZip.Split(';').ToList();
                Console.WriteLine("LICZBA ELEMENTOW: " + filesInZip);
                foreach (string name in names)
                {
                    lstFilesInZip.Items.Add(name);
                }
                    

                // Release the socket.
                // sendSocket.Shutdown(SocketShutdown.Both);
                // sendSocket.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception : {0}", ex.ToString());
            }


        }

        public static byte[] GetStringToBytes(string value)
        {
            SoapHexBinary shb = SoapHexBinary.Parse(value);
            return shb.Value;
        }

            string filesToUnpack = "";
            List<String> nameOfFiles = new List<String>();

        private void btnUnpack_Click(object sender, EventArgs e)
        {
            

            try
            {
                for(int i = 0; i < lstFilesInZip.SelectedIndices.Count; ++i)
                {
                    filesToUnpack += (lstFilesInZip.SelectedIndices[i] + ";");
                    nameOfFiles.Add(lstFilesInZip.SelectedItems[i].ToString());
                }

                filesToUnpack = filesToUnpack.Substring(0, filesToUnpack.Length - 1);


                  //byte[] msg = Encoding.ASCII.GetBytes(text + "/rn/rn/rn");
                  byte[] endTab = Encoding.ASCII.GetBytes("/rn/rn/rn");
                  byte[] msg = Encoding.ASCII.GetBytes("1x1p" + filesToUnpack);

                  int oldLen = msg.Length;
                  Array.Resize(ref msg, msg.Length + endTab.Length);
                  Array.Copy(endTab, 0, msg, oldLen, endTab.Length);

                  int bytesSent = sendSocket.Send(msg);
                  int bytesRec = sendSocket.Receive(bytes);
                byte[] bytesToFile = new byte[0];
                //Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRec));
                int l = 0;
                List<string> byteCodes = Encoding.ASCII.GetString(bytes, 0, bytesRec).Split(';').ToList();
                foreach(string code in byteCodes)
                {
                    bytesToFile = GetStringToBytes(code);
                    File.WriteAllBytes(nameOfFiles[l], bytesToFile);
                    l++;
                }

                foreach (string code in byteCodes)
                {
                    bytesToFile = GetStringToBytes(code);
                    Console.WriteLine(code);
                    l++;
                }
                //logWindowUpdate("Echoed test length:  " + Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.
                // sendSocket.Shutdown(SocketShutdown.Both);
                // sendSocket.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception : {0}", ex.ToString());
            }

        }
    }
}
