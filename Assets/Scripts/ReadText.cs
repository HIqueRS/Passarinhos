using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

public class ReadText : MonoBehaviour
{

    public class NodeFile{
        public string title;
        public string text;

        public List<string> options = new List<string>();
    }

    public string txtpath;

    public List<NodeFile> nodesDeTexto;

    public List<TwineNodes> twineNodes;
   

    [ContextMenu("botao")]
    public void ReadTexts()
    {
        string filepath = Path.Combine(Application.dataPath,txtpath);
        char lastChar = ' ';
        char currentChar = ' ';
        nodesDeTexto = new List<NodeFile>();
        twineNodes = new List<TwineNodes>();

        string trash;

        if(File.Exists(filepath))
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                int character;
                // Lê caractere por caractere
                while ((character = reader.Read()) != -1)
                {
                    
                    lastChar = currentChar;
                    currentChar = (char)character;

                    if(currentChar == ':' && lastChar == ':')
                    {
                        NodeFile novoNode = new NodeFile();
                        TwineNodes novoTwinenode = new TwineNodes();

                        character = reader.Read();

                        ReadTitle( reader,ref novoNode);

                        trash = reader.ReadLine();
                       
                        ReadMiddleText(reader,ref novoNode);

                        Options(reader,ref novoNode);
                        
                        
                        novoTwinenode.title = novoNode.title;
                        novoTwinenode.text = novoNode.text;

                        novoTwinenode.options = new List<string>();

                        foreach(string option in novoNode.options)
                        {
                            novoTwinenode.options.Add(option);
                        }

                        twineNodes.Add(novoTwinenode);


                    }

                    
                    
                }
            }

            CreateScriptable();
        }
        else
        {
            Debug.Log("nao leu");
        }
    }

    public void CreateScriptable()
    {
        string path;
        for(int i =0 ; i < twineNodes.Count; i++)
        {
            TwineNodes data = ScriptableObject.CreateInstance<TwineNodes>();
            data = twineNodes[i];
            path = string.Concat("Assets/Resources/",data.title,".asset");
            AssetDatabase.CreateAsset(data,path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    public void Options(StreamReader reader,ref NodeFile novoNode)
    {
        bool inTheOptions = true;

        char lastChar = '[';

        int character;
        char currentChar;


        while(inTheOptions)
        {
            
            character = reader.Read();
            currentChar = (char)character;
            
            if(currentChar == ':' || reader.EndOfStream) 
            {
                inTheOptions = false;
            }

            if(currentChar == '[' && lastChar == '[')
            {
                ReadOptionUntil(reader,ref novoNode,ref currentChar);
            }

            lastChar = currentChar;


        }
    }

    private void ReadOptionUntil(StreamReader reader,ref NodeFile novoNode,ref char currentChar)
    {
        bool inAOption = true;

        string option = "";
       
        while(inAOption)
        {   
            currentChar = (char)reader.Read();

            if(currentChar == ']')
            {
                inAOption = false;
            }
            else
            {
                option += currentChar;
            }

        }

        novoNode.options.Add(option);
        Debug.Log(option);
    }

    public void ReadMiddleText(StreamReader reader,ref NodeFile novoNode)
    {
        int character;

        bool inTheMiddle = true;
        novoNode.text = "";

        while(inTheMiddle)
        {   
            character = reader.Read();
                            
            if( (char)character == '[' || (char)character == ':')
            {
                inTheMiddle = false;
            }
            else
            {
                novoNode.text +=  (char)character;
            }
        }

        Debug.Log(novoNode.text);

    }
    public void ReadTitle(StreamReader reader,ref NodeFile novoNode)
    {
        int character;

        bool isDone = false;
        novoNode.title = ""; 

       

        while(!isDone)
        {

            character = reader.Read();


            if((char)character == '\n' || (char)character == '{')
            {
                isDone = true;
            }
            else
            {
                if((char)character != ' ')
                {
                    novoNode.title += (char)character; 
                }
            }


        }

        nodesDeTexto.Add(novoNode);

        Debug.Log(novoNode.title);
    }

}
