namespace izolabella.FatSecret.NET.Classes
{
    /// <summary>
    /// 
    /// </summary>
    public class AccessTokenResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccessToken"></param>
        public AccessTokenResult(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AccessToken { get; }
    }
}
