# Author: Patryk Lesiak
# Date: 2023-08-01
# In case of any questions, please contact me via Discord

# This script runs all scripts that start with "initialize-" in the .coin folder

import os
import shutil

for file in os.listdir("./"):
    if file.startswith("initialize-"):
        print("=== RUNNING SCRIPT: " + file + " ===")
        os.system("python " + file)
        print("\n")