# Author: Patryk Lesiak
# Date: 2023-08-01
# In case of any questions, please contact me via Discord

# This script runs all scripts that start with "initialize-" in the .coin folder

import os
import shutil
import datetime
import time

print("Coin Additions v1.0.0\n")

for file in os.listdir("./.coin"):
    if file.startswith("initialize-"):
        print("RUNNING " + file)
        os.system("python ./.coin/" + file)
        print("\n")

with open("./.coin/lastinit", "w") as f:
    f.write(str(datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")))

time.sleep(3)