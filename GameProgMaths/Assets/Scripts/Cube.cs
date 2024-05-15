using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private EasingFunction.Function func;
    private EasingFunction.Function func2;
    private EasingFunction.Function func3;
    public EasingFunction.Ease ease = EasingFunction.Ease.EaseInOutQuad;
    public EasingFunction.Ease ease2 = EasingFunction.Ease.EaseInSine;
    public EasingFunction.Ease ease3 = EasingFunction.Ease.EaseOutSine;

    float t = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        func = EasingFunction.GetEasingFunction(ease);
        func2 = EasingFunction.GetEasingFunction(ease2);
        func3 = EasingFunction.GetEasingFunction(ease3);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 1) t = 1;
        gameObject.transform.position = new Vector3(0, func(-20, 20, t), 60);
        gameObject.transform.rotation = Quaternion.Euler(func2(0, 90, t), 0, 0);
        gameObject.transform.localScale = Vector3.one * func3(0, 20, t);
    }
}
