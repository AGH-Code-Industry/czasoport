# Author: Patryk Lesiak
# Date: 2023-08-01
# In case of any questions, please contact me via Discord

# This script initializes all the hooks present in the .coin\hooks folder

import os
import shutil

for file in os.listdir("./hooks"):
    shutil.copyfile("./hooks/" + file, "../.git/hooks/" + file)
    print("Initialized hook: " + file)