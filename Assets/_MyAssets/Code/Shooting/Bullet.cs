using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IDisposable
{
    private const ushort timeToDispose = 10;

    private void Awake()
    {
        this.StartCoroutine(this.DelayedDispose());
    }

    public void Dispose()
    {
        Destroy(this.gameObject);
    }

    private IEnumerator DelayedDispose()
    {
        yield return new WaitForSeconds(timeToDispose);
        Destroy(this.gameObject);
    }
}
