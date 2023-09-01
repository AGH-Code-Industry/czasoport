using System.Collections.Generic;
using CoinPackage.Debugging;

namespace Application {
    public static class Loggers {
        public static Dictionary<string, CLogger> LoggersList;

        static Loggers() {
            LoggersList = new Dictionary<string, CLogger>();
            
            // Logger for platform (level) changing system
            LoggersList.Add(
                "LEVEL_SYSTEM",
                new CLogger("LEVEL_SYSTEM") {
                    LogEnabled = false
                }
                );
            
            // Logger for Inventory system
            LoggersList.Add(
                "INVENTORY",
                new CLogger("INVENTORY") {
                    LogEnabled = true
                }
            );
        }
    }
}