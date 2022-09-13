using System;

namespace Utilities.UI
{
    /// <summary>
    /// 该接口提供一个ControlType属性来标识控件的类别，该类库的所有自定义控件都实现该接口    
    /// </summary>
    public interface IGMControl
    {
        GMControlType ControlType { get; }
    }
}
