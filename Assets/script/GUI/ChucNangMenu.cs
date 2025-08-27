using UnityEngine;
using UnityEngine.SceneManagement;
public class ChucNangMenu : MonoBehaviour
{
    public void ChoiNgay()
    {
        SceneManager.LoadScene(3);
    }
    public void CotTruyen() 
    {
        SceneManager.LoadScene(1);
    }
    public void HuongDanChoi()
    {
        SceneManager.LoadScene(2);
    }
    public void Thoat()
    {
        SceneManager.LoadScene(0);
        //Application.Quit();
    }
}
