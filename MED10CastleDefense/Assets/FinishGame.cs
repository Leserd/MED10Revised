using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour {


	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            var stateMan = StateManager.Instance;
            var dataLength = PretendData.Instance.Data.Length;
            stateMan.NewLevelComplete = true;

            int total = 0;
            foreach (var item in PretendData.Instance.Data)
            {
                total += int.Parse(item.BSDataAmount);
            }
            stateMan.YearlyExpense = -stateMan.YearlyExpense;

            stateMan.YearlyExpense = total;
            FindObjectOfType<BudgetButton>().BudgetUpdate();
            stateMan.LevelsAvailable = dataLength + 1;

            SceneManager.LoadScene(0);


        }
		
	}
}
