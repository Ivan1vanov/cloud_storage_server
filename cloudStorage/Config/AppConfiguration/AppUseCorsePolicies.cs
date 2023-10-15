namespace CloudStorage.Config.AppConfiguration
{
    public class AppUseCorsPolicies
    {
        public static void UseCorsPolicies(WebApplication app, ConfigurationManager config)
        {
            string[] chosenPolicies = config.GetSection("CorsePolicyConfigurations:UsedPolicies").Get<string[]>();

            foreach (string policyName in chosenPolicies)
            {
                app.UseCors(policyName);
            }
        }
    }
}
