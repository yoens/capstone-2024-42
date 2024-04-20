using System.Text.RegularExpressions;
using UnityEngine;

public class JudgmentZone : MonoBehaviour
{
    private Regex noteRegex = new Regex(@"^Note_\d+$");  // "Note_" 뒤에 하나 이상의 숫자가 오는 패턴

    void OnTriggerEnter(Collider other)
    {
        if (noteRegex.IsMatch(other.tag))
        {
            Debug.Log(other.tag + " Hit!");
            Destroy(other.gameObject);
        }
    }
}
