using System.Collections.Generic;
using CoinPackage.Debugging;

namespace Application {
    public static class Loggers {
        public static Dictionary<string, CLogger> LoggersList;

        static Loggers() {
            LoggersList = new Dictionary<string, CLogger>();
            
            LoggersList.Add(
                "LEVEL_SYSTEM",
                new CLogger("LEVEL_SYSTEM") {
                    LogEnabled = true
                }
                );
        }
    }
}