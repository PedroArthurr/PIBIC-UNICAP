#region Bibliotecas
using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.IO;
using UnityEngine.UI;
#endregion
public class Comunicacao : MonoBehaviour
{
    #region Variáveis públicas
    public string nomeDoUsuario;
    public string data;
    public Text BPM;
    public Slider sliderBPM;
    public SerialPort sp = new SerialPort("COM3", 9600);
    public Text mean;

    #endregion
    #region Variáveis privadas
    string mensagem;
    string msg;
    string porta;
    [SerializeField]
    public TextAsset media;
    #endregion

    void Awake()
    {
        print(media.ToString());
        sliderBPM.minValue = 0;
        sliderBPM.maxValue = 200;
        
        mean.text = media.ToString();
        print("média é:" + mean.text);
    }
    void Start()
    {
        sp.Open();
        print(mean);
        CriaTxt();
        OpenConnection();
        
        foreach (string str in SerialPort.GetPortNames())
        {
            Debug.Log(string.Format("Existing COM port: {0}", str));
        }

        //mean.text = Convert.ToString(pc);
    }

    void OpenConnection()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                print("Porta aberta");
                sp.Close();                //fechando e abrindo de novo
                sp.Open();
            }
            else
            {
                print("Porta == null");
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Debug.Log(sp.ReadLine());
        mensagem = sp.ReadLine();
        msg = sp.ReadLine();
//--------------------------------------------------------------------------------
        BPM.text = mensagem + " batimentos por minuto"; //O que vai aparecer na UI
        AppendTxt();

       
    }
    void LateUpdate()
    {
        sliderBPM.value = Convert.ToInt32(mensagem);
    }
    void CriaTxt()
    {
        string diretorio = Application.dataPath + "/" + nomeDoUsuario + "-" + data + ".txt"; // nomeDoUsuario e data são strings 
        if (!File.Exists(diretorio))
        {
            File.WriteAllText(diretorio, "\n"); // a função "write" apaga tudo que já estava escrito, para não perder os dados mude o nome ou número
        }
        //Cria arquivo se não existir
    }
    void AppendTxt()
    {
        string diretorio = Application.dataPath + "/" + nomeDoUsuario + "-" + data + ".txt"; // nomeDoUsuario e data são strings públicas
        string valores = sp.ReadLine() + "\n"; //Add o valor e salta a linha
        File.AppendAllText(diretorio, valores);
    }

    void OnApplicationQuit()
    {
        sp.Close();
    }
}