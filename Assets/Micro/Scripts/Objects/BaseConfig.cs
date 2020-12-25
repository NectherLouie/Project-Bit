
namespace Micro
{
    public class BaseConfig
    {
        public virtual BaseConfig Clone()
        {
            return new BaseConfig();
        }
    }
}