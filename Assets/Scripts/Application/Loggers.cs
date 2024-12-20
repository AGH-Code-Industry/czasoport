using System.Collections.Generic;
using CoinPackage.Debugging;

namespace Application {
    public static class Loggers {
        public static Dictionary<LoggerType, CLogger> LoggersList;

        public enum LoggerType {
            LEVEL_SYSTEM,
            INVENTORY,
            INTERACTIONS,
            INTERACTABLE_OBJECTS,
            ITEMS,
            PORTALS,
            PAUSE,
            DATA_PERSISTENCE,
            UTILS,
            DIALOGUES
        }

        static Loggers() {
            LoggersList = new Dictionary<LoggerType, CLogger>();

            // Logger for platform (level) changing system
            LoggersList.Add(
                LoggerType.DATA_PERSISTENCE,
                new CLogger(LoggerType.DATA_PERSISTENCE) {
                    LogEnabled = true
                }
            );

            // Logger for platform (level) changing system
            LoggersList.Add(
                LoggerType.LEVEL_SYSTEM,
                new CLogger(LoggerType.LEVEL_SYSTEM) {
                    LogEnabled = true
                }
                );

            // Logger for platform (level) changing system
            LoggersList.Add(
                LoggerType.PORTALS,
                new CLogger(LoggerType.PORTALS) {
                    LogEnabled = true
                }
            );

            // Logger for Inventory system
            LoggersList.Add(
                LoggerType.INVENTORY,
                new CLogger(LoggerType.INVENTORY) {
                    LogEnabled = true
                }
            );

            // Logger for Interactions system
            LoggersList.Add(
                LoggerType.INTERACTIONS,
                new CLogger(LoggerType.INTERACTIONS) {
                    LogEnabled = true
                }
            );

            // Logger for interactable objects
            LoggersList.Add(
                LoggerType.INTERACTABLE_OBJECTS,
                new CLogger(LoggerType.INTERACTABLE_OBJECTS) {
                    LogEnabled = true
                }
            );

            // Logger for item objects
            LoggersList.Add(
                LoggerType.ITEMS,
                new CLogger(LoggerType.ITEMS) {
                    LogEnabled = true
                }
            );

            // Logger for pause menu
            LoggersList.Add(
                LoggerType.PAUSE,
                new CLogger(LoggerType.PAUSE) {
                    LogEnabled = true
                }
            );

            // Logger for utilities
            LoggersList.Add(
                LoggerType.UTILS,
                new CLogger(LoggerType.UTILS) {
                    LogEnabled = true
                }
            );

            // Logger for dialogues
            LoggersList.Add(
                LoggerType.DIALOGUES,
                new CLogger(LoggerType.DIALOGUES) {
                    LogEnabled = true
                }
            );
        }
    }
}