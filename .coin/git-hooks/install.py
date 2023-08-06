# Author: Patryk Lesiak
# Date: 2023-08-03
# In case of any questions, please contact me via Discord (vlesio)

# This script setups git-hooks additions in the project
# IT SHOULD NOT BE RUN MANUALLY - run `python ./coinadds.py setup git-hooks` instead

import os
import sys
import shutil
import subprocess
import logging

# Switch to project directory
os.chdir(os.path.dirname(__file__)+'/../..')

# Is unity initialized?
if not os.path.exists('./Assets/Scripts'):
    print('Unity project seems not to be present.')
    sys.exit()

# Are needed resources present?
resources_needed = ['./.coin/git-hooks/resources/InitializeHooks.cs']
for res in resources_needed:
    if not os.path.exists(res):
        print(f'Resource {res} is missing.')
        sys.exit()

# Copy InitializeHooks.cs to Unity project
shutil.copy('./.coin/git-hooks/resources/InitializeHooks.cs', './Assets/Scripts/InitializeHooks.cs')

# Run load-hooks 
output = subprocess.run(['python', './.coin/git-hooks/load-hooks.py'], text=True, capture_output=True)
print(output.stdout.strip())

print('git-hooks has been setup')
