using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private EasingFunction.Function func;
    private EasingFunction.Function func2;
    private EasingFunction.Function func3;
    public EasingFunction.Ease ease = EasingFunction.Ease.EaseInCirc;
    public EasingFunction.Ease ease2 = EasingFunction.Ease.EaseOutExpo;
    public EasingFunction.Ease ease3 = EasingFunction.Ease.Linear;

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
        gameObject.transform.position = new Vector3(func(30, 60, t), 0, 60);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, func2(0, 180, t));
        gameObject.transform.localScale = Vector3.one * func3(10, 30, t);
    }
}
