using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskMailru
{
    /// <summary>
    /// Интерфейс возвращающий пути к драйверу и браузеру. 
    /// </summary>
    interface IPaths
    {
        /// <summary>
        /// Google Chrome.
        /// </summary>
        string ChromeBrowserPaths { get; }

        /// <summary>
        /// ChromeDriver.
        /// </summary>
        string ChromeDriverPaths { get; }

        /// <summary>
        /// Mozilla FireFox.
        /// </summary>
        string FireFoxDriverPaths { get; }

        /// <summary>
        /// GeckoDriver.
        /// </summary>
        string FireFoxPaths { get; }
    }
}
