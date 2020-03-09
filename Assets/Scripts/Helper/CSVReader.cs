using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CSVReader : Singleton<CSVReader> {
    List<KingItem> hats = new List<KingItem>();
    List<KingItem> scepters = new List<KingItem>();
    void Awake() {
        Debug.Log("Read the CSV file");
        TextAsset DataCSV = Resources.Load<TextAsset>("RewardData");
        string[] line = DataCSV.text.Split(new char[] { '\n' });

        for (int i = 0; i < line.Length; i++) {
            string[] part = line[i].Split(new char[] { ',' });

            KingItem kingItem = new KingItem();
            kingItem.spriteName = part[0];
            int.TryParse(part[1], out kingItem.crownRequirement);
            bool.TryParse(part[2], out kingItem.premiumRequirement);
            string itemType = part[3];
            kingItem.itemName = part[4];

            if (itemType == "hat")
                hats.Add(kingItem);
            if (itemType == "scepter")
                scepters.Add(kingItem);
        }

        Debug.Log("CSVReader hats Count : " + hats.Count);
        Debug.Log("CSVReader scepters Count : " + scepters.Count);
        /*         foreach (KingItem kingItem in items) {
                    //Debug.Log("Nom: " + kingItem.Nom + ", Pays: " + kingItem.Pays + ", Superficie: " + kingItem.Superficie + ", Population: " + kingItem.Population + ", C-Population: " + kingItem.CroissancePopulation + ", PIB: " + kingItem.PIB + ", C-PIB: " + kingItem.CroissancePIB + ", Langue: " + kingItem.Langue + ", Religion: " + kingItem.Religion);
                } */
    }
    public static List<KingItem> GetHats() {
        return Instance.hats;
    }
    public static List<KingItem> GetScepters() {
        return Instance.scepters;
    }
}