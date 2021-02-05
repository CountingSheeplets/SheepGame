using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheepCoordinator : Singleton<SheepCoordinator> {
    public Dictionary<Owner, SheepStack> sheepStacks = new Dictionary<Owner, SheepStack>();

    public static void CreateStack(Owner owner) {
        if (!Instance.sheepStacks.ContainsKey(owner))
            Instance.sheepStacks.Add(owner, new SheepStack());
        else {
            Instance.sheepStacks[owner] = new SheepStack();
        }
        //Debug.Log("stack size " + Instance.sheepStacks[owner].sheepSlots.Count);
    }

    public static SheepUnit SpawnSheep(Owner owner) {
        SheepUnit sheep = SheepFactory.CreateSheep(owner, Instance.sheepStacks[owner].GetNextType());
        Instance.sheepStacks[owner].SetNewSheep(sheep);
        //SkeletonRendererController.MakeSheepActive(sheep);
        return sheep;
    }
    public static void DestroySheep(SheepUnit sheep) {
        if (Instance == null)
            return;
        Instance.sheepStacks[sheep.owner].RemoveSheep(sheep);
        SheepFactory.DestroySheep(sheep);
    }
    public static List<SheepUnit> GetSheepInField(Playfield playfield) {
        //if this seems slow, could be faster to do via playfield.GetComponentInChildren<SheepUnit>()
        List<SheepUnit> sheeps = new List<SheepUnit>();
        foreach (SheepUnit sheep in GetSheepsAll()) {
            if (sheep.currentPlayfield == playfield) {
                sheeps.Add(sheep);
            }
        }
        return sheeps;
    }

    public static List<SheepUnit> GetSheeps(Owner owner) {
        if (!Instance.sheepStacks.ContainsKey(owner))
            return new List<SheepUnit>();
        return Instance.sheepStacks[owner].GetSheeps();
    }
    public static List<SheepUnit> GetSheepsAll() {
        List<SheepUnit> output = new List<SheepUnit>(Instance.sheepStacks.SelectMany(y => y.Value.GetSheeps().ToList())); //Value.sheepSlots.Select(x => x.sheep)
        //return new List<SheepUnit>(Instance.sheepStacks.SelectMany(y => y.Value.GetSheeps().ToList())); //Value.sheepSlots.Select(x => x.sheep)
        return output;
    }
    public static void UpgradeSheep(SheepUnit sheep, SheepType newType) {
        sheep.sheepType = newType;
        Instance.sheepStacks[sheep.owner].ChangeType(sheep, newType);
    }
    public static void IncreaseStacksSize(int delta) {
        foreach (KeyValuePair<Owner, SheepStack> pair in Instance.sheepStacks) {
            pair.Value.IncreaseStackSize(delta);
        }
    }
    public class SheepSlot {
        public SheepUnit sheep = null;
        public SheepType slotType = SheepType.Base;
    }
    public class SheepStack {
        public List<SheepSlot> sheepSlots = new List<SheepSlot>();
        public int currentIndex = 0;
        public SheepStack() {
            for (int i = 0; i < ConstantsBucket.SheepSpawnCapBase; i++)
                sheepSlots.Add(new SheepSlot());
        }
        public SheepSlot GetNextSlot() {
            return sheepSlots[GetNextIndex()];
        }
        public SheepType GetNextType() {
            return GetNextSlot().slotType;
        }
        public int GetNextIndex() {
            List<bool> empties = GetEmpties();

            int thisIndex = currentIndex;
            for (int i = 0; i < sheepSlots.Count; i++) {
                thisIndex++;
                if (thisIndex + 1 > sheepSlots.Count)
                    thisIndex = 0;

                if (empties[thisIndex])
                    return thisIndex;
            }
            return 999;
        }
        public void SetNewSheep(SheepUnit newSheep) {
            int index = GetNextIndex();
            sheepSlots[index].sheep = newSheep;
            currentIndex = index;
            if (currentIndex >= sheepSlots.Count)
                currentIndex = 0;
        }
        public List<SheepUnit> GetSheeps() {
            return new List<SheepUnit>(sheepSlots.Select(x => x.sheep).Where(x => x != null).ToList());
        }
        public SheepSlot GetSlot(SheepUnit sheep) {
            return sheepSlots.Where(x => x.sheep == sheep).FirstOrDefault();
        }
        public void RemoveSheep(SheepUnit sheep) {
            foreach (SheepSlot slot in sheepSlots) {
                if (slot.sheep == sheep)
                    slot.sheep = null;
            }
        }
        public void ChangeType(SheepUnit sheep, SheepType newType) {
            GetSlot(sheep).slotType = newType;
        }
        public void IncreaseStackSize(int delta) {
            for (int i = 0; i < delta; i++)
                sheepSlots.Add(new SheepSlot());
        }
        public List<bool> GetEmpties() {
            return sheepSlots.Select(x => x.sheep == null).ToList();
        }
    }
}