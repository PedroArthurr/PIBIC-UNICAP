using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;

public class BotaoTouch : MonoBehaviour
{
    [SerializeField] string mensagem;
    [SerializeField] string porta;
    public Text BPM;

    int valor;

    public SerialPort sp = new SerialPort("COM3", 9600);
    void Awake()
    {
        sp.Open();

    }
    void Start()
    {
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
                sp.Close();
                //fechando e abrindo de novo
                sp.Open();
            }
            else
            {
                print("Porta == null");
            }
        }
    }

    void OnApplicationQuit()
    {
        sp.Close();
    }
    void Update()
    {
        Debug.Log(sp.ReadLine());
        mensagem = sp.ReadLine();


        BPM.text = " Valor :" + mensagem;


        int.Parse(mensagem);

        valor = Convert.ToInt32(mensagem);

        if( valor == 1)
        {
            AbrirPudim();
        }

    }
    void AbrirPudim()
    {
        Application.OpenURL("www.google.com");
    }
}


    