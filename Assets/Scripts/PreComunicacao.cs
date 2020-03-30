#region Bibliotecas
using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#endregion
public class PreComunicacao : MonoBehaviour
{
    #region Variáveis públicas
    public string nomeDoUsuario;
    public string data;
    public static float media;
    public Text BPM;
    public Slider sliderBPM;
    public SerialPort sp = new SerialPort("COM3", 9600);
    #endregion
    #region Variáveis privadas
    string mensagem;
    string porta;
    float soma;
    float cont;
      #endregion

    void Awake()
    {
        sp.Open();

        sliderBPM.minValue = 0;
        sliderBPM.maxValue = 200;
        
    }
    void Start()
    {
        CriaTxt();
        OpenConnection();
        foreach (string str in SerialPort.GetPortNames())
        {
            Debug.Log(string.Format("Existing COM port: {0}", str));
        }

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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            media = soma / cont;
            EscreveMedia();

            SceneManager.LoadScene("SampleScene");

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        mensagem = sp.ReadLine();
        
        BPM.text = mensagem + " batimentos por minuto";


        AppendTxt();
        Incremento();
    }
    void Incremento()
    {      
        cont++;
    }
    void LateUpdate()
    {
        sliderBPM.value = Convert.ToInt32(mensagem);
        soma += sliderBPM.value;
    }
    void CriaTxt()
    {
        string diretorio = Application.dataPath +"/" + nomeDoUsuario + "-" + data + ".txt"; // nomeDoUsuario e data são strings 
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
    void EscreveMedia()
    {

        string diretorio = Application.dataPath + "/" + "TextAsset" + ".txt"; // 
        if (File.Exists(diretorio))
        {
            File.WriteAllText(diretorio, media.ToString()); // a função "write" apaga tudo que já estava escrito, para não perder os dados mude o nome ou número
        }
    }
    void OnApplicationQuit()
    {
        sp.Close();
        media = soma / cont;
        print(media);
    }
}