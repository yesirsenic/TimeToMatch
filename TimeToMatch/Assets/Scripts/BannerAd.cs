using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
#if UNITY_ANDROID
    private string bannerId = "Banner_Android";
#elif UNITY_IOS
    private string bannerId = "Banner_iOS";
#endif

    void Start()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        LoadBanner();
    }

    void LoadBanner()
    {
        Advertisement.Banner.Load(bannerId, new BannerLoadOptions
        {
            loadCallback = ShowBanner,
            errorCallback = (error) => Debug.Log("Banner Load Error: " + error)
        });
    }

    void ShowBanner()
    {
        Advertisement.Banner.Show(bannerId);
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }
}
