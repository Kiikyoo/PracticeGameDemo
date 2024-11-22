using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : SingleMonoBase<Test>
{
    // Start is called before the first frame update
    void Start()
    {
        MonoManager.INSTANCE.AddUpdateAction(Task);
    }

    void Task()
    {
        Debug.Log("update任务被执行");
    }
}
