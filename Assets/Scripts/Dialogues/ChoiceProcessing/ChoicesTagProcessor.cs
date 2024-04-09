using System;
using System.Collections.Generic;
using CoinPackage.Debugging;
using Items;
using UnityEngine;

namespace Dialogues.ChoiceProcessing {
    public static class ChoicesTagProcessor {
        private static readonly CLogger Logger =
            Application.Loggers.LoggersList[Application.Loggers.LoggerType.DIALOGUES];

        public static ChoiceContext ProcessTagsForChoice(string[] tagsString) {
            var choiceContext = new ChoiceContext();
            var tags = ProcessTags(tagsString);
            foreach (var (key, value) in tags) {
                switch (key) {
                    case "requiresItem":
                        var requiredItem = LoadResource<ItemSO>("ItemDefinitions/" + value);
                        choiceContext.RequiredItem = requiredItem;
                        break;
                    case "getsItem":
                        var getItem = LoadResource<ItemSO>("ItemDefinitions/" + value);
                        choiceContext.GetItem = getItem;
                        break;
                    default:
                        throw new Exception($"Cannot process tag {key} with value {value}");
                }
            }

            return choiceContext;
        }

        private static Dictionary<string, string> ProcessTags(string[] tagsString) {
            var dictionary = new Dictionary<string, string>();

            foreach (var tagString in tagsString) {
                string[] keyValue = tagString.Split(':', StringSplitOptions.RemoveEmptyEntries);

                if (keyValue.Length == 2) {
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();
                    dictionary[key] = value;
                }
                else {
                    Logger.LogError("Invalid tag format. Tag must be in format 'key:value'.");
                }
            }
            return dictionary;
        }

        private static T LoadResource<T>(string path) where T : UnityEngine.Object {
            var resource = Resources.Load<T>(path);
            if (resource == null) {
                throw new Exception($"Resource at {path} not found.");
            }
            return resource;
        }
    }
}