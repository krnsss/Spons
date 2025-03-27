namespace egorDipl.Services
{
    public class AssetService
    {
        public string GetAssetPath(string asset)
        {
            return $"/{asset}"; 
        }
    }
}
