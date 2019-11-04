using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class ExtensionMethods
{
    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.

    //markers stuff:
    public static GameObject GetOrCreateStorableObject(this GameObject prefab, List<GameObject> storage, Transform parent)
    {
        List<GameObject> leftovers = new List<GameObject>(storage.Where(x => x.GetComponent<IMarker>().IsMarked == false).ToList());
        GameObject outputModel;
        if (leftovers.Count == 0)
        {
            GameObject newModel = GameObject.Instantiate(prefab, parent) as GameObject;
            newModel.transform.localPosition = Vector3.zero;
            storage.Add(newModel);
            outputModel = newModel;
        } else{
            outputModel = leftovers[0];
        }
        return outputModel;
    }
    public static List<GameObject> GetOrCreateStorableObjects(this GameObject prefab, List<GameObject> storage, Transform parent, int count)
    {
        //take only items from storage, which have all of their marker components not marked;
        List<GameObject> leftovers = new List<GameObject>(storage.Where(x => x.GetComponents<IMarker>().All(y => y.IsMarked == false)).ToList());
        List<GameObject> outputModels = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            if (leftovers.Count == 0)
            {
                GameObject newModel = GameObject.Instantiate(prefab, parent) as GameObject;
                newModel.transform.localPosition = Vector3.zero;
                storage.Add(newModel);
                outputModels.Add(newModel);
            } else{
                outputModels.Add(leftovers[0]);
                leftovers.RemoveAt(0);
            }
        }
        return outputModels;
    }
    public static void StoreAndUnmark(this List<GameObject> leftovers)
    {
        foreach(GameObject obj in leftovers)
        {
            StoreMarker(obj);
        }
    }
    public static void StoreMarker(this GameObject leftover){
        List<IMarker> markedMarkers = new List<IMarker>(leftover.GetComponents<IMarker>().Where(x => x.IsMarked));
        foreach(IMarker marker in markedMarkers){
            marker.UnMark();
            leftover.transform.position = Vector3.zero;
        }
    }
    //Remove a list from a list:
    public static void RemoveList<T>(this List<T> listToRemoveFrom, List<T> listToRemove)
    {
        foreach(T item in listToRemove)
        {
            listToRemoveFrom.Remove(item);
        }
    }
    //Enable/Disable a whole List:
    public static void EnableAll(MonoBehaviour[] listToRemoveFrom, bool state)
    {
        listToRemoveFrom.ToList().EnableAll(state);
    }
    public static void EnableAll(this List<MonoBehaviour> listToRemoveFrom, bool state)
    {
        foreach(MonoBehaviour item in listToRemoveFrom)
        {
            item.enabled = state;
        }
    }
    //Destroys all Children Objects which have a component of type
    public static void DestroyAllChildren(this GameObject go, System.Type type){
        List<Component> components = new List<Component>(go.GetComponentsInChildren(type));
        for(int i = components.Count-1; i >= 0;i--){
            if (components[i].gameObject != go){
                GameObject.Destroy(components[i].gameObject);
            }
        }
    }
    //other stuff
    public static Vector3Int ConvertToVector3(this Vector3 vec3)
    {
        return new Vector3Int((int)vec3.x, (int)vec3.y, (int)vec3.z);
    }
    public static int Vector3ToCellCount(this Vector3 vec3){
        Vector3Int intVec = vec3.ConvertToVector3();
        return intVec.x * intVec.y * intVec.z;
    }
    public static int Vector3ToCellCount(this Vector3Int intVec){
        return intVec.x * intVec.y * intVec.z;
    }
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }
    //Hierarchy helpers for messagins systems:
    public static bool IsSibling(this Transform trans, Transform otherTransform){
        if(trans.parent.GetComponentsInChildren<Transform>().Contains(otherTransform))
            return true;
        else return false;
    }
    public static T GetComponentInSiblings<T>(this Transform trans){
        T c = trans.parent.gameObject.GetComponentInChildren<T>();
        if (c != null)
            return c;
        else return default(T);
    }
    //collider extentions:
    public static List<Transform> GetBoundBoxColliders(this Collider myCollider, Collider[] theirColliders){
        List<Transform> collided = new List<Transform>();
        foreach (Collider theirCol in theirColliders) {
            if (theirCol.bounds.Intersects(myCollider.bounds)) {
                if (theirCol.transform.parent.gameObject != myCollider.transform.parent.gameObject && //for Targetable,
                    theirCol.gameObject != myCollider.transform.parent.gameObject){//for BuildableSpaceMarker?
                    Vector3 dir;
                    float dist;
                    if(Physics.ComputePenetration(myCollider, myCollider.transform.position, myCollider.transform.rotation,
                            theirCol, theirCol.transform.position, theirCol.transform.rotation,
                            out dir, out dist)){
                        collided.Add(theirCol.transform.parent);
                        //Debug.Log(myCollider.transform.parent.name+" adding "+theirCol.transform.parent.name);
                    }
                }
            }
        }
        return collided;
    }
    //this could be still usable somewhere, i.e. on weird shaped bullet computations, instead of raycast
/*     public static List<Transform> SweepForColliders(this Rigidbody myRigidbody, LayerMask layerMask){
        List<Transform> collided = new List<Transform>();
        RaycastHit[] hits = myRigidbody.SweepTestAll(Vector3.forward, 0.1f, QueryTriggerInteraction.UseGlobal);
        Debug.Log(myRigidbody.transform.parent.name+"hits: "+hits.Length);
        foreach (RaycastHit hit in hits) {
            if (((1<<hit.collider.gameObject.layer) & layerMask) != 0) {
                if (hit.collider.transform.parent.gameObject != myRigidbody.transform.parent.gameObject && //for Targetable,
                    hit.collider.gameObject != myRigidbody.transform.parent.gameObject){//for BuildableSpaceMarker?
                    collided.Add(hit.collider.transform.parent);
                    Debug.Log(myRigidbody.transform.parent.name+" adding "+hit.collider.transform.parent.name);
                }
            }
        }
        return collided;
    } */
/*     public static bool HasComponent<T>(this Component component) where T : Component
    {
        return component.GetComponent() != null;
    } */
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    public static OwnerType Set(this OwnerType ownerType, OwnerType[] newTypes){
        int outputType = 0;
        foreach(OwnerType type in newTypes){
            outputType |= (1 << (int)type-1);
        }
        return (OwnerType)outputType;
    }

}