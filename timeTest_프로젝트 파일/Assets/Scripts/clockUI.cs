using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clockUI : MonoBehaviour
{
    public GameObject second, minute, hour; //시침 객체
    public Text speed; 
    public InputField inputHour, inputMinute; //사용자 입력을 받는 시간 변경 객체
    public Button btnChange; //시간 변경 객체
    public ParticleSystem particleSys;  //파티클 객체

    //초기값.
    public int speedInt = 6;
    public int speedIntUI = 0;

    public float maxValue = -1000.0f;
    public float rateValue = 50.0f;
    private int hourIntUI = 0;
    private int minuteIntUI = 0;

    private void Start()
    {
        particleSys.Play();
    }

    void Update() 
    {
        //시침 애니메이션
        second.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speedInt));
        minute.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speedInt/60));
        hour.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speedInt / 600));

        //particle system과 emission제어.
        var main = particleSys.main;
        var em = particleSys.emission;
        em.rateOverTime = rateValue;
        main.startSpeed = -1000.0f;
    }
    public void TimeSpeedDown()
    {
        if(speedInt!=6)
        {
            speedInt--;
            speedIntUI = speedInt - 6;
            
        }
        speed.text = speedIntUI.ToString();
        maxValue += 100.0f;
        rateValue -= 50.0f;
    }
    public void TimeSpeedUp()
    {
        if (speedInt!=105)
        {
            speedInt++;
            speedIntUI = speedInt - 6;
        }
        speed.text = speedIntUI.ToString();
        maxValue -= 100.0f;
        rateValue += 50.0f;
    }

   
    public void InputFieldH()
    {
        hourIntUI = int.Parse(inputHour.text);
        if (hourIntUI > 24)
        {
            inputHour.text = "23";
        }
        if(hourIntUI < -1)
        {
            inputMinute.text = "0";
        }

    }
    public void InputFieldM()
    {
        minuteIntUI = int.Parse(inputMinute.text);
        if (minuteIntUI > 60)
        {
            inputMinute.text = "59";
        }
        if (minuteIntUI < -1)
        {
            inputMinute.text = "0";
        }

    }
    public void ChangeBtn()
    {
        hourIntUI = int.Parse(inputHour.text);
        minuteIntUI = int.Parse(inputMinute.text);
        minute.transform.localEulerAngles = new Vector3(0, 0, -minuteIntUI * 6); //360/60과 같이 연산으로 들어가야 다른 오브젝트에서 응용이 가능하다. 변수로 선언. 
        hour.transform.localEulerAngles = new Vector3(0, 0, -(hourIntUI * 30)+(minuteIntUI/2));

    }
}

//초침을 기준으로 다른 오브젝트들의 움직임을 제어할 수 있어야한다. 1초일때 분침은 몇도, 시침는 몇도로 이동한다.
//public 초침, private 분침, 시침 조절. 보안관련, 접근할 수 있는 인터페이스는 가장 적게