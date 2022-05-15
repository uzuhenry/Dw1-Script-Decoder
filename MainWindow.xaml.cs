using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DW_Script_decoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool fileIsLoaded = false;
        public int lastlitsboxindex = 0;
        public string fileHeaderstring;
        public struct Eventaso
        {
            public bool active;
            public int indexNo;
            public int evenType;
            public int evenArgInt;
            public string evenName;
            public string evenArg1;
            public string evenArg2;
            public string evenArg3;
            public string evenPos;
            public string[] evenStrings;

            public Eventaso(int numstrings = 0)
            {
                active = true;
                indexNo = 0;
                evenType= 0;
                evenArgInt=0;
                evenName = "";
                evenArg1 = "";
                evenArg2 = "";
                evenArg3 = "";
                evenPos = "";
                evenStrings = new string[numstrings];
            }

        }
        public List<Eventaso> eventasos = new List<Eventaso>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void botonAbrir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ".scn script files (*.scn)|*.scn";
            if (openFileDialog.ShowDialog() == true)
            {
                listaEventos.Items.Clear();
                eventasos.Clear();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding jenc = Encoding.GetEncoding(932);
                int counterEven = 0;
                bool instruccionLeida = false;
                fileIsLoaded = true;
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs, new ASCIIEncoding()))
                    {
                        
                        long finalArchivo = br.BaseStream.Length;
                       
                        byte[] chufillo;
                        string chufilla = "";
                        while(chufilla != "FFFF")
                        {
                            chufillo = br.ReadBytes(2);
                            chufilla = BitConverter.ToString(chufillo).Replace("-", string.Empty);
                            fileHeaderstring += chufilla;
                        }
                        while (br.BaseStream.Position < finalArchivo)
                        {
                            instruccionLeida = false;
                            instruccionLeida = LeerInstruccion(br,counterEven);
                            if (instruccionLeida)
                            {
                                counterEven++;
                            }
                            
                        }
                        for(int i = 0; i < eventasos.Count; i++)
                        {
                            listaEventos.Items.Add(i.ToString("000000") + " - "+ eventasos[i].evenPos + " - "+eventasos[i].evenName);
                        }
                        fileIsLoaded=true;
                        counterEven = 0;
                        supahcombobax.Items.Clear();
                        supahcombobax.Items.Add("No filter");
                        counterEven = 11;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Hide Raw Data");
                        counterEven = 12;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Raw Data");
                        counterEven = 16;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set selection");
                        counterEven = 18;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - UFO 12");
                        counterEven = 19;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Jump and Link");
                        counterEven = 21;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Jump Return");
                        counterEven = 22;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Jump");
                        counterEven = 23;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Jump to File");
                        counterEven = 24;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Switch");
                        counterEven = 25;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - IF");
                        counterEven = 26;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Show Text box");
                        counterEven = 27;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Dialog Owner");
                        counterEven = 28;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Trigger");
                        counterEven = 29;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Unset Trigger");
                        counterEven = 30;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set P Stat");
                        counterEven = 31;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Add P Stat");
                        counterEven = 32;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Reduce P Stat");
                        counterEven = 33;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Store Map ID");
                        counterEven = 34;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Store Digimon Type");
                        counterEven = 35;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Inventory Size");
                        counterEven = 36;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Store Random");
                        counterEven = 37;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Store Date");
                        counterEven = 38;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Textbox Size");
                        counterEven = 39;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Fadeout HUD");
                        counterEven = 40;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Give Item");
                        counterEven = 41;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Remove Item");
                        counterEven = 42;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Add Money");
                        counterEven = 43;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Reduce Money");
                        counterEven = 44;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Compare Date");
                        counterEven = 45;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Learn Move");
                        counterEven = 47;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Give Card");
                        counterEven = 48;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Take Card");
                        counterEven = 49;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Merit");
                        counterEven = 50;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Add Merit");
                        counterEven = 51;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Reduce Merit");
                        counterEven = 52;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Stat");
                        counterEven = 53;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Add Stat");
                        counterEven = 54;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Reduce Stat");
                        counterEven = 55;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Advance To Date At");
                        counterEven = 56;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Add Minutes to Date At");
                        counterEven = 57;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Add Minutes to Date At2");
                        counterEven = 63;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Store Digimon Value");
                        counterEven = 70;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Load Digimon");
                        counterEven = 71;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Digimon");
                        counterEven = 72;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Unload Entity");
                        counterEven = 73;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Call Digimon Routine");
                        counterEven = 74;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Wait for Entity");
                        counterEven = 75;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Warp To");
                        counterEven = 76;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Entity Look at Entity");
                        counterEven = 77;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Entity Set Rotation");
                        counterEven = 78;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Entity Walk To");
                        counterEven = 79;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Move Camera To");
                        counterEven = 80;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Move Camera To Entity");
                        counterEven = 81;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Entity Walk To Entity");
                        counterEven = 82;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Entity Walk To With Camera");
                        counterEven = 83;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Entity Walk To Entity With Camera");
                        counterEven = 84;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Patrol");
                        counterEven = 85;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Textbox Origin");
                        counterEven = 86;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Play Animation");
                        counterEven = 87;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Object Visibility");
                        counterEven = 88;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Teleport");
                        counterEven = 90;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Play Sound");
                        counterEven = 93;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set BGM");
                        counterEven = 94;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Stop BGM");
                        counterEven = 100;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Call Routine");
                        counterEven = 101;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Remove Condition");
                        counterEven = 102;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Start Battle");
                        counterEven = 103;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Delay (Wait)");
                        counterEven = 104;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Textbox Autoclose Delay");
                        counterEven = 105;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Deal Damage");
                        counterEven = 106;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set Autotalk");
                        counterEven = 107;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Unknown function 6B");
                        counterEven = 108;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Entity Move To");
                        counterEven = 112;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Rotate 3D Object");
                        counterEven = 113;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Move Object To");
                        counterEven = 114;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Entity Move To Axis");
                        counterEven = 115;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Entity Move To Axis With Camera");
                        counterEven = 116;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Spawn Item");
                        counterEven = 117;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Spawn Chest");
                        counterEven = 118;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Spawn Boulder");
                        counterEven = 119;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Move Boulder");
                        counterEven = 120;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Despawn Boulder");
                        counterEven = 121;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Unload Digimon");
                        counterEven = 122;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - UFO 7A");
                        counterEven = 123;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - UFO 7B");
                        counterEven = 124;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - UFO 7C");
                        counterEven = 126;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Spawn Sprite at Entity");
                        counterEven = 251;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - Set script");
                        counterEven = 254;
                        supahcombobax.Items.Add(counterEven.ToString("X") + " - End Section");
                    }
                }
            }
        }

        public bool LeerInstruccion(BinaryReader lector,int contador)
        {
            string offsetevento = lector.BaseStream.Position.ToString("X");
            //Console.WriteLine(offsetevento);
            int analiza = lector.ReadByte();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding jenc = Encoding.GetEncoding(932);
            bool donete = true;
            string[] offsetleido;
            string hex;
            byte[] paquito;
            string leido = "";
            switch (analiza)
            {
                case 16:    
                    paquito = lector.ReadBytes(1);
                    offsetleido = new string[paquito[0]];
                    int chuflen = paquito[0];
                    for (int i = 0; i < chuflen; i++)
                    {
                            paquito = lector.ReadBytes(2);
                            Array.Reverse(paquito);
                            offsetleido[i] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                             
                    }                            
                    eventasos.Add(new Eventaso(chuflen) {evenType = analiza, indexNo = contador, evenName = "Set selection", evenPos = offsetevento, evenStrings = offsetleido, evenArgInt = chuflen });
                    break;
                case 18:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "UFO 12", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 19:    
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() {evenType = analiza, indexNo = contador, evenName = "Jump and Link", evenPos = offsetevento, evenArg1 = leido});
                    break;
                case 21:
                    paquito = lector.ReadBytes(1);
                    eventasos.Add(new Eventaso() {evenType = analiza, indexNo = contador, evenName = "Jump Return",  evenPos = offsetevento});
                    break;
                case 22:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Jump", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 23:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[0]= BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Jump to File", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 24: //switch
                    paquito = lector.ReadBytes(1);
                    hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    int juli = paquito[1];
                    offsetleido = new string[juli];
                    for (int i = 0; i < juli; i++)
                    {
                        paquito = lector.ReadBytes(2);
                        Array.Reverse(paquito);
                        offsetleido[i]= BitConverter.ToString(paquito).Replace("-", string.Empty);
                    }
                    eventasos.Add(new Eventaso(juli) { evenType = analiza, indexNo = contador, evenName = "Switch", evenPos = offsetevento, evenStrings = offsetleido, evenArgInt=juli, evenArg1= hex});
                    break;
                case 25: //if
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    byte[] comperator;
                    bool kepplooping = true;
                    comperator = lector.ReadBytes(2);
                    Array.Reverse(comperator);
                    int buferinto = 0;
                    int comperatorid = comperator[1];
                    Array.Reverse(comperator);
                    hex = BitConverter.ToString(comperator).Replace("-", string.Empty);
                    offsetleido[0] += hex;
                    while (kepplooping)
                    {
                        switch (comperatorid)
                        {
                            case 0:
                                paquito = lector.ReadBytes(2);
                                Array.Reverse(paquito);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                break;
                            case 1:
                                paquito = lector.ReadBytes(2);
                                Array.Reverse(paquito);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                break;
                            case 32:
                                paquito = lector.ReadBytes(1);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                paquito = lector.ReadBytes(1);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                paquito = lector.ReadBytes(2);
                                Array.Reverse(paquito);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                break;
                            case 33:
                                paquito = lector.ReadBytes(1);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                paquito = lector.ReadBytes(1);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                paquito = lector.ReadBytes(2);
                                Array.Reverse(paquito);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                break;
                            case 36:
                                paquito = lector.ReadBytes(1);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                paquito = lector.ReadBytes(1);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                paquito = lector.ReadBytes(2);
                                Array.Reverse(paquito);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                break;
                            case 37:
                                paquito = lector.ReadBytes(1);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                paquito = lector.ReadBytes(4);
                                Array.Reverse(paquito);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                break;
                            default:
                                paquito = lector.ReadBytes(1);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                paquito = lector.ReadBytes(1);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                break;
                        }
                        paquito = lector.ReadBytes(2);
                        buferinto = paquito[0];
                        Array.Reverse(paquito);
                        
                        if (buferinto == 24)
                        {
                            kepplooping = false;
                            Array.Reverse(paquito);
                            offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                        }
                        else
                        {
                            
                            if(buferinto > 63 && buferinto < 127)
                            {
                                Array.Reverse(paquito);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                Array.Reverse(paquito);
                                comperatorid = buferinto - 64;
                            }
                            if(buferinto > 127)
                            {
                                Array.Reverse(paquito);
                                offsetleido[0] += BitConverter.ToString(paquito).Replace("-", string.Empty);
                                Array.Reverse(paquito);
                                comperatorid = buferinto - 128;
                            }
                            
                        }
                        //Console.WriteLine(offsetleido[0]);
                    }
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1]= BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "IF", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 26:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    
                    hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    int bufferinto = 0;
                    while (paquito.Length > 0 & hex != "0000")
                    {
                        bufferinto = BitConverter.ToUInt16(paquito, 0);
                        if (bufferinto > 10000)
                        {
                            leido+= jenc.GetString(paquito);
                        }
                        else
                        {
                            leido+="<"+ BitConverter.ToString(paquito).Replace("-", string.Empty)+">";
                            if (hex == "0D00")
                            {
                                leido += "\n";
                            }
                        }
                        paquito = lector.ReadBytes(2);
                        
                        hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    }
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Show Text Box", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 27:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Dialog Owner", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 28:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Trigger", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 29:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Unset Trigger", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 30:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set P Stat", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 31:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Add P Stat", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 32:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Reduce P Stat", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 33:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Store Map ID", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 34:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Store Digimon Type", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 35:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Inventory Size", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 36:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Store Random", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 37:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Store Date", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 38:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Textbox Size", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 39:
                    paquito = lector.ReadBytes(1);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Fadeout HUD", evenPos = offsetevento });
                    break;
                case 40:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Give Item", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 41:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Remove Item", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 42:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(4);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Add Money", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 43:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(4);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Reduce Money", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 44: //CompareDate
                    offsetleido = new string[8];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[3] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[4] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[5] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[6] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[7] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Compare Date", evenPos = offsetevento, evenStrings = offsetleido });
                    break;
                case 45:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Learn Move", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 47:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Give Card", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 48:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Take Card", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 49:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Merit", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 50:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Add Merit", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 51:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Reduce Merit", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 52:
                    paquito = lector.ReadBytes(1);
                    hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Stat", evenPos = offsetevento, evenArg1 = hex, evenArg2 = leido });
                    break;
                case 53:
                    paquito = lector.ReadBytes(1);
                    hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Add Stat", evenPos = offsetevento, evenArg1 = hex, evenArg2 = leido });
                    break;
                case 54:
                    paquito = lector.ReadBytes(1);
                    hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Reduce Stat", evenPos = offsetevento, evenArg1 = hex, evenArg2 = leido });
                    break;
                case 55:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Advance To Date At", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 56:
                    paquito = lector.ReadBytes(1);
                    hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(4);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Add Minutes to Date At", evenPos = offsetevento, evenArg1 = hex, evenArg2 = leido });
                    break;
                case 57:
                    paquito = lector.ReadBytes(1);
                    hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(4);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Add Minutes to Date At2", evenPos = offsetevento, evenArg1 = hex, evenArg2 = leido });
                    break;
                case 63:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Store Digimon Value", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 70:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Load Digimon", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 71:
                    offsetleido = new string[3];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Digimon", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1], evenArg3 = offsetleido[2] });
                    break;
                case 72:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Unload Entity", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 73:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Call Digimon Routine", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 74:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Wait for Entity", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 75:
                    offsetleido = new string[3];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Warp To", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1], evenArg3 = offsetleido[2] });
                    break;
                case 76:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Entity Look at Entity", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 77:
                    paquito = lector.ReadBytes(1);
                    hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Entity Set Rotation", evenPos = offsetevento, evenArg1 = hex, evenArg2 = leido });
                    break;
                case 78:
                    offsetleido = new string[4];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[3] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso(4) { evenType = analiza, indexNo = contador, evenName = "Entity Walk To", evenPos = offsetevento, evenStrings = offsetleido });
                    break;
                case 79:
                    offsetleido = new string[3];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Move Camera To", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1], evenArg3 = offsetleido[2] });
                    break;
                case 80:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Move Camera To Entity", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 81:
                    offsetleido = new string[3];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Entity Walk To Entity", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1], evenArg3 = offsetleido[2] });
                    break;
                case 82:
                    offsetleido = new string[4];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[3] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso(4) { evenType = analiza, indexNo = contador, evenName = "Entity Walk To With Camera", evenPos = offsetevento, evenStrings = offsetleido });
                    break;
                case 83:
                    offsetleido = new string[3];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Entity Walk To Entity With Camera", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1], evenArg3 = offsetleido[2] });
                    break;
                case 84:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Patrol", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 85:
                    offsetleido = new string[3];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Textbox Origin", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1], evenArg3 = offsetleido[2] });
                    break;
                case 86:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Play Animation", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 87:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Object Visibility", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 88:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Teleport", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 90:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Play Sound", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 93:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set BGM", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 94:
                    paquito = lector.ReadBytes(1);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Stop BGM", evenPos = offsetevento });
                    break;
                case 100:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Call Routine", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 101:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Remove Condition", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 102:
                    paquito = lector.ReadBytes(1);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Start Battle", evenPos = offsetevento });
                    break;
                case 103:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Delay (Wait)", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 104:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Textbox Autoclose Delay", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 105:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Deal Damage", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 106:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Autotalk", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 107: //Comando especial, requiere investigación
                    paquito = lector.ReadBytes(1);
                    bool chuperloop = true;
                    hex = "";
                    while (chuperloop)
                    {
                        paquito = lector.ReadBytes(1);
                        hex += BitConverter.ToString(paquito).Replace("-", string.Empty);
                        if (paquito[0] >254)
                        {
                            chuperloop = false;
                        }
                    }
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Unknown function 6B", evenPos = offsetevento, evenArg1 = hex });
                    break;
                case 108:
                    offsetleido = new string[4];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[3] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso(4) { evenType = analiza, indexNo = contador, evenName = "Entity Move To", evenPos = offsetevento, evenStrings = offsetleido });
                    break;
                case 112:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Rotate 3D Object", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 113:
                    offsetleido = new string[7];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[3] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[4] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[5] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[6] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso(7) { evenType = analiza, indexNo = contador, evenName = "Move Object To", evenPos = offsetevento, evenStrings = offsetleido });
                    break;
                case 114:
                    offsetleido = new string[4];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[3] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso(4) { evenType = analiza, indexNo = contador, evenName = "Entity Move To Axis", evenPos = offsetevento, evenStrings = offsetleido });
                    break;
                case 115:
                    offsetleido = new string[4];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(1);
                    offsetleido[3] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso(4) { evenType = analiza, indexNo = contador, evenName = "Entity Move To Axis With Camera", evenPos = offsetevento, evenStrings = offsetleido });
                    break;
                case 116:
                    offsetleido = new string[3];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Spawn Item", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1], evenArg3 = offsetleido[2] });
                    break;
                case 117:
                    offsetleido = new string[6];
                    paquito = lector.ReadBytes(1);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[3] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[4] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[5] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso(6) { evenType = analiza, indexNo = contador, evenName = "Spawn Chest", evenPos = offsetevento, evenStrings = offsetleido });
                    break;
                case 118:
                    paquito = lector.ReadBytes(1);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Spawn Boulder", evenPos = offsetevento });
                    break;
                case 119:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Move Boulder", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 120:
                    paquito = lector.ReadBytes(1);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Despawn Boulder", evenPos = offsetevento });
                    break;
                case 121:
                    paquito = lector.ReadBytes(1);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Unload Digimon", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 122:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "UFO 7A", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 123:
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "UFO 7B", evenPos = offsetevento, evenArg1 = leido });
                    break;
                case 124:
                    offsetleido = new string[4];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[2] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[3] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso(4) { evenType = analiza, indexNo = contador, evenName = "UFO 7C", evenPos = offsetevento, evenStrings = offsetleido });
                    break;
                case 126:
                    paquito = lector.ReadBytes(1);
                    hex = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    leido = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Spawn Sprite at Entity", evenPos = offsetevento, evenArg1 = hex, evenArg2 = leido });
                    break;
                case 251:
                    offsetleido = new string[2];
                    paquito = lector.ReadBytes(1);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[0] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    paquito = lector.ReadBytes(2);
                    Array.Reverse(paquito);
                    offsetleido[1] = BitConverter.ToString(paquito).Replace("-", string.Empty);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "Set Script", evenPos = offsetevento, evenArg1 = offsetleido[0], evenArg2 = offsetleido[1] });
                    break;
                case 254:
                    paquito = lector.ReadBytes(1);
                    eventasos.Add(new Eventaso() { evenType = analiza, indexNo = contador, evenName = "End Section", evenPos = offsetevento });
                    break;
                default:
                    eventasos.Add(new Eventaso() { evenType = 12, indexNo = contador, evenName = analiza.ToString("X")+" - Raw Data", evenPos = offsetevento });
                    //donete = false;
                    break;

            }
            return donete;
        }

        public string Compsigner (int entrada)
        {
            string salida="";
            switch (entrada)
            {
                case 0:
                    salida = "!=";
                    break;
                case 1:
                    salida = "==";
                    break;
                case 2:
                    salida = "<";
                    break;
                case 3:
                    salida = ">";
                    break;
                case 4:
                    salida = "<=";
                    break;
                case 5:
                    salida = ">=";
                    break;
            }
            return salida;
        }

        private void listaEventos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaEventos.SelectedIndex != -1)
            {
                lastlitsboxindex = listaEventos.SelectedIndex;
            }

            if (fileIsLoaded & (listaEventos.SelectedIndex != -1))
            {
                string aveve = listaEventos.SelectedItem.ToString();
                int chupindex = Convert.ToInt32(aveve.Substring(0,6));
                string decodedEvent = "";
                switch (eventasos[chupindex].evenType)
                {
                    case 16:
                        decodedEvent += "Set selection : \n"; 
                        for (int i = 0; i < eventasos[chupindex].evenArgInt; i++)
                        {
                            decodedEvent += "\nSelection " + (i + 1) + " - Jump to: " + eventasos[chupindex].evenStrings[i];
                        }
                        break;
                    case 18:
                        decodedEvent += "Unknown Command : " + eventasos[chupindex].evenArg1;
                        break;
                    case 19:
                        decodedEvent += "Jump to Instruction and Link call : " + eventasos[chupindex].evenArg1;
                        break;
                    case 21:
                        decodedEvent += "Return to Calling instruction.";
                        break;
                    case 22:
                        decodedEvent += "Jump to instruction : " + eventasos[chupindex].evenArg1;
                        break;
                    case 23:
                        decodedEvent += "Jump to File : " + eventasos[chupindex].evenArg1 + " at " + eventasos[chupindex].evenArg2;
                        break;
                    case 24:
                        decodedEvent += "Switch with PStat: "+ eventasos[chupindex].evenArg1;
                        for (int i = 0; i < eventasos[chupindex].evenArgInt; i++)
                        {
                            decodedEvent += "\nCase " + (i + 1) + ": - Jump to: " + eventasos[chupindex].evenStrings[i];
                        }
                        break;
                    case 25:
                        decodedEvent += "If\n\n"; //WIP
                        //decodedEvent += "" + eventasos[chupindex].evenArg1;
                        bool dalealoop = true;
                        byte[] choto = new byte[2];
                        string bufferif;
                        int polposition = 0;

                        bufferif = eventasos[chupindex].evenArg1.Substring(polposition, 2);
                        choto[0] = Convert.ToByte(bufferif, 16);
                        polposition += 2;
                        bufferif = eventasos[chupindex].evenArg1.Substring(polposition, 2);
                        choto[1] = Convert.ToByte(bufferif, 16);
                        polposition += 2;

                        while (dalealoop)
                        {
                            switch (choto[0])
                            {
                                case 0:
                                    decodedEvent += "Trigger: " + eventasos[chupindex].evenArg1.Substring(polposition, 4) + " is True ";
                                    polposition += 4;
                                    break;
                                case 1:
                                    decodedEvent += "Trigger: "+ eventasos[chupindex].evenArg1.Substring(polposition, 4) + " is False ";
                                    polposition += 4;
                                    break;
                                case 8:
                                    decodedEvent += "Pstat: " + eventasos[chupindex].evenArg1.Substring(polposition, 2) + " != ";
                                    polposition += 2;
                                    decodedEvent += eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    break;
                                case 9:
                                    decodedEvent += "Pstat: " + eventasos[chupindex].evenArg1.Substring(polposition, 2) + " == ";
                                    polposition += 2;
                                    decodedEvent += eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    break;
                                case 10:
                                    decodedEvent += "Pstat: " + eventasos[chupindex].evenArg1.Substring(polposition, 2) + " < ";
                                    polposition += 2;
                                    decodedEvent += eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    break;
                                case 11:
                                    decodedEvent += "Pstat: " + eventasos[chupindex].evenArg1.Substring(polposition, 2) + " > ";
                                    polposition += 2;
                                    decodedEvent += eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    break;
                                case 12:
                                    decodedEvent += "Pstat: " + eventasos[chupindex].evenArg1.Substring(polposition, 2) + " <= ";
                                    polposition += 2;
                                    decodedEvent += eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    break;
                                case 13:
                                    decodedEvent += "Pstat: " + eventasos[chupindex].evenArg1.Substring(polposition, 2) + " >= ";
                                    polposition += 2;
                                    decodedEvent += eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    break;
                                case 32:
                                    decodedEvent += "Stat: " + eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    bufferif = eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    choto[1] = Convert.ToByte(bufferif, 16);
                                    polposition += 2;
                                    decodedEvent += " " + Compsigner(choto[1])+" "+ eventasos[chupindex].evenArg1.Substring(polposition, 4);
                                    polposition += 4;
                                    break;
                                case 33:
                                    decodedEvent += "Card with ID: " + eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    bufferif = eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    choto[1] = Convert.ToByte(bufferif, 16);
                                    polposition += 2;
                                    decodedEvent += " " + Compsigner(choto[1]) + " " + eventasos[chupindex].evenArg1.Substring(polposition, 4);
                                    polposition += 4;
                                    break;
                                case 34:
                                    decodedEvent += "Move (attack?): " + eventasos[chupindex].evenArg1.Substring(polposition, 2) + " == ";
                                    polposition += 2;
                                    decodedEvent += eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    break;
                                case 35:
                                    decodedEvent += "Condition: " + eventasos[chupindex].evenArg1.Substring(polposition, 2) + " == ";
                                    polposition += 2;
                                    decodedEvent += eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    break;
                                case 36:
                                    decodedEvent += "Item: " + eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    polposition += 2;
                                    bufferif = eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    choto[1] = Convert.ToByte(bufferif, 16);
                                    polposition += 2;
                                    decodedEvent += " " + Compsigner(choto[1]) + " " + eventasos[chupindex].evenArg1.Substring(polposition, 4);
                                    polposition += 4;
                                    break;
                                case 37:
                                    bufferif = eventasos[chupindex].evenArg1.Substring(polposition, 2);
                                    choto[1] = Convert.ToByte(bufferif, 16);
                                    polposition += 2;
                                    decodedEvent += "Bits (Money) " + Compsigner(choto[1])+" "+ eventasos[chupindex].evenArg1.Substring(polposition, 8);
                                    polposition += 8;
                                    break;
                                default:
                                    break;

                            }

                            bufferif = eventasos[chupindex].evenArg1.Substring(polposition, 2);
                            choto[0] = Convert.ToByte(bufferif, 16);
                            polposition += 2;
                            bufferif = eventasos[chupindex].evenArg1.Substring(polposition, 2);
                            choto[1] = Convert.ToByte(bufferif, 16);
                            polposition += 2;

                            if (choto[0] == 24)
                            {
                                dalealoop = false;
                            }
                            else
                            {

                                if (choto[0] > 63 && choto[0] < 127)
                                {
                                    decodedEvent += "\nAND\n";
                                    choto[0] -= 64;
                                }
                                if (choto[0] > 127)
                                {
                                    decodedEvent += "\nOR\n";
                                    choto[0] -= 128;
                                }

                            }
                        }
                        decodedEvent += "\n\nThen Jump to: " + eventasos[chupindex].evenArg2;
                        break;
                    case 26:
                        decodedEvent += "Display text: \n\n" + eventasos[chupindex].evenArg1;
                        break;
                    case 27:
                        decodedEvent += "Set dialog owner (actorId): " + eventasos[chupindex].evenArg1;
                        break;
                    case 28:
                        decodedEvent += "Set Trigger ID: " + eventasos[chupindex].evenArg1+" to True.";
                        break;
                    case 29:
                        decodedEvent += "Set Trigger ID: " + eventasos[chupindex].evenArg1 + " to False.";
                        break;
                    case 30:
                        decodedEvent += "Set PStat: " + eventasos[chupindex].evenArg1 + " to : "+ eventasos[chupindex].evenArg2;
                        break;
                    case 31:
                        decodedEvent += "Increase PStat: " + eventasos[chupindex].evenArg1 + " by : " + eventasos[chupindex].evenArg2;
                        break;
                    case 32:
                        decodedEvent += "Decrease PStat: " + eventasos[chupindex].evenArg1 + " by : " + eventasos[chupindex].evenArg2;
                        break;
                    case 33:
                        decodedEvent += "Store MAP ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 34:
                        decodedEvent += "Store Digimon Type: " + eventasos[chupindex].evenArg1;
                        break;
                    case 35:
                        decodedEvent += "Set Inventory size to: " + eventasos[chupindex].evenArg1;
                        break;
                    case 36:
                        decodedEvent += "Store PStat: " + eventasos[chupindex].evenArg1 + " a random value between 0 and " + eventasos[chupindex].evenArg2;
                        break;
                    case 37:
                        decodedEvent += "Store Date in PStat: " + eventasos[chupindex].evenArg1;
                        break;
                    case 38:
                        decodedEvent += "Set Textbox size: \n\nWidth: " + eventasos[chupindex].evenArg1+"\nHeight: " + eventasos[chupindex].evenArg2;
                        break;
                    case 39:
                        decodedEvent += "Fadeout HUD";
                        break;
                    case 40:
                        decodedEvent += "Give Item: \n\nItem ID: " + eventasos[chupindex].evenArg1 + "\nAmount: " + eventasos[chupindex].evenArg2;
                        break;
                    case 41:
                        decodedEvent += "Remove Item: \n\nItem ID: " + eventasos[chupindex].evenArg1 + "\nAmount: " + eventasos[chupindex].evenArg2;
                        break;
                    case 42:
                        decodedEvent += "Increase Bits(Money) by: " + eventasos[chupindex].evenArg1;
                        break;
                    case 43:
                        decodedEvent += "Decrease Bits(Money) by: " + eventasos[chupindex].evenArg1;
                        break;
                    case 44:
                        decodedEvent += "Compare date stored in PStat: " + eventasos[chupindex].evenStrings[0]+ "with Mode:" + eventasos[chupindex].evenStrings[2]+"to:\n\nYear: "+ eventasos[chupindex].evenStrings[3]+"\nDay: "+ eventasos[chupindex].evenStrings[4]+"\nHour: "+ eventasos[chupindex].evenStrings[5]+"\nMinutes: "+ eventasos[chupindex].evenStrings[6]+"\nSeconds :"+ eventasos[chupindex].evenStrings[7]+"\n\nIf sucessful set Trigger: "+eventasos[chupindex].evenStrings[1]; //WIP
                        break;
                    case 45:
                        decodedEvent += "Learn Move ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 47:
                        decodedEvent += "Give Card ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 48:
                        decodedEvent += "Remove Move ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 49:
                        decodedEvent += "Set Merit to: " + eventasos[chupindex].evenArg1;
                        break;
                    case 50:
                        decodedEvent += "Increase Merit by: " + eventasos[chupindex].evenArg1;
                        break;
                    case 51:
                        decodedEvent += "Decrease Merit by: " + eventasos[chupindex].evenArg1;
                        break;
                    case 52:
                        decodedEvent += "Set Digimon partner Stat ID: " + eventasos[chupindex].evenArg1+" to " + eventasos[chupindex].evenArg2;
                        break;
                    case 53:
                        decodedEvent += "Increase Digimon partner Stat ID: " + eventasos[chupindex].evenArg1 + " by " + eventasos[chupindex].evenArg2;
                        break;
                    case 54:
                        decodedEvent += "Decrease Digimon partner Stat ID: " + eventasos[chupindex].evenArg1 + " by " + eventasos[chupindex].evenArg2;
                        break;
                    case 55:
                        decodedEvent += "Advance to date stored in PStat: " + eventasos[chupindex].evenArg1;
                        break;
                    case 56:
                        decodedEvent += "Increase minutes to date stored at PStat: "+ eventasos[chupindex].evenArg1+ " by " + eventasos[chupindex].evenArg2;
                        break;
                    case 57:
                        decodedEvent += "Increase minutes(maybe hours) to date stored at PStat: " + eventasos[chupindex].evenArg1 + " by " + eventasos[chupindex].evenArg2;
                        break;
                    case 63:
                        decodedEvent += "Store Digimon value: " + eventasos[chupindex].evenArg1 + " at Pstat: " + eventasos[chupindex].evenArg2;
                        break;
                    case 70:
                        decodedEvent += "Load Digimon with ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 71:
                        decodedEvent += "Set Digimon with following parameters: \n\nType ID: " + eventasos[chupindex].evenArg1 + "\nEntity ID: " + eventasos[chupindex].evenArg2 + "\nAutotalk: " + eventasos[chupindex].evenArg3;
                        break;
                    case 72:
                        decodedEvent += "Unload Entity with ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 73:
                        decodedEvent += "Call Digimon Routine: " + eventasos[chupindex].evenArg1;
                        break;
                    case 74:
                        decodedEvent += "Wait for Entity: " + eventasos[chupindex].evenArg1;
                        break;
                    case 75:
                        decodedEvent += "Warp to: \n\nMap ID: " + eventasos[chupindex].evenArg1 + "\nMap ID: " + eventasos[chupindex].evenArg2 + "\nTrigger: " + eventasos[chupindex].evenArg3;
                        break;
                    case 76:
                        decodedEvent += "Make Entity with ID: " + eventasos[chupindex].evenArg1 + " Look at Entity with ID: " + eventasos[chupindex].evenArg2;
                        break;
                    case 77:
                        decodedEvent += "Set Entity with ID: " + eventasos[chupindex].evenArg1 + " Rotation to: " + eventasos[chupindex].evenArg2;
                        break;
                    case 78: 
                        decodedEvent += "Make Entity with ID: " + eventasos[chupindex].evenStrings[0] + " Walk to:\n\nPos X: " + eventasos[chupindex].evenStrings[1] +"\nPos Y: " + eventasos[chupindex].evenStrings[2] +"\n Sprinting:" + eventasos[chupindex].evenStrings[3];
                        break;
                    case 79: 
                        decodedEvent += "Move camera to:\n\nPos X: " + eventasos[chupindex].evenArg2 + "\nPos Y: " + eventasos[chupindex].evenArg3 +"\nSlowdown:" + eventasos[chupindex].evenArg1;
                        break;
                    case 80: 
                        decodedEvent += "Move camera to Entity with ID: " + eventasos[chupindex].evenArg1 + " With speed " + eventasos[chupindex].evenArg2;
                        break;
                    case 81:
                        decodedEvent += "Make Entity with ID: " + eventasos[chupindex].evenArg1 + " Walk to Entity with ID: " + eventasos[chupindex].evenArg3 + " -Sprinting: " + eventasos[chupindex].evenArg2;
                        break;
                    case 82:
                        decodedEvent += "Make Entity with ID: " + eventasos[chupindex].evenStrings[0] + " Walk with Camera to:\n\nPos X: " + eventasos[chupindex].evenStrings[1] + "\nPos Y: " + eventasos[chupindex].evenStrings[2] + "\n Sprinting:" + eventasos[chupindex].evenStrings[3];
                        break;
                    case 83:
                        decodedEvent += "Make Entity with ID: " + eventasos[chupindex].evenArg1 + " Walk with Camera to Entity with ID: " + eventasos[chupindex].evenArg3 + " -Sprinting: " + eventasos[chupindex].evenArg2;
                        break;
                    case 84:
                        decodedEvent += "Unknown waypoint related instruction, argument is Entity ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 85:
                        decodedEvent += "Set Textbox Origin point:\n\nPos X:" + eventasos[chupindex].evenArg1 + "\nPos Y: " + eventasos[chupindex].evenArg2 + "\nPos Z:" + eventasos[chupindex].evenArg3;
                        break;
                    case 86:
                        decodedEvent += "Make Entity with ID: " + eventasos[chupindex].evenArg1 + " Play Animation ID: " + eventasos[chupindex].evenArg2;
                        break;
                    case 87:
                        decodedEvent += "Set Entity with ID: " + eventasos[chupindex].evenArg1 + " Visibility to " + eventasos[chupindex].evenArg2;
                        break;
                    case 88:
                        decodedEvent += "Teleport to value stored in PStat: " + eventasos[chupindex].evenArg1;
                        break;
                    case 90:
                        decodedEvent += "Play sound:\n\nSound register:" + eventasos[chupindex].evenArg1 + "\nSound ID: " + eventasos[chupindex].evenArg2;
                        break;
                    case 93:
                        decodedEvent += "Start playing BGM ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 94:
                        decodedEvent += "Stop BGM.";
                        break;
                    case 100:
                        decodedEvent += "Call Routine ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 101:
                        decodedEvent += "Remove Condition ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 102:
                        decodedEvent += "Start Battle!!!";
                        break;
                    case 103:
                        decodedEvent += "Wait for: " + eventasos[chupindex].evenArg1;
                        break;
                    case 104:
                        decodedEvent += "Set Textbox Autoclose wait to: " + eventasos[chupindex].evenArg1;
                        break;
                    case 105:
                        decodedEvent += "Deal Damage: " + eventasos[chupindex].evenArg1;
                        break;
                    case 106:
                        decodedEvent += "Set Entity with ID: " + eventasos[chupindex].evenArg1 + " Autotalk to " + eventasos[chupindex].evenArg2;
                        break;
                    case 107:
                        decodedEvent += "Unknown instruction, list of values or IDs:\n" + eventasos[chupindex].evenArg1;
                        break;
                    case 108:
                        decodedEvent += "Move Entity with ID: " + eventasos[chupindex].evenStrings[0] + " to:\n\nPos X: " + eventasos[chupindex].evenStrings[1] + "\nPos Y: " + eventasos[chupindex].evenStrings[2] + "\n Duration of animation:" + eventasos[chupindex].evenStrings[3];
                        break;
                    case 112:
                        decodedEvent += "Rotate 3D object with Model ID: " + eventasos[chupindex].evenArg1 + " - Rotation: " + eventasos[chupindex].evenArg2;
                        break;
                    case 113:
                        decodedEvent += "Move object: " + eventasos[chupindex].evenStrings[1] + "" + eventasos[chupindex].evenStrings[2] + "" + eventasos[chupindex].evenStrings[3] + "" + eventasos[chupindex].evenStrings[4] + " to:\n\nPos X: " + eventasos[chupindex].evenStrings[5] + "\nPos Y: " + eventasos[chupindex].evenStrings[6]; ;
                        break;
                    case 114:
                        decodedEvent += "Move Entity with ID: " + eventasos[chupindex].evenStrings[0] + " to:\n\nPosition: " + eventasos[chupindex].evenStrings[1] +"\nAxis: " + eventasos[chupindex].evenStrings[2] +"\nSpeed: " + eventasos[chupindex].evenStrings[3];
                        break;
                    case 115:
                        decodedEvent += "Move Entity with ID: " + eventasos[chupindex].evenStrings[0] + " with Camera to:\n\nPosition: " + eventasos[chupindex].evenStrings[1] + "\nAxis: " + eventasos[chupindex].evenStrings[2] + "\nSpeed: " + eventasos[chupindex].evenStrings[3];
                        break;
                    case 116:
                        decodedEvent += "Spawn Item with ID: " + eventasos[chupindex].evenArg1 + " at:\n\nPos X:  " + eventasos[chupindex].evenArg2+"\nPos Y: " + eventasos[chupindex].evenArg3;
                        break;
                    case 117:
                        decodedEvent += "Spawn Chest with ID: " + eventasos[chupindex].evenStrings[0] + " at:\n\nPos X:  " + eventasos[chupindex].evenStrings[1] + "\nPos Y: " + eventasos[chupindex].evenStrings[2]+"\nRotation: "+ eventasos[chupindex].evenStrings[3]+"\nTrigger: "+ eventasos[chupindex].evenStrings[4];
                        break;
                    case 118:
                        decodedEvent += "Spawn Boulder.";
                        break;
                    case 119:
                        decodedEvent += "Move Boulder:\n\nTranslate Z: " + eventasos[chupindex].evenArg1 +"\nTranslate Y: " + eventasos[chupindex].evenArg2;
                        break;
                    case 120:
                        decodedEvent += "Despawn Boulder.";
                        break;
                    case 121:
                        decodedEvent += "Unload Digimon with ID: " + eventasos[chupindex].evenArg1;
                        break;
                    case 122:
                        decodedEvent += "Unknown Instruction, value: " + eventasos[chupindex].evenArg1;
                        break;
                    case 123:
                        decodedEvent += "Unknown Instruction, value: " + eventasos[chupindex].evenArg1;
                        break;
                    case 124:
                        decodedEvent += "Unknown Instruction,\n\nValues: " + eventasos[chupindex].evenStrings[0] + " " + eventasos[chupindex].evenStrings[1] + " " + eventasos[chupindex].evenStrings[2]+" "+ eventasos[chupindex].evenStrings[3];
                        break;
                    case 126:
                        decodedEvent += "Spawn at Entity with ID: " + eventasos[chupindex].evenArg1 + " Sprite ID: " + eventasos[chupindex].evenArg2;
                        break;
                    case 251:
                        decodedEvent += "Set script, Script ID: " + eventasos[chupindex].evenArg1 + " - Map ID: " + eventasos[chupindex].evenArg2;
                        break;
                    case 254:
                        decodedEvent += "End of script section.";
                        break;
                    default:
                        break;
                }
                contenidoEvento.Text = decodedEvent;
            }
        }

        private void supahcombobax_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool wefilter = false;
            listaEventos.Items.Clear();
            int filterino = 0;
            if (supahcombobax.SelectedIndex > 0)
            {
                string indicillor = supahcombobax.SelectedItem.ToString();
                wefilter = true;
                filterino = int.Parse(indicillor.Substring(0,2), System.Globalization.NumberStyles.HexNumber);

            }
            for (int i = 0; i < eventasos.Count; i++)
            {
                if (wefilter)
                {
                    if (filterino == 11)
                    {
                        if (eventasos[i].evenType != 12)
                        {
                            listaEventos.Items.Add(i.ToString("000000") + " - " + eventasos[i].evenPos + " - " + eventasos[i].evenName);
                        }
                    }
                    else
                    {
                        if (eventasos[i].evenType == filterino)
                        {
                            listaEventos.Items.Add(i.ToString("000000") + " - " + eventasos[i].evenPos + " - " + eventasos[i].evenName);
                        }
                    }
                }
                else
                {
                    listaEventos.Items.Add(i.ToString("000000") + " - " + eventasos[i].evenPos + " - " + eventasos[i].evenName);
                }
            }
        }
    }
}
