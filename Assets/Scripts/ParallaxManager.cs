using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layer;
        [Range(0,1)] public float parallaxFactor;
    }
    
    public ParallaxLayer[] layers;
    
    public Transform camTransform;
    private Vector3 _lastCamPos;
    
    private void Start()
    {
        _lastCamPos = camTransform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        var cameraDelta = camTransform.position - _lastCamPos;

        foreach (var layer in layers)
        {
            var moveX = cameraDelta.x * layer.parallaxFactor;
            var moveY = cameraDelta.y * layer.parallaxFactor;

            layer.layer.position += new Vector3(moveX, moveY, 0);
        }
        
        _lastCamPos = camTransform.position;
    }
}
