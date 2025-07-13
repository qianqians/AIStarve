using UnityEngine;
using UnityEngine.EventSystems;

public class DropPrefabObject : DropObject
{
    public void StartDrag(string prefabPath)
    {
        if (draggedObject != null)
        {
            Destroy(draggedObject);
        }

        var prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab != null)
        {
            draggedObject = Instantiate(prefab);
            draggedObject.transform.position = Vector3.zero;
            draggedObject.SetActive(true);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        // 可以在这里添加额外的prefab放置逻辑
    }
}