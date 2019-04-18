using UnityEngine;
using System.Collections;


public class DañoPorTiempo : MonoBehaviour {
	public System.DateTime clock; //Sera el reloj del sistema.
	public int mes,dia,hora,minuto,segundo;
	private HealthBar accessToHealth;
	private Hunger accessToHunger;
	public int tiempoEnPerderVida=10,tiempoEnPerderHambre=173;//segundos en los que tarda en perder vida (1296) o hambre De 100hp a 0 tarda 36 horas si no se ha alimentado y de 500 a 0 de hambre tarda 24 horas

	void Start()
	{
		accessToHealth= GameObject.Find ("zombie").GetComponent<HealthBar>();
		accessToHunger= GameObject.Find ("zombie").GetComponent<Hunger>();
		clock= System.DateTime.Now;//asigno el reloj.
		mes= clock.Month;
		dia= clock.Day;
		hora= clock.Hour;
		minuto= clock.Minute;
		segundo= clock.Second;
		//solo carga el juego si no ha sido reseteado.
		//if (PlayerPrefs.GetInt("reseted")==0)
		LoadParameters();

	}

	//revisa la diferencia de horas 
	void LoadParameters()
	{
		int diasRestantesDelMes=(30*(mes - PlayerPrefs.GetInt("mesSave")));//numero de meses que pasaron * 30 dias asumiendo que cada mes tiene 30 dias
		int loseHunger, losehealth;

		//***OPERACIONES**********************************************************************************************
		//Caso1: Paso uno o mas meses
		if(mes != PlayerPrefs.GetInt("mesSave"))
		{
			dia = Mathf.Abs (dia - PlayerPrefs.GetInt("diaSave"))-diasRestantesDelMes; //los dias de los meses que pasaron son restados.
			dia= Mathf.Abs (dia);//vuelvo a sacar valor absoluto para que me de positivo el numero.
			hora = Mathf.Abs (hora - PlayerPrefs.GetInt("horaSave"));
			minuto = Mathf.Abs (minuto - PlayerPrefs.GetInt("minutoSave"));
			segundo = Mathf.Abs (segundo - PlayerPrefs.GetInt("segundoSave"));
		}else{//Caso2:estas sobre el mismo mes
				dia = Mathf.Abs (dia - PlayerPrefs.GetInt("diaSave"));
				hora = Mathf.Abs (hora - PlayerPrefs.GetInt("horaSave"));
				minuto = Mathf.Abs (minuto - PlayerPrefs.GetInt("minutoSave"));
				segundo = Mathf.Abs (segundo - PlayerPrefs.GetInt("segundoSave"));
			}
		//****************************************************************************************************************************

		//Calculamos el hambre que ha perdido durante todo ese tiempo
		loseHunger = Mathf.RoundToInt((segundo + minuto*60 + hora*3600 + dia*86400)/tiempoEnPerderHambre);//la funcion roundtoint me lo redondea a un int,
		accessToHunger.hunger -= loseHunger;//Ya resta el hambre que debe de tener la mascota.

		//Calculamos la vida Health que ha perido si su hambre es cero;
		if (accessToHunger.hunger <=0)
		{
			losehealth = Mathf.RoundToInt((segundo + minuto*60 + hora*3600 + dia*86400)/tiempoEnPerderVida);
			accessToHealth.curHealth -=losehealth;
		}

	}


}
