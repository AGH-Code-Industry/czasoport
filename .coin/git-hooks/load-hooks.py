# Author: Patryk Lesiak
# Date: 2023-08-01
# In case of any questions, please contact me via Discord (vlesio)

# This script copies defined hooks to user's .git/hooks folder
# Files in .git/hooks are picked up by git to perform actions on certain events
# like commit, push etc.

# THIS SCRIPT IS UTILIZED BY InitializeHooks.cs AND SHOULD NOT BE RUN MANUALLY

import os
import shutil
import datetime

for file in os.listdir("./.coin/git-hooks/hooks"):
    shutil.copyfile("./.coin/git-hooks/hooks/" + file, "./.git/hooks/" + file)
    print("Initialized: " + file)

with open("./.coin/git-hooks/lastinit", "w") as f:
    f.write(str(datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")))