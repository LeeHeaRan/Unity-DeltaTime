using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clockUI : MonoBehaviour
{
    public GameObject second, minute, hour; //��ħ ��ü
    public Text speed; 
    public InputField inputHour, inputMinute; //����� �Է��� �޴� �ð� ���� ��ü
    public Button btnChange; //�ð� ���� ��ü
    public ParticleSystem particleSys;  //��ƼŬ ��ü

    //�ʱⰪ.
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
        //��ħ �ִϸ��̼�
        second.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speedInt));
        minute.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speedInt/60));
        hour.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speedInt / 600));

        //particle system�� emission����.
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
        minute.transform.localEulerAngles = new Vector3(0, 0, -minuteIntUI * 6); //360/60�� ���� �������� ���� �ٸ� ������Ʈ���� ������ �����ϴ�. ������ ����. 
        hour.transform.localEulerAngles = new Vector3(0, 0, -(hourIntUI * 30)+(minuteIntUI/2));

    }
}

//��ħ�� �������� �ٸ� ������Ʈ���� �������� ������ �� �־���Ѵ�. 1���϶� ��ħ�� �, ��ħ�� ��� �̵��Ѵ�.
//public ��ħ, private ��ħ, ��ħ ����. ���Ȱ���, ������ �� �ִ� �������̽��� ���� ����