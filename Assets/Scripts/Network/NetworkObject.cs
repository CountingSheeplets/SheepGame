using System.Collections.Generic;
public class NetworkObject
{
    public string type = "";
    public string value = "";
    public Dictionary<string, string> svalues = new Dictionary<string, string>();
    public Dictionary<string, float> fvalues = new Dictionary<string, float>();
    public Dictionary<string, string> PrepairedNetworkObject(){
        Dictionary<string, string> outputDict = new Dictionary<string, string>();
        outputDict["type"] = type;
        outputDict["value"] = value;
        foreach(KeyValuePair<string, string> svalue in svalues){
            outputDict[svalue.Key] = svalue.Value;
        }
        foreach(KeyValuePair<string, float> fvalue in fvalues){
            outputDict[fvalue.Key] = fvalue.Value.ToString();
        }
        return outputDict;
    }
    public NetworkObject(string _element, string _value, Dictionary<string, float> _values){
        type =_element;
        value = _value;
        fvalues = _values;
    }
    public NetworkObject(string _element, Dictionary<string, float> _values){
        type =_element;
        fvalues = _values;
    }
    public NetworkObject(string _element, string _value, Dictionary<string, string> _values){
        type =_element;
        value = _value;
        svalues = _values;
    }
    public NetworkObject(string _element, Dictionary<string, string> _values){
        type =_element;
        svalues = _values;
    }
    public NetworkObject(string _element, string _value){
        type =_element;
        value = _value;
    }
}
