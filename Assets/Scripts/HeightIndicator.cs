using UnityEngine;

public class HeightIndicator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    public void ChangePosition(int yPos)
    {
        _spriteRenderer.enabled = true;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
